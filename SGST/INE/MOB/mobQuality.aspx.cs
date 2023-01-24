using BusinessLayer;
using Common;
using System;
using System.Data;
using PL;
using BusinessLayer.MES.QUALITY;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Linq;
using BusinessLayer.WIP;

namespace SGST.INE.MOB
{
    public partial class mobQuality : System.Web.UI.Page
    {
        BL_mobQuality blobj = new BL_mobQuality();
        static DataTable dtAdd;
        private void BindStationID()
        {
            try
            {
                drpstation.Items.Clear();
                string sResult = string.Empty;
                BL_WIP_VIQuality blobj = new BL_WIP_VIQuality();
                DataTable dtStationID = blobj.BindReWorkStationID(Session["SiteCode"].ToString());
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
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
                Response.AddHeader("Pragma", "no-cache");
                Response.AddHeader("Expires", "0");
                if (Session["usertype"].ToString() != "ADMIN")
                {
                    string _strRights = string.Empty;
                    _strRights = CommonHelper.GetRights("MES QUALITY", (DataTable)Session["USER_RIGHTS"]);
                    CommonHelper._strRights = _strRights.Split('^');
                    if (CommonHelper._strRights[0] == "0")  //Check view rights
                    {
                        Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                    }
                }
                if (!IsPostBack)
                {
                    BindStationID();
                    BindModelName();
                    dtAdd = new DataTable();
                    AddColumns();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        private void BindModelName()
        {
            try
            {
                lblModelName.Text = string.Empty;
                drpFGItemCode.Items.Clear();
                lbllastscanned.Text = string.Empty;
                BL_MobCommon obj = new BL_MobCommon();
                DataTable dt = obj.BindModel();
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillMultiColumnsCombo(drpFGItemCode, dt, true);
                    drpFGItemCode.Focus();
                }
                else
                {
                    CommonHelper.ShowMessage("No data found.", msginfo, CommonHelper.MessageType.Info.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        public void GetData()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (drpFGItemCode.SelectedIndex > 0)
                {
                    lblModelName.Text = string.Empty;
                    lbllastscanned.Text = string.Empty;
                    BL_MobCommon obj = new BL_MobCommon();
                    PL_Printing plobj = new PL_Printing();
                    plobj.sModelName = drpFGItemCode.SelectedValue.ToString();
                    DataTable dt = new DataTable();
                    dt = obj.DisplayedData(plobj);
                    if (dt.Rows.Count > 0)
                    {
                        lblModelName.Text = dt.Rows[0]["MODEL_CODE"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        private void BindDefect()
        {
            try
            {
                drpDefect.Items.Clear();
                string sResult = string.Empty;
                blobj = new BL_mobQuality();
                PL_Printing obj = new PL_Printing();
                obj.sStageCode = txtScanMachineID.Text.Trim();
                DataTable dtStationID = blobj.BindDefect(obj);
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
                drpDefect.Items.Clear();
                blobj = new BL_mobQuality();
                PL_Printing plobj = new PL_Printing();
                plobj.sStageCode = txtScanMachineID.Text.Trim();
                plobj.sBOMCode = drpFGItemCode.SelectedItem.Text.Trim();
                DataTable dt = blobj.ValidateMachine(plobj);
                if (dt.Rows.Count > 0)
                {
                    CommonHelper.ShowMessage("Valid machine", msgsuccess, CommonHelper.MessageType.Success.ToString());
                    lblMachineName.Text = dt.Rows[0][0].ToString();
                    lblModelNo.Text = dt.Rows[0][2].ToString();
                    BindDefect();
                }
                else
                {
                    CommonHelper.ShowMessage("Please scan only IMS Quality machine", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtScanMachineID.Focus();
                    txtScanMachineID.Text = string.Empty;
                    lblMachineName.Text = "";
                    lblModelNo.Text = "";
                    return;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void txtScanHere_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (drpFGItemCode.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select fg item code", msginfo, CommonHelper.MessageType.Info.ToString());
                    drpFGItemCode.Focus();
                    txtScanHere.Text = string.Empty;
                }
                if (string.IsNullOrEmpty(txtScanMachineID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan machine barcode", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtScanMachineID.Focus();
                    txtScanMachineID.Text = string.Empty;
                    return;
                }
                if (dtAdd.Rows.Count > 0)
                {
                    if (drpstation.SelectedIndex == 0)
                    {
                        CommonHelper.ShowMessage("Please select station", msginfo, CommonHelper.MessageType.Info.ToString());
                        drpstation.Focus();
                        txtScanHere.Text = string.Empty;
                        return;
                    }
                }
                else if (string.IsNullOrEmpty(txtScanHere.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan PCB barcode", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtScanHere.Focus();
                    txtScanHere.Text = string.Empty;
                    return;
                }
                BL_mobQuality blobj = new BL_mobQuality();
                PL_Printing obj = new PL_Printing();
                obj.sBOMCode = drpFGItemCode.SelectedItem.Text.Trim();
                obj.sModelName = drpFGItemCode.SelectedValue.ToString();
                obj.sColorCode = "";
                obj.sSNBarcode = txtScanHere.Text.Trim();
                obj.sStageCode = txtScanMachineID.Text.Trim();
                string sDefect = string.Empty;
                sDefect = string.Join(Environment.NewLine,
                    dtAdd.Rows.OfType<DataRow>().Select(x => string.Join(" ; ", x.ItemArray)));
                obj.sDefect = sDefect;
                obj.sReworkStation = drpstation.Text;
                string sResult = blobj.blMobQuality(obj);
                lbllastscanned.Text = txtScanHere.Text;
                if (sResult.StartsWith("SUCCESS"))
                {
                    CommonHelper.ShowMessage(sResult.Split('~')[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                    txtScanHere.Text = string.Empty;
                    txtScanHere.Focus();
                    gvMultiDefect.DataSource = null;
                    gvMultiDefect.DataBind();
                    dtAdd.Rows.Clear();
                }
                else if (sResult.StartsWith("N~") || sResult.StartsWith("ERROR~"))
                {
                    CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    txtScanHere.Text = string.Empty;
                    txtScanHere.Focus();
                    gvMultiDefect.DataSource = null;
                    gvMultiDefect.DataBind();
                    dtAdd.Rows.Clear();
                    return;
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
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                BindModelName();
                lblModelName.Text = string.Empty;
                lbllastscanned.Text = string.Empty;
                txtScanHere.Text = string.Empty;
                lbllastscanned.Text = string.Empty;  
                txtScanMachineID.Text = string.Empty;
                dtAdd.Rows.Clear();
                gvMultiDefect.DataSource = null;
                gvMultiDefect.DataBind();
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

        protected void gvMultiDefect_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
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

        protected void lblFGItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetData();
        }
    }
}