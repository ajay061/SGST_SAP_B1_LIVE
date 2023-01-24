using Common;
using System;
using BusinessLayer.MES.QUALITY;
using System.Data;
using PL;
using BusinessLayer;

namespace SGST.INE.MOB
{
    public partial class mobLTAssembly : System.Web.UI.Page
    {
        DataTable dtScanData;
        BL_mobLifeTesting blobj = new BL_mobLifeTesting();
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
                string _strRights = CommonHelper.GetRights("LIFE TESTING", (DataTable)Session["USER_RIGHTS"]);
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
                    dvOut.Visible = false;
                }
                catch (Exception ex)
                {
                    CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                }
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
                txtobservation.Text = string.Empty;
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
                blobj = new BL_mobLifeTesting();
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
        private void BindFGItemCode()
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                drpFGItemCode.Items.Clear();
                txtPCBID.Text = string.Empty;
                BL_mobLifeTesting blobj = new BL_mobLifeTesting();
                DataTable dtFGItemCode = blobj.BindFGItemCode(txtScanMachineID.Text.Trim(), Session["SiteCode"].ToString(), Session["LINECODE"].ToString());
                if (dtFGItemCode.Rows.Count > 0)
                {
                    clsCommon.FillMultiColumnsCombo(drpFGItemCode, dtFGItemCode, true);
                    drpFGItemCode.SelectedIndex = 0;
                    drpFGItemCode.Focus();
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
                //lblSamplingRate.Text = "0";
                string sResult = string.Empty;
                blobj = new BL_mobLifeTesting();
                DataTable dtStationID = blobj.BindFQASampling(drpFGItemCode.Text, Session["SiteCode"].ToString());
                if (dtStationID.Rows.Count > 0)
                {
                    lblLotSize.Text = dtStationID.Rows[0][1].ToString();
                }
                else
                {

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

        private void BindStationID()
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                drpstation.Items.Clear();
                BL_mobLifeTesting blobj = new BL_mobLifeTesting();
                DataTable dtStationID = blobj.BindReworkstation(Session["SiteCode"].ToString());
                if (dtStationID.Rows.Count > 0)
                {
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
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                drpDefect.Items.Clear();
                BL_mobLifeTesting blobj = new BL_mobLifeTesting();
                DataTable dtStationID = blobj.BindDefect(Session["SiteCode"].ToString());
                if (dtStationID.Rows.Count > 0)
                {
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
        public void GetData()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (drpFGItemCode.SelectedIndex > 0)
                {
                    lblModelNo.Text = string.Empty;
                    BL_MobCommon obj = new BL_MobCommon();
                    PL_Printing plobj = new PL_Printing();
                    plobj.sModelName = drpFGItemCode.SelectedValue.ToString();
                    DataTable dt = new DataTable();
                    dt = obj.DisplayedData(plobj);
                    if (dt.Rows.Count > 0)
                    {
                        lblModelNo.Text = dt.Rows[0]["MODEL_CODE"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }


        protected void txtPCBID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (string.IsNullOrEmpty(txtScanMachineID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan machine id", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtScanMachineID.Focus();
                    txtOutPCBLotBarcode.Text = String.Empty;
                    return;
                }
                if (drpFGItemCode.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select fg item code", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpFGItemCode.Focus();
                    txtPCBID.Text = String.Empty;
                    return;
                }
                if (drpType.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select type", msginfo, CommonHelper.MessageType.Info.ToString());
                    drpType.Focus();
                    txtPCBID.Text = String.Empty;
                    return;
                }
                if (string.IsNullOrEmpty(txtPCBID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPCBID.Focus();
                    return;
                }
                BL_mobLifeTesting blobj = new BL_mobLifeTesting();
                PL_mobLifeTesting plobj = new PL_mobLifeTesting();
                plobj.sFGItemCode = drpFGItemCode.SelectedItem.ToString();
                plobj.sActionType = drpType.Text;
                plobj.sBarcode = txtPCBID.Text.Trim();
                plobj.sSiteCode = Session["SiteCode"].ToString();
                plobj.sScannedBy = Session["UserID"].ToString();
                plobj.sLineCode = Session["LINECODE"].ToString();
                plobj.sMachineID = txtScanMachineID.Text.Trim();
                DataTable dt = blobj.blValidatePCBCreateLot(plobj);
                if (dt.Rows.Count > 0)
                {
                    string Message = dt.Rows[0][0].ToString();
                    if (Message.StartsWith("SUCCESS~"))
                    {
                        CommonHelper.ShowMessage(Message.Split('~')[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                        txtPCBID.Text = "";
                        txtPCBID.Focus();
                        lblLotRefNo.Text = Message.Split('$')[1];
                    }
                    else
                    {
                        CommonHelper.ShowMessage(Message.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        txtPCBID.Text = "";
                        txtPCBID.Focus();
                        lblLotRefNo.Text = "";
                    }
                }
                else
                {
                    CommonHelper.ShowMessage("No result found for scanned barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPCBID.Text = "";
                    txtPCBID.Focus();
                }
            }
            catch (Exception ex)
            {
                txtPCBID.Text = "";
                txtPCBID.Focus();
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void sAction(string sType)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (drpType.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select type", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpType.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtPCBID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan PCB barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPCBID.Focus(); return;
                }
                if (drpType.SelectedIndex == 2 && sType == "2")
                {
                    if (drpstation.SelectedIndex <= 0)
                    {
                        CommonHelper.ShowMessage("Please select station", msgerror, CommonHelper.MessageType.Error.ToString());
                        drpstation.Focus(); return;
                    }
                    if (drpDefect.SelectedIndex <= 0)
                    {
                        CommonHelper.ShowMessage("Please select defect", msgerror, CommonHelper.MessageType.Error.ToString());
                        drpstation.Focus(); return;
                    }
                    if (string.IsNullOrEmpty(txtobservation.Text.Trim()))
                    {
                        CommonHelper.ShowMessage("Please enter observation", msgerror, CommonHelper.MessageType.Error.ToString());
                        txtobservation.Focus(); return;
                    }
                }
                BL_mobLifeTesting blobj = new BL_mobLifeTesting();
                PL_mobLifeTesting plobj = new PL_mobLifeTesting();
                plobj.sFGItemCode = drpFGItemCode.SelectedItem.ToString();
                plobj.sActionType = drpType.Text;
                plobj.sBarcode = txtPCBID.Text.Trim();
                if (drpstation.SelectedIndex > 0)
                {
                    plobj.sReworkStation = drpstation.Text;
                }
                if (drpDefect.SelectedIndex > 0)
                {
                    plobj.sDefect = drpDefect.Text;
                }
                plobj.sObservation = txtobservation.Text.Trim();
                plobj.sFinalResult = sType;
                DataTable dt = blobj.SaveData(plobj);
                if (dt.Rows.Count > 0)
                {
                    string sResult = string.Empty;
                    sResult = dt.Rows[0][0].ToString();
                    string Message = sResult.Split('~')[1].ToString();
                    if (sResult.ToUpper().StartsWith("SUCCESS~"))
                    {
                        CommonHelper.ShowMessage(Message, msgsuccess, CommonHelper.MessageType.Success.ToString());
                        txtPCBID.Text = string.Empty;
                        txtPCBID.Focus();
                        if (drpstation.Items.Count > 0)
                        {
                            drpstation.SelectedIndex = 0;
                        }
                        if (drpDefect.Items.Count > 0)
                        {
                            drpDefect.SelectedIndex = 0;
                        }
                        txtobservation.Text = string.Empty;
                        txtPCBID.Text = string.Empty;
                        gvPCBData.DataSource = null;
                        gvPCBData.DataBind();
                        drpType.SelectedIndex = 0;
                        dvOut.Visible = false;
                        return;
                    }
                    else if (sResult.ToUpper().StartsWith("ERROR~"))
                    {
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    else if (sResult.ToUpper().StartsWith("N~"))
                    {
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                }
                else
                {
                    CommonHelper.ShowMessage("No result found for scanned barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                sAction("1");
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
            try
            {
                drpFGItemCode.Items.Clear();
                lblLotRefNo.Text = "";
                lblLotSize.Text = "";
                txtScanMachineID.Text = "";
                txtPCBID.Text = string.Empty;
                txtOutPCBLotBarcode.Text = string.Empty;
                drpType.SelectedIndex = 0;
                dvOut.Visible = false;
                dvIn.Visible = true;
                lblMachineName.Text = "";
                lblModelNo.Text = "";
                txtobservation.Text = string.Empty;
                gvPCBData.DataSource = null;
                gvPCBData.DataBind();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                sAction("2");
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
        }

        protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dvOut.Visible = false;
                dvIn.Visible = true;
                if (drpstation.Items.Count > 0)
                {
                    drpstation.SelectedIndex = 0;
                }
                if (drpDefect.Items.Count > 0)
                {
                    drpDefect.SelectedIndex = 0;
                }
                txtobservation.Text = string.Empty;
                txtPCBID.Text = string.Empty;
                if (drpType.SelectedIndex == 2)
                {
                    dvIn.Visible = false;
                    dvOut.Visible = true;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }



        protected void txtOutPCBLotBarcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (string.IsNullOrEmpty(txtScanMachineID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan machine id", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtScanMachineID.Focus();
                    txtOutPCBLotBarcode.Text = String.Empty;
                    return;
                }
                if (drpFGItemCode.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select fg item code", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpFGItemCode.Focus();
                    txtOutPCBLotBarcode.Text = String.Empty;
                    return;
                }
                if (drpType.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select type", msginfo, CommonHelper.MessageType.Info.ToString());
                    drpType.Focus();
                    txtOutPCBLotBarcode.Text = String.Empty;
                    return;
                }
                if (string.IsNullOrEmpty(txtOutPCBLotBarcode.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtOutPCBLotBarcode.Focus();
                    return;
                }
                BL_mobLifeTesting blobj = new BL_mobLifeTesting();
                PL_mobLifeTesting plobj = new PL_mobLifeTesting();
                plobj.sFGItemCode = drpFGItemCode.SelectedItem.ToString();
                plobj.sActionType = drpType.Text;
                plobj.sBarcode = txtOutPCBLotBarcode.Text.Trim();
                plobj.sSiteCode = Session["SiteCode"].ToString();
                plobj.sScannedBy = Session["UserID"].ToString();
                plobj.sLineCode = Session["LINECODE"].ToString();
                plobj.sMachineID = txtScanMachineID.Text.Trim();
                DataTable dt = blobj.blValidateOutLot(plobj);
                if (dt.Rows.Count > 0)
                {
                    string Message = dt.Rows[0][0].ToString();
                    if (Message.StartsWith("SUCCESS~"))
                    {
                        CommonHelper.ShowMessage(Message.Split('~')[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                        txtOutPCBLotBarcode.Text = "";
                        txtOutPCBLotBarcode.Focus();
                    }
                    else
                    {
                        CommonHelper.ShowMessage(Message.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        txtOutPCBLotBarcode.Text = "";
                        txtOutPCBLotBarcode.Focus();
                    }
                }
                else
                {
                    CommonHelper.ShowMessage("No result found for scanned barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtOutPCBLotBarcode.Text = "";
                    txtOutPCBLotBarcode.Focus();
                }
            }
            catch (Exception ex)
            {
                txtOutPCBLotBarcode.Text = "";
                txtOutPCBLotBarcode.Focus();
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void txtPCBLotBarcode_TextChanged(object sender, EventArgs e)
        {

        }

        protected void drpStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}