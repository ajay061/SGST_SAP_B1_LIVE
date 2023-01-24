using Common;
using System;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using BusinessLayer.Masters;
using System.Web.UI;
using System.IO;
using System.Configuration;

namespace SGST.INE.Masters
{
    public partial class Binmaster : System.Web.UI.Page
    {
        string Message = string.Empty;
        BL_BinMaster blobj = new BL_BinMaster();
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Signin/v1/Login.aspx?Session=Null");
            }
        }
        private void bindPartCodes()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                BL_LocationMaster blobj = new BL_LocationMaster();
                DataTable dt = blobj.GetPartCode("WIP", Session["SiteCode"].ToString());
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(drpPartCode, dt, true);
                    drpPartCode.SelectedIndex = 0;
                    drpPartCode.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        public void bindPRINTER()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                string sPrinter = string.Empty;
                BL_Common blobj = new BL_Common();
                DataTable dt = blobj.BINDPRINTER("WIP");
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
        private void ShowGridData()
        {
            try
            {
                blobj = new BL_BinMaster();
                DataTable dt = blobj.GetBins(Session["SiteCode"].ToString());
                lblNumberofRecords.Text = dt.Rows.Count.ToString();
                if (dt.Rows.Count > 0)
                {
                    gvBinMaster.DataSource = dt;
                    gvBinMaster.DataBind();
                }
                else
                {
                    gvBinMaster.DataSource = dt;
                    gvBinMaster.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        private void _ResetField()
        {
            txtBinID.Focus();
            txtBinID.Text = "";
            txtBinDesc.Text = string.Empty;
            txtStorageArea.Text = string.Empty;
            txtCapacity.Text = string.Empty;
            btnSave.Text = "Save";
            txtBinID.ReadOnly = false;
            if (drpPartCode.Items.Count > 0)
            {
                drpPartCode.SelectedIndex = 0;
            }
            if (ddlPrinter.Items.Count > 0)
            {
                ddlPrinter.SelectedIndex = 0;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (Session["usertype"].ToString() != "ADMIN")
                {
                    string _strRights = CommonHelper.GetRights("BIN MASTER", (DataTable)Session["USER_RIGHTS"]);
                    CommonHelper._strRights = _strRights.Split('^');
                    if (CommonHelper._strRights[0] == "0")  //Check view rights
                    {
                        Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                    }
                }
                if (!IsPostBack)
                {
                    bindPartCodes();
                    ShowGridData();
                    txtBinID.Focus();
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
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (btnSave.Text == "Save")
                {
                    if (txtBinID.Text.Trim() == "")
                    {
                        CommonHelper.ShowMessage("Please enter bin ID", msgerror, CommonHelper.MessageType.Error.ToString());
                        txtBinID.Focus();
                        return;
                    }
                    if (txtBinDesc.Text.Trim() == "")
                    {
                        CommonHelper.ShowMessage("Please enter bin desc", msgerror, CommonHelper.MessageType.Error.ToString());
                        txtBinDesc.Focus();
                        return;
                    }
                    string sPartCode = string.Empty;
                    if (drpPartCode.SelectedIndex > 0)
                    {
                        sPartCode = drpPartCode.Text;
                    }
                    blobj = new BL_BinMaster();
                    string sResult = blobj.SaveBin(txtBinID.Text.Trim(), txtBinDesc.Text.Trim(), txtStorageArea.Text.Trim()
                        , "0", sPartCode, Session["UserID"].ToString(), Session["SiteCode"].ToString()
                        );
                    if (sResult.Length > 0)
                    {
                        if (sResult.StartsWith("SUCCESS~"))
                        {
                            _ResetField();
                            CommonHelper.ShowMessage("Bin details has been saved", msgsuccess, CommonHelper.MessageType.Success.ToString());
                            ShowGridData();
                        }
                        else
                        {
                            if (sResult.Contains("Violation of PRIMARY KEY"))
                            {
                                txtBinID.Text = string.Empty;
                                txtBinID.Focus();
                                CommonHelper.ShowMessage("Bin id already exist, Please enter another one", msgerror, CommonHelper.MessageType.Error.ToString());
                                ShowGridData();
                            }
                            else
                            {
                                _ResetField();
                                CommonHelper.ShowMessage("Data not saved, Please try again", msgerror, CommonHelper.MessageType.Error.ToString());
                                ShowGridData();
                            }
                        }
                    }
                }
                else
                {
                    string sPartCode = string.Empty;
                    if (drpPartCode.SelectedIndex > 0)
                    {
                        sPartCode = drpPartCode.Text;
                    }
                    blobj = new BL_BinMaster();
                    string sResult = blobj.UpdateBin(txtBinDesc.Text.Trim(), txtStorageArea.Text.Trim()
                        , "0", txtBinID.Text.Trim(), sPartCode, Session["UserID"].ToString(), Session["SiteCode"].ToString()
                        );
                    if (sResult.Length > 0)
                    {
                        if (sResult.StartsWith("SUCCESS~"))
                        {
                            _ResetField();
                            CommonHelper.ShowMessage("Bin details has been updated", msgsuccess, CommonHelper.MessageType.Success.ToString());
                            ShowGridData();
                        }
                        else
                        {
                            _ResetField();
                            CommonHelper.ShowMessage("Data not saved, Please try again", msgerror, CommonHelper.MessageType.Error.ToString());
                            ShowGridData();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                if (ex.Message.Contains("Violation of PRIMARY KEY"))
                {
                    CommonHelper.ShowCustomErrorMessage("Bin Id already exist, Please enter another one", msgerror);
                    txtBinID.Text = string.Empty;
                    txtBinID.Focus();
                }
                else
                {
                    CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                }

            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
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

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvBinMaster.PageIndex = e.NewPageIndex;
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
                blobj = new BL_BinMaster();
                DataTable dt = blobj.GetSeletedData(_SN);
                if (dt.Rows.Count > 0)
                {
                    txtBinID.ReadOnly = true;
                    txtBinID.Text = dt.Rows[0][0].ToString();
                    txtBinDesc.Text = dt.Rows[0][1].ToString();
                    txtStorageArea.Text = dt.Rows[0][2].ToString();
                    txtCapacity.Text = dt.Rows[0][3].ToString();
                    if (dt.Rows[0]["PART_CODE"].ToString() != "")
                    {
                        drpPartCode.Text = dt.Rows[0]["PART_CODE"].ToString();
                    }
                    hidUpdate.Value = "Update";
                    hidUID.Value = _SN;
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
                blobj = new BL_BinMaster();
                string sResult = blobj.DeleteBin(_SN, Session["UserID"].ToString(), Session["SiteCode"].ToString());
                if (sResult.StartsWith("SUCCESS~"))
                {
                    CommonHelper.ShowMessage("Bin details deleted successfully", msgsuccess, CommonHelper.MessageType.Success.ToString());
                    _ResetField();
                    return;
                }
                else
                {
                    CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    return;
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
                if (ddlPrinter.SelectedIndex == 0 && dvPrintergrup.Visible == true)
                {
                    CommonHelper.ShowMessage("Please select printer.", msgerror, CommonHelper.MessageType.Error.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    return;
                }
                string sLineCode = Session["LINECODE"].ToString();
                string sUserID = Session["UserID"].ToString();
                BL_BinMaster blobj = new BL_BinMaster();
                string sResult = string.Empty;
                foreach (GridViewRow item in gvBinMaster.Rows)
                {
                    if (item.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chk = (item.FindControl("chkSelect") as CheckBox);
                        if (chk.Checked)
                        {
                            string lblBinID = gvBinMaster.Rows[item.RowIndex].Cells[1].Text;
                            string lbldescription = gvBinMaster.Rows[item.RowIndex].Cells[2].Text;
                            string lblPartCode = gvBinMaster.Rows[item.RowIndex].Cells[5].Text;
                            sResult = blobj.BinPrinting(lblBinID, lbldescription, ddlPrinter.Text, lblPartCode
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

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                DataTable dt = new DataTable();
                string sProgramID = string.Empty;
                if (System.IO.Path.GetExtension(FileUpload1.FileName).Equals(".csv"))
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
                    if (dt.Columns.Count != 4)
                    {
                        CommonHelper.ShowMessage("Invalid file,No of columns should be 4", msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                    if (dt.Columns[0].ToString() != "SITECODE")
                    {
                        CommonHelper.ShowMessage("Invalid file", msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                    else if (dt.Columns[1].ToString() != "BINID")
                    {
                        CommonHelper.ShowMessage("Invalid file,Column name not matched", msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                    else if (dt.Columns[2].ToString() != "BINDESC")
                    {
                        CommonHelper.ShowMessage("Invalid file,Column name not matched", msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                    else if (dt.Columns[3].ToString() != "PARTCODE")
                    {
                        CommonHelper.ShowMessage("Invalid file,Column name not matched", msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                    BL_BinMaster blobj = new BL_BinMaster();
                    string Message = string.Empty;
                    string sResult = blobj.UploadBin(dt, Session["UserID"].ToString());
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
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                if (ex.Message.ToUpper().Contains("PRIMARY"))
                {
                    CommonHelper.ShowMessage("File contains duplicate bin, Please check file before uploading", msgerror, CommonHelper.MessageType.Error.ToString());
                    return;
                }
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);

            }
        }
    }
}