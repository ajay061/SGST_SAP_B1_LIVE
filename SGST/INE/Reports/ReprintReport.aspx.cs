using BusinessLayer.Reports;
using Common;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGST.INE.Reports
{
    public partial class ReprintReport : System.Web.UI.Page
    {
        static DataTable dtTrackingData = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
            Response.AddHeader("Pragma", "no-cache");
            Response.AddHeader("Expires", "0");
            if (Session["usertype"].ToString() != "ADMIN")
            {
                string _strRights = CommonHelper.GetRights("Reprint Report", (DataTable)Session["USER_RIGHTS"]);
                CommonHelper._strRights = _strRights.Split('^');
                if (CommonHelper._strRights[0] == "0")  //Check view rights
                {
                    Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                }
            }
            if (!IsPostBack)
            {
                try
                {
                   
                    string current_date = DateTime.Now.ToString("yyyy-MM-dd");
                    txtFromDate.Text = current_date;
                    txtToDate.Text = current_date;
                }
                catch (Exception ex)
                {
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                }
            }
            
        }
        protected void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                ShowGridData();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                if (ex.Message.Contains("System.OutOfMemoryException"))
                {
                    CommonHelper.ShowCustomErrorMessage("System Memory is full, Please select different dates", msgerror);
                }
                else
                {
                    CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                }
            }
        }
        private void ShowGridData()
        {
            try
            {
                string sType = string.Empty;
                sType = drpType.Text;
                dtTrackingData.Rows.Clear();
                if (txtFromDate.Text == "" || txtToDate.Text == "")
                {
                    CommonHelper.ShowMessage("Dates are mandatory.", msgerror, CommonHelper.MessageType.Error.ToString());
                    return;
                }
                
                BL_SAPPostingDataReport blobj = new BL_SAPPostingDataReport();
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/ReportViewer/ReprintReport.rdlc");
                dtTrackingData = blobj.GetReprintReport(sType, txtFromDate.Text, txtToDate.Text);
                if (dtTrackingData.Rows.Count == 0)
                {
                    CommonHelper.ShowMessage("No details found.", msgerror, CommonHelper.MessageType.Error.ToString());
                }
                else
                {
                    if (dtTrackingData.Columns.Count == 1)
                    {
                        CommonHelper.ShowMessage(dtTrackingData.Rows[0][0].ToString(), msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                    ReportDataSource datasource = new ReportDataSource();
                    datasource = new ReportDataSource("DataSet1", dtTrackingData);
                    ReportViewer1.Visible = true;
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasource);
                    ReportViewer1.LocalReport.EnableExternalImages = true;
                    //ReportParameter paramete2 = new ReportParameter("DateOFReprint", DateTime.Now.ToString("dd-MM-yyyy"));
                    string imagePath = new Uri(Server.MapPath("~/images/ReportLogo.png")).AbsoluteUri;
                    ReportParameter parameter = new ReportParameter("rptLogo", imagePath);
                    
                    ReportParameter paramete1 = new ReportParameter("rptFromDate", txtFromDate.Text);
                    ReportParameter paramete2 = new ReportParameter("rptToDate", txtToDate.Text);
                    ReportViewer1.LocalReport.SetParameters(parameter);
                    ReportViewer1.LocalReport.SetParameters(paramete1);
                    ReportViewer1.LocalReport.SetParameters(paramete2);
                    //ReportViewer1.LocalReport.SetParameters(paramete2);
                    ReportViewer1.LocalReport.Refresh();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ShowMessage();", true);
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
    }
}