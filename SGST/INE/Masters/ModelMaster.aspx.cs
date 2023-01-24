using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;
using BusinessLayer;
using PL;
using System.Linq;

namespace SGST.INE.Masters
{
    public partial class ModelMaster : System.Web.UI.Page
    {
        string Message = string.Empty;
        PL_ModelMaster plobj = new PL_ModelMaster();
        BL_ModelMaster blobj = new BL_ModelMaster();
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
                    string _strRights = CommonHelper.GetRights("FG MODEL MASTER", (DataTable)Session["USER_RIGHTS"]);
                    CommonHelper._strRights = _strRights.Split('^');
                    if (CommonHelper._strRights[0] == "0")  //Check view rights
                    {
                        Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                    }
                }
                if (!IsPostBack)
                {
                    ShowGridData();
                    blobj = new BL_ModelMaster();
                    plobj = new PL_ModelMaster();
                    plobj.sSiteCode = Session["SiteCode"].ToString();
                    DataTable dt = blobj.blBindFGItemCode(plobj);
                    if (dt.Rows.Count > 0)
                    {
                        clsCommon.FillComboBox(drpFGItemCode, dt, true);
                        drpFGItemCode.Focus();
                    }
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
                ddlModelName.Items.Clear();
                DataTable dt = new DataTable();
                blobj = new BL_ModelMaster();
                plobj = new PL_ModelMaster();
                DataSet ds = blobj.blRetrieveModelDetails(plobj);
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
                if (dt.Rows.Count > 0)
                {
                    gvModel.DataSource = dt;
                    gvModel.DataBind();
                    lblNumberofRecords.Text = dt.Rows.Count.ToString();
                    dt.TableName = "Table1";
                    dt.AcceptChanges();
                    System.Data.DataView view = new System.Data.DataView(dt);
                    System.Data.DataTable selected =
                            view.ToTable("Table1", false, "MODEL_CODE");
                    clsCommon.FillComboBox(ddlModelName, selected, true);
                    ViewState["Data"] = dt;
                }
                else
                {
                    gvModel.DataSource = null; ;
                    gvModel.DataBind();
                    lblNumberofRecords.Text = "0";
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        private void SaveModelDetails()
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (drpFGItemCode.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select fg item code", msgerror, CommonHelper.MessageType.Error.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    drpFGItemCode.Focus();
                    return;
                }
                if (txtModelName.Text.Trim() == "")
                {
                    CommonHelper.ShowMessage("Enter model name", msgerror, CommonHelper.MessageType.Error.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    txtModelName.Focus();
                    return;
                }
                if (txtModelType.Text.Trim() == "")
                {
                    CommonHelper.ShowMessage("Enter model type", msgerror, CommonHelper.MessageType.Error.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    txtModelType.Focus();
                    return;
                }
                if (txtEAN.Text.Trim() == "")
                {
                    CommonHelper.ShowMessage("Enter EAN", msgerror, CommonHelper.MessageType.Error.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    return;
                }
                if (txtMRP.Text.Trim() == "" || txtMRP.Text.Trim() == "." || Convert.ToDouble(txtMRP.Text.Trim()) == 0)
                {
                    CommonHelper.ShowMessage("Enter valid MRP .", msgerror, CommonHelper.MessageType.Error.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    txtMRP.Focus();
                    return;
                }
                if (txtWt.Text.Trim() == "" || txtWt.Text.Trim() == ".")
                {
                    CommonHelper.ShowMessage("Enter valid Wt .", msgerror, CommonHelper.MessageType.Error.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    txtWt.Focus();
                    return;
                }
                if (txtTolPlus.Text.Trim() == "" || txtTolPlus.Text.Trim() == ".")
                {
                    CommonHelper.ShowMessage("Enter valid Tolerance Plus .", msgerror, CommonHelper.MessageType.Error.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    txtTolPlus.Focus();
                    return;
                }
                if (txtTolMinus.Text.Trim() == "" || txtTolMinus.Text.Trim() == ".")
                {
                    CommonHelper.ShowMessage("Enter valid Tolerance Minus .", msgerror, CommonHelper.MessageType.Error.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    txtTolMinus.Focus();
                    return;
                }
                if (txtCartonWt.Text.Trim() == "" || txtCartonWt.Text.Trim() == ".")
                {
                    CommonHelper.ShowMessage("Enter valid Wt .", msgerror, CommonHelper.MessageType.Error.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    txtCartonWt.Focus();
                    return;
                }
                if (txtCartonTolPlus.Text.Trim() == "" || txtCartonTolPlus.Text.Trim() == ".")
                {
                    CommonHelper.ShowMessage("Enter valid Tolerance Plus .", msgerror, CommonHelper.MessageType.Error.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    txtCartonTolPlus.Focus();
                    return;
                }
                if (txtCartonTolMinus.Text.Trim() == "" || txtCartonTolMinus.Text.Trim() == ".")
                {
                    CommonHelper.ShowMessage("Enter valid Tolerance Minus .", msgerror, CommonHelper.MessageType.Error.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    txtCartonTolMinus.Focus();
                    return;
                }
                if (txtEmptyCartonWT.Text.Trim() == "" || txtEmptyCartonWT.Text.Trim() == ".")
                {
                    CommonHelper.ShowMessage("Enter valid Empty Carton WT .", msgerror, CommonHelper.MessageType.Error.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    txtEmptyCartonWT.Focus();
                    return;
                }
                if (txtMacAddress.Text == "0")
                {
                    CommonHelper.ShowMessage("Enter valid No Of Mac Address", msgerror, CommonHelper.MessageType.Error.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    txtMacAddress.Focus();
                    return;
                }
                plobj = new PL_ModelMaster();
                blobj = new BL_ModelMaster();
                plobj.sSiteCode = Session["SiteCode"].ToString();
                plobj.sCreatedBy = Session["UserID"].ToString();
                plobj.sModelName = txtModelName.Text.Trim();
                plobj.dMRP = Convert.ToDecimal(txtMRP.Text.Trim());
                plobj.sSWVER = txtSWVersion.Text.Trim();
                plobj.sModelType = txtModelType.Text.Trim();
                plobj.sEAN = txtEAN.Text.Trim();
                plobj.sBOM = drpFGItemCode.Text.Trim();
                plobj.sVendorSupplying = txtVendor.Text.Trim();
                plobj.iWarruntyInDays = Convert.ToInt32(txtWarentyInDaya.Text.Trim());
                plobj.dGBWT = Convert.ToDecimal(txtWt.Text.Trim());
                plobj.dTOLERANCE_PLUS = Convert.ToDecimal(txtTolPlus.Text.Trim());
                plobj.dTOLERANCE_MINUS = Convert.ToDecimal(txtTolMinus.Text.Trim());
                plobj.dCBWT = Convert.ToDecimal(txtCartonWt.Text.Trim());
                plobj.dCTOLERANCE_PLUS = Convert.ToDecimal(txtCartonTolPlus.Text.Trim());
                plobj.dCTOLERANCE_MINUS = Convert.ToDecimal(txtCartonTolMinus.Text.Trim());
                plobj.dEmptyCartonWT = Convert.ToDecimal(txtEmptyCartonWT.Text.Trim());
                plobj.bWaveToolValidate = chkWaveToolValidate.Checked;
                plobj.bASNShowsMac2 = chkASNMac2.Checked;
                plobj.iNoOfMacAddress = Convert.ToInt32(txtMacAddress.Text);
                plobj.REPORT_LOCATION = txtReportLocation.Text;
                plobj.REPORT_LOT_NO = txtReportLotNo.Text;
                plobj.HWVERSION = txtHWVersion.Text;
                plobj.ASN_MODEL_NO = txtASnModelNo.Text;
                plobj.PIN_NO = txtPINNO.Text;
                DataTable dt = new DataTable();
                if (Session["DuplicateData"] != null)
                {
                    dt = (DataTable)Session["DuplicateData"];
                }
                plobj.dtDuplicateModelList = dt;
                int iResult = 0;
                if (btnSave.Text == "Save")
                {
                    int iDuplicateCheck = Convert.ToInt32(blobj.blCheckDuplicateModelName(plobj));
                    if (iDuplicateCheck > 0)
                    {
                        CommonHelper.ShowMessage("Model name already exist, Please enter another one", msgerror, CommonHelper.MessageType.Error.ToString());
                        txtModelName.Text = string.Empty;
                        return;
                    }
                    iResult = blobj.blSaveModelMaster(plobj);
                }
                else
                {
                    plobj.sModelName = hidModelID.Value;
                    iResult = blobj.blUpdateModelMaster(plobj);
                }
                if (iResult > 0)
                {
                    CommonHelper.ShowMessage(" Data saved successfully ", msgsuccess, CommonHelper.MessageType.Success.ToString());
                }
                else
                {
                    CommonHelper.ShowMessage("Data saved failed,Please try again", msgerror, CommonHelper.MessageType.Error.ToString());
                }
                btnReset_Click(null, null);
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveModelDetails();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                hidModelID.Value = "";
                txtModelName.Text = "";
                txtMRP.Text = "";
                txtSWVersion.Text = "";
                txtEAN.Text = "";
                ShowGridData();
                txtModelType.Text = string.Empty;
                btnSave.Text = "Save";
                txtVendor.Text = string.Empty;
                txtWarentyInDaya.Text = "1";
                txtWt.Text = "0";
                txtTolPlus.Text = "0";
                txtTolMinus.Text = "0";
                txtCartonWt.Text = "0";
                txtCartonTolPlus.Text = "0";
                txtCartonTolMinus.Text = "0";
                txtEmptyCartonWT.Text = "0";
                txtMacAddress.Text = "1";
                txtPINNO.Text = "";
                txtReportLocation.Text = "";
                txtReportLotNo.Text = "";
                txtHWVersion.Text = "";
                txtASnModelNo.Text = "";
                gvDuplicateColumn.DataSource = null;
                gvDuplicateColumn.DataBind();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }

        }
        protected void gvModel_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                string _SN = string.Empty;
                string[] strValue = e.CommandArgument.ToString().Split('~');
                _SN = e.CommandArgument.ToString();
                if (e.CommandName == "DeleteRecords")
                {
                    DeleteRecords(_SN);
                }
                if (e.CommandName == "EditRecords")
                {
                    if (btnSave.Text == "Save")
                    { btnSave.Text = "Update"; }
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
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                blobj = new BL_ModelMaster();
                plobj = new PL_ModelMaster();
                plobj.sModelName = _SN;
                DataTable dtDetails = new DataTable();
                DataSet dsDetails = blobj.blRetrieveModelDetails(plobj);
                if (dsDetails.Tables.Count > 0)
                {
                    dtDetails = dsDetails.Tables[0];
                }
                if (dtDetails.Rows.Count > 0)
                {
                    txtModelType.Text = dtDetails.Rows[0]["MODEL_DESC"].ToString();
                    hidModelID.Value = dtDetails.Rows[0]["MODEL_CODE"].ToString();
                    txtModelName.Text = dtDetails.Rows[0]["MODEL_CODE"].ToString();
                    txtMRP.Text = dtDetails.Rows[0]["MRP"].ToString();
                    txtSWVersion.Text = dtDetails.Rows[0]["SWVERSION"].ToString();
                    txtEAN.Text = dtDetails.Rows[0]["EAN_CODE"].ToString();
                    drpFGItemCode.Text = dtDetails.Rows[0]["BOM_CODE"].ToString();
                    txtVendor.Text = dtDetails.Rows[0]["VENDOR_CODE"].ToString();
                    txtWarentyInDaya.Text = dtDetails.Rows[0]["WARENTYINDAYS"].ToString();
                    txtWt.Text = dtDetails.Rows[0]["GrossWT"].ToString();
                    txtTolPlus.Text = dtDetails.Rows[0]["TolPlus"].ToString();
                    txtTolMinus.Text = dtDetails.Rows[0]["TolMinus"].ToString();
                    txtCartonWt.Text = dtDetails.Rows[0]["CARTON_WT"].ToString();
                    txtCartonTolPlus.Text = dtDetails.Rows[0]["CTolPlus"].ToString();
                    txtCartonTolMinus.Text = dtDetails.Rows[0]["CTolMinus"].ToString();
                    txtEmptyCartonWT.Text = dtDetails.Rows[0]["ECARTONWT"].ToString();
                    txtMacAddress.Text = dtDetails.Rows[0]["MACADDRESSCOUNT"].ToString();
                    txtASnModelNo.Text = dtDetails.Rows[0]["ASN_MODEL_NO"].ToString();
                    txtReportLotNo.Text = dtDetails.Rows[0]["REPORT_LOT_NO"].ToString();
                    txtReportLocation.Text = dtDetails.Rows[0]["REPORT_LOCATION"].ToString();
                    txtPINNO.Text = dtDetails.Rows[0]["PIN_NO"].ToString();
                    txtHWVersion.Text = dtDetails.Rows[0]["HWVERSION"].ToString();
                    if (dtDetails.Rows[0]["WAVE_TOOL_VALIDATE"].ToString() == "1" || dtDetails.Rows[0]["WAVE_TOOL_VALIDATE"].ToString() == "True")
                    {
                        chkWaveToolValidate.Checked = true;
                    }
                    else
                    {
                        chkWaveToolValidate.Checked = false;
                    }
                    if (dtDetails.Rows[0]["IsASNMac2Required"].ToString() == "1" || dtDetails.Rows[0]["WAVE_TOOL_VALIDATE"].ToString() == "True")
                    {
                        chkASNMac2.Checked = true;
                    }
                    else
                    {
                        chkASNMac2.Checked = false;
                    }
                    btnSave.Text = "Update";
                    if (dsDetails.Tables.Count == 2)
                    {
                        Session["DuplicateData"] = dsDetails.Tables[1];
                        gvDuplicateColumn.DataSource = dsDetails.Tables[1];
                        gvDuplicateColumn.DataBind();
                    }
                }
                else
                {
                    CommonHelper.ShowMessage("No details found", msgerror, CommonHelper.MessageType.Error.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        private void DeleteRecords(string _SN)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                blobj = new BL_ModelMaster();
                plobj = new PL_ModelMaster();
                if (_SN.Length > 0)
                {
                    plobj.sModelName = _SN;
                    DataTable dt = blobj.blDeleteUnusedModelDetails(plobj);
                    if (dt.Rows.Count > 0)
                    {
                        string sResult = dt.Rows[0][0].ToString();
                        if (sResult.StartsWith("ERROR~"))
                        {
                            CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        }
                        if (sResult.StartsWith("N~"))
                        {
                            CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        }
                        else
                        {
                            CommonHelper.ShowMessage(sResult.Split('~')[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                        }
                    }
                    else
                    {
                        CommonHelper.ShowMessage("No result found", msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
            finally
            {
                ShowGridData();
            }
        }

        protected void ddlModelName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlModelName.SelectedIndex > 0)
                {
                    DataTable dt = (DataTable)ViewState["Data"];
                    DataView dataView = dt.DefaultView;
                    dataView.RowFilter = "MODEL_CODE = '" + ddlModelName.SelectedValue + "'";
                    gvModel.DataSource = dataView;
                    gvModel.DataBind();
                }
                else
                {
                    DataTable dt = (DataTable)ViewState["Data"];
                    gvModel.DataSource = dt;
                    gvModel.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void gvModel_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvModel.PageIndex = e.NewPageIndex;
            ShowGridData();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (drpColumnName.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select.", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpColumnName.Focus();
                    return;
                }
                DataTable dtAdd;
                dtAdd = new DataTable();
                if (Session["DuplicateData"] == null)
                {
                    DataColumn MODEL = new DataColumn("MODEL", typeof(String));
                    DataColumn duplicateColumn = new DataColumn("duplicateColumn", typeof(string));
                    dtAdd.Columns.Add(MODEL);
                    dtAdd.Columns.Add(duplicateColumn);
                    DataRow dr = dtAdd.NewRow();
                    dr["MODEL"] = txtModelName.Text;
                    dr["duplicateColumn"] = drpColumnName.Text;
                    dtAdd.Rows.Add(dr);
                    Session["DuplicateData"] = dtAdd;
                }
                else
                {
                    dtAdd = (DataTable)Session["DuplicateData"];
                    bool drs = dtAdd.AsEnumerable().Any(tt => tt.Field<string>("duplicateColumn") == drpColumnName.Text);
                    if (drs == true)
                    {
                        CommonHelper.ShowMessage("Column already selected, Please select different one.", msgerror, CommonHelper.MessageType.Error.ToString());
                        drpColumnName.Focus();
                        return;
                    }                    
                    DataRow dr = dtAdd.NewRow();
                    dr["MODEL"] = txtModelName.Text;
                    dr["duplicateColumn"] = drpColumnName.Text;
                    dtAdd.Rows.Add(dr);
                    Session["DuplicateData"] = dtAdd;
                }
                gvDuplicateColumn.DataSource = dtAdd;
                gvDuplicateColumn.DataBind();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void gvDuplicateColumn_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                string _SN = string.Empty;
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                _SN = gvr.Cells[0].Text.Replace("&nbsp;", " ").Trim();
                if (Session["DuplicateData"] != null)
                {
                    DataTable dt = (DataTable)Session["DuplicateData"];
                    if (e.CommandName == "DeleteRecords")
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (dr["duplicateColumn"].ToString() == _SN.Trim())
                            {
                                dt.Rows.Remove(dr);
                                break;
                            }
                        }
                        dt.AcceptChanges();
                    }
                    Session["DuplicateData"] = dt;
                    gvDuplicateColumn.DataSource = dt;
                    gvDuplicateColumn.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}