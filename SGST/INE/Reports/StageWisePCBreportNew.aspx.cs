using BusinessLayer.Reports;
using Common;
using Microsoft.Reporting.WebForms;
using System;
using System.Data;
using System.Web.UI;
namespace SGST.INE.Reports
{
    public partial class StageWisePCBreportNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usertype"].ToString() != "ADMIN")
            {
                string _strRights = CommonHelper.GetRights("MACHINE WISE PCB TRACKING REPORT", (DataTable)Session["USER_RIGHTS"]);
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
                    BindMachineID();
                    BindFGItemCode();
                }
                catch (Exception ex)
                {
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                }
            }
        }
        private void BindMachineID()
        {
            try
            {
                BL_WIP_StageWiseReport blobj;
                blobj = new BL_WIP_StageWiseReport();
                DataTable dtGRN = blobj.BindMachineID();
                if (dtGRN.Rows.Count > 0)
                {
                    CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                    clsCommon.FillComboBox(drpMachineID, dtGRN, true);
                    drpMachineID.Focus();
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

        private void BindFGItemCode()
        {
            try
            {
                BL_WIP_StageWiseReport blobj;
                blobj = new BL_WIP_StageWiseReport();
                DataTable dtGRN = blobj.BindFGItemCode();
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
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/ReportViewer/StageWisePCBDataRDLC.rdlc");
                string sMachineID = "";
                if (drpMachineID.SelectedIndex == 0)
                {
                    sMachineID = "";
                }
                else
                {
                    sMachineID = drpMachineID.Text;
                }
                string sFGItemCode = "";
                if (drpFGItemCode.SelectedIndex == 0)
                {
                    sFGItemCode = "";
                }
                else
                {
                    sFGItemCode = drpFGItemCode.Text;
                }
                BL_WIP_StageWiseReport blobj;
                blobj = new BL_WIP_StageWiseReport();
                DataTable dt = blobj.GetReport(sMachineID, sFGItemCode, txtWorkOrderNo.Text.Trim());
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
                    ReportDataSource datasource = new ReportDataSource();
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/ReportViewer/StageWisePCBDataRDLC.rdlc");
                    datasource = new ReportDataSource("DataSet1", dt);
                    ReportViewer1.Visible = true;
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasource);
                    ReportViewer1.LocalReport.EnableExternalImages = true;
                    string Barcode = string.Empty;
                    string OrderNo = string.Empty;
                    string Desc = string.Empty;
                    string sItemCode = string.Empty;
                    if (drpFGItemCode.SelectedIndex > 0)
                    {
                        Desc = dt.Rows[0]["FG_ITEM_DESC"].ToString();
                        sItemCode = dt.Rows[0]["PART_CODE"].ToString();
                    }
                    if (txtWorkOrderNo.Text.Trim().Length > 0)
                    {
                        OrderNo = dt.Rows[0]["WORK_ORDER_NO"].ToString();
                    }
                    string imagePath = new Uri(Server.MapPath("~/images/ReportLogo.png")).AbsoluteUri;
                    ReportParameter parameter = new ReportParameter("rptLogo", imagePath);
                    ReportParameter paramete = new ReportParameter("rptFGItemCode", sItemCode);
                    ReportParameter paramete1 = new ReportParameter("rptFGDesc", Desc);
                    ReportParameter paramete2 = new ReportParameter("rptFromDate", sMachineID);
                    ReportParameter paramete3 = new ReportParameter("rptPackedOn", DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                    ReportParameter paramete6 = new ReportParameter("rptWorkOrderNo", OrderNo);
                    ReportViewer1.LocalReport.SetParameters(parameter);
                    ReportViewer1.LocalReport.SetParameters(paramete);
                    ReportViewer1.LocalReport.SetParameters(paramete1);
                    ReportViewer1.LocalReport.SetParameters(paramete2);
                    ReportViewer1.LocalReport.SetParameters(paramete3);
                    ReportViewer1.LocalReport.SetParameters(paramete6);
                    ReportViewer1.LocalReport.Refresh();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ShowMessage();", true);
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError,System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}