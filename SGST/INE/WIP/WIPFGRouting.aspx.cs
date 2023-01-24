using Common;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer.WIP;

namespace SGST.INE.WIP
{
    public partial class FGRouting : System.Web.UI.Page
    {
        BL_FGRouting blobj = new BL_FGRouting();
        DataTable dt = new DataTable();
        string Message = string.Empty;
        int i = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["usertype"].ToString() != "ADMIN")
                {
                    string _strRights = CommonHelper.GetRights("WIP FG ROUTING", (DataTable)Session["USER_RIGHTS"]);
                    CommonHelper._strRights = _strRights.Split('^');
                    if (CommonHelper._strRights[0] == "0")  //Check view rights
                    {
                        Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                    }
                }
                if (!IsPostBack)
                {
                    Get_Lineid();
                    GET_FGITEMCODE();
                    value();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        public void value()
        {
            while (i != 51)
            {
                ddlSequence.Items.Add(i.ToString());
                ddlReworkSequence.Items.Add(i.ToString());
                i++;
            }
        }
        public void Get_Lineid()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                blobj = new BL_FGRouting();
                string sResult = string.Empty;
                DataTable dt = blobj.BindLineId();
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(ddlLineId, dt, true);
                    ddlLineId.SelectedIndex = 0;
                    ddlLineId.Focus();
                    return;
                }
                else
                {
                    CommonHelper.ShowMessage("No Line Found", msgerror, CommonHelper.MessageType.Error.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        public void GET_FGITEMCODE()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                blobj = new BL_FGRouting();
                string sResult = string.Empty;
                DataTable dt = blobj.BindFGITEMCOE();
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(ddlFgItemCode, dt, true);
                    ddlFgItemCode.SelectedIndex = 0;
                    ddlFgItemCode.Focus();
                    return;
                }
                else
                {
                    CommonHelper.ShowMessage("No Fg item code found", msgerror, CommonHelper.MessageType.Error.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        public void GetMachineId()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                blobj = new BL_FGRouting();
                string sResult = string.Empty;
                DataTable dt = blobj.BindMachineId(ddlLineId.Text);
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillMachine(ddlMachineid, dt, true);
                    ddlMachineid.SelectedIndex = 0;
                    ddlMachineid.Focus();
                    return;
                }
                else
                {
                    CommonHelper.ShowMessage("No machine found against selected line", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlLineId.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        public void GETProfileId()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                ddlProfileid.Items.Clear();
                blobj = new BL_FGRouting();
                string sResult = string.Empty;
                DataTable dt = blobj.BindPROFILEID(ddlMachineid.SelectedValue.ToString());
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(ddlProfileid, dt, true);
                    ddlProfileid.SelectedIndex = 0;
                    ddlProfileid.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        private void ShowGridData()
        {
            try
            {
                blobj = new BL_FGRouting();
                DataTable dt = blobj.GetRoutingDetails(ddlFgItemCode.Text, txtRouteName.Text);
                lblNumberofRecords.Text = dt.Rows.Count.ToString();
                if (dt.Rows.Count > 0)
                {
                    GV_Routing.DataSource = dt;
                    GV_Routing.DataBind();
                }
                else
                {
                    GV_Routing.DataSource = null;
                    GV_Routing.DataBind();
                }

            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        private void EditRecords(string sFGItemCode, string sMachineID, int iSequence, string sRouteName)
        {
            try
            {
                blobj = new BL_FGRouting();
                DataTable dt = blobj.EditRoutingDetails(sFGItemCode, sMachineID, iSequence, sRouteName);
                if (dt.Rows.Count > 0)
                {
                    ddlFgItemCode.Text = dt.Rows[0]["FG_ITEM_CODE"].ToString();
                    ddlLineId.Text = dt.Rows[0]["LINEID"].ToString();
                    GetMachineId();
                    ddlMachineid.SelectedValue = dt.Rows[0]["MACHINEID"].ToString();
                    GETProfileId();
                    if (dt.Rows[0]["PROFILE_ID"].ToString().Length > 0)
                    {
                        ddlProfileid.SelectedValue = dt.Rows[0]["PROFILE_ID"].ToString();
                    }
                    else
                    {
                        ddlProfileid.SelectedIndex = 0;
                    }                   
                    ddlSequence.Text = dt.Rows[0]["SEQ"].ToString();
                    ddlReworkSequence.Text = dt.Rows[0]["REWORK_SEQ"].ToString();
                    txtTMOPartCode.Text = dt.Rows[0]["TMO_PARTCODE"].ToString();
                    if (dt.Rows[0]["IS_SFG"].ToString() == "1")
                    {
                        chkISSFG.Checked = true;
                        SFGQty.Text= dt.Rows[0]["SFGQty"].ToString();
                    }
                    else
                    {
                        chkISSFG.Checked = false;
                    }
                    if (dt.Rows[0]["ENABLE"].ToString() == "1")
                    {
                        chkIsEnable.Checked = true;
                    }
                    else
                    {
                        chkIsEnable.Checked = false;
                    }
                    if (dt.Rows[0]["OUT_SCAN_REQ"].ToString() == "1")
                    {
                        ckhOutScanReq.Checked = true;
                    }
                    else
                    {
                        ckhOutScanReq.Checked = false;
                    }
                    if (dt.Rows[0]["IS_LOTCREATE"].ToString() == "1")
                    {
                        chkISLotCreate.Checked = true;
                    }
                    else
                    {
                        chkISLotCreate.Checked = false;
                    }
                    if (dt.Rows[0]["ISAUTOXRAYSAMPLING"].ToString() == "1")
                    {
                        chkIsAutoSampledPic.Checked = true;
                    }
                    else
                    {
                        chkIsAutoSampledPic.Checked = false;
                    }
                    txtSFGItemCode.Text = dt.Rows[0]["SFG_ITEM_CODE"].ToString();
                    ddlFgItemCode.CssClass = "form-control select2";
                    ddlLineId.CssClass = "form-control select2";
                    ddlMachineid.CssClass = "form-control select2";
                    ddlProfileid.CssClass = "form-control select2";
                    ddlSequence.CssClass = "form-control select2";
                    ddlReworkSequence.CssClass = "form-control select2";
                    txtSFGItemCode.CssClass = "form-control";
                    chkISSFG.Enabled = false;
                    chkISSFG.CssClass = "form-control";
                    chkISLotCreate.CssClass = "form-control";
                    ckhOutScanReq.CssClass = "form-control";
                    chkIsAutoSampledPic.CssClass = "form-control";
                    chkIsEnable.CssClass = "form-control";
                    ddlFgItemCode.Enabled = false;
                    ddlMachineid.Enabled = false;
                    ddlLineId.Enabled = false;
                    ddlProfileid.Enabled = false;
                    ddlSequence.Enabled = false;
                    ddlReworkSequence.Enabled = false;
                    if (btnSave.Text == "Save")
                    { btnSave.Text = "Update"; }
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
        }


        protected void ddlLineId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlProfileid.Items.Clear();
                ddlMachineid.Items.Clear();
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (ddlLineId.SelectedIndex > 0)
                {
                    GetMachineId();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowMessage(ex.Message, msgerror, CommonHelper.MessageType.Error.ToString());
            }

        }
        protected void ddlMachineid_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlProfileid.Items.Clear();
                if (ddlMachineid.SelectedIndex > 0)
                {
                    GETProfileId();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowMessage(ex.Message, msgerror, CommonHelper.MessageType.Error.ToString());
            }

        }
        protected void ddlFgItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlRoute.DataSource = null;
                ddlRoute.Items.Clear();
                if (ddlFgItemCode.SelectedIndex > 0)
                {
                    blobj = new BL_FGRouting();
                    string sResult = string.Empty;
                    DataTable dt = blobj.GetRouteName(ddlFgItemCode.SelectedValue.ToString());
                    if (dt.Rows.Count > 0)
                    {
                        clsCommon.FillComboBox(ddlRoute, dt, true);
                        return;
                    }
                    else
                    {
                        CommonHelper.ShowMessage("No route details found", msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                string sResult = string.Empty;
                string sIsAutoxraySampledPIC = "0";
                if (btnSave.Text == "Save")
                {
                    
                    if (ddlFgItemCode.SelectedIndex == 0)
                    {
                        CommonHelper.ShowMessage("Please select FG Item Code", msgerror, CommonHelper.MessageType.Error.ToString());
                        ddlFgItemCode.Focus();
                        return;
                    }
                    else if (string.IsNullOrEmpty(txtRouteName.Text))
                    {
                        CommonHelper.ShowMessage("Please select route name", msgerror, CommonHelper.MessageType.Error.ToString());
                        txtRouteName.Focus();
                        return;
                    }
                    else if (ddlLineId.SelectedIndex == 0)
                    {
                        CommonHelper.ShowMessage("Please select line", msgerror, CommonHelper.MessageType.Error.ToString());
                        ddlLineId.Focus();
                        return;
                    }
                    else if (ddlMachineid.SelectedIndex == 0)
                    {
                        CommonHelper.ShowMessage("Please select machine", msgerror, CommonHelper.MessageType.Error.ToString());
                        ddlMachineid.Focus();
                        return;
                    }
                    else if (ddlMachineid.SelectedIndex == 0)
                    {
                        CommonHelper.ShowMessage("Please select machine", msgerror, CommonHelper.MessageType.Error.ToString());
                        ddlMachineid.Focus();
                        return;
                    }
                    else if (ddlSequence.SelectedIndex == 0)
                    {
                        CommonHelper.ShowMessage("Please select sequence", msgerror, CommonHelper.MessageType.Error.ToString());
                        ddlSequence.Focus();
                        return;
                    }
                    else if (ddlReworkSequence.SelectedIndex == 0)
                    {
                        CommonHelper.ShowMessage("Please select rework sequence", msgerror, CommonHelper.MessageType.Error.ToString());
                        ddlReworkSequence.Focus();
                        return;
                    }
                    string sProfileID = "";
                    string sEnable = "1";
                    string sOutScanReq = "0";
                    string sIsLotCreate = "0";
                    if (ddlProfileid.Items.Count == 0)
                    {
                        sProfileID = "";
                    }
                    else if (ddlProfileid.SelectedIndex == 0)
                    {
                        sProfileID = "";
                    }
                    else if (ddlProfileid.SelectedIndex > 0)
                    {
                        sProfileID = ddlProfileid.Text;
                    }
                    string sISSfg = "0";
                    if (chkISSFG.Checked == true)
                    {
                        sISSfg = "1";
                    }
                    if (chkISSFG.Checked == true)
                    {
                        if (string.IsNullOrEmpty(SFGQty.Text.Trim()))
                        {
                            CommonHelper.ShowMessage("Please enter SFG Qty", msgerror, CommonHelper.MessageType.Error.ToString());
                            SFGQty.Focus();
                            return;
                        }
                    }
                    if (chkIsEnable.Checked == false)
                    {
                        sEnable = "0";
                    }
                    if (ckhOutScanReq.Checked == true)
                    {
                        sOutScanReq = "1";
                    }
                    if (chkISLotCreate.Checked == true)
                    {
                        sIsLotCreate = "1";
                    }
                    
                    if (chkIsAutoSampledPic.Checked == true)
                    {
                        sIsAutoxraySampledPIC = "1";
                    }
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtInfo,
                     System.Reflection.MethodBase.GetCurrentMethod().Name, "Route for fg Item Code :" + ddlFgItemCode.SelectedValue.Trim()
                     + " and Program :" + sProfileID
                     +" and Stage : " + ddlSequence.SelectedItem.Text.Trim()
                     +" Save By : " + Session["userid"].ToString()
                     +" ,Route Name:"+ txtRouteName.Text
                     );
                    blobj = new BL_FGRouting();
                    sResult = blobj.GenerateRouting(
                        ddlLineId.SelectedItem.Text.Trim(), ddlMachineid.SelectedValue.Trim(),
                         ddlFgItemCode.SelectedValue.Trim(), "1",
                         sProfileID, ddlSequence.SelectedItem.Text.Trim(),
                          ddlReworkSequence.SelectedItem.Text.Trim(), Session["userid"].ToString()
                          , sISSfg, txtSFGItemCode.Text.Trim(), sEnable, sOutScanReq, sIsLotCreate
                          , txtRouteName.Text,txtTMOPartCode.Text, sIsAutoxraySampledPIC, SFGQty.Text
                       );
                }
                else
                {
                    string sProfileID = "";
                    string sEnable = "1";
                    string sOutScanReq = "0";
                    string sIsLotCreate = "0";
                    if (ddlProfileid.Items.Count == 0)
                    {
                        sProfileID = "";
                    }
                    else if (ddlProfileid.SelectedIndex == 0)
                    {
                        sProfileID = "";
                    }
                    else if (ddlProfileid.SelectedIndex > 0)
                    {
                        sProfileID = ddlProfileid.Text;
                    }
                    string sISSfg = "0";
                    if (chkISSFG.Checked == true)
                    {
                        sISSfg = "1";
                    }
                    if (chkIsEnable.Checked == false)
                    {
                        sEnable = "0";
                    }
                    if (ckhOutScanReq.Checked == true)
                    {
                        sOutScanReq = "1";
                    }
                    if (chkISLotCreate.Checked == true)
                    {
                        sIsLotCreate = "1";
                    }
                    if (chkIsAutoSampledPic.Checked == true)
                    {
                        sIsAutoxraySampledPIC = "1";
                    }
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtInfo,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, "Route for fg Item Code :" + ddlFgItemCode.SelectedValue.Trim()
                    + " and Program :" + sProfileID
                    + " and Stage : " + ddlSequence.SelectedItem.Text.Trim()
                    + " Save By : " + Session["userid"].ToString()
                    + " ,Route Name:" + txtRouteName.Text
                    );
                    blobj = new BL_FGRouting();
                    sResult = blobj.UPDATEROUTING(
                        ddlLineId.SelectedItem.Text.Trim(), ddlMachineid.SelectedValue.Trim(),
                         ddlFgItemCode.SelectedValue.Trim(),
                         sProfileID, ddlSequence.SelectedItem.Text.Trim(), "1",
                          ddlReworkSequence.SelectedItem.Text.Trim(), Session["userid"].ToString()
                         , sEnable, sOutScanReq, sISSfg, txtSFGItemCode.Text.Trim(), sIsLotCreate
                         , txtRouteName.Text, txtTMOPartCode.Text, sIsAutoxraySampledPIC
                       );
                }
                if (sResult.ToUpper().StartsWith("SUCCESS~"))
                {
                    Message = sResult.Split('~')[1];
                    ddlLineId.SelectedIndex = 0;
                    ddlProfileid.Items.Clear();
                    ddlMachineid.Items.Clear();
                    ddlSequence.SelectedIndex = 0;
                    ddlReworkSequence.SelectedIndex = 0;
                    txtSFGItemCode.Text = string.Empty;
                    chkISSFG.Checked = false;
                    chkIsEnable.Checked = false;
                    chkISLotCreate.Checked = false;
                    CommonHelper.ShowMessage(Message, msgsuccess, CommonHelper.MessageType.Success.ToString());
                    ShowGridData();
                    chkISSFG.Enabled = true;
                    ddlFgItemCode.Enabled = true;
                    ddlMachineid.Enabled = true;
                    ddlLineId.Enabled = true;
                    ddlProfileid.Enabled = true;
                    ddlSequence.Enabled = true;
                    ddlReworkSequence.Enabled = true;
                    ckhOutScanReq.Checked = false;
                    txtTMOPartCode.Text = string.Empty;
                    chkIsAutoSampledPic.Checked = false;
                    btnSave.Text = "Save";
                }
                else if (sResult.StartsWith("MachineAlreadyExist~"))
                {
                    Message = sResult.Split('~')[1];
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlProfileid.Items.Clear();
                    ddlMachineid.SelectedIndex = 0;
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, Message);
                }
                else if (sResult.StartsWith("SeqAlreadyExist~"))
                {
                    Message = sResult.Split('~')[1];
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlSequence.SelectedIndex = 0;
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, Message);
                }
                else
                {
                    Message = sResult.Split('~')[1];
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlLineId.SelectedIndex = 0;
                    ddlMachineid.SelectedIndex = 0;
                    ddlFgItemCode.SelectedIndex = 0;
                    //ddlUpdateouttime.SelectedIndex = 0;
                    if (ddlProfileid.Items.Count > 0)
                    {
                        ddlProfileid.SelectedIndex = 0;
                    }
                    ddlSequence.SelectedIndex = 0;
                    ddlReworkSequence.SelectedIndex = 0;
                    txtSFGItemCode.Text = string.Empty;
                    chkISSFG.Checked = false;
                    chkIsEnable.Checked = false;
                    chkISSFG.Enabled = true;
                    ddlFgItemCode.Enabled = true;
                    ddlMachineid.Enabled = true;
                    ddlLineId.Enabled = true;
                    ddlProfileid.Enabled = true;
                    ddlSequence.Enabled = true;
                    ddlReworkSequence.Enabled = true;
                    ckhOutScanReq.Checked = false;
                    chkISLotCreate.Checked = false;
                    btnSave.Text = "Save";
                    txtTMOPartCode.Text = string.Empty;
                    chkIsAutoSampledPic.Checked = false;
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, Message);
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
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                ddlLineId.SelectedIndex = 0;
                ddlMachineid.Items.Clear();
                ddlProfileid.Items.Clear();
                ddlFgItemCode.SelectedIndex = 0;
                //ddlUpdateouttime.SelectedIndex = 0;                
                ddlSequence.SelectedIndex = 0;
                ddlReworkSequence.SelectedIndex = 0;
                txtSFGItemCode.Text = string.Empty;
                chkISSFG.Checked = false;
                SFGQty.Enabled = false;
                this.ShowGridData();
                ddlLineId.Focus();
                chkIsEnable.Checked = false;
                chkISSFG.Enabled = true;
                ddlFgItemCode.Enabled = true;
                ddlMachineid.Enabled = true;
                ddlLineId.Enabled = true;
                ddlProfileid.Enabled = true;
                ddlSequence.Enabled = true;
                ddlReworkSequence.Enabled = true;
                ckhOutScanReq.Checked = false;
                btnSave.Text = "Save";
                chkISLotCreate.Checked = false;
                txtTMOPartCode.Text = string.Empty;
                chkIsAutoSampledPic.Checked = false;
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (ddlFgItemCode.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select FG Item Code", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlFgItemCode.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtRouteName.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please enter route name", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtRouteName.Focus();
                    return;
                }
                blobj = new BL_FGRouting();
                string sResult = blobj.DELETECOMPLETEROUTING(
                    ddlFgItemCode.SelectedItem.Text.Trim(), txtRouteName.Text
                   );
                if (sResult.ToUpper().StartsWith("SUCCESS~"))
                {
                    Message = sResult.Split('~')[1];
                    ddlLineId.SelectedIndex = 0;
                    ddlProfileid.Items.Clear();
                    ddlMachineid.Items.Clear();
                    ddlSequence.SelectedIndex = 0;
                    ddlReworkSequence.SelectedIndex = 0;
                    CommonHelper.ShowMessage(Message, msgsuccess, CommonHelper.MessageType.Success.ToString());
                    ShowGridData();
                }
                else if (sResult.StartsWith("MachineAlreadyExist~"))
                {
                    Message = sResult.Split('~')[1];
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlProfileid.Items.Clear();
                    ddlMachineid.SelectedIndex = 0;
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, Message);
                }
                else if (sResult.StartsWith("SeqAlreadyExist~"))
                {
                    Message = sResult.Split('~')[1];
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlSequence.SelectedIndex = 0;
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, Message);
                }
                else
                {
                    Message = sResult.Split('~')[1];
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlLineId.SelectedIndex = 0;
                    ddlMachineid.SelectedIndex = 0;
                    ddlFgItemCode.SelectedIndex = 0;
                    txtRouteName.Text = string.Empty;
                    if (ddlProfileid.Items.Count > 0)
                    {
                        ddlProfileid.SelectedIndex = 0;
                    }
                    ddlSequence.SelectedIndex = 0;
                    ddlReworkSequence.SelectedIndex = 0;
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, Message);
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void GV_Routing_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteRecords")
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                try
                {
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    int rowIndex = gvr.RowIndex;
                    int Id = Convert.ToInt32(gvr.Cells[0].Text);
                    blobj = new BL_FGRouting();
                    string sResult = blobj.DELETEROUTING(Id);
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
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                }
                finally
                {
                    ShowGridData();
                }
            }
            else if (e.CommandName == "EditRecords")
            {
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                int rowIndex = gvr.RowIndex;
                string sFGItemCode = Convert.ToString(gvr.Cells[3].Text);
                string sMachineID = Convert.ToString(gvr.Cells[2].Text);
                int iSeq = Convert.ToInt32(gvr.Cells[5].Text);
                string sRouteName = Convert.ToString(gvr.Cells[14].Text);
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                EditRecords(sFGItemCode, sMachineID, iSeq, sRouteName);
            }
        }
        protected void GV_Routing_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_Routing.PageIndex = e.NewPageIndex;
            this.ShowGridData();
        }



