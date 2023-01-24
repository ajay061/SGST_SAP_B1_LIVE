using Common;
using Microsoft.Reporting.WebForms;
using System;
using System.Data;
using System.Web.UI;
using BusinessLayer;

namespace SGST.INE.Reports
{
    public partial class Container : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usertype"].ToString() != "ADMIN")
            {
                string _strRights = CommonHelper.GetRights("CONTAINER REPORT", (DataTable)Session["USER_RIGHTS"]);
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
                    BindPackingListNo();
                }
                catch (Exception ex)
                {
                    CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                }
            }
        }
        private void BindPackingListNo()
        {
            try
            {
                BL_ContainerReport blobj;
                blobj = new BL_ContainerReport();
                DataTable dtGRN = blobj.BINDPACKINGLIST();
                if (dtGRN.Rows.Count > 0)
                {
                    CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                    clsCommon.FillComboBox(drpPackingListNo, dtGRN, true);
                    drpPackingListNo.Focus();
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
                string sPackingListNo = string.Empty;
                if (drpPackingListNo.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select packing list no.", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpPackingListNo.Focus();
                    return;
                }
                if (drpPackingListNo.SelectedIndex > 0)
                {
                    sPackingListNo = drpPackingListNo.Text;
                }
                DataTable dtGetReport = new DataTable();
                BL_ContainerReport blobj = new BL_ContainerReport();
                dtGetReport = blobj.GetShipperReport(sPackingListNo, "", "");
                if (dtGetReport.Rows.Count > 0)
                {
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportDataSource datasource = new ReportDataSource();
                    CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/ReportViewer/FG Reports/ContainerPacking.rdlc");
                    datasource = new ReportDataSource("DataSet2", dtGetReport);
                    ReportViewer1.Visible = true;
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasource);
                    ReportViewer1.LocalReport.EnableExternalImages = true;
                    string imagePath = new Uri(Server.MapPath("~/images/ReportLogo.png")).AbsoluteUri;
                    ReportParameter parameter = new ReportParameter("rptLogo", imagePath);
                    ReportViewer1.LocalReport.SetParameters(parameter);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ShowMessage();", true);
                }
                else
                {
                    CommonHelper.ShowMessage("No details found.", msgerror, CommonHelper.MessageType.Error.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
    }
}