using BusinessLayer.Reports;
using Common;
using Microsoft.Reporting.WebForms;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGST.INE.Reports
{
    public partial class RMReceivingNewReport : System.Web.UI.Page
    {
        public static DataTable dtTrackingData = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usertype"].ToString() != "ADMIN")
            {
                string _strRights = CommonHelper.GetRights("RM INVENTORY REPORT", (DataTable)Session["USER_RIGHTS"]);
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
                    BindPartCode();
                    drpLocationType.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    drpLocationType.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                }
            }
        }
        private void BindPartCode()
        {
            try
            {
                blRMTrackingReport blobj;
                blobj = new blRMTrackingReport();
                DataTable dtPartCode = blobj.BindPartCodeForRMTrackingReport();
                if (dtPartCode.Rows.Count > 0)
                {
                    CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                    clsCommon.FillComboBox(drpPartCode, dtPartCode, true);
                    drpPartCode.Focus();
                }
                else
                {
                    CommonHelper.ShowMessage("No data found.", msginfo, CommonHelper.MessageType.Info.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                blRMTrackingReport blobj;
                blobj = new blRMTrackingReport();
                string sPartCode = string.Empty;
                string sLocationType = string.Empty;
                if (drpPartCode.SelectedIndex > 0)
                {
                    sPartCode = drpPartCode.Text;
                }
                sLocationType = drpLocationType.SelectedIndex.ToString();
                dtTrackingData = blobj.GetReport(sPartCode, sLocationType, txtLocation.Text.Trim()
                    , txtGRPONo.Text.Trim(), txtBatch.Text.Trim()
                    );
                if (dtTrackingData.Rows.Count == 0)
                {
                    CommonHelper.ShowCustomErrorMessage("No Records Found", msgerror);
                    drpPartCode.Focus();
                }
                else
                {
                    if (dtTrackingData.Columns.Count == 1)
                    {
                        CommonHelper.ShowMessage(dtTrackingData.Rows[0][0].ToString(), msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtMessage,
                         System.Reflection.MethodBase.GetCurrentMethod().Name, "Invenotry Report No of records :" + dtTrackingData.Rows.Count);
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportDataSource datasource = new ReportDataSource();
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/ReportViewer/RMInventoryReport.rdlc");
                    datasource = new ReportDataSource("DataSet1", dtTrackingData);
                    ReportViewer1.Visible = true;
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasource);
                    ReportViewer1.LocalReport.EnableExternalImages = true;
                    string Barcode = string.Empty;
                    string OrderNo = string.Empty;
                    string Desc = string.Empty;
                    string sItemCode = string.Empty;
                    if (drpPartCode.SelectedIndex > 0)
                    {
                        Desc = dtTrackingData.Rows[0]["PART_DESC"].ToString();
                        sItemCode = dtTrackingData.Rows[0]["PART_CODE"].ToString();
                    }
                    if (txtGRPONo.Text.Trim().Length > 0)
                    {
                        OrderNo = dtTrackingData.Rows[0]["PONO"].ToString();
                    }
                    string imagePath = new Uri(Server.MapPath("~/images/ReportLogo.png")).AbsoluteUri;
                    ReportParameter parameter = new ReportParameter("rptLogo", imagePath);
                    ReportParameter paramete = new ReportParameter("rptFGItemCode", sItemCode);
                    ReportParameter paramete1 = new ReportParameter("rptWorkOrderNo", OrderNo);
                    ReportParameter paramete2 = new ReportParameter("rptFGDesc", Desc);
                    ReportParameter paramete3 = new ReportParameter("rptPackedOn", DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                    ReportViewer1.LocalReport.SetParameters(parameter);
                    ReportViewer1.LocalReport.SetParameters(paramete);
                    ReportViewer1.LocalReport.SetParameters(paramete1);
                    ReportViewer1.LocalReport.SetParameters(paramete2);
                    ReportViewer1.LocalReport.SetParameters(paramete3);
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
                txtBatch.Text = string.Empty;
                txtGRPONo.Text = string.Empty;
                txtLocation.Text = string.Empty; 
            }
        }
    }
}