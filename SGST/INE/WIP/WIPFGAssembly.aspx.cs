using Common;
using System;
using System.Data;
using BusinessLayer.WIP;
using System.Linq;
using BusinessLayer;
using System.Configuration;
using System.Web.UI.WebControls;

namespace SGST.INE.WIP
{
    public partial class WIPFGAssembly : System.Web.UI.Page
    {
        BL_WIP_LaserMachine blobj = new BL_WIP_LaserMachine();
        DataTable dtLaserFileData;
        string Message = "";
        string _FIFORequired = ConfigurationManager.AppSettings["_FIFOREQUIRED"].ToString();
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
                string _strRights = CommonHelper.GetRights("WIP FG ASSEMBLY", (DataTable)Session["USER_RIGHTS"]);
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
                ViewState["Row"] = null;
                txtBarcode.Text = "";
                txtBarcode.ReadOnly = false;
                txtMachineID.Text = string.Empty;
                txtMachineID.Focus();
                txtMachineID.Enabled = true;
                txtMachineID.Height = 35;
                gvProfileMaster.DataSource = null;
                gvProfileMaster.DataBind();
                drpFGItemCode.Enabled = true;

                lblCustomerCode.Text = string.Empty;
                DataTable dt = new DataTable();
                dvLaserFileData.DataSource = dt;
                dvLaserFileData.DataBind();
                drpFGItemCode.Items.Clear();
                drpWorkOrderNo.Items.Clear();
                gvScannedBarcodeData.DataSource = null;
                gvScannedBarcodeData.DataBind();
                BL_WIP_FGAssembly blobj = new BL_WIP_FGAssembly();
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
                    BL_WIP_FGAssembly blobj = new BL_WIP_FGAssembly();
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
                ViewState["Row"] = null;
                txtBarcode.Text = "";
                txtBarcode.ReadOnly = false;
                txtMachineID.Text = string.Empty;
                txtMachineID.Focus();
                txtMachineID.Enabled = true;
                txtMachineID.Height = 35;
                gvProfileMaster.DataSource = null;
                gvProfileMaster.DataBind();
                drpFGItemCode.Enabled = true;

