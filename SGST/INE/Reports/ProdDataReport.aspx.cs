using Common;
using System;
using System.Data;
using System.Web.UI;
using BusinessLayer.Reports;
using Microsoft.Reporting.WebForms;

namespace SGST.INE.Reports
{
    public partial class ProdDataReport : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Signin/v1/Login.aspx?Session=Null");
            }
        }
        static DataTable dtTrackingData = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usertype"].ToString() != "ADMIN")
            {
                string _strRights = CommonHelper.GetRights("PROD DATA REPORT", (DataTable)Session["USER_RIGHTS"]);
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
                BL_OQCSamplingReport blobj;
                blobj = new BL_OQCSamplingReport();
                DataTable dtGRN = blobj.BindProdFGItemCode();
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
                if (drpFGItemCode.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select model", msgerror, CommonHelper.MessageType.Error.ToString());
                    return;
                }
                sFGItemCode = drpFGItemCode.Text;
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                BL_OQCSamplingReport blobj = new BL_OQCSamplingReport();
                DataTable dtTrackingData = blobj.GetProdDataReport(txtFromDate.Text, txtToDate.Text, sFGItemCode);
                if (dtTrackingData.Rows.Count > 0)
                {
                    if (dtTrackingData.Columns.Count == 1 && dtTrackingData.Rows[0][0].ToString().StartsWith("ERROR~"))
                    {
                        CommonHelper.ShowMessage(dtTrackingData.Rows[0][0].ToString(), msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtMessage,
                          System.Reflection.MethodBase.GetCurrentMethod().Name, "Prod Data Report No of records :" + dtTrackingData.Rows.Count);
                    string sFileName = "Production_Report-" + drpFGItemCode.Text.Trim() + DateTime.Now.ToString("_dd_MM_yyyy");
                    DownLoadcsvFile(dtTrackingData, sFileName);
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
        public void DownLoadcsvFile(DataTable dtTrackingData, string sFileName)
        {
            try
            {
                Response.Clear();
                string attachment = "attachment; filename=" + sFileName + ".csv";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.Charset = "";
                Response.ContentType = "application/text";
                string csv = string.Empty;
                foreach (DataColumn column in dtTrackingData.Columns)
                {
                    if (column.ColumnName.ToUpper() == "VMX DATA")
                    {
                        csv += column.ColumnName + ',';
                        csv += "VMX2,VMX3,VMX4,VMX5,VMX6,";
                    }
                    else
                    {
                        csv += column.ColumnName + ',';
                    }
                }

                //Add new line.
                csv += "\r\n";
                foreach (DataRow row in dtTrackingData.Rows)
                {
                    foreach (DataColumn column in dtTrackingData.Columns)
                    {
                        if (column.ColumnName.ToUpper() == "VMX DATA")
                        {
                            if (row[column.ColumnName].ToString().Length > 16)
                            {
                                int iCount = row[column.ColumnName].ToString().Split(',').Length;
                                for (int i = 0; i < iCount; i++)
                                {
                                    if (row[column.ColumnName].ToString().Split(',')[i].StartsWith("0"))
                                    {
                                        csv +=  row[column.ColumnName].ToString().Split(',')[i] + ',';
                                    }
                                    else
                                    {
                                        csv +=  row[column.ColumnName].ToString().Split(',')[i] + ',';
                                    }
                                }
                            }
                            else
                            {
                                csv += row[column.ColumnName].ToString().Replace(",", ";") + ',';
                                csv += ",,,,,";
                            }
                        }
                        else
                        {
                            csv += row[column.ColumnName].ToString().Replace(",", ";") + ',';
                        }
                        //Add the Data rows.

                    }
                    //Add new line.
                    csv += "\r\n";
                }
                Response.Output.Write(csv);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                if (ex.Message.Contains("System.OutOfMemoryException"))
                {
                    CommonHelper.ShowCustomErrorMessage("System Memory is full, Please select different dates", msgerror);
                }
                else
                {
                    CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                }
            }
        }
    }
}