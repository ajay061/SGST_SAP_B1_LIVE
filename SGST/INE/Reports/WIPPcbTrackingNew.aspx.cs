using Common;
using System;
using System.Data;
using System.Web.UI;
using BusinessLayer.Reports;
using Microsoft.Reporting.WebForms;

namespace SGST.INE.Reports
{
    public partial class PcbTrackingNew : System.Web.UI.Page
    {
        static DataTable dtTrackingData = new DataTable();
        public void BindMachineID()
        {
            try
            {
                BL_PCBTrackingReport blobj = new BL_PCBTrackingReport();
                DataTable dt = blobj.GetMachineID();
                if (dt.Rows.Count > 0)
                {
                    CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                    clsCommon.FillComboBox(drpMachineID, dt, true);
                    drpMachineID.Focus();
                }
                else
                {
                    CommonHelper.ShowMessage("No data found.", msginfo, CommonHelper.MessageType.Info.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        public void BindFGItemCode()
        {
            try
            {
                BL_WIPReworkReport blobj = new BL_WIPReworkReport();
                DataTable dt = blobj.GetFGItemCode();
                if (dt.Rows.Count > 0)
                {
                    CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                    clsCommon.FillComboBox(drpFGitemCode, dt, true);
                    drpFGitemCode.Focus();
                }
                else
                {
                    CommonHelper.ShowMessage("No data found.", msginfo, CommonHelper.MessageType.Info.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
            Response.AddHeader("Pragma", "no-cache");
            Response.AddHeader("Expires", "0");
            try
            {
                if (!Page.IsPostBack)
                {
                    string a = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    txtFromDate.Text = a;
                    txtToDate.Text = a;
                    DivPCB.Visible = false;
                    divRMBarcode.Visible = false;
                    divRMBatchNo.Visible = false;
                    dvReportType.Visible = true;
                    divGRPONo.Visible = false;
                    BindMachineID();
                    BindFGItemCode();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        private void ShowGridData()
        {
            try
            {
                string sFGItemCode = string.Empty;
                string sMachineID = string.Empty;
                string sBarocode = string.Empty;
                if (drpFGitemCode.SelectedIndex > 0)
                {
                    sFGItemCode = drpFGitemCode.Text;
                }
                if (drpMachineID.SelectedIndex > 0)
                {
                    sMachineID = drpMachineID.Text;
                }
                if (drpType.Text == "Date")
                {
                    sBarocode = "";
                }
                else if (drpType.Text == "PCB Barcode")
                {
                    sBarocode = txtPCBBarcode.Text;
                }
                else if (drpType.Text == "RM Batch No")
                {
                    drpReportType.SelectedIndex = 0;
                    sBarocode = txtRMBatchNo.Text;
                }
                else if (drpType.Text == "RM Barcode")
                {
                    drpReportType.SelectedIndex = 0;
                    sBarocode = txtRMBarcode.Text;
                }
                if (drpType.Text == "Date" && drpFGitemCode.SelectedIndex == 0)
                {
                    if ((Convert.ToDateTime(txtToDate.Text).Day - Convert.ToDateTime(txtToDate.Text).Day) > 1)
                    {
                        CommonHelper.ShowCustomErrorMessage("Please select FG Item Code", msgerror);
                        drpFGitemCode.Focus();
                        return;
                    }                   
                    
                }
                else if (drpType.Text == "RM GRPO No")
                {
                    drpReportType.SelectedIndex = 0;
                    sBarocode = txtGrpoNo.Text;
                }
                dtTrackingData.Rows.Clear();
                BL_PCBTrackingReport blobj = new BL_PCBTrackingReport();
                dtTrackingData = blobj.GetReport(sMachineID, txtFromDate.Text,
                    txtToDate.Text, sFGItemCode, drpType.Text, sBarocode
                    , txtWorkOrderNo.Text, drpReportType.Text
                    );
                if (dtTrackingData.Rows.Count == 0)
                {
                    CommonHelper.ShowCustomErrorMessage("No Records Found", msgerror);
                    drpFGitemCode.Focus();
                }
                else if (dtTrackingData.Columns.Count == 1)
                {
                    CommonHelper.ShowCustomErrorMessage(dtTrackingData.Rows[0][0].ToString(), msgerror);
                    drpFGitemCode.Focus();
                }
                else
                {
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtMessage,
                        System.Reflection.MethodBase.GetCurrentMethod().Name, "PCB Tracking Report, Report Type : " + drpType.Text + ", No of records :" + dtTrackingData.Rows.Count);
                    string sFGItemCodeData = string.Empty;
                    string Barcode = string.Empty;
                    string WorkOrderNo = string.Empty;
                    string FGDesc = string.Empty;
                    string Customer = string.Empty;
                    string Box = string.Empty;
                    string PackedBy = string.Empty;
                    string PackedOn = string.Empty;
                    string sFromDate = string.Empty;
                    string sToDate = string.Empty;
                    string sReportHeader = string.Empty;                    
                    if (drpType.Text == "PCB Barcode")
                    {
                        sFGItemCodeData = dtTrackingData.Rows[0]["FG_ITEM_CODE"].ToString();
                        Barcode = dtTrackingData.Rows[0]["Barcode"].ToString();
                        WorkOrderNo = dtTrackingData.Rows[0]["Work_Order_No"].ToString();
                        FGDesc = dtTrackingData.Rows[0]["FG_ITEM_DESC"].ToString();
                        Customer = dtTrackingData.Rows[0]["Customer_Code"].ToString();
                        Box = dtTrackingData.Rows[0]["Box_ID"].ToString();
                        PackedBy = dtTrackingData.Rows[0]["Packed_By"].ToString();
                        PackedOn = dtTrackingData.Rows[0]["Packed_On"].ToString();                        
                        sReportHeader = "PCB TRACKING REPORT";
                    }
                    if (drpType.Text == "Date")
                    {
                        sFGItemCodeData = dtTrackingData.Rows[0]["FG_ITEM_CODE"].ToString();
                        FGDesc = dtTrackingData.Rows[0]["FG_ITEM_DESC"].ToString();
                        Customer = dtTrackingData.Rows[0]["Customer_Code"].ToString();
                        sFromDate = txtFromDate.Text;
                        sToDate = txtToDate.Text;
                        sReportHeader = "DATE WISE PCB TRACKING REPORT";
                    }                    
                    if (drpType.Text == "RM Batch No")
                    {
                        sReportHeader = "BATCH WISE TRACKING REPORT";
                    }
                    if (drpType.Text == "RM GRPO No")
                    {
                        sReportHeader = "GRPO NO TRACKING REPORT";
                    }
                    if (drpType.Text == "RM Barcode")
                    {
                        sReportHeader = "RM MATERIAL WISE TRACKING REPORT";
                    }
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportDataSource datasource = new ReportDataSource();
                    if (drpReportType.Text == "Details")
                    {
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/ReportViewer/PCBTrackingReport.rdlc");
                    }
                    else
                    {
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/ReportViewer/PCBTrackingReport_Summary.rdlc");
                    }
                    datasource = new ReportDataSource("PCBTrackingReport", dtTrackingData);
                    ReportViewer1.Visible = true;
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasource);
                    ReportViewer1.LocalReport.EnableExternalImages = true;
                    string imagePath = new Uri(Server.MapPath("~/images/ReportLogo.png")).AbsoluteUri;
                    ReportParameter parameter = new ReportParameter("rptLogo", imagePath);
                    ReportParameter paramete1 = new ReportParameter("rptReportType", drpType.Text);
                    ReportParameter paramete = new ReportParameter("rptFGItemCode", sFGItemCodeData);
                    ReportParameter paramete2 = new ReportParameter("rptBarcode", Barcode);
                    ReportParameter paramete3 = new ReportParameter("rptWorkOrderNo", WorkOrderNo);
                    ReportParameter paramete4 = new ReportParameter("rptFGDesc", FGDesc);
                    ReportParameter paramete5 = new ReportParameter("rptCustomerCode", Customer);
                    ReportParameter paramete6 = new ReportParameter("rptBoxID", Box);
                    ReportParameter paramete7 = new ReportParameter("rptPackedBy", PackedBy);
                    ReportParameter paramete8 = new ReportParameter("rptPackedOn", PackedOn);
                    ReportParameter paramete9 = new ReportParameter("rptFromDate", sFromDate);
                    ReportParameter paramete10 = new ReportParameter("rptToDate", sToDate);
                    ReportParameter paramete11 = new ReportParameter("rptType", drpReportType.Text);
                    ReportParameter paramete12 = new ReportParameter("rptHeader", sReportHeader);
                    string sHeaderRequired = "1";
                    if (drpReportDisplay.SelectedIndex == 1)
                    {
                        sHeaderRequired = "0";
                    }
                    ReportParameter paramete13 = new ReportParameter("rptHeaderRequired", sHeaderRequired);
                    ReportViewer1.LocalReport.SetParameters(parameter);
                    ReportViewer1.LocalReport.SetParameters(paramete);
                    ReportViewer1.LocalReport.SetParameters(paramete1);
                    ReportViewer1.LocalReport.SetParameters(paramete2);
                    ReportViewer1.LocalReport.SetParameters(paramete3);
                    ReportViewer1.LocalReport.SetParameters(paramete4);
                    ReportViewer1.LocalReport.SetParameters(paramete5);
                    ReportViewer1.LocalReport.SetParameters(paramete6);
                    ReportViewer1.LocalReport.SetParameters(paramete7);
                    ReportViewer1.LocalReport.SetParameters(paramete8);
                    ReportViewer1.LocalReport.SetParameters(paramete9);
                    ReportViewer1.LocalReport.SetParameters(paramete10);
                    ReportViewer1.LocalReport.SetParameters(paramete11);
                    ReportViewer1.LocalReport.SetParameters(paramete12);
                    ReportViewer1.LocalReport.SetParameters(paramete13);
                    
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
                else if (ex.Message.Contains("Execution Timeout Expired"))
                {
                    CommonHelper.ShowCustomErrorMessage("Data retrieving fail, Please select different dates", msgerror);
                }
                else
                {
                    CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                }
            }
        }

        protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                divDate.Visible = false;
                DivPCB.Visible = false;
                divRMBarcode.Visible = false;
                divRMBatchNo.Visible = false;
                divGRPONo.Visible = false;
                txtRMBarcode.Text = string.Empty;
                txtPCBBarcode.Text = string.Empty;
                txtRMBatchNo.Text = string.Empty;
                drpFGitemCode.SelectedIndex = 0;
                txtGrpoNo.Text = string.Empty;
                drpMachineID.SelectedIndex = 0;
                string a = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                txtFromDate.Text = a;
                txtToDate.Text = a;
                dvReportType.Visible = true;
                if (drpType.Text == "Date")
                {
                    divDate.Visible = true;
                }
                else if (drpType.Text == "PCB Barcode")
                {
                    DivPCB.Visible = true;
                }
                else if (drpType.Text == "RM Batch No")
                {
                    dvReportType.Visible = false;
                    divRMBatchNo.Visible = true;
                }
                else if (drpType.Text == "RM Barcode")
                {
                    dvReportType.Visible = false;
                    divRMBarcode.Visible = true;
                }
                else if (drpType.Text == "RM GRPO No")
                {
                    dvReportType.Visible = false;
                    divGRPONo.Visible = true;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
    }
}