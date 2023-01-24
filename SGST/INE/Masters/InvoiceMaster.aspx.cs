using Common;
using PL;
using System;
using System.Data;
using System.Web.UI.WebControls;
using BusinessLayer.Masters;

namespace SGST.INE.Masters
{
    public partial class InvoiceMaster : System.Web.UI.Page
    {
        BL_InvoiceMaster blobj = new BL_InvoiceMaster();
        string Message = string.Empty;
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
                Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
                Response.AddHeader("Pragma", "no-cache");
                Response.AddHeader("Expires", "0");
                if (Session["usertype"].ToString() != "ADMIN")
                {
                    string _strRights = CommonHelper.GetRights("INVOICE MASTER", (DataTable)Session["USER_RIGHTS"]);
                    CommonHelper._strRights = _strRights.Split('^');
                    if (CommonHelper._strRights[0] == "0")  //Check view rights
                    {
                        Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                    }
                }
                if (!IsPostBack)
                {
                    BindFGItemCode();
                    BindGrid();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        private void BindFGItemCode()
        {
            try
            {
                BL_InvoiceMaster blobj = new BL_InvoiceMaster();
                DataTable dt = blobj.BindFGitemCode();
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillMultiColumnsCombo(ddlFGItemCode, dt, true);
                    ddlFGItemCode.Focus();
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
        private void BindShipToAddress()
        {
            try
            {
                BL_InvoiceMaster blobj = new BL_InvoiceMaster();
                DataTable dt = blobj.BindShipToAddrss(txtModel.Text);
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillMultiColumnsCombo(drpShipToAddress, dt, true);
                    if (drpShipToAddress.Items.Count == 2)
                    {
                        drpShipToAddress.SelectedIndex = 1;
                    }
                    drpShipToAddress.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void BindPurchaseOrderNo()
        {
            try
            {
                BL_InvoiceMaster blobj = new BL_InvoiceMaster();
                DataTable dt = blobj.BindPurchaseOrderNo(txtModel.Text);
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillMultiColumnsCombo(drpPurchaseOrderNo, dt, true);
                    drpPurchaseOrderNo.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void SaveData()
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (txtInvoiceNo.Text.Trim() == "")
                {
                    CommonHelper.ShowMessage("Enter Invoice No.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtInvoiceNo.Focus();
                    return;
                }
                if (txtpurchaseDate.Text.Trim() == "")
                {
                    CommonHelper.ShowMessage("Enter Invoice Date", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtpurchaseDate.Focus();
                    return;
                }
                if (ddlFGItemCode.SelectedIndex == 0 && btnSave.Text == "Save")
                {
                    CommonHelper.ShowMessage("Please Select FG Item Code", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlFGItemCode.Focus();
                    return;
                }
                if (drpPurchaseOrderNo.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please Select Purchase Order No", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpPurchaseOrderNo.Focus();
                    return;
                }
                if (drpShipToAddress.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please Select Ship To Address", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpShipToAddress.Focus();
                    return;
                }
                if (txtPOQty.Text.Trim() == "")
                {
                    CommonHelper.ShowMessage("Enter Invoice Qty", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPOQty.Focus();
                    return;
                }
                if (Convert.ToInt32(txtPOQty.Text) == 0)
                {
                    CommonHelper.ShowMessage("Please Enter valid Invoice Qty", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPOQty.Focus();
                    return;
                }
                if (txtInvoiceBoxSize.Text.Trim() == "")
                {
                    CommonHelper.ShowMessage("Enter Invoice box Size", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtInvoiceBoxSize.Focus();
                    return;
                }
                if (Convert.ToInt32(txtInvoiceBoxSize.Text) == 0)
                {
                    CommonHelper.ShowMessage("Please Enter valid Invoice box Size", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtInvoiceBoxSize.Focus();
                    return;
                }
                if (txtShipmentDate.Text.Trim() == "")
                {
                    CommonHelper.ShowMessage("Enter shipment date", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtShipmentDate.Focus();
                    return;
                }
                BL_InvoiceMaster blobj = new BL_InvoiceMaster();
                DataTable dt = new DataTable();
                PL.PL_invoiceMaster plobj = new PL.PL_invoiceMaster();
                plobj.sPurchaseOrderNo = drpPurchaseOrderNo.Text;
                plobj.sModelCode = txtModel.Text;
                plobj.sInvoiceNo = txtInvoiceNo.Text;
                plobj.sInvoiceDate = txtpurchaseDate.Text;
                plobj.iInvoice_QTY = Convert.ToInt32(txtPOQty.Text.Trim());
                plobj.iInvoiceBoxSize = Convert.ToInt32(txtInvoiceBoxSize.Text.Trim());
                plobj.MSMID = Convert.ToInt32(drpShipToAddress.SelectedValue.ToString());
                plobj.sSiteCode = Session["SiteCode"].ToString();
                plobj.CREATED_BY = Session["UserID"].ToString();

                plobj.SUPPLIER_CODE = txtSupplierCode.Text;
                plobj.STOCK_POINT_NOTE = txtStockPointNote.Text;
                plobj.SHIPMENT_DATE = Convert.ToDateTime(txtShipmentDate.Text);

                if (btnSave.Text == "Save")
                {
                    dt = blobj.SaveInvoice(plobj);
                }
                else
                {
                    dt = blobj.UpdateInvoice(plobj);
                }
                Message = dt.Rows[0][0].ToString();
                if (dt.Rows.Count > 0)
                {
                    if (Message.StartsWith("ERROR~"))
                    {
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    if (Message.StartsWith("N~"))
                    {
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    else
                    {
                        Reset();
                        BindGrid();
                        CommonHelper.ShowMessage("Record saved successfully", msgsuccess, CommonHelper.MessageType.Success.ToString());
                    }
                }
                else
                {
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    Reset();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError,  System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void BindGrid()
        {
            try
            {
                BL_InvoiceMaster blobj = new BL_InvoiceMaster();
                PL_invoiceMaster plobj = new PL_invoiceMaster();
                DataTable dt = blobj.BindGrid();
                if (dt.Rows.Count > 0)
                {
                    gvInvoiceMaster.DataSource = dt;
                    gvInvoiceMaster.DataBind();
                    lblNumberofRecords.Text = dt.Rows.Count.ToString();
                }
                else
                {
                    gvInvoiceMaster.DataSource = null;
                    gvInvoiceMaster.DataBind();
                    lblNumberofRecords.Text = "0";
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }

        }
        protected void ddlFGItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtModel.Text = string.Empty;
                if (drpPurchaseOrderNo.SelectedIndex > 0)
                {
                    drpPurchaseOrderNo.SelectedIndex = 0;
                }
                drpPurchaseOrderNo.Items.Clear();
                if (drpShipToAddress.SelectedIndex > 0)
                {
                    drpShipToAddress.SelectedIndex = 0;
                }
                drpShipToAddress.Items.Clear();
                if (ddlFGItemCode.SelectedIndex > 0)
                {
                    txtModel.Text = ddlFGItemCode.SelectedValue.ToString();
                    BindPurchaseOrderNo();
                    BindShipToAddress();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void gvInvoiceMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvInvoiceMaster.PageIndex = e.NewPageIndex;
                BindGrid();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void gvInvoiceMaster_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                string _SN = string.Empty;
                string[] strValue = e.CommandArgument.ToString().Split('~');
                _SN = e.CommandArgument.ToString();
                if (e.CommandName == "DeleteRecords")
                {
                    DeleteRecords(_SN);
                }
                if (e.CommandName == "EditRecords")
                {
                    if (btnSave.Text == "Save")
                    {
                        btnSave.Text = "Update";
                    }
                    EditRecords(_SN);
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void EditRecords(string _SN)
        {
            try
            {
                BL_InvoiceMaster dlobj = new BL_InvoiceMaster();
                PL_invoiceMaster plobj = new PL_invoiceMaster();
                plobj.sInvoiceNo = _SN;
                DataTable dtDetails = dlobj.SearchInvoice(_SN);
                if (dtDetails.Rows.Count > 0)
                {
                    if (ddlFGItemCode.Items.Count > 0)
                    {
                        ddlFGItemCode.SelectedValue = dtDetails.Rows[0]["MODEL_CODE"].ToString();                       
                    }
                    txtModel.Text = dtDetails.Rows[0]["MODEL_CODE"].ToString();
                    BindPurchaseOrderNo();
                    BindShipToAddress();
                    drpPurchaseOrderNo.Text = dtDetails.Rows[0]["PURCHASE_ORDER_NO"].ToString();                   
                    drpShipToAddress.SelectedValue = dtDetails.Rows[0]["MSID"].ToString();
                    txtInvoiceNo.Enabled = false;
                    txtInvoiceNo.Text = dtDetails.Rows[0]["INVOICE_NO"].ToString();
                    txtpurchaseDate.Text = dtDetails.Rows[0]["INVOICE_DATE"].ToString();
                    txtPOQty.Text = dtDetails.Rows[0]["INVOICE_QTY"].ToString();
                    txtInvoiceBoxSize.Text = dtDetails.Rows[0]["INVOICE_BOX_SIZE"].ToString();
                    txtStockPointNote.Text = dtDetails.Rows[0]["SUPPLIER_CODE"].ToString();
                    txtSupplierCode.Text = dtDetails.Rows[0]["STOCK_POINT_NOTE"].ToString();
                    txtShipmentDate.Text = dtDetails.Rows[0]["SHIPMENT_DATE"].ToString();
                    hidUpdate.Value = "Update";
                    hidUID.Value = _SN;
                }
                else
                {
                    CommonHelper.ShowMessage("No  details found", msgerror, CommonHelper.MessageType.Error.ToString());

                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        private void DeleteRecords(string _SN)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                BL_InvoiceMaster blobj = new BL_InvoiceMaster();
                PL_invoiceMaster plobj = new PL_invoiceMaster();
                DataTable sResult = new DataTable();
                sResult = blobj.DeleteInvoice(_SN);
                Message = sResult.Rows[0][0].ToString();
                if (sResult.Rows.Count > 0)
                {
                    if (Message.StartsWith("ERROR~"))
                    {
                        txtpurchaseDate.Text = hidPoOrderID.Value;
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    if (Message.StartsWith("N~"))
                    {
                        txtpurchaseDate.Text = hidPoOrderID.Value;
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    else
                    {
                        BindGrid();
                        txtpurchaseDate.Text = hidPoOrderID.Value;
                        btnSave.Text = "Save";
                        CommonHelper.ShowMessage("Record deleted successfully", msgsuccess, CommonHelper.MessageType.Error.ToString());
                    }
                }
                else
                {
                    CommonHelper.ShowMessage("No result found", msgerror, CommonHelper.MessageType.Error.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
            finally
            {
                BindGrid();
            }

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                Reset();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        public void Reset()
        {
            try
            {
                txtModel.Text = string.Empty;
                txtInvoiceNo.Text = "";
                drpShipToAddress.Items.Clear();
                drpPurchaseOrderNo.Items.Clear();
                txtpurchaseDate.Text = "";
                txtPOQty.Text = "";
                txtInvoiceBoxSize.Text = "1";
                txtInvoiceNo.Focus();
                txtInvoiceNo.Enabled = true;
                txtShipmentDate.Text = "";
                txtStockPointNote.Text = "";
                txtSupplierCode.Text = "";
                BindFGItemCode();
                BindGrid();
                btnSave.Text = "Save";
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }
    }
}