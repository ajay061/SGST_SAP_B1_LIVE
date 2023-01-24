using BusinessLayer;
using Common;
using System;
using System.Data;
using PL;
using BusinessLayer.MES.QUALITY;

namespace SGST.INE.MOB
{
    public partial class mobQualityRework : System.Web.UI.Page
    {
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
                    _strRights = CommonHelper.GetRights("MES QUALITY REWORK", (DataTable)Session["USER_RIGHTS"]);
                    CommonHelper._strRights = _strRights.Split('^');
                    if (CommonHelper._strRights[0] == "0")  //Check view rights
                    {
                        Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                    }
                }
                if (!IsPostBack)
                {
                    BindModelName();
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
                ddlModel_Name.Items.Clear();
                ddlColor.Items.Clear();
                lblFGItemCode.Text = string.Empty;
                lblModelType.Text = string.Empty;
                lbllastscanned.Text = string.Empty;
                BL_MobCommon obj = new BL_MobCommon();
                DataTable dt = obj.BindModel();
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillMultiColumnsCombo(ddlModel_Name, dt, true);
                    ddlModel_Name.Focus();
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
        private void BindDefect()
        {
            try
            {
                BL_mobQualityRework blobj = new BL_mobQualityRework();
                string sResult = string.Empty;
                PL_Printing obj = new PL_Printing();
                DataTable dt = blobj.bindefect(obj);
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
        public void GetData()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (ddlModel_Name.SelectedIndex > 0)
                {
                    ddlColor.Items.Clear();
                    lblModelType.Text = string.Empty;
                    lbllastscanned.Text = string.Empty;
                    lblFGItemCode.Text = string.Empty;
                    BL_MobCommon obj = new BL_MobCommon();
                    PL_Printing plobj = new PL_Printing();
                    plobj.iSNModel = Convert.ToInt32(ddlModel_Name.SelectedValue.ToString());
                    //DataTable dt = obj.BindColor(plobj);
                    //if (dt.Rows.Count > 0)
                    //{
                    //    clsCommon.FillMultiColumnsCombo(ddlColor, dt, true);
                    //    ddlColor.Focus();
                    //}
                    DataTable dt = new DataTable();
                    dt = obj.DisplayedData(plobj);
                    if (dt.Rows.Count > 0)
                    {
                        lblModelType.Text = dt.Rows[0]["MODEL_TYPE"].ToString();
                    }

                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                BindModelName();
                lblModelType.Text = string.Empty;
                lbllastscanned.Text = string.Empty;
                txtPCBID.Text = string.Empty;
                lbllastscanned.Text = string.Empty;
                lblFGItemCode.Text = string.Empty;
                txtobservation.Text = string.Empty;
                txtRemarks.Text = string.Empty;
                drpDefect.Items.Clear();

            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void ddlModel_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetData();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        protected void ddlColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlColor.SelectedIndex > 0)
                {
                    lblFGItemCode.Text = string.Empty;
                    BL_MobCommon obj = new BL_MobCommon();
                    PL_Printing plobj = new PL_Printing();
                    plobj.iSNModel = Convert.ToInt32(ddlModel_Name.SelectedValue.ToString());
                    plobj.sColorCode = Convert.ToString(ddlColor.SelectedValue.ToString());
                    DataTable dt = new DataTable();
                    //dt = obj.GetModelColorData(plobj);
                    //if (dt.Rows.Count > 0)
                    //{
                    //    lblFGItemCode.Text = dt.Rows[0]["BOM_CODE"].ToString();
                    //}
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                drpDefect.Items.Clear();
                if (drpType.SelectedValue.ToUpper() == "IN")
                {
                    divOut.Visible = false;
                }
                else
                {
                    divOut.Visible = true;
                    BindDefect();
                }
                txtobservation.Text = string.Empty;
                txtRemarks.Text = string.Empty;
                txtPCBID.Text = string.Empty;
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
        }

        protected void txtPCBID_TextChanged(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (ddlModel_Name.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select model", msginfo, CommonHelper.MessageType.Info.ToString());
                    ddlModel_Name.Focus();
                    txtPCBID.Text = string.Empty;
                    return;
                }
                if (ddlColor.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select color", msginfo, CommonHelper.MessageType.Info.ToString());
                    ddlColor.Focus(); txtPCBID.Text = string.Empty;
                    return;
                }
                if (drpType.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select type", msginfo, CommonHelper.MessageType.Info.ToString());
                    drpType.Focus(); txtPCBID.Text = string.Empty;
                    return;
                }
                if (drpType.Text == "OUT")
                {
                    if (drpDefect.SelectedIndex == 0)
                    {
                        CommonHelper.ShowMessage("Please select defect", msginfo, CommonHelper.MessageType.Info.ToString());
                        drpDefect.Focus(); txtPCBID.Text = string.Empty;
                        return;
                    }
                    if (string.IsNullOrEmpty(txtobservation.Text.Trim()))
                    {
                        CommonHelper.ShowMessage("Please enter observation", msginfo, CommonHelper.MessageType.Info.ToString());
                        txtobservation.Focus(); txtPCBID.Text = string.Empty;
                        return;
                    }
                    if (string.IsNullOrEmpty(txtRemarks.Text.Trim()))
                    {
                        CommonHelper.ShowMessage("Please enter remarks", msginfo, CommonHelper.MessageType.Info.ToString());
                        txtRemarks.Focus();
                        txtPCBID.Text = string.Empty;
                        return;
                    }
                }
                if (string.IsNullOrEmpty(txtPCBID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan PCB barcode", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtPCBID.Focus(); txtPCBID.Text = string.Empty;
                    return;
                }
                else
                {
                    BL_mobQualityRework blobj = new BL_mobQualityRework();
                    PL_Printing plobj = new PL_Printing();
                    plobj.iSNModel = Convert.ToInt32(ddlModel_Name.SelectedValue.ToString());
                    plobj.sColorCode = ddlColor.SelectedValue.ToString();
                    plobj.sBOMCode = lblFGItemCode.Text.Trim();
                    plobj.sDefect = drpDefect.Text.Trim();
                    plobj.sObservation = txtobservation.Text.Trim();
                    plobj.sRemarks = txtRemarks.Text.Trim();
                    plobj.sType = drpType.Text;
                    plobj.sSNBarcode = txtPCBID.Text.Trim();
                    DataTable dt = blobj.validateBarcode(plobj);
                    string sResult = string.Empty;
                    lbllastscanned.Text = txtPCBID.Text.Trim();
                    if (dt.Rows.Count > 0)
                    {
                        sResult = dt.Rows[0][0].ToString();
                    }
                    else
                    {
                        CommonHelper.ShowMessage("No result found for scanned barcode, Please try again", msginfo, CommonHelper.MessageType.Info.ToString());
                        txtPCBID.Text = string.Empty;
                        txtPCBID.Focus();
                        return;
                    }
                    if (sResult.ToUpper().StartsWith("SUCCESS~"))
                    {
                        CommonHelper.ShowMessage(sResult.Split('~')[1].ToString(), msgsuccess, CommonHelper.MessageType.Success.ToString());
                        txtPCBID.Text = string.Empty;
                        txtPCBID.Focus();
                        txtobservation.Text = string.Empty;
                        txtRemarks.Text = string.Empty;                      
                        return;
                    }
                    else
                    {
                        CommonHelper.ShowMessage(sResult.Split('~')[1].ToString(), msgerror, CommonHelper.MessageType.Error.ToString());
                        txtPCBID.Text = string.Empty;
                        txtPCBID.Focus();
                        txtobservation.Text = string.Empty;
                        txtRemarks.Text = string.Empty;
                        return;
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
    }
}