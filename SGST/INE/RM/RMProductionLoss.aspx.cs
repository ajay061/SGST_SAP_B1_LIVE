using Common;
using System;
using System.Data;
using BusinessLayer.RM;

namespace SGST.INE.RM
{
    public partial class RMProductionLoss : System.Web.UI.Page
    {
        string Message = "";
        BL_ProductionLoss objProductionLoss = new BL_ProductionLoss();
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
                    string _strRights = CommonHelper.GetRights("PRODUCTION LOSS", (DataTable)Session["USER_RIGHTS"]);
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
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                ValidateBarcodeFromWIPIssue();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void ValidateBarcodeFromWIPIssue()
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (string.IsNullOrEmpty(txtBarcode.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan barcode.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBarcode.Focus();
                    return;
                }
                objProductionLoss = new BL_ProductionLoss();
                DataTable dt = new DataTable();
                string _Result = objProductionLoss.ValidateBarcodeFromWIPIssue(txtBarcode.Text, hidWorkOrderno.Value
                     , Session["SiteCode"].ToString()
                    );
                Message = _Result;
                string[] msg = Message.Split('~');
                string Msgs = msg[0];
                if (msg.Length > 0)
                {
                    if (msg[0].StartsWith("N") || msg[0].StartsWith("ERROR"))
                    {
                        CommonHelper.ShowMessage(msg[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        txtBarcode.Text = "";
                        txtBarcode.Focus();
                        txtQuantity.Text = string.Empty;
                        hidInvQty.Value = "";
                    }
                    else
                    {
                        string[] messsage = msg[1].Split('^');
                        txtQuantity.Text = messsage[1];
                        hidInvQty.Value = messsage[1];
                        hidPartCode.Value = messsage[2].ToString();
                        txtWorkOrderNo.Text = messsage[3].ToString();
                        hidWorkOrderno.Value = messsage[3].ToString();
                        txtQnt.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void PrintBarcodes()
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                objProductionLoss = new BL_ProductionLoss();
                DataTable dt = new DataTable();
                string _qty = "";
                string _Part_Barcode = "";
                _Part_Barcode = txtBarcode.Text.Trim();
                _qty = txtQnt.Text.Trim();
                Message = objProductionLoss.PrintBarcode(_Part_Barcode,
              Session["userid"].ToString(), Convert.ToDecimal(_qty), hidPartCode.Value.Trim(), txtWorkOrderNo.Text.Trim()
               , Session["SiteCode"].ToString(), Session["LINECODE"].ToString()                   
              );
                if (Message.Length > 0)
                {
                    string[] msg = Message.Split('~');
                    string Msgs = msg[0];
                    if (Message.StartsWith("N~") || Message.StartsWith("ERROR~"))
                    {
                        CommonHelper.ShowMessage(msg[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        txtBarcode.Text = "";
                        txtQuantity.Text = "";
                        txtWorkOrderNo.Text = "";
                        txtQnt.Text = "";
                    }
                    else
                    {
                        CommonHelper.ShowMessage(msg[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                        txtBarcode.Text = "";
                        txtQuantity.Text = "";
                        txtWorkOrderNo.Text = "";
                        txtQnt.Text = "";
                    }
                }
                else
                {
                    txtBarcode.Text = "";
                    txtQuantity.Text = "";
                    txtWorkOrderNo.Text = "";
                    txtQnt.Text = "";
                    CommonHelper.ShowMessage("No result found, Please try again", msgerror, CommonHelper.MessageType.Error.ToString());
                }
            }
            catch (Exception ex)
            {
                txtBarcode.Text = "";
                txtQuantity.Text = "";
                txtWorkOrderNo.Text = "";
                txtQnt.Text = "";
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (txtBarcode.Text.Trim() == "")
                {
                    CommonHelper.ShowMessage("Please scan barcode.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBarcode.Focus();
                    return;
                }
                if (txtQuantity.Text == "")
                {
                    CommonHelper.ShowMessage("Available quantity is not found against scanned barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtQuantity.Focus();
                    return;
                }
                if (txtQnt.Text.Trim() == "")
                {
                    CommonHelper.ShowMessage("Please enter quantity", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtQnt.Focus();
                    return;
                }
                if (Convert.ToDecimal(txtQnt.Text) == 0)
                {
                    CommonHelper.ShowMessage("Enter quantity can not be zero, Please enter valid quantity", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtQnt.Focus();
                    return;
                }
                if (Convert.ToDecimal(txtQuantity.Text) < Convert.ToDecimal(txtQnt.Text))
                {
                    CommonHelper.ShowMessage("Return quantity always be lesser than available quantity", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtQnt.Text = string.Empty;
                    txtQnt.Focus();
                    return;
                }
                PrintBarcodes();
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
                txtBarcode.Text = string.Empty;
                txtQuantity.Text = "";
                txtQnt.Text = "";
                txtBarcode.Focus();
                txtWorkOrderNo.Text = "";
                hidWorkOrderno.Value = "";
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}