using Common;
using Microsoft.Reporting.WebForms;
using System;
using System.Data;
using BusinessLayer.Reports;
using System.Web.UI;

namespace SGST.INE.Reports
{
    public partial class FG_PackingListNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usertype"].ToString() != "ADMIN")
            {
                string _strRights = CommonHelper.GetRights("FG PACKING LIST REPORT", (DataTable)Session["USER_RIGHTS"]);
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
                    BindSalesOrder();
                }
                catch (Exception ex)
                {
                    CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                }
            }
        }
        private void BindSalesOrder()
        {
            try
            {
                BL_FG_PackingListReport blobj = new BL_FG_PackingListReport();
                DataTable dtGRN = blobj.BINDSALESORDERFORREPORT();
                if (dtGRN.Rows.Count > 0)
                {
                    CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                    clsCommon.FillComboBox(drpsalesorder, dtGRN, true);
                    drpsalesorder.Focus();
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
        private void BindPickList()
        {
            try
            {
                if (drpsalesorder.SelectedIndex > 0)
                {
                    BL_FG_PackingListReport blobj = new BL_FG_PackingListReport();
                    DataTable dtGRN = blobj.BindPickList(drpsalesorder.Text);
                    if (dtGRN.Rows.Count > 0)
                    {
                        CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                        clsCommon.FillComboBox(drppicklist, dtGRN, true);
                        drppicklist.Focus();
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
        private void BindPackignList()
        {
            try
            {
                if (drppicklist.SelectedIndex > 0)
                {
                    BL_FG_PackingListReport blobj = new BL_FG_PackingListReport();
                    DataTable dtGRN = blobj.BindPackingList(drppicklist.Text);
                    if (dtGRN.Rows.Count > 0)
                    {
                        CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                        clsCommon.FillComboBox(drppacklist, dtGRN, true);
                        drppacklist.Focus();
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
                if (drpsalesorder.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select sales order.", msgerror, CommonHelper.MessageType.Error.ToString());
                    return;
                }
                if (drppicklist.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select packlist no.", msgerror, CommonHelper.MessageType.Error.ToString());
                    return;
                }
                if (drppacklist.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select picklist no.", msgerror, CommonHelper.MessageType.Error.ToString());
                    return;
                }
                BL_FG_PackingListReport blobj = new BL_FG_PackingListReport();
                DataTable dtReports = blobj.GetReport(drpsalesorder.Text, drppicklist.Text, drppacklist.Text);
                if (dtReports.Rows.Count == 0)
                {
                    CommonHelper.ShowMessage("No details found.", msgerror, CommonHelper.MessageType.Error.ToString());
                    return;
                }
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportDataSource datasource = new ReportDataSource();
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                ReportViewer1.LocalReport.EnableExternalImages = true;
                string imagePath = new Uri(Server.MapPath("~/images/ReportLogo.png")).AbsoluteUri;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/ReportViewer/PicklistReportRDLC.rdlc");
                ReportParameter rp = new ReportParameter("ParameterSalesOrder", drpsalesorder.Text.ToString());
                ReportParameter rp1 = new ReportParameter("ParameterPicklist", drppicklist.Text.ToString());
                ReportParameter rp2 = new ReportParameter("ParameterPackList", drppacklist.Text.ToString());
                ReportParameter rp3 = new ReportParameter("rptLogo", imagePath);
                ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp, rp1, rp2, rp3 });                
                datasource = new ReportDataSource("DataSet1", dtReports);
                ReportViewer1.Visible = true;
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                ReportViewer1.LocalReport.Refresh();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ShowMessage();", true);
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void drpsalesorder_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindPickList();
        }

        protected void drppicklist_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindPackignList();
        }
    }
}