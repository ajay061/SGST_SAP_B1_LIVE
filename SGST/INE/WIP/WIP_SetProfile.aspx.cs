using Common;
using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using BusinessLayer.WIP;
using System.Web.UI;

namespace SGST.INE.WIP
{
    public partial class WIP_SetProfile : System.Web.UI.Page
    {
        BL_SetProfile blobj = new BL_SetProfile();
        DataTable dt = new DataTable();
        string Message = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["usertype"].ToString() != "ADMIN")
                {
                    string _strRights = CommonHelper.GetRights("WIP SET FMS PROGRAM", (DataTable)Session["USER_RIGHTS"]);
                    CommonHelper._strRights = _strRights.Split('^');
                    if (CommonHelper._strRights[0] == "0")  //Check view rights
                    {
                        Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                    }
                }
                if (!IsPostBack)
                {
                    Get_Lineid();
                    ShowGridData();
                    GET_FGITEMCODE();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        public void GET_FGITEMCODE()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                blobj = new BL_SetProfile();
                ddlFgItemCode.Items.Clear();
                DataTable dt = blobj.BindFGITEMCOE();
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(ddlFgItemCode, dt, true);
                    ddlFgItemCode.SelectedIndex = 0;
                    ddlFgItemCode.Focus();
                }
                else
                {
                    ddlFgItemCode.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        public void Get_Lineid()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                blobj = new BL_SetProfile();                
                DataTable dt = blobj.BindLineId();
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(ddlLineId, dt, true);
                    ddlLineId.SelectedIndex = 0;
                    ddlLineId.Focus();
                }
                else
                {
                    ddlLineId.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }   
        public void GetMachineType()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (ddlLineId.SelectedIndex > 0)
                {
                    blobj = new BL_SetProfile();
                    drpMachineType.Items.Clear();
                    DataTable dt = blobj.BindMachineType(ddlLineId.Text);
                    if (dt.Rows.Count > 0)
                    {
                        clsCommon.FillMachine(drpMachineType, dt, true);
                        drpMachineType.SelectedIndex = 0;
                        drpMachineType.Focus();
                    }
                    else
                    {
                        drpMachineType.Items.Clear();
                    }
                }
                else
                {
                    drpMachineType.Items.Clear();
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
                blobj = new BL_SetProfile();
                DataTable dt = blobj.GetSetProgramDetails();
                lblNumberofRecords.Text = dt.Rows.Count.ToString();
                if (dt.Rows.Count > 0)
                {
                    GV_SetProfile.DataSource = dt;
                    GV_SetProfile.DataBind();
                }
                else
                {
                    GV_SetProfile.DataSource = null;
                    GV_SetProfile.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void ddlLineId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetMachineType();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (ddlFgItemCode.SelectedIndex == 0 && ddlFgItemCode.Enabled == true)
                {
                    CommonHelper.ShowMessage("Please select fg item code", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlFgItemCode.Focus();
                    return;
                }
                if (ddlLineId.SelectedIndex == 0 && ddlLineId.Enabled == true)
                {
                    CommonHelper.ShowMessage("Please select line", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlLineId.Focus();
                    return;
                }
                if (drpMachineType.SelectedIndex == 0 && drpMachineType.Enabled == true)
                {
                    CommonHelper.ShowMessage("Please select machine type", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpMachineType.Focus();
                    return;
                }

                blobj = new BL_SetProfile();
                string sResult = blobj.SaveProfileDetails(ddlLineId.SelectedItem.Text.Trim()
                    , ddlFgItemCode.SelectedItem.Text.Trim()
                    , Session["userid"].ToString(), drpMachineType.SelectedItem.Text.Trim()
                    );
                if (sResult.Contains('~'))
                {
                    Message = sResult.Split('~')[1].ToString();
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtMessage, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, Message);
                    if (sResult.ToUpper().StartsWith("SUCCESS~"))
                    {
                        CommonHelper.ShowMessage(Message, msgsuccess, CommonHelper.MessageType.Success.ToString());
                        ddlFgItemCode.SelectedIndex = 0;
                        ddlLineId.SelectedIndex = 0;
                        drpMachineType.Items.Clear();
                        hidMPID.Value = "";
                    }
                    else if (sResult.ToUpper().StartsWith("N~"))
                    {
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                        ddlFgItemCode.SelectedIndex = 0;
                        ddlLineId.SelectedIndex = 0;
                        drpMachineType.Items.Clear();
                        hidMPID.Value = "";
                    }
                    else if (sResult.StartsWith("ERROR~"))
                    {
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                        ddlFgItemCode.SelectedIndex = 0;
                        ddlLineId.SelectedIndex = 0;
                        drpMachineType.Items.Clear();
                        hidMPID.Value = "";
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
        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                ddlFgItemCode.SelectedIndex = 0;
                ddlLineId.SelectedIndex = 0;
                drpMachineType.Items.Clear();
                hidMPID.Value = "";
                ddlFgItemCode.Enabled = true;
                ddlLineId.Enabled = true;
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void GV_SetProfile_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_SetProfile.PageIndex = e.NewPageIndex;
            this.ShowGridData();
        }
        protected void GV_SetProfile_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (e.CommandName == "DeleteRecord")
                {
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    string sMPID = gvr.Cells[0].Text;
                    string sLineID = gvr.Cells[2].Text;
                    string sFGItemCode = gvr.Cells[1].Text;
                    string sMachineType = gvr.Cells[3].Text;
                    blobj = new BL_SetProfile();
                    string sResult = blobj.DeleteProgram(
                        sMPID, sLineID, sMachineType, sFGItemCode
                                          );
                    if (sResult.Contains('~'))
                    {
                        Message = sResult.Split('~')[1].ToString();
                        CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtMessage, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, Message);
                        if (sResult.ToUpper().StartsWith("SUCCESS~"))
                        {
                            CommonHelper.ShowMessage(Message, msgsuccess, CommonHelper.MessageType.Success.ToString());
                            ddlFgItemCode.SelectedIndex = 0;
                            ddlLineId.SelectedIndex = 0;                           
                            drpMachineType.Items.Clear();
                            hidMPID.Value = "";
                            this.ShowGridData();
                        }
                        else if (sResult.ToUpper().StartsWith("N~"))
                        {
                            CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                            ddlFgItemCode.SelectedIndex = 0;
                            ddlLineId.SelectedIndex = 0;
                            drpMachineType.Items.Clear();
                            hidMPID.Value = "";
                            this.ShowGridData();
                            CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, Message);
                        }
                        else if (sResult.StartsWith("ERROR~"))
                        {
                            CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                            ddlFgItemCode.SelectedIndex = 0;
                            ddlLineId.SelectedIndex = 0;
                            drpMachineType.Items.Clear();
                            hidMPID.Value = "";
                            this.ShowGridData();
                            CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, Message);
                        }
                    }
                    else
                    {
                        CommonHelper.ShowMessage("No response found, Please try again", msgerror, CommonHelper.MessageType.Error.ToString());
                    }
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