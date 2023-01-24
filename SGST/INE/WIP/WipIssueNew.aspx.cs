using Common;
using System;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer.WIP;
using System.Data;
using BusinessLayer;

namespace SGST.INE.WIP
{
    public partial class WipIssueNew : System.Web.UI.Page
    {
       BL_WIP_Issue blobj = new BL_WIP_Issue();
        string Message = "";
        static string sFIFORequired = ConfigurationManager.AppSettings["_FIFOREQUIRED"].ToString();

        #region Method Declaration

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Signin/v1/Login.aspx?Session=Null");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usertype"].ToString() != "ADMIN")
            {
                string _strRights = CommonHelper.GetRights("WIP ISSUE", (DataTable)Session["USER_RIGHTS"]);
                CommonHelper._strRights = _strRights.Split('^');
                if (CommonHelper._strRights[0] == "0")  //Check view rights
                {
                    Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                }
            }
            if (!IsPostBack)
            {
                try
                {
                    txtBin.ReadOnly = true;
                    txtFeederID.ReadOnly = true;
                    txtToolID.ReadOnly = true;
                    txtFeederID.ReadOnly = true;
                    txtFeederLocation.ReadOnly = true;
                    BindLineID();
                }
                catch (Exception ex)
                {
                    CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                }
            }
        }
        private void BindLineID()
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                drpLineID.Items.Clear();
                txtScanMachineID.Text = string.Empty;
                drpFGItemCode.Items.Clear();
                blobj = new BL_WIP_Issue();
                DataTable dt = blobj.BINDLineID(Session["SiteCode"].ToString());
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(drpLineID, dt, true);
                    drpLineID.SelectedIndex = 0;
                    drpLineID.Focus();
                }
                else
                {
                    txtScanMachineID.Text = string.Empty;
                    drpFGItemCode.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void BindFGItem()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtScanMachineID.Text))
                {
                    CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                    drpFGItemCode.Items.Clear();
                    blobj = new BL_WIP_Issue();
                    DataTable dt = blobj.BINDFGItemCode(drpLineID.Text, txtScanMachineID.Text, Session["SiteCode"].ToString());
                    if (dt.Rows.Count > 0)
                    {
                        clsCommon.FillComboBox(drpFGItemCode, dt, true);
                        drpFGItemCode.SelectedIndex = 0;
                        drpFGItemCode.Focus();
                    }
                    else
                    {
                        drpFGItemCode.Items.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void BindProgramID()
        {
            try
            {
                if (drpFGItemCode.SelectedIndex > 0)
                {
                    CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                    blobj = new BL_WIP_Issue();
                    DataTable dt = blobj.BindProfileID(drpLineID.Text, txtScanMachineID.Text, drpFGItemCode.Text);
                    if (dt.Rows.Count > 0)
                    {
                        clsCommon.FillComboBox(drpProgramID, dt, true);
                        drpProgramID.SelectedIndex = 0;
                        drpProgramID.Focus();
                    }
                    else
                    {
                        drpProgramID.Items.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void CheckBarcode()
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                string sFeederNo = string.Empty;
                string sItemChecked = string.Empty;
                if (drpLineID.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select Line ID", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpLineID.Focus();
                    txtRmBarcode.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    return;
                }
                if (drpFGItemCode.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select FG Item Code ", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpFGItemCode.Focus(); txtRmBarcode.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    return;
                }
                if (string.IsNullOrEmpty(txtScanMachineID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Scan Machine", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtScanMachineID.Focus(); txtRmBarcode.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    return;
                }
                if (drpProgramID.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Program ID not found, Please add program in routing ", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpProgramID.Focus();
                    txtRmBarcode.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    return;
                }
                if (string.IsNullOrEmpty(txtRmBarcode.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan part barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtRmBarcode.Focus();
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    return;
                }
                if (rdTool.Checked == false && rdBIN.Checked == false && rdFeeder.Checked == false)
                {
                    foreach (GridViewRow row in gvProfileMaster.Rows)
                    {
                        if ((row.Cells[5].Text.Trim().ToString().Trim().Length > 0 && row.Cells[5].Text.Trim() != "&nbsp;")
                            && rdFeeder.Checked == false
                            )
                        {
                            CommonHelper.ShowMessage("Please select feeder type", msgerror, CommonHelper.MessageType.Error.ToString());
                            rdFeeder.Focus();
                            txtRmBarcode.Text = string.Empty;
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                            return;
                        }
                        if ((row.Cells[7].Text.Trim().ToString().Trim().Length > 0 && row.Cells[7].Text.Trim() != "&nbsp;")
                           && rdTool.Checked == false
                           )
                        {
                            CommonHelper.ShowMessage("Please select tool type", msgerror, CommonHelper.MessageType.Error.ToString());
                            rdFeeder.Focus();
                            txtRmBarcode.Text = string.Empty;
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                            return;
                        }
                    }
                }
                if (rdTool.Checked == true || rdBIN.Checked == true)
                {
                    foreach (GridViewRow row in gvProfileMaster.Rows)
                    {
                        if ((row.Cells[5].Text.Trim().ToString().Trim().Length > 0 && row.Cells[5].Text.Trim() != "&nbsp;")
                            && rdFeeder.Checked == false
                            )
                        {
                            CommonHelper.ShowMessage("Please select feeder type", msgerror, CommonHelper.MessageType.Error.ToString());
                            rdFeeder.Focus();
                            txtRmBarcode.Text = string.Empty;
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                            return;
                        }
                        break;
                    }
                }
                if (rdBIN.Checked == true)
                {
                    if (string.IsNullOrEmpty(txtBin.Text.Trim()))
                    {
                        CommonHelper.ShowMessage("Please scan Bin Barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                        txtBin.Focus();
                        txtRmBarcode.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                        return;
                    }
                }
                if (rdFeeder.Checked == true)
                {
                    if (string.IsNullOrEmpty(txtFeederLocation.Text.Trim()))
                    {
                        CommonHelper.ShowMessage("Please scan feeder location", msgerror, CommonHelper.MessageType.Error.ToString());
                        txtFeederLocation.Focus();
                        txtRmBarcode.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                        return;
                    }
                }
                blobj = new BL_WIP_Issue();
                DataTable dt = blobj.ValidateBarcode(txtRmBarcode.Text, drpFGItemCode.Text,
                    txtScanMachineID.Text.Trim(), drpProgramID.SelectedValue.Trim());
                if (dt.Rows.Count > 0)
                {
                    if (dt.Columns.Count == 1)
                    {
                        string sData = dt.Rows[0][0].ToString();
                        CommonHelper.ShowMessage(sData.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        txtRmBarcode.Text = "";
                        txtRmBarcode.Focus();
                        return;
                    }
                    string sBatchNo = dt.Rows[0]["BATCHNO"].ToString();
                    string Part_Code = dt.Rows[0]["Part_Code"].ToString();
                    string QTY = dt.Rows[0]["QTY"].ToString();
                    decimal dAQty = Convert.ToDecimal(dt.Rows[0]["CONSUMED_QTY"].ToString());
                    string sPartType = dt.Rows[0]["PART_TYPE"].ToString();
                    DateTime dtInsertedOn = Convert.ToDateTime(dt.Rows[0]["INSERTED_ON"].ToString());
                    DateTime dtCurrentDate = Convert.ToDateTime(dt.Rows[0]["CURRENT_DATE"].ToString());
                    decimal dShelfLife = Convert.ToDecimal(dt.Rows[0]["SHELF_LIFE"].ToString());
                    string sStatus = dt.Rows[0]["STATUS"].ToString();
                    if (sStatus == "8")
                    {
                        CommonHelper.ShowMessage("Scanned barcode already expired, It can not be issued.", msgerror, CommonHelper.MessageType.Error.ToString());
                        txtRmBarcode.Text = "";
                        txtRmBarcode.Focus();
                        return;
                    }
                    if (sFIFORequired == "1")
                    {
                        DataTable dtResult = new DataTable();
                        dtResult = blobj.GetPendingBarcode(hidPartCode.Value, sPartType, dShelfLife,
                            txtRmBarcode.Text.Trim());
                        if (dtResult.Rows.Count > 0)
                        {
                            string sSolderBarcode = dtResult.Rows[0]["RESULT"].ToString();
                            if (sSolderBarcode.StartsWith("N~"))
                            {
                                CommonHelper.ShowMessage(sSolderBarcode, msgerror, CommonHelper.MessageType.Error.ToString());
                                txtRmBarcode.Text = "";
                                txtRmBarcode.Focus();
                                ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                                return;
                            }
                        }
                    }
                    DataTable dtValidatePCBParcodeBarcpde = blobj.ValidatePCBbarcodepartcode(txtRmBarcode.Text,
                        drpFGItemCode.Text, Part_Code, txtFeederLocation.Text
                           , txtScanMachineID.Text.Trim(), drpProgramID.Text
                           );
                    if (dtValidatePCBParcodeBarcpde.Rows.Count == 0)
                    {
                        CommonHelper.ShowMessage("Scanned partcode barcode not matched with selected partcode against scanned machine ", msgerror, CommonHelper.MessageType.Error.ToString());
                        txtRmBarcode.Text = "";
                        txtRmBarcode.Focus();
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                        return;
                    }
                    if (sPartType.ToUpper() == "SOLDER PASTE" || sPartType.ToUpper() == "SOLDER")
                    /////// || sPartType.ToUpper() == "CONSUMABLES")
                    {
                        sPartType = "SOLDER PASTE";
                    }
                    if (sPartType == "SOLDER PASTE")
                    {
                        dt = new DataTable();
                        int iWIPMaximumTime = Convert.ToInt32(ConfigurationManager.AppSettings["_WIPISSUETIME"].ToString()); // 4 hours time after passing mixing process
                        int iSolderPasteTotalTime = Convert.ToInt32(ConfigurationManager.AppSettings["_MAXISSUETIME"].ToString()); // solder paste total time after open
                        dt = blobj.ValidateSolderBarcode(txtRmBarcode.Text.Trim(), iSolderPasteTotalTime, iWIPMaximumTime);
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0][0].ToString().StartsWith("N~"))
                            {
                                CommonHelper.ShowMessage(dt.Rows[0][0].ToString().Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                                txtRmBarcode.Text = "";
                                txtRmBarcode.Focus();
                                ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                                return;
                            }
                        }
                        else
                        {
                            CommonHelper.ShowMessage("No result found, Please try again", msgerror, CommonHelper.MessageType.Error.ToString());
                            txtRmBarcode.Text = "";
                            txtRmBarcode.Focus();
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                            return;
                        }
                    }
                    else
                    {
                        if (dAQty == Convert.ToDecimal(QTY))
                        {
                            CommonHelper.ShowMessage("Scanned barcode has been fully consumed,It can not be used or Please scan different barcode, Comsumed Qty " + dAQty.ToString() + ", Qty :" + QTY.ToString(), msginfo, CommonHelper.MessageType.Info.ToString());
                            txtRmBarcode.Text = "";
                            txtRmBarcode.Focus();
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                            return;
                        }
                        //else if (sStatus == "2")
                        //{
                        //    CommonHelper.ShowMessage("Scanned barcode can not be issue,it is already allocated", msginfo, CommonHelper.MessageType.Info.ToString());
                        //    txtRmBarcode.Text = "";
                        //    txtRmBarcode.Focus();
                        //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                        //    return;
                        //}
                        else if (sStatus == "1" && dAQty == Convert.ToDecimal(QTY))
                        {
                            CommonHelper.ShowMessage("Scanned barcode can not be issue, Please scan their child barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                            txtRmBarcode.Text = "";
                            txtRmBarcode.Focus();
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                            return;
                        }
                        else if (sPartType == "PCB")
                        {
                            CommonHelper.ShowMessage("Scanned barcode can not be issue, it is pcb barcode", msginfo, CommonHelper.MessageType.Info.ToString());
                            txtRmBarcode.Text = "";
                            txtRmBarcode.Focus();
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                            return;
                        }
                    }
                    if (txtFeederLocation.Text.Trim().Length > 0)
                    {
                        if (sStatus == "0")
                        {
                            CommonHelper.ShowMessage("Putaway not done for scanned barcode, Please putaway first. ", msgerror, CommonHelper.MessageType.Error.ToString());
                            txtRmBarcode.Text = "";
                            txtRmBarcode.Focus();
                            return;
                        }
                        DataTable dtValidateFeeder = blobj.ValidateFeederBarcode(txtRmBarcode.Text, drpFGItemCode.Text, Part_Code,
                            txtFeederLocation.Text
                            , txtScanMachineID.Text.Trim(), drpProgramID.Text
                            );
                        if (dtValidateFeeder.Rows.Count == 0)
                        {
                            CommonHelper.ShowMessage("Scanned parcode barcode not matched with selected feeder location. ", msgerror, CommonHelper.MessageType.Error.ToString());
                            txtRmBarcode.Text = "";
                            txtFeederLocation.Text = "";
                            txtFeederLocation.ReadOnly = false;
                            txtFeederLocation.Focus();
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                            return;
                        }
                        sFeederNo = dtValidateFeeder.Rows[0]["FEEDER_NO"].ToString();
                    }
                    if (rdBIN.Checked == true)
                    {
                        sItemChecked = "BIN";
                    }
                    else if (rdTool.Checked == true)
                    {
                        sItemChecked = "TOOL";
                    }
                    else if (rdFeeder.Checked == true)
                    {
                        sItemChecked = "FEEDER";
                    }
                    string sRemainingQty = dtValidatePCBParcodeBarcpde.Rows[0][1].ToString();
                    PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtMessage,
                        System.Reflection.MethodBase.GetCurrentMethod().Name,
                      "Save Barcode-USP_WIP_ISSUE, Machine ID : " + txtScanMachineID.Text.Trim()
                      + ", Program ID : " + drpProgramID.Text
                      + ", Barcode :" + txtRmBarcode.Text.Trim()
                      + ", FG Item Code :" + drpFGItemCode.Text.Trim()
                      + ", Checked Item Group : " + sItemChecked
                      + ", Feeder Location :" + txtFeederLocation.Text.Trim()
                      +", Scanned By : " + Session["UserID"].ToString()
                      );

                    blobj = new BL_WIP_Issue();
                    string sResult = blobj.SaveData(hidmpid.Value, txtScanMachineID.Text.Trim(), txtRmBarcode.Text.Trim()
                        , sBatchNo, txtBin.Text.Trim(), sRemainingQty, Session["UserID"].ToString(), Part_Code, sPartType
                        , txtFeederID.Text, txtFeederLocation.Text, sFeederNo, txtToolID.Text, drpProgramID.SelectedValue.ToString()
                       , Session["SiteCode"].ToString(), Session["LINECODE"].ToString()
                       , drpFGItemCode.Text
                       );
                    if (sResult.StartsWith("SUCCESS~"))
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                        CommonHelper.ShowMessage("Scanned barcode Issued Sucessfully : " + txtRmBarcode.Text.Trim() + ", Issued Qty :  " + sRemainingQty, msgsuccess, CommonHelper.MessageType.Success.ToString());
                        if (rdTool.Checked == true)
                        {
                            foreach (GridViewRow row in gvProfileMaster.Rows)
                            {
                                if (row.Cells[4].Text.ToString().ToUpper() == Part_Code.ToUpper())
                                {
                                    row.BackColor = System.Drawing.Color.Green;
                                    row.ForeColor = System.Drawing.Color.White;
                                    hidPartCode.Value = row.Cells[4].Text;
                                    row.Cells[8].Text = txtRmBarcode.Text;
                                    if (sStatus == "1")
                                    {
                                        string dWorkOrderQty = sResult.Split('~')[2];
                                        row.Cells[10].Text = dWorkOrderQty;
                                    }
                                }
                            }
                            txtToolID.Focus();
                            txtRmBarcode.Text = string.Empty;
                            txtToolID.Text = string.Empty;
                        }
                        else if (rdFeeder.Checked == true)
                        {
                            foreach (GridViewRow row in gvProfileMaster.Rows)
                            {
                                if (row.Cells[6].Text.ToString().ToUpper() == txtFeederLocation.Text.ToUpper()
                                    && row.Cells[4].Text.ToString().ToUpper() == Part_Code.ToUpper()
                                    )
                                {
                                    row.BackColor = System.Drawing.Color.Green;
                                    row.ForeColor = System.Drawing.Color.White;
                                    hidPartCode.Value = row.Cells[4].Text;
                                    row.Cells[8].Text = txtRmBarcode.Text;
                                    if (sStatus == "1")
                                    {
                                        string dWorkOrderQty = sResult.Split('~')[2];
                                        row.Cells[10].Text = dWorkOrderQty;
                                    }
                                    break;
                                }
                            }
                        }
                        else if (rdBIN.Checked == true)
                        {
                            foreach (GridViewRow row in gvProfileMaster.Rows)
                            {
                                if (row.Cells[4].Text.ToString().ToUpper() == Part_Code.ToUpper())
                                {
                                    row.BackColor = System.Drawing.Color.Green;
                                    row.ForeColor = System.Drawing.Color.White;
                                    hidPartCode.Value = row.Cells[4].Text;
                                    row.Cells[8].Text = txtRmBarcode.Text;
                                    if (sStatus == "1")
                                    {
                                        string dWorkOrderQty = sResult.Split('~')[2];
                                        row.Cells[10].Text = dWorkOrderQty;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                        Message = sResult.Split('~')[1];
                        if (sResult.StartsWith("N~"))
                        {
                            CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                            txtRmBarcode.Text = "";
                            txtRmBarcode.Focus();
                        }
                        if (sResult.StartsWith("ERROR~"))
                        {
                            CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                            txtRmBarcode.Text = "";
                            txtRmBarcode.Focus();
                        }
                    }
                    txtAllocatedQty.Text = "0";
                    lblMatQty.Text = "0";
                    lblExcessQty.Text = "0";
                    Group1_CheckedChanged(null, null);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    CommonHelper.ShowMessage("Scanned barcode not found in wip area against selected data", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtRmBarcode.Text = string.Empty; txtRmBarcode.Focus();
                    txtAllocatedQty.Text = "0";
                    lblMatQty.Text = "0";
                    lblExcessQty.Text = "0";
                    return;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                txtRmBarcode.Text = string.Empty;
                txtRmBarcode.Focus();
            }
        }

        #endregion

        #region On TextChanged Event
        protected void txtScanMachineID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (drpLineID.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Select Line", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpLineID.Focus();
                    txtScanMachineID.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scrollTo(0, document.body.scrollHeight);", true);
                    //ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    return;
                }
                if (string.IsNullOrEmpty(txtScanMachineID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Scan Machine", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtScanMachineID.Focus();
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    return;
                }
                blobj = new BL_WIP_Issue();
                DataTable dt = blobj.ValidateMachine(txtScanMachineID.Text.Trim(), Session["SiteCode"].ToString());
                if (dt.Rows.Count > 0)
                {
                    CommonHelper.ShowMessage("Valid machine", msgsuccess, CommonHelper.MessageType.Success.ToString());
                    lblMachineName.Text = dt.Rows[0][0].ToString();
                    lblModelNo.Text = dt.Rows[0][2].ToString();
                    BindFGItem();
                }
                else
                {
                    CommonHelper.ShowMessage("Invalid machine", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtScanMachineID.Focus();
                    txtScanMachineID.Text = string.Empty;
                    lblMachineName.Text = "";
                    drpFGItemCode.Items.Clear();
                    drpProgramID.Items.Clear();
                    return;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void txtBin_TextChanged(object sender, EventArgs e)
        {
            try
            {
                HidLINEID.Value = drpLineID.Text;
                HIDMACHINEID.Value = txtScanMachineID.Text;
                hidFGItem.Value = drpFGItemCode.Text;
                hidProfileID.Value = drpProgramID.Text;
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (drpLineID.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Select line", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpLineID.Focus();
                    txtFeederID.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    return;
                }
                if (string.IsNullOrEmpty(txtScanMachineID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Scan Machine", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtScanMachineID.Focus();
                    txtFeederID.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    return;
                }
                if (drpProgramID.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Select Program ID", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpProgramID.Focus();
                    txtFeederID.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    return;
                }
                if (string.IsNullOrEmpty(txtBin.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan bin barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtRmBarcode.Focus();
                    return;
                }
                hidbin.Value = txtBin.Text;
                blobj = new BL_WIP_Issue();
                DataTable dt = blobj.GetBin(txtBin.Text);
                if (dt.Rows.Count > 0)
                {
                    CommonHelper.ShowMessage("Scanned bin is OK", msgsuccess, CommonHelper.MessageType.Success.ToString());
                    txtRmBarcode.Focus();
                }
                else
                {
                    CommonHelper.ShowMessage("Scanned bin not found in database", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpLineID.Text = HidLINEID.Value;
                    txtScanMachineID.Text = HIDMACHINEID.Value;
                    drpFGItemCode.Text = hidFGItem.Value;
                    drpProgramID.Text = hidProfileID.Value;
                    txtBin.Text = string.Empty;
                    txtBin.ReadOnly = false;
                    txtBin.Focus();
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    return;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                txtRmBarcode.Text = string.Empty;
                txtRmBarcode.Focus();
            }
        }
        protected void txtFeederLocation_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (drpLineID.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Select Line", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpLineID.Focus();
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    return;
                }
                if (string.IsNullOrEmpty(txtScanMachineID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Scan Machine", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtScanMachineID.Focus();
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    return;
                }
                if (drpProgramID.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Select Program ID", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpProgramID.Focus();
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    return;
                }
                if (string.IsNullOrEmpty(txtFeederLocation.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Scan feeder location", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtFeederLocation.Focus();
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    return;
                }
                blobj = new BL_WIP_Issue();
                DataTable dt = blobj.ValidateFeederLocation(txtFeederLocation.Text.Trim(), drpProgramID.Text, txtScanMachineID.Text.Trim());
                if (dt.Rows.Count > 0)
                {
                    CommonHelper.ShowMessage("Valid feeder location", msgsuccess, CommonHelper.MessageType.Success.ToString());
                    txtRmBarcode.ReadOnly = false;
                    txtRmBarcode.Focus();
                    txtFeederID.ReadOnly = false;
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                }
                else
                {
                    CommonHelper.ShowMessage("Invalid feeder location", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtFeederLocation.Focus();
                    txtFeederLocation.ReadOnly = false;
                    txtFeederLocation.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    return;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void txtFeederID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (drpLineID.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Select line", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpLineID.Focus();
                    txtFeederID.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    return;
                }
                if (string.IsNullOrEmpty(txtScanMachineID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Scan Machine", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtScanMachineID.Focus();
                    txtFeederID.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    return;
                }
                if (drpProgramID.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Select Program ID", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpProgramID.Focus();
                    txtFeederID.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    return;
                }
                if (string.IsNullOrEmpty(txtFeederLocation.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Enter feeder location", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtFeederLocation.Focus();
                    txtFeederID.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    return;
                }
                if (string.IsNullOrEmpty(txtFeederID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Enter feeder ID", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtFeederID.Focus();
                    txtFeederID.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    return;
                }
                blobj = new BL_WIP_Issue();
                DataTable dt = blobj.ValidateFeeder("", drpProgramID.Text,
                    txtScanMachineID.Text.Trim(), txtFeederID.Text.Trim(), txtFeederLocation.Text.Trim());
                if (dt.Rows.Count > 0)
                {
                    CommonHelper.ShowMessage("Valid feeder ID", msgsuccess, CommonHelper.MessageType.Success.ToString());
                    txtRmBarcode.Focus();
                    txtRmBarcode.ReadOnly = false;
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                }
                else
                {
                    CommonHelper.ShowMessage("Invalid feeder ID for selected data", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtFeederID.Focus();
                    txtFeederID.Text = string.Empty;
                    txtFeederID.ReadOnly = false;
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    return;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void txtToolID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (drpLineID.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Select Line", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpLineID.Focus();
                    txtFeederID.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    return;
                }
                if (string.IsNullOrEmpty(txtScanMachineID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Scan Machine", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtScanMachineID.Focus();
                    txtToolID.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    return;
                }
                if (drpProgramID.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Select Program ID", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpProgramID.Focus();
                    txtToolID.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    return;
                }
                if (string.IsNullOrEmpty(txtToolID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Scan Tool", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtToolID.Focus();
                    txtToolID.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    return;
                }
                if (gvProfileMaster.Rows.Count > 0)
                {
                    foreach (GridViewRow row in gvProfileMaster.Rows)
                    {
                        if (row.Cells[7].Text.ToString().ToUpper() == txtToolID.Text.ToUpper() && row.BackColor == System.Drawing.Color.Green
                            && row.Cells[9].BackColor == System.Drawing.Color.Red
                            )
                        {
                            CommonHelper.ShowMessage("Scanned tool already Issued", msginfo, CommonHelper.MessageType.Info.ToString());
                            txtToolID.Text = string.Empty;
                            txtToolID.Focus();
                            return;
                        }
                    }
                }
                blobj = new BL_WIP_Issue();
                DataTable dt = blobj.ValidateTool(txtToolID.Text.Trim(), drpProgramID.Text, txtScanMachineID.Text.Trim());
                if (dt.Rows.Count > 0)
                {
                    BL_Common objblcommon = new BL_Common();
                    string sResult1 = objblcommon.sShowNotificationTool(txtToolID.Text.Trim());
                    if (sResult1.Length > 0)
                    {
                        if (sResult1.StartsWith("N"))
                        {
                            CommonHelper.ShowMessage("You already consume more than 80%, Please be ready", msgerror, CommonHelper.MessageType.Notification.ToString());
                        }
                    }
                    string sResult = blobj.sSaveToolInformation(txtToolID.Text.Trim(), drpProgramID.SelectedValue.ToString(),
                        txtScanMachineID.Text.Trim(), Session["UserID"].ToString(), "", drpFGItemCode.Text
                        , Session["LINECODE"].ToString()
                        );
                    if (sResult.StartsWith("SUCCESS~"))
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                        CommonHelper.ShowMessage("Scanned tool Issued Successfully", msgsuccess, CommonHelper.MessageType.Success.ToString());
                        foreach (GridViewRow row in gvProfileMaster.Rows)
                        {
                            if (row.Cells[7].Text.ToString().ToUpper() == txtToolID.Text.ToUpper())
                            {
                                row.BackColor = System.Drawing.Color.Green;
                                row.ForeColor = System.Drawing.Color.White;
                                hidPartCode.Value = row.Cells[4].Text;
                                row.Cells[9].BackColor = System.Drawing.Color.Red;
                                break;
                            }
                        }
                        txtToolID.Text = string.Empty;
                        txtToolID.Focus();
                    }
                    else
                    {
                        Message = sResult.Split('~')[1];
                        if (sResult.StartsWith("N~"))
                        {
                            CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                            txtRmBarcode.Text = "";
                            txtRmBarcode.Focus();
                        }
                        if (sResult.StartsWith("ERROR~"))
                        {
                            CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                            txtRmBarcode.Text = "";
                            txtRmBarcode.Focus();
                        }
                    }
                    txtToolID.Focus();
                    txtToolID.Text = string.Empty;
                    txtToolID.ReadOnly = false;
                }
                else
                {
                    CommonHelper.ShowMessage("Invalid tool for selected data", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtToolID.Focus();
                    txtToolID.Text = string.Empty;
                    txtToolID.ReadOnly = false;
                    return;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                txtToolID.Focus();
                txtToolID.Text = string.Empty;
                txtToolID.ReadOnly = false;
            }
        }

        protected void txtRmBarcode_TextChanged(object sender, EventArgs e)
        {
            CheckBarcode();
        }

        #endregion

        protected void drpFGItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindProgramID();
        }

        protected void drpProgramID_SelectedIndexChanged(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            if (drpProgramID.SelectedIndex == 0 || drpProgramID.SelectedIndex == -1)
            {
                CommonHelper.ShowMessage("Please select Program ID", msgerror, CommonHelper.MessageType.Error.ToString());
                drpProgramID.Focus();
                return;
            }
            try
            {
                gvProfileMaster.DataSource = null;
                gvProfileMaster.DataBind();
                drpLineID.Focus();
                blobj = new BL_WIP_Issue();
                string Message = string.Empty;
                DataTable dtProgramDetails = blobj.GetProgramDetailsForWipIssue(drpProgramID.SelectedItem.Text, txtScanMachineID.Text);
                if (dtProgramDetails.Rows.Count > 0)
                {
                    gvProfileMaster.DataSource = dtProgramDetails;
                    gvProfileMaster.DataBind();
                    drpProgramID.Enabled = false;
                    drpProgramID.Height = 35;
                    drpLineID.Enabled = false;
                    drpLineID.Height = 35;
                    drpFGItemCode.Enabled = false;
                    drpFGItemCode.Height = 35;
                    txtScanMachineID.Enabled = false;
                    txtScanMachineID.Height = 35;
                }
                else
                {
                    gvProfileMaster.DataSource = null;
                    gvProfileMaster.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
        }

        protected void Group1_CheckedChanged(Object sender, EventArgs e)
        {
            try
            {
                if (rdBIN.Checked)
                {
                    txtFeederID.ReadOnly = true;
                    txtToolID.ReadOnly = true;
                    txtFeederID.ReadOnly = true;
                    txtFeederLocation.ReadOnly = true;
                    txtFeederID.Text = "";
                    txtToolID.Text = "";
                    txtFeederID.Text = "";
                    txtFeederLocation.Text = "";
                    txtBin.ReadOnly = false;
                    txtRmBarcode.ReadOnly = false;
                    txtBin.Text = string.Empty;
                    txtBin.Focus();
                    txtRmBarcode.Text = "";
                }
                else if (rdTool.Checked)
                {
                    txtBin.ReadOnly = true;
                    txtFeederID.ReadOnly = true;
                    txtFeederID.ReadOnly = true;
                    txtFeederLocation.ReadOnly = true;
                    txtBin.Text = "";
                    txtFeederID.Text = "";
                    txtFeederLocation.Text = "";
                    txtToolID.Text = "";
                    txtRmBarcode.ReadOnly = false;
                    txtToolID.ReadOnly = false;
                    txtRmBarcode.Text = "";
                    txtRmBarcode.Focus();
                }
                else if (rdFeeder.Checked)
                {
                    txtBin.ReadOnly = true;
                    txtToolID.ReadOnly = true;
                    txtBin.Text = "";
                    txtToolID.Text = "";
                    txtFeederID.ReadOnly = false;
                    txtFeederLocation.ReadOnly = false;
                    txtRmBarcode.ReadOnly = false;
                    txtFeederID.Text = "";
                    txtFeederLocation.Text = "";
                    txtRmBarcode.Text = "";
                    txtFeederLocation.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        public void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                drpLineID.SelectedIndex = 0;
                txtScanMachineID.Text = string.Empty;
                lblMachineName.Text = string.Empty;
                drpFGItemCode.Items.Clear();
                drpProgramID.Items.Clear();
                txtBin.Text = string.Empty;
                txtFeederID.Text = string.Empty;
                txtFeederLocation.Text = string.Empty;
                txtRmBarcode.Text = string.Empty;
                txtToolID.Text = string.Empty;
                lblModelNo.Text = string.Empty;
                txtScanMachineID.Enabled = true;
                txtBin.ReadOnly = true;
                drpProgramID.Enabled = true;
                drpLineID.Enabled = true;
                drpProgramID.Enabled = true;
                drpFGItemCode.Enabled = true;
                rdBIN.Checked = false;
                rdFeeder.Checked = false;
                rdTool.Checked = false;
                gvProfileMaster.DataSource = null;
                gvProfileMaster.DataBind();
                drpLineID.Focus();
                lblExcessQty.Text = "0";
                lblMatQty.Text = "0";
                txtAllocatedQty.Text = "0";
                rdFeeder.Checked = true;
                txtFeederLocation.Enabled = true;
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void txtAllocatedQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (Convert.ToDecimal(lblMatQty.Text) > 0)
                {
                    lblExcessQty.Text = Convert.ToString(Convert.ToDecimal(lblMatQty.Text) - Convert.ToDecimal(txtAllocatedQty.Text));
                    btnUpdateQty.Focus();

                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void btnUpdateQty_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (txtRmBarcode.Text.Trim().Length == 0)
                {
                    CommonHelper.ShowMessage("Please scan barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtRmBarcode.Focus();
                    return;
                }
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "No")
                {
                    return;
                }
                string sFeederNo = string.Empty;
                if (drpLineID.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select Line ID", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpLineID.Focus();
                    txtRmBarcode.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    return;
                }
                if (drpFGItemCode.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select FG Item Code ", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpFGItemCode.Focus(); txtRmBarcode.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    return;
                }
                if (string.IsNullOrEmpty(txtScanMachineID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Scan Machine", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtScanMachineID.Focus(); txtRmBarcode.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    return;
                }
                if (drpProgramID.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Program ID not found, Please add program in routing ", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpProgramID.Focus();
                    txtRmBarcode.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    return;
                }
                if (string.IsNullOrEmpty(txtRmBarcode.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan part barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtRmBarcode.Focus();
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    return;
                }
                if (Convert.ToDecimal(Convert.ToDecimal(lblMatQty.Text) - Convert.ToDecimal(txtAllocatedQty.Text)) < 0)
                {
                    CommonHelper.ShowMessage("allocated qty always be in positive", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtAllocatedQty.Focus();
                    txtAllocatedQty.Text = string.Empty;
                    lblExcessQty.Text = "0";
                    return;
                }
                if (rdTool.Checked == false && rdBIN.Checked == false && rdFeeder.Checked == false)
                {
                    foreach (GridViewRow row in gvProfileMaster.Rows)
                    {
                        if ((row.Cells[5].Text.Trim().ToString().Trim().Length > 0 && row.Cells[5].Text.Trim() != "&nbsp;")
                            && rdFeeder.Checked == false
                            )
                        {
                            CommonHelper.ShowMessage("Please select feeder type", msgerror, CommonHelper.MessageType.Error.ToString());
                            rdFeeder.Focus();
                            txtRmBarcode.Text = string.Empty;
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                            return;
                        }
                        if ((row.Cells[7].Text.Trim().ToString().Trim().Length > 0 && row.Cells[7].Text.Trim() != "&nbsp;")
                           && rdTool.Checked == false
                           )
                        {
                            CommonHelper.ShowMessage("Please select tool type", msgerror, CommonHelper.MessageType.Error.ToString());
                            rdFeeder.Focus();
                            txtRmBarcode.Text = string.Empty;
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                            return;
                        }
                    }
                }
                if (rdFeeder.Checked == true)
                {
                    if (string.IsNullOrEmpty(txtFeederLocation.Text.Trim()))
                    {
                        CommonHelper.ShowMessage("Please scan feeder location", msgerror, CommonHelper.MessageType.Error.ToString());
                        txtFeederLocation.Focus();
                        txtRmBarcode.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                        return;
                    }
                }
                DataTable dt = blobj.ValidateBarcode(txtRmBarcode.Text, drpFGItemCode.Text,
                    txtScanMachineID.Text.Trim(), drpProgramID.SelectedValue.Trim());
                if (dt.Rows.Count > 0)
                {
                    if (dt.Columns.Count == 1)
                    {
                        string sData = dt.Rows[0][0].ToString();
                        CommonHelper.ShowMessage(sData.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                        txtRmBarcode.Text = "";
                        txtRmBarcode.Focus();
                        return;
                    }
                    string sBatchNo = dt.Rows[0]["BATCHNO"].ToString();
                    string Part_Code = dt.Rows[0]["Part_Code"].ToString();
                    string sPartType = dt.Rows[0]["PART_TYPE"].ToString();
                    string sStatus = dt.Rows[0]["STATUS"].ToString();


                    blobj = new BL_WIP_Issue();
                    string sResult = blobj.SaveData(hidmpid.Value, txtScanMachineID.Text.Trim(), txtRmBarcode.Text.Trim()
                        , sBatchNo, txtBin.Text.Trim(), txtAllocatedQty.Text.Trim(), Session["UserID"].ToString(), Part_Code, sPartType
                        , txtFeederID.Text, txtFeederLocation.Text, sFeederNo, txtToolID.Text, drpProgramID.SelectedValue.ToString()
                       , Session["SiteCode"].ToString(), Session["LINECODE"].ToString()
                       , drpFGItemCode.Text
                       );
                    if (sResult.StartsWith("SUCCESS~"))
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                        CommonHelper.ShowMessage("Scanned barcode Issued Sucessfully : " + txtRmBarcode.Text.Trim() + ", Issued Qty :  " + txtAllocatedQty.Text, msgsuccess, CommonHelper.MessageType.Success.ToString());
                        if (rdTool.Checked == true)
                        {
                            foreach (GridViewRow row in gvProfileMaster.Rows)
                            {
                                if (row.Cells[4].Text.ToString().ToUpper() == Part_Code.ToUpper())
                                {
                                    row.BackColor = System.Drawing.Color.Green;
                                    row.ForeColor = System.Drawing.Color.White;
                                    hidPartCode.Value = row.Cells[4].Text;
                                    row.Cells[8].Text = txtRmBarcode.Text;
                                    if (sStatus == "1")
                                    {
                                        string dWorkOrderQty = sResult.Split('~')[2];
                                        row.Cells[10].Text = dWorkOrderQty;
                                    }
                                }
                            }
                            txtToolID.Focus();
                            txtRmBarcode.Text = string.Empty;
                            txtToolID.Text = string.Empty;
                        }
                        else if (rdFeeder.Checked == true)
                        {
                            foreach (GridViewRow row in gvProfileMaster.Rows)
                            {
                                if (row.Cells[6].Text.ToString().ToUpper() == txtFeederLocation.Text.ToUpper()
                                    && row.Cells[4].Text.ToString().ToUpper() == Part_Code.ToUpper()
                                    )
                                {
                                    row.BackColor = System.Drawing.Color.Green;
                                    row.ForeColor = System.Drawing.Color.White;
                                    hidPartCode.Value = row.Cells[4].Text;
                                    row.Cells[8].Text = txtRmBarcode.Text;
                                    if (sStatus == "1")
                                    {
                                        string dWorkOrderQty = sResult.Split('~')[2];
                                        row.Cells[10].Text = dWorkOrderQty;
                                    }
                                    break;
                                }
                            }
                        }
                        else if (rdBIN.Checked == true)
                        {
                            foreach (GridViewRow row in gvProfileMaster.Rows)
                            {
                                if (row.Cells[4].Text.ToString().ToUpper() == Part_Code.ToUpper())
                                {
                                    row.BackColor = System.Drawing.Color.Green;
                                    row.ForeColor = System.Drawing.Color.White;
                                    hidPartCode.Value = row.Cells[4].Text;
                                    row.Cells[8].Text = txtRmBarcode.Text;
                                    if (sStatus == "1")
                                    {
                                        string dWorkOrderQty = sResult.Split('~')[2];
                                        row.Cells[10].Text = dWorkOrderQty;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                        Message = sResult.Split('~')[1];
                        if (sResult.StartsWith("N~"))
                        {
                            CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                            txtRmBarcode.Text = "";
                            txtRmBarcode.Focus();
                        }
                        if (sResult.StartsWith("ERROR~"))
                        {
                            CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                            txtRmBarcode.Text = "";
                            txtRmBarcode.Focus();
                        }
                    }
                    txtAllocatedQty.Text = "0";
                    lblMatQty.Text = "0";
                    lblExcessQty.Text = "0";
                    Group1_CheckedChanged(null, null);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    CommonHelper.ShowMessage("Scanned barcode not found in wip area against selected data", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtRmBarcode.Text = string.Empty; txtRmBarcode.Focus();
                    txtAllocatedQty.Text = "0";
                    lblMatQty.Text = "0";
                    lblExcessQty.Text = "0";
                    return;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void btnGetDetails_Click(object sender, EventArgs e)
        {
            try
            {
                drpProgramID_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        
        protected void chkAutoRefrsh_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAutoRefrsh.Checked == true)
                {
                    Timer1.Enabled = true;
                }
                else
                {
                    Timer1.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, 
                    System.Reflection.MethodBase.GetCurrentMethod().Name,"Timer running");
                drpProgramID_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}