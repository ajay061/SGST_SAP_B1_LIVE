using Common;
using System;
using System.Data;
using BusinessLayer.WIP;

namespace SGST.INE.WIP
{
    public partial class WIPWorkOrderClose : System.Web.UI.Page
    {
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
                string _strRights = CommonHelper.GetRights("WORK ORDER CLOSE", (DataTable)Session["USER_RIGHTS"]);
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
                    BindFGItemCode();
                }
                catch (Exception ex)
                {
                    CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                }
            }
        }
        private void BindFGItemCode()
        {
            try
            {
                txtRemarks.Text = string.Empty;
                drpWorkOrderNo.Items.Clear();
                drpFGItemCode.Items.Clear();
                blobj = new BL_WIP_BoxPacking();
                DataTable dt = blobj.BindPendingFGItemForWOClose(Session["SiteCode"].ToString());
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(drpFGItemCode, dt, true);
                    drpFGItemCode.SelectedIndex = 0;
                    drpFGItemCode.Focus();
                    return;
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
                drpWorkOrderNo.Items.Clear();
                blobj = new BL_WIP_BoxPacking();
                DataTable dt = blobj.BindPendingWorkOrderForWOClose(drpFGItemCode.SelectedItem.Text
                    , Session["SiteCode"].ToString()
                    );
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
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void drpFGItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpFGItemCode.SelectedIndex > 0)
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

        protected void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                string confirmValue = Request.Form["confirm_value"];
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (drpFGItemCode.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select fg item code", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpFGItemCode.Focus();
                    return;
                }
                if (drpWorkOrderNo.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select work order no.", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpWorkOrderNo.Focus();
                    return;
                }
                if (confirmValue == "No")
                {
                    return;
                }
                blobj = new BL_WIP_BoxPacking();
                DataTable dt = blobj.WorkOrderClose(drpFGItemCode.SelectedItem.Text,drpWorkOrderNo.SelectedItem.Text,
                    txtRemarks.Text.Trim(), Session["SiteCode"].ToString(), Session["UserID"].ToString());
                if (dt.Rows.Count > 0)
                {
                    string sResult = dt.Rows[0][0].ToString();
                    if (sResult.StartsWith("SUCCESS~"))
                    {
                        CommonHelper.ShowMessage("Work order has been close", msgsuccess, CommonHelper.MessageType.Success.ToString());
                        BindFGItemCode();
                    }
                    else
                    {
                        CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    }
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
            BindFGItemCode();         
        }
    }
}