        protected void btnDownloadData_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                blobj = new BL_FGRouting();
                DataTable dt = blobj.GetRoutingDetails(ddlFgItemCode.Text, txtRouteName.Text);
                if (dt.Rows.Count > 0)
                {
                    dt.Columns.RemoveAt(0);
                    dt.AcceptChanges();
                    dt.Columns.RemoveAt(0);
                    dt.AcceptChanges();
                    dt.Columns.Remove("CREATED_ON");
                    dt.Columns.Remove("CREATED_BY");
                    dt.Columns["PROFILE_ID"].ColumnName = "PROGRAMID";
                    dt.Columns["OUT_SCAN_REQ"].ColumnName = "OUT_SCAN_REQUIRED";
                    dt.AcceptChanges();
                    string sData = PCommon.ToCSV(dt);
                    Response.Clear();
                    Response.Buffer = true;
                    string myName = Server.UrlEncode("RoutingData" + "_" + DateTime.Now.ToShortDateString() + ".csv");
                    Response.AddHeader("content-disposition", "attachment;filename=" + myName);
                    Response.Charset = "";
                    Response.ContentType = "application/text";
                    Response.Output.Write(sData);
                    Response.Flush();
                    Response.End();
                }
                else
                {
                    CommonHelper.ShowMessage("Please select FG Item Code", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlFgItemCode.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void txtRouteName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ShowGridData();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void ddlRoute_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtRouteName.Text = string.Empty;
                if (ddlFgItemCode.SelectedIndex > 0)
                {
                    if (ddlRoute.SelectedIndex > 0)
                    {
                        txtRouteName.Text = ddlRoute.SelectedValue.ToString();
                        ShowGridData();
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }

        }

        protected void btnGetRecord_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtRouteName.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please enter or select route name", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlMachineid.Focus();
                    return;
                }
                if (ddlRoute.SelectedIndex > 0)
                {
                    ShowGridData();
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