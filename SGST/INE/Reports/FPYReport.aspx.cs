using Common;
using System;
using System.Data;
using System.Web.UI;
using BusinessLayer.Reports;
using Microsoft.Reporting.WebForms;

namespace SGST.INE.Reports
{
    public partial class FPYReport : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Signin/v1/Login.aspx?Session=Null");
            }
        }
        static DataTable dtTrackingData = new DataTable();
        string sHeaderValue = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
            Response.AddHeader("Pragma", "no-cache");
            Response.AddHeader("Expires", "0");
            if (Request.QueryString["Name"] != null && Request.QueryString["Name"] != string.Empty)
            {
                sHeaderValue = Request.QueryString["Name"].ToString();
                if (Request.QueryString["Name"].Contains("FPY"))
                {
                    lblHeader.Text = "FPY REPORT";
                }
                else if (Request.QueryString["Name"].Contains("XRAY"))
                {
                    lblHeader.Text = "XRAY SAMPLING REPORT";
                }
                else if (Request.QueryString["Name"].Contains("PDI"))
                {
                    lblHeader.Text = "PDI ACCESSORIES SAMPLING REPORT";
                }
                if (Session["usertype"].ToString() != "ADMIN")
                {
                    string _strRights = string.Empty;
                    _strRights = CommonHelper.GetRights(lblHeader.Text.ToUpper(), (DataTable)Session["USER_RIGHTS"]);
                    CommonHelper._strRights = _strRights.Split('^');
                    if (CommonHelper._strRights[0] == "0")  //Check view rights
                    {
                        Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                    }
                }
            }
            if (!IsPostBack)
            {
                try
                {
                    BindFGItemCode();
                    string current_date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    txtFromDate.Text = current_date;
                    txtToDate.Text = current_date;
                }
                catch (Exception ex)
                {
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                }
            }
        }
        private void BindFGItemCode()
        {
            try
            {
                BL_FPYReport blobj;
                blobj = new BL_FPYReport();
                DataTable dtGRN = blobj.BindFGItemCode(sHeaderValue);
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
                ShowGridData();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void ShowGridData()
        {
            try
            {
                string sFGItemCode = string.Empty;
                if (sHeaderValue == "FPY")
                {
                    if (drpFGItemCode.SelectedIndex == 0)
                    {
                        CommonHelper.ShowMessage("Please select fg item code", msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                    else
                    {
                        sFGItemCode = drpFGItemCode.Text;
                    }
                }
                else
                {
                    if (drpFGItemCode.SelectedIndex > 0)
                    {
                        sFGItemCode = drpFGItemCode.Text;
                    }
                }                
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                BL_FPYReport blobj = new BL_FPYReport();
                DataTable dtTrackingData = blobj.GetReport(txtFromDate.Text, txtToDate.Text, sFGItemCode, sHeaderValue);
                if (dtTrackingData.Rows.Count > 0)
                {
                    if (dtTrackingData.Columns.Count == 1)
                    {
                        CommonHelper.ShowMessage(dtTrackingData.Rows[0][0].ToString(), msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtMessage,
                          System.Reflection.MethodBase.GetCurrentMethod().Name, "FPY Sampling Report No of records :" + dtTrackingData.Rows.Count);

                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportDataSource datasource = new ReportDataSource();
                    if (sHeaderValue == "FPY")
                    {
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/ReportViewer/WIPFPYReport.rdlc");
                        datasource = new ReportDataSource("DataSet2", dtTrackingData);
                    }
                    else if (sHeaderValue == "XRAY")
                    {
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/ReportViewer/WIPXRAYReport.rdlc");
                        datasource = new ReportDataSource("DataSet2", dtTrackingData);
                    }
                    else if (sHeaderValue == "PDI")
                    {
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/ReportViewer/WIPPDIReport.rdlc");
                        datasource = new ReportDataSource("DataSet2", dtTrackingData);
                    }
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
                        sItemCode = dtTrackingData.Rows[0]["FG_ITEM_CODE"].ToString();
                        Desc = dtTrackingData.Rows[0]["FG_ITEM_DESC"].ToString();
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
                else
                {
                    CommonHelper.ShowCustomErrorMessage("No Records Found", msgerror);
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
    }
}