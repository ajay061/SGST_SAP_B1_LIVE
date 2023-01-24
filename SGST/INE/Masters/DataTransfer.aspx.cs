using Common;
using System;
using System.Data;
using BusinessLayer.Masters;

namespace SGST.INE.Masters
{
    public partial class DataTransfer : System.Web.UI.Page
    {
        BL_DataTransfer blobj = new BL_DataTransfer();
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Signin/v1/Login.aspx?Session=Null");
            }
        }
        private void _ResetField()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            txtTransferValue.Focus();
            txtTransferValue.Text = "";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (Session["usertype"].ToString() != "ADMIN")
                {
                    string _strRights = CommonHelper.GetRights("DATA TRANSFER", (DataTable)Session["USER_RIGHTS"]);
                    CommonHelper._strRights = _strRights.Split('^');
                    if (CommonHelper._strRights[0] == "0")  //Check view rights
                    {
                        Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void btnTransfer_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (txtTransferValue.Text.Trim() == "")
                {
                    CommonHelper.ShowMessage("Please enter transfer value", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtTransferValue.Focus();
                    return;
                }
                blobj = new BL_DataTransfer();
                DataTable dt = blobj.DataTranfer(drpType.Text, txtTransferValue.Text);
                if (dt.Rows.Count > 0)
                {
                    string sResult = dt.Rows[0][0].ToString();
                    if (sResult.Length > 0)
                    {
                        if (sResult.StartsWith("SUCCESS~"))
                        {
                            _ResetField();
                            CommonHelper.ShowMessage(sResult, msgsuccess, CommonHelper.MessageType.Success.ToString());
                        }
                        else
                        {
                            _ResetField();
                            CommonHelper.ShowMessage(sResult, msgerror, CommonHelper.MessageType.Error.ToString());
                        }
                    }
                }
                else
                {
                    CommonHelper.ShowMessage("No record found to transfer", msgerror, CommonHelper.MessageType.Error.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError,
                     System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                if (ex.Message.Contains("Could not find server"))
                {
                    CommonHelper.ShowMessage("Unable to connect with Base server, Please contact server admin for checking linking server.", msgerror, CommonHelper.MessageType.Error.ToString());
                }
                else
                {
                    CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                }        
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            _ResetField();
        }
    }
}