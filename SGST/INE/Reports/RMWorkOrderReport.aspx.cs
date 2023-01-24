﻿using Common;
using System;
using System.Data;
using System.Web.UI;
using BusinessLayer.Reports;
using Microsoft.Reporting.WebForms;

namespace SGST.INE.Reports
{
    public partial class RMWorkOrderReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usertype"].ToString() != "ADMIN")
            {
                string _strRights = CommonHelper.GetRights("WORK ORDER TRACEABILITY REPORT", (DataTable)Session["USER_RIGHTS"]);
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
                    BindPart_No();
                }
                catch (Exception ex)
                {
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                }
            }
        }
        private void BindPart_No()
        {
            try
            {
                BL_WORK_ORDER_TRACEABILITY_REPORT blobj;
                blobj = new BL_WORK_ORDER_TRACEABILITY_REPORT();
                DataTable dtGRN = blobj.BindPartCode();
                if (dtGRN.Rows.Count > 0)
                {
                    CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                    clsCommon.FillComboBox(drpItemCode, dtGRN, true);
                    drpItemCode.Focus();
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

        protected void drpItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpItemCode.SelectedIndex > 0)
                {
                    BL_WORK_ORDER_TRACEABILITY_REPORT blobj = new BL_WORK_ORDER_TRACEABILITY_REPORT();
                    DataTable dtGRN = blobj.BindWorkOrderNo(drpItemCode.Text);
                    if (dtGRN.Rows.Count > 0)
                    {
                        CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                        clsCommon.FillComboBox(drpWorkOrderNo, dtGRN, true);
                        drpWorkOrderNo.Focus();
                    }
                    else
                    {
                        CommonHelper.ShowMessage("No data found.", msginfo, CommonHelper.MessageType.Info.ToString());
                    }
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
                BL_WORK_ORDER_TRACEABILITY_REPORT blobj;
                blobj = new BL_WORK_ORDER_TRACEABILITY_REPORT();
                string sWorkOrderNo = string.Empty;
                string sPartCode = string.Empty;
                if (drpItemCode.SelectedIndex > 0)
                {
                    sPartCode = drpItemCode.Text;
                }
                if (drpWorkOrderNo.SelectedIndex > 0)
                {
                    sWorkOrderNo = drpWorkOrderNo.Text;
                }
                DataTable dtTrackingData = blobj.GetReport(sPartCode, sWorkOrderNo);
                if (dtTrackingData.Rows.Count == 0)
                {
                    CommonHelper.ShowCustomErrorMessage("No Records Found", msgerror);
                }
                else
                {
                    if (dtTrackingData.Columns.Count == 1)
                    {
                        CommonHelper.ShowMessage(dtTrackingData.Rows[0][0].ToString(), msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtMessage,
                         System.Reflection.MethodBase.GetCurrentMethod().Name, "Work order Traceability Report No of records :" + dtTrackingData.Rows.Count);
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportDataSource datasource = new ReportDataSource();
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/ReportViewer/WorkOrderTraceabilityReport.rdlc");
                    datasource = new ReportDataSource("DataSet1", dtTrackingData);
                    ReportViewer1.Visible = true;
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasource);
                    ReportViewer1.LocalReport.EnableExternalImages = true;                    
                    string Desc = string.Empty;
                    string sItemCode = string.Empty;                    
                    if (drpItemCode.SelectedIndex > 0)
                    {
                        sItemCode = dtTrackingData.Rows[0]["FG_ITEM_CODE"].ToString();
                        Desc = dtTrackingData.Rows[0]["FG_ITEM_DESC"].ToString();                        
                    }                    
                    string imagePath = new Uri(Server.MapPath("~/images/ReportLogo.png")).AbsoluteUri;
                    ReportParameter parameter = new ReportParameter("rptLogo", imagePath);
                    ReportParameter paramete = new ReportParameter("rptFGItemCode", sItemCode);
                    ReportParameter paramete1 = new ReportParameter("rptOrderNo", sWorkOrderNo);
                    ReportParameter paramete2 = new ReportParameter("rptFGDesc", Desc);
                    ReportParameter paramete5 = new ReportParameter("rptCurrentDate", DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                    ReportViewer1.LocalReport.SetParameters(parameter);
                    ReportViewer1.LocalReport.SetParameters(paramete);
                    ReportViewer1.LocalReport.SetParameters(paramete1);
                    ReportViewer1.LocalReport.SetParameters(paramete2);
                    ReportViewer1.LocalReport.SetParameters(paramete5);
                    ReportViewer1.LocalReport.Refresh();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ShowMessage();", true);
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