using Common;
using System;
using BusinessLayer.WIP;
using System.Data;
using System.Linq;

namespace SGST.INE.WIP
{
    public partial class WIPFQAScanning : System.Web.UI.Page
    {
        string Message = "";
        DataTable dtScanData;
        BL_FQA_Scanning blobj = new BL_FQA_Scanning();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usertype"].ToString() != "ADMIN")
            {
                string _strRights = CommonHelper.GetRights("FQA SCANNING", (DataTable)Session["USER_RIGHTS"]);
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
                    BindStationID();
                    BindDefect();
                }
                catch (Exception ex)
                {
                    CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
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
                blobj = new BL_FQA_Scanning();
                if (txtScanMachineID.Text.Length > 0)
                {
                    string sResult = string.Empty;
                    DataTable dtFGItemCode = blobj.BindFGItemCode(txtScanMachineID.Text.Trim(), out sResult,
                        Session["SiteCode"].ToString(), Session["LINECODE"].ToString());
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
                blobj = new BL_FQA_Scanning();
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
                blobj = new BL_FQA_Scanning();
                DataTable dtStationID = blobj.BindDefect(out sResult, Session["SiteCode"].ToString());
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
        private void BindFQASampling()
        {
            try
            {
                lblSamplingRate.Text = "0";
                string sResult = string.Empty;
                blobj = new BL_FQA_Scanning();
                DataTable dtStationID = blobj.BindFQASampling(drpFGItemCode.Text, Session["SiteCode"].ToString(),txtScanMachineID.Text);
                if (dtStationID.Rows.Count > 0)
                {
                    lblSamplingRate.Text = dtStationID.Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        public void AddBoxPCBData(string sPCBBarcode, string sPCBmasterID, string sRefNo)
        {
            try
            {
                if (ViewState["Row"] != null)
                {
                    dtScanData = (DataTable)ViewState["Row"];
                    DataRow dr = null;
                    if (dtScanData.Rows.Count > 0)
                    {
                        bool PCBValidate = false;
                        PCBValidate = dtScanData.Select().ToList().Exists(row => row["PART_BARCODE"].ToString().ToUpper() == txtPCBLotBarcode.Text);
                        if (PCBValidate)
                        {
                            CommonHelper.ShowMessage("barcode already scanned.", msgerror, CommonHelper.MessageType.Error.ToString());
                            txtPCBLotBarcode.Text = string.Empty;
                            txtPCBLotBarcode.Focus();
                            return;
                        }
                        dr = dtScanData.NewRow();
                        dr["PART_BARCODE"] = txtPCBLotBarcode.Text.Trim();
                        dr["PCB_MASTER_ID"] = sPCBmasterID;
                        dr["REFNO"] = sRefNo;
                        dr["DEFECT"] = "";
                        dr["REMARKS"] = txtObservation.Text.Trim();
                        if (drpStatus.Text == "OK")
                        {
                            dr["STATUS"] = "1";
                        }
                        else
                        {
                            dr["DEFECT"] = drpDefect.Text;
                            dr["STATUS"] = "0";
                        }
                        dtScanData.Rows.Add(dr);
                        dtScanData.AcceptChanges();
                        ViewState["Row"] = dtScanData;
                        gvPCBData.DataSource = ViewState["Row"];
                        gvPCBData.DataBind();
                    }
                }
                else
                {
                    dtScanData = new DataTable();
                    dtScanData.Columns.Add("PART_BARCODE", typeof(string));
                    dtScanData.Columns.Add("PCB_MASTER_ID", typeof(string));
                    dtScanData.Columns.Add("REFNO", typeof(string));
                    dtScanData.Columns.Add("DEFECT", typeof(string));
                    dtScanData.Columns.Add("REMARKS", typeof(string));
                    dtScanData.Columns.Add("STATUS", typeof(string));
                    DataRow dr1 = dtScanData.NewRow();
                    dr1 = dtScanData.NewRow();
                    dr1["PART_BARCODE"] = txtPCBID.Text.Trim();
                    dr1["PCB_MASTER_ID"] = sPCBmasterID;
                    dr1["REFNO"] = sRefNo;
                    dr1["REMARKS"] = txtObservation.Text;
                    if (drpDefect.SelectedIndex > 0)
                    {
                        dr1["DEFECT"] = drpDefect.Text;
                    }
                    else
                    {
                        dr1["DEFECT"] = "";
                    }
                    if (drpStatus.Text == "OK")
                    {
                        dr1["STATUS"] = "1";
                    }
                    else
                    {
                        dr1["STATUS"] = "0";
                    }
                    dtScanData.Rows.Add(dr1);
                    dtScanData.AcceptChanges();
                    ViewState["Row"] = dtScanData;
                    gvPCBData.DataSource = ViewState["Row"];
                    gvPCBData.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        protected void txtScanMachineID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                drpFGItemCode.Items.Clear();
                txtScanMachineID.Enabled = true;
                lblMachineName.Text = "";                
                txtPCBID.Text = string.Empty;
                lblModelNo.Text = "";
                drpstation.SelectedIndex = 0;
                txtObservation.Text = string.Empty;
                if (drpDefect.Items.Count > 0)
                {
                    drpDefect.SelectedIndex = 0;
                }
                ViewState["Row"] = null;
                dtScanData = new DataTable();
                gvPCBData.DataSource = null;
                gvPCBData.DataBind();
                lblRefNo.Text = "";
                lblScannedPCBCount.Text = "";
                lblTotalPCB.Text = "";
                txtPCBID.Enabled = true;
                txtPCBLotBarcode.Text = string.Empty;
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (string.IsNullOrEmpty(txtScanMachineID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Scan Machine", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtScanMachineID.Focus();
                    return;
                }
                blobj = new BL_FQA_Scanning();
                DataTable dt = blobj.ValidateMachine(txtScanMachineID.Text.Trim(), Session["SiteCode"].ToString(), Session["LINECODE"].ToString());
                if (dt.Rows.Count > 0)
                {
                    CommonHelper.ShowMessage("Valid machine", msgsuccess, CommonHelper.MessageType.Success.ToString());
                    lblMachineName.Text = dt.Rows[0][0].ToString();
                    lblModelNo.Text = dt.Rows[0][2].ToString();
                    BindFGItemCode();
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
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void drpFGItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindFQASampling();
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
                if (string.IsNullOrEmpty(txtScanMachineID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan machine barcode", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtScanMachineID.Focus();
                    txtScanMachineID.Text = string.Empty;
                    return;
                }
                else if (drpFGItemCode.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select fg item code", msginfo, CommonHelper.MessageType.Info.ToString());
                    drpFGItemCode.Focus(); txtPCBID.Text = string.Empty;
                    return;
                }
                else if (string.IsNullOrEmpty(txtPCBID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan PCB barcode", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtPCBID.Focus();
                    txtPCBID.Text = string.Empty;
                    return;
                }
                blobj = new BL_FQA_Scanning();
                DataTable dt = blobj.ValidatePCB(txtPCBID.Text.Trim(),
                    txtScanMachineID.Text.Trim(),
                   drpFGItemCode.Text, lblRefNo.Text, Session["SiteCode"].ToString(), Session["LINECODE"].ToString()
                    );
                if (dt.Rows.Count > 0)
                {
                    string sResult = dt.Rows[0][0].ToString();
                    PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, "FQA scanning:Barcode:" + txtPCBID.Text.Trim() 
                    + ", Machine ID :" + txtScanMachineID.Text.Trim()
                    + ", Ref No :" + lblRefNo.Text + ", FG Item Code :" + drpFGItemCode.Text
                    + ",Result : " + sResult
                    );
                    Message = sResult.Split('~')[1];
                    if (sResult.StartsWith("SUCCESS"))
                    {
                        CommonHelper.ShowMessage("PCB is ok", msgsuccess, CommonHelper.MessageType.Success.ToString());
                        string sBarcode = Message.Split('^')[0].ToString();
                        string sPCBMasterID1 = Message.Split('^')[1].ToString();
                        string sRefNo = Message.Split('^')[2].ToString();
                        lblTotalPCB.Text = Message.Split('^')[3].ToString();
                        lblLeftPCB.Text = Message.Split('^')[4].ToString();
                        lblWorkOrderNo.Text = Message.Split('^')[5].ToString();
                        hidWorkOrderNo.Value = lblWorkOrderNo.Text;

                        hidRefNo.Value = sRefNo;
                        hidPartBarcode.Value = sBarcode;
                        hidPCBmasterID.Value = sPCBMasterID1;
                        lblRefNo.Text = sRefNo;
                        txtPCBID.Enabled = false;
                        txtPCBID.CssClass = "form-control";
                        if (ViewState["Row"] != null)
                        {
                            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                            dtScanData = (DataTable)ViewState["Row"];
                            if (dtScanData.Rows.Count > 0)
                            {
                                bool BOXValidate = false;
                                BOXValidate = dtScanData.Select().ToList().Exists(row => row["PCB_MASTER_ID"].ToString().ToUpper() == sPCBMasterID1);
                                if (BOXValidate)
                                {
                                    CommonHelper.ShowMessage("Barcode already scanned.", msgerror, CommonHelper.MessageType.Error.ToString());
                                    txtPCBID.Text = string.Empty;
                                    txtPCBID.Focus();
                                    return;
                                }
                            }
                        }
                        txtPCBLotBarcode.Focus();
                        return;
                    }
                    else if (sResult.StartsWith("N~"))
                    {
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                        txtPCBID.Text = string.Empty;
                        txtPCBID.Focus();
                        return;
                    }
                    else if (sResult.StartsWith("ERROR~"))
                    {
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                        txtPCBID.Text = string.Empty;
                        txtPCBID.Focus();
                    }
                    else if (sResult.StartsWith("NOTFOUND~"))
                    {
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                        txtPCBID.Text = string.Empty;
                        txtPCBID.Focus();
                    }
                    else if (sResult.StartsWith("MACHINEFAILED~"))
                    {
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                        txtPCBID.Text = string.Empty;
                        txtPCBID.Focus();
                    }
                }
                else
                {
                    CommonHelper.ShowMessage("No result found for scanned barcode Please try again", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPCBID.Text = string.Empty;
                    txtPCBID.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void txtPCBLotBarcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (string.IsNullOrEmpty(txtScanMachineID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan machine barcode", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtScanMachineID.Focus();
                    txtScanMachineID.Text = string.Empty;
                    return;
                }
                else if (drpFGItemCode.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select fg item code", msginfo, CommonHelper.MessageType.Info.ToString());
                    drpFGItemCode.Focus(); txtPCBID.Text = string.Empty;
                    return;
                }
                else if (string.IsNullOrEmpty(txtPCBID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan PCB barcode", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtPCBID.Focus();
                    txtPCBID.Text = string.Empty;
                    return;
                }
                else if (string.IsNullOrEmpty(txtPCBLotBarcode.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan PCB barcode", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtPCBLotBarcode.Focus();
                    txtPCBLotBarcode.Text = string.Empty;
                    return;
                }
                blobj = new BL_FQA_Scanning();
                DataTable dt = blobj.ValidatePCB(txtPCBLotBarcode.Text.Trim(),
                    txtScanMachineID.Text.Trim(),
                   drpFGItemCode.Text, lblRefNo.Text, Session["SiteCode"].ToString(), Session["LINECODE"].ToString()
                    );
                if (dt.Rows.Count > 0)
                {
                    string sResult = dt.Rows[0][0].ToString();
                    Message = sResult.Split('~')[1];
                    if (sResult.StartsWith("SUCCESS"))
                    {
                        CommonHelper.ShowMessage("PCB is ok", msgsuccess, CommonHelper.MessageType.Success.ToString());
                        string sBarcode = Message.Split('^')[0].ToString();
                        string sPCBMasterID1 = Message.Split('^')[1].ToString();
                        string sRefNo = Message.Split('^')[2].ToString();
                        lblTotalPCB.Text = Message.Split('^')[3].ToString();

                        lblWorkOrderNo.Text = Message.Split('^')[5].ToString();
                        hidWorkOrderNo.Value = lblWorkOrderNo.Text;

                        hidRefNo.Value = sRefNo;                       
                        hidPartBarcode.Value = sBarcode;
                        if (lblRefNo.Text != hidRefNo.Value)
                        {
                            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                            CommonHelper.ShowMessage("Scanned barcode not found against scanned lot barcode.", msgerror, CommonHelper.MessageType.Error.ToString());
                            txtPCBLotBarcode.Text = string.Empty;
                            txtPCBLotBarcode.Focus();
                            return;
                        }
                        hidPCBmasterID.Value = sPCBMasterID1;
                        lblRefNo.Text = sRefNo;
                        if (ViewState["Row"] != null)
                        {
                            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                            dtScanData = (DataTable)ViewState["Row"];
                            if (dtScanData.Rows.Count > 0)
                            {
                                bool BOXValidate = false;
                                BOXValidate = dtScanData.Select().ToList().Exists(row => row["PART_BARCODE"].ToString().ToUpper() == sBarcode);
                                if (BOXValidate)
                                {
                                    CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                                    CommonHelper.ShowMessage("Barcode already scanned.", msgerror, CommonHelper.MessageType.Error.ToString());
                                    txtPCBLotBarcode.Text = string.Empty;
                                    txtPCBLotBarcode.Focus();
                                    return;
                                }
                            }
                        }
                        txtPCBLotBarcode.Focus();
                        return;
                    }
                    else if (sResult.StartsWith("N~"))
                    {
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                        txtPCBLotBarcode.Text = string.Empty;
                        txtPCBLotBarcode.Focus();
                        return;
                    }
                    else if (sResult.StartsWith("ERROR~"))
                    {
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                        txtPCBLotBarcode.Text = string.Empty;
                        txtPCBLotBarcode.Focus();
                    }
                    else if (sResult.StartsWith("NOTFOUND~"))
                    {
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                        txtPCBLotBarcode.Text = string.Empty;
                        txtPCBLotBarcode.Focus();
                    }
                    else if (sResult.StartsWith("MACHINEFAILED~"))
                    {
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                        txtPCBLotBarcode.Text = string.Empty;
                        txtPCBLotBarcode.Focus();
                    }
                }
                else
                {
                    CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                    CommonHelper.ShowMessage("No result found for scanned barcode Please try again", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPCBLotBarcode.Text = string.Empty;
                    txtPCBLotBarcode.Focus();
                }
            }
            catch (Exception ex)
            {
                txtPCBLotBarcode.Text = string.Empty;
                txtPCBLotBarcode.Focus();
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void drpPCBStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpStatus.SelectedIndex > 0)
                {
                    txtPCBLotBarcode.Text = hidPartBarcode.Value;
                    CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                    if (drpFGItemCode.SelectedIndex == 0)
                    {
                        CommonHelper.ShowMessage("Please select FG item Code", msgerror, CommonHelper.MessageType.Error.ToString());
                        drpStatus.SelectedIndex = 0;
                        return;
                    }
                    if (txtPCBID.Text.Trim() == string.Empty)
                    {
                        CommonHelper.ShowMessage("Please scan PCB barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                        drpStatus.SelectedIndex = 0;
                        txtPCBID.Focus();
                        return;
                    }
                    if (txtPCBLotBarcode.Text.Trim() == string.Empty)
                    {
                        CommonHelper.ShowMessage("Please scan lot sampling barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                        drpStatus.SelectedIndex = 0;
                        txtPCBLotBarcode.Focus();
                        return;
                    }
                    AddBoxPCBData(hidPartBarcode.Value, hidPCBmasterID.Value, hidRefNo.Value);
                    lblScannedPCBCount.Text = gvPCBData.Rows.Count.ToString();
                    txtPCBLotBarcode.Text = "";
                    txtPCBLotBarcode.Focus();
                    txtObservation.Text = string.Empty;
                    drpDefect.SelectedIndex = 0;
                    drpStatus.SelectedIndex = 0;
                    return;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
        }
        protected void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (string.IsNullOrEmpty(txtScanMachineID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan machine barcode", msgerror, CommonHelper.MessageType.Info.ToString());
                    txtScanMachineID.Focus();
                    txtScanMachineID.Text = string.Empty;
                    return;
                }
                if (drpFGItemCode.Items.Count == 0)
                {
                    CommonHelper.ShowMessage("Please select fg item code", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpFGItemCode.Focus(); txtPCBID.Text = string.Empty;
                    return;
                }
                if (drpFGItemCode.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select fg item code", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpFGItemCode.Focus(); txtPCBID.Text = string.Empty;
                    return;
                }
                txtPCBID.Text = hidPartBarcode.Value;
                if (string.IsNullOrEmpty(txtPCBID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan PCB barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPCBID.Focus();
                    return;
                }
                if (ViewState["Row"] == null)
                {
                    CommonHelper.ShowMessage("Please scan atleast one pcb", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPCBLotBarcode.Text = string.Empty;
                    txtPCBLotBarcode.Focus();
                    return;
                }
                int iCount = Convert.ToInt32(Convert.ToInt32(lblLeftPCB.Text) * Convert.ToInt32(lblSamplingRate.Text) / 100);
                if (iCount > gvPCBData.Rows.Count)
                {
                    CommonHelper.ShowMessage("Please scan pcb as per sampling rate", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPCBLotBarcode.Text = string.Empty;
                    txtPCBLotBarcode.Focus();
                    return;
                }
                DataTable dtData = (DataTable)ViewState["Row"];
                blobj = new BL_FQA_Scanning();
                DataTable dt = blobj.SaveFQAResult(txtPCBID.Text.Trim(), txtScanMachineID.Text.Trim()
                    , drpFGItemCode.Text, drpDefect.Text, drpstation.Text, txtObservation.Text, "1"
                    , lblRefNo.Text, dtData, "1", Session["SiteCode"].ToString(),
                    Session["LINECODE"].ToString(),Session["UserID"].ToString()
                    );
                if (dt.Rows.Count > 0)
                {
                    string sResult = dt.Rows[0][0].ToString();
                    if (sResult.StartsWith("SUCCESS~"))
                    {
                        if (drpstation.SelectedIndex > 0)
                        {
                            CommonHelper.ShowMessage("Part (" + txtPCBID.Text.Trim() + ") moved to repair station (" + drpstation.Text + ") for rework", msginfo, CommonHelper.MessageType.Error.ToString());
                        }
                        else
                        {
                            CommonHelper.ShowMessage(sResult.Split('~')[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                        }
                        txtPCBID.Text = string.Empty;
                        txtPCBID.Focus();
                        drpstation.SelectedIndex = 0;
                        txtObservation.Text = string.Empty;
                        if (drpDefect.Items.Count > 0)
                        {
                            drpDefect.SelectedIndex = 0;
                        }
                        ViewState["Row"] = null;
                        dtScanData = new DataTable();
                        gvPCBData.DataSource = null;
                        gvPCBData.DataBind();
                        lblRefNo.Text = "";
                        lblScannedPCBCount.Text = "";
                        lblTotalPCB.Text = "";
                        txtPCBID.Enabled = true;
                        txtPCBLotBarcode.Text = string.Empty;
                        lblWorkOrderNo.Text = string.Empty;
                    }
                    else
                    {
                        CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        txtPCBID.Text = string.Empty;
                        txtPCBID.Focus();
                        drpstation.SelectedIndex = 0;
                        txtObservation.Text = string.Empty;
                        if (drpDefect.Items.Count > 0)
                        {
                            drpDefect.SelectedIndex = 0;
                        }
                        ViewState["Row"] = null;
                        dtScanData = new DataTable();
                        gvPCBData.DataSource = null;
                        gvPCBData.DataBind();
                        lblRefNo.Text = "";
                        lblScannedPCBCount.Text = "";
                        lblTotalPCB.Text = "";
                        txtPCBID.Enabled = true;
                        txtPCBLotBarcode.Text = string.Empty;
                        lblWorkOrderNo.Text = string.Empty;
                    }
                }
                else
                {
                    CommonHelper.ShowMessage("No result found for scanned barcode Please try again", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPCBID.Text = string.Empty;
                    txtPCBID.Focus();
                }
            }
            catch (Exception ex)
            {
                if (ViewState["Row"] != null)
                {
                    gvPCBData.DataSource = ViewState["Row"];
                    gvPCBData.DataBind();
                }      
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (string.IsNullOrEmpty(txtScanMachineID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan machine barcode", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtScanMachineID.Focus();
                    txtScanMachineID.Text = string.Empty;
                    return;
                }
                if (drpFGItemCode.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select fg item code", msginfo, CommonHelper.MessageType.Info.ToString());
                    drpFGItemCode.Focus();
                    txtPCBID.Text = string.Empty;
                    return;
                }                
                if (drpstation.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select repair station", msginfo, CommonHelper.MessageType.Info.ToString());
                    drpstation.Focus();
                    return;
                }
                txtPCBID.Text = hidPartBarcode.Value;
                int iCount = Convert.ToInt32(Convert.ToInt32(lblLeftPCB.Text) * Convert.ToInt32(lblSamplingRate.Text) / 100);
                if (iCount > gvPCBData.Rows.Count)
                {
                    CommonHelper.ShowMessage("Please scan pcb as per sampling rate", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPCBLotBarcode.Text = string.Empty;
                    txtPCBLotBarcode.Focus();
                    return;
                }
                DataTable dtData = (DataTable)ViewState["Row"];
                blobj = new BL_FQA_Scanning();
                DataTable dt = blobj.SaveFQAResult(txtPCBID.Text.Trim(), txtScanMachineID.Text.Trim()
                    , drpFGItemCode.Text, drpDefect.Text, drpstation.Text, txtObservation.Text, "2"
                   , lblRefNo.Text, dtData, "1", Session["SiteCode"].ToString(), Session["LINECODE"].ToString()
                   , Session["UserID"].ToString());
                if (dt.Rows.Count > 0)
                {
                    string sResult = dt.Rows[0][0].ToString();
                    if (sResult.StartsWith("SUCCESS~"))
                    {
                        if (drpstation.SelectedIndex > 0)
                        {
                            CommonHelper.ShowMessage("Part (" + txtPCBID.Text.Trim() + ") moved to repair station (" + drpstation.Text + ") for rework", msginfo, CommonHelper.MessageType.Error.ToString());
                        }
                        else
                        {
                            CommonHelper.ShowMessage(sResult.Split('~')[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                        }
                        txtPCBID.Text = string.Empty;
                        txtPCBID.Focus();
                        drpstation.SelectedIndex = 0;
                        txtObservation.Text = string.Empty;
                        if (drpDefect.Items.Count > 0)
                        {
                            drpDefect.SelectedIndex = 0;
                        }
                        ViewState["Row"] = null;
                        dtScanData = new DataTable();
                        gvPCBData.DataSource = null;
                        gvPCBData.DataBind();
                        lblRefNo.Text = "";
                        lblScannedPCBCount.Text = "";
                        lblTotalPCB.Text = "";
                        txtPCBID.Enabled = true;
                        txtPCBLotBarcode.Text = string.Empty;
                        lblWorkOrderNo.Text = string.Empty;
                        lblLeftPCB.Text = "";
                    }
                    else
                    {
                        CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        txtPCBID.Text = string.Empty;
                        txtPCBID.Focus();
                        drpstation.SelectedIndex = 0;
                        txtObservation.Text = string.Empty;
                        if (drpDefect.Items.Count > 0)
                        {
                            drpDefect.SelectedIndex = 0;
                        }
                        ViewState["Row"] = null;
                        dtScanData = new DataTable();
                        gvPCBData.DataSource = null;
                        gvPCBData.DataBind();
                        lblRefNo.Text = "";
                        lblScannedPCBCount.Text = "";
                        lblTotalPCB.Text = "";
                        txtPCBID.Enabled = true;
                        txtPCBLotBarcode.Text = string.Empty;
                        lblWorkOrderNo.Text = string.Empty;
                        lblLeftPCB.Text = "";
                    }
                }
                else
                {
                    CommonHelper.ShowMessage("No result found for scanned barcode Please try again", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPCBID.Text = string.Empty;
                    txtPCBID.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
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
                lblModelNo.Text = "";
                drpstation.SelectedIndex = 0;
                txtObservation.Text = string.Empty;
                if (drpDefect.Items.Count > 0)
                {
                    drpDefect.SelectedIndex = 0;
                }
                ViewState["Row"] = null;
                dtScanData = new DataTable();
                gvPCBData.DataSource = null;
                gvPCBData.DataBind();
                lblRefNo.Text = "";
                lblScannedPCBCount.Text = "";
                lblTotalPCB.Text = "";
                txtPCBID.Enabled = true;
                txtPCBLotBarcode.Text = string.Empty;
                lblWorkOrderNo.Text = string.Empty;
                hidWorkOrderNo.Value = "";
                lblLeftPCB.Text = "";
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }        
    }
}