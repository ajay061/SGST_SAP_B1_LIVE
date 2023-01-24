using Common;
using System;
using System.Data;
using System.Web.UI.WebControls;
using BusinessLayer.MES.MASTERS;

namespace SGST.INE.Masters
{
    public partial class mobQualityStageDefectMaster : System.Web.UI.Page
    {
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
                    string _strRights = CommonHelper.GetRights("QUALITY DEFECT MASTER", (DataTable)Session["USER_RIGHTS"]);
                    CommonHelper._strRights = _strRights.Split('^');
                    if (CommonHelper._strRights[0] == "0")  //Check view rights
                    {
                        Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                    }
                }
                if (!IsPostBack)
                {
                    ShowGridData();
                    //BindMachineiD();
                    //drpMachineID.Focus();
                    BindDefectFilter();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        private void BindMachineiD()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                BL_mobQualityDefectMaster blobj = new BL_mobQualityDefectMaster();
                DataTable dt = blobj.dtbindDefectFilter(Session["SiteCode"].ToString(), Session["LINECODE"].ToString());
                if (dt.Rows.Count > 0)
                {
                    //clsCommon.FillMultiColumnsCombo(drpMachineID, dt, true);
                    //drpMachineID.SelectedIndex = 0;
                    //drpMachineID.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        private void BindDefectFilter()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                DataTable dt = new DataTable();
                BL_mobQualityDefectMaster blobj = new BL_mobQualityDefectMaster();
                dt = blobj.dtbindDefectFilter(Session["SiteCode"].ToString(), Session["LINECODE"].ToString());
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillMultiColumnsCombo(drpMachineFilter, dt, true);
                }
                else
                {
                    gvDefectMaster.DataSource = null;
                    gvDefectMaster.DataBind();
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
                DataTable dt = new DataTable();
                BL_mobQualityDefectMaster blobj = new BL_mobQualityDefectMaster();
                dt = blobj.SearchDetails(Session["SiteCode"].ToString(), Session["LINECODE"].ToString());
                lblNumberofRecords.Text = dt.Rows.Count.ToString();
                if (dt.Rows.Count > 0)
                {
                    gvDefectMaster.DataSource = dt;
                    gvDefectMaster.DataBind();
                    ViewState["Data"] = dt;
                }
                else
                {
                    gvDefectMaster.DataSource = null;
                    gvDefectMaster.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        protected void drpMachineID_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {               
                if (string.IsNullOrEmpty(txtDefectCode.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please enter defect code", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtDefectCode.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtDefect.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please enter defect desc", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtDefect.Focus();
                    return;
                }
                else
                {
                    BL_mobQualityDefectMaster blobj = new BL_mobQualityDefectMaster();
                    DataTable dtData = new DataTable();
                    dtData.Columns.Add("MACHINEID");
                    dtData.Columns.Add("DEFECTCODE");
                    dtData.Columns.Add("FAILURE");
                    dtData.Columns.Add("Active");
                    DataTable dtResult = new DataTable();
                    DataRow dr = dtData.NewRow();
                    dr[0] = "";
                    dr[1] = txtDefectCode.Text.Trim();
                    dr[2] = txtDefect.Text.Trim();
                    if (chkActive.Checked == true)
                    {
                        dr[3] = "1";
                    }
                    else
                    {
                        dr[3] = "0";
                    }
                    dtData.Rows.Add(dr);
                    dtData.AcceptChanges();
                    string sMachineID = hidUID.Value;
                    if (btnSave.Text == "Save")
                    {
                        dtResult = blobj.SaveDetails(dtData,Session["SiteCode"].ToString()
                            , Session["UserID"].ToString()
                            , Session["LINECODE"].ToString(),drpArea.Text);
                    }
                    else
                    {
                        dtResult = blobj.UpdateData(dtData, sMachineID, Session["SiteCode"].ToString()
                            , Session["UserID"].ToString()
                            , Session["LINECODE"].ToString(),drpArea.Text);
                    }
                    if (dtResult.Rows.Count > 0)
                    {
                        string sResult = dtResult.Rows[0][0].ToString();
                        if (sResult.StartsWith("1~"))
                        {
                            CommonHelper.ShowMessage(sResult.Split('~')[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                        }
                        else
                        {
                            CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        }
                        txtDefect.Text = "";
                        chkActive.Checked = false;                      
                        txtDefectCode.Enabled = true;
                        txtDefectCode.Text = string.Empty;
                    }
                    else
                    {
                        CommonHelper.ShowMessage("No result found.", msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
            finally
            {
                ShowGridData();
                btnSave.Text = "Save";
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);          
            txtDefect.Text = "";
            chkActive.Checked = false;
            txtDefectCode.Enabled = true;
            btnSave.Text = "Save";
            ShowGridData();
            txtDefect.Enabled = true;
            txtDefectCode.Text = string.Empty;
            if (drpMachineFilter.Items.Count > 0)
            {
                drpMachineFilter.SelectedIndex = 0;
            }
        }

        protected void gvDefectMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvDefectMaster.PageIndex = e.NewPageIndex;
                ShowGridData();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void gvDefectMaster_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                string _SN = string.Empty;
                string _sUserID = string.Empty;
                string _SiteCode = string.Empty;
                string[] strValue = e.CommandArgument.ToString().Split('~');
                _SN = e.CommandArgument.ToString();
                if (e.CommandName == "EditRecords")
                {
                    if (btnSave.Text == "Save")
                    { btnSave.Text = "Update"; }
                    EditRecords(_SN);
                }
                ShowGridData();
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
                BL_mobQualityDefectMaster blobj = new BL_mobQualityDefectMaster();
                DataTable dtDefectData = blobj.SearchDetailsByID(_SN, Session["SiteCode"].ToString(), Session["LINECODE"].ToString());
                btnSave.Visible = true;
                if (dtDefectData.Rows.Count > 0)
                {                   
                    txtDefect.Text = dtDefectData.Rows[0]["FAILURE_TYPE"].ToString();
                    txtDefectCode.Text = dtDefectData.Rows[0]["DEFECT_CODE"].ToString();
                    if (dtDefectData.Rows[0]["ACTIVE"].ToString() == "True")
                    {
                        chkActive.Checked = true;
                    }
                    else
                    {
                        chkActive.Checked = false;
                    }
                    hidUID.Value = dtDefectData.Rows[0]["DM_NO"].ToString();
                    btnSave.Text = "Update";                  
                    txtDefectCode.Enabled = false;
                    if (drpMachineFilter.Items.Count > 0)
                    {
                        drpMachineFilter.SelectedIndex = 0;
                    }
                }
                else
                {
                    CommonHelper.ShowMessage("No details found.", msgerror, CommonHelper.MessageType.Error.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void drpMachineFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpMachineFilter.SelectedIndex > 0)
                {
                    DataTable dt = (DataTable)ViewState["Data"];
                    DataView dataView = dt.DefaultView;
                    dataView.RowFilter = "DEFECT_CODE = '" + drpMachineFilter.SelectedItem.ToString() + "'";
                    gvDefectMaster.DataSource = dataView;
                    gvDefectMaster.DataBind();
                }
                else
                {
                    DataTable dt = (DataTable)ViewState["Data"];
                    gvDefectMaster.DataSource = dt;
                    gvDefectMaster.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}