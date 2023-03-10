using BusinessLayer;
using Common;
using System;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;

namespace SGST.INE.RM
{
    public partial class Partialissuelabelprinting : System.Web.UI.Page
    {
        BL_ChildLabelPrinting blobj = new BL_ChildLabelPrinting();
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Signin/v1/Login.aspx?Session=Null");
            }
        }
        string sType = "PIR";
        string Message = "";
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
        public void BindPartCode()
        {
            try
            {
                drpItemCode.Items.Clear();
                drpReelID.Items.Clear();
                txtQty.Text = string.Empty;
                blobj = new BL_ChildLabelPrinting();
                DataTable dtPcode = blobj.BindINELPartNo(sType, Session["SiteCode"].ToString());
                if (dtPcode.Rows.Count > 0)
                {
                    CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                    clsCommon.FillComboBox(drpItemCode, dtPcode, true);
                    drpItemCode.Focus();
                }
                else
                {
                    CommonHelper.ShowMessage("Part Code not available", msgerror, CommonHelper.MessageType.Error.ToString());
                }

            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usertype"].ToString() != "ADMIN")
            {
                string _strRights = CommonHelper.GetRights("PARTIAL ISSUE LABEL PRINTING", (DataTable)Session["USER_RIGHTS"]);
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
                    BindPartCode();
                    drpReelID.Items.Insert(0, new ListItem("--Select--", "0", true));
                }
                catch (Exception ex)
                {
                    CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                }
            }
        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (drpItemCode.Items.Count == 0)
                {
                    CommonHelper.ShowMessage("No part code found to print", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpItemCode.Focus();
                    return;
                }
                if (drpItemCode.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select part code", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpItemCode.Focus();
                    return;
                }
                if (drpReelID.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please scan or select barcode.", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpReelID.Focus();
                    return;
                }            
                string PRINTING_TYPE = "PIR";
                string sLineCode = Session["LINECODE"].ToString();
                string sUserID = Session["UserID"].ToString();
                blobj = new BL_ChildLabelPrinting();
                DataTable dt = new DataTable();
                string _Result = blobj.Labelprint(
                        drpItemCode.Text,
                        drpReelID.Text,
                        Session["UserID"].ToString(),
                       Convert.ToDecimal(txtQty.Text),
                       drpPrinterName.Text,
                       PCommon.sPrinterPort,
                        PRINTING_TYPE, sUserID, sLineCode
                    );
                Message = _Result;
                string[] msg = Message.Split('~');
                string Msgs = msg[0];
                drpReelID.Items.Clear();
                txtQty.Text = "";
                drpItemCode.SelectedIndex = 0;
                if (Message.Length > 0)
                {
                    if (msg[0].StartsWith("N"))
                    {
                        CommonHelper.ShowMessage(msg[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    else
                    {
                        txtQty.Text = "";
                        drpItemCode.SelectedIndex = 0;
                        drpPrinterName.SelectedIndex = 0;
                        CommonHelper.ShowMessage(msg[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                    }
                }
                else
                {
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
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
            if (drpReelID.SelectedIndex > 0)
            {
                drpReelID.Items.Clear();
                txtQty.Text = "";                
            }
            drpItemCode.Focus();
            BindPartCode();
        }
        public void bindReelID()
        {
            try
            {
                blobj = new BL_ChildLabelPrinting();
                DataTable dtPcode = blobj.BindReelBarcode(drpItemCode.Text, sType, Session["SiteCode"].ToString());
                if (dtPcode.Rows.Count > 0)
                {
                    CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                    clsCommon.FillComboBox(drpReelID, dtPcode, true);
                    drpReelID.Focus();
                }
                else
                {
                    CommonHelper.ShowMessage("Part Barcode not available", msgerror, CommonHelper.MessageType.Error.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        protected void drpReelID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpReelID.Text == "--Select Reel ID--" || drpReelID.Text == "0")
                {
                    txtQty.Text = string.Empty;
                    drpReelID.Focus();
                    return;
                }

                blobj = new BL_ChildLabelPrinting();
                DataTable dt = new DataTable();
                dt = blobj.SCANBARCODE(drpItemCode.Text, sType, drpReelID.Text, Session["SiteCode"].ToString());
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString().StartsWith("N~") || dt.Rows[0][0].ToString().StartsWith("ERROR~"))
                    {
                        CommonHelper.ShowMessage(dt.Rows[0][0].ToString().Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                    else
                    {
                        Message = dt.Rows[0][0].ToString();
                        txtQty.Text = Convert.ToString(Message.Split('~')[1]);
                        hidqty.Value = Convert.ToString(txtQty.Text);
                    }
                }
                else
                {
                    CommonHelper.ShowMessage("No result found, Please try again", msgerror, CommonHelper.MessageType.Error.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void drpItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpItemCode.SelectedIndex <= 0)
                {
                    drpReelID.Items.Clear();
                    txtQty.Text = string.Empty;
                    return;
                }
                bindReelID();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}