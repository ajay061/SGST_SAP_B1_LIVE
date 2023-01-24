using Common;
using System;
using BusinessLayer.WIP;
using System.Data;

namespace SGST.INE.WIP
{
    public partial class VISampling : System.Web.UI.Page
    {
        string Message = "";
        BL_WIP_VIQuality blobj = null;
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Signin/v1/Login.aspx?Session=Null");
            }
        }
        private void BindFGItemCode()
        {
            try
            {
                drpFGItemCode.Items.Clear();
                txtReelID.Text = string.Empty;
                blobj = new BL_WIP_VIQuality();
                if (txtScanMachineID.Text.Length > 0)
                {
                    string sResult = string.Empty;
                    DataTable dtFGItemCode = blobj.BindFGItemCode(txtScanMachineID.Text.Trim()
                         , Session["SiteCode"].ToString(), Session["LINECODE"].ToString()

                        );
                    if (dtFGItemCode.Rows.Count > 0)
                    {
                        CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                        clsCommon.FillComboBox(drpFGItemCode, dtFGItemCode, true);
                        drpFGItemCode.SelectedIndex = 0;
                        drpFGItemCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usertype"].ToString() != "ADMIN")
            {
                string _strRights = CommonHelper.GetRights("VI QUALITY", (DataTable)Session["USER_RIGHTS"]);
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

        protected void txtReelID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (string.IsNullOrEmpty(txtScanMachineID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan machine barcode", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtScanMachineID.Focus();
                    txtScanMachineID.Text = string.Empty;
                    return;
                }
                else if (drpFGItemCode.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select fg item code", msginfo, CommonHelper.MessageType.Info.ToString());
                    drpFGItemCode.Focus(); txtReelID.Text = string.Empty;
                    return;
                }
                else if (string.IsNullOrEmpty(txtReelID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan PCB barcode", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtReelID.Focus();
                    txtReelID.Text = string.Empty;
                    return;
                }
                string sSamplePCB = "0";
                bool PDIInspectionBox = false;
                if (Convert.ToInt32(lblTotalPCB.Text) <= Convert.ToInt32(lblSamplingPCB.Text))
                {
                    sSamplePCB = "1";
                    PDIInspectionBox = true;

                }
                blobj = new BL_WIP_VIQuality();
                DataTable dt = blobj.QualityBarcode(txtReelID.Text.Trim(),
                    txtScanMachineID.Text.Trim(),
                   drpFGItemCode.Text, Session["SiteCode"].ToString(), Session["LINECODE"].ToString(), sSamplePCB
                   , Session["UserID"].ToString(), chkPDIInspection.Checked
                   );
                if (dt.Rows.Count > 0)
                {
                    string sResult = dt.Rows[0][0].ToString();
                    Message = sResult.Split('~')[1];
                    if (sResult.StartsWith("SUCCESS"))
                    {
                        CommonHelper.ShowMessage(Message, msgsuccess, CommonHelper.MessageType.Success.ToString());
                        if (sSamplePCB == "1")
                        {
                            lblSamplingPCB.Text = "0";
                            Response.Write("<script LANGUAGE='JavaScript' >alert('This is the sample PCB, Please move this PCB to OQC Stage')</script>");
                        }
                        else
                        {
                            drpFGItemCode_SelectedIndexChanged(null, null);
                        }
                        txtReelID.Text = string.Empty;
                        txtReelID.Focus();
                        return;
                    }
                    else if (sResult.StartsWith("N~"))
                    {
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                        txtReelID.Text = string.Empty;
                        txtReelID.Focus();
                        return;
                    }
                    else if (sResult.StartsWith("ERROR~"))
                    {
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                        txtReelID.Text = string.Empty;
                        txtReelID.Focus();
                    }
                    else if (sResult.StartsWith("NOTFOUND~"))
                    {
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                        txtReelID.Text = string.Empty;
                        txtReelID.Focus();
                    }
                    else if (sResult.StartsWith("MACHINEFAILED~"))
                    {
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                        txtReelID.Text = string.Empty;
                        txtReelID.Focus();
                    }
                }
                else
                {
                    CommonHelper.ShowMessage("No result found for scanned barcode Please try again", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtReelID.Text = string.Empty;
                    txtReelID.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                txtReelID.Text = string.Empty;
                txtScanMachineID.Text = string.Empty;
                drpFGItemCode.Items.Clear();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError,
                System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void txtScanMachineID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (string.IsNullOrEmpty(txtScanMachineID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Scan Machine", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtScanMachineID.Focus();
                    return;
                }
                blobj = new BL_WIP_VIQuality();
                DataTable dt = blobj.ValidateMachine(txtScanMachineID.Text.Trim(), Session["SiteCode"].ToString(), Session["LINECODE"].ToString());
                if (dt.Rows.Count > 0)
                {
                    CommonHelper.ShowMessage("Valid machine", msgsuccess, CommonHelper.MessageType.Success.ToString());
                    lblMachineName.Text = dt.Rows[0][0].ToString();
                    lblModelNo.Text = dt.Rows[0][2].ToString();
                    BindFGItemCode();
                }
                else
                {
                    CommonHelper.ShowMessage("Invalid machine", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtScanMachineID.Focus();
                    txtScanMachineID.Text = string.Empty;
                    lblMachineName.Text = "";
                    lblModelNo.Text = "";
                    drpFGItemCode.Items.Clear();
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
                if (string.IsNullOrEmpty(txtScanMachineID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Scan Machine", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtScanMachineID.Focus();
                    return;
                }
                lblSamplingPCB.Text = "0";
                lblTotalPCB.Text = "0";
                lblPDquantityPCB.Text = "0";
                lblSamplingPDIPCB.Text = "0";
                if (drpFGItemCode.SelectedIndex > 0)
                {
                    blobj = new BL_WIP_VIQuality();
                    DataTable dt = blobj.GetSampleQtyData(drpFGItemCode.Text,
                        Session["SiteCode"].ToString(), Session["LINECODE"].ToString(), Session["UserID"].ToString());
                    if (dt.Rows.Count > 0)
                    {
                        lblTotalPCB.Text = dt.Rows[0][0].ToString();
                        lblSamplingPCB.Text = dt.Rows[0][2].ToString();
                    }
                    DataTable dt2 = blobj.GetPdiSamplingQty(drpFGItemCode.Text,
                       Session["SiteCode"].ToString(), txtScanMachineID.Text, Session["UserID"].ToString());
                    if (dt2.Rows.Count > 0)
                    {
                        lblPDquantityPCB.Text = dt2.Rows[0][0].ToString();
                    }
                    DataTable dt3 = blobj.GetPdiSampling(drpFGItemCode.Text,
                       Session["SiteCode"].ToString(), txtScanMachineID.Text, Session["UserID"].ToString());
                    if (dt3.Rows.Count > 0)
                    {
                        lblSamplingPDIPCB.Text = dt3.Rows[0][0].ToString();
                    }


                }
                
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}