using BusinessLayer.Reports;
using Common;
using Microsoft.Reporting.WebForms;
using System;
using System.Data;
using System.Web.UI;

namespace SGST.INE.Reports
{
    public partial class BOMMasterDataReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usertype"].ToString() != "ADMIN")
            {
                string _strRights = CommonHelper.GetRights("RM BOM REPORT", (DataTable)Session["USER_RIGHTS"]);
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
                BL_RM_Issue_Report blobj;
                blobj = new BL_RM_Issue_Report();
                DataTable dtFGItemCode = blobj.BindFGITemCode();
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
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                BL_RM_Issue_Report blobj;
                blobj = new BL_RM_Issue_Report();
                string sFGItemCode = string.Empty;
                if (drpFGItemCode.SelectedIndex > 0)
                {
                    sFGItemCode = drpFGItemCode.Text;
                }
                DataTable dt = blobj.MasterDataReport(txtFromDate.Text, txtToDate.Text
                    , txtWorkOrderNo.Text.Trim(), drpReportType.Text, sFGItemCode);
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
                    if (drpReportType.Text == "HEADER")
                    {
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/ReportViewer/RMHeaderDataReport.rdlc");
                    }
                    else
                    {
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/ReportViewer/RMBOMDataReport.rdlc");
                    }                
                    datasource = new ReportDataSource("DataSet1", dt);
                    ReportViewer1.Visible = true;
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasource);
                    ReportViewer1.LocalReport.EnableExternalImages = true;
                    string sFGDesc = string.Empty;
                    
                    if (drpFGItemCode.SelectedIndex > 0)
                    {
                        sFGDesc = dt.Rows[0]["FG_ITEM_DESC"].ToString();
                        sFGItemCode = dt.Rows[0]["FG_ITEM_CODE"].ToString();
                    }
                    if (txtWorkOrderNo.Text.Trim().Length > 0)
                    {
                        sFGDesc = dt.Rows[0]["FG_ITEM_DESC"].ToString();
                        sFGItemCode = dt.Rows[0]["FG_ITEM_CODE"].ToString();
                    }
                    
                    string imagePath = new Uri(Server.MapPath("~/images/ReportLogo.png")).AbsoluteUri;
                    ReportParameter parameter = new ReportParameter("rptLogo", imagePath);
                    ReportParameter parameter1 = new ReportParameter("rptFromDate", txtFromDate.Text);
                    ReportParameter parameter2 = new ReportParameter("rptToDate", txtToDate.Text);
                    ReportParameter parameter3 = new ReportParameter("rptCurrentDate", DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                    ReportParameter parameter4 = new ReportParameter("rptFGItemDesc", sFGDesc);
                    ReportParameter parameter6 = new ReportParameter("rptFGItemCode", sFGItemCode);
                    ReportParameter parameter5 = new ReportParameter("rptWorkOrderNo", txtWorkOrderNo.Text.Trim());
                    
                    ReportViewer1.LocalReport.SetParameters(parameter);
                    ReportViewer1.LocalReport.SetParameters(parameter1);
                    ReportViewer1.LocalReport.SetParameters(parameter2);
                    ReportViewer1.LocalReport.SetParameters(parameter3);
                    ReportViewer1.LocalReport.SetParameters(parameter4);
                    ReportViewer1.LocalReport.SetParameters(parameter5);
                    ReportViewer1.LocalReport.SetParameters(parameter6);
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