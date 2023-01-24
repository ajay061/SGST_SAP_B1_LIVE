using Common;
using System;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;
using BusinessLayer;
using BusinessLayer.WIP;
namespace SGST.INE.WIP
{
    public partial class WIPComponentReelPrinting : System.Web.UI.Page
    {
        BL_WIPComponentReelPrinting blobj = new BL_WIPComponentReelPrinting();
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Signin/v1/Login.aspx?Session=Null");
            }
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
                    drpPrinterName.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }

        private void BindSFGItemCode()
        {
            try
            {
                blobj = new BL_WIPComponentReelPrinting();
                DataTable dt = blobj.dtBindSFGItemCode();
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(drpSFGItemCode, dt, true);
                    drpSFGItemCode.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }

        string Message = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
            Response.AddHeader("Pragma", "no-cache");
            Response.AddHeader("Expires", "0");

            if (Session["usertype"].ToString() != "ADMIN")
            {
                string _strRights = CommonHelper.GetRights("COMPONENT FORMING", (DataTable)Session["USER_RIGHTS"]);
                CommonHelper._strRights = _strRights.Split('^');
                if (CommonHelper._strRights[0] == "0")  //Check view rights
                {
                    Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                }
            }
            try
            {
                if (!IsPostBack)
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
                    BindSFGItemCode();
                    txtFormingToolID.Focus();
                    txtFormingToolID.ReadOnly = false;
                    txtBarcode.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void ValidateToolID()
        {
            try
            {
                blobj = new BL_WIPComponentReelPrinting();
                string sSFGitemCode = drpSFGItemCode.SelectedItem.Text;
                string sResult = blobj.ValidateFormingTool(txtFormingToolID.Text.Trim(), sSFGitemCode);
                if (sResult.StartsWith("SUCCESS~"))
                {
                    BL_Common objblcommon = new BL_Common();
                    string sResult1 = objblcommon.sShowNotificationTool(txtFormingToolID.Text.Trim());
                    if (sResult1.Length > 0)
                    {
                        CommonHelper.ShowMessage("You already consume more than 80%, Please be ready", msgerror, CommonHelper.MessageType.Notification.ToString());
                    }
                    drpSFGItemCode.Enabled = false;
                    drpSFGItemCode.CssClass = "form-control select2";
                    lblToolDesc.Text = sResult.Split('~')[1];
                    txtLotSize.Text = string.Empty;
                    txtFormingToolID.ReadOnly = true;
                    txtBarcode.ReadOnly = false;
                    txtBarcode.Focus();
                }
                else if (sResult.StartsWith("N~"))
                {
                    Message = sResult.Split('~')[1];
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    txtFormingToolID.Text = "";
                    txtFormingToolID.Focus();
                    return;
                }
                else if (sResult.StartsWith("ERROR~"))
                {
                    Message = sResult.Split('~')[1];
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    txtFormingToolID.Text = "";
                    txtFormingToolID.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        private void ValidateReelBarcode()
        {
            try
            {
                blobj = new BL_WIPComponentReelPrinting();
                string sResult = string.Empty;
                string sSFGItemCode = drpSFGItemCode.SelectedItem.Text;
                DataTable dt = blobj.ValidateReelBarcode(txtBarcode.Text.Trim(), sSFGItemCode, out sResult);
                if (sResult.Length > 0)
                {
                    if (sResult.StartsWith("SUCCESS~"))
                    {
                        Message = sResult.Split('~')[1];
                        txtLotSize.Text = string.Empty;
                        txtFormingToolID.ReadOnly = true;
                        txtBarcode.ReadOnly = true;
                        btnPrint.Focus();
                        DataTable dt1 = new DataTable();
                        divComponentReelPrinting.DataSource = dt1;
                        divComponentReelPrinting.DataBind();

                        divComponentReelPrinting.DataSource = dt;
                        divComponentReelPrinting.DataBind();
                        CommonHelper.ShowMessage(Message, msgsuccess, CommonHelper.MessageType.Success.ToString());
                    }
                    else if (sResult.StartsWith("N~"))
                    {
                        Message = sResult.Split('~')[1];
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                        txtLotSize.Text = string.Empty;
                        txtFormingToolID.ReadOnly = true;
                        txtBarcode.ReadOnly = false;
                        txtBarcode.Text = "";
                        txtBarcode.Focus();
                        DataTable dt1 = new DataTable();
                        divComponentReelPrinting.DataSource = dt1;
                        divComponentReelPrinting.DataBind();
                        return;
                    }
                    else if (sResult.StartsWith("ERROR~"))
                    {
                        Message = sResult.Split('~')[1];
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                        txtLotSize.Text = string.Empty;
                        txtFormingToolID.ReadOnly = true;
                        txtBarcode.ReadOnly = false;
                        txtBarcode.Text = "";
                        txtBarcode.Focus();
                        DataTable dt1 = new DataTable();
                        divComponentReelPrinting.DataSource = dt1;
                        divComponentReelPrinting.DataBind();
                        return;
                    }
                }
                else
                {
                    CommonHelper.ShowMessage("No result found for scanned barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtLotSize.Text = string.Empty;
                    txtFormingToolID.ReadOnly = true;
                    txtBarcode.ReadOnly = false;
                    txtBarcode.Text = "";
                    txtBarcode.Focus();
                    DataTable dt1 = new DataTable();
                    divComponentReelPrinting.DataSource = dt1;
                    divComponentReelPrinting.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        protected void txtFormingToolID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (dvPrintergrup.Visible == true && drpPrinterName.SelectedIndex == 0 )
                {
                    CommonHelper.ShowMessage("Please select printer", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpPrinterName.Focus();
                    return;
                }
                if (drpSFGItemCode.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select SFG Item code", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpSFGItemCode.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtFormingToolID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan forming tool id", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtFormingToolID.Focus();
                    return;
                }
                ValidateToolID();
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
                if (string.IsNullOrEmpty(txtFormingToolID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan forming tool id", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtFormingToolID.Focus();
                    return;
                }
                if (drpSFGItemCode.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select SFG Item code", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpSFGItemCode.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtBarcode.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan or enter reel barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBarcode.Focus();
                    return;
                }
                ValidateReelBarcode();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            if (dvPrintergrup.Visible == true && drpPrinterName.SelectedIndex == 0)
            {
                CommonHelper.ShowMessage("Please select printer", msgerror, CommonHelper.MessageType.Error.ToString());
                drpPrinterName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtFormingToolID.Text.Trim()))
            {
                CommonHelper.ShowMessage("Please scan forming tool id", msgerror, CommonHelper.MessageType.Error.ToString());
                txtFormingToolID.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtBarcode.Text.Trim()))
            {
                CommonHelper.ShowMessage("Please scan or enter reel barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                txtBarcode.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtLotSize.Text.Trim()))
            {
                CommonHelper.ShowMessage("Please enter lot size", msgerror, CommonHelper.MessageType.Error.ToString());
                txtLotSize.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtNoOfLabels.Text.Trim()))
            {
                CommonHelper.ShowMessage("Please enter no of labels", msgerror, CommonHelper.MessageType.Error.ToString());
                txtNoOfLabels.Focus();
                return;
            }
            if (divComponentReelPrinting.Rows.Count == 0)
            {
                CommonHelper.ShowMessage("No details found for printing", msgerror, CommonHelper.MessageType.Error.ToString());
                txtBarcode.ReadOnly = true;
                txtBarcode.Text = ""; txtBarcode.Focus();
            }
            if (Convert.ToInt32(txtLotSize.Text) == 0)
            {
                CommonHelper.ShowMessage("Please enter valid lot size", msgerror, CommonHelper.MessageType.Error.ToString());
                txtLotSize.Text = string.Empty;
                txtLotSize.Focus();
                return;
            }
            if (Convert.ToInt32(txtNoOfLabels.Text) == 0)
            {
                CommonHelper.ShowMessage("Please enter valid no of labels", msgerror, CommonHelper.MessageType.Error.ToString());
                txtNoOfLabels.Text = string.Empty;
                txtNoOfLabels.Focus();
                return;
            }
            int iQTY = 0;
            decimal dQty = 0;
            int iSize = 0;
            int iNoOfLabels = 1;
            string PartDesc = string.Empty;
            string sPartCode = string.Empty;
            string sBatchNo = string.Empty;
            string sPONO = string.Empty;
            string sReservationSlipNo = string.Empty;
            string sLineCode = Session["LINECODE"].ToString();
            string sUserID = Session["UserID"].ToString();
            foreach (GridViewRow dr in divComponentReelPrinting.Rows)
            {
                sPartCode = divComponentReelPrinting.Rows[dr.RowIndex].Cells[0].Text;
                PartDesc = divComponentReelPrinting.Rows[dr.RowIndex].Cells[1].Text;
                dQty = Convert.ToDecimal(divComponentReelPrinting.Rows[dr.RowIndex].Cells[3].Text);
                sBatchNo = divComponentReelPrinting.Rows[dr.RowIndex].Cells[7].Text;
                sPONO = divComponentReelPrinting.Rows[dr.RowIndex].Cells[8].Text;
                sReservationSlipNo = divComponentReelPrinting.Rows[dr.RowIndex].Cells[6].Text;
                break;
            }
            try
            {
                iQTY = Convert.ToInt32(dQty);
                iSize = Convert.ToInt32(txtLotSize.Text);
                iNoOfLabels = Convert.ToInt32(txtNoOfLabels.Text);
                if ((iNoOfLabels * iSize) > iQTY)
                {
                    CommonHelper.ShowMessage("Please enter valid no of labels, Multiplication of labels and lot size can not be greater than available qty ", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtNoOfLabels.Text = "1";
                    txtNoOfLabels.Focus();
                    return;
                }               
                blobj = new BL_WIPComponentReelPrinting();
                string sResult = blobj.ComponentReelPrinting(
                    sPartCode, txtBarcode.Text, Session["UserID"].ToString(), iQTY,
                    drpPrinterName.Text, txtFormingToolID.Text, lblToolDesc.Text, iSize,
                    PartDesc, sBatchNo, sPONO, iNoOfLabels,drpSFGItemCode.Text, sReservationSlipNo, sUserID, sLineCode
                    , Session["SiteCode"].ToString()
                    );
                if (sResult.StartsWith("SUCCESS~"))
                {
                    Message = sResult.Split('~')[1];
                    txtLotSize.Text = string.Empty;
                    txtNoOfLabels.Text = "1";
                    DataTable dt = new DataTable();
                    divComponentReelPrinting.DataSource = dt;
                    divComponentReelPrinting.DataBind();
                    txtBarcode.Text = string.Empty;
                    txtBarcode.ReadOnly = false;
                    CommonHelper.ShowMessage(Message, msgsuccess, CommonHelper.MessageType.Success.ToString());
                }
                else if (sResult.StartsWith("N~"))
                {
                    Message = sResult.Split('~')[1];
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());                    
                    return;
                }
                else if (sResult.StartsWith("ERROR~"))
                {
                    Message = sResult.Split('~')[1];
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());                    
                    return;
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
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            lblToolDesc.Text = "";
            txtNoOfLabels.Text = "1";
            txtLotSize.Text = string.Empty;
            txtFormingToolID.Text = "";
            txtFormingToolID.ReadOnly = false;
            txtFormingToolID.Focus();
            txtBarcode.ReadOnly = true;
            txtBarcode.Text = "";
            DataTable dt = new DataTable();
            divComponentReelPrinting.DataSource = dt;
            divComponentReelPrinting.DataBind();
            drpSFGItemCode.Enabled = true;
            drpSFGItemCode.CssClass = "form-control select2";
        }
    }
}