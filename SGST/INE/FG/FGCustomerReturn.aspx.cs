using BusinessLayer.FG;
using Common;
using System;
using System.Data;

namespace SGST.INE.FG
{
    public partial class FGCustomerReturn : System.Web.UI.Page
    {
        private void BindReturnSlipNo()
        {
            try
            {
                resetData();
                drpReturnSlipNo.Items.Clear();
                BL_FGCustomerReturn blobj = new BL_FGCustomerReturn();
                DataTable dt = blobj.BindReturnSlipNo(Session["SiteCode"].ToString());
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(drpReturnSlipNo, dt, true);
                    drpReturnSlipNo.SelectedIndex = 0;
                }
                else
                {
                    CommonHelper.ShowMessage("Return Slip no not found", msgerror, "Error");
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void BindSlipDetails()
        {
            try
            {
                string RETURN_SLIP_NO = drpReturnSlipNo.Text;
                BL_FGCustomerReturn blobj = new BL_FGCustomerReturn();
                DataTable dt = blobj.GetSlipDetails(RETURN_SLIP_NO, Session["SiteCode"].ToString());
                if (dt.Rows.Count > 0)
                {
                    gvReturnSlip.DataSource = dt;
                    gvReturnSlip.DataBind();
                    hidItemCode.Value = dt.Rows[0]["ITEM_CODE"].ToString();
                    hidSPO.Value = dt.Rows[0]["SALES_ORDER_NO"].ToString();
                    txtBoxID.Text = string.Empty;
                }
                else
                {
                  
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["usertype"].ToString() != "ADMIN")
            {
                string _strRights = CommonHelper.GetRights("FG CUSTOMER RETURN", (DataTable)Session["USER_RIGHTS"]);
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
                    BindReturnSlipNo();
                }
                catch (Exception ex)
                {
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                    CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                }
            }
        }

        private void resetData()
        {
            try
            {
                if (drpReturnSlipNo.Items.Count > 0)
                {
                    drpReturnSlipNo.SelectedIndex = 0;
                }
                txtBoxID.Text = string.Empty;
                txtScanLocation.Text = string.Empty;
                DataTable dt = new DataTable();
                gvReturnSlip.DataSource = dt;
                gvReturnSlip.DataBind();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void Scanbarcode()
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (drpReturnSlipNo.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select return slip no", msginfo, CommonHelper.MessageType.Info.ToString());
                    drpReturnSlipNo.Focus(); return;
                }
                else if (gvReturnSlip.Rows.Count == 0)
                {
                    CommonHelper.ShowMessage("No data found against return slip no", msginfo, CommonHelper.MessageType.Info.ToString());
                    drpReturnSlipNo.Focus(); return;
                }
                else if (string.IsNullOrEmpty(txtScanLocation.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan location", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtScanLocation.Focus(); return;
                }
                if (txtBoxID.Text.Trim() == string.Empty)
                {
                    CommonHelper.ShowMessage("Please scan box ID", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtBoxID.Focus(); return;
                }
                string sItemCode = string.Empty;
                string sPONO = string.Empty;
                string RETURN_SLIP_NO = drpReturnSlipNo.Text;
                string ITEM_CODE = hidItemCode.Value;
                string BOX_BARCODE = txtBoxID.Text.Trim();
                string SCANNED_BY = Session["UserID"].ToString();
                string LOCATION_CODE = txtScanLocation.Text;
                string ORDER_NO = hidSPO.Value;
                BL_FGCustomerReturn blobj = new BL_FGCustomerReturn();
                string sResult = string.Empty;
                sResult = blobj.sScanFGReturnBarcode(
                    RETURN_SLIP_NO, ITEM_CODE, BOX_BARCODE, LOCATION_CODE
                    ,SCANNED_BY, Session["LINECODE"].ToString(), Session["SiteCode"].ToString()
                    );
                string[] Message = sResult.Split('~');
                if (sResult.StartsWith("N~"))
                {
                    txtBoxID.Text = "";
                    txtBoxID.Focus();
                    CommonHelper.ShowMessage(Message[1], msgerror, CommonHelper.MessageType.Error.ToString());
                }
                else if (sResult.StartsWith("ERROR~") || sResult.StartsWith("PRINTINGFAILED~"))
                {
                    CommonHelper.ShowMessage(Message[1], msgerror, CommonHelper.MessageType.Error.ToString());
                }
                else if (sResult.StartsWith("PRNNOTFOUND~") || sResult.StartsWith("PRINTERNOTCONNECTED~"))
                {
                    CommonHelper.ShowMessage(Message[1], msgerror, CommonHelper.MessageType.Error.ToString());
                }
                else if (sResult.StartsWith("SUCCESS~") || sResult.StartsWith("PRINTERPRNNOTPRINT~"))
                {
                    CommonHelper.ShowMessage(Message[1], msgsuccess, CommonHelper.MessageType.Success.ToString());                  
                    txtBoxID.Text = string.Empty;
                    txtBoxID.Focus();
                    DataTable dt = new DataTable();
                    gvReturnSlip.DataSource = dt;
                    gvReturnSlip.DataBind();
                    BindSlipDetails();
                    if (gvReturnSlip.Rows.Count == 0)
                    {
                        CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                        CommonHelper.ShowMessage("Return slip no completed, Please select diffrent no for return more items", msgsuccess, CommonHelper.MessageType.Success.ToString());
                        BindReturnSlipNo();
                        txtScanLocation.Text = string.Empty;
                    }
                    return;
                }
                txtBoxID.Text = string.Empty;
                return;
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void drpReturnSlipNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpReturnSlipNo.SelectedIndex > 0)
                {
                    BindSlipDetails();
                }
                else
                {                   
                    txtBoxID.Text = string.Empty;                    
                    txtScanLocation.Text = string.Empty;                  
                    DataTable dt = new DataTable();
                    gvReturnSlip.DataSource = dt;
                    gvReturnSlip.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
        }      

        protected void btnReset_Click(object sender, EventArgs e)
        {
            resetData();
        }

        protected void txtBoxID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Scanbarcode();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void txtScanLocation_TextChanged(object sender, EventArgs e)
        {
            txtBoxID.Focus();
        }
    }
}