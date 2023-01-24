using BusinessLayer.Reports;
using Common;
using Microsoft.Reporting.WebForms;
using System;
using System.Data;
using System.Web.UI;
namespace SGST.INE.Reports
{
    public partial class Squeezee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usertype"].ToString() != "ADMIN")
            {
                string _strRights = CommonHelper.GetRights("TOOL REPORT", (DataTable)Session["USER_RIGHTS"]);
                CommonHelper._strRights = _strRights.Split('^');
                if (CommonHelper._strRights[0] == "0")  //Check view rights
                {
                    Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                }
            }
            Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
            Response.AddHeader("Pragma", "no-cache");
            Response.AddHeader("Expires", "0");
            if (!IsPostBack)
            {
                try
                {
                    BindTOOL();
                    string current_date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    txtFromDate.Text = current_date;
                    txtToDate.Text = current_date;
                }
                catch (Exception ex)
                {
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                }
            }
        }
        private void BindTOOL()
        {
            try
            {
                BL_WIP_TOOL_Report blobj;
                blobj = new BL_WIP_TOOL_Report();
                DataTable dtGRN = blobj.BindTOOl();
                if (dtGRN.Rows.Count > 0)
                {
                    CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                    clsCommon.FillComboBox(drptools, dtGRN, true);
                    drptools.Focus();
                }
                else
                {
                    CommonHelper.ShowMessage("No data found.", msginfo, CommonHelper.MessageType.Info.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportDataSource datasource = new ReportDataSource();
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);

                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/ReportViewer/SQUEZEERDLC.rdlc");
                BL_WIP_TOOL_Report blobj;
                blobj = new BL_WIP_TOOL_Report();
                DataTable dtGRN = new DataTable();
                if (drpReportType.Text == "Date Wise Count")
                {
                    dtGRN = blobj.GetToolDetailsReport(txtFromDate.Text, txtToDate.Text, drptools.Text);
                }
                else
                {
                    dtGRN = blobj.GetReport(drptools.Text);
                }
                datasource = new ReportDataSource("DataSet1", dtGRN);
                if (dtGRN.Rows.Count == 0)
                {
                    CommonHelper.ShowMessage("No details found.", msgerror, CommonHelper.MessageType.Error.ToString());
                    return;
                }
                ReportViewer1.Visible = true;
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                ReportViewer1.LocalReport.EnableExternalImages = true;
                string imagePath = new Uri(Server.MapPath("~/images/ReportLogo.png")).AbsoluteUri;
                ReportParameter parameter = new ReportParameter("rptLogo", imagePath);
                ReportParameter paramete1 = new ReportParameter("rptFromDate", txtFromDate.Text);
                ReportParameter paramete2 = new ReportParameter("rptToDate", txtToDate.Text);
                ReportViewer1.LocalReport.SetParameters(paramete1);
                ReportViewer1.LocalReport.SetParameters(paramete2);
                ReportViewer1.LocalReport.SetParameters(parameter);
                ReportViewer1.LocalReport.Refresh();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ShowMessage();", true);
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void drpReportType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}