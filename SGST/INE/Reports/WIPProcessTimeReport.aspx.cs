using BusinessLayer.Reports;
using Common;
using Microsoft.Reporting.WebForms;
using System;
using System.Data;
using System.Web.UI;

namespace SGST.INE.Reports
{
    public partial class WIPProcessTimeReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usertype"].ToString() != "ADMIN")
            {
                string _strRights = CommonHelper.GetRights("PROCESS TIME REPORT", (DataTable)Session["USER_RIGHTS"]);
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
                    BindFgItem();
                    BindMachineID();
                    string a = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    txtFromDate.Text = a;
                    txtToDate.Text = a;
                }
                catch (Exception ex)
                {
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                }
            }
        }
        private void BindFgItem()
        {
            try
            {
                BL_WIP_ProcessTimeReport blobj;
                blobj = new BL_WIP_ProcessTimeReport();
                DataTable dtGRN = blobj.BindFGItemCode();
                if (dtGRN.Rows.Count > 0)
                {
                    CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                    clsCommon.FillComboBox(drpFG, dtGRN, true);
                    drpFG.Focus();
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

        private void BindMachineID()
        {
            try
            {
                BL_WIP_ProcessTimeReport blobj;
                blobj = new BL_WIP_ProcessTimeReport();
                DataTable dtGRN = blobj.BindProcessID();
                if (dtGRN.Rows.Count > 0)
                {
                    CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                    clsCommon.FillComboBox(drpMachine, dtGRN, true);
                    drpMachine.Focus();
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
                if (txtFromDate.Text == "" || txtToDate.Text == "")
                {
                    CommonHelper.ShowMessage("Dates are mandatory.", msgerror, CommonHelper.MessageType.Error.ToString());
                    return;
                }
                if (drpMachine.Text == "" || drpMachine.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Machine is mandatory.", msgerror, CommonHelper.MessageType.Error.ToString());
                    return;
                }
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportDataSource datasource = new ReportDataSource();
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/ReportViewer/WIP_PROCESS_TIME_RDLC.rdlc");
                BL_WIP_ProcessTimeReport blobj;
                blobj = new BL_WIP_ProcessTimeReport();
                DataTable dt = blobj.GetReport(drpMachine.Text, drpFG.Text, txtFromDate.Text, txtToDate.Text);
                datasource = new ReportDataSource("DataSet1", dt);
                if (dt.Rows.Count == 0)
                {
                    CommonHelper.ShowMessage("No details found.", msgerror, CommonHelper.MessageType.Error.ToString());
                }
                else
                {
                    if (dt.Columns.Count == 1)
                    {
                        CommonHelper.ShowMessage(dt.Rows[0][0].ToString(), msgerror, CommonHelper.MessageType.Error.ToString());
                    }
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
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }     
    }
}