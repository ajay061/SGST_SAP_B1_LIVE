using Common;
using System;
using System.Text.RegularExpressions;
using BusinessLayer;
using System.Data;

namespace SGST.INE.Operation
{
    public partial class PhysicalStock : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["USERNAME"] == null)
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
                btnsubmit.Visible = false;
                txtScanBarcode.Attributes["OnTextChanged"] = "if ( IsValid(this) == false ) return;";
            }
        }
        public void btnInsert(object sender, EventArgs e)//This method is use for insert physical stock data
        {
            //hiding messages
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                //validate quantity
                if (ValidateQuantity() == false)
                {
                    //Check stock area selected or not
                    if (drpStockArea.SelectedIndex > 0)
                    {
                        BL_PhysicalStock dlobj = new BL_PhysicalStock();
                        string sResult = string.Empty;
                        sResult = string.Empty;
                        sResult = dlobj.SavePhysicalStock(Convert.ToDecimal(txtQuantity.Text.Trim()),
                            txtScanBarcode.Text.Trim(), drpStockArea.Text.Trim(),
                            Session["UserID"].ToString());
                        if (sResult.Length > 0)
                        {
                            if (sResult.StartsWith("ERROR~"))
                            {
                                CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                                txtScanBarcode.Text = "";
                                txtScanBarcode.Focus();
                            }
                            if (sResult.StartsWith("N~"))
                            {
                                CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                                txtScanBarcode.Text = "";
                                txtScanBarcode.Focus();
                            }
                            else
                            {
                                CommonHelper.ShowMessage(sResult.Split('~')[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                                drpStockArea.Focus();
                                txtScanBarcode.Text = "";
                                txtScanBarcode.Focus();
                                txtQuantity.Text = "";                               
                                txtQuantity.ReadOnly = true;
                                chkManual.Checked = false;
                                btnsubmit.Visible = false;
                                txtCounter.Text = Convert.ToString(Convert.ToInt32(txtCounter.Text) + 1);
                            }
                        }
                        else
                        {
                            CommonHelper.ShowMessage("No result found", msgerror, CommonHelper.MessageType.Error.ToString());
                            txtQuantity.Text = "";
                            txtScanBarcode.Text = "";
                        }
                    }
                }
                btnsubmit.Visible = false;
            }
            catch (Exception ex)
            {
                CommonHelper.ShowMessage(ex.Message, msgerror, CommonHelper.MessageType.Error.ToString());
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
        }

        protected void txtScanBarcode_TextChanged(object sender, EventArgs e)//For checking controls are empty or not and retrive information base on stock area and scan barcode
        {
            //hide message
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                //check stock is selected or not if not then display message
                if (drpStockArea.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Stock area is not selected", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpStockArea.Focus();
                    return;
                }
                //check barcode is selected or not  if not then display message
                if (string.IsNullOrEmpty(txtScanBarcode.Text) == true)
                {
                    CommonHelper.ShowMessage("Scan item barcode is empty", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtScanBarcode.Focus();
                    return;
                }
                btnsubmit.Visible = false;
                BL_PhysicalStock dlobj = new BL_PhysicalStock();
                DataTable dt = dlobj.GetPhysicalStock(txtScanBarcode.Text.Trim(), drpStockArea.Text.Trim());
                if (dt.Rows.Count > 0)
                {
                    decimal dQty = Convert.ToDecimal(dt.Rows[0]["APPROVED_QTY"].ToString());
                    if (Convert.ToDecimal(dQty) == 0)
                    {
                        CommonHelper.ShowMessage("Qty not found in inventory area for scanned barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                        btnsubmit.Visible = false;
                        return;
                    }
                    else
                    {
                        //if approved quality is not zero then submit button display and quanity will be appear on quantity text box
                        if (dQty.ToString() != null)
                        {
                            txtQuantity.Text = Convert.ToString(dQty);
                            btnsubmit.Visible = true;
                        }
                        //if approved quality is null then display erro message
                        else if (dQty.ToString() == null)
                        {
                            CommonHelper.ShowMessage("Qty not found in inventory area for scanned barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                            btnsubmit.Visible = false;
                            return;
                        }
                    }
                }
                else
                {
                    CommonHelper.ShowMessage("No result found against scanned barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtScanBarcode.Text = "";
                    txtScanBarcode.Focus();
                    btnsubmit.Visible = false;
                }
                drpStockArea.Focus();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowMessage(ex.Message, msgerror, CommonHelper.MessageType.Error.ToString());
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
        }

        public bool ValidateQuantity()// for validate Quantity textbox 
        {
            //for hiding messages
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                //check quantity textbox is empty if yes then display message
                if (string.IsNullOrEmpty(txtQuantity.Text) == true)
                {
                    CommonHelper.ShowMessage("Quantity is empty", msgerror, CommonHelper.MessageType.Error.ToString());
                    return true;
                }
                if (!Regex.IsMatch(txtQuantity.Text, @"^(\d*\.)?\d+$"))
                {
                    CommonHelper.ShowMessage("Quantity should be numeric", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtQuantity.Text = "";
                    txtQuantity.Focus();
                    txtScanBarcode.Text = string.Empty;
                    txtScanBarcode.Focus();
                    return true;
                }
            }
            catch (Exception ex)
            {
                //for display exception messages
                CommonHelper.ShowMessage(ex.Message.ToString(), msgerror, CommonHelper.MessageType.Error.ToString());
                return true;
            }
            return false;
        }

        protected void chkManual_CheckedChanged(object sender, EventArgs e)//This event is for checkbox if checkbox select then quality text enable other wise not
        {
            //for hiding messages
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                //if user clicked check box then user can edit quantity else not
                if (chkManual.Checked)
                {
                    txtQuantity.ReadOnly = false;
                }
                else
                {
                    txtQuantity.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowMessage(ex.Message, msgerror, CommonHelper.MessageType.Error.ToString());
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
        }

        protected void drpStockArea_SelectedIndexChanged(object sender, EventArgs e)//This event is related to Stock Area
        {
            //if user change value of stock area then all controls will be reset
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                txtScanBarcode.Text = "";
                txtQuantity.Text = "";
                btnsubmit.Visible = false;
            }
            catch (Exception ex)
            {
                CommonHelper.ShowMessage(ex.Message, msgerror, CommonHelper.MessageType.Error.ToString());
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)//This Event is for reset controls
        {
            //for hide messages
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                //reset all controls
                txtScanBarcode.Text = "";
                txtQuantity.Text = "";
                drpStockArea.SelectedIndex = 0;
                txtQuantity.ReadOnly = true;
                chkManual.Checked = false;
                btnsubmit.Visible = false;
                txtCounter.Text = "0";

            }
            catch (Exception ex)
            {
                CommonHelper.ShowMessage(ex.Message, msgerror, CommonHelper.MessageType.Error.ToString());
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
        }
    }
}