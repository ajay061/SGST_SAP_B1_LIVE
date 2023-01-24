using Common;
using System;
using BusinessLayer.WIP;
using System.Data;

namespace SGST.INE.WIP
{
    public partial class WIPBosaSNMapping : System.Web.UI.Page
    {
        string Message = "";
        BL_WIPBosaSNMapping blobj = new BL_WIPBosaSNMapping();
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Signin/v1/Login.aspx?Session=Null");
            }
        }
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
                    lblLineCode.Text = Session["LINECODE"].ToString();
                }
                catch (Exception ex)
                {
                    CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                }
            }
        }
        private void BindFGItemCode()
        {
            try
            {
                drpFGItemCode.Items.Clear();
                txtPCBID.Text = string.Empty;
                lblCount.Text = "0";
                blobj = new BL_WIPBosaSNMapping();
                if (txtScanMachineID.Text.Length > 0)
                {
                    string sResult = string.Empty;
                    DataTable dtFGItemCode = blobj.BindFGItemCode(txtScanMachineID.Text.Trim(),
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
                txtBosaSNBarcode.Text = string.Empty;
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
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
                else if (string.IsNullOrEmpty(txtPCBID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan PCB barcode", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtPCBID.Focus();
                    txtPCBID.Text = string.Empty;
                    return;
                }
                blobj = new BL_WIPBosaSNMapping();               
                string sResult = string.Empty;             
                DataTable dtResult = blobj.SaveQualtiyData(
                    txtPCBID.Text.Trim(), txtScanMachineID.Text.Trim(), drpFGItemCode.Text,
                    Session["SiteCode"].ToString(), Session["LINECODE"].ToString(), Session["UserID"].ToString(),
                    txtBosaSNBarcode.Text.Trim());              
                if (dtResult.Rows.Count > 0)
                {
                    sResult = dtResult.Rows[0][0].ToString();
                    Message = sResult.Split('~')[1];
                }
                if (sResult.StartsWith("SUCCESS"))
                {
                    CommonHelper.ShowMessage(Message, msgsuccess, CommonHelper.MessageType.Success.ToString());
                    lblCount.Text = Convert.ToString(Convert.ToInt32(lblCount.Text) + 1);
                    txtBosaSNBarcode.Text = string.Empty;
                    txtPCBID.Text = string.Empty;
                    txtBosaSNBarcode.Focus();
                    return;
                }
                else if (sResult.StartsWith("N~"))
                {
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBosaSNBarcode.Text = string.Empty;
                    txtPCBID.Text = string.Empty;
                    txtBosaSNBarcode.Focus();
                    return;
                }
                else if (sResult.StartsWith("ERROR~"))
                {
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBosaSNBarcode.Text = string.Empty;
                    txtPCBID.Text = string.Empty;
                    txtBosaSNBarcode.Focus();
                }
                else if (sResult.StartsWith("NOTFOUND~"))
                {
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBosaSNBarcode.Text = string.Empty;
                    txtPCBID.Text = string.Empty;
                    txtBosaSNBarcode.Focus();
                }
                else if (sResult.StartsWith("MACHINEFAILED~"))
                {
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBosaSNBarcode.Text = string.Empty;
                    txtPCBID.Text = string.Empty;
                    txtBosaSNBarcode.Focus();
                }
            }
            catch (Exception ex)
            {
                txtBosaSNBarcode.Text = string.Empty;
                txtPCBID.Text = string.Empty;
                txtBosaSNBarcode.Focus();
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void txtBosaSNBarcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
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
                else if (string.IsNullOrEmpty(txtBosaSNBarcode.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan bosa sn barcode", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtBosaSNBarcode.Focus();
                    txtPCBID.Text = string.Empty;
                    return;
                }
                blobj = new BL_WIPBosaSNMapping();
                DataTable dt = blobj.ValidateBosaBarcode(txtBosaSNBarcode.Text.Trim());
                if (dt.Rows.Count > 0)
                {
                    string sResult = dt.Rows[0][0].ToString();
                    if (sResult.StartsWith("SUCCESS~"))
                    {
                        CommonHelper.ShowMessage("Valid Bosa SN", msgsuccess, CommonHelper.MessageType.Success.ToString());
                        txtPCBID.Focus();
                    }
                    else
                    {
                        CommonHelper.ShowMessage(sResult.Split('~')[1].ToString(), msgerror, CommonHelper.MessageType.Error.ToString());
                        txtPCBID.Text = string.Empty;
                        txtBosaSNBarcode.Text = string.Empty;
                        txtBosaSNBarcode.Focus();
                        return;
                    }
                }
                else
                {
                    CommonHelper.ShowMessage("Invalid Barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPCBID.Text = string.Empty;
                    txtBosaSNBarcode.Text = string.Empty;
                    txtBosaSNBarcode.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
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
                blobj = new BL_WIPBosaSNMapping();
                DataTable dt = blobj.ValidateMachine(txtScanMachineID.Text.Trim(), Session["SiteCode"].ToString()
                    , Session["LINECODE"].ToString()
                    );
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
                txtScanMachineID.Text = string.Empty;
                txtScanMachineID.Focus();
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}