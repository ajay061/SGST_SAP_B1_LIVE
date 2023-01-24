using System;
using System.Data;
using Common;
using BusinessLayer.WIP;
using System.Configuration;

namespace SGST.INE.WIP
{
    public partial class SolderPast_Scanning : System.Web.UI.Page
    {
        static string sFIFORequired = ConfigurationManager.AppSettings["_FIFOREQUIRED"].ToString();
        DataTable dt = new DataTable();
        string Message = string.Empty;
        BL_SolderPastMcDetails blobj = new BL_SolderPastMcDetails();
        protected void Page_Init(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
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
                    string _strRights = CommonHelper.GetRights("SOLDER PASTE SCANNING", (DataTable)Session["USER_RIGHTS"]);
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
                    BindFGItemCode();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        public void ScanMachineCode()
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (txtMachineiD.Text.Trim() == string.Empty)
                {
                    CommonHelper.ShowMessage("Please scan machine barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtMachineiD.Focus();
                    txtRELLID.Text = "";
                    return;
                }
                blobj = new BL_SolderPastMcDetails();
                DataTable dtScanMachineBarcode = blobj.ScanMachineBarcode(txtMachineiD.Text.Trim(), Session["SiteCode"].ToString()
                    , Session["LINECODE"].ToString()
                    );
                if (dtScanMachineBarcode.Rows.Count > 0)
                {                    
                    lblMachineName.Text = dtScanMachineBarcode.Rows[0][0].ToString().ToLower();
                    txtMachineiD.Enabled = false;
                    txtMachineiD.CssClass = "form-control";
                    lblModelName.Text = dtScanMachineBarcode.Rows[0][2].ToString();
                    lblMachineSeq.Text = dtScanMachineBarcode.Rows[0][1].ToString();
                }
                else
                {
                    CommonHelper.ShowMessage("Machine code is not valid, Please scan valid machine", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtMachineiD.Text = "";
                    txtMachineiD.Focus();
                    lblModelName.Text = "";
                    lblMachineSeq.Text = "";
                    lblMachineName.Text = "";
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
            txtMachineiD.Text = "";
            txtMachineiD.Focus();
            lblMachineName.Text = "";
            txtRELLID.Text = "";
            lblMachineSeq.Text = "";
            lblModelName.Text = "";
            txtMachineiD.Enabled = true;         
            if (ddlProcessType.Items.Count > 0)
            {
                ddlProcessType.SelectedIndex = 0;
            }
        }

        private void BindFGItemCode()
        {
            try
            {
                drpFGItemCode.Items.Clear();
                BL_SolderPastMcDetails blobj = new BL_SolderPastMcDetails();
                string sResult = string.Empty;
                DataTable dtFGItemCode = blobj.dtBindFGItemCode(Session["SiteCode"].ToString());
                if (dtFGItemCode.Rows.Count > 0)
                {
                    CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                    clsCommon.FillComboBox(drpFGItemCode, dtFGItemCode, true);
                    drpFGItemCode.SelectedIndex = 0;
                    drpFGItemCode.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }

        protected void txtRELLID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Get_REELDETAILS();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }

        }
        public void Get_REELDETAILS()
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (string.IsNullOrEmpty(txtMachineiD.Text))
                {
                    CommonHelper.ShowMessage("Please scan machine barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtMachineiD.Focus();
                    txtRELLID.Text = "";
                    return;
                }
                if (string.IsNullOrEmpty(lblMachineName.Text))
                {
                    CommonHelper.ShowMessage("Please scan machine barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtMachineiD.Focus();
                    txtRELLID.Text = "";
                    return;
                }
                if (ddlProcessType.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select process", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlProcessType.Focus();
                    txtRELLID.Text = "";
                    return;
                }
                if (drpFGItemCode.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select fg item code", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpFGItemCode.Focus();
                    txtRELLID.Text = "";
                    return;
                }
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,
                      System.Reflection.MethodBase.GetCurrentMethod().Name, "Scan Solder Paste Module Data : " + txtRELLID.Text);
                blobj = new BL_SolderPastMcDetails();
                int iSolderPasteTime = Convert.ToInt32(ConfigurationManager.AppSettings["_RMISSUETIME"].ToString()); // 4 hours time between issuance
                string sResult = string.Empty;
                sResult = blobj.Get_REELIDS(
                    txtRELLID.Text, lblMachineName.Text, ddlProcessType.SelectedItem.Text.Trim(),
                  txtMachineiD.Text.ToUpper(), Convert.ToInt32(lblMachineSeq.Text), drpFGItemCode.SelectedItem.Text
                   ,iSolderPasteTime, Session["SiteCode"].ToString()
                   , Session["LINECODE"].ToString(),sFIFORequired
                   );
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,
                      System.Reflection.MethodBase.GetCurrentMethod().Name, "Validate Solder Paste Module Data : " + sResult);
                if (sResult.ToUpper().StartsWith("SUCCESS~"))
                {                  
                    string PART_CODE = string.Empty;
                    PART_CODE = sResult.Split('~')[2].ToString();
                    sResult = string.Empty;
                    sResult = blobj.SaveMachineData(txtRELLID.Text, PART_CODE,
                       Session["userid"].ToString(),
                       ddlProcessType.SelectedItem.Text.Trim(), lblMachineName.Text, txtMachineiD.Text,
                       drpFGItemCode.SelectedItem.Text, iSolderPasteTime
                       , Session["SiteCode"].ToString(), Session["LINECODE"].ToString()
                       );
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,
                        System.Reflection.MethodBase.GetCurrentMethod().Name, "Save Solder Paste Module Data : " + sResult);
                    if (sResult.ToUpper().StartsWith("SUCCESS~"))
                    {
                        Message = sResult.Split('~')[1].ToString();
                        CommonHelper.ShowMessage(Message, msgsuccess, CommonHelper.MessageType.Success.ToString());
                        txtRELLID.Text = "";
                        txtRELLID.Focus();
                    }
                    else if (sResult.StartsWith("ERROR~"))
                    {
                        Message = sResult.Split('~')[1].ToString();
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());                       
                        txtRELLID.Text = "";
                        txtRELLID.Focus();
                    }
                    else if (sResult.StartsWith("N~"))
                    {
                        Message = sResult.Split('~')[1].ToString();
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                        txtRELLID.Text = "";
                        txtRELLID.Focus();
                    }
                }
                else if (sResult.StartsWith("ERROR~"))
                {
                    Message = sResult.Split('~')[1].ToString();
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    txtRELLID.Text = "";
                    txtRELLID.Focus();
                }
                else if (sResult.StartsWith("N~"))
                {
                    Message = sResult.Split('~')[1].ToString();
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    txtRELLID.Text = "";
                    txtRELLID.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void txtProcessName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ScanMachineCode();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}