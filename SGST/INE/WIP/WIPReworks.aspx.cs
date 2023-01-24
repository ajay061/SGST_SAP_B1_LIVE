using Common;
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using BusinessLayer;

namespace SGST.INE.WIP
{
    public partial class WIPReworks : System.Web.UI.Page
    {
        BL_Rework blobj = new BL_Rework();
        string Message = "";
        static DataTable dtAdd;
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
                    string _strRights = CommonHelper.GetRights("WIP REWORK", (DataTable)Session["USER_RIGHTS"]);
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
                    BindStationID();
                    dtAdd = new DataTable();
                    AddColumns();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void BindStationID()
        {
            try
            {
                blobj = new BL_Rework();
                drpstation.Items.Clear();
                string sResult = string.Empty;
                DataTable dt = blobj.BindReWorkStationID(out sResult, Session["SiteCode"].ToString());
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(drpstation, dt, true);
                    drpstation.SelectedIndex = 0;
                    drpstation.Focus();
                }
            }
            catch (Exception ex)
            {
                //CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        private void BindDefect(string sMachineID)
        {
            try
            {
                blobj = new BL_Rework();
                string sResult = string.Empty;
                DataTable dt = blobj.BindDefectMaster(out sResult, "WIP", sMachineID
                    , Session["SiteCode"].ToString()
                    );
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(drpDefect, dt, true);
                    drpDefect.SelectedIndex = 0;
                    drpDefect.Focus();
                }
            }
            catch (Exception ex)
            {
                //CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        private void ValidateJIOFG(string sfgItemCode)
        {
            try
            {
                dvJIOFGList.Visible = false;
                blobj = new BL_Rework();
                string sResult = string.Empty;
                DataTable dt = blobj.ValidateJioFG(sfgItemCode, Session["SiteCode"].ToString()
                    );
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString().StartsWith("SUCCESS"))
                    {
                        dvJIOFGList.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        protected void ClearControl()
        {
            txtPCBID.Text = string.Empty;
            drpstation.SelectedIndex = 0;
            drpType.SelectedIndex = 0;
            txtRemarks.Text = string.Empty;
            txtobservation.Text = string.Empty;
            drpDefect.Items.Clear();
            drpAction.SelectedIndex = 0;
            gvFailedPCB.DataSource = null;
            gvFailedPCB.DataBind();
            divOut.Visible = false;
            btnSave.Visible = false;
            dvJIOFGList.Visible = false;
        }

        protected void txtPCBID_TextChanged(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (drpType.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select type", msginfo, CommonHelper.MessageType.Info.ToString());
                    drpType.Focus();
                    txtPCBID.Text = String.Empty;
                    return;
                }
                else if (drpstation.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select station", msginfo, CommonHelper.MessageType.Info.ToString());
                    drpstation.Focus();
                    txtPCBID.Text = String.Empty;
                    return;
                }
                else if (string.IsNullOrEmpty(txtPCBID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan PCB barcode", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtPCBID.Focus(); return;
                }
                else
                {
                    blobj = new BL_Rework();
                    string sResult = blobj.checkandSave(
                        txtPCBID.Text.Trim(), drpstation.SelectedItem.Value.Trim(),
                        drpType.SelectedItem.Value.Trim(), Session["UserID"].ToString()
                        , Session["SiteCode"].ToString(), Session["LINECODE"].ToString());
                    if (sResult.ToUpper().StartsWith("SUCCESS~"))
                    {
                        Message = sResult.Split('~')[1].ToString();
                        if (drpType.Text == "2")
                        {
                            gvFailedPCB.DataSource = null;
                            gvFailedPCB.DataBind();
                            DataTable dtRejectedPCB = blobj.BindRejectedPCB(out sResult, txtPCBID.Text.Trim()
                                , Session["SiteCode"].ToString()
                                );
                            if (dtRejectedPCB.Rows.Count > 0)
                            {
                                Response.Write("<script LANGUAGE='JavaScript' >alert('Press save button to store the data')</script>");
                                gvFailedPCB.Visible = true;
                                gvFailedPCB.DataSource = dtRejectedPCB;
                                gvFailedPCB.DataBind();
                                string sMachineID = dtRejectedPCB.Rows[0]["MACHINEID"].ToString();
                                string sFGItemCOde = dtRejectedPCB.Rows[0]["FG_ITEM_CODE"].ToString();
                                BindDefect(sMachineID);
                                ValidateJIOFG(sFGItemCOde);
                            }
                        }
                        else
                        {
                            CommonHelper.ShowMessage(Message, msgsuccess, CommonHelper.MessageType.Success.ToString());
                            DataTable dtRejectedPCB = blobj.BindRejectedPCB(out sResult, txtPCBID.Text.Trim()
                                , Session["SiteCode"].ToString()
                                );
                            if (dtRejectedPCB.Rows.Count > 0)
                            {
                                gvFailedPCB.Visible = true;
                                gvFailedPCB.DataSource = dtRejectedPCB;
                                gvFailedPCB.DataBind();
                            }
                            txtPCBID.Text = string.Empty;
                            txtPCBID.Focus();
                        }
                        return;
                    }
                    else if (sResult.ToUpper().StartsWith("ERROR~"))
                    {
                        Message = sResult.Split('~')[1].ToString();
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                        txtPCBID.Text = string.Empty;
                        txtPCBID.Focus();
                    }
                    else if (sResult.ToUpper().StartsWith("N~"))
                    {
                        Message = sResult.Split('~')[1].ToString();
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                        txtPCBID.Text = string.Empty;
                        txtPCBID.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                txtPCBID.Text = string.Empty;
                txtPCBID.Focus();
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearControl();
        }

        protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpType.SelectedIndex == 1)
                {
                    divOut.Visible = false;
                    btnSave.Visible = false;
                    dvJIOReportType.Visible = false;
                    dvJIOFGList.Visible = false;
                }
                else
                {
                    dvJIOReportType.Visible = true;
                    divOut.Visible = true;
                    btnSave.Visible = true;
                }
                drpDefect.Items.Clear();
                drpAction.SelectedIndex = 0;
                txtobservation.Text = string.Empty;
                txtRemarks.Text = string.Empty;
                txtPCBID.Text = string.Empty;
                gvFailedPCB.DataSource = null;
                gvFailedPCB.DataBind();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
        }
        protected void drpRepairType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dvJIOFGList.Visible = false;
                if (drpRepairType.SelectedIndex > 0)
                {
                    dvJIOFGList.Visible = true;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (drpType.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select type", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpType.Focus(); return;
                }
                else if (drpstation.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select station", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpstation.Focus(); return;
                }
                else if (string.IsNullOrEmpty(txtPCBID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan PCB barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPCBID.Focus(); return;
                }
                else if (string.IsNullOrEmpty(txtPCBID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan PCB barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPCBID.Focus(); return;
                }
                else if (drpDefect.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select defect", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpDefect.Focus(); return;
                }
                else if (string.IsNullOrEmpty(txtobservation.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please enter observation", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtobservation.Focus(); return;
                }

                else if (string.IsNullOrEmpty(txtRemarks.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please enter remarks", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtRemarks.Focus(); return;
                }
                else if (dtAdd.Rows.Count == 0)
                {
                    CommonHelper.ShowMessage("Please enter atleast one component details or else enter NA", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtLocation.Focus(); return;
                }
                else
                {
                    string sJIOMoveStage = string.Empty;
                    string sJIORepairType = string.Empty;
                    if (dvJIOReportType.Visible == true)
                    {
                        sJIORepairType = drpRepairType.Text;
                    }
                    if (dvJIOFGList.Visible == true)
                    {
                        sJIOMoveStage = drpMovingStage.Text;
                    }
                    blobj = new BL_Rework();
                    string sResult = blobj.RepairOut(
                        txtPCBID.Text.Trim(), drpstation.SelectedItem.Value.Trim(), txtobservation.Text.Trim(),
                        txtRemarks.Text.Trim(), drpType.SelectedItem.Value.Trim(), Session["UserID"].ToString()
                        , drpDefect.Text, drpAction.SelectedValue.ToString(), ""
                        , Session["SiteCode"].ToString(), Session["LINECODE"].ToString(), dtAdd
                        , sJIOMoveStage, sJIORepairType
                        );
                    if (sResult.ToUpper().StartsWith("SUCCESS~"))
                    {
                        dtAdd.Rows.Clear();
                        Message = sResult.Split('~')[1].ToString();
                        CommonHelper.ShowMessage(Message, msgsuccess, CommonHelper.MessageType.Success.ToString());
                        if (drpType.Text == "2")
                        {
                            gvFailedPCB.DataSource = null;
                            gvFailedPCB.DataBind();
                            drpDefect.Items.Clear();
                            txtobservation.Text = string.Empty;
                            txtRemarks.Text = string.Empty;
                            dvJIOFGList.Visible = false;
                            dvJIOReportType.Visible = false;
                            drpType.SelectedIndex = 0;
                        }
                        else
                        {
                            DataTable dtRejectedPCB = blobj.BindRejectedPCB(out sResult, txtPCBID.Text.Trim()
                                , Session["SiteCode"].ToString()
                                );
                            if (dtRejectedPCB.Rows.Count > 0)
                            {
                                gvFailedPCB.Visible = true;
                                gvFailedPCB.DataSource = dtRejectedPCB;
                                gvFailedPCB.DataBind();
                            }
                        }
                        txtPCBID.Text = string.Empty;
                        txtPCBID.Focus();
                        return;
                    }
                    else if (sResult.ToUpper().StartsWith("ERROR~"))
                    {
                        Message = sResult.Split('~')[1].ToString();
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    else if (sResult.ToUpper().StartsWith("N~"))
                    {
                        Message = sResult.Split('~')[1].ToString();
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
        }

        protected void gvRepairData_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            try
            {
                gvRepairData.PageIndex = e.NewPageIndex;
                gvRepairData.DataSource = dtAdd;
                gvRepairData.DataBind();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void gvRepairData_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                string _SN = string.Empty;
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                _SN = gvr.Cells[2].Text.Replace("&nbsp;", " ").Trim();
                if (e.CommandName == "DeleteRecords")
                {
                    foreach (DataRow dr in dtAdd.Rows)
                    {
                        if (dr["FORMAT_NO"].ToString() == _SN.Trim())
                        {
                            dtAdd.Rows.Remove(dr);
                            break;
                        }
                    }
                    dtAdd.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {
                gvRepairData.DataSource = dtAdd;
                gvRepairData.DataBind();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtLocation.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please enter component location", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtLocation.Focus(); return;
                }
                if (string.IsNullOrEmpty(txtPartCode.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please enter component part code", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPartCode.Focus(); return;
                }
                if (string.IsNullOrEmpty(txtPartDesc.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please enter component part desc", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPartDesc.Focus(); return;
                }
                if (string.IsNullOrEmpty(txtActionTaken.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please enter action taken", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtActionTaken.Focus(); return;
                }
                AddNewRow();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void AddColumns()
        {
            DataColumn LOCATION = new DataColumn("LOCATION", typeof(String));
            DataColumn DESCRIPTION = new DataColumn("DESCRIPTION", typeof(String));
            DataColumn COMPARTCODE = new DataColumn("COMPARTCODE", typeof(string));
            DataColumn ACTIONTAKEN = new DataColumn("ACTIONTAKEN", typeof(string));
            dtAdd.Columns.Add(LOCATION);
            dtAdd.Columns.Add(DESCRIPTION);
            dtAdd.Columns.Add(COMPARTCODE);
            dtAdd.Columns.Add(ACTIONTAKEN);
        }
        private void AddNewRow()
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                DataRow dr = dtAdd.NewRow();
                dr["LOCATION"] = txtLocation.Text;
                dr["DESCRIPTION"] = txtPartDesc.Text;
                dr["COMPARTCODE"] = txtPartCode.Text; ;
                dr["ACTIONTAKEN"] = txtActionTaken.Text;
                dtAdd.Rows.Add(dr);
                gvRepairData.DataSource = dtAdd;
                gvRepairData.DataBind();
                txtLocation.Text = string.Empty;
                txtPartDesc.Text = string.Empty;
                txtPartCode.Text = string.Empty;
                txtActionTaken.Text = string.Empty;
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }


    }
}