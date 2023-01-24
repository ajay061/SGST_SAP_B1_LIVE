using BusinessLayer;
using BusinessLayer.MES.MASTERS;
using Common;
using PL;
using System;
using System.Data;
using System.Reflection;
using System.Web.UI.WebControls;

namespace SGST.INE.Masters
{
    public partial class ModelAccesoriesMapping : System.Web.UI.Page
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
                if (Session["usertype"].ToString().ToUpper() != "ADMIN")
                {
                    string sModuleName = "Model Accessoreis Mapping";
                    string _strRights = CommonHelper.GetRights(sModuleName, (DataTable)Session["USER_RIGHTS"]);
                    CommonHelper._strRights = _strRights.Split('^');
                    if (CommonHelper._strRights[0] == "0")  //Check view rights
                    {
                        Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                    }
                }
                if (!IsPostBack)
                {
                    Session["UserRights"] = null;
                    BindModelName();
                    BindAccessories();
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
        private void BindAccessories()
        {
            try
            {
                BL_ModelAccessoriesMapping blobj = new BL_ModelAccessoriesMapping();
                DataTable dt = blobj.dtBindKeysInGrid();
                if (dt.Rows.Count > 0)
                {
                    gvModel.DataSource = dt;
                    gvModel.DataBind();
                    ViewState["dt"] = dt;
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
        public void GetModelAccessoriesDetails()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (ddlModel_Name.SelectedIndex > 0)
                {
                    BL_ModelAccessoriesMapping blobj = new BL_ModelAccessoriesMapping();
                    DataTable dt = blobj.Bind_Model_Mapping_Keys(lblModelName.Text.Trim());
                    if (dt.Rows.Count > 0)
                    {
                        DataTable dtAccessoriesDetails = (DataTable)ViewState["dt"];
                        for (int i = 0; i < dtAccessoriesDetails.Rows.Count; i++)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (Convert.ToString(dtAccessoriesDetails.Rows[i][1].ToString().ToUpper()) == dr.ItemArray[1].ToString().ToUpper())
                                {
                                    dtAccessoriesDetails.Rows[i][0] = "True";
                                    dtAccessoriesDetails.Rows[i][2] = dr.ItemArray[2].ToString();
                                    dtAccessoriesDetails.Rows[i][3] = dr.ItemArray[3].ToString();
                                    dtAccessoriesDetails.Rows[i][4] = dr.ItemArray[4].ToString();
                                    dtAccessoriesDetails.Rows[i][5] = dr.ItemArray[5].ToString();
                                    break;
                                }
                            }
                        }
                        gvModel.DataSource = dtAccessoriesDetails;
                        gvModel.DataBind();
                        dt = (DataTable)gvModel.DataSource;
                        ViewState["dt"] = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }

        public void Databind()
        {
            DataTable dt = (DataTable)ViewState["dt"];
            gvModel.DataSource = dt;
            gvModel.DataBind();
        }

        protected void ddlModel_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlModel_Name.SelectedIndex > 0)
                {
                    lblModelName.Text = ddlModel_Name.SelectedValue.ToString();
                    GetModelAccessoriesDetails();
                }
                else
                {
                    lblModelName.Text = string.Empty;
                    BindAccessories();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError,
                   System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                BindModelName();
                lblModelName.Text = string.Empty;
                BindAccessories();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                DataTable dt = new DataTable();
                if (ddlModel_Name.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select FG Item Code.", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlModel_Name.Focus();
                    return;
                }
                if (gvModel.Rows.Count > 0)
                {
                    bool _ViewRights = false;
                    DataTable dtRigthsData = new DataTable();
                    dtRigthsData.Columns.Add("MODELNAME");
                    dtRigthsData.Columns.Add("FGITEMCODE");
                    dtRigthsData.Columns.Add("ACCNAME");
                    dtRigthsData.Columns.Add("START_DIGIT");
                    dtRigthsData.Columns.Add("ENDDIGIT");
                    dtRigthsData.Columns.Add("KEYVALUE");
                    dtRigthsData.Columns.Add("ISDUPLICATE");
                    foreach (GridViewRow gvRow in gvModel.Rows)
                    {
                        if (gvRow.RowType == DataControlRowType.DataRow)
                        {
                            _ViewRights = ((CheckBox)gvRow.FindControl("ChkViewRights")).Checked;
                            if (_ViewRights.ToString() == "True")
                            {
                                Label lblModuleID = gvRow.FindControl("lblAccessName") as Label;
                                Label lblStartDigit = gvRow.FindControl("StartDigit") as Label;
                                Label lblEndDigit = gvRow.FindControl("EndDigit") as Label;
                                Label lblKeyValue = gvRow.FindControl("Value") as Label;
                                Label lblIsDuplicate = gvRow.FindControl("lblISDUPLICATE") as Label;
                                if (lblModuleID.Text != "STAND LABEL")
                                {
                                    if (string.IsNullOrEmpty(lblStartDigit.Text.Trim()))
                                    {
                                        CommonHelper.ShowMessage("Please enter start digit.", msgerror, CommonHelper.MessageType.Error.ToString());
                                        gvRow.Cells[3].Focus();
                                        return;
                                    }
                                    if (Convert.ToInt32(lblStartDigit.Text) < 0)
                                    {
                                        CommonHelper.ShowMessage("Please enter valid start digit.", msgerror, CommonHelper.MessageType.Error.ToString());
                                        gvRow.Cells[3].Focus();
                                        return;
                                    }
                                    if (string.IsNullOrEmpty(lblEndDigit.Text.Trim()))
                                    {
                                        CommonHelper.ShowMessage("Please enter end digit.", msgerror, CommonHelper.MessageType.Error.ToString());
                                        gvRow.Cells[3].Focus();
                                        return;
                                    }
                                    if (Convert.ToInt32(lblEndDigit.Text) < 0)
                                    {
                                        CommonHelper.ShowMessage("Please enter valid start digit.", msgerror, CommonHelper.MessageType.Error.ToString());
                                        gvRow.Cells[3].Focus();
                                        return;
                                    }
                                    if (string.IsNullOrEmpty(lblKeyValue.Text.Trim()))
                                    {
                                        CommonHelper.ShowMessage("Please enter key value.", msgerror, CommonHelper.MessageType.Error.ToString());
                                        gvRow.Cells[3].Focus();
                                        return;
                                    }
                                    //if (lblKeyValue.Text.Length < Convert.ToInt32(lblEndDigit.Text))
                                    //{
                                    //    CommonHelper.ShowMessage("End digit not accureate as per key", msgerror, CommonHelper.MessageType.Error.ToString());
                                    //    gvRow.Cells[3].Focus();
                                    //    return;
                                    //}
                                    //
                                }
                                dtRigthsData.Rows.Add(ddlModel_Name.SelectedValue.ToString(), ddlModel_Name.SelectedItem.Text.Trim(),
                                    lblModuleID.Text.ToUpper(), lblStartDigit.Text, lblEndDigit.Text, lblKeyValue.Text, lblIsDuplicate.Text);
                            }
                        }
                    }
                    BL_ModelAccessoriesMapping blobj = new BL_ModelAccessoriesMapping();
                    DataTable dtResult = blobj.SaveKeysModelMapping(dtRigthsData, ddlModel_Name.SelectedValue.ToString()
                        , Session["UserID"].ToString(), Session["SiteCode"].ToString(), ddlModel_Name.SelectedItem.Text.Trim());
                    if (dtResult.Rows.Count > 0)
                    {
                        string sSaveResult = dtResult.Rows[0][0].ToString();
                        if (sSaveResult.StartsWith("N~") || sSaveResult.StartsWith("ERROR~"))
                        {
                            CommonHelper.ShowMessage(sSaveResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        }
                        else
                        {
                            CommonHelper.ShowMessage(sSaveResult.Split('~')[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                        }
                    }
                    else
                    {
                        CommonHelper.ShowMessage("No result found,Please try again", msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                }
                ddlModel_Name.SelectedIndex = 0;
                BindAccessories();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
            finally
            {

            }
        }

        protected void checkAll_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void gvModel_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                gvModel.EditIndex = -1;
                Databind();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError,
                 System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void gvModel_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                gvModel.EditIndex = e.NewEditIndex;
                Databind();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError,
                   System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void gvModel_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)gvModel.Rows[e.RowIndex];
                string Zone = ((Label)gvModel.Rows[e.RowIndex].FindControl("lblAccessName")).Text;
                string T1 = ((TextBox)gvModel.Rows[e.RowIndex].FindControl("txtt1")).Text;
                string T2 = ((TextBox)gvModel.Rows[e.RowIndex].FindControl("txtt2")).Text;
                string T3 = ((TextBox)gvModel.Rows[e.RowIndex].FindControl("txtt3")).Text;
                string T4 = ((DropDownList)gvModel.Rows[e.RowIndex].FindControl("drpDuplcate")).Text;
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["dt"];
                DataRow[] datarow = dt.Select("ACCNAME='" + Zone + "'");
                dt.Rows[e.RowIndex].BeginEdit();
                dt.Rows[e.RowIndex]["Start_Digit"] = T1;
                dt.Rows[e.RowIndex]["End_Digit"] = T2;
                dt.Rows[e.RowIndex]["KEY_VAL1"] = T3;
                dt.Rows[e.RowIndex]["ISDUPLICATE"] = T4;
                dt.Rows[e.RowIndex].EndEdit();
                dt.AcceptChanges();
                gvModel.EditIndex = -1;
                gvModel.DataSource = dt;
                gvModel.DataBind();
                ViewState["dt"] = dt;
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError,
                   System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}