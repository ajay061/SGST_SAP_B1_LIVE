using Common;
using System;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.IO;
using System.Globalization;

namespace SGST.INE.FG
{
    /// <summary>
    /// 
    /// </summary>
    public partial class WIPMachineMaster : System.Web.UI.Page
    {
        BL_MachineMaster blobj = new BL_MachineMaster();
        string Message = "";
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
                Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
                Response.AddHeader("Pragma", "no-cache");
                Response.AddHeader("Expires", "0");
                if (Session["usertype"].ToString() != "ADMIN")
                {
                    string _strRights = CommonHelper.GetRights("MACHINE MASTER", (DataTable)Session["USER_RIGHTS"]);
                    CommonHelper._strRights = _strRights.Split('^');
                    if (CommonHelper._strRights[0] == "0")  //Check view rights
                    {
                        Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                    }
                }
                if (!IsPostBack)
                {
                    ShowGridData();
                    bindLine();
                    drpLineID.Focus();
                    dvPrintergrup.Visible = true;
                    if (PCommon.sUseNetworkPrinter == "1")
                    {
                        bindPRINTER();
                    }
                    else
                    {
                        dvPrintergrup.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
        }
        public void bindPRINTER()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                BL_Common objBlCommon = new BL_Common();
                DataTable dt = objBlCommon.BINDPRINTER("WIP");
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(dddlPrinter, dt, true);
                    dddlPrinter.SelectedIndex = 0;
                    dddlPrinter.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        private void bindLine()
        {
            try
            {
                blobj = new BL_MachineMaster();
                DataTable dt = blobj.GetLineID(Session["SiteCode"].ToString());
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(drpLineID, dt, true);
                    drpLineID.SelectedIndex = 0;
                    drpLineID.Focus();
                }
                else
                {
                    drpLineID.DataSource = null;
                    drpLineID.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }

        }
        private void ShowGridData()
        {
            try
            {
                blobj = new BL_MachineMaster();
                DataTable dt = blobj.GetMachineRecord(Session["SiteCode"].ToString());
                if (dt.Rows.Count > 0)
                {
                    gvMachinemst.DataSource = dt;
                    gvMachinemst.DataBind();
                    lblNumberofRecords.Text = dt.Rows.Count.ToString();

                    dt.TableName = "Table1";
                    dt.AcceptChanges();
                    System.Data.DataView view = new System.Data.DataView(dt);
                    System.Data.DataTable selected =
                            view.ToTable("Table1", false, "MACHINEID", "MACHINENAME");
                    clsCommon.FillComboBox(drpMachineFilter, selected, true);
                    ViewState["Data"] = dt;
                }
                else
                {
                    gvMachinemst.DataSource = null;
                    gvMachinemst.DataBind();
                    lblNumberofRecords.Text = "0";
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        public void save()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (drpLineID.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select Line ID.", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpLineID.Focus();
                    return;
                }
                if (txtMachineID.Text.Trim() == string.Empty)
                {
                    CommonHelper.ShowMessage("Please enter machine ID", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtMachineID.Focus();
                    return;
                }
                if (txtMachinename.Text.Trim() == string.Empty)
                {
                    CommonHelper.ShowMessage("Please enter machine name.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtMachinename.Focus();
                    return;
                }
                if (txtMachineDesc.Text.Trim() == string.Empty)
                {
                    CommonHelper.ShowMessage("Please enter machine desc.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtMachineDesc.Focus();
                    return;
                }
                if (ddltype.SelectedIndex == 0 || ddltype.Text == "")
                {
                    CommonHelper.ShowMessage("Please select type", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddltype.Focus();
                    return;
                }
                else
                {
                    if (ddltype.Text == "Solder Storage" && drpMachineSequence.SelectedIndex == 0)
                    {
                        CommonHelper.ShowMessage("Please select sequence", msgerror, CommonHelper.MessageType.Error.ToString());
                        drpMachineSequence.Focus();
                        return;
                    }
                    int iMachineSeq = 0;
                    if (ddltype.Text == "Solder Storage")
                    {
                        iMachineSeq = Convert.ToInt32(drpMachineSequence.Text);
                    }
                    else
                    {
                        iMachineSeq = 0;
                    }
                    if (btnSave.Text == "Save")
                    {
                        blobj = new BL_MachineMaster();
                        string sResult = blobj.SaveMachineMstData(txtMachineID.Text.Trim(),
                            txtMachinename.Text.Trim(), txtMachineDesc.Text.Trim(), drpLineID.SelectedItem.Value,
                            Session["UserID"].ToString(), ddltype.SelectedItem.Text, iMachineSeq,
                            drpMachineFileValidation.SelectedItem.Value, Session["SiteCode"].ToString()
                            );
                        if (sResult.ToUpper().StartsWith("SUCCESS~"))
                        {
                            CommonHelper.ShowMessage(sResult.Split('~')[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                            txtMachinename.Text = "";
                            txtMachineDesc.Text = "";
                            txtMachineID.Text = "";
                            drpLineID.SelectedIndex = 0;
                            drpLineID.Focus();
                        }
                        else if (sResult.ToUpper().StartsWith("N~"))
                        {
                            CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                            txtMachineID.Text = "";
                            txtMachinename.Text = "";
                            txtMachineDesc.Text = "";
                            drpLineID.SelectedIndex = 0;
                            drpLineID.Focus();
                        }
                        else if (sResult.ToUpper().StartsWith("SQ~"))
                        {
                            CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                            drpMachineSequence.Focus();
                        }
                    }
                    else
                    {
                        blobj = new BL_MachineMaster();
                        string sResult = blobj.UpdateMachineRecords(txtMachineID.Text.Trim(),
                            txtMachinename.Text.Trim(), txtMachineDesc.Text.Trim(), drpLineID.SelectedItem.Value,
                            Session["UserID"].ToString(), ddltype.SelectedItem.Text, iMachineSeq,
                            drpMachineFileValidation.SelectedItem.Value, Session["SiteCode"].ToString()
                            );
                        if (sResult.ToUpper().StartsWith("SUCCESS~"))
                        {
                            CommonHelper.ShowMessage(sResult.Split('~')[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                            txtMachinename.Text = "";
                            txtMachineDesc.Text = "";
                            txtMachineID.Text = "";
                            drpLineID.SelectedIndex = 0;
                            drpLineID.Focus();
                        }
                        else
                        {
                            CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                            txtMachineID.Text = "";
                            txtMachinename.Text = "";
                            txtMachineDesc.Text = "";
                            drpLineID.SelectedIndex = 0;
                            drpLineID.Focus();
                        }
                        txtMachineID.ReadOnly = false;
                        btnSave.Text = "Save";
                    }
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

            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtMachineID.Text = "";
            txtMachinename.Text = "";
            txtMachineDesc.Text = "";
            drpLineID.SelectedIndex = 0;
            drpLineID.Focus();
            btnSave.Text = "Save";
            btnPrint.Enabled = false;
            btnPrint.CssClass = "btn btn-primary btn-block btn-flat";
            txtMachineID.ReadOnly = false;
            ddltype.SelectedIndex = 0;
            drpMachineSequence.Enabled = true;
            drpMachineSequence.SelectedIndex = 0;
            drpMachineFileValidation.SelectedIndex = 0;
            if (drpMachineFilter.Items.Count > 0)
            {
                gvMachinemst.DataSource = null;
                gvMachinemst.DataBind();
                drpMachineFilter.SelectedIndex = 0;
                DataTable dt = (DataTable)ViewState["Data"];
                gvMachinemst.DataSource = dt;
                gvMachinemst.DataBind();
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            save();
        }

        protected void txtMachineID_TextChanged(object sender, EventArgs e)
        {
            txtMachinename.Focus();
        }

        protected void txtMachineName_TextChanged(object sender, EventArgs e)
        {
            txtMachineDesc.Focus();
        }

        protected void txtMachineDesc_TextChanged(object sender, EventArgs e)
        {
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvMachinemst.PageIndex = e.NewPageIndex;
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
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void EditRecords(string _SN)
        {
            try
            {
                blobj = new BL_MachineMaster();
                DataTable dt = blobj.GetSeletedData(_SN, Session["SiteCode"].ToString());
                if (dt.Rows.Count > 0)
                {
                    txtMachineID.ReadOnly = true;
                    string sMachineType = string.Empty;
                    foreach (DataRow dr in dt.Rows)
                    {
                        txtMachineID.Text = dr.ItemArray[0].ToString();
                        txtMachinename.Text = dr.ItemArray[1].ToString();
                        txtMachineDesc.Text = dr.ItemArray[2].ToString();
                        sMachineType = dr.ItemArray[3].ToString();
                        if (sMachineType != "SMT")
                        {
                            sMachineType = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(sMachineType.ToLower());
                        }          
                        ddltype.SelectedValue = sMachineType;
                        if (ddltype.SelectedValue == "Solder Storage")
                        {
                            drpMachineSequence.Enabled = false;
                            drpMachineSequence.CssClass = "form-control select2";
                            drpMachineSequence.SelectedValue = dr.ItemArray[4].ToString();
                        }
                        else
                        {
                            drpMachineSequence.SelectedValue = "0";
                            drpMachineSequence.Enabled = false;
                            drpMachineSequence.CssClass = "form-control select2";
                        }
                        drpMachineFileValidation.SelectedValue = dr.ItemArray[5].ToString();
                        drpLineID.Text = dr.ItemArray[6].ToString();
                    }
                    btnPrint.Enabled = true;
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
                blobj = new BL_MachineMaster();
                string sResult = blobj.DeleteMachine(_SN, Session["SiteCode"].ToString());
                if (sResult.StartsWith("SUCCESS~"))
                {
                    CommonHelper.ShowMessage(sResult.Split('~')[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                    txtMachinename.Text = "";
                    txtMachineDesc.Text = "";
                    txtMachineID.Text = "";
                    drpLineID.SelectedIndex = 0;
                    drpLineID.Focus();
                }
                else
                {
                    if (sResult.Contains("The DELETE statement conflicted with the REFERENCE"))
                    {
                        CommonHelper.ShowMessage("Machine already used in transaction, It can not delete", msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    else
                    {
                        CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    txtMachineID.Text = "";
                    txtMachinename.Text = "";
                    txtMachineDesc.Text = "";
                    drpLineID.SelectedIndex = 0;
                    drpLineID.Focus();
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

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (dddlPrinter.SelectedIndex == 0 && dvPrintergrup.Visible == true)
                {
                    CommonHelper.ShowMessage("Please select printer.", msgerror, CommonHelper.MessageType.Error.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    return;
                }
                string sLineCode = Session["LINECODE"].ToString();
                string sUserID = Session["UserID"].ToString();
                blobj = new BL_MachineMaster();
                string sResult = string.Empty;
                foreach (GridViewRow item in gvMachinemst.Rows)
                {
                    if (item.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chk = (item.FindControl("chkSelect") as CheckBox);
                        if (chk.Checked)
                        {
                            string lblMachineID = gvMachinemst.Rows[item.RowIndex].Cells[2].Text;
                            string lbldescription = gvMachinemst.Rows[item.RowIndex].Cells[4].Text;
                            string lblMachineName = gvMachinemst.Rows[item.RowIndex].Cells[3].Text;
                            sResult = blobj.MachinePrinting(lblMachineID, lblMachineName, lbldescription, dddlPrinter.Text
                                , sUserID, sLineCode
                                );
                        }
                    }
                }
                if (sResult.StartsWith("SUCCESS~"))
                {
                    Message = sResult.Split('~')[1];
                    CommonHelper.ShowMessage("Machine label printed successfully", msgsuccess, CommonHelper.MessageType.Success.ToString());
                    btnPrint.Enabled = true;
                }
                else if (sResult.Contains("N~"))
                {
                    Message = sResult.Split('~')[1];
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    return;
                }
                else if (sResult.Contains("ERROR~"))
                {
                    Message = sResult.Split('~')[1];
                    CommonHelper.ShowMessage(Message, msgsuccess, CommonHelper.MessageType.Success.ToString());
                    return;
                }
                else if (sResult.StartsWith("PRINTERNOTCONNECTED~"))
                {
                    Message = sResult.Split('~')[1];
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                }
                else if (sResult.StartsWith("PRNNOTFOUND~"))
                {
                    Message = sResult.Split('~')[1];
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                }
                btnReset_Click(null, null);
                btnPrint.Enabled = true;
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                drpMachineSequence.SelectedIndex = 0;
                if (ddltype.Text == "Solder Storage")
                {
                    drpMachineSequence.CssClass = "form-control select2";
                    drpMachineSequence.Enabled = true;
                }
                else
                {
                    drpMachineSequence.CssClass = "form-control select2";
                    drpMachineSequence.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void drpMachineFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpMachineFilter.SelectedIndex > 0)
                {
                    DataTable dt = (DataTable)ViewState["Data"];
                    DataView dataView = dt.DefaultView;
                    dataView.RowFilter = "MACHINEID = '" + drpMachineFilter.SelectedValue + "'";
                    gvMachinemst.DataSource = dataView;
                    gvMachinemst.DataBind();
                }
                else
                {
                    DataTable dt = (DataTable)ViewState["Data"];
                    gvMachinemst.DataSource = dt;
                    gvMachinemst.DataBind();
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
                    if (dt.Columns.Count != 8)
                    {
                        CommonHelper.ShowMessage("Invalid file,No of columns should be 8", msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                    BL_MachineMaster blobj = new BL_MachineMaster();
                    string Message = string.Empty;
                    DataTable dtResult = blobj.UploadMachine(dt, Session["UserID"].ToString());
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
                    else if (sResult.StartsWith("SQ~"))
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