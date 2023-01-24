using BusinessLayer.WIP;
using Common;
using System;
using System.Configuration;
using System.Data;

namespace SGST.INE.WIP
{
    public partial class WIPPutway : System.Web.UI.Page
    {
        static string sFIFORequired = ConfigurationManager.AppSettings["_FIFOREQUIRED"].ToString();
        BL_WIP_Putway blobj = new BL_WIP_Putway();
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Signin/v1/Login.aspx?Session=Null");
            }
        }
        string Message = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    txtBarcode.ReadOnly = true;
                    txtLocation.Focus();
                }
                if (Session["usertype"].ToString() != "ADMIN")
                {
                    string _strRights = CommonHelper.GetRights("WIP PUTAWAY", (DataTable)Session["USER_RIGHTS"]);
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
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void txtLocation_TextChanged(object sender, EventArgs e)
        {
            CheckIfLocationExists();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            txtLocation.Text = "";
            txtLocation.Enabled = true;
            txtLocation.ReadOnly = false;
            txtBarcode.Text = "";
            txtLocation.Focus();
            lblLocationType.Text = string.Empty;
            lblCounter.Text = "0";
            hidLocationType.Value = "";
        }

        protected void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            if (txtLocation.Text == "")
            {
                CommonHelper.ShowMessage("Please scan location barcode.", msgerror, CommonHelper.MessageType.Error.ToString());
                txtLocation.Focus();
                return;

            }
            SaveBarcode();
        }
        private void CheckIfLocationExists()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            if (string.IsNullOrEmpty(txtLocation.Text.Trim()))
            {
                CommonHelper.ShowMessage("Please scan location barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                txtLocation.Focus();
                return;
            }
            try
            {
                blobj = new BL_WIP_Putway();
                DataTable dtGRN = blobj.GetLocationCode(txtLocation.Text.Trim(), Session["SiteCode"].ToString());
                if (dtGRN.Rows.Count > 0)
                {
                    string Credits = Convert.ToString(dtGRN.Rows[0][0].ToString());
                    if (Credits.StartsWith("N~"))
                    {
                        CommonHelper.ShowMessage("Scanned location not found", msgerror, CommonHelper.MessageType.Error.ToString());
                        txtLocation.Focus();
                        txtLocation.Text = "";
                        txtBarcode.Text = "";
                        txtBarcode.ReadOnly = false;
                        return;
                    }
                    else
                    {
                        txtBarcode.Focus();
                        txtBarcode.ReadOnly = false;
                        lblLocationType.Text = Credits.Split('~')[2];
                        hidLocationType.Value = Credits.Split('~')[2];
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message); txtLocation.Text = string.Empty;
                txtLocation.Focus();
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
                blobj = new BL_WIP_Putway();
                string result = blobj.SaveBarcode(txtLocation.Text, txtBarcode.Text
                    , Session["SiteCode"].ToString()
                    , Session["UserID"].ToString()
                    , Session["LINECODE"].ToString(), sFIFORequired
                    );
                Message = result;
                string[] msg = Message.Split('~');
                string Msgs = msg[0];
                if (result.Length > 0)
                {
                    if (msg[0].StartsWith("N"))
                    {
                        if (msg[1].Contains("Location is not mapped with UNDERQUALITY"))
                        {
                            CommonHelper.ShowMessage(msg[1], msgerror, CommonHelper.MessageType.Error.ToString());
                            txtLocation.Text = "";
                            txtLocation.Focus();
                            txtBarcode.Text = "";
                            hidLocationType.Value = "";
                            lblLocationType.Text = string.Empty;
                        }
                        else
                        {
                            if (Message.Contains("Scanned location is not"))
                            {
                                CommonHelper.ShowMessage(msg[1], msgerror, CommonHelper.MessageType.Error.ToString());
                                txtLocation.Text = "";
                                txtLocation.Focus();
                                txtBarcode.Text = "";

                            }
                            else
                            {
                                CommonHelper.ShowMessage(msg[1], msgerror, CommonHelper.MessageType.Error.ToString());
                                txtBarcode.Text = "";
                                txtBarcode.Focus();
                            }
                        }
                    }
                    else
                    {
                        CommonHelper.ShowMessage(msg[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                        txtBarcode.Text = "";
                        txtBarcode.Focus();
                        txtBarcode.Enabled = true;
                        lblCounter.Text = Convert.ToString(Convert.ToInt32(lblCounter.Text) + 1);
                    }
                }
                else
                {
                    CommonHelper.ShowMessage(msg[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBarcode.Text = "";
                    txtBarcode.Focus();
                }
            }
            catch (Exception ex)
            {
                txtBarcode.Text = "";
                hidLocationType.Value = "";
                lblLocationType.Text = string.Empty;
                txtLocation.Text = string.Empty;
                txtLocation.Focus();
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {
                lblLocationType.Text = hidLocationType.Value;
            }
        }
    }
}