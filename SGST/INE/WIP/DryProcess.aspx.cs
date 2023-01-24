using BusinessLayer;
using BusinessLayer.WIP;
using Common;
using System;
using System.Data;
namespace SGST.INE.WIP
{
    public partial class DryProcess : System.Web.UI.Page
    {
        string Message = "";
        static string sHeaderValue = string.Empty;
        static string sArea = string.Empty;
        BL_WIPDryProcess blobj = new BL_WIPDryProcess();
        private void getprinterlist()
        {
            try
            {
                BL_Common blCommonobj = new BL_Common();
                DataTable dt = blCommonobj.BINDPRINTER("RM");
                if (dt.Rows.Count > 0)
                {
                    CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                    clsCommon.FillComboBox(drpPrinterName, dt, true);
                    drpPrinterName.Focus();
                }
                else
                {
                    CommonHelper.ShowMessage("Printer not available", msgerror, CommonHelper.MessageType.Error.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Signin/v1/Login.aspx?Session=Null");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string sHeaderName = string.Empty;
            if (Request.QueryString["Name"] != null && Request.QueryString["Name"] != string.Empty)
            {
                sHeaderValue = Request.QueryString["Name"].ToString();
                if (Request.QueryString["Name"].Contains("WIP"))
                {
                    sArea = "WIP";
                }
                if (Request.QueryString["Name"].Contains("RM"))
                {
                    sArea = "RM";
                }
            }
           
            if (Session["usertype"].ToString() != "ADMIN")
            {
                string _strRights = string.Empty;
                if (sArea == "RM")
                {
                    _strRights = CommonHelper.GetRights("RM DRY PROCESS", (DataTable)Session["USER_RIGHTS"]);
                }
                else
                {
                    _strRights = CommonHelper.GetRights("WIP DRY PROCESS", (DataTable)Session["USER_RIGHTS"]);
                }
                CommonHelper._strRights = _strRights.Split('^');
                if (CommonHelper._strRights[0] == "0")  //Check view rights
                {
                    Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                }                
            }
            drpModule.Enabled = false;
            drpModule.CssClass = "form-control select2";
            if (sArea == "RM")
            {
                drpModule.SelectedIndex = 0;
            }
            else
            {
                drpModule.SelectedIndex = 1;
            }
            dvOutProcess.Visible = false;
            Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
            Response.AddHeader("Pragma", "no-cache");
            Response.AddHeader("Expires", "0");
            if (!IsPostBack)
            {
                try
                {
                    dvPrintergrup.Visible = true;
                    if (PCommon.sUseNetworkPrinter == "1")
                    {
                        getprinterlist();
                    }
                    else
                    {
                        dvPrintergrup.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                }
            }
        }

        protected void txtReelBarcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (string.IsNullOrEmpty(txtReelBarcode.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please enter part barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtReelBarcode.Focus();
                    return;
                }
                blobj = new BL_WIPDryProcess();
                string _Result = blobj.blDryProcess(
                    drpProcess.Text, drpModule.Text,
                    txtReelBarcode.Text, Convert.ToInt32(txtDays.Text), Session["SiteCode"].ToString()
                    , Session["UserID"].ToString(),
                    drpPrinterName.Text, "9100",
                    Session["LINECODE"].ToString()
                    );
                if (_Result.Length > 0)
                {
                    Message = _Result;
                    if (Message.StartsWith("SUCCESS~"))
                    {
                        CommonHelper.ShowMessage(Message.Split('~')[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                        txtReelBarcode.Text = string.Empty;
                        txtReelBarcode.Focus();
                    }
                    else
                    {
                        CommonHelper.ShowMessage(Message.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        txtReelBarcode.Text = string.Empty;
                        txtReelBarcode.Focus();
                    }
                }
                else
                {
                    txtReelBarcode.Text = string.Empty;
                    txtReelBarcode.Focus();
                    CommonHelper.ShowMessage("No result found for scanned barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                }
            }
            catch (Exception ex)
            {
                txtReelBarcode.Text = string.Empty;
                txtReelBarcode.Focus();
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                txtReelBarcode.Text = string.Empty;
                txtReelBarcode.Focus();
                drpProcess.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void drpProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dvOutProcess.Visible = false;
                if (drpProcess.SelectedIndex == 1)
                {
                    dvOutProcess.Visible = true;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}