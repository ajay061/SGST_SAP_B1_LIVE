using BusinessLayer;
using Common;
using System;
using System.Data;
using PL;
using BusinessLayer.MES.PRINTING;
using System.Threading;

namespace SGST.INE.MOB
{
    public partial class MobLabelPrinting : System.Web.UI.Page
    {
        BL_LabelPrinting blobj = new BL_LabelPrinting();
        string Message = string.Empty;
        static string sHeaderValue = string.Empty;
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
                Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
                Response.AddHeader("Pragma", "no-cache");
                Response.AddHeader("Expires", "0");
                string sHeaderName = string.Empty;
                if (Request.QueryString["Name"] != null && Request.QueryString["Name"] != string.Empty)
                {
                    sHeaderValue = Request.QueryString["Name"].ToString();
                    if (Request.QueryString["Name"].Contains("PCB"))
                    {
                        lblHeader.Text = "UNIT LABEL PRINTING";
                    }
                    else if (Request.QueryString["Name"].Contains("IMEI"))
                    {
                        lblHeader.Text = "STAND LABEL PRINTING";
                    }
                    else if (Request.QueryString["Name"].Contains("GB"))
                    {
                        lblHeader.Text = "UNIT GB PRINTING";
                    }
                    else if (Request.QueryString["Name"].Contains("MRP"))
                    {
                        lblHeader.Text = "UNIT MRP PRINTING";
                    }
                }
                if (Session["usertype"].ToString() != "ADMIN")
                {
                    string _strRights = string.Empty;
                    _strRights = CommonHelper.GetRights(lblHeader.Text.ToUpper(), (DataTable)Session["USER_RIGHTS"]);
                    CommonHelper._strRights = _strRights.Split('^');
                    if (CommonHelper._strRights[0] == "0")  //Check view rights
                    {
                        Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                    }
                }
                if (!IsPostBack)
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
                    lblLineCode.Text = Session["LINECODE"].ToString();
                    BindFGItemCode();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError,
                 System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        private void getprinterlist()
        {
            try
            {
                BL_Common blCommonobj = new BL_Common();
                DataTable dt = blCommonobj.BINDPRINTER("MOB");
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(ddlprinter, dt, true);
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }

        private void BindFGItemCode()
        {
            try
            {
                drpFGItemCode.Items.Clear();
                txtScanHere.Text = string.Empty;
                BL_MobCommon blobj = new BL_MobCommon();
                string sResult = string.Empty;
                DataTable dtFGItemCode = blobj.BindModel();
                if (dtFGItemCode.Rows.Count > 0)
                {
                    CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                    clsCommon.FillMultiColumnsCombo(drpFGItemCode, dtFGItemCode, true);
                    drpFGItemCode.SelectedIndex = 0;
                    drpFGItemCode.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        public void GetData()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (drpFGItemCode.SelectedIndex > 0)
                {
                    lblModelName.Text = string.Empty;
                    hidModelType.Value = "";
                    lbllastscanned.Text = string.Empty;
                    BL_MobCommon obj = new BL_MobCommon();
                    PL_Printing plobj = new PL_Printing();
                    plobj.sModelName = drpFGItemCode.SelectedValue.ToString();
                    DataTable dt = new DataTable();
                    dt = obj.DisplayedData(plobj);
                    if (dt.Rows.Count > 0)
                    {
                        lblModelName.Text = dt.Rows[0]["MODEL_CODE"].ToString();
                        lblModelType.Text = dt.Rows[0]["MODEL_DESC"].ToString();
                        hidModelType.Value = dt.Rows[0]["MODEL_DESC"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        protected void txtScanHere_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (drpFGItemCode.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select fg item code", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpFGItemCode.Focus();
                    return;
                }
                if (dvPrintergrup.Visible == true && ddlprinter.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select printer", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlprinter.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtScanHere.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please Scan Here", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtScanHere.Focus();
                    return;
                }
                BL_LabelPrinting blobj = new BL_LabelPrinting();
                DataTable dt = new DataTable();
                PL.PL_Printing plob = new PL.PL_Printing();
                plob.sBOMCode = drpFGItemCode.SelectedItem.Text;
                plob.sColorCode = "";
                plob.sCustomerCode = "";
                plob.sPrinterIP = ddlprinter.Text.Trim();
                plob.sModelName = lblModelName.Text;
                plob.sSNBarcode = txtScanHere.Text.Trim();
                plob.sSiteCode = Session["SiteCode"].ToString();
                plob.sLineCode = Session["LINECODE"].ToString();
                plob.sPrintedBy = Session["UserID"].ToString();
                plob.sUserID = Session["UserID"].ToString();
                string sStageCode = sHeaderValue.Split('~')[1].ToString();
                string sStageName = sHeaderValue.Split('~')[0].ToString();
                plob.sStageCode = sStageCode;
                plob.sStageName = sStageName;                
                string _Result = blobj.blLabelPrinting(plob);
                Message = _Result;
                if (Message.StartsWith("SUCCESS~"))
                {
                    CommonHelper.ShowMessage("Scanned barcode successfully passed this stage", msgsuccess, CommonHelper.MessageType.Success.ToString());
                    lbllastscanned.Text = txtScanHere.Text.Trim();
                    txtScanHere.Text = ""; txtScanHere.Focus();
                }
                else
                {
                    CommonHelper.ShowMessage(Message.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    lbllastscanned.Text = txtScanHere.Text.Trim();
                    txtScanHere.Text = ""; txtScanHere.Focus();
                }
                txtScanHere.Enabled = false;
                Thread.Sleep(2000);
                txtScanHere.Enabled = true;
            }
            catch (Exception ex)
            {
                txtScanHere.Text = ""; txtScanHere.Focus();
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                drpFGItemCode.Items.Clear();
                lblModelName.Text = string.Empty;
                lblModelName.Text = string.Empty;
                hidModelType.Value = "";
                lbllastscanned.Text = string.Empty;
                txtScanHere.Text = string.Empty;
                lblModelType.Text = string.Empty;
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
                GetData();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}