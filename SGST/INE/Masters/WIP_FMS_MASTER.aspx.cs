using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;
using BusinessLayer.WIP;

namespace SGST.INE.WIP
{
    public partial class WIP_FMS_MASTER : System.Web.UI.Page
    {
        BL_FmsMaster blobj = new BL_FmsMaster();
        string Message = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["usertype"].ToString() != "ADMIN")
                {
                    string _strRights = CommonHelper.GetRights("WIP FMS MASTER", (DataTable)Session["USER_RIGHTS"]);
                    CommonHelper._strRights = _strRights.Split('^');
                    if (CommonHelper._strRights[0] == "0")  //Check view rights
                    {
                        Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                    }
                }

                if (!IsPostBack)
                {
                    Bind_LINEID();
                    Bind_PROCESSID();
                    ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
                    ShowGridData();
                    btnupdate.Enabled = false;
                    btnupdate.CssClass = "btn btn-primary btn-block btn-flat";
                    btnFMMASTER.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        public void Bind_LINEID()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                DataTable dt = new DataTable();
                ddlLineId.Items.Clear();
                ddlMachineid.Items.Clear();
                blobj = new BL_FmsMaster();
                dt = blobj.BIND_LINEID();
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(ddlLineId, dt, true);
                    ddlLineId.SelectedIndex = 0;
                    ddlLineId.Focus();
                }
                else
                {
                    ddlMachineid.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        public void Bind_PROCESSID()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                DataTable dt = new DataTable();
                ddlMachineid.Items.Clear();
                if (ddlLineId.SelectedIndex > 0)
                {
                    blobj = new BL_FmsMaster();
                    dt = blobj.BIND_MACHINEID(ddlLineId.Text.Trim());
                    if (dt.Rows.Count > 0)
                    {
                        clsCommon.FillMachine(ddlMachineid, dt, true);
                        ddlMachineid.SelectedIndex = 0;
                        ddlMachineid.Focus();
                    }
                    else
                    {
                        ddlMachineid.Items.Clear();
                    }
                }

            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }

