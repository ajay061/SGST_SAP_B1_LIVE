using BusinessLayer;
using Common;
using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGST.INE.RM
{
    public partial class Issuetoshopfloor : System.Web.UI.Page
    {
        BL_RM_Issue blobj = new BL_RM_Issue();
        decimal RemQty;
        string Message = "";
        static string sFIFORequired = ConfigurationManager.AppSettings["_RMFIFOREQUIRED"].ToString();
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
                if (Session["usertype"].ToString() != "ADMIN")
                {
                    string _strRights = CommonHelper.GetRights("ISSUE TO SHOP FLOOR", (DataTable)Session["USER_RIGHTS"]);
                    CommonHelper._strRights = _strRights.Split('^');
                    if (CommonHelper._strRights[0] == "0")  //Check view rights
                    {
                        Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                    }
                }
                if (!IsPostBack)
                {
                    BindIssueSlipNo();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        public void BindIssueSlipNo()
        {
            try
            {
                lblFGItemCode.Text = string.Empty;
                txtLocation.ReadOnly = false;
                txtLocation.Text = string.Empty;
                txtBarcode.Text = string.Empty;
                txtBarcode.ReadOnly = true;
                drpItemCode.Items.Clear();
                DataTable dt = new DataTable();
                dvIssue.DataSource = dt;
                dvIssue.DataBind();
                txtBarcode.ReadOnly = true;
                BL_RM_Issue blobj = new BL_RM_Issue();
                DataTable dtItem = blobj.BINDISSUESLIPNO(Session["SiteCode"].ToString());
                if (dtItem.Rows.Count > 0)
                {
                    CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                    clsCommon.FillComboBox(drpIssueSlip, dtItem, true);
                    drpIssueSlip.Focus();
                }
                else
                {
                    drpIssueSlip.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }

        public void BindIssuedLocationDetails()
        {
            try
            {
                blobj = new BL_RM_Issue();
                DataTable dtPcode = blobj.BindLocationDetails(
                    drpItemCode.Text, Session["SiteCode"].ToString()
                    );
                if (dtPcode.Rows.Count > 0)
                {
                    string sValue = clsCommon.ExportToTextFile(dtPcode);
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + sValue + "')", true);
                }
                else
                {
                    Response.Write("<script LANGUAGE='JavaScript' >alert('No data found for display')</script>");
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }

        protected void drpIssueSlip_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                txtLocation.Text = string.Empty; txtBarcode.ReadOnly = true;
                txtBarcode.Text = string.Empty;
                txtBarcode.ReadOnly = false;
                txtLocation.ReadOnly = false;
                lblFGItemCode.Text = string.Empty;
                DataTable dt = new DataTable();
                dvIssue.DataSource = dt;
                dvIssue.DataBind();
                if (drpIssueSlip.SelectedIndex > 0)
                {
                    BindData();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void BindData()
        {
            try
            {
                if (drpIssueSlip.Text == "--Select Issue No--")
                {
                    CommonHelper.ShowMessage("Please select work order no", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpIssueSlip.Focus();
                    return;
                }
                blobj = new BL_RM_Issue();
                DataTable dt = blobj.BINDDETAILS(drpIssueSlip.Text, drpItemCode.Text, Session["SiteCode"].ToString());
                if (dt.Rows.Count > 0)
                {
                    dvIssue.DataSource = dt;
                    dvIssue.DataBind();
                    RemQty = Convert.ToDecimal(dt.Rows[0]["REM_QTY"]);
                    hidrm.Value = Convert.ToString(dt.Rows[0]["RMI_ID"]);
                    lblFGItemCode.Text = Convert.ToString(dt.Rows[0]["FG_ITEM_CODE"]);
                }
                else
                {
                    dvIssue.DataSource = null;
                    dvIssue.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }

        private void CloseIssueSlipNo()
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (drpIssueSlip.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    CommonHelper.ShowMessage("Please select work order no.", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpIssueSlip.Focus();
                    return;
                }
                if (drpIssueSlip.Text == "--Select Issue No--")
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    CommonHelper.ShowMessage("Please select work order no", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpIssueSlip.Focus();
                    return;
                }
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "No")
                {
                    return;
                }
                blobj = new BL_RM_Issue();
                DataTable dt = blobj.CloseIssueSlipNo(drpIssueSlip.Text, Session["SiteCode"].ToString());
                if (dt.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    string sResult = dt.Rows[0][0].ToString();
                    if (sResult.StartsWith("SUCCESS~"))
                    {
                        CommonHelper.ShowMessage(dt.Rows[0][0].ToString().Split('~')[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                        BindIssueSlipNo();
                    }
                    else
                    {
                        CommonHelper.ShowMessage(dt.Rows[0][0].ToString().Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    CommonHelper.ShowMessage("No result found,Please try again", msgerror, CommonHelper.MessageType.Error.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }

        private void CheckIfLocationExists()
        {
            try
            {

                blobj = new BL_RM_Issue();
                string _Result = blobj.SCANLOCATION(txtLocation.Text, drpItemCode.Text, drpIssueSlip.Text, Session["SiteCode"].ToString());
                string[] msg = _Result.Split('~');
                string Msgs = msg[0];
                if (msg[0] == "SUCCESS")
                {
                    txtBarcode.Focus();
                    txtBarcode.Text = "";
                }
                else
                {
                    CommonHelper.ShowMessage("Location not found. Please select correct location.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtLocation.Text = "";
                    txtLocation.Focus();
                    txtLocation.Enabled = true;
                    txtLocation.ReadOnly = false;
                }

            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        private void ValidateReelBarcode(string sFIFOByPass)
        {
            try
            {
                if (drpIssueSlip.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select work order no.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBarcode.Text = string.Empty;
                    drpIssueSlip.Focus();
                    return;
                }
                blobj = new BL_RM_Issue();
                decimal dRQTY = 0;
                foreach (GridViewRow dr in dvIssue.Rows)
                {
                    dRQTY = Convert.ToDecimal(dvIssue.Rows[dr.RowIndex].Cells[4].Text);
                    break;
                }
                string _Result = blobj.sScanIssueBarcode(
                      drpIssueSlip.Text,
                       drpItemCode.Text,
                         txtLocation.Text,
                          Session["UserID"].ToString(),
                       txtBarcode.Text
                      , Session["SiteCode"].ToString(), sFIFORequired
                       , Session["LINECODE"].ToString()
                    );
                Message = _Result;
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.MethodBase.GetCurrentMethod().Name,
                   "USP_RM_ISSUE_SHOP_FLOR : Location :" + txtLocation.Text + ", Work Ordr No:" + drpIssueSlip.Text + ",Barcode : " + txtBarcode.Text +
                   ", Result :" + _Result);
                string[] msg = Message.Split('~');
                string Msgs = msg[0];
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtMessage, System.Reflection.MethodBase.GetCurrentMethod().Name, Message);
                if (Message.StartsWith("N~"))
                {
                    if (sFIFOByPass == "1" && msg[0].ToString().Contains("Barcode already scanned."))
                    {
                        CommonHelper.ShowMessage("Barcode scanned successfully,1", msgsuccess, CommonHelper.MessageType.Success.ToString());
                        txtBarcode.Focus();
                        txtBarcode.Text = "";
                        BindData();
                        return;
                    }
                    else
                    {
                        CommonHelper.ShowMessage(Message.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        txtBarcode.Focus();
                        txtBarcode.Text = "";
                        BindData();
                        return;
                    }
                }
                else if (msg[0].StartsWith("ERROR~"))
                {
                    CommonHelper.ShowMessage(msg[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBarcode.Focus();
                    txtBarcode.Text = "";
                    BindData();
                    return;
                }
                if (sFIFORequired == "1")
                {
                    if (msg[0].StartsWith("ALERT"))
                    {
                        CommonHelper.ShowMessage(msg[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        txtBarcode.Focus();
                        txtBarcode.Text = "";
                    }
                    else
                    {
                        if (msg[0].StartsWith("SUCCESS"))
                        {
                            CommonHelper.ShowMessage("Barcode scanned successfully", msgsuccess, CommonHelper.MessageType.Success.ToString());
                            txtBarcode.Focus();
                            txtBarcode.Text = "";
                            BindData();
                            return;
                        }
                    }
                    return;
                }
                if (msg[0].StartsWith("ALERT"))
                {
                    if (sFIFORequired == "0")
                    {
                        ValidateReelBarcode("1");
                    }
                    return;
                }
                if (msg[0].StartsWith("SUCCESS"))
                {
                    CommonHelper.ShowMessage("Barcode scanned successfully", msgsuccess, CommonHelper.MessageType.Success.ToString());
                    txtBarcode.Focus();
                    txtBarcode.Text = "";
                    BindData();
                    return;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        private bool ValidInput()
        {
            bool bTrue = true;
            if (drpIssueSlip.SelectedIndex <= 0)
            {
                CommonHelper.ShowMessage("Please select work order no.", msgerror, CommonHelper.MessageType.Error.ToString());
                txtBarcode.Text = string.Empty;
                drpIssueSlip.Focus();
                return bTrue = false;
            }
            if (string.IsNullOrEmpty(txtLocation.Text))
            {
                CommonHelper.ShowMessage("Please scan location.", msgerror, CommonHelper.MessageType.Error.ToString());
                txtLocation.Focus();
                txtBarcode.Text = string.Empty;
                return bTrue = false;
            }
            return bTrue;
        }

        protected void txtLocation_TextChanged(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            if (drpIssueSlip.SelectedIndex <= 0)
            {
                CommonHelper.ShowMessage("Please select issue slip no.", msgerror, CommonHelper.MessageType.Error.ToString());
                return;
            }
            if (txtLocation.Text.Trim().Length > 0)
            {
                txtBarcode.ReadOnly = false;
                txtLocation.ReadOnly = true;
                txtBarcode.Focus();
                return;
            }
            CheckIfLocationExists();
        }
        protected void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (ValidInput())
                {
                    if (string.IsNullOrEmpty(txtBarcode.Text.Trim()))
                    {
                        CommonHelper.ShowMessage("Please scan or enter reel barcode.", msgerror, CommonHelper.MessageType.Error.ToString());
                        txtBarcode.Focus();
                        return;
                    }
                    ValidateReelBarcode("0");
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                //Bind ISSUE SLIP NO
                BindIssueSlipNo();
                lblFGItemCode.Text = string.Empty;
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void sReset_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            txtLocation.Text = "";
            txtBarcode.Text = "";
            txtLocation.ReadOnly = false;
            txtBarcode.ReadOnly = true;
            txtLocation.Focus();
        }

        protected void btnViewLocation_Click(object sender, EventArgs e)
        {
            try
            {
                if (drpItemCode.Text != "")
                {
                    BindIssuedLocationDetails();
                }
                else
                {
                    CommonHelper.ShowMessage("Item code not found", msgerror, CommonHelper.MessageType.Error.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }


        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TableCell cell = e.Row.Cells[6];
                decimal REM_QTY = Convert.ToDecimal(cell.Text);
                TableCell cell1 = e.Row.Cells[5];
                decimal ISSUE_QTY = Convert.ToDecimal(cell1.Text);

                TableCell cell2 = e.Row.Cells[4];
                decimal QTY = Convert.ToDecimal(cell2.Text);
                if (REM_QTY <= 0)
                {
                    e.Row.BackColor = Color.Green;
                    e.Row.ForeColor = Color.White;
                    return;
                    //cell.BackColor = Color.Green;
                }
                if (ISSUE_QTY > 0)
                {
                    e.Row.BackColor = Color.Yellow;
                    e.Row.ForeColor = Color.Green;
                }
                //if (quantity > 50 && quantity <= 100)
                //{
                //    cell.BackColor = Color.Orange;
                //}
            }
        }

        protected void dvIssue_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dvIssue.PageIndex = e.NewPageIndex;
            BindData();
        }

        protected void dvIssue_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                try
                {
                    int rowIndex = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = dvIssue.Rows[rowIndex];
                    string sPartCode = row.Cells[1].Text;
                    blobj = new BL_RM_Issue();
                    DataTable dtPcode = blobj.BindLocationDetails(
                        sPartCode, Session["SiteCode"].ToString()
                        );
                    if (dtPcode.Rows.Count > 0)
                    {
                        string sValue = clsCommon.ExportToTextFile(dtPcode);
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + sValue + "')", true);
                    }
                    else
                    {
                        Response.Write("<script LANGUAGE='JavaScript' >alert('No location found for display')</script>");
                    }
                }
                catch (Exception ex)
                {
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                    Response.Write("<script LANGUAGE='JavaScript' >alert('" + ex.Message + "')</script>");
                }
            }
        }

        protected void btnCompleteSlip_Click(object sender, EventArgs e)
        {
            try
            {
                if (drpIssueSlip.SelectedIndex > 0)
                {
                    CloseIssueSlipNo();
                }
                else
                {
                    CommonHelper.ShowMessage("Please select work order no", msgerror, CommonHelper.MessageType.Error.ToString());
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