using Common;
using System;
using System.Data;
using System.Web.UI;
using BusinessLayer.Reports;
using Microsoft.Reporting.WebForms;

namespace SGST.INE.Reports
{
    public partial class RMReceivingReport : System.Web.UI.Page
    {
        public void BindPartCode()
        {
            try
            {
                BL_RMReceivingReport blobj = new BL_RMReceivingReport();
                DataTable dt = blobj.BindPartCode();
                if (dt.Rows.Count > 0)
                {
                    CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                    clsCommon.FillComboBox(drpItemCode, dt, true);
                    drpItemCode.Focus();
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usertype"].ToString().ToUpper() != "ADMIN")
            {
                string _strRights = CommonHelper.GetRights("RECEIVING REPORT", (DataTable)Session["USER_RIGHTS"]);
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
                    BindPartCode();
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
                string sPartCode = string.Empty;
                string sBarocode = string.Empty;
                string sSearchStatus = "";
                if (drpItemCode.SelectedIndex > 0)
                {
                    sPartCode = drpItemCode.Text;
                }
                if (drpData.SelectedIndex > 0)
                {
                    sSearchStatus = drpData.SelectedIndex.ToString();
                }
                string sFromDate = string.Empty;
                string sToDate = string.Empty;
                if (sFromDate.Contains("d") || sFromDate.Contains("m") || sFromDate.Contains("y")
                    || sToDate.Contains("d") || sToDate.Contains("m") || sToDate.Contains("y")
                    )
                {
                    sFromDate = "";
                    sToDate = "";
                }
                else
                {
                    sFromDate = txtFromDate.Text;
                    sToDate = txtToDate.Text;
                }
                BL_RMReceivingReport blobj = new BL_RMReceivingReport();
                DataTable dtTrackingData = blobj.GetReport(txtGRPONO.Text, sPartCode, txtLocation.Text.Trim()
                    , sSearchStatus, sFromDate, sToDate);
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
                    string Barcode = string.Empty;
                    string OrderNo = string.Empty;
                    string Desc = string.Empty;
                    string sItemCode = string.Empty;
                    if (drpItemCode.SelectedIndex > 0)
                    {
                        sItemCode = dtTrackingData.Rows[0]["PART_CODE"].ToString();
                        Desc = dtTrackingData.Rows[0]["PART_DESC"].ToString();
                        OrderNo = dtTrackingData.Rows[0]["GRPONO"].ToString();
                    }
                    if (txtGRPONO.Text.Trim().Length > 0)
                    {
                        OrderNo = dtTrackingData.Rows[0]["GRPONO"].ToString();
                    }
                    ReportDataSource datasource = new ReportDataSource();
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/ReportViewer/RMRecevingRDLC.rdlc");
                    datasource = new ReportDataSource("DataSet1", dtTrackingData);
                    ReportViewer1.Visible = true;
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasource);
                    ReportViewer1.LocalReport.EnableExternalImages = true;
                    string imagePath = new Uri(Server.MapPath("~/images/ReportLogo.png")).AbsoluteUri;
                    ReportParameter parameter = new ReportParameter("rptLogo", imagePath);
                    ReportParameter paramete = new ReportParameter("rptFGItemCode", sItemCode);
                    ReportParameter paramete1 = new ReportParameter("rptOrderNo", OrderNo);
                    ReportParameter paramete2 = new ReportParameter("rptLocation", txtLocation.Text);
                    ReportParameter paramete3 = new ReportParameter("rptFGDesc", Desc);
                    ReportParameter paramete4 = new ReportParameter("rptPackedOn", DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                    ReportViewer1.LocalReport.SetParameters(parameter);
                    ReportViewer1.LocalReport.SetParameters(paramete);
                    ReportViewer1.LocalReport.SetParameters(paramete1);
                    ReportViewer1.LocalReport.SetParameters(paramete2);
                    ReportViewer1.LocalReport.SetParameters(paramete3);
                    ReportViewer1.LocalReport.SetParameters(paramete4);
                    ReportViewer1.LocalReport.Refresh();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ShowMessage();", true);
                }
               
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                txtFromDate.Text = string.Empty;
                txtGRPONO.Text = string.Empty;
                txtLocation.Text = string.Empty;
                txtToDate.Text = string.Empty;
                drpData.SelectedIndex = 0;
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
