using Common;
using System;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Data;
using BusinessLayer;

namespace SGST.INE.RM
{
    public partial class RMPrinting : System.Web.UI.Page
    {
        string Message = "";
        BL_RM_Printing blobj = new BL_RM_Printing();
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
            if (Session["usertype"].ToString() != "ADMIN")
            {
                string _strRights = CommonHelper.GetRights("RM PRINTING", (DataTable)Session["USER_RIGHTS"]);
                CommonHelper._strRights = _strRights.Split('^');
                if (CommonHelper._strRights[0] == "0")  //Check view rights
                {
                    Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                }
            }
            drpMFR.CssClass = "form-control";
            if (!IsPostBack)
            {
                try
                {
                    dvPrintergrup.Visible = true;
                    if (PCommon.sUseNetworkPrinter == "1")
                    {
                        getprinterlist();
                    }
                    else
                    {
                        dvPrintergrup.Visible = false;
                    }
                    BindGRPODate();
                }
                catch (Exception ex)
                {
                    CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                }
            }
        }
        private void getprinterlist()
        {
            try
            {
                BL_Common blCommonobj = new BL_Common();
                DataTable dt = blCommonobj.BINDPRINTER("RM");
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(drpPrinterName, dt, true);
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        private void ResetData()
        {
            txtBatchNo.Text = string.Empty;
            txtMFGDate.Text = string.Empty;
            txtExpiryDate.Text = string.Empty;
            txtInvoiceNumber.Text = "";
            txtMFGDate.Text = "";
            txtExpiryDate.Text = "";
            txtDealerName.Text = "";
            txtComDescription.Text = "";
            txtNoOfPrints.Text = "";
            txtRemainingQty.Text = "";
            txtMFGDate.Text = "";
            txtQty.Text = "";
            txtpackSize.Text = string.Empty;
            drpLF.SelectedIndex = 0;
            txtMSLValue.Text = string.Empty;
            hidMFGDate.Value = "";
            hidEXPDATE.Value = "";
            lblMake.Text = string.Empty;
        }
        private void BindGRPODate()
        {
            try
            {
                drpGRPODate.Items.Clear();
                txtMFGDate.Text = "";
                txtExpiryDate.Text = "";
                drpReceiptNo.Items.Clear();
                drpItemCode.Items.Clear();
                btnPrint.Visible = true;
                drpMFR.Items.Clear();
                ResetData();
                blobj = new BL_RM_Printing();
                DataTable dtGRN = blobj.BindGRPODate(Session["SiteCode"].ToString());
                if (dtGRN.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(drpGRPODate, dtGRN, true);
                    drpGRPODate.Focus();
                }
                else
                {
                    CommonHelper.ShowMessage("No data found.", msginfo, CommonHelper.MessageType.Info.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void BindPO()
        {
            try
            {
                txtMFGDate.Text = "";
                txtExpiryDate.Text = "";
                drpReceiptNo.Items.Clear();
                drpItemCode.Items.Clear();
                btnPrint.Visible = true;
                drpMFR.Items.Clear();
                ResetData();
                if (drpGRPODate.SelectedIndex > 0)
                {
                    blobj = new BL_RM_Printing();
                    DataTable dtGRN = blobj.BINDGRN(drpGRPODate.Text, Session["SiteCode"].ToString());
                    if (dtGRN.Rows.Count > 0)
                    {
                        clsCommon.FillComboBox(drpReceiptNo, dtGRN, true);
                        drpReceiptNo.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void BindPartCode()
        {
            try
            {
                txtMFGDate.Text = "";
                txtExpiryDate.Text = "";
                drpItemCode.Items.Clear();
                ResetData();
                blobj = new BL_RM_Printing();
                if (drpReceiptNo.SelectedIndex > 0)
                {
                    DataTable dtpartCode = blobj.BINDPART_CODE(drpReceiptNo.Text, drpGRPODate.Text, Session["SiteCode"].ToString());
                    if (dtpartCode.Rows.Count > 0)
                    {
                        CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                        clsCommon.FillMultiColumnsCombo(drpItemCode, dtpartCode, true);
                        drpItemCode.Focus();
                    }
                    else
                    {
                        CommonHelper.ShowMessage("Part code not available", msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void BindLineNo()
        {
            try
            {
                txtMFGDate.Text = "";
                txtExpiryDate.Text = "";
                btnPrint.Visible = true;
                blobj = new BL_RM_Printing();
                DataTable dtGRN = blobj.BINDPART_LINENO(drpReceiptNo.Text, drpItemCode.Text
                    , drpGRPODate.Text, Session["SiteCode"].ToString());
                if (dtGRN.Rows.Count > 0)
                {
                    clsCommon.FillMultiColumnsCombo(drpLineNo, dtGRN, true);
                    drpLineNo.Focus();
                }
                else
                {
                    CommonHelper.ShowMessage("No data found.", msginfo, CommonHelper.MessageType.Info.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void BindGRNDetails()
        {
            try
            {
                if (drpItemCode.SelectedIndex > 0)
                {
                    blobj = new BL_RM_Printing();
                    DataTable dtDetails = blobj.BINDGrnDetails(drpReceiptNo.Text, drpMFR.Text,
                        drpItemCode.Text, drpLineNo.SelectedItem.Text.ToString(), drpGRPODate.Text
                        , Session["SiteCode"].ToString()
                        );
                    if (dtDetails.Rows.Count > 0)
                    {
                        txtMFGDate.Text = string.Empty;
                        txtExpiryDate.Text = string.Empty;
                        txtQty.Text = "";
                        foreach (DataRow c in dtDetails.Rows)
                        {
                            hidRNO.Value = c.ItemArray[0].ToString();
                            txtComDescription.Text = c.ItemArray[2].ToString();
                            txtInvoiceNumber.Text = c.ItemArray[3].ToString();
                            txtSupplierDate.Text = c.ItemArray[4].ToString();
                            txtQty.Text = Convert.ToString(c.ItemArray[5].ToString());
                            txtRemainingQty.Text = Convert.ToString(c.ItemArray[7].ToString());
                            txtDealerName.Text = c.ItemArray[8].ToString();
                            txtComDescription.Text = c.ItemArray[9].ToString();
                            txtpackSize.Text = c.ItemArray[10].ToString();
                            hidSupplierName.Value = Convert.ToString(c.ItemArray[8].ToString());
                            hidUOM.Value = Convert.ToString(c.ItemArray[11].ToString());
                            hidNoODays.Value = Convert.ToString(c.ItemArray[12].ToString());
                            drpMFR.Items.Add(c.ItemArray[13].ToString());
                            if (drpMFR.Items.Count > 0)
                            {
                                drpMFR.SelectedIndex = 0;
                            }
                            //hidGRNdate.Value = txtGRNDate.Text;
                            hiddiscription.Value = txtComDescription.Text;
                            hidQty.Value = txtQty.Text;
                            hidDealerName.Value = txtDealerName.Text;
                            hidRemQty.Value = txtRemainingQty.Text;
                            hidPK.Value = txtpackSize.Text;
                            hidInvoice.Value = txtInvoiceNumber.Text;
                            hidInvoiceDate.Value = txtSupplierDate.Text;
                            lblMake.Text = c.ItemArray[14].ToString();
                        }
                    }
                    else
                    {
                        txtMFGDate.Text = string.Empty;
                        txtExpiryDate.Text = string.Empty;
                        txtQty.Text = "0";
                        txtInvoiceNumber.Text = "";
                        txtDealerName.Text = string.Empty;
                        txtComDescription.Text = string.Empty;
                        txtExpiryDate.Enabled = true;
                        txtNoOfPrints.Text = "0";
                        txtMFGDate.Text = "";
                        if (drpLineNo.Items.Count > 0)
                        {
                            drpLineNo.Items.Clear();
                        }
                        if (drpItemCode.Items.Count > 0)
                        {
                            drpItemCode.Items.Clear();
                        }
                        if (drpMFR.Items.Count > 0)
                        {
                            drpMFR.Items.Clear();
                        }
                        txtQty.Text = "0";
                        txtSupplierDate.Text = "";
                        txtpackSize.Text = "";
                        txtRemainingQty.Text = "0";
                        if (drpPrinterName.Items.Count > 0)
                        {
                            drpPrinterName.SelectedIndex = 0;
                        }
                        if (drpReceiptNo.Items.Count > 1)
                        {
                            drpReceiptNo.SelectedIndex = 1;
                            BindPartCode();
                            hidppo.Value = drpReceiptNo.Text;
                        }
                        CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                        CommonHelper.ShowMessage("No qty remains for printing, All barcode printed successfully.", msgsuccess, CommonHelper.MessageType.Success.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
        }
        private void Reset()
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                txtInvoiceNumber.Text = "";
                //txtGRNDate.Text = "";
                txtMFGDate.Text = "";
                txtExpiryDate.Text = "";
                txtDealerName.Text = "";
                txtComDescription.Text = "";
                txtNoOfPrints.Text = "";
                txtRemainingQty.Text = "";
                txtMFGDate.Text = "";
                drpItemCode.Items.Clear();
                drpMFR.Items.Clear();
                txtBatchNo.Text = string.Empty;
                lblMake.Text = string.Empty;
                BindPO();
                txtQty.Text = "";
                txtSupplierDate.Text = "";
                txtpackSize.Text = "";
                if (drpPrinterName.Items.Count > 0)
                {
                    drpPrinterName.SelectedIndex = 0;
                }
                if (drpReceiptNo.Items.Count > 0)
                {
                    drpReceiptNo.SelectedIndex = 1;
                    BindPartCode();
                }
                drpLineNo.Items.Clear();
                txtMSLValue.Text = string.Empty;
                drpLF.SelectedIndex = 0;
                dvDetails.DataSource = null;
                dvDetails.DataBind();
                txtExpiryDate.CssClass = "form-control";
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        protected void getDetails()
        {
            txtComDescription.Text = hiddiscription.Value;
            txtDealerName.Text = hidDealerName.Value;
            txtQty.Text = hidQty.Value;
            txtRemainingQty.Text = hidRemQty.Value;
            txtExpiryDate.Text = hidEXPDATE.Value;
            txtDealerName.Text = hidDealerName.Value;
            txtMFGDate.Text = hidMFGDate.Value;
            txtpackSize.Text = hidPK.Value;
            txtMSLValue.Text = hidMSLValue.Value;
            drpLF.Text = hidLHRH.Value;
            txtInvoiceNumber.Text = hidInvoice.Value;
            txtBatchNo.Text = hidbatchno.Value;
            txtSupplierDate.Text = Convert.ToDateTime(hidInvoiceDate.Value).ToString("yyyy-MM-dd");
        }
        protected void drpGRPODate_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (drpGRPODate.SelectedIndex > 0)
                {
                    BindPO();
                    hidGRNdate.Value = drpGRPODate.Text;
                }
                else
                {
                    drpGRPODate.Items.Clear();
                    drpReceiptNo.Items.Clear();
                    drpItemCode.Items.Clear();
                    drpLineNo.Items.Clear();
                    drpMFR.Items.Clear();
                    ResetData();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void drpReceiptNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (drpReceiptNo.SelectedIndex > 0)
                {
                    BindPartCode();
                    hidppo.Value = drpReceiptNo.Text;
                }
                else
                {
                    drpItemCode.Items.Clear();
                    drpLineNo.Items.Clear();
                    drpMFR.Items.Clear();
                    ResetData();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void drpItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                dvDetails.DataSource = null;
                dvDetails.DataBind();
                if (drpItemCode.SelectedIndex > 0)
                {
                    BindLineNo();
                    drpReceiptNo.Text = hidppo.Value;
                    txtExpiryDate.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void drpLineNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (drpLineNo.SelectedIndex > 0)
                {
                    txtInvoiceNumber.Text = string.Empty;
                    txtMFGDate.Text = string.Empty;
                    txtExpiryDate.Text = string.Empty;
                    txtMSLValue.Text = string.Empty;
                    txtBatchNo.Text = string.Empty;
                    txtSupplierDate.Text = string.Empty;
                    txtMSLValue.Text = string.Empty;
                    txtQty.Text = string.Empty;
                    txtRemainingQty.Text = string.Empty;
                    txtpackSize.Text = "0";

                    hiddiscription.Value = "";
                    hidDealerName.Value = "";
                    hidQty.Value = "0";
                    hidRemQty.Value = "";
                    hidEXPDATE.Value = "";
                    hidDealerName.Value = "";
                    hidMFGDate.Value = "";
                    hidPK.Value = "";
                    hidMSLValue.Value = "";
                    hidLHRH.Value = "";
                    hidInvoice.Value = "";
                    hidbatchno.Value = "";
                    hidInvoiceDate.Value = "";

                    BindGRNDetails();
                    hidMFGDate.Value = "";
                    hidEXPDATE.Value = "";
                    drpReceiptNo.Text = hidppo.Value;
                    hidRMID.Value = drpLineNo.SelectedValue.ToString();
                    BindPrintDetails();
                }
                else
                {
                    drpMFR.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                             
                if (drpGRPODate.SelectedIndex == 0 || drpGRPODate.SelectedIndex == -1)
                {
                    CommonHelper.ShowMessage("Please select GRPO date", msgerror, CommonHelper.MessageType.Error.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    getDetails();
                    return;
                }
                if (drpReceiptNo.SelectedIndex == 0 || drpReceiptNo.SelectedIndex == -1)
                {
                    CommonHelper.ShowMessage("Please select GRPO number", msgerror, CommonHelper.MessageType.Error.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    getDetails();
                    return;
                }
                if (drpItemCode.SelectedIndex == 0 || drpItemCode.SelectedIndex == -1)
                {
                    CommonHelper.ShowMessage("Please select part code", msgerror, CommonHelper.MessageType.Error.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    getDetails();
                    return;
                }
                if (drpLineNo.SelectedIndex == 0 || drpLineNo.SelectedIndex == -1)
                {
                    CommonHelper.ShowMessage("Please select line no", msgerror, CommonHelper.MessageType.Error.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    getDetails(); return;
                }
                if (dvPrintergrup.Visible == true)
                {
                    if (drpPrinterName.SelectedIndex == 0 || drpPrinterName.SelectedIndex == -1)
                    {
                        CommonHelper.ShowMessage("Please select the printer", msgerror, CommonHelper.MessageType.Error.ToString());
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                        getDetails();
                        return;
                    }
                }

                txtInvoiceNumber.Text = hidInvoice.Value;
                hidInvoice.Value = txtInvoiceNumber.Text;
                txtSupplierDate.Text = Convert.ToDateTime(hidInvoiceDate.Value).ToString("yyyy-MM-dd");
                if (string.IsNullOrEmpty(txtSupplierDate.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please enter invoice Date", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtSupplierDate.Focus();
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    getDetails(); return;
                }
                //if (Convert.ToDateTime(txtSupplierDate.Text).Date > Convert.ToDateTime(DateTime.Now).Date)
                //{
                //    CommonHelper.ShowMessage("Invoice date always lesser or equal to current date", msgerror, CommonHelper.MessageType.Error.ToString());
                //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                //    txtSupplierDate.Text = string.Empty;
                //    txtSupplierDate.Focus(); getDetails();
                //    return;
                //}
                hidInvoiceDate.Value = txtSupplierDate.Text;
                hidLHRH.Value = drpLF.Text;
                if (txtBatchNo.Text.Trim() == "")
                {
                    CommonHelper.ShowMessage("Please enter batch no", msgerror, CommonHelper.MessageType.Error.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    txtBatchNo.Focus();
                    getDetails();
                    return;
                }
                hidbatchno.Value = txtBatchNo.Text;
                if (txtMFGDate.Text == "")
                {
                    CommonHelper.ShowMessage("Please enter manufacturing date", msgerror, CommonHelper.MessageType.Error.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    txtMFGDate.Focus();
                    getDetails(); return;
                }
                if (txtExpiryDate.Text == "")
                {
                    CommonHelper.ShowMessage("Please enter expiry date", msgerror, CommonHelper.MessageType.Error.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    getDetails(); return;
                }
                if (Convert.ToDateTime(txtMFGDate.Text).Date > Convert.ToDateTime(DateTime.Now).Date)
                {
                    CommonHelper.ShowMessage("MFG date always lesser or equal to current date", msgerror, CommonHelper.MessageType.Error.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    txtMFGDate.Text = string.Empty;
                    txtMFGDate.Focus();
                    getDetails();
                    return;
                }
                if (Convert.ToDateTime(txtMFGDate.Text) > Convert.ToDateTime(txtExpiryDate.Text))
                {
                    CommonHelper.ShowMessage("Expiry date can't be lesser than manufacturing date", msgerror, CommonHelper.MessageType.Error.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    txtExpiryDate.Text = string.Empty;
                    txtExpiryDate.Focus(); getDetails();
                    return;
                }
                if (Convert.ToDateTime(txtMFGDate.Text) == Convert.ToDateTime(txtExpiryDate.Text))
                {
                    CommonHelper.ShowMessage("Expiry date can't be same as manufacturing date", msgerror, CommonHelper.MessageType.Error.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    txtExpiryDate.Text = string.Empty;
                    txtExpiryDate.Focus();
                    getDetails();
                    return;
                }
                if (Convert.ToDateTime(txtExpiryDate.Text).Date < Convert.ToDateTime(DateTime.Now).Date)
                {
                    CommonHelper.ShowMessage("EXP date always lesser or equal to current date", msgerror, CommonHelper.MessageType.Error.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    txtMFGDate.Text = string.Empty;
                    txtMFGDate.Focus();
                    getDetails();
                    return;
                }
                hidMFGDate.Value = txtMFGDate.Text;
                hidEXPDATE.Value = txtExpiryDate.Text;
                if (string.IsNullOrEmpty(txtpackSize.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please enter pack size.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtpackSize.Focus();
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    getDetails(); return;
                }
                blobj = new BL_RM_Printing();
                DataTable dtDetails = blobj.BINDGrnDetails(drpReceiptNo.Text, drpMFR.Text,
                    drpItemCode.Text, drpLineNo.SelectedItem.Text.ToString(), drpGRPODate.Text
                    , Session["SiteCode"].ToString()
                    );
                if (dtDetails.Rows.Count > 0)
                {
                    foreach (DataRow c in dtDetails.Rows)
                    {
                        txtQty.Text = Convert.ToString(c.ItemArray[5].ToString());
                        txtRemainingQty.Text = Convert.ToString(c.ItemArray[7].ToString());
                        hidRemQty.Value = txtRemainingQty.Text;
                        hidQty.Value = txtQty.Text;
                    }
                }
                if (txtQty.Text.Length == 0)
                {
                    CommonHelper.ShowMessage("No Qty left for printing", msgerror, CommonHelper.MessageType.Error.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    BindGRNDetails();
                    txtMFGDate.Text = hidMFGDate.Value;
                    txtExpiryDate.Text = hidEXPDATE.Value;
                    txtNoOfPrints.Text = "";
                    txtNoOfPrints.Focus();
                    BindPrintDetails();
                    if (txtQty.Text == "")
                    {
                        Reset();
                    }
                }
                if (Convert.ToDecimal(txtpackSize.Text.Trim()) == 0)
                {
                    CommonHelper.ShowMessage("Pack size can not be zero, Please enter valid pack size.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtpackSize.Focus();
                    txtpackSize.Text = "0";
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    getDetails(); return;
                }
                hidPK.Value = txtpackSize.Text;
                if (string.IsNullOrEmpty(txtMSLValue.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please enter MSL value", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtMSLValue.Focus();
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    getDetails(); return;
                }
                hidMSLValue.Value = txtMSLValue.Text;
                if (txtRemainingQty.Text == "")
                {
                    CommonHelper.ShowMessage("Remaining quantity not available", msgerror, CommonHelper.MessageType.Error.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    getDetails(); return;
                }
                if (txtNoOfPrints.Text == "")
                {
                    CommonHelper.ShowMessage("Please enter print quantity", msgerror, CommonHelper.MessageType.Error.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    getDetails(); return;
                }
                if (txtNoOfPrints.Text == "") // NO of prints
                {
                    CommonHelper.ShowMessage("Please enter no of print quantity", msgerror, CommonHelper.MessageType.Error.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    getDetails();
                    return;
                }
                if (Convert.ToDecimal(txtpackSize.Text) > Convert.ToDecimal(txtRemainingQty.Text))
                {
                    CommonHelper.ShowMessage("Pack size can't be greater then remaining quantity", msgerror, CommonHelper.MessageType.Error.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    getDetails();
                    txtpackSize.Focus();
                    return;
                }
                if (Convert.ToInt32(txtNoOfPrints.Text) == 0) // NO of prints
                {
                    CommonHelper.ShowMessage("No of prints should be greater than 0.", msgerror, CommonHelper.MessageType.Error.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    getDetails();
                    txtNoOfPrints.Focus();
                    return;
                }
                if (Convert.ToDecimal(Convert.ToDecimal(txtNoOfPrints.Text) * Convert.ToDecimal(txtpackSize.Text))
                    > Convert.ToDecimal(txtRemainingQty.Text))
                {
                    CommonHelper.ShowMessage("Print quantity can't be greater than remaining quantity", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtNoOfPrints.Text = "";
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    getDetails();
                    txtNoOfPrints.Focus();
                    return;
                }
                hidRemQty.Value = txtRemainingQty.Text;
                blobj = new BL_RM_Printing();
                string sMFRPartCode = string.Empty;
                if (drpMFR.SelectedIndex < 0)
                {
                    sMFRPartCode = "";
                }
                else
                {
                    sMFRPartCode = drpMFR.Text;
                }
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "No")
                {
                    return;
                }
                string sLineCode = Session["LINECODE"].ToString();
                string sUserID = Session["UserID"].ToString();
                PCommon.myList = new string[Convert.ToInt32(txtNoOfPrints.Text)];
                for (int i = 0; i < Convert.ToInt32(txtNoOfPrints.Text); i++)
                {
                    string _Result = blobj.GRNPrinting(
                        drpReceiptNo.Text, sMFRPartCode, drpItemCode.Text,
                        txtInvoiceNumber.Text, Convert.ToDateTime(txtMFGDate.Text),
                        Convert.ToDecimal(txtpackSize.Text), Session["UserID"].ToString(),
                        drpPrinterName.Text, PCommon.sPrinterPort, txtBatchNo.Text.Trim(), Convert.ToInt32(hidRNO.Value),
                         Convert.ToDateTime(txtExpiryDate.Text), Convert.ToDateTime(txtSupplierDate.Text),
                         hidSupplierName.Value
                         , 0, 0,
                         PCommon.sSiteCode, drpLineNo.SelectedItem.Text.ToString(), hidSupplierCode.Value, hidUOM.Value
                        , "",
                         drpLF.Text.Trim(), txtMSLValue.Text.Trim(), drpGRPODate.Text, sUserID, sLineCode
                         );

                    Message = _Result;
                    if (Message.Length > 0)
                    {
                        if (Message.Contains("SUCCESS~"))
                        {
                            CommonHelper.ShowMessage("RM label printed successfully", msgsuccess, CommonHelper.MessageType.Success.ToString());
                            string sPrintQty = txtpackSize.Text;
                            //BindGRNDetails();
                            if (drpReceiptNo.SelectedIndex > 0)
                            {
                                txtMFGDate.Text = hidMFGDate.Value;
                                txtExpiryDate.Text = hidEXPDATE.Value;
                                //BindPrintDetails();
                                txtpackSize.Text = sPrintQty;
                            }
                        }
                        if (Message.Contains("FULLQTY~"))
                        {
                            CommonHelper.ShowMessage(Message.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                            BindGRNDetails();
                            if (drpReceiptNo.SelectedIndex > 0)
                            {
                                txtMFGDate.Text = hidMFGDate.Value;
                                txtExpiryDate.Text = hidEXPDATE.Value;
                                txtNoOfPrints.Text = "";
                                txtNoOfPrints.Focus();
                            }
                            return;
                        }
                    }
                }
                if (Message.Length > 0)
                {
                    if (Message.Contains("N~"))
                    {
                        CommonHelper.ShowMessage(Message.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        BindGRNDetails();
                        if (drpReceiptNo.SelectedIndex > 0)
                        {
                            txtMFGDate.Text = hidMFGDate.Value;
                            txtExpiryDate.Text = hidEXPDATE.Value;
                            txtNoOfPrints.Text = "";
                            txtNoOfPrints.Focus();
                        }
                        return;

                    }
                    else if (Message.Contains("PRNNOTFOUND~"))
                    {
                        CommonHelper.ShowMessage("PRN for RM printing is not available.", msgerror, CommonHelper.MessageType.Error.ToString());
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                        BindGRNDetails();
                        txtMFGDate.Text = hidMFGDate.Value;
                        txtExpiryDate.Text = hidEXPDATE.Value;
                        txtNoOfPrints.Text = "";
                        txtNoOfPrints.Focus();
                        BindPrintDetails();
                        return;
                    }
                    else if (Message.Contains("PRINTERNOTCONNECTED~"))
                    {
                        CommonHelper.ShowMessage("Printer is not attached or printer is offline. Printing failed.", msgerror, CommonHelper.MessageType.Error.ToString());
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                        BindGRNDetails();
                        txtMFGDate.Text = hidMFGDate.Value;
                        txtExpiryDate.Text = hidEXPDATE.Value;
                        txtNoOfPrints.Text = "";
                        txtNoOfPrints.Focus();
                        BindPrintDetails();
                        return;
                    }
                    else if (Message.Contains("SUCCESS~"))
                    {
                        CommonHelper.ShowMessage("RM label printed successfully", msgsuccess, CommonHelper.MessageType.Success.ToString());
                        BindGRNDetails();
                        txtMFGDate.Text = hidMFGDate.Value;
                        txtExpiryDate.Text = hidEXPDATE.Value;
                        txtNoOfPrints.Text = "";
                        txtNoOfPrints.Focus();
                        BindPrintDetails();
                        if (txtQty.Text == "")
                        {
                            Reset();
                        }
                        return;
                    }
                    if (Message.Contains("FULLQTY~"))
                    {
                        CommonHelper.ShowMessage(Message.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        BindGRNDetails();
                        if (drpReceiptNo.SelectedIndex > 0)
                        {
                            txtMFGDate.Text = hidMFGDate.Value;
                            txtExpiryDate.Text = hidEXPDATE.Value;
                            txtNoOfPrints.Text = "";
                            txtNoOfPrints.Focus();
                        }
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                if (ex.Message.Contains("The string was not recognized as a valid DateTime."))
                {
                    CommonHelper.ShowMessage("Date is not in correct format", msgerror, CommonHelper.MessageType.Error.ToString());
                    getDetails();
                }
                else
                {
                    CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                    getDetails();
                }
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            msgdiv.InnerText = "";
            Reset();
        }
        private void BindPrintDetails()
        {
            try
            {
                getDetails();
                blobj = new BL_RM_Printing();
                DataTable dtPrintData = blobj.BINDPrintDetails(Convert.ToInt32(hidRMID.Value));
                if (dtPrintData.Rows.Count > 0)
                {
                    dvDetails.DataSource = dtPrintData;
                    dvDetails.DataBind();
                }
                else
                {
                    dvDetails.DataSource = null;
                    dvDetails.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
        }
        protected void dvDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
            dvDetails.PageIndex = e.NewPageIndex;
            this.BindPrintDetails();
        }
        protected void drpPrinterName_SelectedIndexChanged(object sender, EventArgs e)
        {
            CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
            getDetails();
        }
        protected void txtMFGDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                txtExpiryDate.Enabled = true;
                if (string.IsNullOrEmpty(txtMFGDate.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please enter MFG Date", msgerror, CommonHelper.MessageType.Error.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    txtMFGDate.Text = string.Empty;
                    txtMFGDate.Focus();
                    return;
                }
                if (hidNoODays.Value != null && Convert.ToInt32(hidNoODays.Value) > 0)
                {
                    txtExpiryDate.Text = Convert.ToString(Convert.ToDateTime(txtMFGDate.Text).AddDays(Convert.ToInt32(hidNoODays.Value)).ToString("yyyy-MM-dd"));
                    hidEXPDATE.Value = txtExpiryDate.Text;
                    txtExpiryDate.Enabled = false;
                    txtExpiryDate.CssClass = "form-control";
                }
                hidMFGDate.Value = txtMFGDate.Text;
                hidbatchno.Value = txtBatchNo.Text;
                hidMake.Value = lblMake.Text;
                getDetails();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
        }
    }
}

