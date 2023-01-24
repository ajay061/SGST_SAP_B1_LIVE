using BusinessLayer.Reports;
using Common;
using Microsoft.Reporting.WebForms;
using System;
using System.Data;
using System.Web.UI;

namespace SGST.INE.Reports
{
    public partial class PCBSerialGenrationReportNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usertype"].ToString() != "ADMIN")
            {
                string _strRights = CommonHelper.GetRights("PCB SERIAL GENERATION REPORT", (DataTable)Session["USER_RIGHTS"]);
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
                    BindFGItemCode();
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
        private void BindFGItemCode()
        {
            try
            {
                BL_PCB_Serial_Gen_Report blobj;
                blobj = new BL_PCB_Serial_Gen_Report();
                DataTable dtFGItemCode = blobj.BindFGItemCode();
                if (dtFGItemCode.Rows.Count > 0)
                {
                    CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                    clsCommon.FillComboBox(drpFGItemCode, dtFGItemCode, true);
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
                if (drpFGItemCode.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select FG item code.", msgerror, CommonHelper.MessageType.Error.ToString());
                    return;
                }
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);

                BL_PCB_Serial_Gen_Report blobj;
                blobj = new BL_PCB_Serial_Gen_Report();
                DataTable dt = blobj.GetReport(drpFGItemCode.Text, txtFromDate.Text, txtToDate.Text
                    , txtWorkOrderNo.Text.Trim());
                if (dt.Rows.Count == 0)
                {
                    CommonHelper.ShowMessage("No details found.", msgerror, CommonHelper.MessageType.Error.ToString());
                }
                else if (dt.Columns.Count == 1)
                {
                    CommonHelper.ShowCustomErrorMessage(dt.Rows[0][0].ToString(), msgerror);
                    return;
                }
                else
                {
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportDataSource datasource = new ReportDataSource();
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/ReportViewer/PCBSerailGeneration.rdlc");
                    datasource = new ReportDataSource("DataSet1", dt);
                    ReportViewer1.Visible = true;
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasource);
                    ReportViewer1.LocalReport.EnableExternalImages = true;
                    string sFGDesc = string.Empty;
                    sFGDesc = dt.Rows[0]["FG_ITEM_DESC"].ToString();
                    string imagePath = new Uri(Server.MapPath("~/images/ReportLogo.png")).AbsoluteUri;
                    ReportParameter parameter = new ReportParameter("rptLogo", imagePath);
                    ReportParameter parameter1 = new ReportParameter("rptFromDate", txtFromDate.Text);
                    ReportParameter parameter2 = new ReportParameter("rptToDate", txtToDate.Text);
                    ReportParameter parameter3 = new ReportParameter("rptCurrentDate", DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                    ReportParameter parameter4 = new ReportParameter("rptFGItemDesc", sFGDesc);
                    ReportViewer1.LocalReport.SetParameters(parameter);
                    ReportViewer1.LocalReport.SetParameters(parameter1);
                    ReportViewer1.LocalReport.SetParameters(parameter2);
                    ReportViewer1.LocalReport.SetParameters(parameter3);
                    ReportViewer1.LocalReport.SetParameters(parameter4);
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