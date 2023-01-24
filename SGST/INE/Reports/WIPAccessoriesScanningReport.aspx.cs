using Common;
using System;
using System.Data;
using System.Web.UI;
using BusinessLayer.Reports;
using Microsoft.Reporting.WebForms;

namespace SGST.INE.Reports
{
    public partial class WIPAccessoriesScanningReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usertype"].ToString() != "ADMIN")
            {
                string _strRights = CommonHelper.GetRights("ACCESSORIES SCANNING REPORT", (DataTable)Session["USER_RIGHTS"]);
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
                    BindPart_No();
                    string a = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    txtFromDate.Text = a;
                    txtToDate.Text = a;
                }
                catch (Exception ex)
                {
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                }
            }
        }
        private void BindPart_No()
        {
            try
            {
                BL_ASNReport blobj;
                blobj = new BL_ASNReport();
                DataTable dtGRN = blobj.BindAccessoriesFGitemCode();
                if (dtGRN.Rows.Count > 0)
                {
                    CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                    clsCommon.FillComboBox(drpFGitemCode, dtGRN, true);
                    drpFGitemCode.Focus();
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
                BL_ASNReport blobj;
                blobj = new BL_ASNReport();
                string sPartCode = string.Empty;
                if (drpFGitemCode.SelectedIndex > 0)
                {
                    sPartCode = drpFGitemCode.Text;
                }
                DataTable dtTrackingData = blobj.GetReportAccessoriesScanning(
                    txtFromDate.Text, txtToDate.Text, sPartCode,txtWorkOrderNo.Text.Trim());
                if (dtTrackingData.Rows.Count == 0)
                {
                    CommonHelper.ShowCustomErrorMessage("No Records Found", msgerror);
                }
                else
                {
                    if (dtTrackingData.Columns.Count == 1)
                    {
                        CommonHelper.ShowMessage(dtTrackingData.Rows[0][0].ToString(), msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtMessage,
                         System.Reflection.MethodBase.GetCurrentMethod().Name, "Report No of records :" + dtTrackingData.Rows.Count);
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportDataSource datasource = new ReportDataSource();
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/ReportViewer/AccessoriesScanningReport.rdlc");
                    datasource = new ReportDataSource("DataSet1", dtTrackingData);
                    ReportViewer1.Visible = true;
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasource);
                    ReportViewer1.LocalReport.EnableExternalImages = true;
                    string Barcode = string.Empty;
                    string OrderNo = string.Empty;
                    string Desc = string.Empty;
                    string sItemCode = string.Empty;
                    if (drpFGitemCode.SelectedIndex > 0)
                    {
                        sItemCode = dtTrackingData.Rows[0]["FG_ITEM_CODE"].ToString();
                        Desc = dtTrackingData.Rows[0]["FG_ITEM_DESC"].ToString();
                    }                  
                    string imagePath = new Uri(Server.MapPath("~/images/ReportLogo.png")).AbsoluteUri;
                    ReportParameter parameter = new ReportParameter("rptLogo", imagePath);
                    ReportParameter paramete = new ReportParameter("rptFGItemCode", sItemCode);
                    ReportParameter paramete1 = new ReportParameter("rptWorkOrderNo", OrderNo);
                    ReportParameter paramete2 = new ReportParameter("rptFGDesc", Desc);
                    ReportParameter paramete3 = new ReportParameter("rptFromDate", txtFromDate.Text);
                    ReportParameter paramete4 = new ReportParameter("rptToDate", txtToDate.Text);
                    ReportParameter paramete5 = new ReportParameter("rptPackedOn", DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                    ReportViewer1.LocalReport.SetParameters(parameter);
                    ReportViewer1.LocalReport.SetParameters(paramete);
                    ReportViewer1.LocalReport.SetParameters(paramete1);
                    ReportViewer1.LocalReport.SetParameters(paramete2);
                    ReportViewer1.LocalReport.SetParameters(paramete3);
                    ReportViewer1.LocalReport.SetParameters(paramete4);
                    ReportViewer1.LocalReport.SetParameters(paramete5);
                    ReportViewer1.LocalReport.Refresh();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ShowMessage();", true);
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {
                txtWorkOrderNo.Text = string.Empty;
            }
        }
    }
}