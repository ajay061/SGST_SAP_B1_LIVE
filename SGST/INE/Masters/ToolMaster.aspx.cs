using Common;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGST.INE.WIP
{
    public partial class ToolMaster : System.Web.UI.Page
    {
        string Message = string.Empty;
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Signin/v1/Login.aspx?Session=Null");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["usertype"].ToString() != "ADMIN")
                {
                    string _strRights = CommonHelper.GetRights("TOOL MASTER", (DataTable)Session["USER_RIGHTS"]);
                    CommonHelper._strRights = _strRights.Split('^');
                    if (CommonHelper._strRights[0] == "0")  //Check view rights
                    {
                        Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                    }
                }
                if (!IsPostBack)
                {
                    ShowGridData();                    
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, 
                   System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        private void ShowGridData()
        {
            try
            {
                DataTable dt = new DataTable();
                BL_ToolMaster dlobj = new BL_ToolMaster();
                dt = dlobj.GetToolID(Session["SiteCode"].ToString());
                lblNumberofRecords.Text = dt.Rows.Count.ToString();
                if (dt.Rows.Count > 0)
                {
                    gvToolId.DataSource = dt;
                    gvToolId.DataBind();
                    dt.TableName = "Table1";
                    dt.AcceptChanges();
                    System.Data.DataView view = new System.Data.DataView(dt);
                    System.Data.DataTable selected =
                            view.ToTable("Table1", false, "TOOL_ID");
                    clsCommon.FillComboBox(drpToolFilter, selected, true);
                    ViewState["Data"] = dt;                    
                }
                else
                {
                    gvToolId.DataSource = null;
                    gvToolId.DataBind();
                    lblNumberofRecords.Text = "0";
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        private void EditRecords(string _SN)
        {
            try
            {
                BL_ToolMaster dlobj = new BL_ToolMaster();
                DataTable dtToolDetails = dlobj.GetSeletedData(_SN, Session["SiteCode"].ToString());
                if (dtToolDetails.Rows.Count > 0)
                {
                    txttoolid.Text = dtToolDetails.Rows[0]["tool_id"].ToString();
                    txtDescription.Text = dtToolDetails.Rows[0]["description"].ToString();
                    ddltype.SelectedItem.Text = dtToolDetails.Rows[0]["type"].ToString();
                    txtqty.Text = dtToolDetails.Rows[0]["qty"].ToString();
                    txtmake.Text = dtToolDetails.Rows[0]["Make"].ToString();
                    txtModelNo.Text = dtToolDetails.Rows[0]["ModelNo"].ToString();
                    txtEquipSRNo.Text = dtToolDetails.Rows[0]["EqipSRNo"].ToString();
                    txtUsageRange.Text = dtToolDetails.Rows[0]["UsageRange"].ToString();
                    txtAccuracy.Text = dtToolDetails.Rows[0]["Accuracy"].ToString();
                    txtCalibrationDate.Text = dtToolDetails.Rows[0]["CalibrationDate"].ToString();
                    txtAlertDate.Text = dtToolDetails.Rows[0]["AlertDate"].ToString();
                    hidUpdate.Value = "Update";
                    hidUID.Value = _SN;
                }
                else
                {
                    CommonHelper.ShowMessage("No Tool Master details found", msgerror, CommonHelper.MessageType.Error.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        private void DeleteRecords(string _SN)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                BL_ToolMaster blobj = new BL_ToolMaster();
                string sResult = blobj.DeleteTool(_SN, Session["SiteCode"].ToString());
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
                        CommonHelper.ShowMessage(sResult.Split('~')[1], msgsuccess, CommonHelper.MessageType.Error.ToString());
                    }
                }
                else
                {
                    CommonHelper.ShowMessage("No result found", msgerror, CommonHelper.MessageType.Error.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
            finally
            {
                ShowGridData();
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (btnSave.Text == "Save")
                {
                    if (txttoolid.Text == string.Empty)
                    {
                        CommonHelper.ShowMessage("Please enter tool id.", msgerror, CommonHelper.MessageType.Error.ToString());
                        txttoolid.Focus();
                        return;
                    }
                    if (ddltype.SelectedIndex == 0)
                    {
                        CommonHelper.ShowMessage("Please select type.", msgerror, CommonHelper.MessageType.Error.ToString());
                        ddltype.Focus();
                        return;
                    }
                    if (txtDescription.Text == "")
                    {
                        CommonHelper.ShowMessage("Please enter description.", msgerror, CommonHelper.MessageType.Error.ToString());
                        txtDescription.Focus();
                        return;
                    }
                    if (txtCalibrationDate.Text == "")
                    {
                        CommonHelper.ShowMessage("Please enter expiry date", msgerror, CommonHelper.MessageType.Error.ToString());
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                        return;
                    }
                    if (txtAlertDate.Text == "")
                    {
                        CommonHelper.ShowMessage("Please enter expiry date", msgerror, CommonHelper.MessageType.Error.ToString());
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                         return;
                    }
                    //if (Convert.ToDateTime(txtCalibrationDate.Text).Date < Convert.ToDateTime(DateTime.Now).Date)
                    //{
                    //    CommonHelper.ShowMessage("Calibration date always lesser or equal to current date", msgerror, CommonHelper.MessageType.Error.ToString());
                    //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    //    txtCalibrationDate.Text = string.Empty;
                    //    txtCalibrationDate.Focus();
                    //    return;
                    //}
                    if (Convert.ToDateTime(txtAlertDate.Text).Date < Convert.ToDateTime(DateTime.Now).Date)
                    {
                        CommonHelper.ShowMessage("Alert date always greater or equal to current date", msgerror, CommonHelper.MessageType.Error.ToString());
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                        txtAlertDate.Text = string.Empty;
                        txtAlertDate.Focus();
                        return;
                    }
                    if (Convert.ToDateTime(txtAlertDate.Text) < Convert.ToDateTime(txtCalibrationDate.Text))
                    {
                        CommonHelper.ShowMessage("alert date can't be lesser than calibration date", msgerror, CommonHelper.MessageType.Error.ToString());
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                        txtAlertDate.Text = string.Empty;
                        txtAlertDate.Focus();
                        return;
                    }
                    if (txtqty.Text == string.Empty)
                    {
                        CommonHelper.ShowMessage("Please enter quantity.", msgerror, CommonHelper.MessageType.Error.ToString());
                        txtqty.Focus();
                        return;
                    }
                    BL_ToolMaster dlobj = new BL_ToolMaster();
                    string sResult = dlobj.SaveToolID(txttoolid.Text, txtDescription.Text, ddltype.SelectedItem.Text, txtqty.Text, ""
                        , Session["SiteCode"].ToString(),
                        txtmake.Text,txtModelNo.Text,txtEquipSRNo.Text,txtUsageRange.Text,txtAccuracy.Text,Convert.ToDateTime(txtCalibrationDate.Text),
                        Convert.ToDateTime(txtAlertDate.Text)
                        );
                    if (sResult.Length > 0)
                    {
                        if (sResult.StartsWith("ERROR~"))
                        {
                            CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        }
                        else if (sResult.StartsWith("N~"))
                        {
                            CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        }
                        else
                        {
                            _ResetField();
                            CommonHelper.ShowMessage(sResult.Split('~')[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                        }
                    }
                    else
                    {
                        CommonHelper.ShowMessage("No result found.", msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                }
                else
                {
                    if (txtCalibrationDate.Text == "")
                    {
                        CommonHelper.ShowMessage("Please enter expiry date", msgerror, CommonHelper.MessageType.Error.ToString());
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                        return;
                    }
                    if (txtAlertDate.Text == "")
                    {
                        CommonHelper.ShowMessage("Please enter expiry date", msgerror, CommonHelper.MessageType.Error.ToString());
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                        return;
                    }
                    //if (Convert.ToDateTime(txtCalibrationDate.Text).Date < Convert.ToDateTime(DateTime.Now).Date)
                    //{
                    //    CommonHelper.ShowMessage("Calibration date always lesser or equal to current date", msgerror, CommonHelper.MessageType.Error.ToString());
                    //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    //    txtCalibrationDate.Text = string.Empty;
                    //    txtCalibrationDate.Focus();
                    //    return;
                    //}
                    if (Convert.ToDateTime(txtAlertDate.Text).Date < Convert.ToDateTime(DateTime.Now).Date)
                    {
                        CommonHelper.ShowMessage("Alert date always greater or equal to current date", msgerror, CommonHelper.MessageType.Error.ToString());
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                        txtAlertDate.Text = string.Empty;
                        txtAlertDate.Focus();
                        return;
                    }
                    if (Convert.ToDateTime(txtAlertDate.Text) < Convert.ToDateTime(txtCalibrationDate.Text))
                    {
                        CommonHelper.ShowMessage("alert date can't be lesser than calibration date", msgerror, CommonHelper.MessageType.Error.ToString());
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                        txtAlertDate.Text = string.Empty;
                        txtAlertDate.Focus();
                        return;
                    }
                    BL_ToolMaster blobj = new BL_ToolMaster();
                    string sResult = blobj.UpdateTool(txttoolid.Text.Trim(), txtDescription.Text.Trim(), ddltype.SelectedItem.Text.Trim(),
                        txtqty.Text, "", Session["SiteCode"].ToString(),
                        txtmake.Text, txtModelNo.Text, txtEquipSRNo.Text, txtUsageRange.Text, txtAccuracy.Text, Convert.ToDateTime(txtCalibrationDate.Text),
                        Convert.ToDateTime(txtAlertDate.Text));
                    if (sResult.Length > 0)
                    {
                        if (sResult.StartsWith("ERROR~"))
                        {
                            txttoolid.Text = hidToolID.Value;
                            CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        }
                        if (sResult.StartsWith("N~"))
                        {
                            txttoolid.Text = hidToolID.Value;
                            CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        }
                        else
                        {
                            _ResetField();
                            ShowGridData();
                            txttoolid.Text = hidToolID.Value;
                            CommonHelper.ShowMessage(sResult.Split('~')[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                        }
                    }
                    else
                    {
                        CommonHelper.ShowMessage("No result found.", msgerror, CommonHelper.MessageType.Error.ToString());
                    }

                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
            finally
            {
                ShowGridData();
            }
        }

        protected void gvToolId_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvToolId.PageIndex = e.NewPageIndex;
                ShowGridData();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void gvToolId_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                string _SN = string.Empty;
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
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (!IsPostBack)
                {
                    ShowGridData();
                }
                _ResetField();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        private void _ResetField()
        {
            txttoolid.Text = "";
            txtDescription.Text = "";
            txtqty.Text = "";
            txtmake.Text = string.Empty;
            txtModelNo.Text = string.Empty;
            txtAccuracy.Text = string.Empty;
            txtAlertDate.Text = string.Empty;
            txtCalibrationDate.Text = string.Empty;
            txtEquipSRNo.Text = string.Empty;
            txtUsageRange.Text = string.Empty;
            ddltype.SelectedIndex = 0;
            btnSave.Text = "Save";
            if (drpToolFilter.Items.Count> 0)
            {
                drpToolFilter.SelectedIndex = 0;
            }           
            DataTable dt = (DataTable)ViewState["Data"];
            gvToolId.DataSource = dt;
            gvToolId.DataBind();

        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string confirmValue = Request.Form["confirm_value"];
                string _sPrinterPort = ConfigurationManager.AppSettings["sPrinterPort"].ToString();

                string sLineCode = Session["LINECODE"].ToString();
                string sUserID = Session["UserID"].ToString();
                BL_ToolMaster blobj = new BL_ToolMaster();
                string sResult = "";
                bool bPrint = false;
                foreach (GridViewRow item in gvToolId.Rows)
                {
                    if (item.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chk = (item.FindControl("chkSelect") as CheckBox);
                        if (chk.Checked)
                        {
                            bPrint = true;
                            string sID = item.Cells[1].Text;
                            string sTOOLDesc = item.Cells[2].Text;
                            sResult = blobj.ToolBinPrinting(sID, _sPrinterPort, "", sTOOLDesc, "Tool", confirmValue
                             , sUserID, sLineCode, Session["SiteCode"].ToString()
                                );
                        }
                    }
                }
                if (bPrint == false)
                {
                    CommonHelper.ShowMessage("Please select at least one tool for print.", msgerror, CommonHelper.MessageType.Error.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    return;
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
                        CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);                     
                        CommonHelper.ShowMessage(sResult.Split('~')[1], msgsuccess, CommonHelper.MessageType.Error.ToString());
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
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
            finally
            {
                ShowGridData();
            }
        }

        protected void drpToolFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpToolFilter.SelectedIndex > 0)
                {
                    DataTable dt = (DataTable)ViewState["Data"];
                    DataView dataView = dt.DefaultView;
                    dataView.RowFilter = "TOOL_ID = '" + drpToolFilter.SelectedValue + "'";
                    gvToolId.DataSource = dataView;
                    gvToolId.DataBind();
                }
                else
                {
                    DataTable dt = (DataTable)ViewState["Data"];
                    gvToolId.DataSource = dt;
                    gvToolId.DataBind();
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
                    dt = PCommon.ConvertCSVtoDataTable_ToolData(sFileName);
                    if (dt.Columns.Count != 12)
                    {
                        CommonHelper.ShowMessage("Invalid file,No of columns should be 5", msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                    if (dt.Columns[0].ToString() != "SITE_CODE")
                    {
                        CommonHelper.ShowMessage("Invalid file, Wrong column name", msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                    if (dt.Columns[1].ToString() != "TOOL_ID")
                    {
                        CommonHelper.ShowMessage("Invalid file, Wrong column name", msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }                   

                    BL_ToolMaster blobj = new BL_ToolMaster();
                    string Message = string.Empty;
                    DataTable dtResult = blobj.UploadTool(dt, Session["UserID"].ToString());
                    string sResult = string.Empty;
                    if (dtResult.Rows.Count > 0)
                    {
                        sResult = dtResult.Rows[0][0].ToString();
                    }
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
                        if (sResult.ToUpper().Contains("PRIMARY"))
                        {
                            Message = "Data in selected file already uploaded in database, Please upload fresh record only";
                        }
                        else if (sResult.ToUpper().Contains("FOREIGN KEY"))
                        {
                            Message = "Data in selected file not found in master tables, Please check master data before upload";
                        }
                        else
                        {
                            Message = sResult.Split('~')[1];
                        }
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
    }
}