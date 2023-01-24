using Common;
using PL;
using System;
using System.Data;
using System.Web.UI.WebControls;
using BusinessLayer.Masters;

namespace SGST.INE.Masters
{
    public partial class ShippingMaster : System.Web.UI.Page
    {
        BL_InvoiceMaster blobj = new BL_InvoiceMaster();
        string Message = string.Empty;
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
                    string _strRights = CommonHelper.GetRights("ADDRESS MASTER", (DataTable)Session["USER_RIGHTS"]);
                    CommonHelper._strRights = _strRights.Split('^');
                    if (CommonHelper._strRights[0] == "0")  //Check view rights
                    {
                        Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                    }
                }
                if (!IsPostBack)
                {
                    BindFGItemCode();
                    BindGrid();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        private void BindFGItemCode()
        {
            try
            {
                BL_InvoiceMaster blobj = new BL_InvoiceMaster();
                DataTable dt = blobj.BindFGitemCode();
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillMultiColumnsCombo(ddlFGItemCode, dt, true);
                    ddlFGItemCode.Focus();
                }
                else
                {
                    CommonHelper.ShowMessage("No data found.", msginfo, CommonHelper.MessageType.Info.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void BindGrid()
        {
            try
            {
                BL_InvoiceMaster blobj = new BL_InvoiceMaster();
                PL_invoiceMaster plobj = new PL_invoiceMaster();
                DataTable dt = blobj.BindAddressgRID();
                if (dt.Rows.Count > 0)
                {
                    gvShippingAddress.DataSource = dt;
                    gvShippingAddress.DataBind();
                    lblNumberofRecords.Text = dt.Rows.Count.ToString();
                }
                else
                {
                    gvShippingAddress.DataSource = null;
                    gvShippingAddress.DataBind();
                    lblNumberofRecords.Text = "0";
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }

        }
        protected void SaveData()
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (ddlFGItemCode.SelectedIndex == 0 && btnSave.Text == "Save")
                {
                    CommonHelper.ShowMessage("Please Select FG Item Code", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlFGItemCode.Focus();
                    return;
                }
                if (txtAddress1.Text.Trim() == "")
                {
                    CommonHelper.ShowMessage("Enter Address 1.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtAddress1.Focus();
                    return;
                }
                if (txtAdd2.Text.Trim() == "")
                {
                    CommonHelper.ShowMessage("Enter Address 2.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtAdd2.Focus();
                    return;
                }
                BL_InvoiceMaster blobj = new BL_InvoiceMaster();
                DataTable dt = new DataTable();
                PL.PL_invoiceMaster plobj = new PL.PL_invoiceMaster();
                plobj.sModelCode = txtModel.Text;
                plobj.Address1 = txtAddress1.Text;
                plobj.Address2 = txtAdd2.Text;
                plobj.Address3 = txtAddress3.Text;
                plobj.Address4 = txtAdd4.Text;
                plobj.Address5 = txtAdd5.Text;
                plobj.Address6 = txtAdd6.Text;
                plobj.Address7 = txtAdd7.Text;
                if (btnSave.Text == "Update")
                {
                    plobj.MSMID = Convert.ToInt32(hidUID.Value);
                }
                plobj.sSiteCode = Session["SiteCode"].ToString();
                plobj.CREATED_BY = Session["UserID"].ToString();
                if (btnSave.Text == "Save")
                {
                    dt = blobj.SaveAdderss(plobj);
                }
                else
                {
                    dt = blobj.UpdateAddress(plobj);
                }
                Message = dt.Rows[0][0].ToString();
                if (dt.Rows.Count > 0)
                {
                    if (Message.StartsWith("ERROR~"))
                    {
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    else if (Message.StartsWith("N~"))
                    {
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    else
                    {
                        Reset();
                        BindGrid();
                        CommonHelper.ShowMessage("Record saved successfully", msgsuccess, CommonHelper.MessageType.Success.ToString());
                    }
                }
                else
                {
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    Reset();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void EditRecords(string _SN)
        {
            try
            {
                BL_InvoiceMaster dlobj = new BL_InvoiceMaster();
                PL_invoiceMaster plobj = new PL_invoiceMaster();
                plobj.sInvoiceNo = _SN;
                DataTable dtDetails = dlobj.SearchAddress(_SN);
                if (dtDetails.Rows.Count > 0)
                {
                    if (ddlFGItemCode.Items.Count > 0)
                    {
                        ddlFGItemCode.SelectedValue = dtDetails.Rows[0]["MODEL_CODE"].ToString();
                    }
                    txtModel.Text = dtDetails.Rows[0]["MODEL_CODE"].ToString();
                    txtAddress1.Text = dtDetails.Rows[0]["ADDRESS1"].ToString();
                    txtAdd2.Text = dtDetails.Rows[0]["ADDRESS2"].ToString();
                    txtAddress3.Text = dtDetails.Rows[0]["ADDRESS3"].ToString();
                    txtAdd4.Text = dtDetails.Rows[0]["ADDRESS4"].ToString();
                    txtAdd4.Text = dtDetails.Rows[0]["ADDRESS5"].ToString();
                    txtAdd6.Text = dtDetails.Rows[0]["ADDRESS6"].ToString();
                    txtAdd7.Text = dtDetails.Rows[0]["ADDRESS7"].ToString();
                    ddlFGItemCode.Enabled = false;
                    hidUpdate.Value = "Update";
                    hidUID.Value = _SN;
                }
                else
                {
                    CommonHelper.ShowMessage("No  details found", msgerror, CommonHelper.MessageType.Error.ToString());

                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        private void DeleteRecords(string _SN)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                BL_InvoiceMaster blobj = new BL_InvoiceMaster();
                PL_invoiceMaster plobj = new PL_invoiceMaster();
                DataTable sResult = new DataTable();
                sResult = blobj.DeleteAddress(_SN);
                Message = sResult.Rows[0][0].ToString();
                if (sResult.Rows.Count > 0)
                {
                    if (Message.StartsWith("ERROR~"))
                    {
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    else if (Message.StartsWith("N~"))
                    {
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    else
                    {
                        
                        CommonHelper.ShowMessage("Record deleted successfully", msgsuccess, CommonHelper.MessageType.Error.ToString());
                    }
                }
                else
                {
                    CommonHelper.ShowMessage("No result found", msgerror, CommonHelper.MessageType.Error.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
            finally
            {
                btnSave.Text = "Save";
                BindGrid();
            }

        }
        public void Reset()
        {
            try
            {
                txtModel.Text = string.Empty;
                ddlFGItemCode.Enabled = true;
                txtAddress1.Text = string.Empty;
                txtAddress3.Text = string.Empty;
                txtAdd2.Text = string.Empty;
                txtAdd4.Text = string.Empty;
                txtAdd5.Text = string.Empty;
                txtAdd6.Text = string.Empty;
                txtAdd7.Text = string.Empty;
                BindFGItemCode();
                BindGrid();
                btnSave.Text = "Save";
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        protected void gvShippingAddress_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvShippingAddress.PageIndex = e.NewPageIndex;
                BindGrid();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void gvShippingAddress_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                string _SN = string.Empty;
                string[] strValue = e.CommandArgument.ToString().Split('~');
                _SN = e.CommandArgument.ToString();
                if (e.CommandName == "DeleteRecords")
                {
                    DeleteRecords(_SN);
                }
                if (e.CommandName == "EditRecords")
                {
                    if (btnSave.Text == "Save")
                    {
                        btnSave.Text = "Update";
                    }
                    EditRecords(_SN);
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                Reset();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void ddlFGItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlFGItemCode.SelectedIndex > 0)
                {
                    txtModel.Text = ddlFGItemCode.SelectedValue.ToString();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }

        }
    }
}