using Common;
using System;
using BusinessLayer.FG;
using System.Data;
namespace SGST.INE.FG
{
    public partial class Quality_MarketReturn : System.Web.UI.Page
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
            try
            {
                if (Session["usertype"].ToString() != "ADMIN")
                {
                    string _strRights = CommonHelper.GetRights("MARKET RETURN QUALITY", (DataTable)Session["USER_RIGHTS"]);
                    CommonHelper._strRights = _strRights.Split('^');
                    if (CommonHelper._strRights[0] == "0")  //Check view rights
                    {
                        Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                    }
                }
                Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
                Response.AddHeader("Pragma", "no-cache");
                Response.AddHeader("Expires", "0");
                txtBoxID.Focus();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void txtBoxID_TextChanged(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (txtBoxID.Text.Trim() == string.Empty)
                {
                    CommonHelper.ShowMessage("Scan barcode is empty", msgerror, CommonHelper.MessageType.Error.ToString());
                    return;
                }
                else
                {
                    string BARCODE = txtBoxID.Text;
                    BL_FGCustomerReturn dlobj = new BL_FGCustomerReturn();
                    string _OuptPut = dlobj.CheckFGBarcode(BARCODE, Session["SiteCode"].ToString());
                    string[] Message = _OuptPut.Split('~');
                    if (_OuptPut.StartsWith("N~"))
                    {
                        CommonHelper.ShowMessage(Message[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        txtBoxID.Text = string.Empty;
                        txtBoxID.Focus();
                        btnOK.Visible = true;
                        btnReject.Visible = true;
                    }
                    else if (_OuptPut.StartsWith("ERROR~"))
                    {
                        CommonHelper.ShowMessage(Message[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        txtBoxID.Text = string.Empty;
                        txtBoxID.Focus();
                    }
                    else
                    {
                        CommonHelper.ShowMessage(Message[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (txtBoxID.Text.Trim() == string.Empty)
                {
                    CommonHelper.ShowMessage("Barcode is empty", msgerror, CommonHelper.MessageType.Error.ToString());
                    return;
                }
                else
                {
                    string BARCODE = txtBoxID.Text.Trim();
                    string SCANBY = Session["UserID"].ToString();
                    string VAL = "1";
                    BL_FGCustomerReturn dlobj = new BL_FGCustomerReturn();
                    string _OuptPut = dlobj.UpdateCustomerReturnQuality(BARCODE, VAL, SCANBY,txtRemarks.Text,txtObservation.Text
                        , Session["SiteCode"].ToString(), Session["LINECODE"].ToString()
                        );
                    string[] Message = _OuptPut.Split('~');
                    if (_OuptPut.StartsWith("N~"))
                    {
                        CommonHelper.ShowMessage(Message[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    else if (_OuptPut.StartsWith("ERROR~"))
                    {
                        CommonHelper.ShowMessage(Message[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    else
                    {
                        CommonHelper.ShowMessage(Message[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                    }
                    txtBoxID.Text = string.Empty;
                    txtBoxID.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (txtBoxID.Text.Trim() == string.Empty)
                {
                    CommonHelper.ShowMessage("Barcode is empty", msgerror, CommonHelper.MessageType.Error.ToString());
                    return;
                }
                else
                {

                    string BARCODE = txtBoxID.Text.Trim();
                    string SCANBY = Session["UserID"].ToString();
                    string VAL = "2";
                    BL_FGCustomerReturn dlobj = new BL_FGCustomerReturn();
                    string _OuptPut = dlobj.UpdateCustomerReturnQuality(BARCODE, VAL, SCANBY, txtRemarks.Text, txtObservation.Text
                        , Session["SiteCode"].ToString(), Session["LINECODE"].ToString()
                        );
                    string[] Message = _OuptPut.Split('~');
                    if (_OuptPut.StartsWith("N~"))
                    {
                        CommonHelper.ShowMessage(Message[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    else if (_OuptPut.StartsWith("ERROR~"))
                    {
                        CommonHelper.ShowMessage(Message[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    else
                    {
                        CommonHelper.ShowMessage(Message[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                    }
                    txtBoxID.Text = string.Empty;
                    txtBoxID.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
        }
    }
}