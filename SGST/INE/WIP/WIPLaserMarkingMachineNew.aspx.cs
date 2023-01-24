using Common;
using System;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;
using BusinessLayer.WIP;

namespace SGST.INE.WIP
{
    public partial class WIPLaserFileGenerationNew : System.Web.UI.Page
    {
        BL_WIP_LaserMachine blobj = new BL_WIP_LaserMachine();
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Signin/v1/Login.aspx?Session=Null");
            }
        }

        string Message = "";
        string _FIFORequired = ConfigurationManager.AppSettings["_FIFOREQUIRED"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usertype"].ToString() != "ADMIN")
            {
                string _strRights = CommonHelper.GetRights("PCB SN GENERATION", (DataTable)Session["USER_RIGHTS"]);
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
                    blobj = new BL_WIP_LaserMachine();
                    DataTable dtIssue = blobj.BindProcess(Session["SiteCode"].ToString());
                    if (dtIssue.Rows.Count > 0)
                    {
                        clsCommon.FillComboBox(drpType, dtIssue, true);
                    }
                    txtScanMachineBarcode.Focus();
                    lblScanRH.InnerText = "Scan Barcode";
                    txtBarcode.Attributes.Add("placeholder", "Scan Barcode");
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowMessage(ex.Message, msgerror, CommonHelper.MessageType.Error.ToString());
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void BindCustomerCode(string sFGItemCode, DropDownList drp)
        {
            try
            {
                if (sFGItemCode.Length > 0)
                {
                    blobj = new BL_WIP_LaserMachine();
                    DataSet dtFGItemCodeLH = blobj.BindCustomerPartCode(sFGItemCode, Session["SiteCode"].ToString());
                    if (dtFGItemCodeLH.Tables.Count > 0)
                    {
                        DataTable dtCustomerCode = new DataTable();
                        dtCustomerCode = dtFGItemCodeLH.Tables[0];
                        if (dtCustomerCode.Rows.Count > 0)
                        {
                            clsCommon.FillComboBox(drp, dtCustomerCode, true);
                            drpCustomerCode.SelectedIndex = 1;
                        }
                        string sDesignerFormat = dtFGItemCodeLH.Tables[1].Rows[0][0].ToString();
                        string sPrefix = dtFGItemCodeLH.Tables[1].Rows[0][1].ToString();
                        Session["sDesignerFormat"] = sDesignerFormat;
                        Session["sPrefix"] = sPrefix;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void ScannedReelBarcode()
        {

            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (string.IsNullOrEmpty(txtScanMachineBarcode.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan machine barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtScanMachineBarcode.Focus();
                    return;
                }
                if (drpType.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select type.", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpType.Focus();
                    return;
                }
                if (drpIssueSlipNo.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select work order no.", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpIssueSlipNo.Focus();
                    return;
                }
                if (drpFGItemCodeRH.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select fg item code", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpFGItemCodeRH.Focus();
                    return;
                }
                if (drpCustomerCode.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select customer code", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpCustomerCode.Focus();
                    return;
                }
                if (drpType.Text == "TMO_PROCESS")
                {
                    if (Convert.ToInt32(lblAvailableQty.Text) == 0)
                    {
                        CommonHelper.ShowMessage("Qty is not available for generation the serial no", msgerror, CommonHelper.MessageType.Error.ToString());
                        txtBarcode.Text = string.Empty;
                        txtBarcode.Focus();
                        return;
                    }
                }
                if (string.IsNullOrEmpty(txtBarcode.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan reel barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBarcode.Focus();
                    return;
                }
                int iNoOfRMScan = 1;
                int iNoOFFGScan = 1;
                int iPCBQty = 1;
                DataTable dtIssue = blobj.GetProcessDetails(Session["SiteCode"].ToString(), drpType.Text);
                if (dtIssue.Rows.Count > 0)
                {
                    iNoOFFGScan = Convert.ToInt32(dtIssue.Rows[0][0].ToString());
                    iNoOfRMScan = Convert.ToInt32(dtIssue.Rows[0][1].ToString());
                    iPCBQty = Convert.ToInt32(dtIssue.Rows[0][2].ToString());
                }
                if (ViewState["PARTBARCODE"] != null)
                {
                    DataTable dtPacket = (DataTable)ViewState["PARTBARCODE"];
                    DataRow[] foundAuthors = dtPacket.Select("PARTBARCODE = '" + txtBarcode.Text.Trim() + "'");
                    if (foundAuthors.Length > 0)
                    {
                        CommonHelper.ShowMessage("Duplicate barcode scanned", msgerror, CommonHelper.MessageType.Error.ToString());
                        txtBarcode.Text = string.Empty;
                        txtBarcode.Focus();
                        return;
                    }
                    if (iNoOfRMScan * iNoOFFGScan == dtPacket.Rows.Count)
                    {
                        CommonHelper.ShowMessage("All the required rm barcode already scanned, Please press generate button to print the label", msgerror, CommonHelper.MessageType.Error.ToString());
                        txtBarcode.ReadOnly = false;
                        txtBarcode.Text = string.Empty;
                        txtBarcode.Focus();
                        return;
                    }
                    DataTable dt1 = dtPacket.DefaultView.ToTable(true, "FGITEMCODE"); ;
                    if (iNoOFFGScan < dt1.Rows.Count)
                    {
                        CommonHelper.ShowMessage("Selected type allow only single FG item code, Please select previous fg item code", msgerror, CommonHelper.MessageType.Error.ToString());
                        drpFGItemCodeRH.Focus();
                        txtBarcode.ReadOnly = false;
                        txtBarcode.Text = string.Empty;
                        txtBarcode.Focus();
                        return;
                    }
                }
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.MethodBase.GetCurrentMethod().Name,
                  "PCB Sn Generation Reel Barcode Scan :" + txtBarcode.Text.Trim()
                  + ", FG ITEM CODE :" + drpFGItemCodeRH.Text
                  + ", Work Order No :" + drpIssueSlipNo.SelectedItem.Text
                  + ", User ID :" + Session["UserID"].ToString()
                  );
                blobj = new BL_WIP_LaserMachine();
                DataTable dt = blobj.ScanReelBarcode(txtBarcode.Text.Trim(), drpFGItemCodeRH.Text, txtScanMachineBarcode.Text
                    , drpCustomerCode.Text, drpIssueSlipNo.SelectedItem.Text
                    , Session["SiteCode"].ToString()
                    );
                if (dt.Rows.Count > 0)
                {
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.MethodBase.GetCurrentMethod().Name,
                   "PCB Sn Generation Reel Barcode Scan :" + txtBarcode.Text.Trim()
                   + ", Output fOund :" + dt.Rows[0][0].ToString()
                   );
                    if (dt.Rows[0][0].ToString().StartsWith("N~") || dt.Rows[0][0].ToString().StartsWith("ERROR~"))
                    {
                        CommonHelper.ShowMessage(dt.Rows[0][0].ToString().Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        txtBarcode.ReadOnly = false;
                        txtBarcode.Text = string.Empty;
                        txtBarcode.Focus();
                        if (drpType.Text == "TOP_SIDED" || drpType.Text == "TMO_PROCESS")
                        {
                            dvLaserFileData.DataSource = null;
                            dvLaserFileData.DataBind();
                        }
                        return;
                    }
                    if (dt.Rows[0]["PART_TYPE"].ToString().ToUpper() != "PCB")
                    {
                        CommonHelper.ShowMessage("Scanned barcode not a valid type of PCB, Please scan valid barcode ", msgerror, CommonHelper.MessageType.Error.ToString());
                        txtBarcode.ReadOnly = false;
                        txtBarcode.Text = string.Empty;
                        txtBarcode.Focus();
                        if (drpType.Text == "TOP_SIDED" || drpType.Text == "TMO_PROCESS")
                        {
                            dvLaserFileData.DataSource = null;
                            dvLaserFileData.DataBind();
                        }
                        return;
                    }
                    if (dt.Rows[0]["ISSUE_SLIP_NO"].ToString().ToUpper() != drpIssueSlipNo.Text.ToUpper())
                    {
                        CommonHelper.ShowMessage("Scanned barcode not matched with selected work order no, Please scan valid barcode ", msgerror, CommonHelper.MessageType.Error.ToString());
                        txtBarcode.ReadOnly = false;
                        txtBarcode.Text = string.Empty;
                        txtBarcode.Focus();
                        if (drpType.Text == "TOP_SIDED" || drpType.Text == "TMO_PROCESS")
                        {
                            dvLaserFileData.DataSource = null;
                            dvLaserFileData.DataBind();
                        }
                        return;
                    }
                    else if (dt.Rows[0]["STATUS"].ToString().ToUpper() == "2")
                    {
                        CommonHelper.ShowMessage("Barcode is already scanned", msgerror, CommonHelper.MessageType.Error.ToString());
                        txtBarcode.ReadOnly = false;
                        txtBarcode.Text = string.Empty;
                        txtBarcode.Focus();
                        if (drpType.Text == "TOP_SIDED" || drpType.Text == "TMO_PROCESS")
                        {
                            dvLaserFileData.DataSource = null;
                            dvLaserFileData.DataBind();
                        }
                        return;
                    }
                    if (_FIFORequired == "1")
                    {
                        DataTable dtValidateTopBarcode = blobj.GetTopBarcode(dt.Rows[0]["PART_CODE"].ToString().ToUpper()
                            , Session["SiteCode"].ToString()
                            );
                        if (dtValidateTopBarcode.Rows.Count > 0)
                        {
                            if (dtValidateTopBarcode.Rows[0][0].ToString().ToUpper() != txtBarcode.Text.Trim().ToUpper())
                            {
                                CommonHelper.ShowMessage("As per fifo scanned barcode is invalid, Please scan : " + dtValidateTopBarcode.Rows[0][0].ToString(), msgerror, CommonHelper.MessageType.Error.ToString());
                                txtBarcode.ReadOnly = false;
                                txtBarcode.Text = string.Empty;
                                txtBarcode.Focus();
                                if (drpType.Text == "TOP_SIDED" || drpType.Text == "TMO_PROCESS")
                                {
                                    dvLaserFileData.DataSource = null;
                                    dvLaserFileData.DataBind();
                                }
                                return;
                            }
                        }
                    }
                    if (Convert.ToInt32(dt.Rows[0]["PCB_ARRAY_SIZE"].ToString()) == 0)
                    {
                        CommonHelper.ShowMessage("Array size not found in part master against partcode of scanned barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                        txtBarcode.Text = string.Empty;
                        txtBarcode.ReadOnly = false;
                        txtBarcode.Focus();
                        return;
                    }
                    if (drpPacketType.SelectedIndex == 1)
                    {
                        if (Convert.ToDecimal(dt.Rows[0]["REM_QTY"].ToString()) % Convert.ToDecimal(dt.Rows[0]["PCB_ARRAY_SIZE"].ToString()) == 0)
                        {
                            CommonHelper.ShowMessage("Packet qty is divisible by Pack size. Please select normal PCB ", msgerror, CommonHelper.MessageType.Error.ToString());
                            return;
                        }
                    }
                    if (drpType.Text == "TMO_PROCESS")
                    {
                        if (Convert.ToDecimal(lblAvailableQty.Text) < Convert.ToDecimal(dt.Rows[0]["REM_QTY"].ToString()))
                        {
                            CommonHelper.ShowMessage("You do not have sufficient available qty to generated the serial no ", msgerror, CommonHelper.MessageType.Error.ToString());
                            return;
                        }
                    }
                    string sDesignerFormat = string.Empty;
                    string sPrefix = string.Empty;
                    DataSet dtFGItemCodeLH = blobj.BindCustomerPartCode(drpFGItemCodeRH.Text, Session["SiteCode"].ToString());
                    if (dtFGItemCodeLH.Tables.Count > 0)
                    {
                        sDesignerFormat = dtFGItemCodeLH.Tables[1].Rows[0][0].ToString();
                        sPrefix = dtFGItemCodeLH.Tables[1].Rows[0][1].ToString();
                    }
                    DataTable dtPacket = new DataTable();
                    if (ViewState["PARTBARCODE"] == null)
                    {
                        dtPacket.Columns.Add("PART_CODE");
                        dtPacket.Columns.Add("REM_QTY");
                        dtPacket.Columns.Add("PCB_ARRAY_SIZE");
                        dtPacket.Columns.Add("BATCHNO");
                        dtPacket.Columns.Add("PONO");
                        dtPacket.Columns.Add("ISSUE_SLIP_NO");
                        dtPacket.Columns.Add("SUPPLIER_ID");
                        dtPacket.Columns.Add("PART_DESC");
                        dtPacket.Columns.Add("FGITEMCODE");
                        dtPacket.Columns.Add("CUSTOMERCODE");
                        dtPacket.Columns.Add("DESIGNFORMAT");
                        dtPacket.Columns.Add("PARTBARCODE");
                        dtPacket.Columns.Add("PREFIX");
                        DataRow dr1 = dtPacket.NewRow();
                        dr1["PART_CODE"] = dt.Rows[0]["PART_CODE"].ToString();
                        dr1["REM_QTY"] = dt.Rows[0]["REM_QTY"].ToString();
                        dr1["PCB_ARRAY_SIZE"] = dt.Rows[0]["PCB_ARRAY_SIZE"].ToString();
                        dr1["BATCHNO"] = dt.Rows[0]["BATCHNO"].ToString();
                        dr1["PONO"] = dt.Rows[0]["PONO"].ToString();
                        dr1["ISSUE_SLIP_NO"] = drpIssueSlipNo.Text.Trim();
                        dr1["SUPPLIER_ID"] = dt.Rows[0]["SUPPLIER_ID"].ToString();
                        dr1["PART_DESC"] = dt.Rows[0]["PART_DESC"].ToString();
                        dr1["FGITEMCODE"] = drpFGItemCodeRH.Text.Trim();
                        dr1["CUSTOMERCODE"] = drpCustomerCode.Text.Trim();
                        dr1["DESIGNFORMAT"] = sDesignerFormat.Trim();
                        dr1["PARTBARCODE"] = txtBarcode.Text.Trim();
                        dr1["PREFIX"] = sPrefix;
                        dtPacket.Rows.Add(dr1);
                        ViewState["PARTBARCODE"] = dtPacket;
                    }
                    else
                    {
                        dtPacket = (DataTable)ViewState["PARTBARCODE"];
                        DataRow dr1 = dtPacket.NewRow();
                        dr1["PART_CODE"] = dt.Rows[0]["PART_CODE"].ToString();
                        dr1["REM_QTY"] = dt.Rows[0]["REM_QTY"].ToString();
                        dr1["PCB_ARRAY_SIZE"] = dt.Rows[0]["PCB_ARRAY_SIZE"].ToString();
                        dr1["BATCHNO"] = dt.Rows[0]["BATCHNO"].ToString();
                        dr1["PONO"] = dt.Rows[0]["PONO"].ToString();
                        dr1["ISSUE_SLIP_NO"] = drpIssueSlipNo.Text.Trim();
                        dr1["SUPPLIER_ID"] = dt.Rows[0]["SUPPLIER_ID"].ToString();
                        dr1["PART_DESC"] = dt.Rows[0]["PART_DESC"].ToString();
                        dr1["FGITEMCODE"] = drpFGItemCodeRH.Text.Trim();
                        dr1["CUSTOMERCODE"] = drpCustomerCode.Text.Trim();
                        dr1["DESIGNFORMAT"] = sDesignerFormat.Trim();
                        dr1["PARTBARCODE"] = txtBarcode.Text.Trim();
                        dr1["PREFIX"] = sPrefix;
                        if (dt.Rows[0]["REM_QTY"].ToString() != dtPacket.Rows[0]["REM_QTY"].ToString())
                        {
                            CommonHelper.ShowMessage("Packet qty of all the barcode should be same.", msgerror, CommonHelper.MessageType.Error.ToString());
                            txtBarcode.ReadOnly = false;
                            txtBarcode.Text = string.Empty;
                            txtBarcode.Focus();
                            return;
                        }
                        if (dt.Rows[0]["PCB_ARRAY_SIZE"].ToString() != dtPacket.Rows[0]["PCB_ARRAY_SIZE"].ToString())
                        {
                            CommonHelper.ShowMessage("Array size of all the barcode should be same.", msgerror, CommonHelper.MessageType.Error.ToString());
                            txtBarcode.ReadOnly = false;
                            txtBarcode.Text = string.Empty;
                            txtBarcode.Focus();
                            return;
                        }
                        dtPacket.Rows.Add(dr1);
                        ViewState["PARTBARCODE"] = dtPacket;
                    }
                    dvLaserFileData.DataSource = dtPacket;
                    dvLaserFileData.DataBind();
                    txtBarcode.Text = string.Empty;
                    if (dtPacket.Rows.Count == iNoOfRMScan)
                    {
                        btnPrint.Focus();
                    }
                    else
                    {
                        drpIssueSlipNo.Focus();
                    }
                }
                else
                {
                    CommonHelper.ShowMessage("Part mapping not found against selected FG .", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBarcode.ReadOnly = false;
                    txtBarcode.Text = string.Empty;
                    txtBarcode.Focus();
                    if (drpType.Text == "TOP_SIDED" || drpType.Text == "TMO_PROCESS")
                    {
                        dvLaserFileData.DataSource = null;
                        dvLaserFileData.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        private void PrintLabel(bool bManualEntery)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                int iQty = 0;
                decimal dQty = 0;
                int iArraySize = 0;
                string sPONO = string.Empty;
                string Part_Code = string.Empty;
                string sBatchNo = string.Empty;
                string CustomerPartCode_RH = string.Empty;
                string sReelBarcode = string.Empty;
                foreach (GridViewRow item in dvLaserFileData.Rows)
                {
                    Part_Code = dvLaserFileData.Rows[item.RowIndex].Cells[1].Text;
                    dQty = Convert.ToDecimal(dvLaserFileData.Rows[item.RowIndex].Cells[2].Text);
                    iArraySize = Convert.ToInt32(dvLaserFileData.Rows[item.RowIndex].Cells[3].Text);
                    sBatchNo = dvLaserFileData.Rows[item.RowIndex].Cells[4].Text;
                    sPONO = dvLaserFileData.Rows[item.RowIndex].Cells[5].Text;
                    CustomerPartCode_RH = dvLaserFileData.Rows[item.RowIndex].Cells[9].Text;
                    sReelBarcode = dvLaserFileData.Rows[item.RowIndex].Cells[10].Text;
                    break;
                }
                iQty = Convert.ToInt32(dQty);
                if (iArraySize == 0)
                {
                    CommonHelper.ShowMessage("Array size not found in part master against partcode of scanned barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBarcode.Text = string.Empty;
                    txtBarcode.ReadOnly = false;
                    txtBarcode.Focus();
                    return;
                }
                string sPCBType = "N";
                if (drpPacketType.SelectedIndex == 1)
                {
                    if (iQty % iArraySize == 0)
                    {
                        CommonHelper.ShowMessage("Packet qty is divisible by Pack size. Please select normal PCB ", msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                    else
                    {
                        sPCBType = "C";
                        iArraySize = 1;
                    }
                }
                if (iQty % iArraySize != 0)
                {
                    string sResult1 = "N~Packet size is not divisible by array size,Please check the array size(" + iArraySize.ToString() + ") or packet size(" + iQty.ToString() + ") for scanned barcode ";
                    CommonHelper.ShowMessage(sResult1, msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBarcode.Focus();
                    txtBarcode.Text = "";
                    txtBarcode.ReadOnly = false;
                    return;
                }

                string sDesignerFormat = string.Empty;
                string sPrefix = string.Empty;
                DataSet dtFGItemCodeLH = blobj.BindCustomerPartCode(drpFGItemCodeRH.Text, Session["SiteCode"].ToString());
                if (dtFGItemCodeLH.Tables.Count > 0)
                {
                    sDesignerFormat = dtFGItemCodeLH.Tables[1].Rows[0][0].ToString();
                    sPrefix = dtFGItemCodeLH.Tables[1].Rows[0][1].ToString();
                }
                blobj = new BL_WIP_LaserMachine();
                string sSiteCode = Session["SiteCode"].ToString();
                string sResult = string.Empty;
                int iNoOfRMScan = 1;
                int iNoOFFGScan = 1;
                int iPCBQty = 1;
                DataTable dtIssue = blobj.GetProcessDetails(Session["SiteCode"].ToString(), drpType.Text);
                if (dtIssue.Rows.Count > 0)
                {
                    iNoOFFGScan = Convert.ToInt32(dtIssue.Rows[0][0].ToString());
                    iNoOfRMScan = Convert.ToInt32(dtIssue.Rows[0][1].ToString());
                    iPCBQty = Convert.ToInt32(dtIssue.Rows[0][2].ToString());
                }
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtMessage,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, " Barcode Scaning Type : " + drpType.Text);
                if (drpType.Text == "TMO_PROCESS")
                {
                    sResult = blobj.TMOGeneration(
                   sPONO, drpIssueSlipNo.SelectedItem.Text,
                   Part_Code, sBatchNo, iQty, iArraySize,
                   sReelBarcode, CustomerPartCode_RH, drpFGItemCodeRH.Text
                   , Session["SiteCode"].ToString(), Session["UserID"].ToString()
                   , Session["LINECODE"].ToString(), sDesignerFormat, sPCBType, iPCBQty
                   , sPrefix,""
                    );
                }
                else if (drpType.Text == "TOP_SIDED" || drpType.Text == "IJL_XF3Z")
                {
                    sResult = blobj.SinglePrintLasserSerailNosXLS(
                   sPONO, drpIssueSlipNo.SelectedItem.Text,
                   Part_Code, sBatchNo, iQty, iArraySize,
                   sReelBarcode, CustomerPartCode_RH, drpFGItemCodeRH.Text
                   , Session["SiteCode"].ToString(), Session["UserID"].ToString()
                   , Session["LINECODE"].ToString(), sDesignerFormat, sPCBType, iPCBQty
                   , sPrefix
                    );
                }
                else
                {
                    DataTable dtPacket = (DataTable)ViewState["PARTBARCODE"];
                    sResult = blobj.sLaserFileGenerate(
                    iQty, iArraySize,
                    sReelBarcode, drpCustomerCode.Text, drpFGItemCodeRH.Text
                    , Session["SiteCode"].ToString(), Session["UserID"].ToString()
                    , Session["LINECODE"].ToString(), sDesignerFormat, sPCBType, iPCBQty, dtPacket
                    , sPrefix
                     );
                }

                if (sResult.StartsWith("SUCCESS~"))
                {
                    Message = sResult.Split('~')[1].ToString();
                    CommonHelper.ShowMessage("PCB SN Generated Successfully", msgsuccess, CommonHelper.MessageType.Success.ToString());
                    txtBarcode.Text = string.Empty;
                    txtBarcode.ReadOnly = false;
                    txtBarcode.Focus();
                    DataTable dt = new DataTable();
                    dvLaserFileData.DataSource = dt;
                    dvLaserFileData.DataBind();
                    txtBarcode.Focus();
                    ViewState["PARTBARCODE"] = null;
                    if (drpType.Text == "TMO_PROCESS")
                    {
                        lblAvailableQty.Text = "0";
                        lblModel.Text = "";
                        blobj = new BL_WIP_LaserMachine();
                        DataTable dtPurchaseOrder = blobj.GetPurchaseOrderDetails(drpFGItemCodeRH.Text, Session["SiteCode"].ToString());
                        if (dtPurchaseOrder.Rows.Count > 0)
                        {
                            lblModel.Text = dtPurchaseOrder.Rows[0][1].ToString();
                            lblAvailableQty.Text = dtPurchaseOrder.Rows[0][0].ToString();
                        }
                    }
                }
                else
                {
                    txtBarcode.Text = string.Empty;
                    txtBarcode.ReadOnly = false;
                    txtBarcode.Focus();
                    Message = sResult.Split('~')[1].ToString();
                    if (Message.ToUpper().Contains("PRIMARY KEY"))
                    {
                        CommonHelper.ShowMessage("Generated SN already stored in database, Please check the log file or check the SN Logic", msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    if (Message.ToUpper().Contains("INPUT STRING"))
                    {
                        CommonHelper.ShowMessage("Generated SN is not in correct format, Please check the log file or check the SN Logic", msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    else
                    {
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                if (ex.Message.ToUpper().Contains("PRIMARY KEY"))
                {
                    CommonHelper.ShowMessage("Generated SN already stored in database, Please check the log file or check the SN Logic", msgerror, CommonHelper.MessageType.Error.ToString());
                }
                if (ex.Message.ToUpper().Contains("INPUT STRING"))
                {
                    CommonHelper.ShowMessage("Generated SN is not in correct format, Please check the log file or check the SN Logic", msgerror, CommonHelper.MessageType.Error.ToString());
                }
                else
                {
                    throw ex;
                }
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (string.IsNullOrEmpty(txtScanMachineBarcode.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan machine barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtScanMachineBarcode.Focus();
                    return;
                }
                if (dvLaserFileData.Rows.Count == 0)
                {
                    CommonHelper.ShowMessage("No Data found for print", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpType.Focus();
                    return;
                }
                int iNoOfRMScan = 1;
                int iNoOFFGScan = 1;
                int iPCBQty = 1;
                DataTable dtIssue = blobj.GetProcessDetails(Session["SiteCode"].ToString(), drpType.Text);
                if (dtIssue.Rows.Count > 0)
                {
                    iNoOFFGScan = Convert.ToInt32(dtIssue.Rows[0][0].ToString());
                    iNoOfRMScan = Convert.ToInt32(dtIssue.Rows[0][1].ToString());
                    iPCBQty = Convert.ToInt32(dtIssue.Rows[0][2].ToString());
                }
                DataTable dtPacket = (DataTable)ViewState["PARTBARCODE"];
                if (dtPacket.Rows.Count != (iNoOfRMScan * iNoOFFGScan))
                {
                    CommonHelper.ShowMessage("Please scan all the RM barcode as required", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpType.Focus();
                    return;
                }
                PrintLabel(false);
            }
            catch (Exception ex)
            {
                CommonHelper.ShowMessage(ex.Message, msgerror, CommonHelper.MessageType.Error.ToString());
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
                DataTable dt = new DataTable();
                dvLaserFileData.DataSource = dt;
                dvLaserFileData.DataBind();
                txtBarcode.Text = "";
                txtBarcode.ReadOnly = false;
                txtScanMachineBarcode.ReadOnly = false;
                txtScanMachineBarcode.Text = string.Empty;
                txtScanMachineBarcode.Focus();
                drpFGItemCodeRH.Enabled = true;
                drpFGItemCodeRH.CssClass = "form-control";
                if (drpIssueSlipNo.Items.Count > 0)
                {
                    drpIssueSlipNo.SelectedIndex = 0;
                }
                if (drpType.Items.Count > 0)
                {
                    drpType.SelectedIndex = 0;
                }
                lblAvailableQty.Text = "0";
                lblModel.Text = "";
                ViewState["PARTBARCODE"] = null;
            }
            catch (Exception ex)
            {
                CommonHelper.ShowMessage(ex.Message, msgerror, CommonHelper.MessageType.Error.ToString());
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void txtScanMachineBarcode_TextChanged(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (string.IsNullOrEmpty(txtScanMachineBarcode.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan machine barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtScanMachineBarcode.Focus();
                    return;
                }
                dvLaserFileData.DataSource = null;
                dvLaserFileData.DataBind();
                blobj = new BL_WIP_LaserMachine();
                string sResult = blobj.ValidateMachineLabel(txtScanMachineBarcode.Text.Trim(), Session["SiteCode"].ToString());
                if (sResult.Length > 0)
                {
                    if (sResult.StartsWith("SUCCESS~"))
                    {
                        CommonHelper.ShowMessage("Machine barcode is OK ", msgsuccess, CommonHelper.MessageType.Success.ToString());
                        txtScanMachineBarcode.ReadOnly = true;
                        txtBarcode.Text = string.Empty;
                        drpType.Focus();
                        DataTable dt = new DataTable();
                        dvLaserFileData.DataSource = null;
                        dvLaserFileData.DataBind();
                        txtScanMachineBarcode.CssClass = "form-control";
                        return;
                    }
                    else if (sResult.StartsWith("N~"))
                    {
                        CommonHelper.ShowMessage("Invalid Machine barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                        txtScanMachineBarcode.ReadOnly = false;
                        txtScanMachineBarcode.Text = string.Empty;
                        txtScanMachineBarcode.Focus();
                        DataTable dt = new DataTable();
                        dvLaserFileData.DataSource = null;
                        dvLaserFileData.DataBind();
                        txtScanMachineBarcode.CssClass = "form-control";
                        return;
                    }
                }
                else
                {
                    CommonHelper.ShowMessage("No result found for scanned barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtScanMachineBarcode.ReadOnly = false;
                    txtScanMachineBarcode.Text = string.Empty;
                    txtScanMachineBarcode.Focus();
                    DataTable dt = new DataTable();
                    dvLaserFileData.DataSource = null;
                    dvLaserFileData.DataBind();
                    txtScanMachineBarcode.CssClass = "form-control";
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowMessage(ex.Message, msgerror, CommonHelper.MessageType.Error.ToString());
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
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
                CommonHelper.ShowMessage(ex.Message, msgerror, CommonHelper.MessageType.Error.ToString());
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblScanRH.InnerText = "Scan Barcode";
                txtBarcode.Attributes.Add("placeholder", "Scan Barcode");
                txtBarcode.ReadOnly = false;
                DataTable dt = new DataTable();
                dvLaserFileData.DataSource = dt;
                dvLaserFileData.DataBind();
                txtBarcode.Text = "";
                txtBarcode.ReadOnly = false;
                drpFGItemCodeRH.Enabled = true;
                drpIssueSlipNo.Items.Clear();
                lblAvailableQty.Text = "0";
                lblModel.Text = "";
                divTMO.Visible = false;
                if (drpType.SelectedIndex > 0)
                {
                    blobj = new BL_WIP_LaserMachine();
                    DataTable dtIssue = blobj.BindIssueslipno(Session["SiteCode"].ToString());
                    if (dtIssue.Rows.Count > 0)
                    {
                        clsCommon.FillComboBox(drpIssueSlipNo, dtIssue, true);
                    }
                    if (drpType.Text == "TMO_PROCESS")
                    {
                        divTMO.Visible = true;
                    }
                }
                ViewState["PARTBARCODE"] = null;
            }
            catch (Exception ex)
            {
                CommonHelper.ShowMessage(ex.Message, msgerror, CommonHelper.MessageType.Error.ToString());
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void drpIssueSlipNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                drpFGItemCodeRH.Items.Clear();
                if (drpIssueSlipNo.SelectedIndex > 0)
                {
                    blobj = new BL_WIP_LaserMachine();
                    DataTable dtFGItemCodeLH = blobj.BindFGItemCode("", drpIssueSlipNo.SelectedItem.Text, Session["SiteCode"].ToString());
                    if (dtFGItemCodeLH.Rows.Count > 0)
                    {
                        clsCommon.FillComboBox(drpFGItemCodeRH, dtFGItemCodeLH, true);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowMessage(ex.Message, msgerror, CommonHelper.MessageType.Error.ToString());
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void drpFGItemCodeRH_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpFGItemCodeRH.SelectedIndex > 0)
                {
                    BindCustomerCode(drpFGItemCodeRH.Text, drpCustomerCode);
                    if (drpType.Text == "TMO_PROCESS")
                    {
                        CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                        lblAvailableQty.Text = "0";
                        lblModel.Text = "";
                        blobj = new BL_WIP_LaserMachine();
                        DataTable dtPurchaseOrder = blobj.GetPurchaseOrderDetails(drpFGItemCodeRH.Text, Session["SiteCode"].ToString());
                        if (dtPurchaseOrder.Rows.Count > 0)
                        {
                            lblModel.Text = dtPurchaseOrder.Rows[0][1].ToString();
                            lblAvailableQty.Text = dtPurchaseOrder.Rows[0][0].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowMessage(ex.Message, msgerror, CommonHelper.MessageType.Error.ToString());
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}