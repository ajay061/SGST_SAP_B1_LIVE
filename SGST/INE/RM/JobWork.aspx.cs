using BusinessLayer;
using Common;
using System;
using System.Data;

namespace SGST.INE.Operation
{
    public partial class JobWork : System.Web.UI.Page
    {
        BL_JobWork blobj = new BL_JobWork();
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
                Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
                Response.AddHeader("Pragma", "no-cache");
                Response.AddHeader("Expires", "0");
                if (Session["usertype"].ToString() != "ADMIN")
                {
                    string _strRights = CommonHelper.GetRights("JOB WORK", (DataTable)Session["USER_RIGHTS"]);
                    CommonHelper._strRights = _strRights.Split('^');
                    if (CommonHelper._strRights[0] == "0")  //Check view rights
                    {
                        Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                    }
                }
                if (!IsPostBack)
                {
                    BindOrderNo();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
        }

        public void BindOrderNo()
        {
            //for hiding messages
            drpOrderNumber.Items.Clear();
            drpItemCode.Items.Clear();
            drpItemLineNo.Items.Clear();
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                blobj = new BL_JobWork();
                DataTable dtorderno = blobj.BindOrderNo(Session["SiteCode"].ToString()
                    );
                if (dtorderno.Rows.Count > 0)
                {
                    CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                    clsCommon.FillComboBox(drpOrderNumber, dtorderno, true);
                    drpOrderNumber.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
        }

        public void BindPartCode()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                blobj = new BL_JobWork();
                DataTable dtPartCode = blobj.BindPartCode(drpOrderNumber.SelectedValue.ToString(), Session["SiteCode"].ToString()
                );
                if (dtPartCode.Rows.Count > 0)
                {
                    CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                    clsCommon.FillComboBox(drpItemCode, dtPartCode, true);
                    drpItemCode.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
        }

        public void BindItemLineNo()
        {
            //for hiding messages
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                blobj = new BL_JobWork();
                DataTable dtorderno = blobj.BindItemLineNo(drpOrderNumber.SelectedValue.ToString(), drpItemCode.SelectedValue.ToString()
                  , Session["SiteCode"].ToString());
                if (dtorderno.Rows.Count > 0)
                {
                    CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                    clsCommon.FillComboBox(drpItemLineNo, dtorderno, true);
                    drpItemLineNo.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
        }

        //this method is for clear controls
        public void ClearControl()
        {
            try
            {
                txtLocation.Text = "";
                txtBarcode.Text = "";
                drpOrderNumber.SelectedIndex = 0;
                drpItemCode.Items.Clear();
                drpItemLineNo.Items.Clear();
                gvJobWork.DataSource = null;
                gvJobWork.DataBind();
                BindOrderNo();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void GetOrderDetails()
        {
            try
            {
                blobj = new BL_JobWork();
                DataTable dt = new DataTable();
                dt = blobj.GetDetails(
                drpOrderNumber.SelectedItem.Text.Trim(), drpItemCode.SelectedItem.Text.Trim()
                , drpItemLineNo.SelectedValue.ToString(), Session["SiteCode"].ToString()
                    );
                if (dt.Rows.Count > 0)
                {
                    gvJobWork.DataSource = dt;
                    gvJobWork.DataBind();
                }
                else
                {
                    gvJobWork.DataSource = null;
                    gvJobWork.DataBind();
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool checkField()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (drpOrderNumber.SelectedIndex == 0 || drpOrderNumber.SelectedIndex == -1)
                {
                    CommonHelper.ShowMessage("Order number is not selected", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpOrderNumber.Focus();
                    return true;
                }
                if (drpItemCode.SelectedIndex == 0 || drpItemCode.SelectedIndex == -1)
                {
                    CommonHelper.ShowMessage("Item code is not selected", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpItemCode.Focus();
                    return true;
                }
                if (drpItemLineNo.SelectedIndex == 0 || drpItemLineNo.SelectedIndex == -1)
                {
                    CommonHelper.ShowMessage("Item Line No is not selected", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpItemLineNo.Focus();
                    return true;
                }
                if (string.IsNullOrEmpty(txtLocation.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan location", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtLocation.Focus();
                    return true;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowMessage(ex.Message, msgerror, CommonHelper.MessageType.Error.ToString());
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return true;
            }
            return false;
        }
        protected void drpOrderNumber_SelectedIndexChanged1(object sender, EventArgs e)
        {
            drpItemCode.Items.Clear();
            txtBarcode.Text = "";
            txtLocation.Text = "";
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (drpOrderNumber.SelectedIndex > 0)
                {
                    BindPartCode();
                }
                else
                {
                    drpItemCode.Items.Clear();
                    drpItemCode.DataBind();
                    gvJobWork.DataSource = null;
                    gvJobWork.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowMessage(ex.Message, msgerror, CommonHelper.MessageType.Error.ToString());
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
        }
        protected void drpItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtLocation.Text = "";
            txtBarcode.Text = "";
            drpItemLineNo.Items.Clear();
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (drpOrderNumber.SelectedIndex > 0 && drpItemCode.SelectedIndex > 0)
                {
                    BindItemLineNo();
                }
                else
                {
                    gvJobWork.DataSource = null;
                    gvJobWork.DataBind();
                    drpItemLineNo.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowMessage(ex.Message, msgerror, CommonHelper.MessageType.Error.ToString());
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
        }

        protected void drpItemLineNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtLocation.Text = "";
            txtBarcode.Text = "";
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (drpOrderNumber.SelectedIndex > 0 && drpItemCode.SelectedIndex > 0 && drpItemLineNo.SelectedIndex > 0)
                {
                    GetOrderDetails();
                }
                else
                {
                    gvJobWork.DataSource = null;
                    gvJobWork.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
        }

        protected void txtLocation_TextChanged(object sender, EventArgs e)
        {
            if (drpOrderNumber.SelectedIndex == 0 || drpOrderNumber.SelectedIndex == -1)
            {
                CommonHelper.ShowMessage("Order number is not selected", msgerror, CommonHelper.MessageType.Error.ToString());
                drpOrderNumber.Focus();
            }
            else if (drpItemCode.SelectedIndex == 0 || drpItemCode.SelectedIndex == -1)
            {
                CommonHelper.ShowMessage("Item code is not selected", msgerror, CommonHelper.MessageType.Error.ToString());
                drpItemCode.Focus();
            }
            else if (drpItemLineNo.SelectedIndex == 0 || drpItemLineNo.SelectedIndex == -1)
            {
                CommonHelper.ShowMessage("Item Line No is not selected", msgerror, CommonHelper.MessageType.Error.ToString());
                drpItemLineNo.Focus();
            }
            else if (string.IsNullOrEmpty(txtLocation.Text.Trim()))
            {
                CommonHelper.ShowMessage("Please scan location", msgerror, CommonHelper.MessageType.Error.ToString());
                txtLocation.Focus();
            }
            else
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                txtBarcode.Focus();
            }
        }
        protected void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (checkField() == false)
                {
                    if (string.IsNullOrEmpty(txtBarcode.Text.Trim()))
                    {
                        CommonHelper.ShowMessage("Please scan barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                        txtBarcode.Focus();
                        return;
                    }
                    if (gvJobWork.Rows.Count >= 0)
                    {
                        blobj = new BL_JobWork();
                        string _Result = string.Empty;
                        DataTable dtResult = blobj.SaveJobBarcode(
                            drpOrderNumber.SelectedValue.ToString(), drpItemCode.SelectedValue.ToString(),
                            drpItemLineNo.SelectedValue.ToString(), txtLocation.Text.Trim(), txtBarcode.Text.Trim(),
                            Session["UserID"].ToString(), Session["SiteCode"].ToString()
                           , Session["LINECODE"].ToString()
                            );
                        if (dtResult.Rows.Count > 0)
                        {
                            _Result = dtResult.Rows[0][0].ToString();
                            string[] msg = _Result.Split('~');
                            string Msgs = msg[0];
                            if (_Result.Length > 0)
                            {
                                if (_Result.StartsWith("N~"))
                                {
                                    CommonHelper.ShowMessage(msg[1], msgerror, CommonHelper.MessageType.Error.ToString());
                                    txtBarcode.Text = "";
                                    txtBarcode.Focus();
                                    GetOrderDetails();
                                }
                                else
                                {
                                    CommonHelper.ShowMessage(msg[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                                    txtBarcode.Text = "";
                                    txtBarcode.Focus();
                                    GetOrderDetails();
                                }
                            }
                            else
                            {
                                CommonHelper.ShowMessage("No result found against scanned barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                            }
                        }
                        else
                        {
                            CommonHelper.ShowMessage("No result found against scanned barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (drpOrderNumber.SelectedIndex == -1)
                {

                }
                else
                    drpOrderNumber.SelectedIndex = 0;
                if (drpItemCode.Items.Count > 0)
                {
                    drpItemCode.SelectedIndex = 0;
                    drpItemLineNo.Items.Clear();
                }
                txtBarcode.Text = "";
                txtLocation.Text = "";
                gvJobWork.DataSource = null;
                gvJobWork.DataBind();
                drpOrderNumber.Focus();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
        }

        protected void gvJobWork_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            gvJobWork.PageIndex = e.NewPageIndex;
            GetOrderDetails();
        }
    }
}