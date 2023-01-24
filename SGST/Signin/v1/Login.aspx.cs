using BusinessLayer;
using Common;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SGST.Signin.v1
{
    public partial class Login : System.Web.UI.Page
    {
        string sLoginExpired = ConfigurationManager.AppSettings["_TimeExpired"].ToString();
        string sMaxWorkingDate = ConfigurationManager.AppSettings["MAXWORKDATE"].ToString();
        string sCompanyName = ConfigurationManager.AppSettings["SMALLCOMPANYNAME"].ToString();
        BL_UserLogin blobj = new BL_UserLogin();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                CaptionHere.InnerHtml = sCompanyName;
                lblVersion.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
                if (!IsPostBack)
                {
                    if (Request.Cookies["UserName"] != null)
                    {
                        txtUID.Text = Request.Cookies["UserName"].Value;
                        txtUID.Focus();
                    }
                    txtSiteCode.Text = ConfigurationManager.AppSettings["SITECODE"].ToString();
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);

            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-30);
            Response.Cookies["SiteCode"].Expires = DateTime.Now.AddDays(-30);
            Response.Cookies["UserName"].Value = txtUID.Text.Trim();
            try
            {
                if (sLoginExpired == "1")
                {
                    if (System.DateTime.Now > Convert.ToDateTime(sMaxWorkingDate))
                    {
                        Response.Write("<script LANGUAGE='JavaScript' >alert('Application Login expired, Please contact BCI')</script>");
                        return;
                    }
                }
                blobj = new BL_UserLogin();
                DataSet ds = blobj.GetSiteCode();
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    int iValidate = 0;
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (dr[0].ToString().ToUpper() == txtSiteCode.Text.Trim().ToUpper())
                            {
                                Session["SiteCode"] = txtSiteCode.Text.Trim();
                                PCommon.sSiteCode = Session["SiteCode"].ToString();
                                iValidate = 1;
                                break;
                            }
                        }
                        if (iValidate == 0)
                        {
                            Response.Write("<script LANGUAGE='JavaScript' >alert('Enter site details not found in database, Please contact admin')</script>");
                            return;
                        }
                    }
                    dt = new DataTable();
                    dt = ds.Tables[1];
                    iValidate = 0;
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (dr[0].ToString().ToUpper() == txtLineCode.Text.Trim().ToUpper())
                            {
                                Session["LINECODE"] = txtLineCode.Text.Trim();
                                iValidate = 1;
                                break;
                            }
                        }
                        if (iValidate == 0)
                        {
                            Response.Write("<script LANGUAGE='JavaScript' >alert('Enter Line details not found in database, Please contact admin')</script>");
                            return;
                        }
                    }
                }
                else
                {
                    Response.Write("<script LANGUAGE='JavaScript' >alert('No site details found. Please enter site details in database.')</script>");
                    return;
                }
                string sResult = blobj.UserLogin(txtUID.Text.Trim(), Encrypt(txtpassword.Text.Trim()), Session["SiteCode"].ToString());
                if (sResult.Length > 0)
                {
                    if (sResult.StartsWith("SUCCESS~"))
                    {
                        DataTable dt = new DataTable();
                        dt = blobj.GetuserDetails(txtUID.Text.ToUpper());
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0]["Active"].ToString() == "0")
                            {
                                Response.Write("<script LANGUAGE='JavaScript' >alert('Entered user is not active, Please contact admin')</script>");
                                txtUID.Text = string.Empty;
                                txtUID.Focus();
                                return;
                            }
                            Session["LINECODE"] = txtLineCode.Text.Trim().ToUpper();
                            Session["USERNAME"] = dt.Rows[0]["USERNAME"].ToString().ToUpper();
                            Session["UserID"] = dt.Rows[0]["USERID"].ToString().ToUpper();
                            Session["Department"] = dt.Rows[0]["DEPARTMENT"].ToString().ToUpper();
                            Session["UserType"] = dt.Rows[0]["USERTYPE"].ToString().ToUpper();
                        }
                        else
                        {
                            Response.Write("<script LANGUAGE='JavaScript' >alert('User ID and Password not matched, Please enter another id')</script>");
                            txtUID.Text = string.Empty;
                            txtUID.Focus();
                            return;
                        }
                        BL_UserLogin _BL_UserLogin = new BL_UserLogin();
                        DataTable dtGrpRights = _BL_UserLogin.GetGroupRights(txtUID.Text.Trim(), Session["SiteCode"].ToString());
                        if (dtGrpRights.Rows.Count > 0 || Session["UserType"].ToString() == "ADMIN")
                        {
                            if (Session["UserType"].ToString().ToUpper() != "ADMIN")
                            {
                                Session["USER_RIGHTS"] = dtGrpRights;
                            }
                        }
                        else
                        {
                            Response.Write("<script LANGUAGE='JavaScript' >alert('No rights assigned. Kindly contact your administrator')</script>");
                            return;
                        }
                        Common.CommonHelper.sValue = ConfigurationManager.AppSettings["ERRORTYPE"].ToString();
                        Common.CommonHelper.sCustomMessage = ConfigurationManager.AppSettings["CUSTOMMESSAGE"].ToString();
                        Common.CommonHelper.sBYPassLogin = ConfigurationManager.AppSettings["PRINTBYPASS"].ToString();
                        Common.CommonHelper.sCompanyName = sCompanyName;
                        PCommon.sUseNetworkPrinter = ConfigurationManager.AppSettings["_NETWORKPRINTER"].ToString();
                        PCommon.sPrinterPort = ConfigurationManager.AppSettings["sPrinterPort"].ToString();
                        PCommon.sServerSidePrintingPath = ConfigurationManager.AppSettings["_SERVERSIDEFOLDER"].ToString();
                        PCommon.sClientPrintingPath = ConfigurationManager.AppSettings["_CLIENTSIDEFOLDER"].ToString();

                        Response.Redirect("~/INE/v1/Home.aspx", false);
                    }
                    if (sResult.StartsWith("N~"))
                    {
                        Response.Write("<script LANGUAGE='JavaScript' >alert('Invalid User ID and Password')</script>");
                    }
                    if (sResult.StartsWith("ERROR~"))
                    {
                        Response.Write("<script LANGUAGE='JavaScript' >alert('Invalid User ID and Password')</script>");
                    }
                }
                else
                {
                    Response.Write("<script LANGUAGE='JavaScript' >alert('Invalid User ID and Password')</script>");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("The type initializer for 'COMMON.PCommon' threw an exception"))
                {
                    Response.Write("<script LANGUAGE='JavaScript' >alert('Application Connection Failed')</script>");
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, " UserLogin:: btnLogin_Click() ", "Connection string is not available.");
                }
                else if (ex.Message.Contains("Login failed for user"))
                {
                    Response.Write("<script LANGUAGE='JavaScript' >alert('Application Connection Failed')</script>");
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, "" + "  ::  UserLogin:: btnLogin_Click() ", "Wrong user id or password for DB provided in config. file.");
                }
                else if (ex.Message.Contains("Cannot open database"))
                {
                    Response.Write("<script LANGUAGE='JavaScript' >alert('Application Connection Failed')</script>");
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, "" + "  ::  UserLogin:: btnLogin_Click() ", "Database name is not correct in config. file.");
                }
                else if (ex.Message.Contains("A network-related or instance-specific"))
                {
                    Response.Write("<script LANGUAGE='JavaScript' >alert('Application Connection Failed')</script>");
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, "" + "  ::  UserLogin:: btnLogin_Click() ", "Server name or DB name is not correct in config. file.");
                }
                else if (ex.Message.Contains("There is no row at position"))
                {
                    Response.Write("<script LANGUAGE='JavaScript' >alert('Application Connection Failed')</script>");
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, "" + "  ::  UserLogin:: btnLogin_Click() ", "Server name or DB name is not correct in config. file.");
                }
                else
                {
                    Response.Write("<script LANGUAGE='JavaScript' >alert('Application Connection Failed')</script>");
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, "" + "  ::  UserLogin:: btnLogin_Click() ", ex.Message);
                }
            }
        }
        private string Encrypt(string clearText)
        {
            try
            {
                string EncryptionKey = "MAKV2SPBNI99212";
                byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        clearText = Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return clearText;
        }
    }
}