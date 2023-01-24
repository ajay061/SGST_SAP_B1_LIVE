using System;
using System.Configuration;

namespace SGST.INE.v1
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Signin/v1/Login.aspx?Session=Null");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
            Response.AddHeader("Pragma", "no-cache");
            Response.AddHeader("Expires", "0");
            string sCompanyName = ConfigurationManager.AppSettings["PROJECTNAME"].ToString();
            string sComapnyName1 = ConfigurationManager.AppSettings["PROJECTNAME1"].ToString();
            if (sComapnyName1.Length > 0)
            {
                lblCompanyName1.Text = sComapnyName1;
            }
            if (sCompanyName.Length > 0)
            {
                lblCompanyName.Text = sCompanyName;
            }            
        }
    }
}