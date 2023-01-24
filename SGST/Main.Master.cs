using Common;
using System;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.Services;

namespace SGST
{
    public partial class Main : System.Web.UI.MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;
        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            //During the initial page load, add the Anti-XSRF token and user
            //name to the ViewState
            if (!IsPostBack)
            {
                //Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;

                //If a user name is assigned, set the user name
                ViewState[AntiXsrfUserNameKey] =
                Context.User.Identity.Name ?? String.Empty;
            }
            //During all subsequent post backs to the page, the token value from
            //the cookie should be validated against the token in the view state
            //form field. Additionally user name should be compared to the
            //authenticated users name
            else
            {
                //Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                || (string)ViewState[AntiXsrfUserNameKey] !=
                (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Invalid page access");
                }
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                string sData = Common.CommonHelper.sCompanyName;
                if (Session["UserID"] == null)
                {
                    Response.Redirect("~/Signin/v1/Login.aspx?Session=Null");
                    return;
                }
                //#region CSRF Gives Error While Logout
                ////First, check for the existence of the Anti-XSS cookie
                var requestCookie = Request.Cookies[AntiXsrfTokenKey];
                Guid requestCookieGuidValue;

                //If the CSRF cookie is found, parse the token from the cookie.
                //Then, set the global page variable and view state user
                //key. The global variable will be used to validate that it matches in the view state form field in the Page.PreLoad
                //method.
                if (requestCookie != null
                && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
                {
                    //Set the global token variable so the cookie value can be
                    //validated against the value in the view state form field in
                    //the Page.PreLoad method.
                    _antiXsrfTokenValue = requestCookie.Value;

                    //Set the view state user key, which will be validated by the
                    //framework during each request
                    Page.ViewStateUserKey = _antiXsrfTokenValue;
                }
                //If the CSRF cookie is not found, then this is a new session.
                else
                {
                    //Generate a new Anti-XSRF token
                    _antiXsrfTokenValue = Guid.NewGuid().ToString("N");

                    //Set the view state user key, which will be validated by the
                    //framework during each request
                    Page.ViewStateUserKey = _antiXsrfTokenValue;

                    //Create the non-persistent CSRF cookie
                    var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                    {
                        //Set the HttpOnly property to prevent the cookie from
                        //being accessed by client side script
                        HttpOnly = true,

                        //Add the Anti-XSRF token to the cookie value
                        Value = _antiXsrfTokenValue
                    };

                    //If we are using SSL, the cookie should be set to secure to
                    //prevent it from being sent over HTTP connections
                    if (FormsAuthentication.RequireSSL &&
                    Request.IsSecureConnection)
                        responseCookie.Secure = true;

                    //Add the CSRF cookie to the response
                    Response.Cookies.Set(responseCookie);
                }
                Page.PreLoad += master_Page_PreLoad;
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
                Response.AddHeader("Pragma", "no-cache");
                Response.AddHeader("Expires", "0");
                lblUserName.Text = Session["UserName"].ToString();
                lblUserID.Text = Session["UserID"].ToString();
                lblSiteCode.Text = Session["SiteCode"].ToString();
                lblLineCode.Text = Session["LINECODE"].ToString();
                CaptionHere.InnerHtml = Common.CommonHelper.sCompanyName;
                lblCompanyName.Text = Common.CommonHelper.sCompanyName;
                lblCompanyFullName.Text = ConfigurationManager.AppSettings["COMPANYNAME"].ToString();
                lblVersion.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
                //if (Session["SiteCode"].ToString() == "4")
                //{
                //    lstMOB.Visible = false;
                //}
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            try
            {
                if (HttpContext.Current.Session != null)
                {
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, " MasterPage:: btnLogOut_Click() ", "User LogOut From System :" + Session["UserID"].ToString());
                    Session["UserName"] = null;
                    Session["UserID"] = null;
                    Session["SiteCode"] = null;
                    // Add by sanchit for clear cookies
                    if (HttpContext.Current != null)
                    {
                        int cookieCount = HttpContext.Current.Request.Cookies.Count;
                        for (var i = 0; i < cookieCount; i++)
                        {
                            var cookie = HttpContext.Current.Request.Cookies[i];
                            if (cookie != null)
                            {
                                var cookieName = cookie.Name;
                                var expiredCookie = new HttpCookie(cookieName);
                                Response.Cookies[cookieName].Expires = DateTime.Now.AddDays(-1);
                                Response.Cookies[cookieName].Value = null;
                                HttpContext.Current.Response.Cookies.Add(expiredCookie); // overwrite it
                                HttpContext.Current.Response.SetCookie(expiredCookie);
                            }
                        }
                        // clear cookies server side

                    }
                    if (Response.Cookies.Count > 0)
                    {
                        foreach (string s in Response.Cookies.AllKeys)
                        {
                            if (s == FormsAuthentication.FormsCookieName || s.ToLower() == "asp.net_sessionid")
                            {
                                Response.Cookies[s].Secure = true;
                            }
                        }
                    }
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
                    Response.Cache.SetNoStore();
                    Session.Clear();
                    Session.RemoveAll();
                    Session.Abandon();                    
                    Response.Redirect("~/Signin/v1/Login.aspx",false);
                    Context.ApplicationInstance.CompleteRequest(); // end response
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError," MasterPage:: btnLogOut_Click() ", ex.Message);
            }
        }
        [WebMethod]
        public string AbandonSession()
        {
            string sResult = string.Empty;
            try
            {

                if (HttpContext.Current.Session != null)
                {
                    Session["UserName"] = null;
                    Session["UserID"] = null;
                    Session["SiteCode"] = null;

                    if (HttpContext.Current != null)
                    {
                        int cookieCount = HttpContext.Current.Request.Cookies.Count;
                        for (var i = 0; i < cookieCount; i++)
                        {
                            var cookie = HttpContext.Current.Request.Cookies[i];
                            if (cookie != null)
                            {
                                var cookieName = cookie.Name;
                                var expiredCookie = new HttpCookie(cookieName);
                                Response.Cookies[cookieName].Expires = DateTime.Now.AddDays(-1);
                                Response.Cookies[cookieName].Value = null;
                                HttpContext.Current.Response.Cookies.Add(expiredCookie); // overwrite it
                                HttpContext.Current.Response.SetCookie(expiredCookie);
                            }
                        }
                    }
                    if (Response.Cookies.Count > 0)
                    {
                        foreach (string s in Response.Cookies.AllKeys)
                        {
                            if (s == FormsAuthentication.FormsCookieName || s.ToLower() == "asp.net_sessionid")
                            {
                                Response.Cookies[s].Secure = true;
                            }
                        }
                    }
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
                    Response.Cache.SetNoStore();

                    Response.AddHeader("Cache-control", "no-store, must-revalidate, private,no-cache");
                    Response.AddHeader("Pragma", "no-cache");
                    Response.AddHeader("Expires", "0");

                    if (Request.Cookies["ASP.NET_SessionId"] != null)
                    {
                        Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                        Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);

                        Response.Cookies["__AntiXsrfToken"].Value = string.Empty;
                        Response.Cookies["__AntiXsrfToken"].Expires = DateTime.Now.AddMonths(-20);
                        Response.Cookies.Remove("ASP.NET_SessionId");
                        Response.Cookies.Remove("__AntiXsrfToken");
                    }
                    Session.Clear();
                    Session.RemoveAll();
                    Session.Abandon();
                }

            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, "MasterPage:: AbandonSession() ", ex.Message);
            }
            return sResult;
        }
    }
}