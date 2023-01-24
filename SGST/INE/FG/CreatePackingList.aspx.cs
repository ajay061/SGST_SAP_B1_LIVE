using System;
using System.Web.UI.WebControls;
using System.Data;
using Common;
using BusinessLayer.FG;

namespace SGST.INE.FG
{
    public partial class PackingList : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
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
                if (Session["usertype"].ToString() != "ADMIN")
                {
                    string _strRights = CommonHelper.GetRights("ITEMS PICKING", (DataTable)Session["USER_RIGHTS"]);
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
                    GetOutboundDeliveryNo();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        public void GetOutboundDeliveryNo()
        {
            try
            {
                drpOutboundDelivery.Items.Clear();
                BL_PackingList blobj = new BL_PackingList();
                DataTable dt = blobj.BINDOUTBOUND(Session["SiteCode"].ToString(), Session["LINECODE"].ToString());
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(drpOutboundDelivery, dt, true);
                    drpOutboundDelivery.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        public void Bind_Sales_Order()
        {
            try
            {
                if (drpOutboundDelivery.SelectedIndex > 0)
                {
                    ddlsalesOrder.Items.Clear();
                    string OUTBOUND_DELIVERY = drpOutboundDelivery.SelectedItem.Text;
                    BL_PackingList blobj = new BL_PackingList();
                    DataTable dt = blobj.Bind_Sales_Order(OUTBOUND_DELIVERY
                        , Session["SiteCode"].ToString()
                        , Session["LINECODE"].ToString()
                        );
                    if (dt.Rows.Count > 0)
                    {
                        clsCommon.FillComboBox(ddlsalesOrder, dt, true);
                        ddlsalesOrder.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        public void GetPickList()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                txtScanbar.Text = string.Empty;
                ddlPicklist.Items.Clear();
                ddlItemCode.Items.Clear();
                string SALES_ORDER_NO = ddlsalesOrder.SelectedItem.Text.Trim();
                BL_PackingList blobj = new BL_PackingList();
                DataTable dt = blobj.BindPickList(SALES_ORDER_NO
                     , Session["SiteCode"].ToString()
                        , Session["LINECODE"].ToString());
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(ddlPicklist, dt, true);
                    ddlPicklist.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        public void GetItemCode()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                txtScanbar.Text = string.Empty;
                ddlItemCode.Items.Clear();
                string SALES_ORDER_NO = ddlsalesOrder.SelectedItem.Text.Trim();
                string PICKLIST_NO = ddlPicklist.Text;
                BL_PackingList blobj = new BL_PackingList();
                DataTable dt = blobj.BindItemCode(SALES_ORDER_NO, PICKLIST_NO
                     , Session["SiteCode"].ToString()
                        , Session["LINECODE"].ToString()
                    );
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(ddlItemCode, dt, true);
                    ddlItemCode.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }

        protected void drpOutboundDelivery_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bind_Sales_Order();
        }

        protected void ddlsalesOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (ddlsalesOrder.SelectedIndex > 0)
                {
                    GetPickList();
                }
                else
                {
                    ddlPicklist.DataSource = null;
                    ddlPicklist.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void ddlPackinglist_SelectedIndexChanged(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                ddlItemCode.DataSource = null;
                ddlItemCode.DataBind();
                GV_Packinglist.DataSource = null;
                GV_Packinglist.DataBind();
                if (ddlPicklist.SelectedIndex > 0)
                {
                    GetItemCode();
                    BindPicklstDetails();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void ddlItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (ddlItemCode.SelectedIndex > 0)
                {
                    BindPicklstDetails();
                }
                else
                {
                    GV_Packinglist.DataSource = null;
                    GV_Packinglist.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        public void BindPicklstDetails()
        {
            try
            {
                string SALES_ORDER_NO = ddlsalesOrder.SelectedItem.Text.Trim();
                string PICKLIST_NO = ddlPicklist.SelectedItem.Text.Trim();
                string ITEM_CODE = "";
                BL_PackingList blobj = new BL_PackingList();
                DataTable dt = blobj.BINDDETAILS(SALES_ORDER_NO, PICKLIST_NO, ITEM_CODE
                     , Session["SiteCode"].ToString()
                        , Session["LINECODE"].ToString()
                    );
                if (dt.Rows.Count > 0)
                {
                    GV_Packinglist.DataSource = dt;
                    GV_Packinglist.DataBind();
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void txtScanbar_TextChanged(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (drpOutboundDelivery.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select outbound delivery no", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpOutboundDelivery.Focus();
                    txtScanbar.Text = string.Empty;
                    return;
                }
                if (ddlsalesOrder.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select order no", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlsalesOrder.Focus();
                    txtScanbar.Text = string.Empty;
                    return;
                }
                if (ddlPicklist.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select picklist", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlPicklist.Focus();
                    txtScanbar.Text = string.Empty;
                    return;
                }
                if (string.IsNullOrEmpty(txtScanLocation.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan location", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtScanLocation.Focus();
                    txtScanbar.Text = string.Empty;
                    return;
                }
                if (string.IsNullOrEmpty(txtScanbar.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan box barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtScanbar.Focus();
                    return;
                }
                string SALES_ORDER_NO = ddlsalesOrder.SelectedItem.Text.Trim();
                string PICKLIST_NO = ddlPicklist.SelectedItem.Text.Trim();
                string ITEM_CODE = "";
                string BOX_ID = txtScanbar.Text.Trim();
                string CREATED_BY = Session["userid"].ToString();
                string sLocation = txtScanLocation.Text.Trim();
                BL_PackingList blobj = new BL_PackingList();
                string sResult = string.Empty;
                sResult = blobj.sScanFGBarcode(SALES_ORDER_NO, ITEM_CODE, PICKLIST_NO, BOX_ID, CREATED_BY, sLocation
                     , Session["SiteCode"].ToString()
                        , Session["LINECODE"].ToString()
                    );
                string[] Message = sResult.Split('~');
                if (sResult.StartsWith("N~"))
                {
                    txtScanbar.Text = "";
                    txtScanbar.Focus();
                    CommonHelper.ShowMessage(Message[1], msgerror, CommonHelper.MessageType.Error.ToString());
                }
                else if (sResult.StartsWith("ERROR~"))
                {
                    CommonHelper.ShowMessage(Message[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    txtScanbar.Text = "";
                    txtScanbar.Focus();
                }
                else
                {
                    CommonHelper.ShowMessage(Message[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                    txtScanbar.Text = string.Empty;
                    txtScanbar.Focus();
                    BindPicklstDetails();
                }
            }
            catch (Exception ex)
            {
                txtScanbar.Text = "";
                txtScanbar.Focus();
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void btnPicklist_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (drpOutboundDelivery.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select outbound delivery no.", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpOutboundDelivery.Focus();
                    return;
                }
                if (drpOutboundDelivery.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select outbond delivery no.", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpOutboundDelivery.Focus();
                    return;
                }
                if (ddlsalesOrder.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select order no.", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlsalesOrder.Focus();
                    return;
                }
                if (ddlPicklist.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select picklist", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlPicklist.Focus();
                    return;
                }
                string SALES_ORDER_NO = ddlsalesOrder.SelectedItem.Text.Trim();
                string PICKLIST_NO = ddlPicklist.SelectedItem.Text.Trim();
                string ITEM_CODE = "";
                string CREATED_BY = Session["userid"].ToString();
                if (GV_Packinglist.Rows.Count > 0)
                {
                    decimal dSQty = 0;
                    decimal dRQty = 0;
                    foreach (GridViewRow rows in GV_Packinglist.Rows)
                    {
                        dSQty = Convert.ToDecimal(rows.Cells[5].Text.ToString());
                        dRQty = Convert.ToDecimal(rows.Cells[4].Text.ToString());
                        if (dSQty > 0)
                        {
                            break;
                        }
                    }
                    if (dSQty == 0)
                    {
                        CommonHelper.ShowMessage("Please scan atleast one box", msgerror, CommonHelper.MessageType.Error.ToString());
                        txtScanbar.Focus();
                        return;
                    }
                }
                BL_PackingList blobj = new BL_PackingList();
                string sResult = string.Empty;
                sResult = blobj.GeneratePackingListNo(SALES_ORDER_NO, ITEM_CODE, PICKLIST_NO, CREATED_BY
                     , Session["SiteCode"].ToString()
                        , Session["LINECODE"].ToString()
                    );
                string[] Message = sResult.Split('~');
                if (sResult.StartsWith("N~"))
                {
                    CommonHelper.ShowMessage(Message[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    return;
                }
                else if (sResult.StartsWith("ERROR~"))
                {
                    CommonHelper.ShowMessage(Message[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    return;
                }
                else
                {
                    CommonHelper.ShowMessage(Message[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                    ddlsalesOrder.Items.Clear();
                    ddlPicklist.Items.Clear();
                    ddlItemCode.Items.Clear();
                    GetOutboundDeliveryNo();
                    txtScanbar.Text = string.Empty;
                    GV_Packinglist.DataSource = null;
                    GV_Packinglist.DataBind();
                    drpOutboundDelivery.Focus();
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
                ddlsalesOrder.Items.Clear();
                ddlPicklist.Items.Clear();
                ddlItemCode.Items.Clear();
                GetOutboundDeliveryNo();
                txtScanbar.Text = string.Empty;
                GV_Packinglist.DataSource = null;
                GV_Packinglist.DataBind();
                drpOutboundDelivery.Focus();
                txtScanLocation.Text = string.Empty;
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void txtScanLocation_TextChanged(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (drpOutboundDelivery.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select outbound delivery no.", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpOutboundDelivery.Focus();
                    txtScanLocation.Text = string.Empty; ;
                    return;
                }
                if (ddlsalesOrder.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select order no.", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlsalesOrder.Focus();
                    txtScanLocation.Text = string.Empty; ;
                    return;
                }
                if (ddlPicklist.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select picklist", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlPicklist.Focus();
                    txtScanLocation.Text = string.Empty; ;
                    return;
                }
                if (string.IsNullOrEmpty(txtScanLocation.Text))
                {
                    CommonHelper.ShowMessage("Please scan location", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtScanLocation.Focus();
                    return;
                }
                string SALES_ORDER_NO = ddlsalesOrder.SelectedItem.Text.Trim();
                string PICKLIST_NO = ddlPicklist.SelectedItem.Text.Trim();
                string ITEM_CODE = "";
                string sLocation = txtScanLocation.Text.Trim();
                string CREATED_BY = Session["userid"].ToString();
                BL_PackingList blobj = new BL_PackingList();
                string sResult = string.Empty;
                sResult = blobj.sScanLocation(SALES_ORDER_NO, ITEM_CODE, PICKLIST_NO, sLocation, CREATED_BY
                     , Session["SiteCode"].ToString()
                        , Session["LINECODE"].ToString());
                string[] Message = sResult.Split('~');
                if (sResult.StartsWith("N~"))
                {
                    txtScanbar.Text = "";
                    CommonHelper.ShowMessage(Message[1], msgerror, CommonHelper.MessageType.Error.ToString());
                }
                else if (sResult.StartsWith("ERROR~"))
                {
                    CommonHelper.ShowMessage(Message[1], msgerror, CommonHelper.MessageType.Error.ToString());
                }
                else
                {
                    CommonHelper.ShowMessage(Message[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                    txtScanbar.Text = string.Empty;
                    txtScanbar.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}