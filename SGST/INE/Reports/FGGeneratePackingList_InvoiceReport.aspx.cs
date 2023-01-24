using Common;
using System;
using System.Data;
using System.Web.UI;
using BusinessLayer.FG;
using Microsoft.Reporting.WebForms;

namespace SGST.INE.Reports
{
    public partial class GeneratePackingList_InvoiceReport : System.Web.UI.Page
    {
        static DataTable dtGetReport = new DataTable();
        public void bindOutBondDeliveryNo()
        {
            try
            {
                BL_GenereatePackingList blobj = new BL_GenereatePackingList();
                DataTable dt = blobj.BindReportDeliveryNo();
                if (dt.Rows.Count > 0)
                {
                    CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                    clsCommon.FillComboBox(drpoutBondDeliveryNo, dt, true);
                    drpoutBondDeliveryNo.Focus();
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
        public void BindInvoiceNo()
        {
            try
            {
                BL_GenereatePackingList blobj = new BL_GenereatePackingList();
                DataTable dt = blobj.BindReportInvoiceNo();
                if (dt.Rows.Count > 0)
                {
                    CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                    clsCommon.FillComboBox(drpInvoiceNo, dt, true);
                    drpInvoiceNo.Focus();
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
            if (Session["usertype"].ToString() != "ADMIN")
            {
                string _strRights = CommonHelper.GetRights("INVOICE REPORT", (DataTable)Session["USER_RIGHTS"]);
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
                    bindOutBondDeliveryNo();
                    BindInvoiceNo();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        private void ShowGridData()
        {
            try
            {
                string sInvoiceNo = string.Empty;
                string sOutBondDeliveryNo = string.Empty;
                string sBarocode = string.Empty;
                if (drpInvoiceNo.SelectedIndex > 0)
                {
                    sInvoiceNo = drpInvoiceNo.Text;
                }
                if (drpoutBondDeliveryNo.SelectedIndex > 0)
                {
                    sOutBondDeliveryNo = drpoutBondDeliveryNo.Text;
                }
                dtGetReport.Rows.Clear();
                BL_GenereatePackingList blobj = new BL_GenereatePackingList();
                dtGetReport = blobj.GetReport(txtFromDate.Text,
                    txtToDate.Text, sOutBondDeliveryNo, sInvoiceNo);
                if (dtGetReport.Rows.Count > 0)
                {
                    ReportDataSource datasource = new ReportDataSource();
                    CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/ReportViewer/FG Reports/InvoiceLabel.rdlc");
                    datasource = new ReportDataSource("DataSet1", dtGetReport);
                    ReportViewer1.Visible = true;
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasource);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ShowMessage();", true);
                }
                else
                {
                    CommonHelper.ShowMessage("No details found.", msgerror, CommonHelper.MessageType.Error.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void btnGetReport_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                ShowGridData();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
    }
}