using Common;
using System;
using System.Data;
using BusinessLayer.WIP;
using System.Linq;
using BusinessLayer;

namespace SGST.INE.WIP
{
    public partial class WIPBarcodeMapping : System.Web.UI.Page
    {
        static DataTable dtLaserFileData;
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
                string _strRights = CommonHelper.GetRights("LINE PCB SN MAPPING", (DataTable)Session["USER_RIGHTS"]);
                CommonHelper._strRights = _strRights.Split('^');
                if (CommonHelper._strRights[0] == "0")  //Check view rights
                {
                    Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                }
            }
            Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
            Response.AddHeader("Pragma", "no-cache");
            Response.AddHeader("Expires", "0");
            try
            {
                if (!IsPostBack)
                {
                    dtLaserFileData = new DataTable();
                    dtLaserFileData.Columns.Add("PART_BARCODE");
                    BindWorkOrderNo();
                    txtBarcode.Focus();
                    txtBarcode.Attributes.Add("placeholder", "Scan Barcode");
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
        private void BindWorkOrderNo()
        {
            try
            {
                txtBarcode.Text = "";
                txtBarcode.ReadOnly = false;
                txtMachineID.Text = string.Empty;
                txtMachineID.Focus();
                txtMachineID.Enabled = true;
                txtMachineID.Height = 35;
                dtLaserFileData.Rows.Clear();
                drpFGItemCode.Enabled = true;

                lblCustomerCode.Text = string.Empty;
                dtLaserFileData.Rows.Clear();
                drpFGItemCode.Items.Clear();
                drpWorkOrderNo.Items.Clear();
                gvScannedBarcodeData.DataSource = null;
                gvScannedBarcodeData.DataBind();
                BL_WIPSFGAssembly blobj = new BL_WIPSFGAssembly();
                DataTable dtWorkOrderNo = blobj.BindWorkOrderNo(Session["SiteCode"].ToString());
                if (dtWorkOrderNo.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(drpWorkOrderNo, dtWorkOrderNo, true);
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        private void BindFGitemCode()
        {
            try
            {
                if (drpWorkOrderNo.SelectedIndex > 0)
                {
                    BL_WIPSFGAssembly blobj = new BL_WIPSFGAssembly();
                    DataTable dtWorkOrderNo = blobj.BindFGItemCode(drpWorkOrderNo.SelectedItem.Text, Session["SiteCode"].ToString()
                        , txtMachineID.Text.Trim(), Session["LINECODE"].ToString()
                        );
                    if (dtWorkOrderNo.Rows.Count > 0)
                    {
                        clsCommon.FillComboBox(drpFGItemCode, dtWorkOrderNo, true);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        protected void drpWorkOrderNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtBarcode.Text = "";
                txtBarcode.ReadOnly = false;
                txtMachineID.Text = string.Empty;
                txtMachineID.Focus();
                txtMachineID.Enabled = true;
                txtMachineID.Height = 35;
                dtLaserFileData.Rows.Clear();
                drpFGItemCode.Enabled = true;
                lblCustomerCode.Text = string.Empty;
                dtLaserFileData.Rows.Clear();
                drpFGItemCode.Items.Clear();
                gvScannedBarcodeData.DataSource = null;
                gvScannedBarcodeData.DataBind();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void drpFGItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblCustomerCode.Text = string.Empty;
                txtBarcode.Text = "";
                txtBarcode.ReadOnly = false;
                txtBarcode.Text = "";
                txtBarcode.ReadOnly = false;
                dtLaserFileData.Rows.Clear();
                if (drpFGItemCode.SelectedIndex > 0)
                {
                    BL_WIPSFGAssembly blobj = new BL_WIPSFGAssembly();
                    DataTable dt = new DataTable();
                    dt = blobj.GetCustomerCode(drpFGItemCode.Text, Session["SiteCode"].ToString());
                    if (dt.Rows.Count > 0)
                    {
                        lblCustomerCode.Text = dt.Rows[0][0].ToString();
                        hidNoOFSCAN.Value = dt.Rows[0][1].ToString();
                    }
                }
                else
                {
                    CommonHelper.ShowMessage("Customer code not found against selected fg item code, please select different fg item code", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpFGItemCode.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void ScannedReelBarcode()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (drpWorkOrderNo.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select work order no.", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpWorkOrderNo.Focus();
                    return;
                }
                if (drpFGItemCode.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select fg item code", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpFGItemCode.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtBarcode.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan reel barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBarcode.Focus();
                    return;
                }
                bool contains = dtLaserFileData.AsEnumerable().Any(row => txtBarcode.Text.Trim() == row.Field<String>("PART_BARCODE"));
                if (contains == true)
                {
                    CommonHelper.ShowMessage("Duplicate barcode scanned", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBarcode.Text = string.Empty;
                    txtBarcode.Focus();
                    gvScannedBarcodeData.DataSource = dtLaserFileData;
                    gvScannedBarcodeData.DataBind();
                    return;
                }
                if (dtLaserFileData.Rows.Count >= Convert.ToInt32(hidNoOFSCAN.Value))
                {
                    CommonHelper.ShowMessage("No of required barcode already mapped with selected fg item code", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBarcode.Text = string.Empty;
                    txtBarcode.Focus();
                    gvScannedBarcodeData.DataSource = dtLaserFileData;
                    gvScannedBarcodeData.DataBind();
                }
                BL_WIPSFGAssembly blobj = new BL_WIPSFGAssembly();
                DataTable dt = blobj.ScanReelBarcode(txtBarcode.Text.Trim(), drpFGItemCode.Text, lblCustomerCode.Text
                    , Session["SiteCode"].ToString(), Session["LINECODE"].ToString(), drpWorkOrderNo.Text
                    , txtMachineID.Text.Trim()                    
                    );
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString().StartsWith("N~") || dt.Rows[0][0].ToString().StartsWith("ERROR~"))
                    {
                        CommonHelper.ShowMessage(dt.Rows[0][0].ToString().Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        txtBarcode.Text = string.Empty;
                        txtBarcode.Focus();
                        return;
                    }
                    dtLaserFileData.Rows.Add(txtBarcode.Text.Trim());
                    drpFGItemCode.Enabled = false;
                    drpWorkOrderNo.Enabled = false;
                    gvScannedBarcodeData.DataSource = dtLaserFileData;
                    gvScannedBarcodeData.DataBind();
                    txtBarcode.Text = string.Empty;
                    txtBarcode.Focus();
                }
                else
                {
                    CommonHelper.ShowMessage("No data found for scanned barcode .", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBarcode.Text = string.Empty;
                    txtBarcode.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        protected void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                ScannedReelBarcode();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void txtPCBBarcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (dvPrintergrup.Visible == true && drpPrinterName.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select printer", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpPrinterName.Focus();
                    return;
                }
                if (drpWorkOrderNo.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select work order no.", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpWorkOrderNo.Focus();
                    return;
                }
                if (drpFGItemCode.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select fg item code", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpFGItemCode.Focus();
                    return;
                }
                if (dtLaserFileData.Rows.Count == 0)
                {
                    CommonHelper.ShowMessage("Please scan at least one barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBarcode.Focus();
                    return;
                }
                string sDesignerFormat = string.Empty;
                BL_WIP_LaserMachine blobj1 = new BL_WIP_LaserMachine();
                DataSet dtFGItemCodeLH = blobj1.BindCustomerPartCode(drpFGItemCode.Text, Session["SiteCode"].ToString());
                if (dtFGItemCodeLH.Tables.Count > 0)
                {
                    sDesignerFormat = dtFGItemCodeLH.Tables[1].Rows[0][0].ToString();
                }
                string sLineCode = Session["LINECODE"].ToString();
                string sUserID = Session["UserID"].ToString();
                BL_WIPSFGAssembly blobj = new BL_WIPSFGAssembly();
                string sResult = blobj.PCBPrinting(dtLaserFileData, drpFGItemCode.Text,
                    Session["SiteCode"].ToString(), sLineCode, sUserID, txtMachineID.Text.Trim(),
                    lblCustomerCode.Text, drpWorkOrderNo.Text, sDesignerFormat);
                if (sResult.Length > 0)
                {
                    if (sResult.StartsWith("SUCCESS~"))
                    {
                        dtLaserFileData.Rows.Clear();
                        Message = sResult.Split('~')[1].ToString();
                        CommonHelper.ShowMessage("Barcode mapped successfully : " + Message, msgsuccess, CommonHelper.MessageType.Success.ToString());
                        txtBarcode.Text = string.Empty;
                        txtBarcode.Focus();                       
                        dtLaserFileData.Rows.Clear();
                        gvScannedBarcodeData.DataSource = null;
                        gvScannedBarcodeData.DataBind();     
                    }
                    else if (sResult.StartsWith("Duplicate~"))
                    {
                        Message = sResult.Split('~')[1].ToString();
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                        txtBarcode.Text = string.Empty;
                        txtBarcode.Focus();
                    }
                    else if (sResult.StartsWith("N~"))
                    {
                        Message = sResult.Split('~')[1].ToString();
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                        txtBarcode.Text = string.Empty;
                        txtBarcode.Focus();
                    }
                    else
                    {
                        dtLaserFileData.Rows.Clear();
                        txtBarcode.Text = string.Empty;
                        txtBarcode.ReadOnly = false;
                        txtBarcode.Focus();
                        DataTable dt = new DataTable();
                        gvScannedBarcodeData.DataSource = null;
                        gvScannedBarcodeData.DataBind();
                        gvScannedBarcodeData.DataSource = dt;
                        gvScannedBarcodeData.DataBind();
                        Message = sResult.Split('~')[1].ToString();
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                }
                else
                {
                    CommonHelper.ShowMessage("No result found, Please try again", msgerror, CommonHelper.MessageType.Error.ToString());
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
                txtPCBBarcode_TextChanged(null, null);
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
                txtBarcode.Text = "";
                txtBarcode.ReadOnly = false;
                txtBarcode.Text = "";
                txtBarcode.ReadOnly = false;
                dtLaserFileData.Rows.Clear();
                BindWorkOrderNo();
                gvScannedBarcodeData.DataSource = null;
                gvScannedBarcodeData.DataBind();
                txtMachineID.Text = string.Empty;
                txtMachineID.Focus();
                txtMachineID.Enabled = true;
                txtMachineID.Height = 35;
                dtLaserFileData.Rows.Clear();
                drpFGItemCode.Enabled = true;
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void txtMachineID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (string.IsNullOrEmpty(txtMachineID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Scan Machine", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtMachineID.Focus();
                    return;
                }
                BL_WIPSFGAssembly blobj = new BL_WIPSFGAssembly();
                DataTable dt = blobj.ValidateMachine(txtMachineID.Text.Trim(), Session["LINECODE"].ToString()
                    , Session["SiteCode"].ToString()
                    );
                if (dt.Rows.Count > 0)
                {
                    CommonHelper.ShowMessage("Valid machine", msgsuccess, CommonHelper.MessageType.Success.ToString());
                    BindFGitemCode();
                }
                else
                {
                    CommonHelper.ShowMessage("Invalid machine", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtMachineID.Focus();
                    txtMachineID.Text = string.Empty;
                    drpFGItemCode.Items.Clear();
                    return;
                }
            }
            catch (Exception ex)
            {
                txtMachineID.Focus();
                txtMachineID.Text = string.Empty;
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}