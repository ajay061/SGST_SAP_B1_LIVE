using Common;
using System;
using System.Web.UI.WebControls;
using BusinessLayer.Masters;
using System.Data;
using PL;

namespace SGST.INE.Masters
{
    public partial class WIPSamplingMaster : System.Web.UI.Page
    {
        string Message = string.Empty;
        BL_SamplingMaster blobj = new BL_SamplingMaster();
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
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (Session["usertype"].ToString() != "ADMIN")
                {
                    string _strRights = CommonHelper.GetRights("SAMPLING MASTER", (DataTable)Session["USER_RIGHTS"]);
                    CommonHelper._strRights = _strRights.Split('^');
                    if (CommonHelper._strRights[0] == "0")  //Check view rights
                    {
                        Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                    }
                }
                if (!IsPostBack)
                {
                    BindFGitemcode();
                    BindMachineID();
                    ShowGridData();
                    drpFGItemCode.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        private void BindFGitemcode()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                BL_SamplingMaster blobj = new BL_SamplingMaster();
                DataTable dt = blobj.BindFGitemCode(Session["SiteCode"].ToString());
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(drpFGItemCode, dt, true);
                    drpFGItemCode.SelectedIndex = 0;
                    drpFGItemCode.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        private void BindMachineID()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                BL_SamplingMaster blobj = new BL_SamplingMaster();
                DataTable dt = blobj.BindMachineID(Session["SiteCode"].ToString());
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillMachine(drpModuleType, dt, true);
                    drpModuleType.SelectedIndex = 0;
                    drpModuleType.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        private void ShowGridData()
        {
            try
            {
                BL_SamplingMaster blobj = new BL_SamplingMaster();
                DataTable dt = blobj.GetData(Session["SiteCode"].ToString());
                lblNumberofRecords.Text = dt.Rows.Count.ToString();
                if (dt.Rows.Count > 0)
                {
                    gvSamplingMaster.DataSource = dt;
                    gvSamplingMaster.DataBind();
                    dt.TableName = "Table1";
                    dt.AcceptChanges();
                    System.Data.DataView view = new System.Data.DataView(dt.DefaultView.ToTable(true, "FG_ITEM_CODE"));
                    System.Data.DataTable selected =
                            view.ToTable("Table1", false, "FG_ITEM_CODE");
                    clsCommon.FillComboBox(drpFGItemCodeSearch, selected, true);
                    ViewState["Data"] = dt;
                }
                else
                {
                    gvSamplingMaster.DataSource = dt;
                    gvSamplingMaster.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        protected void gvSamplingMaster_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                string _SN = string.Empty; 
                _SN = e.CommandArgument.ToString();
                if (e.CommandName == "EditRecords")
                {
                    if (btnSave.Text == "Save")
                    { btnSave.Text = "Update"; }
                    EditRecords(_SN);
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void gvSamplingMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvSamplingMaster.PageIndex = e.NewPageIndex;
                ShowGridData();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        private void EditRecords(string _SN)
        {
            try
            {
                blobj = new BL_SamplingMaster();
                PL_SamplingMaster plboj = new PL_SamplingMaster();
                plboj.iSMID = Convert.ToInt32(_SN);
                DataTable dt = blobj.GetSeletedData(plboj, Session["SiteCode"].ToString());
                if (dt.Rows.Count > 0)
                {
                    drpFGItemCode.Enabled = false;
                    drpFGItemCode.Text = dt.Rows[0][2].ToString();
                    drpModuleType.Text = dt.Rows[0][3].ToString();
                    txtLotQty.Text = dt.Rows[0][4].ToString();
                    txtSamplingQty.Text = dt.Rows[0][5].ToString();
                    txtLTHours.Text = dt.Rows[0][6].ToString();
                    txtXraySamplingQty.Text = dt.Rows[0]["XRAYSAMPLINGQTY"].ToString();
                    txtPdiSamplingQty.Text = dt.Rows[0]["PDISAMPLINGQTY"].ToString();
                    hidUID.Value = _SN;
                    btnSave.Text = "Update";
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (drpFGItemCode.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select fg item code", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpFGItemCode.Focus();
                    return;
                }
                if (drpModuleType.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select machine id", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpModuleType.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtLotQty.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please enter lot quantity", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtLotQty.Focus();
                    return;
                }
                if (Convert.ToInt32(txtLotQty.Text.Trim()) == 0)
                {
                    CommonHelper.ShowMessage("Please enter valid lot quantity", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtLotQty.Focus();
                    txtLotQty.Text = string.Empty;
                    return;
                }
                if (string.IsNullOrEmpty(txtSamplingQty.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please enter sampling quantity", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtSamplingQty.Focus();
                    return;
                }
                if (Convert.ToInt32(txtSamplingQty.Text.Trim()) == 0)
                {
                    CommonHelper.ShowMessage("Please enter valid sampling quantity", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtSamplingQty.Focus();
                    txtSamplingQty.Text = string.Empty;
                    return;
                }
                if (Convert.ToInt32(txtSamplingQty.Text.Trim()) > Convert.ToInt32(txtLotQty.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Sampling quantity can not be greater than lot quantity ", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtSamplingQty.Focus();
                    txtSamplingQty.Text = string.Empty;
                    return;
                }
                if (string.IsNullOrEmpty(txtLTHours.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please enter LT hours", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtLTHours.Focus();
                    return;
                }
                if (Convert.ToInt32(txtLTHours.Text.Trim()) == 0)
                {
                    CommonHelper.ShowMessage("Please enter valid LT hours", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtLTHours.Focus();
                    txtLTHours.Text = string.Empty;
                    return;
                }
                if (Convert.ToInt32(txtXraySamplingQty.Text.Trim()) == 0)
                {
                    CommonHelper.ShowMessage("Please enter valid Xray sampling quantity", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtXraySamplingQty.Focus();
                    txtXraySamplingQty.Text = "1";
                    return;
                }
                if (Convert.ToInt32(txtPdiSamplingQty.Text.Trim()) == 0 || string.IsNullOrEmpty(txtPdiSamplingQty.Text))
                {
                    CommonHelper.ShowMessage("Please enter valid PDI sampling quantity", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPdiSamplingQty.Focus();
                    txtPdiSamplingQty.Text = "1";
                    return;
                }
                blobj = new BL_SamplingMaster();
                PL_SamplingMaster plboj = new PL_SamplingMaster();
                plboj.sFGItemCode = drpFGItemCode.Text;
                plboj.sModuleType = drpModuleType.SelectedValue.ToString();
                plboj.iLotqty = Convert.ToInt32(txtLotQty.Text);
                plboj.iSamplingQty = Convert.ToInt32(txtSamplingQty.Text);
                plboj.iLtHours = Convert.ToInt32(txtLTHours.Text);
                plboj.iXraySamplingQty = Convert.ToInt32(txtXraySamplingQty.Text);
                plboj.iPDISamplingQty = Convert.ToInt32(txtPdiSamplingQty.Text);
                string sResult = string.Empty;
                if (btnSave.Text.ToUpper() == "SAVE")
                {
                    sResult = blobj.SaveData(plboj, Session["SiteCode"].ToString(), Session["UserID"].ToString());
                }
                else
                {
                    plboj.iSMID = Convert.ToInt32(hidUID.Value);
                    sResult = blobj.UpdateData(plboj, Session["SiteCode"].ToString(), Session["UserID"].ToString());
                }
                if (sResult.Length > 0)
                {
                    if (sResult.StartsWith("SUCCESS~"))
                    {
                        CommonHelper.ShowMessage(sResult.Split('~')[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                        ShowGridData();
                    }
                    else
                    {
                        CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        ShowGridData();
                    }
                    btnReset_Click(null, null);
                }
                else
                {
                    CommonHelper.ShowMessage("No result found, Please try again", msgerror, CommonHelper.MessageType.Error.ToString());
                    ShowGridData();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                txtXraySamplingQty.Text = "1";
                drpFGItemCode.SelectedIndex = 0;                
                txtLotQty.Text = string.Empty;
                txtLTHours.Text = string.Empty;
                txtSamplingQty.Text = string.Empty;
                hidUID.Value = "";
                drpModuleType.SelectedIndex = 0;
                btnSave.Text = "Save";
                drpFGItemCode.Enabled = true;
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void drpFGItemCodeSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpFGItemCodeSearch.SelectedIndex > 0)
                {
                    DataTable dt = (DataTable)ViewState["Data"];
                    DataView dataView = dt.DefaultView;
                    dataView.RowFilter = "FG_ITEM_CODE = '" + drpFGItemCodeSearch.SelectedValue + "'";
                    gvSamplingMaster.DataSource = dataView;
                    gvSamplingMaster.DataBind();
                }
                else
                {
                    DataTable dt = (DataTable)ViewState["Data"];
                    gvSamplingMaster.DataSource = dt;
                    gvSamplingMaster.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}