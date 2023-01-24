using BusinessLayer;
using Common;
using PL;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace SGST.INE.Masters
{
    public partial class Purchase_Order : System.Web.UI.Page
    {
        BL_Purchase_Order blobj = new BL_Purchase_Order();
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
                    string _strRights = CommonHelper.GetRights("PURCHASE ORDER Master", (DataTable)Session["USER_RIGHTS"]);
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
                BL_Purchase_Order blobj = new BL_Purchase_Order();
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
        protected void SaveData()
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (txtPurchaseOrderNo.Text.Trim() == "")
                {
                    CommonHelper.ShowMessage("Enter Purchase Order No.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPurchaseOrderNo.Focus();
                    return;
                }
                if (txtpurchaseDate.Text.Trim() == "")
                {
                    CommonHelper.ShowMessage("Enter Purchase Date", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtpurchaseDate.Focus();
                    return;
                }
                if (ddlFGItemCode.SelectedIndex == 0 && btnSave.Text == "Save")
                {
                    CommonHelper.ShowMessage("Please Select FG Item Code", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlFGItemCode.Focus();
                    return;
                }
                if (txtPOQty.Text.Trim() == "")
                {
                    CommonHelper.ShowMessage("Enter PO Qty", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPOQty.Focus();
                    return;
                }
                if (Convert.ToInt32(txtPOQty.Text) == 0)
                {
                    CommonHelper.ShowMessage("Please Enter valid PO Qty", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPOQty.Focus();
                    return;
                }
                BL_Purchase_Order blobj = new BL_Purchase_Order();
                DataTable dt = new DataTable();
                PL.PL_PurchaseOrder plobj = new PL.PL_PurchaseOrder();
                plobj.sPurchaseOrderNo = txtPurchaseOrderNo.Text;
                plobj.sModelCode = txtModel.Text;
                plobj.sPurchaseDate = txtpurchaseDate.Text;
                plobj.iPO_QTY = Convert.ToInt32(txtPOQty.Text.Trim());
                plobj.sSiteCode = Session["SiteCode"].ToString();
                plobj.CREATED_BY = Session["UserID"].ToString();
                plobj.Address1 = txtAddress1.Text.Trim();
                plobj.Address2 = "";
                plobj.Address3 = "";
                plobj.Address4 = "";
                plobj.Active = true;
                if (btnSave.Text == "Save")
                {
                    dt = blobj.SavePurChaseOrder(plobj);
                }
                else
                {
                    dt = blobj.UpdatePurChaseOrder(plobj);
                }
                Message = dt.Rows[0][0].ToString();
                if (dt.Rows.Count > 0)
                {
                    if (Message.StartsWith("ERROR~") || Message.StartsWith("N~"))
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
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError,  System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }
        protected void BindGrid()
        {
            try
            {
                BL_Purchase_Order blobj = new BL_Purchase_Order();
                PL_PurchaseOrder plobj = new PL_PurchaseOrder();
                DataTable dt = blobj.BindGrid();
                if (dt.Rows.Count > 0)
                {
                    gvPurchaseOrser.DataSource = dt;
                    gvPurchaseOrser.DataBind();
                    lblNumberofRecords.Text = dt.Rows.Count.ToString();
                }
                else
                {
                    gvPurchaseOrser.DataSource = null;
                    gvPurchaseOrser.DataBind();
                    lblNumberofRecords.Text = "0";
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }

        }
        protected void gvPurchaseOrser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvPurchaseOrser.PageIndex = e.NewPageIndex;
                BindGrid();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        protected void gvPurchaseOrser_RowCommand(object sender, GridViewCommandEventArgs e)
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
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void EditRecords(string _SN)
        {
            try
            {
                BL_Purchase_Order dlobj = new BL_Purchase_Order();
                PL_PurchaseOrder plobj = new PL_PurchaseOrder();
                plobj.PO_ID = Convert.ToInt32(_SN);
                DataTable dtDetails = dlobj.SearchPurchaseOrder(_SN);
                if (dtDetails.Rows.Count > 0)
                {
                    txtPurchaseOrderNo.Text = dtDetails.Rows[0]["PURCHASE_ORDER"].ToString();
                    if (ddlFGItemCode.Items.Count > 0)
                    {
                        ddlFGItemCode.SelectedItem.Text = dtDetails.Rows[0]["BOM_CODE"].ToString();
                    }
                    txtModel.Text = dtDetails.Rows[0]["MODEL_CODE"].ToString();
                    txtPurchaseOrderNo.Enabled = false;
                    ddlFGItemCode.Enabled = false;
                    txtpurchaseDate.Text = dtDetails.Rows[0]["PURCHASE_DATE"].ToString();
                    txtPOQty.Text = dtDetails.Rows[0]["PO_QTY"].ToString();
                    txtAddress1.Text = dtDetails.Rows[0]["ADDRESS1"].ToString();
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

                BL_Purchase_Order blobj = new BL_Purchase_Order();
                PL_PurchaseOrder plobj = new PL_PurchaseOrder();
                DataTable sResult = new DataTable();
                sResult = blobj.DeletePurchaseOrder(_SN);
                Message = sResult.Rows[0][0].ToString();
                if (sResult.Rows.Count > 0)
                {
                    if (Message.StartsWith("ERROR~"))
                    {
                        txtpurchaseDate.Text = hidPoOrderID.Value;
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    if (Message.StartsWith("N~"))
                    {
                        txtpurchaseDate.Text = hidPoOrderID.Value;
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    else
                    {
                        BindGrid();
                        txtpurchaseDate.Text = hidPoOrderID.Value;
                        CommonHelper.ShowMessage(Message, msgsuccess, CommonHelper.MessageType.Error.ToString());
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
                BindGrid();
            }

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
        public void Reset()
        {
            try
            {
                txtModel.Text = string.Empty;
                txtPurchaseOrderNo.Text = "";
                ddlFGItemCode.Items.Clear();
                txtpurchaseDate.Text = "";
                txtPOQty.Text = "";
                txtAddress1.Text = "";
                txtPurchaseOrderNo.Focus();
                txtPurchaseOrderNo.Enabled = true;
                ddlFGItemCode.Enabled = true;
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