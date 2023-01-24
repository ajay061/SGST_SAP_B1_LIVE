using Common;
using System;
using BusinessLayer.WIP;
using System.Data;
using System.Web.UI;

namespace SGST.INE.WIP
{
    public partial class PCBScanningNew : System.Web.UI.Page
    {
        string Message = "";
        BL_WIP_PCBScanning blobj = new BL_WIP_PCBScanning();
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
            Response.AddHeader("Pragma", "no-cache");
            Response.AddHeader("Expires", "0");
            if (!IsPostBack)
            {
                try
                {
                    lblCount.Text = "0";
                    BindStationID();
                    //diRework.Visible = false;
                    lblLineCode.Text = Session["LINECODE"].ToString();

                }
                catch (Exception ex)
                {
                    CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                }
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Signin/v1/Login.aspx?Session=Null");
            }
        }

        private void BindFGItemCode()
        {
            try
            {
                drpFGItemCode.Items.Clear();
                txtPCBID.Text = string.Empty;
                lblCount.Text = "0";
                blobj = new BL_WIP_PCBScanning();
                if (txtScanMachineID.Text.Length > 0)
                {
                    string sResult = string.Empty;
                    DataTable dtFGItemCode = blobj.BindFGItemCode(Session["LINECODE"].ToString(), txtScanMachineID.Text.Trim(),
                        out sResult, Session["SiteCode"].ToString());
                    if (dtFGItemCode.Rows.Count > 0)
                    {
                        CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                        clsCommon.FillComboBox(drpFGItemCode, dtFGItemCode, true);
                        drpFGItemCode.SelectedIndex = 0;
                        drpFGItemCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void BindStationID()
        {
            try
            {
                drpstation.Items.Clear();
                string sResult = string.Empty;
                blobj = new BL_WIP_PCBScanning();
                DataTable dtStationID = blobj.BindReWorkStationID(out sResult, Session["SiteCode"].ToString());
                if (dtStationID.Rows.Count > 0)
                {
                    CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                    clsCommon.FillComboBox(drpstation, dtStationID, true);
                    drpstation.SelectedIndex = 0;
                    drpstation.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void BindDefect()
        {
            try
            {
                drpDefect.Items.Clear();
                string sResult = string.Empty;
                blobj = new BL_WIP_PCBScanning();
                DataTable dtStationID = blobj.BindDefect(txtScanMachineID.Text.Trim(), out sResult
                    , Session["SiteCode"].ToString());
                if (dtStationID.Rows.Count > 0)
                {
                    CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                    clsCommon.FillComboBox(drpDefect, dtStationID, true);
                    drpDefect.SelectedIndex = 0;
                    drpDefect.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void GetWaveTool()
        {
            try
            {
                Session["WaveToolData"] = null;
                string sResult = string.Empty;
                blobj = new BL_WIP_PCBScanning();
                DataTable dtStationID = blobj.GetWaveTool(drpFGItemCode.Text, Session["SiteCode"].ToString(), Session["LineCode"].ToString());
                if (dtStationID.Rows.Count > 0)
                {
                    if (dtStationID.Rows[0][0].ToString() == "1")
                    {
                        Session["WaveToolData"] = dtStationID.Rows[0][0].ToString();
                        GetWaveToolCount();
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void GetWaveToolCount()
        {
            try
            {
                string sResult = string.Empty;
                blobj = new BL_WIP_PCBScanning();
                DataTable dtStationID = blobj.GetWaveToolCount(drpFGItemCode.Text, Session["SiteCode"].ToString(), Session["LineCode"].ToString());
                if (dtStationID.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dtStationID.Rows[0][0].ToString()) < Convert.ToInt32(dtStationID.Rows[0][1].ToString()) + 10)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Only 10 qty left for tool mapped with selected fg item code')", true);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void GetOutProcess()
        {
            try
            {
                drpType.Items.Clear();
                if (drpFGItemCode.SelectedIndex > 0 && txtScanMachineID.Text.Length > 0)
                {
                    string sResult = string.Empty;
                    blobj = new BL_WIP_PCBScanning();
                    DataTable dtStationID = blobj.GetOutProces(out sResult, drpFGItemCode.Text, txtScanMachineID.Text.Trim()
                        , Session["SiteCode"].ToString(), Session["LINECODE"].ToString()
                        );
                    if (dtStationID.Rows.Count > 0 && dtStationID.Rows[0][0].ToString() != "0")
                    {
                        CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                        drpType.Items.Add("In");
                        drpType.Items.Add("Out");
                    }
                    else
                    {
                        drpType.Items.Add("In");
                    }
                    dtStationID = new DataTable();
                    dtStationID = blobj.ValidateMaterail(txtScanMachineID.Text.Trim(), Session["LINECODE"].ToString(),
                         Session["SiteCode"].ToString(), drpFGItemCode.Text, drpTOPBottom.Text
                        );
                    if (dtStationID.Rows.Count > 0 && dtStationID.Rows[0][0].ToString() != "0")
                    {
                        string sData = dtStationID.Rows[0][0].ToString();
                        if (sData.StartsWith("ERROR~"))
                        {
                            CommonHelper.ShowMessage(sData, msgerror, CommonHelper.MessageType.Error.ToString());
                            drpFGItemCode.SelectedIndex = 0;
                            return;
                        }
                    }
                }
                else
                {
                    drpType.Items.Add("In");
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void txtPCBID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                string sIsSamplePCBChecked = "1";
                if (chkSamplePCB.Checked == true)
                {
                    sIsSamplePCBChecked = "0";
                }
                else
                {
                    sIsSamplePCBChecked = "1";
                }
                if (string.IsNullOrEmpty(txtScanMachineID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan machine barcode", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtScanMachineID.Focus();
                    txtScanMachineID.Text = string.Empty;
                    txtPCBID.Text = string.Empty;
                    chkSamplePCB.Checked = false;
                    return;
                }

                else if (drpFGItemCode.Items.Count == 0)
                {
                    CommonHelper.ShowMessage("Please select fg item code", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpFGItemCode.Focus(); txtPCBID.Text = string.Empty;
                    chkSamplePCB.Checked = false;
                    return;
                }
                else if (drpFGItemCode.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select fg item code", msginfo, CommonHelper.MessageType.Info.ToString());
                    drpFGItemCode.Focus();
                    txtPCBID.Text = string.Empty;
                    chkSamplePCB.Checked = false;
                    return;
                }
                else if (chkRepairRequired.Checked == true)
                {
                    if (drpstation.SelectedIndex == 0)
                    {
                        CommonHelper.ShowMessage("Please select repair station", msginfo, CommonHelper.MessageType.Info.ToString());
                        drpstation.Focus();
                        txtPCBID.Text = string.Empty;
                        chkSamplePCB.Checked = false;
                        return;
                    }
                    if (drpDefect.SelectedIndex == 0)
                    {
                        CommonHelper.ShowMessage("Please select defect", msginfo, CommonHelper.MessageType.Info.ToString());
                        drpDefect.Focus();
                        txtPCBID.Text = string.Empty;
                        chkSamplePCB.Checked = false;
                        return;
                    }
                }
                else if (string.IsNullOrEmpty(txtPCBID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan PCB barcode", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtPCBID.Focus();
                    txtPCBID.Text = string.Empty;
                    chkSamplePCB.Checked = false;
                    return;
                }
                blobj = new BL_WIP_PCBScanning();
                string sIsRepairRequired = string.Empty;
                string sReworkStationID = string.Empty;
                string sMachineFailureValidate = string.Empty;
                string sDefect = string.Empty;
                string sAOIPCBScanned = "0";
                string sOutRequired = "0";
                if (chkRepairRequired.Checked == true)
                {
                    sIsRepairRequired = "0";
                    sReworkStationID = drpstation.Text;
                    sAOIPCBScanned = "1";
                }
                else
                {
                    sIsRepairRequired = "1";
                    sReworkStationID = "";
                }
                if (chkPassedFailedBarcode.Checked == true)
                {
                    sMachineFailureValidate = "1";
                }
                else
                {
                    sMachineFailureValidate = "0";
                }
                if (drpType.Items.Count > 1)
                {
                    sOutRequired = "1";
                }
                string sWaveToolValidate = "0";
                if (txtScanMachineID.Text.ToUpper().Contains("WAVE"))
                {
                    if (Session["WaveToolData"] != null)
                    {
                        sWaveToolValidate = Session["WaveToolData"].ToString();
                    }
                }
                string sAgingProcessValidate = "0";
                if (txtScanMachineID.Text.ToUpper().Contains("AFT"))
                {
                    sAgingProcessValidate = "1";
                }
                sDefect = drpDefect.Text;
                string sProcessType = drpType.Text;
                string sResult = blobj.SavePCBbarcode(Session["LINECODE"].ToString(), txtScanMachineID.Text.Trim(),
                   drpFGItemCode.Text,
                    txtPCBID.Text.Trim().ToUpper()
                    , Session["UserID"].ToString(), sReworkStationID, sIsRepairRequired, sMachineFailureValidate,
                    drpType.Text.ToUpper(), sAOIPCBScanned, Session["SiteCode"].ToString(), sOutRequired
                    , sDefect, txtRefBarcode.Text, txtRemarks.Text.Trim(), txtToolBarcode.Text.Trim(), sWaveToolValidate, sAgingProcessValidate, sIsSamplePCBChecked
                    );
                Message = sResult.Split('~')[1];
                if (sResult.StartsWith("SUCCESS"))
                {
                    if (chkSamplePCB.Checked == false)
                    {


                        if (chkRepairRequired.Checked == true)
                        {
                            CommonHelper.ShowMessage("Part (" + txtPCBID.Text.Trim() + ") moved to repair station (" + sReworkStationID + ") for rework", msgsuccess, CommonHelper.MessageType.Success.ToString());
                        }
                        else
                        {
                            CommonHelper.ShowMessage(Message, msgsuccess, CommonHelper.MessageType.Success.ToString());
                        }
                        //lblCount.Text = Convert.ToString(Convert.ToInt32(lblCount.Text) + 1);
                        //if (txtScanMachineID.Text.ToUpper().Contains("WAVE"))
                        //{
                        //    if (Convert.ToInt32(lblCount.Text) % 10 == 0)
                        //    {
                        //        if (Session["WaveToolData"] != null)
                        //        {
                        //            GetWaveToolCount();
                        //        }
                        //    }
                        //}
                        if (txtRefBarcode.Text.Length > 0)
                        {
                            txtRefBarcode.Text = string.Empty;
                            txtPCBID.Text = string.Empty;
                            txtRefBarcode.Focus();
                        }
                        else
                        {
                            txtRefBarcode.Text = string.Empty;
                            //txtPCBID.Text = string.Empty;
                            txtPCBID.Focus();
                        }
                        if (lblAOIMessage.Visible == false)
                        {
                            //chkRepairRequired.Checked = false;
                            //diRework.Visible = false;
                        }
                        return;
                    }
                    else
                    {
                        CommonHelper.ShowMessage(Message, msgsuccess, CommonHelper.MessageType.Success.ToString());
                        txtRefBarcode.Text = string.Empty;
                        txtPCBID.Text = string.Empty;
                        txtPCBID.Focus();
                        return;
                    }
                }
                else if (sResult.StartsWith("N~"))
                {
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    if (txtRefBarcode.Text.Length > 0)
                    {
                        txtRefBarcode.Text = string.Empty;
                        txtPCBID.Text = string.Empty;
                        txtRefBarcode.Focus();
                    }
                    else
                    {
                        txtRefBarcode.Text = string.Empty;
                        txtPCBID.Text = string.Empty;
                        txtPCBID.Focus();
                    }
                    if (lblAOIMessage.Visible == false)
                    {
                        chkRepairRequired.Checked = false;
                    }
                    return;
                }
                else if (sResult.StartsWith("ERROR~"))
                {
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    if (txtRefBarcode.Text.Length > 0)
                    {
                        txtRefBarcode.Text = string.Empty;
                        txtPCBID.Text = string.Empty;
                        txtRefBarcode.Focus();
                    }
                    else
                    {
                        txtRefBarcode.Text = string.Empty;
                        txtPCBID.Text = string.Empty;
                        txtPCBID.Focus();
                    }
                    chkRepairRequired.Checked = false;
                }
                else if (sResult.StartsWith("NOTFOUND~"))
                {
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    if (txtRefBarcode.Text.Length > 0)
                    {
                        txtRefBarcode.Text = string.Empty;
                        txtPCBID.Text = string.Empty;
                        txtRefBarcode.Focus();
                    }
                    else
                    {
                        txtRefBarcode.Text = string.Empty;
                        txtPCBID.Text = string.Empty;
                        txtPCBID.Focus();
                    }
                    chkRepairRequired.Checked = false;
                }
                else if (sResult.StartsWith("MACHINEFAILED~"))
                {
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    if (txtRefBarcode.Text.Length > 0)
                    {
                        txtRefBarcode.Text = string.Empty;
                        txtPCBID.Text = string.Empty;
                        txtRefBarcode.Focus();
                    }
                    else
                    {
                        txtRefBarcode.Text = string.Empty;
                        txtPCBID.Text = string.Empty;
                        txtPCBID.Focus();
                    }
                    chkRepairRequired.Checked = false;
                }

            }
            catch (Exception ex)
            {
                txtRefBarcode.Text = string.Empty;
                txtPCBID.Text = string.Empty;
                txtRefBarcode.Focus();
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                drpFGItemCode.Items.Clear();
                txtScanMachineID.Enabled = true;
                lblMachineName.Text = "";
                txtScanMachineID.Text = "";
                txtPCBID.Text = string.Empty;
                lblCount.Text = "0";
                lblModelNo.Text = "";
                drpstation.SelectedIndex = 0;
                chkPassedFailedBarcode.Checked = false;
                chkRepairRequired.Checked = false;
                diRework.Visible = false;
                lblAOIMessage.Visible = false;
                txtRefBarcode.Text = string.Empty;
                txtToolBarcode.Text = string.Empty;
                txtRefBarcode.Text = string.Empty;
                chkSamplePCB.Checked = false;
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void chkRepairRequired_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                //if (string.IsNullOrEmpty(txtScanMachineID.Text.Trim()))
                //{
                //    CommonHelper.ShowMessage("Please scan machine barcode", msginfo, CommonHelper.MessageType.Info.ToString());
                //    txtScanMachineID.Focus();
                //    txtScanMachineID.Text = string.Empty;
                //    chkRepairRequired.Checked = false;
                //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                //    return;
                //}
                //if (chkRepairRequired.Checked == true)
                //{
                //    BindDefect();
                //    diRework.Visible = true;
                //    lblAOIMessage.Visible = true;
                //}
                //else
                //{
                //    diRework.Visible = false;
                //    drpDefect.Items.Clear();
                //    lblAOIMessage.Visible = false;
                //}
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void txtScanMachineID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (string.IsNullOrEmpty(txtScanMachineID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Scan Machine", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtScanMachineID.Focus();
                    return;
                }
                drpDefect.Items.Clear();
                blobj = new BL_WIP_PCBScanning();
                DataTable dt = blobj.ValidateMachine(txtScanMachineID.Text.Trim(), Session["LINECODE"].ToString()
                    , Session["SiteCode"].ToString()
                    );
                if (dt.Rows.Count > 0)
                {
                    CommonHelper.ShowMessage("Valid machine", msgsuccess, CommonHelper.MessageType.Success.ToString());
                    lblMachineName.Text = dt.Rows[0][0].ToString();
                    lblModelNo.Text = dt.Rows[0][2].ToString();
                    BindFGItemCode();
                    diRework.Visible = true;
                    BindDefect();
                }
                else
                {
                    CommonHelper.ShowMessage("Invalid machine", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtScanMachineID.Focus();
                    txtScanMachineID.Text = string.Empty;
                    lblMachineName.Text = "";
                    lblModelNo.Text = "";
                    drpFGItemCode.Items.Clear();
                    return;
                }
            }
            catch (Exception ex)
            {
                txtScanMachineID.Text = string.Empty;
                txtScanMachineID.Focus();
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void drpFGItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            lblAOIMessage.Visible = false;
            chkRepairRequired.Checked = false;
            //diRework.Visible = false;
            lblCount.Text = "0";
            txtPCBID.Text = string.Empty;
            txtToolBarcode.Text = string.Empty;
            txtRefBarcode.Text = string.Empty;
            drpType.SelectedIndex = 0;
            GetOutProcess();
            GetWaveTool();

        }

        protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                lblAOIMessage.Visible = false;
                chkRepairRequired.Checked = false;
                //diRework.Visible = false;
                txtToolBarcode.Text = string.Empty;
                txtRefBarcode.Text = string.Empty;
                lblCount.Text = "0";
                txtPCBID.Text = string.Empty;
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void txtRefBarcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtPCBID.Focus();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void txtToolBarcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtScanMachineID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan machine barcode", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtScanMachineID.Focus();
                    txtScanMachineID.Text = string.Empty;
                    txtPCBID.Text = string.Empty;
                    return;
                }
                else if (drpFGItemCode.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select fg item code", msginfo, CommonHelper.MessageType.Info.ToString());
                    drpFGItemCode.Focus();
                    txtPCBID.Text = string.Empty;
                    return;
                }
                else if (chkRepairRequired.Checked == true)
                {
                    if (drpstation.SelectedIndex == 0)
                    {
                        CommonHelper.ShowMessage("Please select repair station", msginfo, CommonHelper.MessageType.Info.ToString());
                        drpstation.Focus();
                        txtPCBID.Text = string.Empty;
                        return;
                    }
                    if (drpDefect.SelectedIndex == 0)
                    {
                        CommonHelper.ShowMessage("Please select defect", msginfo, CommonHelper.MessageType.Info.ToString());
                        drpDefect.Focus();
                        txtPCBID.Text = string.Empty;
                        return;
                    }
                }
                else if (string.IsNullOrEmpty(txtToolBarcode.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please select tool barcode", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtToolBarcode.Focus();
                    txtPCBID.Text = string.Empty;
                    return;
                }
                blobj = new BL_WIP_PCBScanning();
                DataTable dt = blobj.ValidateTool(txtToolBarcode.Text.Trim(), Session["SiteCode"].ToString(),
                    txtScanMachineID.Text.Trim(), drpFGItemCode.Text.Trim()
                    );
                if (dt.Rows.Count > 0)
                {
                    string sResult = dt.Rows[0][0].ToString();
                    if (sResult.StartsWith("SUCCESS~"))
                    {
                        CommonHelper.ShowMessage("Valid Tool", msgsuccess, CommonHelper.MessageType.Success.ToString());
                        txtPCBID.Focus();
                    }
                    else
                    {
                        CommonHelper.ShowMessage(sResult.Split('~')[1].ToString(), msgerror, CommonHelper.MessageType.Error.ToString());
                        txtPCBID.Text = string.Empty;
                        txtToolBarcode.Text = string.Empty;
                        txtToolBarcode.Focus();
                        return;
                    }
                }
                else
                {
                    CommonHelper.ShowMessage("Invalid Tool", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPCBID.Text = string.Empty;
                    txtToolBarcode.Text = string.Empty;
                    txtToolBarcode.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                string sIsSamplePCBChecked = "1";
                if (chkSamplePCB.Checked == true)
                {
                    sIsSamplePCBChecked = "0";
                }
                else
                {
                    sIsSamplePCBChecked = "1";
                }
                if (string.IsNullOrEmpty(txtScanMachineID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan machine barcode", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtScanMachineID.Focus();
                    txtScanMachineID.Text = string.Empty;
                    txtPCBID.Text = string.Empty;
                    chkSamplePCB.Checked = false;
                    return;
                }

                else if (drpFGItemCode.Items.Count == 0)
                {
                    CommonHelper.ShowMessage("Please select fg item code", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpFGItemCode.Focus(); txtPCBID.Text = string.Empty;
                    chkSamplePCB.Checked = false;
                    return;
                }
                else if (drpFGItemCode.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select fg item code", msginfo, CommonHelper.MessageType.Info.ToString());
                    drpFGItemCode.Focus();
                    txtPCBID.Text = string.Empty;
                    chkSamplePCB.Checked = false;
                    return;
                }
                else if (chkRepairRequired.Checked == true)
                {
                    if (drpstation.SelectedIndex == 0)
                    {
                        CommonHelper.ShowMessage("Please select repair station", msginfo, CommonHelper.MessageType.Info.ToString());
                        drpstation.Focus();
                        txtPCBID.Text = string.Empty;
                        chkSamplePCB.Checked = false;
                        return;
                    }
                    if (drpDefect.SelectedIndex == 0)
                    {
                        CommonHelper.ShowMessage("Please select defect", msginfo, CommonHelper.MessageType.Info.ToString());
                        drpDefect.Focus();
                        txtPCBID.Text = string.Empty;
                        chkSamplePCB.Checked = false;
                        return;
                    }
                }
                else if (string.IsNullOrEmpty(txtPCBID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan PCB barcode", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtPCBID.Focus();
                    txtPCBID.Text = string.Empty;
                    chkSamplePCB.Checked = false;
                    return;
                }
                blobj = new BL_WIP_PCBScanning();
                string sIsRepairRequired = "1";
                string sReworkStationID = ""; 
                string sMachineFailureValidate = string.Empty;
                string sDefect = string.Empty;
                string sAOIPCBScanned = "0";
                string sOutRequired = "0";
                //if (chkRepairRequired.Checked == true)
                //{
                //    sIsRepairRequired = "0";
                //    sReworkStationID = drpstation.Text;
                //    sAOIPCBScanned = "1";
                //}
                //else
                //{
                //    sIsRepairRequired = "1";
                //    sReworkStationID = "";
                //}
                if (chkPassedFailedBarcode.Checked == true)
                {
                    sMachineFailureValidate = "1";
                }
                else
                {
                    sMachineFailureValidate = "0";
                }
                if (drpType.Items.Count > 1)
                {
                    sOutRequired = "1";
                }
                string sWaveToolValidate = "0";
                if (txtScanMachineID.Text.ToUpper().Contains("WAVE"))
                {
                    if (Session["WaveToolData"] != null)
                    {
                        sWaveToolValidate = Session["WaveToolData"].ToString();
                    }
                }
                string sAgingProcessValidate = "0";
                if (txtScanMachineID.Text.ToUpper().Contains("AFT"))
                {
                    sAgingProcessValidate = "1";
                }
                sDefect = drpDefect.Text;
                string sProcessType = drpType.Text;
                string sResult = blobj.SaveOKPCBbarcode(Session["LINECODE"].ToString(), txtScanMachineID.Text.Trim(),
                   drpFGItemCode.Text,
                    txtPCBID.Text.Trim().ToUpper()
                    , Session["UserID"].ToString(), sReworkStationID, sIsRepairRequired, sMachineFailureValidate,
                    drpType.Text.ToUpper(), sAOIPCBScanned, Session["SiteCode"].ToString(), sOutRequired
                    , sDefect, txtRefBarcode.Text, txtRemarks.Text.Trim(), txtToolBarcode.Text.Trim(), sWaveToolValidate, sAgingProcessValidate, sIsSamplePCBChecked
                    );
                Message = sResult.Split('~')[1];
                if (sResult.StartsWith("SUCCESS"))
                {
                    if (chkSamplePCB.Checked == false)
                    {


                        if (chkRepairRequired.Checked == true)
                        {
                            CommonHelper.ShowMessage("Part (" + txtPCBID.Text.Trim() + ") moved to repair station (" + sReworkStationID + ") for rework", msgsuccess, CommonHelper.MessageType.Success.ToString());
                        }
                        else
                        {
                            CommonHelper.ShowMessage(Message, msgsuccess, CommonHelper.MessageType.Success.ToString());
                        }
                        lblCount.Text = Convert.ToString(Convert.ToInt32(lblCount.Text) + 1);
                        if (txtScanMachineID.Text.ToUpper().Contains("WAVE"))
                        {
                            if (Convert.ToInt32(lblCount.Text) % 10 == 0)
                            {
                                if (Session["WaveToolData"] != null)
                                {
                                    GetWaveToolCount();
                                }
                            }
                        }
                        if (txtRefBarcode.Text.Length > 0)
                        {
                            txtRefBarcode.Text = string.Empty;
                            txtPCBID.Text = string.Empty;
                            txtRefBarcode.Focus();
                        }
                        else
                        {
                            txtRefBarcode.Text = string.Empty;
                            txtPCBID.Text = string.Empty;
                            txtPCBID.Focus();
                        }
                        if (lblAOIMessage.Visible == false)
                        {
                            chkRepairRequired.Checked = false;
                            diRework.Visible = false;
                        }
                        return;
                    }
                    else
                    {
                        CommonHelper.ShowMessage(Message, msgsuccess, CommonHelper.MessageType.Success.ToString());
                        txtRefBarcode.Text = string.Empty;
                        txtPCBID.Text = string.Empty;
                        txtPCBID.Focus();
                        return;
                    }
                }
                else if (sResult.StartsWith("N~"))
                {
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    if (txtRefBarcode.Text.Length > 0)
                    {
                        txtRefBarcode.Text = string.Empty;
                        txtPCBID.Text = string.Empty;
                        txtRefBarcode.Focus();
                    }
                    else
                    {
                        txtRefBarcode.Text = string.Empty;
                        txtPCBID.Text = string.Empty;
                        txtPCBID.Focus();
                    }
                    if (lblAOIMessage.Visible == false)
                    {
                        chkRepairRequired.Checked = false;
                    }
                    return;
                }
                else if (sResult.StartsWith("ERROR~"))
                {
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    if (txtRefBarcode.Text.Length > 0)
                    {
                        txtRefBarcode.Text = string.Empty;
                        txtPCBID.Text = string.Empty;
                        txtRefBarcode.Focus();
                    }
                    else
                    {
                        txtRefBarcode.Text = string.Empty;
                        txtPCBID.Text = string.Empty;
                        txtPCBID.Focus();
                    }
                    chkRepairRequired.Checked = false;
                }
                else if (sResult.StartsWith("NOTFOUND~"))
                {
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    if (txtRefBarcode.Text.Length > 0)
                    {
                        txtRefBarcode.Text = string.Empty;
                        txtPCBID.Text = string.Empty;
                        txtRefBarcode.Focus();
                    }
                    else
                    {
                        txtRefBarcode.Text = string.Empty;
                        txtPCBID.Text = string.Empty;
                        txtPCBID.Focus();
                    }
                    chkRepairRequired.Checked = false;
                }
                else if (sResult.StartsWith("MACHINEFAILED~"))
                {
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    if (txtRefBarcode.Text.Length > 0)
                    {
                        txtRefBarcode.Text = string.Empty;
                        txtPCBID.Text = string.Empty;
                        txtRefBarcode.Focus();
                    }
                    else
                    {
                        txtRefBarcode.Text = string.Empty;
                        txtPCBID.Text = string.Empty;
                        txtPCBID.Focus();
                    }
                    chkRepairRequired.Checked = false;
                }

            }
            catch (Exception ex)
            {
                txtRefBarcode.Text = string.Empty;
                txtPCBID.Text = string.Empty;
                txtRefBarcode.Focus();
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                string sIsSamplePCBChecked = "1";
                chkRepairRequired.Checked = true;
                if (chkSamplePCB.Checked == true)
                {
                    sIsSamplePCBChecked = "0";
                }
                else
                {
                    sIsSamplePCBChecked = "1";
                }
                if (string.IsNullOrEmpty(txtScanMachineID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan machine barcode", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtScanMachineID.Focus();
                    txtScanMachineID.Text = string.Empty;
                    txtPCBID.Text = string.Empty;
                    chkSamplePCB.Checked = false;
                    chkRepairRequired.Checked = false;
                    return;
                }

                else if (drpFGItemCode.Items.Count == 0)
                {
                    CommonHelper.ShowMessage("Please select fg item code", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpFGItemCode.Focus(); txtPCBID.Text = string.Empty;
                    chkSamplePCB.Checked = false;
                    chkRepairRequired.Checked = false;
                    return;
                }
                else if (drpFGItemCode.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select fg item code", msginfo, CommonHelper.MessageType.Info.ToString());
                    drpFGItemCode.Focus();
                    txtPCBID.Text = string.Empty;
                    chkSamplePCB.Checked = false;
                    chkRepairRequired.Checked = false;
                    return;
                }
                else if (drpstation.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select repair station", msginfo, CommonHelper.MessageType.Info.ToString());
                    drpstation.Focus();
                    txtPCBID.Text = string.Empty;
                    chkSamplePCB.Checked = false;
                    chkRepairRequired.Checked = false;
                    return;
                }
                else if (drpDefect.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select defect", msginfo, CommonHelper.MessageType.Info.ToString());
                    drpDefect.Focus();
                    txtPCBID.Text = string.Empty;
                    chkSamplePCB.Checked = false;
                    chkRepairRequired.Checked = false;
                    return;
                }

                else if (string.IsNullOrEmpty(txtPCBID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan PCB barcode", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtPCBID.Focus();
                    txtPCBID.Text = string.Empty;
                    chkSamplePCB.Checked = false;
                    chkRepairRequired.Checked = false;
                    return;
                }
                blobj = new BL_WIP_PCBScanning();
                string sIsRepairRequired = string.Empty;
                string sReworkStationID = string.Empty;
                string sMachineFailureValidate = string.Empty;
                string sDefect = string.Empty;
                string sAOIPCBScanned = "0";
                string sOutRequired = "0";
                //For Repair
                sIsRepairRequired = "0";
                sReworkStationID = drpstation.Text;
                sAOIPCBScanned = "1";
                if (chkPassedFailedBarcode.Checked == true)
                {
                    sMachineFailureValidate = "1";
                }
                else
                {
                    sMachineFailureValidate = "0";
                }
                if (drpType.Items.Count > 1)
                {
                    sOutRequired = "1";
                }
                string sWaveToolValidate = "0";
                if (txtScanMachineID.Text.ToUpper().Contains("WAVE"))
                {
                    if (Session["WaveToolData"] != null)
                    {
                        sWaveToolValidate = Session["WaveToolData"].ToString();
                    }
                }
                string sAgingProcessValidate = "0";
                if (txtScanMachineID.Text.ToUpper().Contains("AFT"))
                {
                    sAgingProcessValidate = "1";
                }
                sDefect = drpDefect.Text;
                string sProcessType = drpType.Text;
                string sResult = blobj.SaveRejectPCBbarcode(Session["LINECODE"].ToString(), txtScanMachineID.Text.Trim(),
                   drpFGItemCode.Text,
                    txtPCBID.Text.Trim().ToUpper()
                    , Session["UserID"].ToString(), sReworkStationID, sIsRepairRequired, sMachineFailureValidate,
                    drpType.Text.ToUpper(), sAOIPCBScanned, Session["SiteCode"].ToString(), sOutRequired
                    , sDefect, txtRefBarcode.Text, txtRemarks.Text.Trim(), txtToolBarcode.Text.Trim(), sWaveToolValidate, sAgingProcessValidate, sIsSamplePCBChecked
                    );
                //Message = sResult.Split('~')[1];
                if (sResult.StartsWith("SUCCESS"))
                {
                    if (chkSamplePCB.Checked == false)
                    {
                        if (chkRepairRequired.Checked == true)
                        {
                            CommonHelper.ShowMessage("Part (" + txtPCBID.Text.Trim() + ") moved to repair station (" + sReworkStationID + ") for rework", msgsuccess, CommonHelper.MessageType.Success.ToString());
                        }
                        else
                        {
                            CommonHelper.ShowMessage(Message, msgsuccess, CommonHelper.MessageType.Success.ToString());
                        }
                        lblCount.Text = Convert.ToString(Convert.ToInt32(lblCount.Text) + 1);
                        if (txtScanMachineID.Text.ToUpper().Contains("WAVE"))
                        {
                            if (Convert.ToInt32(lblCount.Text) % 10 == 0)
                            {
                                if (Session["WaveToolData"] != null)
                                {
                                    GetWaveToolCount();
                                }
                            }
                        }
                        if (txtRefBarcode.Text.Length > 0)
                        {
                            txtRefBarcode.Text = string.Empty;
                            txtPCBID.Text = string.Empty;
                            chkRepairRequired.Checked = false;
                            txtRefBarcode.Focus();
                        }
                        else
                        {
                            txtRefBarcode.Text = string.Empty;
                            txtPCBID.Text = string.Empty;
                            chkRepairRequired.Checked = false;
                            txtPCBID.Focus();
                        }
                        if (lblAOIMessage.Visible == false)
                        {
                            chkRepairRequired.Checked = false;
                            diRework.Visible = false;
                        }
                        return;
                    }
                    else
                    {
                        CommonHelper.ShowMessage(Message, msgsuccess, CommonHelper.MessageType.Success.ToString());
                        txtRefBarcode.Text = string.Empty;
                        txtPCBID.Text = string.Empty;
                        chkRepairRequired.Checked = false;
                        txtPCBID.Focus();
                        return;
                    }
                }
                else if (sResult.StartsWith("N~"))
                {
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    if (txtRefBarcode.Text.Length > 0)
                    {
                        txtRefBarcode.Text = string.Empty;
                        txtPCBID.Text = string.Empty;
                        chkRepairRequired.Checked = false;
                        txtRefBarcode.Focus();
                    }
                    else
                    {
                        txtRefBarcode.Text = string.Empty;
                        txtPCBID.Text = string.Empty;
                        chkRepairRequired.Checked = false;
                        txtPCBID.Focus();
                    }
                    if (lblAOIMessage.Visible == false)
                    {
                        chkRepairRequired.Checked = false;
                    }
                    return;
                }
                else if (sResult.StartsWith("ERROR~"))
                {
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    if (txtRefBarcode.Text.Length > 0)
                    {
                        txtRefBarcode.Text = string.Empty;
                        txtPCBID.Text = string.Empty;
                        chkRepairRequired.Checked = false;
                        txtRefBarcode.Focus();
                    }
                    else
                    {
                        txtRefBarcode.Text = string.Empty;
                        txtPCBID.Text = string.Empty;
                        chkRepairRequired.Checked = false;
                        txtPCBID.Focus();
                    }
                    chkRepairRequired.Checked = false;
                }
                else if (sResult.StartsWith("NOTFOUND~"))
                {
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    if (txtRefBarcode.Text.Length > 0)
                    {
                        txtRefBarcode.Text = string.Empty;
                        txtPCBID.Text = string.Empty;
                        chkRepairRequired.Checked = false;
                        txtRefBarcode.Focus();
                    }
                    else
                    {
                        txtRefBarcode.Text = string.Empty;
                        txtPCBID.Text = string.Empty;
                        txtPCBID.Focus();
                    }
                    chkRepairRequired.Checked = false;
                }
                else if (sResult.StartsWith("MACHINEFAILED~"))
                {
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    if (txtRefBarcode.Text.Length > 0)
                    {
                        txtRefBarcode.Text = string.Empty;
                        txtPCBID.Text = string.Empty;
                        txtRefBarcode.Focus();
                    }
                    else
                    {
                        txtRefBarcode.Text = string.Empty;
                        txtPCBID.Text = string.Empty;
                        txtPCBID.Focus();
                    }
                    chkRepairRequired.Checked = false;
                }

            }
            catch (Exception ex)
            {
                txtRefBarcode.Text = string.Empty;
                txtPCBID.Text = string.Empty;
                chkRepairRequired.Checked = false;
                txtRefBarcode.Focus();
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}