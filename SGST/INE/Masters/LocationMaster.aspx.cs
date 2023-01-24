using BusinessLayer.Masters;
using Common;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.IO;
using System.Configuration;
using System.Linq;

namespace SGST.INE.Masters
{
    public partial class LocationMaster : System.Web.UI.Page
    {
        static string sHeaderValue = string.Empty;
        static string sArea = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string sHeaderName = string.Empty;
                if (Request.QueryString["Name"] != null && Request.QueryString["Name"] != string.Empty)
                {
                    sHeaderValue = Request.QueryString["Name"].ToString();
                    if (Request.QueryString["Name"].Contains("WIP"))
                    {
                        lblHeader.Text = "WIP Location Master";
                        sArea = "WIP";
                    }
                    else if (Request.QueryString["Name"].Contains("FG"))
                    {
                        lblHeader.Text = "FG Location Master";
                        sArea = "FG";
                    }
                    else if (Request.QueryString["Name"].Contains("RM"))
                    {
                        lblHeader.Text = "RM Location Master";
                        sArea = "RM";
                    }
                }
                if (Session["usertype"].ToString() != "ADMIN")
                {
                    string _strRights = CommonHelper.GetRights(lblHeader.Text.ToUpper(), (DataTable)Session["USER_RIGHTS"]);
                    CommonHelper._strRights = _strRights.Split('^');
                    if (CommonHelper._strRights[0] == "0")  //Check view rights
                    {
                        Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                    }
                }
                if (!IsPostBack)
                {
                    ShowGridData();
                    txtLocationCode.Focus();
                    dvPrintergrup.Visible = true;
                    if (PCommon.sUseNetworkPrinter == "1")
                    {
                        bindPRINTER();
                    }
                    else
                    {
                        dvPrintergrup.Visible = false;
                    }
                    drpArea.Items.Add("--Select--");
                    if (lblHeader.Text.Contains("RM"))
                    {
                        drpArea.Items.Add("RM");
                        drplocatioType.Items.Add("REWORK");
                        drplocatioType.Items.Add("SCRAPE");
                    }
                    else if (lblHeader.Text.Contains("WIP"))
                    {
                        drpArea.Items.Add("WIP");
                    }
                    else
                    {
                        drpArea.Items.Add("FG");
                    }
                    bindPartCodes(sArea);
                    drpArea.SelectedIndex = 1;
                    drpArea.CssClass = "form-control select2";
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        private void bindPartCodes(string sArea)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                BL_LocationMaster blobj = new BL_LocationMaster();
                DataTable dtPartCode = blobj.GetPartCode(sArea, Session["SiteCode"].ToString());
                if (dtPartCode.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(drpPartCode, dtPartCode, true);
                    drpPartCode.SelectedIndex = 0;
                    drpPartCode.Focus();
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
                BL_LocationMaster dlobj = new BL_LocationMaster();
                DataTable dtLocation = dlobj.GetLocationRecord(sArea, Session["SiteCode"].ToString());
                if (dtLocation.Rows.Count == 0)
                {
                    gvLocationMst.DataSource = null;
                    gvLocationMst.DataBind();
                    lblNumberofRecords.Text = "0";
                }
                if (dtLocation.Rows.Count > 0)
                {
                    gvLocationMst.DataSource = dtLocation;
                    gvLocationMst.DataBind();
                    lblNumberofRecords.Text = dtLocation.Rows.Count.ToString();
                    dtLocation.TableName = "Table1";
                    dtLocation.AcceptChanges();
                    string[] columnNames = dtLocation.Columns.Cast<DataColumn>()
                                 .Select(x => x.ColumnName)
                                 .ToArray();
                    drpLocationColumnType.DataSource = columnNames;
                    drpLocationColumnType.DataBind();
                    ViewState["Data"] = dtLocation;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvLocationMst.PageIndex = e.NewPageIndex;
                ShowGridData();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }

        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                string _SN = string.Empty;
                string _sUserID = string.Empty;
                string _SiteCode = string.Empty;
                string[] strValue = e.CommandArgument.ToString().Split('~');
                _SN = e.CommandArgument.ToString();
                if (e.CommandName == "DeleteRecords")
                {
                    DeleteRecords(_SN);
                }
                if (e.CommandName == "EditRecords")
                {
                    if (btnSave.Text == "Save")
                    { btnSave.Text = "Update"; }
                    EditRecords(_SN);
                }
                ShowGridData();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void EditRecords(string sLocationCode)
        {
            try
            {
                BL_LocationMaster dlobj = new BL_LocationMaster();
                DataTable dtLocationRecord = dlobj.GetSeletedData(sLocationCode, sArea, Session["SiteCode"].ToString());
                btnSave.Visible = true;
                if (dtLocationRecord.Rows.Count > 0)
                {
                    txtLocationCode.Text = dtLocationRecord.Rows[0]["LOCATIONCODE"].ToString();
                    drpArea.Text = dtLocationRecord.Rows[0]["LOCATIONAREA"].ToString();
                    drplocatioType.Text = dtLocationRecord.Rows[0]["LOCATIONTYPE"].ToString();
                    txtDescription.Text = dtLocationRecord.Rows[0]["DESCRIPTION"].ToString();
                    txtWHLocation.Text = dtLocationRecord.Rows[0]["WH_LOC"].ToString();
                    if (dtLocationRecord.Rows[0]["PART_CODE"].ToString() != "")
                    {
                        drpPartCode.Text = dtLocationRecord.Rows[0]["PART_CODE"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        private void DeleteRecords(string sLocationCode)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                BL_LocationMaster blobj = new BL_LocationMaster();
                string sResult = blobj.DeleteLocation(sLocationCode, sArea, Session["SiteCode"].ToString());
                if (sResult.Length > 0)
                {
                    if (sResult.StartsWith("ERROR~"))
                    {
                        CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    if (sResult.StartsWith("N~"))
                    {
                        CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    else
                    {
                        CommonHelper.ShowMessage(sResult.Split('~')[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                    }
                }
                else
                {
                    CommonHelper.ShowMessage("No result found.", msgerror, CommonHelper.MessageType.Error.ToString());
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("REFERENCE constraint"))
                {
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                    CommonHelper.ShowMessage("location already in transaction, it can not be delete.", msgerror, CommonHelper.MessageType.Error.ToString());
                }
                else
                {
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                    CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                }

            }
            finally
            {
                ShowGridData();
            }
        }
        public void save()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (txtLocationCode.Text.Trim() == string.Empty)
                {
                    CommonHelper.ShowMessage("Please enter location code.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtLocationCode.Focus();
                    return;
                }
                if (drplocatioType.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select location type.", msgerror, CommonHelper.MessageType.Error.ToString());
                    drplocatioType.Focus();
                    return;
                }
                if (txtWHLocation.Text.Trim() == string.Empty)
                {
                    CommonHelper.ShowMessage("Please enter WH location.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtWHLocation.Focus();
                    return;
                }
                else
                {
                    string sPartCode = string.Empty;
                    if (drpPartCode.SelectedIndex > 0)
                    {
                        sPartCode = drpPartCode.Text;
                    }
                    BL_LocationMaster dlobj = new BL_LocationMaster();
                    string _OuptPut = string.Empty;
                    if (btnSave.Text == "Save")
                    {
                        _OuptPut = dlobj.SaveLocationData(txtLocationCode.Text.Trim(), sArea,
    drplocatioType.SelectedItem.Text.Trim(), txtDescription.Text.Trim(),
    Session["UserID"].ToString(), sPartCode, txtWHLocation.Text, Session["SiteCode"].ToString());
                    }
                    else
                    {
                        _OuptPut = dlobj.UpdateLocation(hidUID.Value, txtLocationCode.Text.Trim(),
                       sArea,
                        drplocatioType.SelectedItem.Text.Trim(), txtDescription.Text.Trim(),
                        Session["UserID"].ToString(), sPartCode, txtWHLocation.Text, Session["SiteCode"].ToString());
                    }
                    if (_OuptPut.StartsWith("N~") || _OuptPut.StartsWith("ERROR~"))
                    {
                        CommonHelper.ShowMessage(_OuptPut.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    else if (_OuptPut.ToUpper().StartsWith("SUCCESS~"))
                    {
                        CommonHelper.ShowMessage(_OuptPut.Split('~')[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                    }
                    else
                    {
                        CommonHelper.ShowMessage("No result found.", msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    txtLocationCode.Text = "";
                    drplocatioType.SelectedIndex = 0;
                    txtDescription.Text = "";
                    if (ddlPrinter.Items.Count > 0)
                    {
                        ddlPrinter.SelectedIndex = 0;
                    }
                    txtLocationCode.Focus();
                    txtWHLocation.Text = string.Empty;
                    txtLocationCode.ReadOnly = false;
                    drpLocationFilter.Items.Clear();
                    drpLocationColumnType.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
            finally
            {
                ShowGridData();
                btnSave.Text = "Save";
                txtLocationCode.ReadOnly = false;

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            save();
        }

        protected void txtLocationCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                //drpArea.Focus();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
           
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            txtDescription.Text = "";
            txtLocationCode.Text = "";
            txtLocationCode.Focus();
            drplocatioType.SelectedIndex = 0;
            txtLocationCode.ReadOnly = false;
            btnSave.Text = "Save";
            drpPartCode.SelectedIndex = 0;
            txtWHLocation.Text = string.Empty;
            if (drpLocationFilter.Items.Count > 0)
            {
                drpLocationFilter.SelectedIndex = 0;
                gvLocationMst.DataSource = null;
                gvLocationMst.DataBind();
                DataTable dt = (DataTable)ViewState["Data"];
                gvLocationMst.DataSource = dt;
                gvLocationMst.DataBind();
            }
        }
        public void bindPRINTER()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                string sPrinter = string.Empty;
                BL_Common blobj = new BL_Common();
                DataTable dt = blobj.BINDPRINTER("RM");
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(ddlPrinter, dt, true);
                    ddlPrinter.SelectedIndex = 0;
                    ddlPrinter.Focus();
                }
                else
                {
                    CommonHelper.ShowMessage("No result found.", msgerror, CommonHelper.MessageType.Error.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlPrinter.SelectedIndex == 0 && dvPrintergrup.Visible == true)
                {
                    CommonHelper.ShowMessage("Please select printer.", msgerror, CommonHelper.MessageType.Error.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    return;
                }

                string sLineCode = Session["LINECODE"].ToString();
                string sUserID = Session["UserID"].ToString();
                BL_LocationMaster blobj = new BL_LocationMaster();
                string sResult = string.Empty;
                foreach (GridViewRow item in gvLocationMst.Rows)
                {
                    if (item.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chk = (item.FindControl("chkSelect") as CheckBox);
                        if (chk.Checked)
                        {
                            string lblLocationCode = gvLocationMst.Rows[item.RowIndex].Cells[1].Text;
                            string lbldescription = gvLocationMst.Rows[item.RowIndex].Cells[4].Text;
                            string lblPartCode = gvLocationMst.Rows[item.RowIndex].Cells[5].Text;
                            sResult = blobj.LocationPrinting(lblLocationCode, lbldescription, ddlPrinter.Text, lblPartCode
                                , sUserID, sLineCode
                                );
                        }
                    }
                }
                if (sResult.Length > 0)
                {
                    if (sResult.StartsWith("ERROR"))
                    {
                        CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    else if (sResult.StartsWith("N~"))
                    {
                        CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    else if (sResult.StartsWith("PRINTERNOTCONNECTED~"))
                    {
                        CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    else if (sResult.StartsWith("PRNNOTFOUND~"))
                    {
                        CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    else if (sResult.StartsWith("SUCCESS~"))
                    {
                        ShowGridData();
                        CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                        CommonHelper.ShowMessage(sResult.Split('~')[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                    }
                    else
                    {
                        CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                }
                else
                {
                    CommonHelper.ShowMessage("No result found for printing.", msgerror, CommonHelper.MessageType.Error.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                if (ex.Message.Contains("The string was not recognized as a valid DateTime."))
                {
                    CommonHelper.ShowMessage("Date is not in correct format.", msgerror, CommonHelper.MessageType.Error.ToString());
                }
                else
                {
                    CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                }
            }
        }

        protected void drpLocationFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpLocationFilter.SelectedIndex > 0)
                {
                    DataTable dt = (DataTable)ViewState["Data"];
                    DataView dataView = dt.DefaultView;
                    dataView.RowFilter = "" + drpLocationColumnType.Text + " = '" + drpLocationFilter.Text + "'";
                    gvLocationMst.DataSource = dataView;
                    gvLocationMst.DataBind();
                }
                else
                {
                    DataTable dt = (DataTable)ViewState["Data"];
                    gvLocationMst.DataSource = dt;
                    gvLocationMst.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                DataTable dt = new DataTable();
                string sProgramID = string.Empty;
                if (Path.GetExtension(FileUpload1.FileName).Equals(".csv"))
                {
                    DirectoryInfo _dir = null;
                    string CSVFilePath = Path.GetFileName(FileUpload1.FileName);    //getting full file path of Uploaded file  
                    string sUploadFilePath = ConfigurationManager.AppSettings["Upload_File_Path"].ToString();
                    string sPath = Server.MapPath("~/" + sUploadFilePath + "//");
                    _dir = new DirectoryInfo(sPath);
                    if (_dir.Exists == false)
                    {
                        _dir.Create();
                        Directory.CreateDirectory(_dir.ToString());
                    }
                    FileUpload1.SaveAs(Server.MapPath("~/Upload_File//") + CSVFilePath);
                    string sFileName = Server.MapPath("~/Upload_File//") + CSVFilePath;
                    dt = PCommon.ConvertCSVtoDataTable(sFileName);
                    if (dt.Columns.Count != 7)
                    {
                        CommonHelper.ShowMessage("Invalid file,No of columns should be 7", msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                    if (dt.Columns[0].ToString() != "SITECODE")
                    {
                        CommonHelper.ShowMessage("Invalid file", msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                    else if (dt.Columns[1].ToString() != "PART_CODE")
                    {
                        CommonHelper.ShowMessage("Invalid file,Column name not matched", msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                    else if (dt.Columns[2].ToString() != "LOCATIONCODE")
                    {
                        CommonHelper.ShowMessage("Invalid file,Column name not matched", msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                    else if (dt.Columns[3].ToString() != "LOCATIONAREA")
                    {
                        CommonHelper.ShowMessage("Invalid file,Column name not matched", msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                    else if (dt.Columns[4].ToString() != "LOCATIONTYPE")
                    {
                        CommonHelper.ShowMessage("Invalid file,Column name not matched", msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                    else if (dt.Columns[5].ToString() != "DESCRIPTION")
                    {
                        CommonHelper.ShowMessage("Invalid file,Column name not matched", msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                    else if (dt.Columns[6].ToString() != "WH_LOC")
                    {
                        CommonHelper.ShowMessage("Invalid file,Column name not matched", msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                    BL_LocationMaster blobj = new BL_LocationMaster();
                    string Message = string.Empty;
                    string sResult = blobj.UploadLocation(dt, Session["UserID"].ToString());
                    if (sResult.ToUpper().StartsWith("SUCCESS"))
                    {
                        Message = sResult.Split('~')[1];
                        CommonHelper.ShowMessage(Message, msgsuccess, CommonHelper.MessageType.Success.ToString());
                        return;
                    }
                    else if (sResult.StartsWith("N~"))
                    {
                        Message = sResult.Split('~')[1];
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                    else if (sResult.StartsWith("ERROR~"))
                    {
                        Message = sResult.Split('~')[1];
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    else if (sResult.StartsWith("NOTFOUND~"))
                    {
                        Message = sResult.Split('~')[1];
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                }
                else
                {
                    CommonHelper.ShowMessage("Please select valid(.csv) file only", msgerror, CommonHelper.MessageType.Error.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void drpLocationColumnType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpLocationColumnType.SelectedIndex > 0)
                {
                    DataTable dtLocation = (DataTable)ViewState["Data"];
                    System.Data.DataView view = new System.Data.DataView(dtLocation);
                    System.Data.DataTable selected =
                            view.ToTable("Table1", true, drpLocationColumnType.Text);
                    clsCommon.FillComboBox(drpLocationFilter, selected, true);
                }
                else
                {
                    drpLocationFilter.Items.Clear();
                }

            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        public void SearchData()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtLocationFilter.Text.Trim()))
                {
                    DataTable dt = (DataTable)ViewState["Data"];
                    DataView dataView = dt.DefaultView;
                    dataView.RowFilter = " locationcode like '%" + txtLocationFilter.Text + "%'";
                    gvLocationMst.DataSource = dataView;
                    gvLocationMst.DataBind();
                }
                else
                {
                    DataTable dt = (DataTable)ViewState["Data"];
                    gvLocationMst.DataSource = dt;
                    gvLocationMst.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void txtLocationFilter_TextChanged(object sender, EventArgs e)
        {
            SearchData();
        }
    }
}