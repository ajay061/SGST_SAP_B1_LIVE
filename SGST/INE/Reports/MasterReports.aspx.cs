using Common;
using System;
using System.Data;
using System.Web.UI;
using BusinessLayer.Reports;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using System.IO;

namespace SGST.INE.Reports
{
    public partial class MasterReports : System.Web.UI.Page
    {
        static DataTable dtTrackingData = new DataTable();
        static DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usertype"].ToString() != "ADMIN")
            {
                string _strRights = CommonHelper.GetRights("MASTER DATA REPORT", (DataTable)Session["USER_RIGHTS"]);
                CommonHelper._strRights = _strRights.Split('^');
                if (CommonHelper._strRights[0] == "0")  //Check view rights
                {
                    Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                }
            }
            Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
            Response.AddHeader("Pragma", "no-cache");
            Response.AddHeader("Expires", "0");
        }
        protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpType.SelectedIndex > 0)
                {
                    string sType = string.Empty;
                    sType = drpType.Text;
                    dtTrackingData.Rows.Clear();
                    DataTable dtFilterData = new DataTable();
                    BL_MasterReports blobj = new BL_MasterReports();
                    dtTrackingData = blobj.GetReports(Session["SiteCode"].ToString(), sType, "");
                    System.Data.DataView view = new System.Data.DataView(dtTrackingData);
                    if (drpType.SelectedIndex == 1)
                    {
                        SearchHeader.Text = "FG Item Code :";
                        dtFilterData = view.ToTable("Table1", true, "FG_ITEM_CODE");
                    }
                    if (drpType.SelectedIndex == 2)
                    {
                        SearchHeader.Text = "Item Code :";
                        dtFilterData = view.ToTable("Table1", true, "PART_CODE");
                    }
                    if (drpType.SelectedIndex == 3)
                    {
                        SearchHeader.Text = "Item Code :";
                        dtFilterData = view.ToTable("Table1", true, "ITEMCODE");
                    }
                    clsCommon.FillComboBox(drpSearchData, dtFilterData, true);
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        private void ShowGridData()
        {
            try
            {
                DataView dataView = dtTrackingData.AsDataView();
                if (drpType.SelectedIndex == 1)
                {
                    if (drpSearchData.SelectedIndex > 0)
                    {
                        dataView.RowFilter = "FG_ITEM_CODE = '" + drpSearchData.Text + "'";
                    }
                }
                else if (drpType.SelectedIndex == 2)
                {
                    if (drpSearchData.SelectedIndex > 0)
                    {
                        dataView.RowFilter = "PART_CODE = '" + drpSearchData.Text + "'";
                    }
                }
                else if (drpType.SelectedIndex == 3)
                {
                    if (drpSearchData.SelectedIndex > 0)
                    {
                        dataView.RowFilter = " ITEMCODE = '" + drpSearchData.Text + "'";
                    }
                }
                if (dtTrackingData.Rows.Count == 0)
                {
                    gvData.DataSource = dataView;
                    gvData.DataBind();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ShowMessage();", true);
                    gvData.EmptyDataText = "No Records Found";
                }
                else
                {
                    dt = dataView.ToTable();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ShowMessage();", true);
                    lblHeader.Text = drpType.Text;
                    if (dt.Rows.Count > 10000)
                    {
                        DownLoadExcelFile();
                    }
                    else
                    {
                        gvData.DataSource = dataView;
                        gvData.DataBind();
                        gvData.UseAccessibleHeader = true;
                        gvData.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        protected void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                ShowGridData();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
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
        public MemoryStream GetStream(XLWorkbook excelWorkbook)
        {
            MemoryStream fs = new MemoryStream();
            excelWorkbook.SaveAs(fs);
            fs.Position = 0;
            return fs;
        }
        public void DownLoadExcelFile()
        {
            try
            {
                DataTable dt = dtTrackingData;
                string sFileName = "MasterData" + "_" + drpType.Text + "_" +
                    DateTime.Now.ToShortDateString();
                string attachment = "attachment; filename=" + sFileName + ".xlsx";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.ms-excel";
                string tab = "";
                foreach (DataColumn dc in dt.Columns)
                {
                    Response.Write(tab + dc.ColumnName);
                    tab = "\t";
                }
                Response.Write("\n");
                int i;
                foreach (DataRow dr in dt.Rows)
                {
                    tab = "";
                    for (i = 0; i < dt.Columns.Count; i++)
                    {
                        Response.Write(tab + dr[i].ToString());
                        tab = "\t";
                    }
                    Response.Write("\n");
                }
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
        protected void btnDownloadData_Click(object sender, EventArgs e)
        {
            try
            {
                using (XLWorkbook wb = new XLWorkbook())
                {

                    wb.Worksheets.Add(dt, "MasterData");
                    string myName = Server.UrlEncode("MasterData" + "_" + drpType.Text + "_" +
                    DateTime.Now.ToShortDateString() + ".xlsx");
                    System.IO.MemoryStream stream = GetStream(wb);// The method is defined below
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition",
                    "attachment; filename=" + myName);
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.BinaryWrite(stream.ToArray());
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
            finally
            {
                dtTrackingData.Rows.Clear();
                dt.Rows.Clear();
            }
        }
    }
}