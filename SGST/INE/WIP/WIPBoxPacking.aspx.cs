using Common;
using System;
using System.Data;
using BusinessLayer;
using BusinessLayer.WIP;
using PL;
using System.Reflection;
using System.Linq;
using System.Web.UI.WebControls;
using System.Configuration;
using BusinessLayer.MES.PRINTING;

namespace SGST.INE.WIP
{
    public partial class WIPBoxPacking : System.Web.UI.Page
    {
        string Message = "";
        static string sScanningAllowed = "0";
        static int iTimeExpired = 0;
        BL_WIP_BoxPacking blobj = new BL_WIP_BoxPacking();
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
                string _strRights = CommonHelper.GetRights("PRIMARY BOX PACKING", (DataTable)Session["USER_RIGHTS"]);
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
                    dvModelName.Visible = false;
                    lblWT.Text = "0";
                    lblTP.Text = "0";
                    lblTM.Text = "0";                   
                }
                catch (Exception ex)
                {
                    CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                    PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                }
            }
        }
        private void ResetData()
        {
            txtBatchNo.Text.Trim();
            txtPCBID.Text = string.Empty;
            lblPackingQty.Text = "0";
            lblScanQty.Text = "0";
            lblModelName.Text = string.Empty;
            lblPartDesc.Text = string.Empty;
            drpCustomerCode.Items.Clear();
            txtCustomerName.Text = string.Empty;
            txtCustomerPartNo.Text = string.Empty;
            txtMSN.Text = string.Empty;
            gvCartonLabelPrint.DataSource = null;
            gvCartonLabelPrint.DataBind();
            gvPCBPrinting.DataSource = null;
            gvPCBPrinting.DataBind();
            lblLastScanned.Text = string.Empty;
            ddlPurchaseOrder.Items.Clear();
            lblWOScanQty.Text = "";
            lblWOTotalQty.Text = "";
            if (drpWorkOrderNo.Items.Count > 0)
            {
                drpWorkOrderNo.Items.Clear();
            }
        }
        private void BindFGItemCode()
        {
            try
            {
                ResetData();
                drpFGItemCode.Items.Clear();
                blobj = new BL_WIP_BoxPacking();
                DataTable dt = blobj.BindFGItemCode(Session["SiteCode"].ToString(),drpScanType.Text);
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillMultiColumnsCombo(drpFGItemCode, dt, true);
                    drpFGItemCode.SelectedIndex = 0;
                    drpFGItemCode.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void BindWorkOrderNo()
        {
            try
            {
                lblWOTotalQty.Text = "0";
                lblWOScanQty.Text = "0";
                drpWorkOrderNo.Items.Clear();
                blobj = new BL_WIP_BoxPacking();
                DataTable dt = blobj.BindWorkOrderNo(Session["SiteCode"].ToString(), drpFGItemCode.SelectedItem.Text);
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
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void BindCustomerCode(string sFGItemCode)
        {
            try
            {
                sScanningAllowed = "0";
                iTimeExpired = 0;
                drpCustomerCode.Items.Clear();
                txtCustomerName.Text = string.Empty;
                txtCustomerPartNo.Text = string.Empty;
                txtPCBID.Text = string.Empty;
                txtMSN.Text = string.Empty;
                lblPackingQty.Text = "0";
                lblScanQty.Text = "0";
                gvCartonLabelPrint.DataSource = null;
                gvCartonLabelPrint.DataBind();
                gvPCBPrinting.DataSource = null;
                gvPCBPrinting.DataBind();
                lblLastScanned.Text = string.Empty;
                blobj = new BL_WIP_BoxPacking();
                string sResult = string.Empty;
                DataTable dt = new DataTable();
                DataSet ds = blobj.BindCustomerCode(sFGItemCode, out sResult, Session["SiteCode"].ToString());
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    sScanningAllowed = ds.Tables[1].Rows[0][0].ToString();
                    iTimeExpired = Convert.ToInt32(ds.Tables[1].Rows[0][1].ToString());                   
                }
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
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        public void GetData()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (drpFGItemCode.SelectedIndex > 0)
                {
                    lblModelName.Text = string.Empty;
                    blobj = new BL_WIP_BoxPacking();
                    PL_Printing plobj = new PL_Printing();
                    plobj.sBOMCode = drpFGItemCode.SelectedItem.Text;
                    plobj.sSiteCode = Session["SiteCode"].ToString();
                    plobj.sLineCode = Session["LINECODE"].ToString();
                    plobj.sUserID = Session["UserID"].ToString();
                    DataTable dt = new DataTable();
                    dt = blobj.blGetModelDetails(plobj);
                    if (dt.Rows.Count > 0)
                    {
                        if (drpScanType.SelectedIndex == 1)
                        {
                            lblModelName.Text = dt.Rows[0]["MODEL_CODE"].ToString();
                            lblWT.Text = dt.Rows[0]["CARTON_WT"].ToString();
                            lblTP.Text = dt.Rows[0]["CTolPlus"].ToString();
                            lblTM.Text = dt.Rows[0]["CTolMinus"].ToString();
                        }
                        else 
                        {
                            drpCustomerCode.Items.Clear();
                            CommonHelper.ShowMessage("Selected fg contains the data of IMEI process", msgerror, CommonHelper.MessageType.Error.ToString());
                            drpScanType.SelectedIndex = 0;
                            if (drpFGItemCode.SelectedIndex > 0)
                            {
                                drpFGItemCode.SelectedIndex = 0;
                            }
                            drpFGItemCode.Items.Clear();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        private void GetWODetails()
        {
            try
            {
                blobj = new BL_WIP_BoxPacking();
                string sResult = string.Empty;
                PL_Printing plobj = new PL_Printing();
                plobj.sWorkOrderNo = drpWorkOrderNo.Text;
                plobj.sBOMCode = drpFGItemCode.SelectedItem.Text;
                plobj.sSiteCode = Session["SiteCode"].ToString();
                plobj.sLineCode = Session["LINECODE"].ToString();
                plobj.sUserID = Session["UserID"].ToString();
                DataTable dt = blobj.blGetWODetails(plobj);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString().StartsWith("ERROR~"))
                    {
                        CommonHelper.ShowCustomErrorMessage(dt.Rows[0][0].ToString(), msgerror);
                        return;
                    }

                    lblWOTotalQty.Text = dt.Rows[0][1].ToString();
                    lblWOScanQty.Text = dt.Rows[0][2].ToString();
                    if (lblWOTotalQty.Text == lblWOScanQty.Text)
                    {
                        DataTable dt1 = new DataTable();
                        plobj.sBOMCode = drpFGItemCode.SelectedItem.Text;
                        plobj.sCustomerCode = drpCustomerCode.SelectedValue.ToString();
                        plobj.sScanType = drpScanType.Text;
                        plobj.sSiteCode = Session["SiteCode"].ToString();
                        plobj.sLineCode = Session["LINECODE"].ToString();
                        plobj.sUserID = Session["UserID"].ToString();
                        dt1 = blobj.blGetPickedImei(plobj);
                        return;
                    }
                    if (dt.Rows[0][3].ToString() == "2")
                    {
                        CommonHelper.ShowMessage("Work Order has been closed, You can not scan more PCB in selected work order no", msginfo, CommonHelper.MessageType.Info.ToString());
                        DataTable dt2 = new DataTable();
                        plobj.sBOMCode = drpFGItemCode.SelectedItem.Text;
                        plobj.sCustomerCode = drpCustomerCode.SelectedValue.ToString();
                        plobj.sScanType = drpScanType.Text;
                        plobj.sSiteCode = Session["SiteCode"].ToString();
                        plobj.sLineCode = Session["LINECODE"].ToString();
                        plobj.sUserID = Session["UserID"].ToString();
                        dt2 = blobj.blGetPickedImei(plobj);
                        if (dt2.Rows.Count > 0)
                        {
                            PrintCompleteBarcode(plobj);
                        }
                        BindWorkOrderNo();
                        return;
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        private void BindPurchaseOrder()
        {
            try
            {
                ddlPurchaseOrder.Items.Clear();
                if (drpFGItemCode.SelectedIndex > 0)
                {
                    blobj = new BL_WIP_BoxPacking();
                    PL_Printing plboj = new PL_Printing();
                    plboj.sBOMCode = drpFGItemCode.SelectedItem.Text;
                    plboj.sSiteCode = Session["SiteCode"].ToString();
                    plboj.sLineCode = Session["LINECODE"].ToString();
                    plboj.sUserID = Session["UserID"].ToString();
                    DataTable dt = new DataTable();
                    dt = blobj.blBindPurchaseOrder(plboj);
                    if (dt.Rows.Count > 0)
                    {
                        clsCommon.FillComboBox(ddlPurchaseOrder, dt, true);
                        ddlPurchaseOrder.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        private void GetCustomerDetail(string sCustomerCode, string sFGitemCode)
        {
            try
            {
                txtCustomerName.Text = string.Empty;
                txtCustomerPartNo.Text = string.Empty;
                txtPCBID.Text = string.Empty;
                blobj = new BL_WIP_BoxPacking();
                string sResult = string.Empty;
                DataTable dt = blobj.GetCustomerDetails(sFGitemCode, sCustomerCode, out sResult
                    , Session["SiteCode"].ToString()
                    );
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
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);

            }
        }
        private void BindBoxDetails()
        {
            try
            {
                txtPCBID.Text = string.Empty;
                lblPackingQty.Text = "0";
                gvCartonLabelPrint.DataSource = null;
                gvCartonLabelPrint.DataBind();
                gvPCBPrinting.DataSource = null;
                gvPCBPrinting.DataBind();
                blobj = new BL_WIP_BoxPacking();
                string sResult = string.Empty;
                if (drpCustomerCode.SelectedIndex > 0)
                {
                    PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,
                        System.Reflection.MethodBase.GetCurrentMethod().Name, "Fg Item Code : " + drpFGItemCode.SelectedItem.Text
                        + ", Customer Code :" + drpCustomerCode.SelectedValue.ToString() + ",Site Code :" +
                         Session["SiteCode"].ToString() + ", User ID :" + Session["UserID"].ToString()
                        );
                    DataTable dt = blobj.GetFGDetails(drpFGItemCode.SelectedItem.Text,
                        drpCustomerCode.SelectedValue.ToString()
                        , Session["SiteCode"].ToString()
                        , Session["LINECODE"].ToString(), Session["UserID"].ToString());
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString().Contains('~'))
                        {
                            CommonHelper.ShowMessage(dt.Rows[0][0].ToString(), msgerror, CommonHelper.MessageType.Error.ToString());
                            return;
                        }
                        foreach (DataRow dr in dt.Rows)
                        {
                            lblPackingQty.Text = dr.ItemArray[0].ToString();
                            lblScanQty.Text = dr.ItemArray[2].ToString();
                        }
                    }
                    else
                    {
                        CommonHelper.ShowMessage("Please check the box logic in serial generation master module", msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                    dt = new DataTable();
                    PL_Printing plobj = new PL_Printing();
                    plobj.sBOMCode = drpFGItemCode.SelectedItem.Text;
                    plobj.sCustomerCode = drpCustomerCode.SelectedValue.ToString();
                    plobj.sScanType = drpScanType.Text;
                    plobj.sSiteCode = Session["SiteCode"].ToString();
                    plobj.sLineCode = Session["LINECODE"].ToString();
                    plobj.sUserID = Session["UserID"].ToString();
                    dt = blobj.blGetPickedImei(plobj);
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,
                            "Get IMEI Details()",
                            "FG item Code  :" + plobj.sBOMCode + ",Line Code:" + plobj.sLineCode + ", User ID : " + plobj.sUserID);
                    if (dt.Rows.Count > 0)
                    {
                        AddDetailsIngrid(dt);
                    }
                }
                else
                {
                    txtPCBID.Text = string.Empty;
                    lblPackingQty.Text = "0";
                    lblPartDesc.Text = "";
                    lblScanQty.Text = "0";
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
        }
        private void AddDetailsIngrid(DataTable dtData)
        {
            try
            {
                if (dtData.Rows.Count > 0)
                {
                    if (drpScanType.SelectedIndex == 0)
                    {
                        gvPCBPrinting.DataSource = null;
                        gvPCBPrinting.DataBind();
                        gvPCBPrinting.DataSource = dtData;
                        gvPCBPrinting.DataBind();
                    }
                    else
                    {
                        gvCartonLabelPrint.DataSource = null;
                        gvCartonLabelPrint.DataBind();
                        gvCartonLabelPrint.DataSource = dtData;
                        gvCartonLabelPrint.DataBind();
                    }
                    lblScanQty.Text = dtData.Rows.Count.ToString();
                }
                else
                {
                    dtData.Rows.Clear();
                    gvCartonLabelPrint.DataSource = null;
                    gvCartonLabelPrint.DataBind();
                    gvPCBPrinting.DataSource = null;
                    gvPCBPrinting.DataBind();
                    lblScanQty.Text = "0";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
        }





        protected void drpScanType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ResetData();
                if (drpScanType.SelectedIndex == 0)
                {
                    dvModelName.Visible = false;
                    gvPCBPrinting.Visible = true;
                    gvCartonLabelPrint.Visible = false;
                }
                else
                {
                    dvModelName.Visible = true;
                    gvPCBPrinting.Visible = false;
                    gvCartonLabelPrint.Visible = true;
                }
                BindFGItemCode();
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
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (drpFGItemCode.SelectedIndex > 0)
                {
                    lblPartDesc.Text = drpFGItemCode.SelectedValue.ToString();
                    BindWorkOrderNo();
                    BindCustomerCode(drpFGItemCode.SelectedItem.Text.ToString());
                    GetData();
                    if (drpScanType.SelectedIndex == 1)
                    {
                        dvModelName.Visible = true;
                        BindPurchaseOrder();
                    }
                    else
                    {
                        dvModelName.Visible = false;
                        lblModelName.Text = string.Empty;
                    }
                }
                else
                {
                    if (drpWorkOrderNo.SelectedIndex > 0)
                    {
                        drpWorkOrderNo.SelectedIndex = 0;
                    }
                    drpWorkOrderNo.Items.Clear();
                    lblWOTotalQty.Text = "";
                    lblWOScanQty.Text = "";
                    lblPartDesc.Text = "";
                    dvModelName.Visible = false;
                    txtCustomerName.Text = string.Empty;
                    txtCustomerPartNo.Text = string.Empty;
                    drpCustomerCode.Items.Clear();
                    txtPCBID.Text = string.Empty;
                    lblPackingQty.Text = "0";
                    lblScanQty.Text = "0";                    
                    txtMSN.Text = string.Empty;
                    gvCartonLabelPrint.DataSource = null;
                    gvCartonLabelPrint.DataBind();
                    gvPCBPrinting.DataSource = null;
                    gvPCBPrinting.DataBind();
                    lblLastScanned.Text = string.Empty;
                    ddlPurchaseOrder.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void drpWorkOrderNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblWOScanQty.Text = "";
                lblWOTotalQty.Text = "";
                if (drpWorkOrderNo.SelectedIndex > 0)
                {
                    GetWODetails();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void drpCustomerCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (drpCustomerCode.SelectedIndex > 0)
                {
                    GetCustomerDetail(drpCustomerCode.SelectedValue.ToString(), drpFGItemCode.SelectedItem.Text.ToString());
                    BindBoxDetails();
                }
                else
                {
                    txtPCBID.Text = string.Empty;
                    txtCustomerPartNo.Text = string.Empty;
                    txtCustomerName.Text = string.Empty;
                    txtPCBID.Text = string.Empty;
                    lblPackingQty.Text = "0";
                    lblScanQty.Text = "0";
                    txtMSN.Text = string.Empty;
                    lblLastScanned.Text = string.Empty;
                    gvCartonLabelPrint.DataSource = null;
                    gvCartonLabelPrint.DataBind();
                    gvPCBPrinting.DataSource = null;
                    gvPCBPrinting.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void txtPCBID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (drpFGItemCode.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select fg item code", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpFGItemCode.Focus();
                    txtPCBID.Text = string.Empty;
                    return;
                }
                else if (drpCustomerCode.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select customer part code", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpCustomerCode.Focus();
                    txtPCBID.Text = string.Empty;
                    return;
                }
                else if (ddlPurchaseOrder.Items.Count > 0)
                {
                    if (ddlPurchaseOrder.SelectedIndex <= 0)
                    {
                        CommonHelper.ShowMessage("Please select purchase order no.", msgerror, CommonHelper.MessageType.Error.ToString());
                        ddlPurchaseOrder.Focus();
                        txtPCBID.Text = string.Empty;
                        return;
                    }
                }
                else if (txtCustomerName.Text.Trim() == string.Empty)
                {
                    CommonHelper.ShowMessage("Please enter customer name", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtCustomerName.Focus();
                    txtPCBID.Text = string.Empty;
                    return;
                }
                else if (txtCustomerPartNo.Text.Trim() == string.Empty)
                {
                    CommonHelper.ShowMessage("Please enter customer part no", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtCustomerPartNo.Focus();
                    txtPCBID.Text = string.Empty;
                    return;
                }
                else if (string.IsNullOrEmpty(txtBatchNo.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please enter batch no.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBatchNo.Focus();
                    txtPCBID.Text = string.Empty;
                    return;
                }
                else if (string.IsNullOrEmpty(txtPCBID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan PCB ID", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPCBID.Focus();
                    txtPCBID.Text = string.Empty;
                    return;
                }
                else if (drpWorkOrderNo.SelectedIndex > 0)
                {
                    if (Convert.ToInt32(lblScanQty.Text) > 0)
                    {
                        if (drpScanType.SelectedIndex == 0)
                        {
                            var vv1 = (from GridViewRow row in gvPCBPrinting.Rows
                                       select row.Cells[3].Text).Distinct().Count();
                            if (Convert.ToInt32(vv1) > 1)
                            {
                                CommonHelper.ShowMessage("You have scanned the pcb of multiple work order no, Please pack the box without selecting work order no", msginfo, CommonHelper.MessageType.Info.ToString());
                                txtPCBID.Focus();
                                txtPCBID.Text = string.Empty;
                                return;
                            }
                        }
                        else
                        {
                            var vv1 = (from GridViewRow row in gvCartonLabelPrint.Rows
                                       select row.Cells[11].Text).Distinct().Count();
                            if (Convert.ToInt32(vv1) > 1)
                            {
                                CommonHelper.ShowMessage("You have scanned the pcb of multiple work order no, Please pack the box without selecting work order no", msginfo, CommonHelper.MessageType.Info.ToString());
                                txtPCBID.Focus();
                                txtPCBID.Text = string.Empty;
                                return;
                            }
                        }
                    }
                }
                PL_Printing plobj = new PL_Printing();
                if (drpWorkOrderNo.SelectedIndex > 0)
                {
                    plobj.sWorkOrderNo = drpWorkOrderNo.SelectedItem.Text;
                }
                else
                {
                    plobj.sWorkOrderNo = "";
                }
                plobj.sBOMCode = drpFGItemCode.SelectedItem.Text;
                plobj.sScanType = drpScanType.Text;
                plobj.sPCBBarcode = txtPCBID.Text.Trim();
                plobj.sPO_Number = ddlPurchaseOrder.Text;
                plobj.sCustomerCode = drpCustomerCode.Text;
                plobj.dBoxWT = 0;
                try
                {
                    plobj.dBoxGrossWt = Convert.ToDecimal(0);
                    plobj.dBoxNetWt = Convert.ToDecimal(0);
                }
                catch (Exception ex)
                {
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, "Error On Weiging issue :" + ex.Message);
                }


                plobj.sPO_Date = "";
                plobj.sMSN = txtMSN.Text.Trim();
                plobj.sCustomerName = txtCustomerName.Text.Trim();
                plobj.sCustomerPartNo = txtCustomerPartNo.Text.Trim();
                plobj.sBatchNo = txtBatchNo.Text.Trim();
                plobj.sBOMCode = drpFGItemCode.SelectedItem.Text;
                plobj.sCustomerCode = drpCustomerCode.SelectedValue.ToString();
                plobj.sScanType = drpScanType.Text;
                plobj.sSiteCode = Session["SiteCode"].ToString();
                plobj.sLineCode = Session["LINECODE"].ToString();
                plobj.sUserID = Session["UserID"].ToString();
                plobj.sScanningAllowed = sScanningAllowed;
                plobj.iScanningTime = iTimeExpired;
                plobj.sSamplingPCB = "0";                
                blobj = new BL_WIP_BoxPacking();
                DataTable dt3 = blobj.blGetPickedImei(plobj);
                if (dt3.Rows.Count > 0)
                {
                    if (drpWorkOrderNo.SelectedIndex > 0)
                    {
                        if (drpWorkOrderNo.SelectedValue.ToString().ToUpper() != dt3.Rows[0]["WORK_ORDER_NO"].ToString().ToUpper())
                        {
                            CommonHelper.ShowMessage("Selected Work Order No should be equal to existing order No in displayed data", msgerror, CommonHelper.MessageType.Error.ToString());
                            txtPCBID.Text = String.Empty;
                            txtPCBID.Focus();
                            return;
                        }
                        if (Convert.ToDecimal(lblWOTotalQty.Text) > 0 && Convert.ToDecimal(lblWOTotalQty.Text) <= Convert.ToDecimal(lblWOScanQty.Text))
                        {
                            CommonHelper.ShowMessage("Selected Work Order is completed, Please select different work order no", msgerror, CommonHelper.MessageType.Error.ToString());
                            txtPCBID.Text = String.Empty;
                            txtPCBID.Focus();
                            return;
                        }
                    }
                    if (ddlPurchaseOrder.Items.Count > 1)
                    {
                        if (ddlPurchaseOrder.SelectedValue.ToString().ToUpper() != dt3.Rows[0]["PONO"].ToString().ToUpper())
                        {
                            CommonHelper.ShowMessage("Selected PO No should be equal to existing PO No in displayed data", msgerror, CommonHelper.MessageType.Error.ToString());
                            txtPCBID.Text = String.Empty;
                            txtPCBID.Focus();
                            return;
                        }
                    }
                    plobj.dPackingDetail = dt3;
                }
                if (drpScanType.SelectedIndex == 1)
                {
                    if (Convert.ToInt32(gvCartonLabelPrint.Rows.Count) >= Convert.ToInt32(lblPackingQty.Text))
                    {
                        CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.MethodBase.GetCurrentMethod().Name, "Pack size and scanned qty matched, Scan Qty :" + gvCartonLabelPrint.Rows.Count);
                        plobj.sSNBarcode = dt3.Rows[0]["IMEI1"].ToString();
                        txtPCBID.Text = dt3.Rows[0]["IMEI1"].ToString();
                        Response.Write("<script LANGUAGE='JavaScript' >alert('Please put weight on weighing module and click validate')</script>");
                        dvMessage.Visible = true;
                        return;
                    }
                }
                else
                {
                    if (lblPackingQty.Text == lblScanQty.Text)
                    {
                        CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.MethodBase.GetCurrentMethod().Name, "Pack size and scanned qty matched, Scan Qty :" + lblScanQty.Text);
                        plobj.sSNBarcode = dt3.Rows[0]["PCB_BARCODE"].ToString();
                        string sResultData = BoxGenerateAndDataInDatabase(plobj);
                        if (sResultData.StartsWith("SUCCESS~"))
                        {
                            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                            CommonHelper.ShowMessage("Some error occurs on generting last Box id, Box id has been generated, Please scan again this barcode", msginfo, CommonHelper.MessageType.Info.ToString());
                            txtPCBID.Text = string.Empty;
                            txtPCBID.Focus(); return;
                        }
                        else
                        {
                            txtPCBID.Text = string.Empty;
                            txtPCBID.Focus(); return;
                        }
                    }
                }
                PrintBarcode(plobj);
                lblLastScanned.Text = txtPCBID.Text;
                txtPCBID.Text = string.Empty;
                txtPCBID.Focus();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {
                if (drpWorkOrderNo.SelectedIndex > 0)
                {
                    GetWODetails();
                }
            }
        }


        public void PrintBarcode(PL_Printing plobj)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                string sFIFORequired = ConfigurationManager.AppSettings["_FIFOREQUIRED"].ToString();
                BL_WIP_BoxPacking blobj = new BL_WIP_BoxPacking();
                plobj.sSiteCode = Session["SiteCode"].ToString();
                plobj.sLineCode = Session["LINECODE"].ToString();
                plobj.sUserID = Session["UserID"].ToString();
                plobj.sFIFORequied = sFIFORequired;
                string _sResult = blobj.sScanBarcode(plobj);
                if (_sResult.IndexOf('~') > 0)
                {
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.MethodBase.GetCurrentMethod().Name, "Barcode scanned  :" + plobj.sSNBarcode + ", Work Order No :" + plobj.sWorkOrderNo + ", Result : " + _sResult);
                    if (_sResult.StartsWith("SUCCESS~"))
                    {
                        plobj.sBoxId = _sResult.Split('~')[1];
                        PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtAll, MethodBase.GetCurrentMethod().Name, "Records in datatable dtDetail " + gvPCBPrinting.Rows.Count + " and no of rows in grid = " + gvCartonLabelPrint.Rows.Count + "Box ID " + plobj.sBoxId + "");
                        CommonHelper.ShowMessage("Box has been packed and printed successfully", msgsuccess, CommonHelper.MessageType.Success.ToString());
                        gvCartonLabelPrint.DataSource = null;
                        gvCartonLabelPrint.DataBind();
                        lblLastScanned.Text = txtPCBID.Text;
                        txtPCBID.Text = string.Empty;
                        txtPCBID.Focus();
                    }
                    else if (_sResult.StartsWith("OKAY~"))
                    {
                        PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,
                            MethodBase.GetCurrentMethod().Name, " Barcode Scanned, Added in displayed View, Scanned barcode : " + txtPCBID.Text.Trim()
                            + ", FG item Code : " + drpFGItemCode.SelectedItem.Text + ", User ID : " + plobj.sUserID + ", Line code:" + plobj.sLineCode);
                        //AddDetailsIngrid(_sResult.Split('~')[1]);
                        DataTable dt = new DataTable();
                        plobj.sBOMCode = drpFGItemCode.SelectedItem.Text;
                        plobj.sCustomerCode = drpCustomerCode.SelectedValue.ToString();
                        plobj.sScanType = drpScanType.Text;
                        plobj.sSiteCode = Session["SiteCode"].ToString();
                        plobj.sLineCode = Session["LINECODE"].ToString();
                        plobj.sUserID = Session["UserID"].ToString();
                        CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,
                            "Get IMEI Details()",
                            "FG item Code  :" + plobj.sBOMCode + ",Line Code:" + plobj.sLineCode + ", User ID : " + plobj.sUserID);
                        dt = blobj.blGetPickedImei(plobj);
                        if (dt.Rows.Count > 0)
                        {
                            AddDetailsIngrid(dt);
                        }
                        CommonHelper.ShowMessage("Data scanned successfully", msgsuccess, CommonHelper.MessageType.Success.ToString());
                        lblLastScanned.Text = txtPCBID.Text;
                        txtPCBID.Text = string.Empty;
                        txtPCBID.Focus();                        
                        int iPackSize = Convert.ToInt32(lblPackingQty.Text.Trim());
                        if (drpScanType.SelectedIndex == 0)
                        {
                            if (gvPCBPrinting.Rows.Count >= iPackSize)
                            {
                                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtInfo, MethodBase.GetCurrentMethod().Name, "PACK SIZE MATCHED, Scanned Count :" + gvPCBPrinting.Rows.Count.ToString() + ", Pack Size :" + iPackSize.ToString() + ", Print Box ID ");
                                BoxGenerateAndDataInDatabase(plobj);
                            }
                        }
                        else
                        {
                            if (gvCartonLabelPrint.Rows.Count >= iPackSize)
                            {
                                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtInfo, MethodBase.GetCurrentMethod().Name, "PACK SIZE MATCHED, Scanned Count :" + gvCartonLabelPrint.Rows.Count.ToString() + ", Pack Size :" + iPackSize.ToString());
                                Response.Write("<script LANGUAGE='JavaScript' >alert('Please put weight on weighing module and click validate')</script>");
                                dvMessage.Visible = true;
                            }
                        }
                    }
                    else if (_sResult.StartsWith("N~"))
                    {
                        CommonHelper.ShowMessage(_sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        lblLastScanned.Text = txtPCBID.Text;
                        txtPCBID.Text = string.Empty;
                        txtPCBID.Focus();
                        if (_sResult.ToUpper().Contains("ALREADY"))
                        {
                            BindBoxDetails();
                        }
                    }                    
                    else
                    {
                        CommonHelper.ShowMessage(_sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        lblLastScanned.Text = txtPCBID.Text;
                        txtPCBID.Text = string.Empty;
                        txtPCBID.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }



        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                BindWorkOrderNo();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
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
                if (drpFGItemCode.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select fg item code", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpFGItemCode.Focus();
                    txtPCBID.Text = string.Empty;
                    return;
                }
                else if (drpCustomerCode.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select customer part code", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpCustomerCode.Focus();
                    txtPCBID.Text = string.Empty;
                    return;
                }
                else if (ddlPurchaseOrder.Items.Count > 0)
                {
                    if (ddlPurchaseOrder.SelectedIndex <= 0)
                    {
                        CommonHelper.ShowMessage("Please select purchase order no.", msgerror, CommonHelper.MessageType.Error.ToString());
                        ddlPurchaseOrder.Focus();
                        txtPCBID.Text = string.Empty;
                        return;
                    }
                }
                else if (txtCustomerName.Text.Trim() == string.Empty)
                {
                    CommonHelper.ShowMessage("Please enter customer name", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtCustomerName.Focus();
                    txtPCBID.Text = string.Empty;
                    return;
                }
                else if (txtCustomerPartNo.Text.Trim() == string.Empty)
                {
                    CommonHelper.ShowMessage("Please enter customer part no", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtCustomerPartNo.Focus();
                    txtPCBID.Text = string.Empty;
                    return;
                }
                else if (txtBatchNo.Text.Trim() == string.Empty)
                {
                    CommonHelper.ShowMessage("Please enter batch no", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBatchNo.Focus();
                    txtPCBID.Text = string.Empty;
                    return;
                }
                if (drpWorkOrderNo.SelectedIndex > 0)
                {
                    if (Convert.ToInt32(lblScanQty.Text) > 0)
                    {
                        if (drpScanType.SelectedIndex == 0)
                        {
                            var vv1 = (from GridViewRow row in gvPCBPrinting.Rows
                                       select row.Cells[3].Text).Distinct().Count();
                            if (Convert.ToInt32(vv1) > 1)
                            {
                                CommonHelper.ShowMessage("You have scanned the pcb of multiple work order no, Please pack the box without selecting work order no", msginfo, CommonHelper.MessageType.Info.ToString());
                                txtPCBID.Focus();
                                txtPCBID.Text = string.Empty;
                                return;
                            }
                        }
                        else
                        {
                            var vv1 = (from GridViewRow row in gvCartonLabelPrint.Rows
                                       select row.Cells[11].Text).Distinct().Count();
                            if (Convert.ToInt32(vv1) > 1)
                            {
                                CommonHelper.ShowMessage("You have scanned the pcb of multiple work order no, Please pack the box without selecting work order no", msginfo, CommonHelper.MessageType.Info.ToString());
                                txtPCBID.Focus();
                                txtPCBID.Text = string.Empty;
                                return;
                            }
                        }
                    }
                }
                
                txtID.Text = string.Empty;
                txtPassword.Text = string.Empty;
                dvCompletePannel.Visible = true;
                txtID.Focus();

                
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void btnValidate_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (drpFGItemCode.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select fg item code", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpFGItemCode.Focus();
                    txtPCBID.Text = string.Empty;
                    return;
                }
                else if (drpCustomerCode.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select customer part code", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpCustomerCode.Focus();
                    txtPCBID.Text = string.Empty;
                    return;
                }
                else if (ddlPurchaseOrder.Items.Count > 0)
                {
                    if (ddlPurchaseOrder.SelectedIndex <= 0)
                    {
                        CommonHelper.ShowMessage("Please select purchase order no.", msgerror, CommonHelper.MessageType.Error.ToString());
                        ddlPurchaseOrder.Focus();
                        txtPCBID.Text = string.Empty;
                        return;
                    }
                }
                else if (txtCustomerName.Text.Trim() == string.Empty)
                {
                    CommonHelper.ShowMessage("Please enter customer name", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtCustomerName.Focus();
                    txtPCBID.Text = string.Empty;
                    return;
                }
                else if (txtCustomerPartNo.Text.Trim() == string.Empty)
                {
                    CommonHelper.ShowMessage("Please enter customer part no", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtCustomerPartNo.Focus();
                    txtPCBID.Text = string.Empty;
                    return;
                }
                else if (txtBatchNo.Text.Trim() == string.Empty)
                {
                    CommonHelper.ShowMessage("Please enter batch no", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBatchNo.Focus();
                    txtPCBID.Text = string.Empty;
                    return;
                }
                BL_GB_WT_Printing blobj = new BL_GB_WT_Printing();
                DataTable dt = new DataTable();
                PL.PL_Printing plob = new PL.PL_Printing();
                plob.sSiteCode = Session["SiteCode"].ToString();
                plob.sUserID = Session["UserID"].ToString();
                plob.sLineCode = Session["LineCode"].ToString();
                plob.sBOMCode = drpFGItemCode.SelectedItem.Text.Trim();
                plob.sBarcodestring = "";
                DataTable dtResult = blobj.blGetCaptureWeight(plob);
                if (dtResult.Rows.Count == 0)
                {
                    CommonHelper.ShowMessage("Weight not found, Please try again", msgerror, CommonHelper.MessageType.Error.ToString());
                    btnValidate.Focus();
                    return;
                }
                else
                {
                    Message = dtResult.Rows[0][0].ToString();
                    CommonHelper.ShowMessage(Message.Split('~')[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                    txtCapWT.Text = Message.Split('~')[1].ToString();
                }
                if (string.IsNullOrEmpty(txtCapWT.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Wt not found, Please try again", msgerror, CommonHelper.MessageType.Error.ToString());
                    return;
                }
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, "Weight result found, Capture weight :" + txtCapWT.Text.Trim()
                    + ", Tol + " + lblTM.Text + ", Tol -" + lblTP.Text + ", GB Wt : " + lblWT.Text
                    );
                double capWt = Convert.ToDouble(txtCapWT.Text.Trim());
                double tMinus = Convert.ToDouble(lblTM.Text);
                double tPlus = Convert.ToDouble(lblTP.Text);
                double GBwt = Convert.ToDouble(lblWT.Text);
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (capWt >= GBwt - tMinus && capWt <= GBwt + tPlus)
                {
                    CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                    dt = new DataTable();
                    plob = new PL.PL_Printing();
                    plob.sSiteCode = Session["SiteCode"].ToString();
                    plob.sUserID = Session["UserID"].ToString();
                    plob.sLineCode = Session["LineCode"].ToString();
                    plob.dBoxGrossWt = Convert.ToDecimal(txtCapWT.Text.Trim());
                    plob.dBoxNetWt = Convert.ToDecimal(GBwt + tMinus);
                    plob.sBOMCode = drpFGItemCode.SelectedItem.Text.Trim();
                    plob.sCustomerCode = drpCustomerCode.Text;
                    plob.sScanType = drpScanType.Text;
                    plob.sPO_Number = ddlPurchaseOrder.Text;
                    plob.sCustomerName = txtCustomerName.Text.Trim();
                    plob.sCustomerPartNo = txtCustomerPartNo.Text.Trim();
                    if (drpWorkOrderNo.SelectedIndex > 0)
                    {
                        plob.sWorkOrderNo = drpWorkOrderNo.SelectedItem.Text;
                    }
                    else
                    {
                        plob.sWorkOrderNo = "";
                    }
                    plob.sMSN = txtMSN.Text;
                    plob.sBatchNo = txtBatchNo.Text.Trim();

                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,
                 System.Reflection.MethodBase.GetCurrentMethod().Name, "Weight result found, Save data in DB, Capture weight :" + txtCapWT.Text.Trim()
                 + ", Tol + " + lblTM.Text + ", Tol -" + lblTP.Text + ", GB Wt : " + lblWT.Text);
                    BoxGenerateAndDataInDatabase(plob);
                }
                else
                {
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,
                     System.Reflection.MethodBase.GetCurrentMethod().Name, "WT is not acureate, Capture weight :" + txtCapWT.Text.Trim()
                     + ", Tol + " + lblTM.Text + ", Tol -" + lblTP.Text + ", GB Wt : " + lblWT.Text
                     );
                    CommonHelper.ShowMessage("Wt is not accureate, Please put weight again", msgerror, CommonHelper.MessageType.Error.ToString());
                    return;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }



        private string PrintCompleteBarcode(PL_Printing plobj)
        {
            try
            {
                if (string.IsNullOrEmpty(txtBatchNo.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please enter batch no.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBatchNo.Focus();
                    return "";
                }
                string sSiteCode = Session["SiteCode"].ToString();
                blobj = new BL_WIP_BoxPacking();
                if (drpWorkOrderNo.SelectedIndex > 0)
                {
                    plobj.sWorkOrderNo = drpWorkOrderNo.SelectedItem.Text;
                }
                else
                {
                    plobj.sWorkOrderNo = "";
                }
                plobj.sBOMCode = drpFGItemCode.SelectedItem.Text;
                plobj.sScanType = drpScanType.Text;
                plobj.sPCBBarcode = txtPCBID.Text.Trim();
                plobj.sPO_Number = ddlPurchaseOrder.Text;
                plobj.sCustomerCode = drpCustomerCode.Text;
                plobj.dBoxWT = 0;
                if (drpScanType.Text != "IMEI")
                {
                    plobj.dBoxGrossWt = 0;
                    plobj.dBoxNetWt = 0;
                }
                plobj.sPO_Date = "";
                plobj.sMSN = txtMSN.Text.Trim();
                plobj.sCustomerName = txtCustomerName.Text.Trim();
                plobj.sCustomerPartNo = txtCustomerPartNo.Text.Trim();
                plobj.sWorkOrderComplete = "1";
                plobj.sBatchNo = txtBatchNo.Text.Trim();
                plobj.sBOMCode = drpFGItemCode.SelectedItem.Text;
                plobj.sCustomerCode = drpCustomerCode.SelectedValue.ToString();
                plobj.sScanType = drpScanType.Text;
                plobj.sSiteCode = Session["SiteCode"].ToString();
                plobj.sLineCode = Session["LINECODE"].ToString();
                plobj.sUserID = Session["UserID"].ToString();
                DataTable dt1 = blobj.blGetPickedImei(plobj);
                if (drpScanType.SelectedIndex == 1)
                {
                    plobj.sSNBarcode = dt1.Rows[0]["PCB_BARCODE"].ToString();
                    string sResultData = BoxGenerateAndDataInDatabase(plobj);
                    if (sResultData.StartsWith("SUCCESS~"))
                    {
                        CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                        txtPCBID.Text = string.Empty;
                        txtPCBID.Focus();
                        return sResultData;
                    }
                    else
                    {
                        txtPCBID.Text = string.Empty;
                        txtPCBID.Focus();
                        return sResultData;
                    }
                }
                else
                {
                    plobj.sSNBarcode = dt1.Rows[0]["PCB_BARCODE"].ToString();
                    string sResultData = BoxGenerateAndDataInDatabase(plobj);
                    if (sResultData.StartsWith("SUCCESS~"))
                    {
                        CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                        txtPCBID.Text = string.Empty;
                        txtPCBID.Focus();
                        return sResultData;
                    }
                    else
                    {
                        txtPCBID.Text = string.Empty;
                        txtPCBID.Focus();
                        return sResultData;
                    }
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        private string BoxGenerateAndDataInDatabase(PL_Printing plobj)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            string sResult = string.Empty;
            try
            {
                blobj = new BL_WIP_BoxPacking();
                plobj.sSiteCode = Session["SiteCode"].ToString();
                plobj.sLineCode = Session["LINECODE"].ToString();
                plobj.sUserID = Session["UserID"].ToString();
                sResult = blobj.PrintBoxID(plobj);
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtMessage, MethodBase.GetCurrentMethod().Name, " Box ID Printed, Result Found : " + sResult + ", FG item Code : " + drpFGItemCode.SelectedItem.Text);
                if (sResult.StartsWith("SUCCESS~"))
                {
                    PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtInfo, MethodBase.GetCurrentMethod().Name, " Box ID Printed: " + sResult + ", FG item Code : " + drpFGItemCode.SelectedItem.Text);
                    Message = sResult.Split('~')[1];
                    CommonHelper.ShowMessage(Message, msgsuccess, CommonHelper.MessageType.Success.ToString());
                    lblLastScanned.Text = txtPCBID.Text;
                    txtPCBID.Text = string.Empty;
                    txtPCBID.Focus();
                    gvCartonLabelPrint.DataSource = null;
                    gvCartonLabelPrint.DataBind();
                    gvPCBPrinting.DataSource = null;
                    gvPCBPrinting.DataBind();
                    txtPCBID.Text = string.Empty;
                    txtPCBID.Focus();
                    BindBoxDetails();
                    txtCapWT.Text = string.Empty;
                    dvMessage.Visible = false;
                    sResult = "SUCCESS~";
                }
                else if (sResult.StartsWith("N~"))
                {
                    CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPCBID.Text = string.Empty;
                    txtPCBID.Focus();
                }
                else if (sResult.StartsWith("PRNNOTFOUND~"))
                {
                    CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPCBID.Text = string.Empty;
                    txtPCBID.Focus();
                }
                else if (sResult.StartsWith("PRINTERNOTCONNECTED~"))
                {
                    CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPCBID.Text = string.Empty;
                    txtPCBID.Focus();
                }
                else if (sResult.StartsWith("PRINTINGFAILED~"))
                {
                    CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPCBID.Text = string.Empty;
                    txtPCBID.Focus();
                }
                else if (sResult.StartsWith("PRINTERPRNNOTPRINT~"))
                {
                    CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPCBID.Text = string.Empty;
                    txtPCBID.Focus();
                }
                else if (sResult.StartsWith("ERROR~"))
                {
                    CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPCBID.Text = string.Empty;
                    txtPCBID.Focus();
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            txtID.Text = string.Empty;
            txtPassword.Text = string.Empty;
            dvCompletePannel.Visible = false;
        }

        public void PrintLabel() 
        {
            try
            {
                string sSiteCode = Session["SiteCode"].ToString();
                blobj = new BL_WIP_BoxPacking();
                PL_Printing plobj = new PL_Printing();
                if (drpWorkOrderNo.SelectedIndex > 0)
                {
                    plobj.sWorkOrderNo = drpWorkOrderNo.SelectedItem.Text;
                }
                else
                {
                    plobj.sWorkOrderNo = "";
                }
                plobj.sBOMCode = drpFGItemCode.SelectedItem.Text;
                plobj.sScanType = drpScanType.Text;
                plobj.sPCBBarcode = txtPCBID.Text.Trim();
                plobj.sPO_Number = ddlPurchaseOrder.Text;
                plobj.sCustomerCode = drpCustomerCode.Text;
                if (drpScanType.Text == "IMEI")
                {
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.MethodBase.GetCurrentMethod().Name, "Pack size and scanned qty matched, Scan Qty :" + gvCartonLabelPrint.Rows.Count);
                    Response.Write("<script LANGUAGE='JavaScript' >alert('Please put weight on weighing module and click validate')</script>");
                    dvMessage.Visible = true;
                    dvCompletePannel.Visible = false;
                    return;
                }
                plobj.dBoxWT = 0;
                try
                {
                    plobj.dBoxGrossWt = Convert.ToDecimal(0);
                }
                catch (Exception ex)
                {
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, "Error On Weiging issue :" + ex.Message);
                }
                plobj.sPO_Date = "";
                plobj.sMSN = txtMSN.Text.Trim();
                plobj.sCustomerName = txtCustomerName.Text.Trim();
                plobj.sCustomerPartNo = txtCustomerPartNo.Text.Trim();
                plobj.sBatchNo = txtBatchNo.Text.Trim();
                plobj.sBOMCode = drpFGItemCode.SelectedItem.Text;
                plobj.sCustomerCode = drpCustomerCode.SelectedValue.ToString();
                plobj.sScanType = drpScanType.Text;
                plobj.sSiteCode = Session["SiteCode"].ToString();
                plobj.sLineCode = Session["LINECODE"].ToString();
                plobj.sUserID = Session["UserID"].ToString();
                DataTable dtIMEI1 = blobj.blGetPickedImei(plobj);
                if (dtIMEI1.Rows.Count == 0)
                {
                    CommonHelper.ShowMessage("No data found for print", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPCBID.Focus();
                    txtPCBID.Text = string.Empty;
                    return;
                }
                plobj.sSNBarcode = dtIMEI1.Rows[0]["PCB_BARCODE"].ToString();
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtInfo, MethodBase.GetCurrentMethod().Name
                    , "Complete Box Method Called for FG item Code : " + drpFGItemCode.SelectedItem.Text
                    + ", and work order no : " + plobj.sWorkOrderNo
                    + ", User ID : " + Session["UserID"].ToString()
                    );
                string sResultData = BoxGenerateAndDataInDatabase(plobj);
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtInfo, MethodBase.GetCurrentMethod().Name
                    , "Complete Box Method Called for FG item Code : " + drpFGItemCode.SelectedItem.Text
                    + ", and work order no : " + plobj.sWorkOrderNo
                    + ", User ID : " + Session["UserID"].ToString()
                    + ", Result : " + sResultData
                    );
                if (sResultData.StartsWith("SUCCESS~"))
                {
                    CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                    CommonHelper.ShowMessage("Box has been packed and printed successfully", msgsuccess, CommonHelper.MessageType.Success.ToString());
                    txtPCBID.Text = string.Empty;
                    txtPCBID.Focus(); return;
                }
                else
                {
                    txtPCBID.Text = string.Empty;
                    txtPCBID.Focus(); return;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void btnVerify_Click(object sender, EventArgs e)
        {
            try
            {
                BL_UserLogin blobj = new BL_UserLogin();
                DataTable dt = blobj.dtValidateCompleteButton(txtID.Text, PCommon.DataEncrypt(txtPassword.Text), "PRIMARY BOX PACKING");
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
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
    }
}