using Common;
using System;
using System.Data;
using BusinessLayer.WIP;

namespace SGST.INE.WIP
{
    public partial class QualityRework : System.Web.UI.Page
    {
        BL_QualityRework blobj = new BL_QualityRework();
        string Message = "";
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
                    string _strRights = CommonHelper.GetRights("WIP QUALITY REWORK", (DataTable)Session["USER_RIGHTS"]);
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
                    BindDefect();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void BindDefect()
        {
            try
            {
                blobj = new BL_QualityRework();
                string sResult = string.Empty;
                DataTable dt = blobj.BindDefectMaster(out sResult, "WIP");
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(drpDefect, dt, true);
                    drpDefect.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void txtPCBID_TextChanged(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (drpType.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select type", msginfo, CommonHelper.MessageType.Info.ToString());
                    drpType.Focus();
                    txtPCBID.Text = string.Empty;
                    return;
                }
                else if (string.IsNullOrEmpty(txtPCBID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan barcode", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtPCBID.Focus(); return;
                }
                else
                {
                    string sDefectData = string.Empty;
                    if (drpDefect.Items.Count > 0)
                    {
                        if (drpDefect.SelectedIndex > 0)
                        {
                            sDefectData = drpDefect.SelectedValue.ToString();
                        }
                    }
                    blobj = new BL_QualityRework();
                    string sResult = blobj.QualityRework(
                        drpType.SelectedValue.ToString(),
                        txtPCBID.Text.Trim(), sDefectData, txtobservation.Text.Trim(),
                        txtRemarks.Text.Trim(), drpAction.SelectedValue.Trim(), Session["UserID"].ToString()
                        , Session["SiteCode"].ToString(), Session["LINECODE"].ToString()
                        );
                    if (sResult.ToUpper().StartsWith("SUCCESS~"))
                    {
                        Message = sResult.Split('~')[1].ToString();
                        CommonHelper.ShowMessage(Message, msgsuccess, CommonHelper.MessageType.Success.ToString());
                        txtPCBID.Text = string.Empty;
                        txtPCBID.Focus();
                        return;
                    }
                    else if (sResult.ToUpper().StartsWith("ERROR~"))
                    {
                        Message = sResult.Split('~')[1].ToString();
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                        txtPCBID.Text = string.Empty;
                        txtPCBID.Focus();

                    }
                    else if (sResult.ToUpper().StartsWith("N~"))
                    {
                        Message = sResult.Split('~')[1].ToString();
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                        txtPCBID.Text = string.Empty;
                        txtPCBID.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void ClearControl()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            txtPCBID.Text = string.Empty;            
            txtRemarks.Text = string.Empty;
            txtobservation.Text = string.Empty;
            if (drpDefect.Items.Count > 0)
            {
                drpDefect.SelectedIndex = 0;
            }
            if (drpAction.Items.Count > 0)
            {
                drpAction.SelectedIndex = 0;
            }
            divOut.Visible = false;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearControl();
        }

        protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpType.SelectedValue.ToUpper() == "1")
                {
                    divOut.Visible = false;                    
                }
                else
                {
                    divOut.Visible = true;
                }
                if (drpDefect.Items.Count > 0)
                {
                    drpDefect.SelectedIndex = 0;
                }
                if (drpAction.Items.Count > 0)
                {
                    drpAction.SelectedIndex = 0;
                }
                txtobservation.Text = string.Empty;
                txtRemarks.Text = string.Empty;
                txtPCBID.Text = string.Empty;
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}