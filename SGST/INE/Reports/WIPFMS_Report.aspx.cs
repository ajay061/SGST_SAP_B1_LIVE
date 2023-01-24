using BusinessLayer.Reports;
using Common;
using Microsoft.Reporting.WebForms;
using System;
using System.Data;
using System.Web.UI;
namespace SGST.INE.Reports
{
    public partial class FMS_Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usertype"].ToString() != "ADMIN")
            {
                string _strRights = CommonHelper.GetRights("FMS DATA REPORT", (DataTable)Session["USER_RIGHTS"]);
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
                    BindMachineid();
                    rdsummary.Checked = true;
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
        private void BindMachineid()
        {
            try
            {
                BL_FMS_Report blobj;
                blobj = new BL_FMS_Report();
                DataTable dtMCID = blobj.BINDMACHINEID();
                if (dtMCID.Rows.Count > 0)
                {
                    CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                    clsCommon.FillComboBox(ddlmachineid, dtMCID, true);
                    ddlmachineid.Focus();
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
        private void BindFGITEMCODE()
        {
            try
            {
                BL_FMS_Report blobj;
                blobj = new BL_FMS_Report();
                DataTable dtMCID = blobj.BindFGITEMCODE(ddlmachineid.SelectedItem.Text);
                if (dtMCID.Rows.Count > 0)
                {
                    CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                    clsCommon.FillComboBox(ddlfgitemcode, dtMCID, true);
                    ddlfgitemcode.Focus();
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
                if (ddlmachineid.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select machine id", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlmachineid.Focus();
                    return;
                }
                if (ddlfgitemcode.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select machine id", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlfgitemcode.Focus();
                    return;
                }
                BL_FMS_Report blobj = new BL_FMS_Report();
                DataTable dt = new DataTable();
                string sType = string.Empty;
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportDataSource datasource = new ReportDataSource();
                if (rdsummary.Checked)
                {
                    sType = "Summary";
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/ReportViewer/FMS_Report_Details.rdlc");
                }
                if (rdDertails.Checked)
                {
                    sType = "DETAILS";
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/ReportViewer/FMS_Report.rdlc");
                }
                string sMachineID = string.Empty;
                string sFGItemCode = string.Empty;
                if (ddlmachineid.SelectedIndex > 0)
                {
                    sMachineID = ddlmachineid.Text;
                }
                if (ddlfgitemcode.SelectedIndex > 0)
                {
                    sFGItemCode = ddlfgitemcode.Text;
                }
                dt = blobj.GetFMSReport(sMachineID, txtFromDate.Text, txtToDate.Text, sFGItemCode, sType);              
                if (dt.Rows.Count == 0)
                {
                    CommonHelper.ShowMessage("No details found.", msgerror, CommonHelper.MessageType.Error.ToString());
                    return;
                }
                datasource = new ReportDataSource("DataSet1", dt);
                ReportViewer1.Visible = true;
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                ReportViewer1.LocalReport.EnableExternalImages = true;
                string imagePath = new Uri(Server.MapPath("~/images/ReportLogo.png")).AbsoluteUri;
                ReportParameter parameter = new ReportParameter("rptLogo", imagePath);
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
        protected void ddlmachineid_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindFGITEMCODE();
        }
    }
}