using Common;
using System;
using System.Data;
using System.Web.UI;
using BusinessLayer.Reports;
using Microsoft.Reporting.WebForms;

namespace SGST.INE.Reports
{
    public partial class WIPReworkReport : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Signin/v1/Login.aspx?Session=Null");
            }
        }
        static DataTable dtRepairReport = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usertype"].ToString() != "ADMIN")
            {
                string _strRights = CommonHelper.GetRights("WIP REWORK REPORT", (DataTable)Session["USER_RIGHTS"]);
                CommonHelper._strRights = _strRights.Split('^');
                if (CommonHelper._strRights[0] == "0")  //Check view rights
                {
                    Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                }
            }
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
                    BindMachineID();
                    BindFGItemCode();
                    BindReworkStationID();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        public void BindMachineID()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                BL_WIPReworkReport blobj = new BL_WIPReworkReport();
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
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        public void BindFGItemCode()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
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
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        public void BindReworkStationID()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                BL_WIPReworkReport blobj = new BL_WIPReworkReport();
                DataTable dt = blobj.GetReworkSationId();
                if (dt.Rows.Count > 0)
                {
                    CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                    clsCommon.FillComboBox(REWORK_STATION_ID, dt, true);
                    REWORK_STATION_ID.Focus();
                }
                else
                {
                    CommonHelper.ShowMessage("No data found.", msginfo, CommonHelper.MessageType.Info.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        private void ShowGridData(string from, string to, string sMachineID, string sType,
            string sFGItemCode,string sReworkStID)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                dtRepairReport.Rows.Clear();
                BL_WIPReworkReport blobj = new BL_WIPReworkReport();
                dtRepairReport = blobj.GetReport(sMachineID, from, to, sFGItemCode, sType,txtWorkOrderNo.Text.Trim()
                    ,txtPCBID.Text.Trim(), sReworkStID
                    );
                if (dtRepairReport.Rows.Count == 0)
                {
                    CommonHelper.ShowCustomErrorMessage("No Records Found", msgerror);
                }
                else
                {
                    if (dtRepairReport.Columns.Count == 1)
                    {
                        CommonHelper.ShowMessage(dtRepairReport.Rows[0][0].ToString(), msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtMessage,
                          System.Reflection.MethodBase.GetCurrentMethod().Name, "Repair Report No of records :" + dtRepairReport.Rows.Count);
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportDataSource datasource = new ReportDataSource();
                    if (drpType.Text == "Repair Out")
                    {
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/ReportViewer/WIPReworkOutReport.rdlc");
                    }
                    else
                    {
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/ReportViewer/WIPReworkReport.rdlc");
                    }
                    datasource = new ReportDataSource("DataSet1", dtRepairReport);
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
                        sItemCode = dtRepairReport.Rows[0]["FG_ITEM_CODE"].ToString();
                        Desc = dtRepairReport.Rows[0]["FG_ITEM_DESC"].ToString();
                    }
                    if (txtWorkOrderNo.Text.Trim().Length > 0)
                    {
                        OrderNo = dtRepairReport.Rows[0]["WORK_ORDER_NO"].ToString();
                        sItemCode = dtRepairReport.Rows[0]["FG_ITEM_CODE"].ToString();
                        Desc = dtRepairReport.Rows[0]["FG_ITEM_DESC"].ToString();
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
        protected void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                string sFGItemCode = string.Empty;
                string sMachineID = string.Empty;
                string sReworkStID = string.Empty;
                if (drpFGitemCode.SelectedIndex > 0)
                {
                    sFGItemCode = drpFGitemCode.Text;
                }
                if (drpMachineID.SelectedIndex > 0)
                {
                    sMachineID = drpMachineID.Text;
                }
                if (REWORK_STATION_ID.SelectedIndex > 0)
                {
                    sReworkStID = REWORK_STATION_ID.Text;
                }
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                ShowGridData(txtFromDate.Text, txtToDate.Text,
                  sMachineID, drpType.Text, sFGItemCode, sReworkStID
                   );
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