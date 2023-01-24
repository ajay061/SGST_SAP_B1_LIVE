using Common;
using Microsoft.Reporting.WebForms;
using System;
using System.Data;
using System.Data.SqlClient;
using BusinessLayer.Reports;
using System.Web.UI;

namespace SGST.INE.Reports
{
    public partial class MaterialIssueToMachineReportNew : System.Web.UI.Page
    {
        SqlCommand cmd = new SqlCommand();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usertype"].ToString() != "ADMIN")
            {
                string _strRights = CommonHelper.GetRights("MATERIAL ISSUE TO MACHINE REPORT", (DataTable)Session["USER_RIGHTS"]);
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
                    BINDLINES();
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
        private void BINDLINES()
        {
            try
            {
                BL_MaterialIssuenceReport blobj;
                blobj = new BL_MaterialIssuenceReport();
                DataTable dtGRN = blobj.BINDLINES();
                if (dtGRN.Rows.Count > 0)
                {
                    CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                    clsCommon.FillComboBox(drpLine, dtGRN, true);
                    drpLine.Focus();
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
                if (drpLine.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select line", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpLine.Focus();
                    return;
                }
                if (drpmachine.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select machine", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpmachine.Focus();
                    return;
                }
                if (drpFGitem.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select FG Item Code", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpFGitem.Focus();
                    return;
                }
                BL_MaterialIssuenceReport blobj = new BL_MaterialIssuenceReport();
                DataTable dt = blobj.GetReport(drpLine.Text, drpmachine.SelectedValue.ToString(), drpFGitem.Text, txtFromDate.Text,
                    txtToDate.Text);
                if (dt.Rows.Count == 0)
                {
                    CommonHelper.ShowMessage("No details found.", msgerror, CommonHelper.MessageType.Error.ToString());
                    return;
                }
                if (dt.Columns.Count == 1)
                {
                    CommonHelper.ShowMessage(dt.Rows[0][0].ToString(), msgerror, CommonHelper.MessageType.Error.ToString());
                    return;
                }
                string Line = string.Empty;
                string sItemCode = string.Empty;
                string Desc = string.Empty;
                string sMachine = string.Empty;
                if (drpFGitem.SelectedIndex > 0)
                {
                    Desc = dt.Rows[0]["fg_item_desc"].ToString();
                    sItemCode = dt.Rows[0]["PCB_SN_PARTCODE"].ToString();
                }
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportDataSource datasource = new ReportDataSource();
                datasource = new ReportDataSource("DataSet1", dt);
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/ReportViewer/MaterialIssueToMachineRPT.rdlc");
                ReportViewer1.Visible = true;
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                ReportViewer1.LocalReport.EnableExternalImages = true;                

                string imagePath = new Uri(Server.MapPath("~/images/ReportLogo.png")).AbsoluteUri;
                ReportParameter parameter = new ReportParameter("rptLogo", imagePath);
                ReportParameter paramete1 = new ReportParameter("rptLine", drpLine.Text);
                ReportParameter paramete = new ReportParameter("rptFGItemCode", sItemCode);
                ReportParameter paramete2 = new ReportParameter("rptMachineName", drpmachine.Text);
                ReportParameter paramete3 = new ReportParameter("rptFromDate", txtFromDate.Text);
                ReportParameter paramete4 = new ReportParameter("rptToDate", txtToDate.Text);
                ReportParameter paramete5 = new ReportParameter("rptFGDesc", Desc);
                ReportViewer1.LocalReport.SetParameters(paramete);
                ReportViewer1.LocalReport.SetParameters(paramete1);
                ReportViewer1.LocalReport.SetParameters(paramete2);
                ReportViewer1.LocalReport.SetParameters(paramete3);
                ReportViewer1.LocalReport.SetParameters(paramete4);
                ReportViewer1.LocalReport.SetParameters(paramete5);
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
        protected void drpLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                BL_MaterialIssuenceReport blobj;
                blobj = new BL_MaterialIssuenceReport();
                DataTable dtGRN = blobj.GetMachineRecord(drpLine.Text);
                if (dtGRN.Rows.Count > 0)
                {
                    CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                    clsCommon.FillMachine(drpmachine, dtGRN, true);
                    drpmachine.Focus();
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

        protected void drpmachine_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (drpmachine.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select machine", msginfo, CommonHelper.MessageType.Info.ToString());
                    drpmachine.Focus();
                    return;
                }
                BL_MaterialIssuenceReport blobj;
                blobj = new BL_MaterialIssuenceReport();
                string sMachineID = drpmachine.SelectedValue.ToString();
                DataTable dtGRN = blobj.BindFGItemCode(drpLine.Text, sMachineID);
                if (dtGRN.Rows.Count > 0)
                {
                    CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                    clsCommon.FillComboBox(drpFGitem, dtGRN, true);
                    drpFGitem.Focus();
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
    }
}