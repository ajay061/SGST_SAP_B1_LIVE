using PL;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;
using BusinessLayer.FG;

namespace SGST.INE.FG
{
    public partial class InvoiceLabel : System.Web.UI.Page
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
                    string _strRights = CommonHelper.GetRights("GENERATE INVOICE", (DataTable)Session["USER_RIGHTS"]);
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
                    BindCustomerCode();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }

        }
        public void BindCustomerCode()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                drpCustomerCode.Items.Clear();
                BL_GenereatePackingList blobj = new BL_GenereatePackingList();
                DataTable dt = blobj.BindData("BINDCUSTOMERCODE", "", "", "");
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(drpCustomerCode, dt, true);
                    drpCustomerCode.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        public void BindOutBondDeliveryNo()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                drpOutBondDeliveryNo.Items.Clear();
                string sCustomerCode = drpCustomerCode.Text;
                BL_GenereatePackingList blobj = new BL_GenereatePackingList();
                DataTable dt = blobj.BindData("BINDORDERDELIVERYNO", sCustomerCode, "", "");
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(drpOutBondDeliveryNo, dt, true);
                    drpOutBondDeliveryNo.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        public void BindInvoiceNo()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                drpInvoiceNo.Items.Clear();
                string sCustomerCode = drpCustomerCode.Text;
                string sOutBondDeliveryNo = drpOutBondDeliveryNo.Text;
               
                BL_GenereatePackingList blobj = new BL_GenereatePackingList();
                DataTable dt = blobj.BindData("BINDINVOICENO", sCustomerCode, sOutBondDeliveryNo, "");
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(drpInvoiceNo, dt, true);
                    drpInvoiceNo.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        protected void drpCustomerCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (drpCustomerCode.SelectedIndex > 0 )
                {
                    BindOutBondDeliveryNo();
                }
                else
                {
                    drpOutBondDeliveryNo.DataSource = null;
                    drpOutBondDeliveryNo.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void drpOutBondDeliveryNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (drpOutBondDeliveryNo.SelectedIndex > 0)
                {
                    BindInvoiceNo();
                }
                else
                {
                    drpInvoiceNo.DataSource = null;
                    drpInvoiceNo.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void drpInvoiceNo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (drpCustomerCode.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select customer code", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpCustomerCode.Focus();
                    return;
                }
                if (drpOutBondDeliveryNo.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select outbond delivery no", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpOutBondDeliveryNo.Focus();
                    return;
                }
                if (drpInvoiceNo.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select invoice no", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpInvoiceNo.Focus();
                    return;
                }
                string customercode = drpCustomerCode.SelectedItem.Text.Trim();
                string OutbonddeliveryNo = drpOutBondDeliveryNo.SelectedItem.Text.Trim();
                string InvoiceNo = drpInvoiceNo.SelectedItem.Text.Trim();                
                string CREATED_BY = Session["userid"].ToString();
                decimal dGrossWeight = 0;
                decimal dNetWeight = 0;
                if (txtGrossWeight.Text.Trim().Length > 0)
                {
                    dGrossWeight = Convert.ToDecimal(txtGrossWeight.Text);
                }
                if (txtNetWeight.Text.Trim().Length > 0)
                {
                    dNetWeight = Convert.ToDecimal(txtNetWeight.Text);
                }               

                BL_GenereatePackingList blobj = new BL_GenereatePackingList();
                string sResult = string.Empty;
                DataTable dt = blobj.blSavePackingData("SAVEPACKINGDATA",customercode, OutbonddeliveryNo, InvoiceNo
                    ,txtFlightNo.Text,txtPortOfLoading.Text,txtPlaceOfReceipt.Text,txtPreCarragedBy.Text,
                    txtPortOfDischarged.Text,txtFinalDestination.Text, dGrossWeight, dNetWeight, txtDimensionOfcargo.Text
                    );
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
                    if (sResult.StartsWith("N~"))
                    {
                        CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                    if (sResult.StartsWith("ERROR~"))
                    {
                        CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                    else
                    {
                        CommonHelper.ShowMessage(sResult.Split('~')[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                        txtDimensionOfcargo.Text = string.Empty;
                        txtFinalDestination.Text = string.Empty;
                        txtFlightNo.Text = string.Empty;
                        txtGrossWeight.Text = string.Empty;
                        txtNetWeight.Text = string.Empty;
                        txtPlaceOfReceipt.Text = string.Empty;
                        txtPortOfDischarged.Text = string.Empty;
                        txtPortOfLoading.Text = string.Empty;
                        txtPreCarragedBy.Text = string.Empty;
                        drpInvoiceNo.Items.Clear();
                        drpOutBondDeliveryNo.Items.Clear();
                        BindCustomerCode();
                    }
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
                txtDimensionOfcargo.Text = string.Empty;
                txtFinalDestination.Text = string.Empty;
                txtFlightNo.Text = string.Empty;
                txtGrossWeight.Text = string.Empty;
                txtNetWeight.Text = string.Empty;
                txtPlaceOfReceipt.Text = string.Empty;
                txtPortOfDischarged.Text = string.Empty;
                txtPortOfLoading.Text = string.Empty;
                txtPreCarragedBy.Text = string.Empty;
                drpInvoiceNo.Items.Clear();
                drpOutBondDeliveryNo.Items.Clear();
                BindCustomerCode();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}