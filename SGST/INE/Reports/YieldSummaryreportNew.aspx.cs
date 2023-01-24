using BusinessLayer.Reports;
using Common;
using Microsoft.Reporting.WebForms;
using System;
using System.Data;
using System.Web.UI;

namespace SGST.INE.Reports
{
    public partial class YieldSummaryreportNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
            Response.AddHeader("Pragma", "no-cache");
            Response.AddHeader("Expires", "0");
            if (!IsPostBack)
            {
                try
                {
                    BindPartCode();
                    string a = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    txtFromDate.Text = a;
                    txtToDate.Text = a;
                }
                catch (Exception ex)
                {
                    CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                }
            }
        }
        private void BindPartCode()
        {
            try
            {
                BL_YIELD_Summary_Report blobj;
                blobj = new BL_YIELD_Summary_Report();
                DataTable dtGRN = blobj.BindPartCode();
                if (dtGRN.Rows.Count > 0)
                {
                    CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                    clsCommon.FillComboBox(drpFGItemCode, dtGRN, true);
                    drpFGItemCode.Focus();
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
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (drpFGItemCode.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select FG Item Code", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpFGItemCode.Focus();
                    return;
                }
                if (drpReportType.SelectedIndex == 1)
                {
                    if (drpMachine.Items.Count > 0)
                    {
                        if (drpMachine.SelectedIndex == 0)
                        {
                            CommonHelper.ShowMessage("Please select machine", msgerror, CommonHelper.MessageType.Error.ToString());
                            drpMachine.Focus();
                            return;
                        }                        
                    }
                }
                string sPartCode = string.Empty;
                string sMachineID = string.Empty;
                if (drpFGItemCode.SelectedIndex == 0)
                {
                    sPartCode = "";
                }
                else
                {
                    sPartCode = drpFGItemCode.Text;
                }
                if (drpMachine.SelectedIndex > 0)
                {
                    sMachineID = drpMachine.SelectedValue.ToString();
                }
                BL_YIELD_Summary_Report blobj;
                blobj = new BL_YIELD_Summary_Report();
                DataTable dt = blobj.GetReport(sPartCode, txtFromDate.Text, txtToDate.Text, drpReportType.Text, sMachineID
                    ,txtWorkOrderNo.Text.Trim()
                    );
                if (dt.Rows.Count == 0)
                {
                    CommonHelper.ShowMessage("No details found.", msgerror, CommonHelper.MessageType.Error.ToString());
                }
                else
                {
                    if (dt.Columns.Count == 1)
                    {
                        CommonHelper.ShowMessage(dt.Rows[0][0].ToString(), msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                    string Desc = string.Empty;
                    string sItemCode = string.Empty;
                    if (drpFGItemCode.SelectedIndex > 0)
                    {
                        Desc = dt.Rows[0]["FG_ITEM_DESC"].ToString();
                        sItemCode = dt.Rows[0]["FG_ITEM_CODE"].ToString();
                    }
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportDataSource datasource = new ReportDataSource();
                    if (drpReportType.SelectedIndex == 0)
                    {
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/ReportViewer/YieldSummery.rdlc");
                    }
                    else
                    {
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/ReportViewer/YieldDetailsReport.rdlc");
                    }
                    datasource = new ReportDataSource("DataSet1", dt);
                    ReportViewer1.Visible = true;
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasource);
                    ReportViewer1.LocalReport.EnableExternalImages = true;
                    string imagePath = new Uri(Server.MapPath("~/images/ReportLogo.png")).AbsoluteUri;
                    ReportParameter parameter = new ReportParameter("rptLogo", imagePath);
                    ReportParameter paramete = new ReportParameter("rptFGItemCode", sItemCode);
                    ReportParameter paramete2 = new ReportParameter("rptFGDesc", Desc);
                    ReportParameter paramete3 = new ReportParameter("rptFromDate", txtFromDate.Text);
                    ReportParameter paramete4 = new ReportParameter("rptToDate", txtToDate.Text);
                    ReportParameter paramete6 = new ReportParameter("rptWorkOrderNo", txtWorkOrderNo.Text.Trim());
                    ReportParameter paramete5 = new ReportParameter("rptCurrentDate", DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                    ReportViewer1.LocalReport.SetParameters(parameter);
                    ReportViewer1.LocalReport.SetParameters(paramete);
                    ReportViewer1.LocalReport.SetParameters(paramete2);
                    ReportViewer1.LocalReport.SetParameters(paramete3);
                    ReportViewer1.LocalReport.SetParameters(paramete4);
                    ReportViewer1.LocalReport.SetParameters(paramete5);
                    ReportViewer1.LocalReport.SetParameters(paramete6);
                    ReportViewer1.LocalReport.Refresh();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ShowMessage();", true);
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void drpFGItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpFGItemCode.SelectedIndex > 0)
                {
                    BL_YIELD_Summary_Report blobj;
                    blobj = new BL_YIELD_Summary_Report();
                    DataTable dtGRN = blobj.BindMachineID(drpFGItemCode.Text);
                    if (dtGRN.Rows.Count > 0)
                    {
                        CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                        clsCommon.FillMultiColumnsCombo(drpMachine, dtGRN, true);
                        drpMachine.Focus();
                    }
                    else
                    {
                        CommonHelper.ShowMessage("No data found.", msginfo, CommonHelper.MessageType.Info.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}