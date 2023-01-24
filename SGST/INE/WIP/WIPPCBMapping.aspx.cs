using Common;
using System;
using System.Data;
using BusinessLayer.WIP;
using System.Linq;

namespace SGST.INE.WIP
{
    public partial class WIPPCBMapping : System.Web.UI.Page
    {
        BL_WIP_LaserMachine blobj = new BL_WIP_LaserMachine();
        static DataTable dtLaserFileData;        
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
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void BindWorkOrderNo()
        {
            try
            {
                txtBarcode.Text = "";
                txtBarcode.ReadOnly = false;
                txtScanChildBarcode.Text = string.Empty;
                DataTable dt = new DataTable();
                gvScannedBarcodeData.DataSource = dt;
                gvScannedBarcodeData.DataBind();
                drpIssueSlipNo.Items.Clear();
                gvScannedBarcodeData.DataSource = null;
                gvScannedBarcodeData.DataBind();
                blobj = new BL_WIP_LaserMachine();
                DataTable dtWorkOrderNo = blobj.BindMappingWorkOrder(Session["SiteCode"].ToString());
                if (dtWorkOrderNo.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(drpIssueSlipNo, dtWorkOrderNo, true);
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        protected void drpIssueSlipNo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                ViewState["PCB"] = null;
                if (drpIssueSlipNo.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select work order no.", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpIssueSlipNo.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtBarcode.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan reel barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBarcode.Focus();
                    return;
                }
                blobj = new BL_WIP_LaserMachine();
                DataTable dt = blobj.ValidateMappedPCBBarcode(Session["SiteCode"].ToString(),
                    drpIssueSlipNo.Text, txtBarcode.Text.Trim());
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString().StartsWith("N~") || dt.Rows[0][0].ToString().StartsWith("ERROR~"))
                    {
                        CommonHelper.ShowMessage(dt.Rows[0][0].ToString().Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        txtBarcode.Text = string.Empty;
                        txtBarcode.Focus();
                        return;
                    }
                    txtBarcode.Enabled = false;
                    txtBarcode.CssClass = "form-control";
                    txtScanChildBarcode.Focus();
                }
                else
                {
                    CommonHelper.ShowMessage("No result found for scanned barcode .", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBarcode.Text = string.Empty;
                    txtBarcode.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void txtScanChildBarcode_TextChanged(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (drpIssueSlipNo.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select work order no.", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpIssueSlipNo.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtBarcode.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan reel barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBarcode.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtScanChildBarcode.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan child barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtScanChildBarcode.Focus();
                    return;
                }
                gvScannedBarcodeData.DataSource = null;
                gvScannedBarcodeData.DataBind();
                if (ViewState["PCB"] != null)
                {
                    dtLaserFileData = (DataTable)ViewState["PCB"];
                    bool contains = dtLaserFileData.AsEnumerable().Any(row => txtScanChildBarcode.Text.Trim() == row.Field<String>("PART_BARCODE"));
                    if (contains == true)
                    {
                        CommonHelper.ShowMessage("Duplicate barcode scanned", msgerror, CommonHelper.MessageType.Error.ToString());
                        txtScanChildBarcode.Text = string.Empty;
                        txtScanChildBarcode.Focus();
                        gvScannedBarcodeData.DataSource = dtLaserFileData;
                        gvScannedBarcodeData.DataBind();
                        return;
                    }
                }
                blobj = new BL_WIP_LaserMachine();
                DataTable dt = blobj.ValidateMappedPCBBarcode(Session["SiteCode"].ToString(),
                    drpIssueSlipNo.Text, txtScanChildBarcode.Text.Trim());
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString().StartsWith("N~") || dt.Rows[0][0].ToString().StartsWith("ERROR~"))
                    {
                        CommonHelper.ShowMessage(dt.Rows[0][0].ToString().Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        txtScanChildBarcode.Text = string.Empty;
                        txtScanChildBarcode.Focus();
                        return;
                    }
                    
                    DataRow dr = null;
                    if (ViewState["PCB"] == null)
                    {
                        dtLaserFileData = new DataTable();
                        dtLaserFileData.Columns.Add("PART_BARCODE", typeof(string));
                        dr = dtLaserFileData.NewRow();
                        dr["PART_BARCODE"] = txtScanChildBarcode.Text.Trim();
                    }
                    else
                    {
                        dtLaserFileData = (DataTable)ViewState["PCB"];
                        dr = dtLaserFileData.NewRow();
                        dr["PART_BARCODE"] = txtScanChildBarcode.Text.Trim();
                    }                    
                    dtLaserFileData.Rows.Add(dr);
                    dtLaserFileData.AcceptChanges();
                    ViewState["PCB"] = dtLaserFileData;
                    gvScannedBarcodeData.DataSource = dtLaserFileData;
                    gvScannedBarcodeData.DataBind();
                    txtBarcode.Enabled = false;
                    txtScanChildBarcode.Text = string.Empty;
                    txtScanChildBarcode.Focus();
                }
                else
                {
                    CommonHelper.ShowMessage("No result found for scanned barcode .", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtScanChildBarcode.Text = string.Empty;
                    txtScanChildBarcode.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (drpIssueSlipNo.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select work order no.", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpIssueSlipNo.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtBarcode.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan reel barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBarcode.Focus();
                    return;
                }
                if (dtLaserFileData.Rows.Count == 0)
                {
                    CommonHelper.ShowMessage("No Child barcode found for mapping", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtScanChildBarcode.Focus();
                    return;
                }
                blobj = new BL_WIP_LaserMachine();
                DataTable dtData = blobj.UpdateMappingData(Session["SiteCode"].ToString(),
                  drpIssueSlipNo.Text, txtBarcode.Text.Trim(), dtLaserFileData, Session["UserID"].ToString());
                if (dtData.Rows.Count > 0)
                {
                    if (dtData.Rows[0][0].ToString().StartsWith("N~") || dtData.Rows[0][0].ToString().StartsWith("ERROR~"))
                    {
                        CommonHelper.ShowMessage(dtData.Rows[0][0].ToString().Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        txtBarcode.Text = string.Empty;
                        txtBarcode.Focus();
                        return;
                    }
                    dtLaserFileData.Rows.Clear();
                    DataTable dt = new DataTable();
                    gvScannedBarcodeData.DataSource = dt;
                    gvScannedBarcodeData.DataBind();
                    CommonHelper.ShowMessage(dtData.Rows[0][0].ToString().Split('~')[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                    txtBarcode.Text = string.Empty;
                    txtBarcode.Enabled = true;
                    txtBarcode.Focus();
                    txtScanChildBarcode.Text = string.Empty;
                    ViewState["PCB"] = null;
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
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                txtBarcode.Text = "";
                txtBarcode.ReadOnly = false;
                DataTable dt = new DataTable();
                gvScannedBarcodeData.DataSource = dt;
                gvScannedBarcodeData.DataBind();
                txtBarcode.Text = "";
                txtBarcode.Enabled = true;
                dtLaserFileData.Rows.Clear();
                ViewState["PCB"] = null;
                txtScanChildBarcode.Text = string.Empty;
                BindWorkOrderNo();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}