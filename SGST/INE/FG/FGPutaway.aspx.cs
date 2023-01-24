using BusinessLayer;
using Common;
using System;
using System.Configuration;
using System.Data;

namespace SGST.INE.Operation
{
    public partial class Putaway : System.Web.UI.Page
    {
        static string sFIFORequired = ConfigurationManager.AppSettings["_FIFOREQUIRED"].ToString();
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Signin/v1/Login.aspx?Session=Null");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["usertype"].ToString() != "ADMIN")
                {
                    string _strRights = CommonHelper.GetRights("FG PUTAWAY", (DataTable)Session["USER_RIGHTS"]);
                    CommonHelper._strRights = _strRights.Split('^');
                    if (CommonHelper._strRights[0] == "0")  //Check view rights
                    {
                        Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                    }
                }
                Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
                Response.AddHeader("Pragma", "no-cache");
                Response.AddHeader("Expires", "0");
                txtBarcode.Focus();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (string.IsNullOrEmpty(txtBarcode.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan barcode.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBarcode.Focus();
                    return;
                }
                string BARCODE = txtBarcode.Text.Trim();
                string SCANBY = Session["UserID"].ToString();

                BL_FGPutAway dlobj = new BL_FGPutAway();
                string _OuptPuts = dlobj.sScanSecondaryBarcode(BARCODE, Session["SiteCode"].ToString(), Session["LINECODE"].ToString(),
                    drpType.Text, sFIFORequired);
                string[] Message = _OuptPuts.Split('~');
                if (_OuptPuts.StartsWith("Ns~") || _OuptPuts.StartsWith("ERROR~") || _OuptPuts.StartsWith("N~"))
                {
                    CommonHelper.ShowMessage(Message[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBarcode.Text = string.Empty;
                    txtBarcode.Focus();
                }
                else
                {
                    CommonHelper.ShowMessage(Message[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                    txtLocation.Focus();
                }
            }
            catch (Exception ex)
            {
                txtBarcode.Text = string.Empty;
                txtBarcode.Focus();
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void txtLocation_TextChanged(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (string.IsNullOrEmpty(txtBarcode.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan barcode.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBarcode.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtLocation.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan location.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtLocation.Focus();
                    return;
                }
                string BARCODE = txtBarcode.Text.Trim();
                string SCANBY = Session["UserID"].ToString();
                string sLocation = txtLocation.Text.Trim();

                BL_FGPutAway dlobj = new BL_FGPutAway();
                string _OuptPuts = dlobj.StoreFGLocation(BARCODE, SCANBY, sLocation, Session["SiteCode"].ToString(),
                    Session["LINECODE"].ToString(), drpType.Text);
                string[] Message = _OuptPuts.Split('~');
                if (_OuptPuts.StartsWith("Ns~") || _OuptPuts.StartsWith("ERROR~") || _OuptPuts.StartsWith("N~"))
                {
                    CommonHelper.ShowMessage(Message[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    txtLocation.Text = string.Empty;
                    txtLocation.Focus();
                }
                else
                {
                    CommonHelper.ShowMessage(Message[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                    txtLocation.Text = string.Empty;
                    txtBarcode.Text = string.Empty;
                    txtBarcode.Focus();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.ToUpper().Contains("PRIMARY KEY"))
                {
                    CommonHelper.ShowMessage("Barcode already scanned, Please scan another barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtLocation.Text = string.Empty;
                    txtBarcode.Text = string.Empty;
                    txtBarcode.Focus();
                }
                else
                {
                    CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                }
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            txtLocation.Enabled = true;
            txtLocation.ReadOnly = false;
            txtLocation.Text = "";
            txtBarcode.Text = "";
            txtBarcode.Focus();
        }
    }
}


