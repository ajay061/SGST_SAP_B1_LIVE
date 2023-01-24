using BusinessLayer;
using Common;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace SGST.INE.RM
{
    public partial class MaterialToMaterial : System.Web.UI.Page
    {
        string Message = "";
        BL_MaterialToMaterialTransfer blobj = new BL_MaterialToMaterialTransfer();
        public void BindMatTransferNo()
        {
            try
            {
                drpMatRefNo.Items.Clear();
                drpItemCode.Items.Clear();
                dvData.DataSource = null;
                dvData.DataBind();
                txtQuantity.Text = string.Empty;
                blobj = new BL_MaterialToMaterialTransfer();
                DataTable dtPcode = blobj.BindMaterialRefNo(Session["SiteCode"].ToString());
                if (dtPcode.Rows.Count > 0)
                {
                    CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                    clsCommon.FillComboBox(drpMatRefNo, dtPcode, true);
                    drpMatRefNo.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        public void bindPartCode(string sPartCode)
        {
            try
            {
                txtQuantity.Text = string.Empty;              
                blobj = new BL_MaterialToMaterialTransfer();
                DataTable dtPcode = blobj.BindPARTCODE(sPartCode, Session["SiteCode"].ToString());
                if (dtPcode.Rows.Count > 0)
                {
                    CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                    clsCommon.FillComboBox(drpItemCode, dtPcode, true);
                    drpItemCode.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
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
                    CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                    clsCommon.FillComboBox(drpPrinterName, dt, true);
                    drpPrinterName.Focus();
                }
                else
                {
                    CommonHelper.ShowMessage("Printer not available", msgerror, CommonHelper.MessageType.Error.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
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
                string _strRights = CommonHelper.GetRights("MATERIAL TO MATERIAL TRANSFER", (DataTable)Session["USER_RIGHTS"]);
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
                    dvPrintergrup.Visible = true;
                    if (PCommon.sUseNetworkPrinter == "1")
                    {
                        getprinterlist();
                    }
                    else
                    {
                        dvPrintergrup.Visible = false;
                    }
                    BindMatTransferNo();
                }
                catch (Exception ex)
                {
                    CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                }
            }
        }
        protected void drpMatRefNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dvData.DataSource = null;
                dvData.DataBind();
                drpItemCode.Items.Clear();
                if (drpMatRefNo.SelectedIndex > 0)
                {
                    bindPartCode(drpMatRefNo.SelectedItem.Text);
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
                dvData.DataSource = null;
                dvData.DataBind();
                if (drpItemCode.SelectedIndex > 0)
                {
                    blobj = new BL_MaterialToMaterialTransfer();
                    DataTable dt = blobj.GetMatDetails(drpMatRefNo.Text, drpItemCode.Text, Session["SiteCode"].ToString()
);
                    if (dt.Rows.Count > 0)
                    {
                        dvData.DataSource = dt;
                        dvData.DataBind();
                    }
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
                txtQty.Text = hidqty.Value;
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (drpMatRefNo.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select tranfer ref no", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpMatRefNo.Focus();
                    return;
                }
                if (drpItemCode.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select tranfer item code", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpItemCode.Focus();
                    return;
                }
                if (dvData.Rows.Count == 0)
                {
                    CommonHelper.ShowMessage("No record found against selected data", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpItemCode.Focus();
                    return;
                }
                if (txtReelBarcode.Text.Trim() == "")
                {
                    CommonHelper.ShowMessage("Please enter part barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtReelBarcode.Focus();
                    return;
                }
                if (txtQuantity.Text.Trim() == "")
                {
                    CommonHelper.ShowMessage("Please enter quantity", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtQuantity.Focus();
                    return;
                }
                if (Convert.ToDecimal(txtQuantity.Text) > Convert.ToDecimal(txtQty.Text))
                {
                    CommonHelper.ShowMessage("New quantity can not be greater than old quantity", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtQuantity.Text = "0";
                    txtQuantity.Focus();
                    return;
                }
                if (drpItemCode.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select part code", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpItemCode.Focus();
                    return;
                }
                if (drpPrinterName.SelectedIndex == 0 && dvPrintergrup.Visible == true)
                {
                    CommonHelper.ShowMessage("Please select printer name", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpPrinterName.Focus();
                    return;
                }
                string sType = string.Empty;
                string sExist = "1";
                string sInputPartcode = txtReelBarcode.Text.Trim().Split(',')[0];
                string sNewBatchNo = string.Empty;
                foreach (GridViewRow row in dvData.Rows)
                {
                    if (row.Cells[3].Text.ToUpper() != sInputPartcode.ToUpper())
                    {
                        sType = "Part Code";
                        sExist = "0";
                        break;
                    }
                    if (Convert.ToDecimal(row.Cells[6].Text) < Convert.ToDecimal(txtQuantity.Text.Trim()))
                    {
                        sType = "Quantity";
                        sExist = "0";
                        break;
                    }
                    //if (row.Cells[8].Text.ToUpper() != txtBatchNo.Text.Trim().ToUpper())
                    //{
                    //    sType = "Batch";
                    //    sExist = "0";
                    //    break;
                    //}
                    if (Convert.ToDecimal(row.Cells[0].Text) < Convert.ToDecimal(txtQuantity.Text.Trim()))
                    {
                        sType = "New quantity";
                        sExist = "0";
                        break;
                    }
                    if (row.Cells[3].Text.ToUpper() == sInputPartcode.ToUpper())
                    {
                        sExist = "1";
                        break;
                    }
                    sNewBatchNo = row.Cells[2].Text;

                }
                if (sExist == "0")
                {
                    CommonHelper.ShowMessage(sType + " of scanned barcode not found in selected data", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtReelBarcode.Focus();
                    txtReelBarcode.Text = string.Empty;
                    return;
                }
                blobj = new BL_MaterialToMaterialTransfer();
                DataTable dt = new DataTable();

                string sLineCode = Session["LINECODE"].ToString();
                string sUserID = Session["UserID"].ToString();

                string _Result = blobj.SaveMTransfer(
                         drpItemCode.Text,
                         txtReelBarcode.Text,
                         Session["UserID"].ToString(),
                         Convert.ToDecimal(txtQuantity.Text),
                         drpPrinterName.Text,
                        txtInvoiceNo.Text,
                     Convert.ToDateTime(txtMFGDate.Text),
                    Convert.ToDateTime(txtEXPDate.Text),
                     Convert.ToDateTime(txtInvoiceDate.Text),
                    sNewBatchNo,
                     txtQuantity.Text
                     , drpMatRefNo.Text, sUserID, sLineCode, Session["SiteCode"].ToString()
                    );
                Message = _Result;
                string[] msg = Message.Split('~');
                if (Message.Length > 0)
                {
                    if (msg[0].StartsWith("N") || msg[0].StartsWith("ERROR"))
                    {
                        CommonHelper.ShowMessage(msg[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    else
                    {
                        CommonHelper.ShowMessage(msg[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                        txtQty.Text = "";
                        txtQuantity.Text = "";
                        txtReelBarcode.Text = string.Empty;
                        txtBatchNo.Text = string.Empty;
                        txtEXPDate.Text = string.Empty;
                        txtInvoiceDate.Text = string.Empty;
                        txtInvoiceNo.Text = string.Empty;
                        txtMFGDate.Text = string.Empty;
                        dvData.DataSource = null;
                        dvData.DataBind();
                        drpItemCode_SelectedIndexChanged(null, null);
                        if (dvData.Rows.Count  == 0)
                        {
                            drpItemCode.Items.Clear();
                            bindPartCode(drpMatRefNo.Text);
                            if (drpItemCode.Items.Count == 0)
                            {
                                drpMatRefNo.Items.Clear();
                                BindMatTransferNo();
                            }
                        }
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
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            txtQuantity.Text = "";
            txtQty.Text = "";
            txtReelBarcode.Text = string.Empty;
            txtInvoiceDate.Text = string.Empty;
            txtBatchNo.Text = string.Empty;
            txtEXPDate.Text = string.Empty;
            txtInvoiceNo.Text = string.Empty;
            txtMFGDate.Text = string.Empty;
            hidqty.Value = "";
            dvData.DataSource = null;
            dvData.DataBind();
            drpItemCode.Items.Clear();
            drpMatRefNo.Items.Clear();
            BindMatTransferNo();
        }
        protected void txtReelBarcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtQuantity.Text = string.Empty;
                txtQty.Text = string.Empty;
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (drpMatRefNo.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select tranfer ref no", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpMatRefNo.Focus(); txtReelBarcode.Text = string.Empty;
                    return;
                }
                if (drpItemCode.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select tranfer item code", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpItemCode.Focus();
                    txtReelBarcode.Text = string.Empty;
                    return;
                }
                if (dvData.Rows.Count == 0)
                {
                    CommonHelper.ShowMessage("No record found against selected data", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpItemCode.Focus();
                    txtReelBarcode.Text = string.Empty;
                    return;
                }
                if (string.IsNullOrEmpty(txtReelBarcode.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please enter part barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtReelBarcode.Focus();
                    return;
                }
                string sInputPartcode = txtReelBarcode.Text.Trim().Split(',')[0];
                string sExist = "0";
                foreach (GridViewRow row in dvData.Rows)
                {
                    if (row.Cells[3].Text.ToUpper() == sInputPartcode.ToUpper())
                    {
                        sExist = "1";
                        break;
                    }
                }
                if (sExist == "0")
                {
                    CommonHelper.ShowMessage("Part code of scanned barcode not found in selected data", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtReelBarcode.Focus();
                    txtReelBarcode.Text = string.Empty;
                    return;
                }

                blobj = new BL_MaterialToMaterialTransfer();
                DataTable dt = new DataTable();
                string _Result = blobj.SCANPARTBARCODE(txtReelBarcode.Text, Session["SiteCode"].ToString());
                if (_Result.Length > 0)
                {
                    Message = _Result;
                    if (Message.StartsWith("SUCCESS~"))
                    {
                        string[] array = _Result.Split('~');
                        sExist = "1";
                        string sType = string.Empty;
                        foreach (GridViewRow row in dvData.Rows)
                        {
                            if (row.Cells[3].Text.ToUpper() != sInputPartcode.ToUpper())
                            {
                                sType = "Part Code";
                                sExist = "0";
                                break;
                            }
                            if (Convert.ToDecimal(row.Cells[6].Text) < Convert.ToDecimal(array[1].ToString()))
                            {
                                sType = "Quantity";
                                sExist = "0";
                                break;
                            }
                            if (row.Cells[3].Text.ToUpper() == sInputPartcode.ToUpper())
                            {
                                sExist = "1";
                                break;                                
                            }                            
                        }
                        if (sExist == "0")
                        {
                            CommonHelper.ShowMessage(sType + " of scanned barcode not found in selected data", msgerror, CommonHelper.MessageType.Error.ToString());
                            txtReelBarcode.Focus();
                            txtReelBarcode.Text = string.Empty;
                            return;
                        }
                        txtQty.Text = Convert.ToString(array[1]);
                        hidqty.Value = txtQty.Text;
                        txtQuantity.Text = Convert.ToString(array[1]);
                        txtMFGDate.Text = Convert.ToString(array[2]);
                        txtEXPDate.Text = Convert.ToString(array[3]);
                        txtInvoiceNo.Text = Convert.ToString(array[4]);
                        txtInvoiceDate.Text = Convert.ToString(array[5]);
                        txtBatchNo.Text = Convert.ToString(array[6]);
                    }
                    else
                    {
                        CommonHelper.ShowMessage(Message.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        txtReelBarcode.Text = string.Empty;
                        txtReelBarcode.Focus();
                        txtQuantity.Text = "";
                        txtQty.Text = "";
                        txtInvoiceDate.Text = string.Empty;
                        txtBatchNo.Text = string.Empty;
                        txtEXPDate.Text = string.Empty;
                        txtInvoiceNo.Text = string.Empty;
                        txtMFGDate.Text = string.Empty;
                        hidqty.Value = "";
                    }
                }
                else
                {
                    txtReelBarcode.Text = string.Empty;
                    txtReelBarcode.Focus();
                    CommonHelper.ShowMessage("No result found for scanned barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                }
            }
            catch (Exception ex)
            {
                txtReelBarcode.Text = string.Empty;
                txtReelBarcode.Focus();
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }


    }
}