                lblCustomerCode.Text = string.Empty;
                DataTable dt = new DataTable();
                dvLaserFileData.DataSource = dt;
                dvLaserFileData.DataBind();
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
                DataTable dt = new DataTable();
                dvLaserFileData.DataSource = dt;
                dvLaserFileData.DataBind();
                txtBarcode.Text = "";
                txtBarcode.ReadOnly = false;
                if (drpFGItemCode.SelectedIndex > 0)
                {
                    BL_WIP_FGAssembly blobj = new BL_WIP_FGAssembly();
                    dt = new DataTable();
                    dt = blobj.GetCustomerCode(drpFGItemCode.Text, Session["SiteCode"].ToString());
                    if (dt.Rows.Count > 0)
                    {
                        lblCustomerCode.Text = dt.Rows[0][0].ToString();
                    }

                    gvProfileMaster.DataSource = null;
                    gvProfileMaster.DataBind();
                    DataTable dtProgramDetails = blobj.GetProgramDetails(drpFGItemCode.Text, txtMachineID.Text
                        , Session["SiteCode"].ToString()
                        );
                    if (dtProgramDetails.Rows.Count > 0)
                    {
                        gvProfileMaster.DataSource = dtProgramDetails;
                        gvProfileMaster.DataBind();
                        drpFGItemCode.Enabled = false;
                        drpFGItemCode.Height = 35;
                        drpFGItemCode.CssClass = "form-control";

                        txtMachineID.Enabled = false;
                        txtMachineID.Height = 35;
                        txtMachineID.CssClass = "form-control";
                    }
                    else
                    {
                        gvProfileMaster.DataSource = null;
                        gvProfileMaster.DataBind();
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
                if (gvProfileMaster.Rows.Count == 0)
                {
                    CommonHelper.ShowMessage("No program details found for selected FG item code and machine id", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpFGItemCode.Focus();
                    return;
                }
                dvLaserFileData.DataSource = null;
                dvLaserFileData.DataBind();
                if (ViewState["Row"] != null)
                {
                    dtLaserFileData = (DataTable)ViewState["Row"];
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
                }
                BL_WIP_FGAssembly blobj = new BL_WIP_FGAssembly();
                DataTable dt = blobj.ScanReelBarcode(txtBarcode.Text.Trim(), drpFGItemCode.Text, lblCustomerCode.Text
                    , Session["SiteCode"].ToString(), Session["LINECODE"].ToString(), drpWorkOrderNo.Text
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

                    dvLaserFileData.DataSource = dt;
                    dvLaserFileData.DataBind();

                    string Part_Code = string.Empty;
                    string sBatchNo = string.Empty;
                    decimal dQty = 0;
                    sBatchNo = dt.Rows[0]["BatchNo"].ToString();
                    Part_Code = dt.Rows[0]["Part_Code"].ToString();
                    dQty = Convert.ToDecimal(dt.Rows[0]["REM_QTY"].ToString());

                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,
                        System.Reflection.MethodBase.GetCurrentMethod().Name,
                        " FG Assembly Module: Work Order No :" + drpWorkOrderNo.Text +
                        ", Part Barcode :" + txtBarcode.Text.Trim()+
                        ", Scanned By :" + Session["UserID"].ToString()
                        );
                    if (ViewState["Row"] != null)
                    {
                        dtLaserFileData = (DataTable)ViewState["Row"];
                        DataRow dr = null;
                        dr = dtLaserFileData.NewRow();
                        dr["USERID"] = Session["UserID"].ToString();
                        dr["FGITEMCODE"] = drpFGItemCode.Text;
                        dr["BATCH_NO"] = sBatchNo;
                        dr["PART_CODE"] = Part_Code;
                        dr["PART_BARCODE"] = txtBarcode.Text.Trim();
                        dr["REM_QTY"] = dQty;
                        dtLaserFileData.Rows.Add(dr);
                        dtLaserFileData.AcceptChanges();
                        ViewState["Row"] = dtLaserFileData;
                    }
                    else
                    {
                        dtLaserFileData = new DataTable();
                        dtLaserFileData.Columns.Add("USERID");
                        dtLaserFileData.Columns.Add("FGITEMCODE");
                        dtLaserFileData.Columns.Add("BATCH_NO");
                        dtLaserFileData.Columns.Add("PART_CODE");
                        dtLaserFileData.Columns.Add("PART_BARCODE");
                        dtLaserFileData.Columns.Add("REM_QTY");
                        DataRow dr1 = dtLaserFileData.NewRow();
                        dr1["USERID"] = Session["UserID"].ToString();
                        dr1["FGITEMCODE"] = drpFGItemCode.Text;
                        dr1["BATCH_NO"] = sBatchNo;
                        dr1["PART_CODE"] = Part_Code;
                        dr1["PART_BARCODE"] = txtBarcode.Text.Trim();
                        dr1["REM_QTY"] = dQty;
                        dtLaserFileData.Rows.Add(dr1);
                        dtLaserFileData.AcceptChanges();
                        ViewState["Row"] = dtLaserFileData;
                    }
                    foreach (GridViewRow row in gvProfileMaster.Rows)
                    {
                        if (row.Cells[4].Text.ToString().ToUpper() == Part_Code.ToUpper())
                        {
                            row.BackColor = System.Drawing.Color.Green;
                            row.ForeColor = System.Drawing.Color.White;
                            row.Cells[5].Text = txtBarcode.Text;
                        }
                    }
                    drpFGItemCode.Enabled = false;
                    drpWorkOrderNo.Enabled = false;
                    txtBarcode.Text = string.Empty;
                    txtBarcode.Focus();
                    if (ViewState["Row"] != null)
                    {
                        dtLaserFileData = (DataTable)ViewState["Row"];
                        DataTable dtPostingData = dtLaserFileData.Select(" USERID = '" + Session["UserID"].ToString() + "'").CopyToDataTable();
                        gvScannedBarcodeData.DataSource = dtPostingData;
                        gvScannedBarcodeData.DataBind();
                    }
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
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
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
                BL_WIP_FGAssembly blobj = new BL_WIP_FGAssembly();
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
                gvProfileMaster.DataSource = null;
                gvProfileMaster.DataBind();
                gvScannedBarcodeData.DataSource = null;
                gvScannedBarcodeData.DataBind();


                dtLaserFileData = new DataTable();
                ViewState["Row"] = null;
                BindWorkOrderNo();

                txtMachineID.Text = string.Empty;
                txtMachineID.Focus();
                txtMachineID.Enabled = true;
                txtMachineID.Height = 35;
                drpFGItemCode.Enabled = true;
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }


        private void MappedData()
        {
            try
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,
                     System.Reflection.MethodBase.GetCurrentMethod().Name,
                     " Work Order No :" + drpWorkOrderNo.Text +
                      ", Scanned By :" + Session["UserID"].ToString() +
                     ", btn Event Called "
                     );
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
                if (ViewState["Row"] == null)
                {
                    CommonHelper.ShowMessage("Please scan all the barcode again", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBarcode.Focus();
                    dtLaserFileData = new DataTable();
                    txtBarcode.Text = string.Empty;
                    txtBarcode.Focus();
                    DataTable dt1 = new DataTable();
                    dvLaserFileData.DataSource = dt1;
                    dvLaserFileData.DataBind();
                    gvScannedBarcodeData.DataSource = dt1;
                    gvScannedBarcodeData.DataBind();
                    gvProfileMaster.DataSource = null;
                    gvProfileMaster.DataBind();
                    BL_WIP_FGAssembly blobj2 = new BL_WIP_FGAssembly();
                    DataTable dtProgramDetails = blobj2.GetProgramDetails(drpFGItemCode.Text, txtMachineID.Text
                        , Session["SiteCode"].ToString()
                        );
                    if (dtProgramDetails.Rows.Count > 0)
                    {
                        gvProfileMaster.DataSource = dtProgramDetails;
                        gvProfileMaster.DataBind();
                        drpFGItemCode.Enabled = false;
                        drpFGItemCode.Height = 35;
                        drpFGItemCode.CssClass = "form-control";

                        txtMachineID.Enabled = false;
                        txtMachineID.Height = 35;
                        txtMachineID.CssClass = "form-control";
                    }
                    return;
                }
                DataTable dtData = (DataTable)ViewState["Row"];
                DataTable dtPostingData = dtData.Select(" USERID = '" + Session["UserID"].ToString() + "'").CopyToDataTable();
                if (dtPostingData.Rows.Count == 0)
                {
                    CommonHelper.ShowMessage("Please scan at least one barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBarcode.Focus();
                    return;
                }
                if (dtPostingData.Rows.Count != gvProfileMaster.Rows.Count)
                {
                    CommonHelper.ShowMessage("Please scan all the barcode of item code showing above", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBarcode.Focus();
                    return;
                }
                dtPostingData.Columns.Remove("USERID");
                dtPostingData.Columns.Remove("FGITEMCODE");
                dtPostingData.AcceptChanges();
                string Part_Code = string.Empty;
                int iQty = 0;
                decimal dQty = 0;
                int iArraySize = 0;
                string sTwoSide = "0";
                string sBatchNo = string.Empty;
                string sPONO = string.Empty;
                string CustomerPartCode_RH = string.Empty;
                foreach (GridViewRow item in dvLaserFileData.Rows)
                {
                    Part_Code = dvLaserFileData.Rows[item.RowIndex].Cells[2].Text;
                    dQty = Convert.ToDecimal(dvLaserFileData.Rows[item.RowIndex].Cells[3].Text);
                    iArraySize = Convert.ToInt32(dvLaserFileData.Rows[item.RowIndex].Cells[4].Text);
                    sTwoSide = dvLaserFileData.Rows[item.RowIndex].Cells[5].Text;
                    sBatchNo = dvLaserFileData.Rows[item.RowIndex].Cells[6].Text;
                    sPONO = dvLaserFileData.Rows[item.RowIndex].Cells[7].Text;
                    CustomerPartCode_RH = dvLaserFileData.Rows[item.RowIndex].Cells[10].Text;
                    break;
                }
                iQty = Convert.ToInt32(dQty);
                DataTable dt = new DataTable();
                if (iArraySize == 0)
                {
                    CommonHelper.ShowMessage("Array size not found in part master against partcode of scanned barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBarcode.Text = string.Empty;
                    txtBarcode.ReadOnly = false;
                    txtBarcode.Focus();
                    dt = new DataTable();
                    dvLaserFileData.DataSource = dt;
                    dvLaserFileData.DataBind();
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
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,
                      System.Reflection.MethodBase.GetCurrentMethod().Name,
                      " Work Order No :" + drpWorkOrderNo.Text +
                       ", Scanned By :" + Session["UserID"].ToString() +
                      ", Called PCB printing method for FG Assembly module in Bussiness Layer"
                      );
                BL_WIP_FGAssembly blobj = new BL_WIP_FGAssembly();
                string sResult = blobj.FG_Assembly_PCBPrinting(Part_Code, iQty, iArraySize,
                  sBatchNo, drpWorkOrderNo.Text,
                  CustomerPartCode_RH, dtPostingData, drpFGItemCode.Text
                  , sUserID, sLineCode, Session["SiteCode"].ToString(),
                  txtMachineID.Text.Trim(), sDesignerFormat
                  );
                if (sResult.Length > 0)
                {
                    if (sResult.StartsWith("SUCCESS~"))
                    {
                        dtLaserFileData = new DataTable();
                        ViewState["Row"] = null;
                        Message = sResult.Split('~')[1].ToString();
                        CommonHelper.ShowMessage("Label printed successfully, : " + Message, msgsuccess, CommonHelper.MessageType.Success.ToString());
                        txtBarcode.Text = string.Empty;
                        txtBarcode.Focus();
                        DataTable dt1 = new DataTable();
                        dvLaserFileData.DataSource = dt1;
                        dvLaserFileData.DataBind();
                        gvScannedBarcodeData.DataSource = dt1;
                        gvScannedBarcodeData.DataBind();
                        gvProfileMaster.DataSource = null;
                        gvProfileMaster.DataBind();
                        DataTable dtProgramDetails = blobj.GetProgramDetails(drpFGItemCode.Text, txtMachineID.Text
                            , Session["SiteCode"].ToString()
                            );
                        if (dtProgramDetails.Rows.Count > 0)
                        {
                            gvProfileMaster.DataSource = dtProgramDetails;
                            gvProfileMaster.DataBind();
                            drpFGItemCode.Enabled = false;
                            drpFGItemCode.Height = 35;
                            drpFGItemCode.CssClass = "form-control";

                            txtMachineID.Enabled = false;
                            txtMachineID.Height = 35;
                            txtMachineID.CssClass = "form-control";
                        }
                    }
                    else if (sResult.StartsWith("Duplicate~"))
                    {
                        Message = sResult.Split('~')[1].ToString();
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    else if (sResult.StartsWith("N~"))
                    {
                        Message = sResult.Split('~')[1].ToString();
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    else
                    {
                        ViewState["Row"] = null;
                        dtLaserFileData = new DataTable();
                        txtBarcode.Text = string.Empty;
                        txtBarcode.ReadOnly = false;
                        txtBarcode.Focus();
                        dt = new DataTable();
                        dvLaserFileData.DataSource = null;
                        dvLaserFileData.DataBind();
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
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,
                       System.Reflection.MethodBase.GetCurrentMethod().Name,
                       "  FG Assembly Module: Work Order No :" + drpWorkOrderNo.Text +
                       ", Scanned By :" + Session["UserID"].ToString()+
                       ", btn Event Called "
                       );
                MappedData();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }


    }
}