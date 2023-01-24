using Common;
using System;
using System.Data;
using System.Web.UI;
using BusinessLayer.Reports;
using Microsoft.Reporting.WebForms;

namespace SGST.INE.Reports
{
    public partial class SAPPostingReport : System.Web.UI.Page
    {
        static DataTable dtTrackingData = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
            Response.AddHeader("Pragma", "no-cache");
            Response.AddHeader("Expires", "0");
        }
        private void ShowGridData()
        {
            try
            {
                string sType = string.Empty;
                sType = drpType.Text;
                dtTrackingData.Rows.Clear();
                BL_SAPPostingDataReport blobj = new BL_SAPPostingDataReport();
                dtTrackingData = blobj.GetReport(sType, txtOrderNo.Text.Trim());
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
                    if (drpType.Text == "RM Quality Posting")
                    {
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/ReportViewer/RMQualityPostingReport.rdlc");
                    }
                    else if (drpType.Text == "RM Issue Posting")
                    {
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/ReportViewer/RMIssuePostingReport.rdlc");
                    }
                    else if (drpType.Text == "FG Posting")
                    {
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/ReportViewer/FGPostingReport.rdlc");
                    }
                    else if (drpType.Text == "FG Quality Posting")
                    {
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/ReportViewer/FGQualityPostingReport.rdlc");
                    }
                    else if (drpType.Text == "Work Order Close")
                    {
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/ReportViewer/WOClosePostingReport.rdlc");
                    }

                    datasource = new ReportDataSource("DataSet1", dtTrackingData);
                    ReportViewer1.Visible = true;
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasource);
                    ReportViewer1.LocalReport.EnableExternalImages = true;
                    string Barcode = string.Empty;
                    string imagePath = new Uri(Server.MapPath("~/images/ReportLogo.png")).AbsoluteUri;
                    ReportParameter parameter = new ReportParameter("rptLogo", imagePath);                   
                    ReportParameter paramete2 = new ReportParameter("rptPackedOn", DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                    ReportParameter paramete1 = new ReportParameter("rptWorkOrderNo", txtOrderNo.Text.Trim());
                    ReportViewer1.LocalReport.SetParameters(parameter);
                    ReportViewer1.LocalReport.SetParameters(paramete1);
                    ReportViewer1.LocalReport.SetParameters(paramete2);
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
    }
}