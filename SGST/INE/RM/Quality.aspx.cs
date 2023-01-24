using BusinessLayer;
using Common;
using System;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;

namespace SGST.INE.RM
{
    public partial class Quality : System.Web.UI.Page
    {
        BL_Quality blobj = new BL_Quality();
        string Message = "";
        static DataTable dtAdd;
        private void AddColumns()
        {
            dtAdd = new DataTable();
            dtAdd.Columns.Add(new System.Data.DataColumn("ITEM_CODE", typeof(String)));
            dtAdd.Columns.Add(new System.Data.DataColumn("ITEM_DESC", typeof(String)));
            dtAdd.Columns.Add(new System.Data.DataColumn("SPECIFICATION", typeof(String)));
            dtAdd.Columns.Add(new System.Data.DataColumn("ZONE", typeof(String)));
            dtAdd.Columns.Add(new System.Data.DataColumn("METHOD", typeof(String)));
            dtAdd.Columns.Add(new System.Data.DataColumn("RESULT_1", typeof(String)));
            dtAdd.Columns.Add(new System.Data.DataColumn("RESULT_2", typeof(String)));
            dtAdd.Columns.Add(new System.Data.DataColumn("RESULT_3", typeof(String)));
            dtAdd.Columns.Add(new System.Data.DataColumn("RESULT_4", typeof(String)));
            dtAdd.Columns.Add(new System.Data.DataColumn("RESULT_5", typeof(String)));
            dtAdd.Columns.Add(new System.Data.DataColumn("REMARKS", typeof(String)));
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
            try
            {
                Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
                Response.AddHeader("Pragma", "no-cache");
                Response.AddHeader("Expires", "0");
                txtReelID.Focus();
                if (Session["usertype"].ToString() != "ADMIN")
                {
                    string _strRights = CommonHelper.GetRights("RM QUALITY", (DataTable)Session["USER_RIGHTS"]);
                    CommonHelper._strRights = _strRights.Split('^');
                    if (CommonHelper._strRights[0] == "0")  //Check view rights
                    {
                        Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                    }
                }
                if (!IsPostBack)
                {
                    rdNo.Checked = true;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void BindDefect(string sPartCode)
        {
            try
            {
                gvMultiDefect.DataSource = null;
                gvMultiDefect.DataBind();
                ViewState["dt"] = null;
                DataSet dsData = blobj.BindDefect(sPartCode, Session["SiteCode"].ToString());
                if (dsData.Tables.Count > 0)
                {
                    DataTable dtpartCode = dsData.Tables[0];
                    if (dtpartCode.Rows.Count > 0)
                    {
                        ViewState["dt"] = dtpartCode;
                        CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                        gvMultiDefect.DataSource = dtpartCode;
                        gvMultiDefect.DataBind();
                    }
                    DataTable dtMakeData = dsData.Tables[1];
                    if (dtMakeData.Rows.Count > 0)
                    {
                        gvmakeData.DataSource = dtMakeData;
                        gvmakeData.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void ValidateReelBarcode()
        {
            try
            {
                gvMultiDefect.DataSource = null;
                gvMultiDefect.DataBind();
                lblBatch.Text = string.Empty;
                hidGRNNo.Value = "";
                hidItemLineNo.Value = "";
                hidQty.Value = "";
                lblItemCode.Text = "";
                lblInvQuantity.Text = "";
                lblMake.Text = "";
                lblMPN.Text = "";
                hidMake.Value = "";
                hidMPN.Value = "";
                lblPartDesc.Text = "";
                hidPartDesc.Value = "";
                blobj = new BL_Quality();
                StringBuilder sb = new StringBuilder();
                string result = blobj.GetBarcodeCode(txtReelID.Text, chkIsMRN.Checked, Session["SiteCode"].ToString());
                Message = result;
                string[] msg = Message.Split('~');
                txtReelID.Text.Trim();
                txtReelID.Focus();
                if (msg[0].StartsWith("N"))
                {
                    CommonHelper.ShowMessage(msg[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    txtReelID.Text = "";
                    txtReelID.Focus();
                    gvMultiDefect.DataSource = null;
                    gvMultiDefect.DataBind();
                }
                else
                {
                    string sBatchNo = string.Empty;
                    string sPartCode = string.Empty;
                    string sGRNNO = string.Empty;
                    string sItemLineNo = string.Empty;
                    string Qty = string.Empty;
                    sBatchNo = msg[1];
                    lblBatch.Text = sBatchNo;
                    hidBatch.Value = lblBatch.Text;
                    sPartCode = msg[2];
                    hidPartCode1.Value = sPartCode;
                    lblItemCode.Text = sPartCode;
                    sGRNNO = msg[3];
                    sItemLineNo = msg[4];
                    Qty = msg[5];
                    lblMake.Text = msg[6];
                    lblMPN.Text = msg[7];
                    hidMake.Value = msg[6];
                    hidMPN.Value = msg[7];
                    hidQty.Value = Qty;
                    lblInvQuantity.Text = Qty;
                    hidGRNNo.Value = sGRNNO;
                    lblPartDesc.Text = msg[8];
                    hidPartDesc.Value = msg[8];
                    hidItemLineNo.Value = sItemLineNo;
                    if (chkIsMRN.Checked == true)
                    {
                        rdNo.Checked = true;
                    }
                    BindDefect(hidPartCode1.Value);
                }
            }
            catch (Exception ex)
            {
                txtReelID.Text = "";
                txtReelID.Focus();
                gvMultiDefect.DataSource = null;
                gvMultiDefect.DataBind();
                lblBatch.Text = string.Empty;
                hidGRNNo.Value = "";
                hidItemLineNo.Value = "";
                hidQty.Value = "";
                lblItemCode.Text = "";
                lblInvQuantity.Text = "";
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }

        private void QualityChange(string sQualityStatus)
        {
            if (txtReelID.Text.Trim() == "")
            {
                CommonHelper.ShowMessage("Please scan barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                txtReelID.Focus();
                return;
            }
            try
            {
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "No")
                {
                    return;
                }
                DataTable dt = (DataTable)ViewState["dt"];
                if (dt == null)
                {
                    CommonHelper.ShowMessage("Quality parameters not found for item code of scanned barcode, Please contact admin to enter parameters", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtReelID.Focus();
                    txtReelID.Text = string.Empty;
                    gvMultiDefect.DataSource = null;
                    gvMultiDefect.DataBind();
                    lblBatch.Text = string.Empty;
                    hidGRNNo.Value = "";
                    hidItemLineNo.Value = "";
                    hidQty.Value = "";
                    lblItemCode.Text = "";
                    lblInvQuantity.Text = "";
                    lblMake.Text = "";
                    lblMPN.Text = "";
                    hidMake.Value = "";
                    hidMPN.Value = "";
                    lblMake.Text = string.Empty;
                    lblMPN.Text = string.Empty;
                    gvmakeData.DataSource = null;
                    gvmakeData.DataBind();
                    lblPartDesc.Text = "";
                    return;
                }
                DataRow dr;
                AddColumns();

                foreach (DataRow dr1 in dt.Rows)
                {
                    dr = dtAdd.NewRow();
                    dr[0] = lblItemCode.Text;
                    dr[1] = dr1.ItemArray[3].ToString();
                    dr[2] = dr1.ItemArray[5].ToString();
                    dr[3] = dr1.ItemArray[4].ToString();
                    dr[4] = dr1.ItemArray[6].ToString();
                    dr[5] = dr1.ItemArray[7].ToString();
                    dr[6] = dr1.ItemArray[8].ToString();
                    dr[7] = dr1.ItemArray[9].ToString();
                    dr[8] = dr1.ItemArray[10].ToString();
                    dr[9] = dr1.ItemArray[11].ToString();
                    dr[10] = txtRemarks.Text;
                    dtAdd.Rows.Add(dr);
                }
                if (dtAdd.Rows.Count == 0)
                {
                    CommonHelper.ShowMessage("Please enter parameters, Please contact admin.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtReelID.Focus();
                    txtReelID.Text = string.Empty;
                    lblInvQuantity.Text = "0";
                    lblBatch.Text = "-";
                    hidPartCode1.Value = "";
                    hidItemLineNo.Value = "";
                    hidGRNNo.Value = "";
                    lblItemCode.Text = "";
                    hidMake.Value = "";
                    hidMPN.Value = "";
                    gvmakeData.DataSource = null;
                    gvmakeData.DataBind();
                    lblMake.Text = string.Empty;
                    lblMPN.Text = string.Empty;
                    lblPartDesc.Text = "";
                }
                blobj = new BL_Quality();
                string _Result = string.Empty;
                if (chkIsMRN.Checked == true)
                {
                    _Result = blobj.QualityMRNBarcode(sQualityStatus, Convert.ToDecimal(hidQty.Value), txtReelID.Text
                      , hidGRNNo.Value, hidItemLineNo.Value, hidPartCode1.Value, Session["UserID"].ToString()
                      , dtAdd, txtRemarks.Text.Trim()
                      , Session["SiteCode"].ToString(), Session["LINECODE"].ToString()
                      ,drpResult.Text
                      );
                }
                else
                {
                    _Result = blobj.QualityUpdate(sQualityStatus, Convert.ToDecimal(hidQty.Value), txtReelID.Text
                       , hidGRNNo.Value, hidItemLineNo.Value, hidPartCode1.Value
                       , dtAdd, txtRemarks.Text.Trim(), Session["SiteCode"].ToString()
                       , Session["UserID"].ToString()
                       , Session["LINECODE"].ToString(), drpResult.Text
                       );
                }
                Message = _Result;
                string[] msg = Message.Split('~');
                if (_Result.StartsWith("SUCCESS~"))
                {
                    CommonHelper.ShowMessage(msg[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                }
                else
                {
                    CommonHelper.ShowMessage(msg[1], msgerror, CommonHelper.MessageType.Error.ToString());
                }
                txtReelID.ReadOnly = false;
                txtReelID.Text = "";
                txtReelID.Focus();
                lblInvQuantity.Text = "0";
                lblBatch.Text = "-";
                hidPartCode1.Value = "";
                hidItemLineNo.Value = "";
                hidGRNNo.Value = "";
                hidQty.Value = "0";
                lblItemCode.Text = "";
                gvMultiDefect.DataSource = null;
                gvMultiDefect.DataBind();
                txtRemarks.Text = string.Empty;
                hidMake.Value = "";
                hidMPN.Value = "";
                gvmakeData.DataSource = null;
                gvmakeData.DataBind();
                lblMake.Text = string.Empty;
                lblMPN.Text = string.Empty;
                lblPartDesc.Text = "";
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError,  System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                txtReelID.Focus();
                rdyes.Checked = false;
                rdNo.Checked = true;
            }
        }

        private void QualityChangeFullBatch(string sQualityStatus)
        {
            if (txtReelID.Text.Trim() == "")
            {
                CommonHelper.ShowMessage("Please scan barcode.", msgerror, CommonHelper.MessageType.Error.ToString());
                txtReelID.Focus();
                return;
            }
            try
            {
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "No")
                {
                    return;
                }
                DataTable dt = (DataTable)ViewState["dt"];
                if (dt == null)
                {
                    CommonHelper.ShowMessage("Quality parameters not found for item code of scanned barcode, Please contact admin to enter parameters", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtReelID.Focus();
                    txtReelID.Text = string.Empty;
                    gvMultiDefect.DataSource = null;
                    gvMultiDefect.DataBind();
                    lblBatch.Text = string.Empty;
                    hidGRNNo.Value = "";
                    hidItemLineNo.Value = "";
                    hidQty.Value = "";
                    lblItemCode.Text = "";
                    lblInvQuantity.Text = "";
                    hidMake.Value = "";
                    hidMPN.Value = "";
                    gvmakeData.DataSource = null;
                    gvmakeData.DataBind();
                    lblMake.Text = string.Empty;
                    lblMPN.Text = string.Empty;
                    lblPartDesc.Text = "";
                    return;
                }
                DataRow dr;
                AddColumns();
                foreach (DataRow dr1 in dt.Rows)
                {
                    dr = dtAdd.NewRow();
                    dr[0] = lblItemCode.Text;
                    dr[1] = dr1.ItemArray[3].ToString();
                    dr[2] = dr1.ItemArray[5].ToString();
                    dr[3] = dr1.ItemArray[4].ToString();
                    dr[4] = dr1.ItemArray[6].ToString();
                    dr[5] = dr1.ItemArray[7].ToString();
                    dr[6] = dr1.ItemArray[8].ToString();
                    dr[7] = dr1.ItemArray[9].ToString();
                    dr[8] = dr1.ItemArray[10].ToString();
                    dr[9] = dr1.ItemArray[11].ToString();
                    dr[10] = txtRemarks.Text;
                    dtAdd.Rows.Add(dr);
                }
                if (dtAdd.Rows.Count == 0)
                {
                    CommonHelper.ShowMessage("Defect not found, Please contact admin.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtReelID.Focus();
                    txtReelID.Text = string.Empty;
                    lblInvQuantity.Text = "0";
                    lblBatch.Text = "-";
                    hidPartCode1.Value = "";
                    hidItemLineNo.Value = "";
                    hidGRNNo.Value = "";
                    lblItemCode.Text = "";
                    txtRemarks.Text = string.Empty;
                    hidMake.Value = "";
                    hidMPN.Value = "";
                    gvmakeData.DataSource = null;
                    gvmakeData.DataBind();
                    lblMake.Text = string.Empty;
                    lblMPN.Text = string.Empty;
                    lblPartDesc.Text = "";
                }
                blobj = new BL_Quality();
                string _Result = blobj.QualityUpdateFullBatch(sQualityStatus,
                    Convert.ToDecimal(lblInvQuantity.Text),
                    txtReelID.Text, hidBatch.Value, hidPartCode1.Value,
                    hidGRNNo.Value, hidItemLineNo.Value
                    , dtAdd, txtRemarks.Text.Trim(), Session["SiteCode"].ToString(), Session["UserID"].ToString()
                    , Session["LINECODE"].ToString(), drpResult.Text
                    );
                Message = _Result;
                if (Message.Length > 0)
                {
                    string[] msg = Message.Split('~');
                    if (Message.StartsWith("N~"))
                    {
                        CommonHelper.ShowMessage(msg[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    else
                    {
                        CommonHelper.ShowMessage(msg[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                    }
                    txtReelID.ReadOnly = false;
                    txtReelID.Text = "";
                    txtReelID.Focus();
                    lblInvQuantity.Text = "0";
                    lblBatch.Text = "-";
                    hidPartCode1.Value = "";
                    hidItemLineNo.Value = "";
                    hidGRNNo.Value = "";
                    lblItemCode.Text = "";
                    gvMultiDefect.DataSource = null;
                    gvMultiDefect.DataBind();
                    ViewState["dt"] = null;
                    hidMake.Value = "";
                    hidMPN.Value = "";
                    gvmakeData.DataSource = null;
                    gvmakeData.DataBind();
                    lblMake.Text = string.Empty;
                    lblMPN.Text = string.Empty;
                    lblPartDesc.Text = "";
                }
                else
                {
                    CommonHelper.ShowMessage("No output found, Please try again", msgerror, CommonHelper.MessageType.Error.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                txtReelID.Focus();
                rdyes.Checked = false;
                rdNo.Checked = true;
            }
        }

        protected void txtReelID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (txtReelID.Text.Trim() == "")
                {
                    CommonHelper.ShowMessage("Please scan barcode.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtReelID.Focus();
                    return;
                }
                ValidateReelBarcode();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (txtReelID.Text.Trim() == "")
                {
                    CommonHelper.ShowMessage("Please scan barcode.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtReelID.Focus();
                    return;
                }
                if (rdyes.Checked)
                {
                    QualityChangeFullBatch("1");
                }
                else if (rdNo.Checked)
                {
                    QualityChange("1");
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (txtReelID.Text.Trim() == "")
                {
                    CommonHelper.ShowMessage("Please scan barcode.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtReelID.Focus();
                    return;
                }
                if (rdyes.Checked)
                {
                    QualityChangeFullBatch("2");
                }
                else if (rdNo.Checked)
                {
                    QualityChange("2");
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
                txtReelID.Text = string.Empty;
                txtReelID.Enabled = true;
                rdyes.Checked = false;
                rdNo.Checked = true;
                txtReelID.ReadOnly = false;
                txtReelID.Focus();
                lblInvQuantity.Text = "0";
                lblBatch.Text = "-";
                hidPartCode1.Value = "";
                hidItemLineNo.Value = "";
                hidGRNNo.Value = "";
                lblItemCode.Text = "";
                ViewState["dt"] = null;
                gvMultiDefect.DataSource = null;
                gvMultiDefect.DataBind();
                txtRemarks.Text = string.Empty;
                lblMake.Text = "";
                lblMPN.Text = "";
                lblPartDesc.Text = "";
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void chkIsMRN_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkIsMRN.Checked == true)
                {
                    rdNo.Checked = true;
                    pn1.Visible = false;
                }
                else
                {
                    pn1.Visible = true;
                }
                txtReelID.Text = string.Empty;
                txtReelID.Focus();
                lblBatch.Text = "";
                lblInvQuantity.Text = "";
                hidMake.Value = "";
                hidMPN.Value = "";
                gvmakeData.DataSource = null;
                gvmakeData.DataBind();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        protected void btnHold_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (txtReelID.Text.Trim() == "")
                {
                    CommonHelper.ShowMessage("Please scan barcode.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtReelID.Focus();
                    return;
                }
                if (rdyes.Checked)
                {
                    QualityChangeFullBatch("3");
                }
                else if (rdNo.Checked)
                {
                    QualityChange("3");
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void btnScraped_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (txtReelID.Text.Trim() == "")
                {
                    CommonHelper.ShowMessage("Please scan barcode.", msgerror, CommonHelper.MessageType.Error.ToString());
                    return;
                }
                if (rdyes.Checked)
                {
                    QualityChangeFullBatch("3");
                }
                else if (rdNo.Checked)
                {
                    QualityChange("3");
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        public void Databind()
        {
            DataTable dt = (DataTable)ViewState["dt"];
            gvMultiDefect.DataSource = dt;
            gvMultiDefect.DataBind();
        }
        protected void gvMultiDefect_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                gvMultiDefect.EditIndex = -1;
                Databind();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void gvMultiDefect_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                gvMultiDefect.EditIndex = e.NewEditIndex;
                Databind();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void gvMultiDefect_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)gvMultiDefect.Rows[e.RowIndex];
                string Zone = ((Label)gvMultiDefect.Rows[e.RowIndex].FindControl("ZONE")).Text;
                string T1 = ((TextBox)gvMultiDefect.Rows[e.RowIndex].FindControl("txtt1")).Text;
                string T2 = ((TextBox)gvMultiDefect.Rows[e.RowIndex].FindControl("txtt2")).Text;
                string T3 = ((TextBox)gvMultiDefect.Rows[e.RowIndex].FindControl("txtT3")).Text;
                string T4 = ((TextBox)gvMultiDefect.Rows[e.RowIndex].FindControl("txtt4")).Text;
                string T5 = ((TextBox)gvMultiDefect.Rows[e.RowIndex].FindControl("txtT5")).Text;
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["dt"];
                DataRow[] datarow = dt.Select("ZONE='" + Zone + "'");
                dt.Rows[e.RowIndex].BeginEdit();
                dt.Rows[e.RowIndex]["T1"] = T1;
                dt.Rows[e.RowIndex]["T2"] = T2;
                dt.Rows[e.RowIndex]["T3"] = T3;
                dt.Rows[e.RowIndex]["T4"] = T4;
                dt.Rows[e.RowIndex]["T5"] = T5;
                dt.Rows[e.RowIndex].EndEdit();
                dt.AcceptChanges();
                gvMultiDefect.EditIndex = -1;
                gvMultiDefect.DataSource = dt;
                gvMultiDefect.DataBind();
                ViewState["dt"] = dt;
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}