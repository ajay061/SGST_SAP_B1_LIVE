using BusinessLayer;
using Common;
using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace SGST.INE.RM
{
    public partial class RMReturnNew : System.Web.UI.Page
    {
        BL_RM_Return blobj = new BL_RM_Return();
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
                    string _strRights = CommonHelper.GetRights("RM RETURN", (DataTable)Session["USER_RIGHTS"]);
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
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                ValidateBarcodeFromWIPIssue();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void ValidateBarcodeFromWIPIssue()
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (string.IsNullOrEmpty(txtBarcode.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan barcode.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBarcode.Focus();
                    return;
                }
                blobj = new BL_RM_Return();
                DataTable dt = new DataTable();
                string _Result = blobj.ValidateBarcodeFromWIPIssue(txtBarcode.Text,hidWorkOrderno.Value, drpType.Text
                    , Session["SiteCode"].ToString());
                Message = _Result;
                string[] msg = Message.Split('~');
                string Msgs = msg[0];
                if (msg.Length > 0)
                {
                    if (msg[0].StartsWith("N") || msg[0].StartsWith("ERROR"))
                    {
                        CommonHelper.ShowMessage(msg[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        txtBarcode.Text = "";
                        txtBarcode.Focus();
                        txtQuantity.Text = string.Empty;
                        hidInvQty.Value = "";
                    }
                    else
                    {
                        string[] messsage = msg[1].Split('^');
                        txtQuantity.Text = messsage[1];
                        hidInvQty.Value = messsage[1];
                        hidPartCode.Value = messsage[2].ToString();
                        txtWorkOrderNo.Text = messsage[3].ToString();
                        hidWorkOrderno.Value = messsage[3].ToString();
                        txtQnt.Focus();
                        drpType.Enabled = false;
                        drpType.CssClass = "form-control";
                        if (drpType.Text == "SFG")
                        {                            
                            txtQnt.Text = txtQuantity.Text;                            
                            txtQnt.ReadOnly = true;
                            txtQnt.CssClass = "form-control";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                gvReturnBarcode.Visible = true;
                if (txtBarcode.Text.Trim() == "")
                {
                    CommonHelper.ShowMessage("Please scan barcode.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBarcode.Focus();
                    return;
                }
                if (txtQuantity.Text == "")
                {
                    CommonHelper.ShowMessage("Available quantity is not found against scanned barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtQuantity.Focus();
                    return;
                }
                if (txtQnt.Text.Trim() == "")
                {
                    CommonHelper.ShowMessage("Please enter quantity", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtQnt.Focus();
                    return;
                }               
                if (Convert.ToDecimal(txtQnt.Text) == 0)
                {
                    CommonHelper.ShowMessage("Enter quantity can not be zero, Please enter valid quantity", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtQnt.Focus();
                    return;
                }
                if (Convert.ToDecimal(txtQuantity.Text) < Convert.ToDecimal(txtQnt.Text))
                {
                    CommonHelper.ShowMessage("Return quantity always be lesser than available quantity", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtQnt.Text = string.Empty;
                    txtQnt.Focus();
                    return;
                }
                createnewrow();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        DataTable mytable = new DataTable();
        string barcode = "";
        public void createnewrow()
        {
            try
            {
                if (ViewState["Row"] != null)
                {
                    mytable = (DataTable)ViewState["Row"];
                    DataRow dr = null;
                    if (mytable.Rows.Count > 0)
                    {
                        barcode = txtBarcode.Text;
                        bool exists = mytable.Select().ToList().Exists(row => row["itemcode"].ToString().ToUpper() == barcode);
                        if (exists)
                        {
                            CommonHelper.ShowMessage("Barcode already scanned", msgerror, CommonHelper.MessageType.Error.ToString());
                            txtBarcode.Text = "";
                            txtQuantity.Text = "";
                            txtQnt.Text = "";
                            txtBarcode.Focus();
                        }
                        else
                        {
                            dr = mytable.NewRow();
                            dr["itemcode"] = txtBarcode.Text;
                            dr["availableqty"] = hidInvQty.Value;
                            dr["qty"] = txtQnt.Text;
                            dr["PartCode"] = hidPartCode.Value;
                            dr["WORKORDERNO"] = txtWorkOrderNo.Text;
                            dr["WORKORDERNO"] = hidWorkOrderno.Value;
                            txtWorkOrderNo.Text = "";
                            mytable.Rows.Add(dr);
                            ViewState["Row"] = mytable;
                            gvReturnBarcode.DataSource = ViewState["Row"];
                            gvReturnBarcode.DataBind();
                            ViewState["Row"] = mytable;
                            txtBarcode.Text = "";
                            txtQuantity.Text = "";
                            txtQnt.Text = "";
                            txtBarcode.Focus();
                        }
                    }
                }
                else
                {
                    bool exists = mytable.Select().ToList().Exists(row => row["itemcode"].ToString().ToUpper() == barcode);
                    if (exists)
                    {
                        CommonHelper.ShowMessage("Item already added.", msginfo, CommonHelper.MessageType.Info.ToString());
                    }
                    else
                    {
                        barcode = txtBarcode.Text;
                        mytable.Columns.Add("itemcode", typeof(string));
                        mytable.Columns.Add("availableqty", typeof(string));
                        mytable.Columns.Add("qty", typeof(string));
                        mytable.Columns.Add("PartCode", typeof(string));
                        mytable.Columns.Add("WORKORDERNO", typeof(string));                       
                        DataRow dr1 = mytable.NewRow();
                        dr1 = mytable.NewRow();
                        dr1["itemcode"] = txtBarcode.Text;
                        dr1["availableqty"] = hidInvQty.Value;
                        dr1["qty"] = txtQnt.Text;
                        dr1["PartCode"] = hidPartCode.Value;
                        dr1["WORKORDERNO"] = txtWorkOrderNo.Text;
                        dr1["WORKORDERNO"] = hidWorkOrderno.Value;
                        txtWorkOrderNo.Text = "";
                        mytable.Rows.Add(dr1);
                        ViewState["Row"] = mytable;
                        gvReturnBarcode.DataSource = ViewState["Row"];
                        gvReturnBarcode.DataBind();
                        txtBarcode.Text = "";
                        txtQuantity.Text = "";
                        txtQnt.Text = "";
                        txtBarcode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (gvReturnBarcode.Rows.Count == 0)
                {
                    CommonHelper.ShowMessage("Please scan atleast one barcode for return", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBarcode.Focus();
                    return;
                }
                PrintBarcodes();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void PrintBarcodes()
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                blobj = new BL_RM_Return();
                DataTable dt = new DataTable();
                string _ReturnSlip = "MRN_" + DateTime.Now.ToString("ddMMyyyyHHmmss");
                string _qty = "";
                string _Part_Barcode = "";
                string _Result = string.Empty;
                string sLineCode = Session["LINECODE"].ToString();
                string sUserID = Session["UserID"].ToString();
                foreach (GridViewRow row in gvReturnBarcode.Rows)
                {
                    _Part_Barcode = row.Cells[0].Text;
                    _qty = row.Cells[2].Text;
                    _Result = blobj.PrintBarcode(
                   _ReturnSlip,
                    _Part_Barcode,
                  Session["userid"].ToString(),
                  Convert.ToDecimal(_qty),
                   drpPrinterName.Text,
                    Session["sitecode"].ToString(), row.Cells[3].Text
                    , row.Cells[4].Text,drpType.Text, sUserID, sLineCode
                    ,txtRemarks.Text.Trim()
               );
                }
                Message = _Result;
                string[] msg = Message.Split('~');
                string Msgs = msg[0];
                if (Message.Length > 0)
                {
                    if (msg[0].StartsWith("N~") || msg[0].StartsWith("ERROR~"))
                    {
                        CommonHelper.ShowMessage(msg[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        txtBarcode.Text = "";
                        txtQuantity.Text = "";
                        txtWorkOrderNo.Text = "";
                        txtRemarks.Text = "";
                        gvReturnBarcode.DataSource = null;
                        gvReturnBarcode.DataBind();
                        mytable.Rows.Clear();
                        ViewState["Row"] = null;
                        drpType.Enabled = true;
                        txtQnt.ReadOnly = false;
                    }
                    else
                    {
                        CommonHelper.ShowMessage(msg[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                        txtBarcode.Text = "";
                        txtQuantity.Text = "";
                        txtWorkOrderNo.Text = "";
                        txtRemarks.Text = "";
                        gvReturnBarcode.DataSource = null;
                        gvReturnBarcode.DataBind();
                        mytable.Rows.Clear();
                        ViewState["Row"] = null;
                        drpType.Enabled = true;
                        txtQnt.ReadOnly = false;
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

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                txtBarcode.Text = string.Empty;
                txtQuantity.Text = "";
                txtQnt.Text = "";
                mytable.Rows.Clear();
                gvReturnBarcode.DataSource = null;
                gvReturnBarcode.DataBind();
                txtBarcode.Focus(); txtWorkOrderNo.Text = "";
                hidWorkOrderno.Value = "";
                ViewState["Row"] = null;
                drpType.Enabled = true;
                txtQnt.ReadOnly = false;
                txtRemarks.Text = string.Empty;
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}