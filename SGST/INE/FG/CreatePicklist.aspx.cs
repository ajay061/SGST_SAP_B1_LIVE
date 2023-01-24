using System;
using System.Web.UI.WebControls;
using System.Data;
using Common;
using BusinessLayer.FG;

namespace SGST.INE.FG
{
    public partial class CreatePicklist : System.Web.UI.Page
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
                    string _strRights = CommonHelper.GetRights("PICKLIST ALLOCATION", (DataTable)Session["USER_RIGHTS"]);
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
                    getCustomers();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void drpCustomerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bind_Outbound();
        }

        protected void drpOutboundDelivery_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bind_Sales_Order();
            Bind_Createpicklist();
        }
        public void getCustomers()
        {
            try
            {
                drpCustomerName.Items.Clear();
                BL_Dispatch blobj = new BL_Dispatch();
                DataTable dt = blobj.BindCustomers(Session["SiteCode"].ToString()
                        , Session["LINECODE"].ToString());
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillMultiColumnsCombo(drpCustomerName, dt, true);
                    drpCustomerName.SelectedIndex = 0;
                    drpCustomerName.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                return;
            }
        }
        public void Bind_Outbound()
        {
            try
            {
                if (drpCustomerName.SelectedIndex > 0)
                {
                    string CUSTOMER_NO = drpCustomerName.SelectedValue.ToString();
                    BL_Dispatch blobj = new BL_Dispatch();
                    DataTable dt = blobj.BINDOUTBOUND(CUSTOMER_NO
                        , Session["SiteCode"].ToString(), Session["LINECODE"].ToString()
                        );
                    if (dt.Rows.Count > 0)
                    {
                        clsCommon.FillComboBox(drpOutboundDelivery, dt, true);
                        drpOutboundDelivery.SelectedIndex = 0;
                        drpOutboundDelivery.Focus();
                    }
                }
                else
                {
                    GV_Picklist.DataSource = null;
                    GV_Picklist.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        public void Bind_Sales_Order()
        {
            try
            {        
                if (drpCustomerName.SelectedIndex > 0)
                {
                    string CUSTOMER_NO = drpCustomerName.SelectedValue.ToString();
                    string OUTBOUND_DELIVERY = drpOutboundDelivery.SelectedItem.Text;
                    BL_Dispatch blobj = new BL_Dispatch();
                    DataTable dt = blobj.Bind_Sales_Order(CUSTOMER_NO, OUTBOUND_DELIVERY, Session["SiteCode"].ToString(), Session["LINECODE"].ToString());
                    if (dt.Rows.Count > 0)
                    {
                        string Credits = dt.Rows[0]["SALES_ORDER_NO"].ToString();
                        lblOrderNo.Text = Credits;
                    }
                }
                else
                {
                    GV_Picklist.DataSource = null;
                    GV_Picklist.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        public void Bind_Createpicklist()
        {
            try
            {
                string OUTBOUND = drpOutboundDelivery.SelectedItem.Text.Trim();
                string CUDTOMERNO = drpCustomerName.SelectedValue.ToString();
                string SALESORDERNO = lblOrderNo.Text.Trim();
                BL_Dispatch blobj = new BL_Dispatch();
                DataTable dt = blobj.BINDDETAILS(OUTBOUND, CUDTOMERNO, SALESORDERNO, Session["SiteCode"].ToString(), Session["LINECODE"].ToString());
                if (dt.Rows.Count > 0)
                {
                    GV_Picklist.DataSource = dt;
                    GV_Picklist.DataBind();
                    Session["RequestValues"] = dt;
                    lblInvoiceNo.Text = dt.Rows[0]["INVOICE_NO"].ToString();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void GV_Picklist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_Picklist.PageIndex = e.NewPageIndex;
            GV_Picklist.DataBind();
        }
        protected void btnCreatePicklist_Click(object sender, EventArgs e)
        {
            try
            {
                if (drpCustomerName.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select customer ", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpCustomerName.Focus();
                    return;
                }
                if (drpOutboundDelivery.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select outbound delivery no. ", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpOutboundDelivery.Focus();
                    return;
                }
                if (GV_Picklist.Rows.Count == 0)
                {
                    CommonHelper.ShowMessage("No data found for allocate, Please contact admin", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpOutboundDelivery.Focus();
                    return;
                }
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                DataTable dt1 = Session["RequestValues"] as DataTable;
                string SALES_ORDER_NO = lblOrderNo.Text.Trim();
                string DPNO = lblOrderNo.Text.Trim();
                string CREATED_BY = Session["userid"].ToString();
                string OUTBOUND = drpOutboundDelivery.Text.Trim();
                string CUDTOMERNO = drpCustomerName.SelectedValue.ToString();
                BL_Dispatch blobj = new BL_Dispatch();
                string sResult = string.Empty;
                if (dt1.Rows.Count > 0)
                {
                    sResult = blobj.GeneratePickListNo(SALES_ORDER_NO, CREATED_BY, CUDTOMERNO, OUTBOUND, Session["SiteCode"].ToString(), Session["LINECODE"].ToString());
                    if (sResult.Length > 0)
                    {
                        if (sResult.ToUpper().StartsWith("SUCCESS"))
                        {
                            CommonHelper.ShowMessage(sResult.Split('~')[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                            drpCustomerName.Enabled = true;
                            drpCustomerName.Height = 35;
                            drpOutboundDelivery.Items.Clear();
                            lblOrderNo.Text = string.Empty;
                            getCustomers();
                            GV_Picklist.DataSource = null;
                            GV_Picklist.DataBind();
                        }
                        else
                        {
                            CommonHelper.ShowMessage(sResult, msgerror, CommonHelper.MessageType.Error.ToString());
                        }
                    }
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
                drpCustomerName.Enabled = true;
                drpCustomerName.Height = 35;
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                drpOutboundDelivery.Items.Clear();
                drpOutboundDelivery.DataSource = null;
                lblOrderNo.Enabled = true;
                lblOrderNo.Text = string.Empty;
                getCustomers();
                GV_Picklist.DataSource = null;
                GV_Picklist.DataBind();
                lblInvoiceNo.Text = string.Empty;
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}