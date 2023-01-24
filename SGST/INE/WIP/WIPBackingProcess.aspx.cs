using BusinessLayer.WIP;
using Common;
using System;
using System.Data;

namespace SGST.INE.WIP
{
    public partial class WIPBackingProcess : System.Web.UI.Page
    {
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
                string _strRights = CommonHelper.GetRights("PCB BACKING PROCESS", (DataTable)Session["USER_RIGHTS"]);
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
                    txtBarcode.Focus();
                    BindFGItemCode();
                    divOut.Visible = false;
                }
                catch (Exception ex)
                {
                    CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                }
            }
        }
        private void BindFGItemCode()
        {
            try
            {
                drpPendingBarcode.Items.Clear();
                drpFGItemCode.Items.Clear();
                drpWorkOrderNo.Items.Clear();
                txtBarcode.Text = string.Empty;
                divOut.Visible = false;
                divIN.Visible = true;
                BL_WIP_BakingProcess blobj = new BL_WIP_BakingProcess();
                DataTable dt = blobj.BindFGItemCode(Session["SiteCode"].ToString(),
                    Session["UserID"].ToString(),
                    Session["LINECODE"].ToString());
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(drpFGItemCode, dt, true);
                    drpFGItemCode.Focus();
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
                BL_WIP_BakingProcess blobj = new BL_WIP_BakingProcess();
                drpPendingBarcode.Items.Clear();
                drpWorkOrderNo.Items.Clear();                
                if (drpFGItemCode.SelectedIndex > 0)
                {
                    DataTable dt = blobj.BindWorkOrderNo(drpFGItemCode.SelectedItem.Text, Session["SiteCode"].ToString(), Session["LINECODE"].ToString());
                    if (dt.Rows.Count > 0)
                    {
                        CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                        clsCommon.FillComboBox(drpWorkOrderNo, dt, true);
                        drpWorkOrderNo.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void BindPendingBarcode()
        {
            try
            {
                BL_WIP_BakingProcess blobj = new BL_WIP_BakingProcess();
                DataTable dt = new DataTable();
                drpPendingBarcode.Items.Clear();              
                if (drpWorkOrderNo.SelectedIndex > 0)
                {
                    dt = blobj.BindPendingBarcode(drpFGItemCode.SelectedItem.Text, drpWorkOrderNo.SelectedItem.Text
                        , Session["SiteCode"].ToString(), Session["LINECODE"].ToString());
                    if (dt.Rows.Count > 0)
                    {
                        CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                        clsCommon.FillComboBox(drpPendingBarcode, dt, true);
                        drpPendingBarcode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void ScanBarcode(string sType)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (drpFGItemCode.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select fg item code", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpFGItemCode.Focus();
                    return;
                }
                if (drpWorkOrderNo.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select work order no", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpWorkOrderNo.Focus();
                    return;
                }
                string sBarcode = string.Empty;
                if (sType == "1")
                {
                    sBarcode = txtBarcode.Text.Trim();
                }
                else
                {
                    sBarcode = drpPendingBarcode.Text;
                }
                BL_WIP_BakingProcess blobj = new BL_WIP_BakingProcess();
                DataTable dt = blobj.dtValidatePCB(sBarcode, drpProcessID.Text, drpFGItemCode.Text, drpWorkOrderNo.Text
                    , Session["SiteCode"].ToString(), Session["UserID"].ToString(), Session["LINECODE"].ToString()
                    );
                if (dt.Rows.Count > 0)
                {
                    string sMessage = dt.Rows[0][0].ToString();
                    if (sMessage.StartsWith("N~") || sMessage.StartsWith("ERROR~"))
                    {
                        CommonHelper.ShowMessage(sMessage.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        txtBarcode.Text = string.Empty;
                        txtBarcode.Focus();
                        if (drpPendingBarcode.Items.Count > 0)
                        {
                            drpPendingBarcode.SelectedIndex = 0;
                        }
                        return;
                    }
                    else
                    {
                        CommonHelper.ShowMessage(sMessage.Split('~')[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                        txtBarcode.Text = string.Empty;
                        txtBarcode.Focus();
                        BindPendingBarcode();
                        return;
                    }
                }
                else
                {
                    CommonHelper.ShowMessage("No result found, Please try again", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBarcode.Text = string.Empty;
                    txtBarcode.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            msgdiv.InnerText = "";
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            drpPendingBarcode.Items.Clear();
            drpFGItemCode.Items.Clear();
            drpWorkOrderNo.Items.Clear();
            txtBarcode.Text = string.Empty;
            divOut.Visible = false;
            divIN.Visible = true;
        }
        protected void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtBarcode.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBarcode.Focus();
                    return;
                }
                ScanBarcode("1");
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
           
        }
        protected void drpProcessID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtBarcode.Text = string.Empty;
                divIN.Visible = true;
                divOut.Visible = false;
                drpPendingBarcode.Items.Clear();
                if (drpProcessID.SelectedIndex == 1)
                {
                    divIN.Visible = false;
                    divOut.Visible = true;
                    BindPendingBarcode();                   
                }                
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        protected void drpFGItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindWorkOrderNo();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void drpPendingBarcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpPendingBarcode.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select pending barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpPendingBarcode.Focus();
                    return;
                }
                ScanBarcode("2");
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
    }
}