        private void ShowGridData()
        {
            try
            {
                blobj = new BL_FmsMaster();
                DataTable dt = blobj.GetFMSMASTERDETAILS();
                lblNumberofRecords.Text = dt.Rows.Count.ToString();
                if (dt.Rows.Count > 0)
                {
                    GV_FMSMATER.DataSource = dt;
                    GV_FMSMATER.DataBind();
                }
                else
                {
                    GV_FMSMATER.DataSource = null;
                    GV_FMSMATER.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void btnFMMASTER_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (ddlLineId.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select line", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlLineId.Focus();
                    return;
                }
                else if (ddlMachineid.SelectedIndex == 0 && ddlMachineid.Enabled == true)
                {
                    CommonHelper.ShowMessage("Please select machine", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlMachineid.Focus();
                    return;
                }
                else if (string.IsNullOrEmpty(txtFMS_TOP_IP.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please enter FMS ip", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtFMS_TOP_IP.Focus();
                    return;
                }
                else if (string.IsNullOrEmpty(txt_FMSTOPPORT.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please enter FMS port", msgerror, CommonHelper.MessageType.Error.ToString());
                    txt_FMSTOPPORT.Focus();
                    return;
                }
                else if (ddlFMSTOPIPENABLE.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select fms status", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlFMSTOPIPENABLE.Focus();
                    return;
                }
                else if (ddlLocation.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select fms location", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlLocation.Focus();
                    return;
                }
                blobj = new BL_FmsMaster();
                string sResult = blobj.SaveFMSMasterDetails(
                    ddlMachineid.SelectedValue, txtFMS_TOP_IP.Text, ddlFMSTOPIPENABLE.SelectedItem.Text, ddlLineId.SelectedItem.Text
                    , ddlLocation.SelectedItem.Text,
                   txt_FMSTOPPORT.Text
                    );
                if (sResult.Contains('~'))
                {
                    Message = sResult.Split('~')[1];
                    if (sResult.ToUpper().StartsWith("SUCCESS~"))
                    {
                        txtFMS_TOP_IP.Text = "";
                        txt_FMSTOPPORT.Text = "";
                        ddlFMSTOPIPENABLE.SelectedIndex = 0;
                        ddlLineId.SelectedIndex = 0;
                        ddlLocation.SelectedIndex = 0;
                        CommonHelper.ShowMessage(Message, msgsuccess, CommonHelper.MessageType.Success.ToString());
                    }
                    else
                    {
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                        txtFMS_TOP_IP.Text = "";
                        txt_FMSTOPPORT.Text = "";
                        ddlFMSTOPIPENABLE.SelectedIndex = 0;
                        ddlLineId.SelectedIndex = 0;
                        ddlLocation.SelectedIndex = 0;
                        CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, Message);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {
                ShowGridData();
            }
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (ddlLineId.SelectedIndex == 0 && ddlLineId.Enabled == true)
                {
                    CommonHelper.ShowMessage("Please select line", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlLineId.Focus();
                    return;
                }
                else if (ddlMachineid.SelectedIndex == 0 && ddlMachineid.Enabled == true)
                {
                    CommonHelper.ShowMessage("Please select machine", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlMachineid.Focus();
                    return;
                }
                else if (string.IsNullOrEmpty(txtFMS_TOP_IP.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please enter FMS ip", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtFMS_TOP_IP.Focus();
                    return;
                }
                else if (string.IsNullOrEmpty(txt_FMSTOPPORT.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please enter FMS port", msgerror, CommonHelper.MessageType.Error.ToString());
                    txt_FMSTOPPORT.Focus();
                    return;
                }
                else if (ddlFMSTOPIPENABLE.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select fms status", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlFMSTOPIPENABLE.Focus();
                    return;
                }
                else if (ddlLocation.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select fms location", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlLocation.Focus();
                    return;
                }
                string MACHINE_ID = "";
                if (Session["MCID"] != null)
                {
                    MACHINE_ID = Session["MCID"].ToString();
                }
                else
                {
                    MACHINE_ID = ddlMachineid.SelectedValue.Trim();
                }
                Session["MCID"] = null;
                blobj = new BL_FmsMaster();
                string sResult = blobj.UpdateFMSDetails(
                    MACHINE_ID, txtFMS_TOP_IP.Text, txt_FMSTOPPORT.Text, ddlFMSTOPIPENABLE.SelectedItem.Text, ddlLocation.SelectedItem.Text
                    , ddlLineId.SelectedItem.Text
                    );
                if (sResult.Contains('~'))
                {
                    Message = sResult.Split('~')[1];
                    if (sResult.ToUpper().StartsWith("SUCCESS~"))
                    {
                        GV_FMSMATER.EditIndex = -1;
                        this.ShowGridData();
                        txtFMS_TOP_IP.Text = "";
                        txt_FMSTOPPORT.Text = "";
                        ddlFMSTOPIPENABLE.SelectedIndex = 0;
                        ddlLocation.SelectedIndex = 0;
                        ddlLineId.SelectedIndex = 0;
                        ddlLineId.Enabled = true;
                        ddlMachineid.Enabled = true;
                        CommonHelper.ShowMessage(Message, msgsuccess, CommonHelper.MessageType.Success.ToString());
                    }
                    else
                    {
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                        txtFMS_TOP_IP.Text = "";
                        txt_FMSTOPPORT.Text = "";
                        ddlFMSTOPIPENABLE.SelectedIndex = 0;
                        ddlLineId.SelectedIndex = 0;
                        ddlLocation.SelectedIndex = 0;
                        ddlLineId.Enabled = true;
                        ddlMachineid.Enabled = true;
                        CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, Message);
                    }
                }
                btnFMMASTER.Enabled = true;
                btnupdate.CssClass = "btn btn-primary btn-block btn-flat";
                btnupdate.Enabled = false;
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
                txtFMS_TOP_IP.Text = "";
                txt_FMSTOPPORT.Text = "";
                ddlLineId.SelectedIndex = 0;
                ddlLineId.Enabled = true;
                ddlMachineid.Items.Clear();
                ddlMachineid.Enabled = true;
                ddlFMSTOPIPENABLE.SelectedIndex = 0;
                ddlLocation.SelectedIndex = 0;
                btnupdate.Enabled = false;
                btnupdate.CssClass = "btn btn-primary btn-block btn-flat";
                btnFMMASTER.Enabled = true;
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }       

        protected void ddlLineId_SelectedIndexChanged(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (ddlLineId.SelectedIndex > 0)
                {
                    Bind_PROCESSID();
                }
                else
                {
                    ddlMachineid.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void GV_FMSMATER_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GV_FMSMATER.PageIndex = e.NewPageIndex;
                ShowGridData();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void GV_FMSMATER_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteRecords")
            {
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                int rowIndex = gvr.RowIndex;
                string sIP = gvr.Cells[2].Text;
                string sMachineID = gvr.Cells[0].Text;
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                try
                {
                    blobj = new BL_FmsMaster();
                    string sResult = blobj.DeleteFMS(sIP.ToString(), sMachineID);
                    if (sResult.ToUpper().StartsWith("SUCCESS~"))
                    {
                        Message = sResult.Split('~')[1];
                        CommonHelper.ShowMessage(Message, msgsuccess, CommonHelper.MessageType.Success.ToString());
                    }
                    else
                    {
                        Message = sResult.Split('~')[1];
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                        CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, Message);
                    }
                }
                catch (Exception ex)
                {
                    CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                }
                finally
                {
                    ShowGridData();
                }
            }
            else if (e.CommandName == "EditRecords")
            {
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                EditRecords(gvr);
            }
        }
        private void EditRecords(GridViewRow gvr)
        {
            try
            {
                btnFMMASTER.Enabled = false;
                btnFMMASTER.CssClass = "btn btn-primary btn-block btn-flat";
                btnupdate.Enabled = true;
                ddlLineId.Enabled = false;
                ddlLineId.CssClass = "form-control input-sm";
                ddlMachineid.Enabled = false;
                ddlMachineid.CssClass = "form-control input-sm";
                string MACHINEID = gvr.Cells[0].Text;
                string FMSLINEID = gvr.Cells[1].Text;
                string FMS_IP = gvr.Cells[2].Text;
                string FMS_PORT = gvr.Cells[3].Text;
                string FMSIPENABLE = gvr.Cells[4].Text;
                string FMLOCATION = gvr.Cells[5].Text;
                string MACHINENAME = gvr.Cells[6].Text;
                ddlLineId.SelectedValue = FMSLINEID;
                Bind_PROCESSID();
                ddlMachineid.SelectedValue = MACHINEID;
               
                txtFMS_TOP_IP.Text = FMS_IP;
                txt_FMSTOPPORT.Text = FMS_PORT;
                if (FMSIPENABLE.ToUpper() == "YES")
                {
                    ddlFMSTOPIPENABLE.SelectedValue = "1";
                }
                else if (FMSIPENABLE.ToUpper() == "NO")
                {
                    ddlFMSTOPIPENABLE.SelectedValue = "2";
                }
                if (FMLOCATION.ToUpper() == "TOP")
                {
                    ddlLocation.SelectedValue = "1";
                }
                else if (FMLOCATION.ToUpper() == "BOTTOM")
                {
                    ddlLocation.SelectedValue = "2";
                }
                Session["MCID"] = MACHINEID;
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}