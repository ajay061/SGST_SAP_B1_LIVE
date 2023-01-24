using System;
using System.Web.UI.WebControls;
using System.Data;
using Common;
using BusinessLayer.WIP;

namespace SGST.INE.WIP
{
    public partial class WIPSolderPastConfig : System.Web.UI.Page
    {
        BL_SolderPastConfig blobj = new BL_SolderPastConfig();
        string Message = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                btnupdate.CssClass = "btn btn-primary btn-block btn-flat";
                btnMachineConfig.Enabled = true;
                btnupdate.Enabled = false;
                if (Session["usertype"].ToString() != "ADMIN")
                {
                    string _strRights = CommonHelper.GetRights("SOLDER PASTE CONFIG MASTER", (DataTable)Session["USER_RIGHTS"]);
                    CommonHelper._strRights = _strRights.Split('^');
                    if (CommonHelper._strRights[0] == "0")  //Check view rights
                    {
                        Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                    }
                }
                if (!IsPostBack)
                {
                    ShowGridData();
                    Bind_MachineName();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void ShowGridData()
        {
            try
            {
                blobj = new BL_SolderPastConfig();
                DataTable dt = blobj.GetMachineConfig();
                lblNumberofRecords.Text = dt.Rows.Count.ToString();
                if (dt.Rows.Count > 0)
                {
                    GV_MachineConfig.DataSource = dt;
                    GV_MachineConfig.DataBind();
                }
                else
                {
                    GV_MachineConfig.DataSource = null;
                    GV_MachineConfig.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        public void Bind_MachineName()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                DataTable dt = new DataTable();
                ddlMACHINENAME.Items.Clear();
                blobj = new BL_SolderPastConfig();
                dt = blobj.BIND_MACHINEID();
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillMultiColumnsCombo(ddlMACHINENAME, dt, true);
                    ddlMACHINENAME.SelectedIndex = 0;
                    ddlMACHINENAME.Focus();
                }
                else
                {
                    ddlMACHINENAME.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        public void SaveMachineConfig()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (ddlMACHINENAME.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select machine", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlMACHINENAME.Focus();
                    return;
                }
                else if (string.IsNullOrEmpty(txtPROCESS_TIME.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please enter process time", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPROCESS_TIME.Focus();
                    return;
                }
                if (ddlPROCESS_TIME_ENABLE.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select process time enable", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlPROCESS_TIME_ENABLE.Focus();
                    return;
                }
                else if (string.IsNullOrEmpty(txtNEXT_PROCESS_TIME.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please enter previous process time", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtNEXT_PROCESS_TIME.Focus();
                    return;
                }
                if (ddlNEXT_PROCESS_TIME_ENABLE.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select previous process time enable", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlNEXT_PROCESS_TIME_ENABLE.Focus();
                    return;
                }

                blobj = new BL_SolderPastConfig();
                string sResult = blobj.GenerateMachineDetails(ddlMACHINENAME.SelectedValue.Trim()
                    , ddlMACHINENAME.SelectedItem.Text.Split('(')[0],
                    txtPROCESS_TIME.Text,
                ddlPROCESS_TIME_ENABLE.SelectedItem.Text,
                txtNEXT_PROCESS_TIME.Text, ddlNEXT_PROCESS_TIME_ENABLE.SelectedItem.Text
                    );
                if (sResult.StartsWith("SUCCESS~"))
                {
                    Message = sResult.Split('~')[1].ToString();
                    txtPROCESS_TIME.Text = "";
                    ddlPROCESS_TIME_ENABLE.SelectedIndex = 0;
                    txtNEXT_PROCESS_TIME.Text = "";
                    ddlNEXT_PROCESS_TIME_ENABLE.SelectedIndex = 0;
                    ddlMACHINENAME.SelectedIndex = 0;
                    CommonHelper.ShowMessage(Message, msgsuccess, CommonHelper.MessageType.Success.ToString());
                }
                else if (sResult.StartsWith("N~"))
                {
                    Message = sResult.Split('~')[1].ToString();
                    txtPROCESS_TIME.Text = "";
                    ddlPROCESS_TIME_ENABLE.SelectedIndex = 0;
                    txtNEXT_PROCESS_TIME.Text = "";
                    ddlNEXT_PROCESS_TIME_ENABLE.SelectedIndex = 0;
                    ddlMACHINENAME.SelectedIndex = 0;
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, Message);
                }
                else if (sResult.StartsWith("ERROR~"))
                {
                    Message = sResult.Split('~')[1].ToString();
                    txtPROCESS_TIME.Text = "";
                    ddlPROCESS_TIME_ENABLE.SelectedIndex = 0;
                    txtNEXT_PROCESS_TIME.Text = "";
                    ddlNEXT_PROCESS_TIME_ENABLE.SelectedIndex = 0;
                    ddlMACHINENAME.SelectedIndex = 0;
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, Message);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PK_WIP_SOLDERPASTE_CONFIG'."))
                {
                    CommonHelper.ShowCustomErrorMessage("Machine id already exist, Please enter another ID ", msgerror);
                }
                else
                {
                    CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                }
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {
                ShowGridData();
            }
        }
        protected void btnMachineConfig_Click(object sender, EventArgs e)
        {
            SaveMachineConfig();
        }
        protected void btnupdate_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (ddlMACHINENAME.Text.ToUpper().Contains("SELECT"))
                {
                    CommonHelper.ShowMessage("Please select machine", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlMACHINENAME.Focus();
                    return;
                }
                else if (string.IsNullOrEmpty(txtPROCESS_TIME.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please enter process time", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPROCESS_TIME.Focus();
                    return;
                }
                if (ddlPROCESS_TIME_ENABLE.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select process time enable", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlPROCESS_TIME_ENABLE.Focus();
                    return;
                }
                else if (string.IsNullOrEmpty(txtNEXT_PROCESS_TIME.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please enter previous process time", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtNEXT_PROCESS_TIME.Focus();
                    return;
                }
                if (ddlNEXT_PROCESS_TIME_ENABLE.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select previous process time enable", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlNEXT_PROCESS_TIME_ENABLE.Focus();
                    return;
                }
                blobj = new BL_SolderPastConfig();
                string sResult = blobj.UpdateMachineDetails(ddlMACHINENAME.SelectedValue.Trim(),
                    ddlMACHINENAME.SelectedItem.Text, txtPROCESS_TIME.Text, ddlPROCESS_TIME_ENABLE.SelectedItem.Text, 
                    txtNEXT_PROCESS_TIME.Text,
                    ddlNEXT_PROCESS_TIME_ENABLE.SelectedItem.Text
                    );
                if (sResult.StartsWith("SUCCESS~"))
                {
                    Message = sResult.Split('~')[1].ToString();
                    GV_MachineConfig.EditIndex = -1;
                    this.ShowGridData();
                    txtPROCESS_TIME.Text = "";
                    ddlPROCESS_TIME_ENABLE.SelectedIndex = 0;
                    txtNEXT_PROCESS_TIME.Text = "";
                    ddlNEXT_PROCESS_TIME_ENABLE.SelectedIndex = 0;
                    CommonHelper.ShowMessage(Message, msgsuccess, CommonHelper.MessageType.Success.ToString());
                }
                else if (sResult.StartsWith("N~"))
                {
                    Message = sResult.Split('~')[1].ToString();
                    txtPROCESS_TIME.Text = "";
                    ddlPROCESS_TIME_ENABLE.SelectedIndex = 0;
                    txtNEXT_PROCESS_TIME.Text = "";
                    ddlNEXT_PROCESS_TIME_ENABLE.SelectedIndex = 0;
                    ddlMACHINENAME.SelectedIndex = 0;
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, Message);
                }
                else if (sResult.StartsWith("ERROR~"))
                {
                    Message = sResult.Split('~')[1].ToString();
                    txtPROCESS_TIME.Text = "";
                    ddlPROCESS_TIME_ENABLE.SelectedIndex = 0;
                    txtNEXT_PROCESS_TIME.Text = "";
                    ddlNEXT_PROCESS_TIME_ENABLE.SelectedIndex = 0;
                    ddlMACHINENAME.SelectedIndex = 0;
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, Message);
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
            Response.Redirect("WIPSolderPastConfig.aspx");
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            Bind_MachineName();
            txtPROCESS_TIME.Text = "";
            txtNEXT_PROCESS_TIME.Text = "";
            btnupdate.CssClass = "btn btn-primary btn-block btn-flat";
            btnMachineConfig.Enabled = true;
            btnupdate.Enabled = false;
        }

        protected void GV_MachineConfig_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                string _SN = string.Empty;
                string[] strValue = e.CommandArgument.ToString().Split('~');
                _SN = e.CommandArgument.ToString();
                if (e.CommandName == "EditRecords")
                {
                    btnupdate.Enabled = true;
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
                blobj = new BL_SolderPastConfig();
                DataTable dt = blobj.GetSeletedData(_SN);
                if (dt.Rows.Count > 0)
                {
                    string sMachineID = dt.Rows[0][0].ToString();
                    string MACHINENAME = dt.Rows[0][1].ToString();
                    string PROCESSTIME = dt.Rows[0][2].ToString();
                    string PROCESSTIMEENAME = dt.Rows[0][3].ToString();
                    string NEXTPROCESSTIME = dt.Rows[0][4].ToString();
                    string NEXTPROCESSTIMEENABLE = dt.Rows[0][5].ToString();
                    ddlMACHINENAME.SelectedValue = sMachineID;
                    txtPROCESS_TIME.Text = PROCESSTIME;
                    ddlPROCESS_TIME_ENABLE.SelectedValue = PROCESSTIMEENAME;
                    txtNEXT_PROCESS_TIME.Text = NEXTPROCESSTIME;
                    ddlNEXT_PROCESS_TIME_ENABLE.SelectedValue = NEXTPROCESSTIMEENABLE;
                    // ddlMACHINENAME.Items.Clear();
                    btnMachineConfig.CssClass = "btn btn-primary btn-block btn-flat";
                    btnupdate.Enabled = true;
                    btnMachineConfig.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void GV_MachineConfig_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GV_MachineConfig.PageIndex = e.NewPageIndex;
                ShowGridData();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
    }
}