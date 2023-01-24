using Common;
using System;
using BusinessLayer.WIP;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using System.Web.UI;


namespace SGST.INE.WIP
{
    public partial class WIPXraySampling : System.Web.UI.Page
    {
        string Message = "";
        BL_WIP_VIQuality blobj = null;
        static DataTable dtAdd;
        private void AddColumns()
        {
            DataColumn DEFECT = new DataColumn("DEFECT", typeof(String));
            dtAdd.Columns.Add(DEFECT);
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Signin/v1/Login.aspx?Session=Null");
            }
        }
        private void BindFGItemCode()
        {
            try
            {
                drpFGItemCode.Items.Clear();
                txtReelID.Text = string.Empty;
                blobj = new BL_WIP_VIQuality();
                if (txtScanMachineID.Text.Length > 0)
                {
                    string sResult = string.Empty;
                    DataTable dtFGItemCode = blobj.BindXrayFGItemCode(txtScanMachineID.Text.Trim()
                         , Session["SiteCode"].ToString(), Session["LINECODE"].ToString()

                        );
                    if (dtFGItemCode.Rows.Count > 0)
                    {
                        CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                        clsCommon.FillComboBox(drpFGItemCode, dtFGItemCode, true);
                        drpFGItemCode.SelectedIndex = 0;
                        drpFGItemCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void BindDefect()
        {
            try
            {
                drpDefect.Items.Clear();
                string sResult = string.Empty;
                blobj = new BL_WIP_VIQuality();
                DataTable dtStationID = blobj.BindDefect(Session["SiteCode"].ToString());
                if (dtStationID.Rows.Count > 0)
                {
                    CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                    clsCommon.FillComboBox(drpDefect, dtStationID, true);
                    drpDefect.SelectedIndex = 0;
                    drpDefect.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void BindStationID()
        {
            try
            {
                drpstation.Items.Clear();
                string sResult = string.Empty;
                blobj = new BL_WIP_VIQuality();
                DataTable dtStationID = blobj.BindReWorkStationID(
                     Session["SiteCode"].ToString()
                    );
                if (dtStationID.Rows.Count > 0)
                {
                    CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                    clsCommon.FillComboBox(drpstation, dtStationID, true);
                    drpstation.SelectedIndex = 0;
                    drpstation.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usertype"].ToString() != "ADMIN")
            {
                string _strRights = CommonHelper.GetRights("XRAY SAMPLING", (DataTable)Session["USER_RIGHTS"]);
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
                try
                {
                    BindStationID();
                    BindDefect();
                    dtAdd = new DataTable();
                    AddColumns();
                }
                catch (Exception ex)
                {
                    CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                }
            }
        }

        protected void txtScanMachineID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (string.IsNullOrEmpty(txtScanMachineID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Scan Machine", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtScanMachineID.Focus();
                    return;
                }
                blobj = new BL_WIP_VIQuality();
                DataTable dt = blobj.ValidateXrayMachine(txtScanMachineID.Text.Trim(), Session["SiteCode"].ToString(), Session["LINECODE"].ToString());
                if (dt.Rows.Count > 0)
                {
                    CommonHelper.ShowMessage("Valid machine", msgsuccess, CommonHelper.MessageType.Success.ToString());
                    lblMachineName.Text = dt.Rows[0][0].ToString();
                    lblModelNo.Text = dt.Rows[0][2].ToString();
                    BindFGItemCode();
                }
                else
                {
                    CommonHelper.ShowMessage("Invalid machine", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtScanMachineID.Focus();
                    txtScanMachineID.Text = string.Empty;
                    lblMachineName.Text = "";
                    lblModelNo.Text = "";
                    drpFGItemCode.Items.Clear();
                    return;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void txtReelID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (string.IsNullOrEmpty(txtScanMachineID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan machine barcode", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtScanMachineID.Focus();
                    txtScanMachineID.Text = string.Empty;
                    return;
                }
                else if (drpFGItemCode.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select fg item code", msginfo, CommonHelper.MessageType.Info.ToString());
                    drpFGItemCode.Focus(); txtReelID.Text = string.Empty;
                    return;
                }
                else if (string.IsNullOrEmpty(txtReelID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan PCB barcode", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtReelID.Focus();
                    txtReelID.Text = string.Empty;
                    return;
                }
                blobj = new BL_WIP_VIQuality();
                DataTable dt = blobj.ValidateXrayPCB(txtReelID.Text.Trim(),
                    txtScanMachineID.Text.Trim(),
                   drpFGItemCode.Text, Session["SiteCode"].ToString(), Session["LINECODE"].ToString());
                if (dt.Rows.Count > 0)
                {
                    string sResult = dt.Rows[0][0].ToString();
                    Message = sResult.Split('~')[1];
                    if (sResult.StartsWith("SUCCESS"))
                    {
                        CommonHelper.ShowMessage("PCB is ok", msgsuccess, CommonHelper.MessageType.Success.ToString());
                        return;
                    }
                    else if (sResult.StartsWith("N~"))
                    {
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                        txtReelID.Text = string.Empty;
                        txtReelID.Focus();
                        return;
                    }
                    else if (sResult.StartsWith("ERROR~"))
                    {
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                        txtReelID.Text = string.Empty;
                        txtReelID.Focus();
                    }
                    else if (sResult.StartsWith("NOTFOUND~"))
                    {
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                        txtReelID.Text = string.Empty;
                        txtReelID.Focus();
                    }
                    else if (sResult.StartsWith("MACHINEFAILED~"))
                    {
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                        txtReelID.Text = string.Empty;
                        txtReelID.Focus();
                    }
                }
                else
                {
                    CommonHelper.ShowMessage("No result found for scanned barcode Please try again", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtReelID.Text = string.Empty;
                    txtReelID.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (drpDefect.Items.Count == 0)
                {
                    CommonHelper.ShowMessage("No defect found to add.", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpDefect.Focus();
                    return;
                }
                if (drpDefect.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select defect.", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpDefect.Focus();
                    return;
                }
                string sFormatNo = drpDefect.SelectedValue.ToString();
                bool drs = dtAdd.AsEnumerable().Any(tt => tt.Field<string>("DEFECT") == sFormatNo);
                if (drs == true)
                {
                    CommonHelper.ShowMessage("Defect already selected, Please select different one.", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpDefect.Focus();
                    return;
                }
                DataRow dr = dtAdd.NewRow();
                dr["DEFECT"] = drpDefect.SelectedValue.ToString();
                dtAdd.Rows.Add(dr);
                gvMultiDefect.DataSource = dtAdd;
                gvMultiDefect.DataBind();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void gvMultiDefect_RowCommand(object sender, GridViewCommandEventArgs e)
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
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {
                gvMultiDefect.DataSource = dtAdd;
                gvMultiDefect.DataBind();
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (string.IsNullOrEmpty(txtScanMachineID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan machine barcode", msgerror, CommonHelper.MessageType.Info.ToString());
                    txtScanMachineID.Focus();
                    txtScanMachineID.Text = string.Empty;
                    return;
                }
                if (drpFGItemCode.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select fg item code", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpFGItemCode.Focus();
                    txtReelID.Text = string.Empty;
                    return;
                }
                if (string.IsNullOrEmpty(txtReelID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtReelID.Focus();
                    return;
                }
                string sDefect = string.Empty;
                blobj = new BL_WIP_VIQuality();
                DataTable dt = blobj.SaveXrayQualtiyData(txtReelID.Text.Trim(), txtScanMachineID.Text.Trim()
                    , drpFGItemCode.Text, "", "", txtObservation.Text.Trim(), "1"
                    , Session["SiteCode"].ToString(), Session["LINECODE"].ToString()
                    , Session["UserID"].ToString()
                    );
                if (dt.Rows.Count > 0)
                {
                    string sResult = dt.Rows[0][0].ToString();
                    if (sResult.StartsWith("SUCCESS~"))
                    {
                        if (drpstation.SelectedIndex > 0)
                        {
                            CommonHelper.ShowMessage("Part (" + txtReelID.Text.Trim() + ") moved to repair station (" + drpstation.Text + ") for rework", msginfo, CommonHelper.MessageType.Error.ToString());
                        }
                        else
                        {
                            CommonHelper.ShowMessage(sResult.Split('~')[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                        }
                        txtReelID.Text = string.Empty;
                        txtReelID.Focus();
                        drpstation.SelectedIndex = 0;
                        txtObservation.Text = string.Empty;
                        if (drpDefect.Items.Count > 0)
                        {
                            drpDefect.SelectedIndex = 0;
                        }
                        gvMultiDefect.DataSource = null;
                        gvMultiDefect.DataBind();
                    }
                    else
                    {
                        CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        txtReelID.Text = string.Empty;
                        txtReelID.Focus();
                        drpstation.SelectedIndex = 0;
                        txtObservation.Text = string.Empty;
                        if (drpDefect.Items.Count > 0)
                        {
                            drpDefect.SelectedIndex = 0;
                        }
                        dtAdd.Rows.Clear();
                        gvMultiDefect.DataSource = null;
                        gvMultiDefect.DataBind();
                    }
                }
                else
                {
                    CommonHelper.ShowMessage("No result found for scanned barcode Please try again", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtReelID.Text = string.Empty;
                    txtReelID.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (string.IsNullOrEmpty(txtScanMachineID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan machine barcode", msgerror, CommonHelper.MessageType.Info.ToString());
                    txtScanMachineID.Focus();
                    txtScanMachineID.Text = string.Empty;
                    return;
                }
                if (drpFGItemCode.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select fg item code", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpFGItemCode.Focus();
                    txtReelID.Text = string.Empty;
                    return;
                }
                if (string.IsNullOrEmpty(txtReelID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtReelID.Focus();
                    return;
                }
                if (gvMultiDefect.Rows.Count == 0)
                {
                    CommonHelper.ShowMessage("Please select at least one defect", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpDefect.Focus();
                    return;
                }
                if (drpstation.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select rework station", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpstation.Focus();
                    return;
                }
                string sDefect = string.Empty;
                sDefect = string.Join(Environment.NewLine,
                    dtAdd.Rows.OfType<DataRow>().Select(x => string.Join(" ; ", x.ItemArray)));

                blobj = new BL_WIP_VIQuality();
                DataTable dt = blobj.SaveXrayQualtiyData(txtReelID.Text.Trim(), txtScanMachineID.Text.Trim()
                    , drpFGItemCode.Text, sDefect, drpstation.Text, txtObservation.Text, "0"
                     , Session["SiteCode"].ToString(), Session["LINECODE"].ToString()
                    , Session["UserID"].ToString()
                    );
                if (dt.Rows.Count > 0)
                {
                    string sResult = dt.Rows[0][0].ToString();
                    if (sResult.StartsWith("SUCCESS~"))
                    {
                        if (drpstation.SelectedIndex > 0)
                        {
                            CommonHelper.ShowMessage("Part (" + txtReelID.Text.Trim() + ") moved to repair station (" + drpstation.Text + ") for rework", msginfo, CommonHelper.MessageType.Error.ToString());
                        }
                        else
                        {
                            CommonHelper.ShowMessage(sResult.Split('~')[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                        }
                        txtReelID.Text = string.Empty;
                        txtReelID.Focus();
                        drpstation.SelectedIndex = 0;
                        txtObservation.Text = string.Empty;
                        if (drpDefect.Items.Count > 0)
                        {
                            drpDefect.SelectedIndex = 0;
                        }
                        dtAdd.Rows.Clear();
                        gvMultiDefect.DataSource = null;
                        gvMultiDefect.DataBind();
                    }
                    else
                    {
                        CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        txtReelID.Text = string.Empty;
                        txtReelID.Focus();
                        drpstation.SelectedIndex = 0;
                        txtObservation.Text = string.Empty;
                        if (drpDefect.Items.Count > 0)
                        {
                            drpDefect.SelectedIndex = 0;
                        }
                        dtAdd.Rows.Clear();
                        gvMultiDefect.DataSource = null;
                        gvMultiDefect.DataBind();
                    }
                }
                else
                {
                    CommonHelper.ShowMessage("No result found for scanned barcode Please try again", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtReelID.Text = string.Empty;
                    txtReelID.Focus();
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
                txtReelID.Text = string.Empty;
                txtObservation.Text = string.Empty;
                txtScanMachineID.Text = string.Empty;
                dtAdd.Rows.Clear();
                gvMultiDefect.DataSource = null;
                gvMultiDefect.DataBind();
                if (drpDefect.Items.Count > 0)
                {
                    drpDefect.SelectedIndex = 0;
                }
                if (drpstation.Items.Count > 0)
                {
                    drpstation.SelectedIndex = 0;
                }
                drpFGItemCode.Items.Clear();

            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}