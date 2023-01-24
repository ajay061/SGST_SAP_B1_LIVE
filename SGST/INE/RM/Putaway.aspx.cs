using BusinessLayer;
using Common;
using System;
using System.Data;

namespace SGST.INE.RM
{
    public partial class Putaway : System.Web.UI.Page
    {
        BL_Putaway blobj = new BL_Putaway();
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
                    string _strRights = CommonHelper.GetRights("RM PUTAWAY", (DataTable)Session["USER_RIGHTS"]);
                    CommonHelper._strRights = _strRights.Split('^');
                    if (CommonHelper._strRights[0] == "0")  //Check view rights
                    {
                        Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                    }
                }
                Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
                Response.AddHeader("Pragma", "no-cache");
                Response.AddHeader("Expires", "0");
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            txtLocation.Text = "";
            txtBarcode.Text = "";
            txtLocation.Focus();
            lblLocationType.Text = string.Empty;
            lblCounter.Text = "0";
            hidLocationType.Value = "";
            txtSuggestedLocation.Text = string.Empty;
            txtGetLocation.Text = string.Empty;
            lblPendingCount.Text = "0";
            lblLastScanned.Text = string.Empty;
            txtGetLocation.Focus();
            lblBatchNo.Text = "";
        }

        private void GetLocation()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            if (string.IsNullOrEmpty(txtGetLocation.Text.Trim()))
            {
                CommonHelper.ShowMessage("Please scan barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                txtGetLocation.Focus();
                return;
            }
            try
            {
                blobj = new BL_Putaway();
                DataTable dtGRN = blobj.GetLocationCode(txtGetLocation.Text.Trim());
                if (dtGRN.Rows.Count > 0)
                {
                    string Credits = Convert.ToString(dtGRN.Rows[0][0].ToString());
                    if (Credits.StartsWith("N~"))
                    {
                        CommonHelper.ShowMessage(Credits.Split('~')[1].ToString(), msgerror, CommonHelper.MessageType.Error.ToString());
                        txtGetLocation.Focus();
                        txtLocation.Text = "";
                        txtBarcode.Text = "";
                        lblLocationType.Text = string.Empty;
                        txtSuggestedLocation.Text = string.Empty;
                        txtGetLocation.Text = string.Empty;
                        return;
                    }
                    else
                    {
                        txtLocation.Focus();
                        //txtLocation.Text = Credits.Split('~')[3];
                        lblLocationType.Text = Credits.Split('~')[2];
                        hidLocationType.Value = Credits.Split('~')[2];
                        txtSuggestedLocation.Text = Credits.Split('~')[3];
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message); txtLocation.Text = string.Empty;
                txtGetLocation.Focus();
            }
        }
        private void SaveBarcode()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (string.IsNullOrEmpty(txtLocation.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan location barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtLocation.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtBarcode.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBarcode.Focus();
                    return;
                }
                blobj = new BL_Putaway();
                string result = blobj.SaveBarcode(txtLocation.Text, txtBarcode.Text, Session["UserID"].ToString());
                if (result.Length > 0)
                {
                    if (result.StartsWith("N~"))
                    {
                        CommonHelper.ShowMessage(result.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());                        
                        txtBarcode.Focus();
                        txtBarcode.Text = "";
                    }
                    else
                    {
                        CommonHelper.ShowMessage(result.Split('~')[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                        lblLastScanned.Text = txtBarcode.Text.Trim();
                        txtBarcode.Text = "";
                        txtBarcode.Focus();
                        lblPendingCount.Text = result.Split('~')[2];
                        lblBatchNo.Text = result.Split('~')[3];
                        lblCounter.Text = Convert.ToString(Convert.ToInt32(lblCounter.Text) + 1);
                        if (Convert.ToInt32(lblPendingCount.Text) == 0)
                        {
                            CommonHelper.ShowMessage("Putway completed for scanned item ", msgsuccess, CommonHelper.MessageType.Success.ToString());
                        }
                    }
                }
                else
                {
                    CommonHelper.ShowMessage("No result found for scanned barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtLocation.Text = "";
                    txtBarcode.Focus();
                    txtBarcode.Text = "";
                    hidLocationType.Value = "";
                    lblBatchNo.Text = "";
                    lblLocationType.Text = string.Empty;
                    txtSuggestedLocation.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {

            }
        }
        protected void txtGetLocation_TextChanged(object sender, EventArgs e)
        {
            GetLocation();
        }
        protected void txtLocation_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtLocation.Text.Trim()))
            {
                CommonHelper.ShowMessage("Please scan location", msgerror, CommonHelper.MessageType.Error.ToString());
                txtLocation.Focus();
                return;
            }
            txtBarcode.Focus();
        }
        protected void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            SaveBarcode();
        }
    }
}


