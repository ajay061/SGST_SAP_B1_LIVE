using Common;
using Microsoft.Reporting.WebForms;
using System;
using System.Data;
using System.Web.UI;
using BusinessLayer.Reports;

namespace SGST.INE.Reports
{
    public partial class ShipperLabelReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
            Response.AddHeader("Pragma", "no-cache");
            Response.AddHeader("Expires", "0");
            if (!IsPostBack)
            {
                if (Session["usertype"].ToString() != "ADMIN")
                {
                    string _strRights = CommonHelper.GetRights("SHIPPER LABEL REPORT", (DataTable)Session["USER_RIGHTS"]);
                    CommonHelper._strRights = _strRights.Split('^');
                    if (CommonHelper._strRights[0] == "0")  //Check view rights
                    {
                        Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                    }
                }
                try
                {
                    BindBoxID();
                }
                catch (Exception ex)
                {
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                }
            }
        }
        private void BindBoxID()
        {
            try
            {
                BL_FG_Box_Label_Report blobj;
                blobj = new BL_FG_Box_Label_Report();
                DataTable dtGRN = blobj.BindBoxID();
                if (dtGRN.Rows.Count > 0)
                {
                    CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                    clsCommon.FillComboBox(drpBoxID, dtGRN, true);
                    drpBoxID.Focus();
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
                string sBoxID = string.Empty;
                if (drpBoxID.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select box id.", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpBoxID.Focus();
                    return;
                }
                if (drpBoxID.SelectedIndex > 0)
                {
                    sBoxID = drpBoxID.Text;
                }
                DataTable dtGetReport = new DataTable();
                BL_FG_Box_Label_Report blobj;
                blobj = new BL_FG_Box_Label_Report();
                dtGetReport = blobj.GetReport(sBoxID);
                if (dtGetReport.Rows.Count > 0)
                {
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportDataSource datasource = new ReportDataSource();
                    CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/ReportViewer/FG Reports/ShipperLabel.rdlc");
                    datasource = new ReportDataSource("DataSet1", dtGetReport);
                    ReportViewer1.Visible = true;
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasource);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ShowMessage();", true);
                }
                else
                {
                    CommonHelper.ShowMessage("No details found.", msgerror, CommonHelper.MessageType.Error.ToString());
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