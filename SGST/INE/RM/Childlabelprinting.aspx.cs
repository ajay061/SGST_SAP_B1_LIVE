using BusinessLayer;
using Common;
using System;
using System.Data;

namespace SGST.INE.RM
{
    public partial class Childlabelprinting : System.Web.UI.Page
    {
        BL_ChildLabelPrinting blobj = new BL_ChildLabelPrinting();
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Signin/v1/Login.aspx?Session=Null");
            }
        }
        string sType = "CL";
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
                drpReelID.Items.Clear();
                hidqty.Value = "";
                txtQuantity.Text = string.Empty;
                txtQty.Text = string.Empty;
                drpItemCode.Items.Clear();
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
                    CommonHelper.ShowMessage("No part code is available for prinitng.", msginfo, CommonHelper.MessageType.Info.ToString());
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
                string _strRights = CommonHelper.GetRights("KITTING", (DataTable)Session["USER_RIGHTS"]);
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
                txtQty.Text = hidqty.Value;
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (drpItemCode.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select part code", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpItemCode.Focus();
                    return;
                }
                if (drpReelID.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select part barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpItemCode.Focus();
                    return;
                }
                if (txtQuantity.Text.Trim() == "")
                {
                    CommonHelper.ShowMessage("Please enter quantity", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtQuantity.Focus();
                    return;
                }
                if (Convert.ToDecimal(txtQuantity.Text) == 0)
                {
                    CommonHelper.ShowMessage("Please enter valid quantity", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtQuantity.Text = string.Empty;
                    txtQuantity.Focus();
                    return;
                }
                if (Convert.ToDecimal(txtQuantity.Text) > Convert.ToDecimal(txtQty.Text))
                {
                    CommonHelper.ShowMessage("Enter quantity can not be greater than displayed quantity", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtQuantity.Text = string.Empty;
                    txtQuantity.Focus();
                    return;
                }
                decimal dLotQty = Convert.ToDecimal(txtQty.Text);
                decimal dQty = Convert.ToDecimal(txtQuantity.Text);
                if (dLotQty- dQty == 0)
                {
                    CommonHelper.ShowMessage("Lot qty can not be fully split or kitting ", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtQuantity.Text = string.Empty;
                    txtQuantity.Focus();
                    return;
                }
                if (drpPrinterName.SelectedIndex == 0 && dvPrintergrup.Visible == true)
                {
                    CommonHelper.ShowMessage("Please select Printer Name", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpPrinterName.Focus();
                    return;
                }
                string sLineCode = Session["LINECODE"].ToString();
                string sUserID = Session["UserID"].ToString();
                blobj = new BL_ChildLabelPrinting();
                DataTable dt = new DataTable();
                string _Result = blobj.ChildLabelPrint(
                         drpItemCode.Text,
                         drpReelID.Text,
                         Session["UserID"].ToString(),
                         Convert.ToDecimal(txtQuantity.Text),
                         drpPrinterName.Text,
                         PCommon.sPrinterPort, sUserID, sLineCode, Session["SiteCode"].ToString()
                    );
                Message = _Result;
                string[] msg = Message.Split('~');
                string Msgs = msg[0];

                if (Message.Length > 0)
                {
                    if (msg[0].StartsWith("N") || msg[0].StartsWith("ERROR"))
                    {
                        CommonHelper.ShowMessage(msg[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    else
                    {
                        CommonHelper.ShowMessage(msg[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                        drpReelID.Items.Clear();
                        txtQty.Text = "";
                        drpItemCode.SelectedIndex = 0;
                        drpItemCode.Focus();
                        txtQuantity.Text = "";
                    }
                }
                else
                {
                    CommonHelper.ShowMessage(msg[1], msgerror, CommonHelper.MessageType.Error.ToString());
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
            if (drpItemCode.SelectedIndex > 0)
            {
                drpReelID.Items.Clear();
                txtQty.Text = "";
                drpItemCode.SelectedIndex = 0;
                drpItemCode.Focus();
                txtQuantity.Text = "";
            }
            BindPartCode();
        }

        public void bindReelID()
        {
            try
            {
                txtQuantity.Text = string.Empty;
                txtQty.Text = string.Empty;
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
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
            }
        }

        protected void drpReelID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtQuantity.Text = string.Empty;
                txtQty.Text = string.Empty;
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (drpReelID.Text == "--Select Reel ID--" || drpReelID.Text == "0")
                {
                    txtQty.Text = string.Empty;
                    txtQuantity.Text = string.Empty;
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
                    txtQuantity.Text = string.Empty;
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