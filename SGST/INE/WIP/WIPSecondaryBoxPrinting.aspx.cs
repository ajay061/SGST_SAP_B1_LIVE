using Common;
using System;
using System.Configuration;
using BusinessLayer;
using BusinessLayer.WIP;
using System.Data;
using PL;

namespace SGST.INE.WIP
{
    public partial class WIPSecondaryBoxPrinting : System.Web.UI.Page
    {
        BL_WIPSecondaryBoxPacking blobj = new BL_WIPSecondaryBoxPacking();
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
            if (Session["usertype"].ToString() != "ADMIN")
            {
                string _strRights = CommonHelper.GetRights("PALLET PACKING", (DataTable)Session["USER_RIGHTS"]);
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
                    lblPackingQty.Text = "0";
                    lblScanQty.Text = "0";
                    BindFGItemCode();
                    dvPrintergrup.Visible = true;
                    if (PCommon.sUseNetworkPrinter == "1")
                    {
                        getprinterlist();
                    }
                    else
                    {
                        dvPrintergrup.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                }
            }
        }
        private void ResetData()
        {
            txtBoxID.Text = string.Empty;
            lblPackingQty.Text = "0";
            lblScanQty.Text = "0";
            txtCustomerLocation.Text = string.Empty;
            txtCustomerName.Text = string.Empty;
            txtCustomerPartNo.Text = string.Empty;
            drpCustomerCode.Items.Clear();
            ddlPurchaseOrder.Items.Clear();
            drpWorkOrderNo.Items.Clear();
            drpInvoiceNo.Items.Clear();
            txtInvoiceData.Text = string.Empty;
            lblLastScanned.Text = string.Empty;
            txtNetWeight.Text = "0";
            txtWeight.Text = "0";
        }
        private void getprinterlist()
        {
            try
            {
                BL_Common objBlCommon = new BL_Common();
                DataTable dt = objBlCommon.BINDPRINTER("WIP");
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(drpPrinterName, dt, true);
                    drpPrinterName.SelectedIndex = 0;
                    drpPrinterName.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        private void BindFGItemCode()
        {
            try
            {
                ResetData();
                drpFGItemCode.Items.Clear();
                blobj = new BL_WIPSecondaryBoxPacking();
                string sResult = string.Empty;
                DataTable dt = blobj.BindFGItemCode(out sResult
                    , Session["SiteCode"].ToString(), Session["LINECODE"].ToString()
                    );
                if (dt.Rows.Count > 0)
                {
                    Message = string.Empty;
                    Message = sResult.Split('~')[1];
                    if (sResult.StartsWith("SUCCESS~"))
                    {
                        clsCommon.FillComboBox(drpFGItemCode, dt, true);
                        drpFGItemCode.SelectedIndex = 0;
                        drpFGItemCode.Focus();
                        return;
                    }
                    else if (sResult.StartsWith("NOTFOUND~"))
                    {
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    else if (sResult.StartsWith("ERROR~"))
                    {
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void BindWorkOrderNo()
        {
            try
            {
                drpWorkOrderNo.Items.Clear();
                blobj = new BL_WIPSecondaryBoxPacking();
                string sResult = string.Empty;
                DataTable dt = blobj.BindWorkOrderNo(Session["SiteCode"].ToString(), Session["LINECODE"].ToString()
                    );
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(drpWorkOrderNo, dt, true);
                    drpWorkOrderNo.SelectedIndex = 0;
                    drpWorkOrderNo.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void BindCustomerCode(string sFGItemCode)
        {
            try
            {
                drpCustomerCode.Items.Clear();
                txtBoxID.Text = string.Empty;
                blobj = new BL_WIPSecondaryBoxPacking();
                string sResult = string.Empty;
                DataTable dt = blobj.BindCustomerCode(sFGItemCode, out sResult
                    , Session["SiteCode"].ToString(), Session["LINECODE"].ToString()
                    );
                if (dt.Rows.Count > 0)
                {
                    Message = string.Empty;
                    Message = sResult.Split('~')[1];
                    if (sResult.StartsWith("SUCCESS~"))
                    {
                        clsCommon.FillComboBox(drpCustomerCode, dt, true);
                        drpCustomerCode.SelectedIndex = 0;
                        drpCustomerCode.Focus();
                        return;
                    }
                    else if (sResult.StartsWith("NOTFOUND~"))
                    {
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    else if (sResult.StartsWith("ERROR~"))
                    {
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);

            }
        }
        private void BindPurchaseOrder()
        {
            try
            {
                drpInvoiceNo.Items.Clear();
                ddlPurchaseOrder.Items.Clear();
                txtInvoiceData.Text = string.Empty;
                lblLastScanned.Text = string.Empty;
                txtNetWeight.Text = "0";
                txtWeight.Text = "0";
                if (drpFGItemCode.SelectedIndex > 0)
                {
                    blobj = new BL_WIPSecondaryBoxPacking();
                    DataTable dt = new DataTable();
                    dt = blobj.BindPurchaseOrderNo(drpFGItemCode.SelectedValue.ToString()
                        , Session["SiteCode"].ToString());
                    if (dt.Rows.Count > 0)
                    {
                        clsCommon.FillMultiColumnsCombo(ddlPurchaseOrder, dt, true);
                        ddlPurchaseOrder.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        private void BindBoxDetails()
        {
            try
            {
                txtBoxID.Text = string.Empty;
                lblPackingQty.Text = "0";
                lblScanQty.Text = "0";
                blobj = new BL_WIPSecondaryBoxPacking();
                string sResult = string.Empty;
                DataTable dt = blobj.GetFGDetails(drpFGItemCode.Text, drpCustomerCode.Text, out sResult,
                    Session["SiteCode"].ToString(), Session["LINECODE"].ToString()
                    , Session["UserID"].ToString()
                    );
                if (dt.Rows.Count > 0)
                {
                    Message = string.Empty;
                    Message = sResult.Split('~')[1];
                    if (sResult.StartsWith("SUCCESS~"))
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            lblPackingQty.Text = dr.ItemArray[0].ToString();
                            lblScanQty.Text = dr.ItemArray[3].ToString();
                        }
                        return;
                    }
                    else if (sResult.StartsWith("NOTFOUND~"))
                    {
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    else if (sResult.StartsWith("ERROR~"))
                    {
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    else if (sResult.StartsWith("N~"))
                    {
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
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
        private void GetCustomerDetail(string sCustomerCode, string sFGitemCode)
        {
            try
            {
                txtCustomerName.Text = string.Empty;
                txtCustomerPartNo.Text = string.Empty;
                txtBoxID.Text = string.Empty;
                blobj = new BL_WIPSecondaryBoxPacking();
                string sResult = string.Empty;
                DataTable dt = blobj.GetCustomerDetails(sFGitemCode, sCustomerCode, out sResult, Session["SiteCode"].ToString(), Session["LINECODE"].ToString());
                if (dt.Rows.Count > 0)
                {
                    Message = string.Empty;
                    Message = sResult.Split('~')[1];
                    if (sResult.StartsWith("SUCCESS~"))
                    {
                        txtCustomerName.Text = dt.Rows[0][0].ToString();
                        txtCustomerPartNo.Text = dt.Rows[0][1].ToString();
                        return;
                    }
                    else if (sResult.StartsWith("NOTFOUND~"))
                    {
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    else if (sResult.StartsWith("ERROR~"))
                    {
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void PrintLabel()
        {
            string sLineCode = Session["LINECODE"].ToString();
            string sUserID = Session["UserID"].ToString();
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            string sResult = string.Empty;
            DateTime dInvoiceDate = DateTime.Now;
            if (txtInvoiceData.Text.Length > 0)
            {
                dInvoiceDate = Convert.ToDateTime(txtInvoiceData.Text);
            }
            try
            {
                blobj = new BL_WIPSecondaryBoxPacking();
                sResult = blobj.PrintBoxID(drpFGItemCode.Text, txtCustomerName.Text,
                    txtCustomerPartNo.Text, txtCustomerLocation.Text,
                      drpLocatioType.Text, "", drpPrinterName.Text, drpCustomerCode.Text
                      , drpInvoiceNo.SelectedItem.Text, Convert.ToDecimal(txtWeight.Text), drpWorkOrderNo.Text
                      , sUserID, sLineCode, Session["SiteCode"].ToString(),
                     dInvoiceDate, Convert.ToDecimal(txtNetWeight.Text)
                      , ddlPurchaseOrder.SelectedValue.ToString()
                      );
                if (sResult.StartsWith("SUCCESS~"))
                {
                    BindBoxDetails();
                    lblScanQty.Text = "0";
                    Message = sResult.Split('~')[1];
                    CommonHelper.ShowMessage(Message, msgsuccess, CommonHelper.MessageType.Success.ToString());
                    txtBoxID.Text = string.Empty;
                    txtBoxID.Focus();
                    sResult = "SUCCESS~";
                }
                else if (sResult.StartsWith("N~"))
                {
                    Message = sResult.Split('~')[1];
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBoxID.Text = string.Empty;
                    txtBoxID.Focus();
                    sResult = "N~";
                }
                else if (sResult.StartsWith("PRNNOTFOUND~"))
                {
                    Message = sResult.Split('~')[1];
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBoxID.Text = string.Empty;
                    txtBoxID.Focus();
                    sResult = "N~";
                }
                else if (sResult.StartsWith("PRINTERNOTCONNECTED~"))
                {
                    Message = sResult.Split('~')[1];
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBoxID.Text = string.Empty;
                    txtBoxID.Focus();
                    sResult = "N~";
                }
                else if (sResult.StartsWith("PRINTINGFAILED~"))
                {
                    Message = sResult.Split('~')[1];
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBoxID.Text = string.Empty;
                    txtBoxID.Focus();
                    sResult = "N~";
                }
                else if (sResult.StartsWith("ERROR~"))
                {
                    BindBoxDetails();
                    lblScanQty.Text = "0";
                    Message = sResult.Split('~')[1];
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBoxID.Text = string.Empty;
                    txtBoxID.Focus();
                    sResult = "N~";
                }
                else if (sResult.StartsWith("PRINTERPRNNOTPRINT~"))
                {
                    Message = sResult.Split('~')[1];
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBoxID.Text = string.Empty;
                    txtBoxID.Focus();
                    sResult = "N~";
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }

        protected void txtBoxID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (drpFGItemCode.Items.Count == 0)
                {
                    CommonHelper.ShowMessage("Fg item code not found", msginfo, CommonHelper.MessageType.Info.ToString());
                    drpFGItemCode.Focus();
                    txtBoxID.Text = string.Empty;
                    return;
                }
                if (drpFGItemCode.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select fg item code", msginfo, CommonHelper.MessageType.Info.ToString());
                    drpFGItemCode.Focus();
                    txtBoxID.Text = string.Empty;
                    return;
                }
                else if (drpCustomerCode.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select customer code", msginfo, CommonHelper.MessageType.Info.ToString());
                    drpCustomerCode.Focus();
                    txtBoxID.Text = string.Empty;
                    return;
                }
                else if (txtCustomerName.Text.Trim() == string.Empty)
                {
                    CommonHelper.ShowMessage("Please enter customer name", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtCustomerName.Focus();
                    txtBoxID.Text = string.Empty;
                    return;
                }
                else if (txtCustomerPartNo.Text.Trim() == string.Empty)
                {
                    CommonHelper.ShowMessage("Please enter customer part no", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtCustomerPartNo.Focus();
                    txtBoxID.Text = string.Empty;
                    return;
                }
                else if (txtCustomerLocation.Text.Trim() == string.Empty)
                {
                    CommonHelper.ShowMessage("Please enter customer location", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtCustomerLocation.Focus();
                    txtBoxID.Text = string.Empty;
                    return;
                }
                if (ddlPurchaseOrder.Items.Count > 0)
                {
                    if (ddlPurchaseOrder.SelectedIndex == 0)
                    {
                        CommonHelper.ShowMessage("Please select puchase order No", msginfo, CommonHelper.MessageType.Info.ToString());
                        txtBoxID.Text = string.Empty;
                        return;
                    }
                    if (drpInvoiceNo.Items.Count > 0)
                    {
                        if (drpInvoiceNo.SelectedIndex == 0)
                        {
                            CommonHelper.ShowMessage("Please select Invoice No", msginfo, CommonHelper.MessageType.Info.ToString());
                            txtBoxID.Text = string.Empty;
                            return;
                        }
                        if (txtInvoiceData.Text.Trim() == string.Empty)
                        {
                            CommonHelper.ShowMessage("Please enter Invoice date", msginfo, CommonHelper.MessageType.Info.ToString());
                            txtInvoiceData.Focus();
                            txtBoxID.Text = string.Empty;
                            return;
                        }
                    }

                }
                else if (txtWeight.Text.Trim() == string.Empty)
                {
                    CommonHelper.ShowMessage("Please enter weight", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtWeight.Focus();
                    txtBoxID.Text = string.Empty;
                    return;
                }
                else if (txtWeight.Text.Trim().Split('.').Length > 2)
                {
                    CommonHelper.ShowMessage("Invalid Weight, Please enter correct weight", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtWeight.Text = string.Empty;
                    txtWeight.Focus();
                    txtBoxID.Text = string.Empty;
                    return;
                }
                else if (txtNetWeight.Text.Trim() == string.Empty)
                {
                    CommonHelper.ShowMessage("Please enter Net weight", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtNetWeight.Focus();
                    txtBoxID.Text = string.Empty;
                    return;
                }
                else if (txtNetWeight.Text.Trim().Split('.').Length > 2)
                {
                    CommonHelper.ShowMessage("Invalid Net Weight, Please enter correct weight", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtWeight.Text = "0";
                    txtNetWeight.Focus();
                    txtBoxID.Text = string.Empty;
                    return;
                }
                try
                {
                    decimal dWeight = Convert.ToDecimal(txtWeight.Text.Trim());
                }
                catch (Exception)
                {
                    CommonHelper.ShowMessage("Invalid Weight, Please enter correct weight", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtWeight.Text = string.Empty;
                    txtWeight.Focus();
                    txtBoxID.Text = string.Empty;
                    return;
                }
                try
                {
                    decimal dNetWeight = Convert.ToDecimal(txtNetWeight.Text.Trim());
                }
                catch (Exception)
                {
                    CommonHelper.ShowMessage("Invalid Net Weight, Please enter correct weight", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtNetWeight.Text = "0";
                    txtNetWeight.Focus();
                    txtBoxID.Text = string.Empty;
                    return;
                }
                if (ddlPurchaseOrder.Items.Count > 0)
                {
                    if (ddlPurchaseOrder.SelectedIndex <= 0)
                    {
                        CommonHelper.ShowMessage("Please select purchase order no.", msgerror, CommonHelper.MessageType.Error.ToString());
                        ddlPurchaseOrder.Focus();
                        txtBoxID.Text = string.Empty;
                        return;
                    }
                }
                if (string.IsNullOrEmpty(txtBoxID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan box id", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtBoxID.Text = string.Empty;
                    txtBoxID.Focus(); return;
                }
                else if (dvPrintergrup.Visible == true && drpPrinterName.Text.Trim() == string.Empty)
                {
                    CommonHelper.ShowMessage("Please select printer", msginfo, CommonHelper.MessageType.Info.ToString());
                    drpPrinterName.Focus();
                    txtBoxID.Text = string.Empty;
                    return;
                }
                else if (dvPrintergrup.Visible == true && drpPrinterName.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select printer", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtBoxID.Text = string.Empty;
                    drpPrinterName.Focus(); return;
                }
                else if (Convert.ToInt32(lblPackingQty.Text) <= Convert.ToInt32(lblScanQty.Text))
                {
                    PrintLabel();
                    txtBoxID.Text = string.Empty;
                    txtBoxID.Focus(); return;
                }
                string sWONO = string.Empty;
                string sPONO = string.Empty;
                if (drpWorkOrderNo.SelectedIndex > 0)
                {
                    sWONO = drpWorkOrderNo.Text;
                }
                if (ddlPurchaseOrder.Items.Count > 0)
                {
                    if (ddlPurchaseOrder.SelectedIndex > 0)
                    {
                        sPONO = ddlPurchaseOrder.SelectedValue.ToString();
                    }
                }
                blobj = new BL_WIPSecondaryBoxPacking();
                string sResult = blobj.sScanBarcode(
                    drpFGItemCode.Text, txtBoxID.Text, sWONO
                    , Session["SiteCode"].ToString(), Session["LINECODE"].ToString()
                    , Session["UserID"].ToString(), sPONO);
                if (sResult.StartsWith("SUCCESS~"))
                {
                    BindBoxDetails();
                    Message = sResult.Split('~')[1];
                    if (Convert.ToInt32(lblPackingQty.Text) <= Convert.ToInt32(lblScanQty.Text))
                    {
                        PrintLabel();
                    }
                    else
                    {
                        CommonHelper.ShowMessage(Message, msgsuccess, CommonHelper.MessageType.Success.ToString());
                        txtBoxID.Text = string.Empty;
                        txtBoxID.Focus();
                    }
                    return;
                }
                else if (sResult.StartsWith("NOTFOUND~"))
                {
                    Message = sResult.Split('~')[1];
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    BindBoxDetails();
                }
                else if (sResult.StartsWith("ERROR~"))
                {
                    Message = sResult.Split('~')[1];
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                }
                else if (sResult.StartsWith("N~"))
                {
                    Message = sResult.Split('~')[1];
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                }
                txtBoxID.Text = string.Empty;
                txtBoxID.Focus();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void txtInvoiceData_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime d = Convert.ToDateTime(txtInvoiceData.Text);
            }
            catch (Exception ex)
            {
                CommonHelper.ShowMessage("Invalid datetime format", msgerror, CommonHelper.MessageType.Error.ToString());
                txtInvoiceData.Text = string.Empty;
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void drpFGItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ResetData();
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (drpFGItemCode.SelectedIndex > 0)
                {
                    BindCustomerCode(drpFGItemCode.SelectedValue.ToString());
                    BindWorkOrderNo();
                    BindPurchaseOrder();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void drpWorkOrderNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void drpCustomerCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtBoxID.Text = string.Empty;
                txtCustomerPartNo.Text = string.Empty;
                txtCustomerName.Text = string.Empty;
                lblPackingQty.Text = "0";
                lblScanQty.Text = "0";
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (drpCustomerCode.SelectedIndex > 0)
                {
                    BindBoxDetails();
                    GetCustomerDetail(drpCustomerCode.SelectedValue.ToString(), drpFGItemCode.SelectedValue.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void btnComplete_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "No")
                {
                    return;
                }
                if (drpFGItemCode.Items.Count == 0)
                {
                    CommonHelper.ShowMessage("Fg item code not found", msginfo, CommonHelper.MessageType.Info.ToString());
                    drpFGItemCode.Focus();
                    txtBoxID.Text = string.Empty;
                    return;
                }
                if (drpFGItemCode.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select fg item code", msginfo, CommonHelper.MessageType.Info.ToString());
                    drpFGItemCode.Focus();
                    txtBoxID.Text = string.Empty;
                    return;
                }
                else if (drpCustomerCode.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select customer code", msginfo, CommonHelper.MessageType.Info.ToString());
                    drpCustomerCode.Focus();
                    txtBoxID.Text = string.Empty;
                    return;
                }
                else if (txtCustomerName.Text.Trim() == string.Empty)
                {
                    CommonHelper.ShowMessage("Please enter customer name", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtCustomerName.Focus();
                    txtBoxID.Text = string.Empty;
                    return;
                }
                else if (txtCustomerPartNo.Text.Trim() == string.Empty)
                {
                    CommonHelper.ShowMessage("Please enter customer part no", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtCustomerPartNo.Focus();
                    txtBoxID.Text = string.Empty;
                    return;
                }
                else if (txtCustomerLocation.Text.Trim() == string.Empty)
                {
                    CommonHelper.ShowMessage("Please enter customer location", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtCustomerLocation.Focus();
                    txtBoxID.Text = string.Empty;
                    return;
                }
                if (ddlPurchaseOrder.Items.Count > 0)
                {
                    if (ddlPurchaseOrder.SelectedIndex == 0)
                    {
                        CommonHelper.ShowMessage("Please select puchase order No", msginfo, CommonHelper.MessageType.Info.ToString());
                        txtBoxID.Text = string.Empty;
                        return;
                    }
                    if (drpInvoiceNo.Items.Count > 0)
                    {
                        if (drpInvoiceNo.SelectedIndex == 0)
                        {
                            CommonHelper.ShowMessage("Please select Invoice No", msginfo, CommonHelper.MessageType.Info.ToString());
                            txtBoxID.Text = string.Empty;
                            return;
                        }
                        if (txtInvoiceData.Text.Trim() == string.Empty)
                        {
                            CommonHelper.ShowMessage("Please enter Invoice date", msginfo, CommonHelper.MessageType.Info.ToString());
                            txtInvoiceData.Focus();
                            txtBoxID.Text = string.Empty;
                            return;
                        }
                    }

                }
                else if (txtNetWeight.Text.Trim() == string.Empty)
                {
                    CommonHelper.ShowMessage("Please enter Net weight", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtNetWeight.Focus();
                    txtBoxID.Text = string.Empty;
                    return;
                }
                else if (txtWeight.Text.Trim() == string.Empty)
                {
                    CommonHelper.ShowMessage("Please enter weight", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtWeight.Focus();
                    txtBoxID.Text = string.Empty;
                    return;
                }
                else if (txtWeight.Text.Trim().Split('.').Length > 2)
                {
                    CommonHelper.ShowMessage("Invalid Weight, Please enter correct weight", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtWeight.Text = string.Empty;
                    txtWeight.Focus();
                    txtBoxID.Text = string.Empty;
                    return;
                }
                else if (txtNetWeight.Text.Trim() == string.Empty)
                {
                    CommonHelper.ShowMessage("Please enter Net weight", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtNetWeight.Focus();
                    txtBoxID.Text = string.Empty;
                    return;
                }
                else if (txtNetWeight.Text.Trim().Split('.').Length > 2)
                {
                    CommonHelper.ShowMessage("Invalid Net Weight, Please enter correct weight", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtWeight.Text = "0";
                    txtNetWeight.Focus();
                    txtBoxID.Text = string.Empty;
                    return;
                }
                try
                {
                    decimal dWeight = Convert.ToDecimal(txtWeight.Text.Trim());
                }
                catch (Exception)
                {
                    CommonHelper.ShowMessage("Invalid Weight, Please enter correct weight", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtWeight.Text = string.Empty;
                    txtWeight.Focus();
                    txtBoxID.Text = string.Empty;
                    return;
                }

                try
                {
                    decimal dNetWeight = Convert.ToDecimal(txtNetWeight.Text.Trim());
                }
                catch (Exception)
                {
                    CommonHelper.ShowMessage("Invalid Net Weight, Please enter correct weight", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtNetWeight.Text = "0";
                    txtNetWeight.Focus();
                    txtBoxID.Text = string.Empty;
                    return;
                }
                if (ddlPurchaseOrder.Items.Count > 0)
                {
                    if (ddlPurchaseOrder.SelectedIndex <= 0)
                    {
                        CommonHelper.ShowMessage("Please select purchase order no.", msgerror, CommonHelper.MessageType.Error.ToString());
                        ddlPurchaseOrder.Focus();
                        txtBoxID.Text = string.Empty;
                        return;
                    }
                }
                else if (dvPrintergrup.Visible == true && drpPrinterName.Text.Trim() == string.Empty)
                {
                    CommonHelper.ShowMessage("Please select printer", msginfo, CommonHelper.MessageType.Info.ToString());
                    drpPrinterName.Focus();
                    txtBoxID.Text = string.Empty;
                    return;
                }
                else if (dvPrintergrup.Visible == true && drpPrinterName.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select printer", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtBoxID.Text = string.Empty;
                    drpPrinterName.Focus(); return;
                }
                txtID.Text = string.Empty;
                txtPassword.Text = string.Empty;
                dvCompletePannel.Visible = true;
                txtID.Focus();

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
                txtBoxID.Text = string.Empty;
                txtCustomerLocation.Text = string.Empty;
                txtCustomerName.Text = string.Empty;
                txtCustomerPartNo.Text = string.Empty;
                lblPackingQty.Text = "0";
                lblScanQty.Text = "0";
                drpCustomerCode.Items.Clear();
                drpFGItemCode.Items.Clear();
                drpWorkOrderNo.Items.Clear();
                BindFGItemCode();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void ddlPurchaseOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtBoxID.Text = string.Empty;
                txtInvoiceData.Text = string.Empty;
                drpInvoiceNo.Items.Clear();
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (ddlPurchaseOrder.SelectedIndex > 0)
                {
                    blobj = new BL_WIPSecondaryBoxPacking();
                    DataTable dt = blobj.bindInvoiceNo(drpFGItemCode.Text, Session["SiteCode"].ToString(), ddlPurchaseOrder.SelectedItem.Text);
                    if (dt.Rows.Count > 0)
                    {
                        string sResult = dt.Rows[0][0].ToString();
                        if (sResult.StartsWith("NOTFOUND~"))
                        {
                            CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                        }
                        else if (sResult.StartsWith("ERROR~"))
                        {
                            CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                        }
                        else
                        {
                            clsCommon.FillMultiColumnsCombo(drpInvoiceNo, dt, true);
                            drpInvoiceNo.SelectedIndex = 0;
                            drpInvoiceNo.Focus();
                            drpInvoiceNo.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void drpInvoiceNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtInvoiceData.Text = string.Empty;
            if (drpInvoiceNo.SelectedIndex > 0)
            {
                txtInvoiceData.Text = Convert.ToDateTime(drpInvoiceNo.SelectedValue.ToString()).ToString("yyyy-MM-dd");
            }
        }

        protected void btnVerify_Click(object sender, EventArgs e)
        {
            try
            {
                BL_UserLogin blobj = new BL_UserLogin();
                DataTable dt = blobj.dtValidateCompleteButton(txtID.Text,PCommon.DataEncrypt(txtPassword.Text), "PALLET PACKING");
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString().StartsWith("SUCCESS~"))
                    {
                        PrintLabel();
                        dvCompletePannel.Visible = false;
                    }
                    else
                    {
                        CommonHelper.ShowMessage(dt.Rows[0][0].ToString(), msginfo, CommonHelper.MessageType.Info.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            txtID.Text = string.Empty;
            txtPassword.Text = string.Empty;
            dvCompletePannel.Visible = false;
        }
    }
}