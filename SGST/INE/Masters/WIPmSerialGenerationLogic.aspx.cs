using Common;
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Linq;
using BusinessLayer.WIP;
using System.Web.UI;
using System.IO;
using PL;

namespace SGST.INE.WIP
{
    public partial class WIPmSerialGenerationLogic : System.Web.UI.Page
    {
        BL_WIP_Serial_Generation blobj = new BL_WIP_Serial_Generation();
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
                    string _strRights = CommonHelper.GetRights("WIP SERIAL GENERATION MASTER", (DataTable)Session["USER_RIGHTS"]);
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
                    BindBarCode();
                    BindSiteCode();
                    ShowGridData();
                    //AddColumns();
                    btnGetRunningSN.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void AddColumns()
        {
            DataTable dtAdd = new DataTable();
            DataColumn ID = new DataColumn("P_ID", typeof(int));
            DataColumn FORMAT_NO = new DataColumn("FORMAT_NO", typeof(String));
            DataColumn FORMAT_NAME = new DataColumn("FORMAT_NAME", typeof(string));
            DataColumn FORMAT_VALUE = new DataColumn("FORMAT_VALUE", typeof(string));
            dtAdd.Columns.Add(ID);
            dtAdd.Columns.Add(FORMAT_NO);
            dtAdd.Columns.Add(FORMAT_NAME);
            dtAdd.Columns.Add(FORMAT_VALUE);
        }
        protected void BindFormatGrid()
        {
            try
            {
                if (ViewState["FORMATDATA"] != null)
                {
                    gvFormatData.DataSource = (DataTable)ViewState["FORMATDATA"];
                    gvFormatData.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }

        }
        private void BindBarCode()
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                ddllBarcodeGenerater.Items.Clear();
                blobj = new BL_WIP_Serial_Generation();
                DataTable dtBindPartCode = blobj.BindBarcodeGen();
                if (dtBindPartCode.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(ddllBarcodeGenerater, dtBindPartCode, true);
                    ddllBarcodeGenerater.SelectedIndex = 0;
                    ddllBarcodeGenerater.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void BindSiteCode()
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                ddlPlant.Items.Clear();
                blobj = new BL_WIP_Serial_Generation();
                DataTable dtBindPlant = blobj.BindPlant();
                if (dtBindPlant.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(ddlPlant, dtBindPlant, true);
                    ddlPlant.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void BindCustomer(string sFGItemCode)
        {
            try
            {
                ddlCustomer.Items.Clear();
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                blobj = new BL_WIP_Serial_Generation();
                DataTable dtBindPlant = blobj.GetCustomer(sFGItemCode);
                if (dtBindPlant.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(ddlCustomer, dtBindPlant, true);
                    ddlCustomer.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void BindFGItemCode(string sSiteCode)
        {
            try
            {
                ddlCustomer.Items.Clear();
                drpfgitemCode.Items.Clear();
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                blobj = new BL_WIP_Serial_Generation();
                DataTable dtBindPlant = blobj.GetFGitemCode(sSiteCode);
                if (dtBindPlant.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(drpfgitemCode, dtBindPlant, true);
                    drpfgitemCode.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void BindResetPeriod(string sSiteCode)
        {
            try
            {
                ddlRestPeriod.Items.Clear();
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                blobj = new BL_WIP_Serial_Generation();
                DataTable dtBindPlant = blobj.GetRestPeriod(sSiteCode);
                if (dtBindPlant.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(ddlRestPeriod, dtBindPlant, true);
                    ddlRestPeriod.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void GetPreFixData(string sSiteCode)
        {
            try
            {
                ddlSuffix.Items.Clear();
                blobj = new BL_WIP_Serial_Generation();
                DataTable dtBindPrefix = blobj.GetPrefix(sSiteCode);
                if (dtBindPrefix.Rows.Count > 0)
                {
                    clsCommon.FillMultiColumnsCombo(ddlSuffix, dtBindPrefix, true);
                    ddlSuffix.SelectedIndex = 0;
                    ddlSuffix.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void GetFormatData(string sFormat)
        {
            try
            {
                ddlFormat.Items.Clear();
                blobj = new BL_WIP_Serial_Generation();
                DataTable dtBindformat = blobj.GetFormat(sFormat);
                if (dtBindformat.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(ddlFormat, dtBindformat, true);
                    ddlFormat.SelectedIndex = 0;
                    ddlFormat.Focus();
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
                lblNumberofRecords.Text = "0";
                blobj = new BL_WIP_Serial_Generation();
                DataTable dt = blobj.GetGVData();
                if (dt.Rows.Count > 0)
                {
                    gvDetails.DataSource = dt;
                    gvDetails.DataBind();
                    lblNumberofRecords.Text = dt.Rows.Count.ToString();
                    dt.TableName = "Table1";
                    dt.AcceptChanges();
                    ViewState["Data"] = dt;
                    System.Data.DataView view = new System.Data.DataView(dt.DefaultView.ToTable(true, "FG_ITEM_CODE"));
                    System.Data.DataTable selected =
                            view.ToTable("Table1", false, "FG_ITEM_CODE");
                    drpFilterFGItemCode.Items.Clear();
                    if (selected.Rows.Count > 0)
                    {
                        clsCommon.FillComboBox(drpFilterFGItemCode, selected, true);
                        drpFilterFGItemCode.Focus();
                    }
                }
                else
                {
                    gvDetails.DataSource = null;
                    gvDetails.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        private void EditData(int PID)
        {
            try
            {
                blobj = new BL_WIP_Serial_Generation();
                DataTable dt = blobj.GetEditData(PID);
                if (dt.Rows.Count > 0)
                {
                    hidPID.Value = dt.Rows[0][0].ToString();
                    ddllBarcodeGenerater.Text = dt.Rows[0][1].ToString();
                    ddlPlant.Text = dt.Rows[0][2].ToString();
                    BindFGItemCode(ddlPlant.Text);
                    drpfgitemCode.Text = dt.Rows[0][6].ToString();
                    BindCustomer(drpfgitemCode.Text);
                    ddlCustomer.Text = dt.Rows[0][3].ToString();
                    txtPartno.Text = dt.Rows[0][4].ToString();
                    txtPartDescription.Text = dt.Rows[0][5].ToString();
                    txtRevision.Text = dt.Rows[0][7].ToString();
                    txtFGqtyperBox.Text = dt.Rows[0][8].ToString();
                    txtStartno.Text = dt.Rows[0][9].ToString();
                    txtLength.Text = dt.Rows[0][10].ToString();
                    BindResetPeriod(ddlPlant.Text);
                    ddlRestPeriod.Text = dt.Rows[0][11].ToString();
                    txtPrefix.Text = dt.Rows[0][12].ToString();
                    txtPRN.Text = dt.Rows[0][13].ToString();
                    txtDesignerFormat.Text = dt.Rows[0][14].ToString();
                    txtOtherValue.Text = dt.Rows[0][16].ToString();
                    txtpageLabelCount.Text = dt.Rows[0][17].ToString();
                    if (dt.Rows[0][15].ToString() == "True")
                    {
                        chkActive.Checked = true;
                    }
                    else
                    {
                        chkActive.Checked = false;
                    }
                    ViewState["FORMATDATA"] = null;
                    DataTable dtAdd = new DataTable();
                    DataColumn ID = new DataColumn("P_ID", typeof(int));
                    DataColumn FORMAT_NO = new DataColumn("FORMAT_NO", typeof(String));
                    DataColumn FORMAT_NAME = new DataColumn("FORMAT_NAME", typeof(string));
                    DataColumn FORMAT_VALUE = new DataColumn("FORMAT_VALUE", typeof(string));
                    dtAdd.Columns.Add(ID);
                    dtAdd.Columns.Add(FORMAT_NO);
                    dtAdd.Columns.Add(FORMAT_NAME);
                    dtAdd.Columns.Add(FORMAT_VALUE);
                    GetPreFixData(ddlPlant.Text);
                    blobj = new BL_WIP_Serial_Generation();
                    DataTable dtt = blobj.EditFormatData(PID);
                    if (dtt.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in dtt.Rows)
                        {
                            DataRow dr = dtAdd.NewRow();
                            dr["P_ID"] = dr1["P_ID"];
                            dr["FORMAT_NO"] = dr1["FORMAT_NO"];
                            dr["FORMAT_NAME"] = dr1["FORMAT_NAME"];
                            dr["FORMAT_VALUE"] = dr1["FORMAT_VALUE"];
                            dtAdd.Rows.Add(dr);
                        }
                        ViewState["FORMATDATA"] = dtAdd;
                        gvFormatData.DataSource = dtAdd;
                        gvFormatData.DataBind();
                    }
                    else
                    {
                        gvFormatData.DataSource = null;
                        gvFormatData.DataBind();
                    }
                    ddllBarcodeGenerater.Enabled = false;
                    ddllBarcodeGenerater.CssClass = "form-control select2";
                    ddlPlant.Enabled = false;
                    ddlPlant.CssClass = "form-control select2";
                    ddlCustomer.Enabled = false;
                    ddlCustomer.CssClass = "form-control select2";
                    drpfgitemCode.Enabled = false;
                    drpfgitemCode.CssClass = "form-control select2";
                    if (dt.Rows[0][1].ToString() == "PCB")
                    {
                        lblQtyHeader.Text = "Array Size :";
                    }
                    else if (dt.Rows[0][1].ToString().Contains("PACKING"))
                    {
                        lblQtyHeader.Text = "Pack Size : ";
                    }
                    else
                    {
                        lblQtyHeader.Text = "Size : ";
                    }
                    btnGetRunningSN.Enabled = true;
                }
                else
                {

                    // lblNumberofRecords.Text = "0";
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        private void AddNewRow()
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (ddlSuffix.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select suffix.", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlSuffix.Focus();
                    return;
                }
                if (ddlFormat.Items.Count > 0)
                {
                    if (ddlFormat.SelectedIndex == 0)
                    {
                        CommonHelper.ShowMessage("Please select format.", msgerror, CommonHelper.MessageType.Error.ToString());
                        ddlFormat.Focus();
                        return;
                    }
                }
                string sFormatNo = ddlSuffix.SelectedValue.ToString();
                DataTable dtAdd;
                if (ddlSuffix.SelectedItem.ToString() == "OTHERS" && txtOtherValue.Text.Trim() == string.Empty)
                {
                    CommonHelper.ShowMessage("Others value can not be empty, Please select different one.", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlFormat.Focus();
                    return;
                }
                if (ViewState["FORMATDATA"] == null)
                {
                    dtAdd = new DataTable();
                    DataColumn ID = new DataColumn("P_ID", typeof(int));
                    DataColumn FORMAT_NO = new DataColumn("FORMAT_NO", typeof(String));
                    DataColumn FORMAT_NAME = new DataColumn("FORMAT_NAME", typeof(string));
                    DataColumn FORMAT_VALUE = new DataColumn("FORMAT_VALUE", typeof(string));
                    dtAdd.Columns.Add(ID);
                    dtAdd.Columns.Add(FORMAT_NO);
                    dtAdd.Columns.Add(FORMAT_NAME);
                    dtAdd.Columns.Add(FORMAT_VALUE);
                    DataRow dr = dtAdd.NewRow();
                    dr["P_ID"] = 1;
                    dr["FORMAT_NO"] = ddlSuffix.SelectedValue.ToString();
                    dr["FORMAT_NAME"] = ddlSuffix.SelectedItem.ToString();
                    if (ddlFormat.SelectedIndex > 0)
                    {
                        dr["FORMAT_VALUE"] = ddlFormat.Text.ToString();
                    }
                    else
                    {
                        dr["FORMAT_VALUE"] = "";
                    }
                    dtAdd.Rows.Add(dr);
                    ViewState["FORMATDATA"] = dtAdd;
                }
                else
                {
                    dtAdd = (DataTable)ViewState["FORMATDATA"];
                    bool drs = dtAdd.AsEnumerable().Any(tt => tt.Field<string>("FORMAT_NO") == sFormatNo);
                    if (drs == true)
                    {
                        CommonHelper.ShowMessage("Suffix already selected, Please select different one.", msgerror, CommonHelper.MessageType.Error.ToString());
                        ddlFormat.Focus();
                        return;
                    }
                    DataRow dr = dtAdd.NewRow();
                    dr["P_ID"] = 1;
                    dr["FORMAT_NO"] = ddlSuffix.SelectedValue.ToString();
                    dr["FORMAT_NAME"] = ddlSuffix.SelectedItem.ToString();
                    if (ddlFormat.SelectedIndex > 0)
                    {
                        dr["FORMAT_VALUE"] = ddlFormat.Text.ToString();
                    }
                    else
                    {
                        dr["FORMAT_VALUE"] = "";
                    }
                    dtAdd.Rows.Add(dr);
                    ViewState["FORMATDATA"] = dtAdd;
                }

                gvFormatData.DataSource = dtAdd;
                gvFormatData.DataBind();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void ddllBarcodeGenerater_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtFGqtyperBox.Text = "0";
                txtFGqtyperBox.Enabled = true;
                txtpageLabelCount.Text = "1";
                if (ddllBarcodeGenerater.SelectedIndex > 0)
                {
                    if (ddllBarcodeGenerater.SelectedItem.Text == "PCB")
                    {
                        lblQtyHeader.Text = "Array Size : ";
                    }
                    else if (ddllBarcodeGenerater.SelectedItem.Text.Contains("PACKING"))
                    {
                        lblQtyHeader.Text = "Pack Size : ";
                    }
                    else
                    {
                        lblQtyHeader.Text = "Size : ";
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void ddlPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (ddlPlant.SelectedIndex > 0)
                {
                    BindFGItemCode(ddlPlant.SelectedItem.Text);
                    BindResetPeriod(ddlPlant.SelectedItem.Text);
                    GetPreFixData(ddlPlant.SelectedItem.Text);
                }
                else
                {
                    ddlPlant.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void drpfgitemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlCustomer.Items.Clear();
                if (drpfgitemCode.SelectedIndex > 0)
                {
                    BindCustomer(drpfgitemCode.SelectedValue.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        protected void ddlSuffix_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (ddlSuffix.SelectedIndex > 0)
                {
                    GetFormatData(ddlSuffix.SelectedItem.Text);
                }
                else
                {
                    ddlSuffix.SelectedIndex = 0;
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
                AddNewRow();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }

        }
        protected void btnReadPRN_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (Path.GetExtension(FileUpload1.FileName).Equals(".prn"))
                {
                    string inputContent;
                    using (StreamReader inputStreamReader = new StreamReader(FileUpload1.PostedFile.InputStream))
                    {
                        inputContent = inputStreamReader.ReadToEnd();
                        txtPRN.Text = inputContent;
                    }
                }
                else
                {
                    CommonHelper.ShowMessage("Please select valid file", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPRN.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                DataTable dtAdd = new DataTable();
                if (ViewState["FORMATDATA"] == null)
                {
                    CommonHelper.ShowMessage("Please add at least one format.", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlSuffix.Focus();
                    return;
                }
                dtAdd = (DataTable)ViewState["FORMATDATA"];
                if (btnSave.Text == "Save")
                {
                    if (ddllBarcodeGenerater.SelectedIndex == 0)
                    {
                        CommonHelper.ShowMessage("Please select barcode generate for.", msgerror, CommonHelper.MessageType.Error.ToString());
                        ddllBarcodeGenerater.Focus();
                        return;
                    }
                    if (ddlPlant.SelectedIndex == 0)
                    {
                        CommonHelper.ShowMessage("Please select site code.", msgerror, CommonHelper.MessageType.Error.ToString());
                        ddlPlant.Focus();
                        return;
                    }
                    if (drpfgitemCode.SelectedIndex == 0)
                    {
                        CommonHelper.ShowMessage("Please select fg item code.", msgerror, CommonHelper.MessageType.Error.ToString());
                        drpfgitemCode.Focus();
                        return;
                    }
                    if (ddlCustomer.SelectedIndex == 0)
                    {
                        CommonHelper.ShowMessage("Please select customer.", msgerror, CommonHelper.MessageType.Error.ToString());
                        ddlCustomer.Focus();
                        return;
                    }
                }
                if (string.IsNullOrEmpty(txtPartno.Text))
                {
                    CommonHelper.ShowMessage("Please enter part no.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPartno.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtRevision.Text))
                {
                    CommonHelper.ShowMessage("Please enter revision.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtRevision.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtFGqtyperBox.Text))
                {
                    CommonHelper.ShowMessage("Please enter fg qty per box.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtFGqtyperBox.Focus();
                    return;
                }
                if (Convert.ToInt32(txtFGqtyperBox.Text) == 0)
                {
                    CommonHelper.ShowMessage("Please enter valid fg qty per box.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtFGqtyperBox.Text = string.Empty;
                    txtFGqtyperBox.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtStartno.Text))
                {
                    CommonHelper.ShowMessage("Please enter start no.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtStartno.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtLength.Text))
                {
                    CommonHelper.ShowMessage("Please enter length.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtLength.Focus();
                    return;
                }
                if (Convert.ToInt32(txtLength.Text) == 0)
                {
                    CommonHelper.ShowMessage("Please enter valid length.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtLength.Text = string.Empty;
                    txtLength.Focus();
                    return;
                }
                if (ddlRestPeriod.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select reset period.", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlRestPeriod.Focus();
                    return;
                }
                if (dtAdd.Rows.Count == 0)
                {
                    CommonHelper.ShowMessage("Please add at least one format.", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlSuffix.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtDesignerFormat.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please enter design format.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtDesignerFormat.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtpageLabelCount.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please enter page label count.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtpageLabelCount.Focus();
                    return;
                }
                if (Convert.ToInt32(txtpageLabelCount.Text.Trim()) == 0)
                {
                    CommonHelper.ShowMessage("Please enter valid page label count.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtpageLabelCount.Focus();
                    txtpageLabelCount.Text = "1";
                    return;
                }
                PL_WIP_SerialGeneration plobj = new PL_WIP_SerialGeneration();
                plobj.sBarcodeGenerationType = ddllBarcodeGenerater.SelectedValue.ToString();
                plobj.sSiteCode = ddlPlant.SelectedValue.ToString();
                plobj.sFGItemCode = drpfgitemCode.SelectedValue.ToString();
                plobj.sCustomer = ddlCustomer.Text.ToString();
                plobj.sPartCode = txtPartno.Text;
                plobj.sPartDesc = txtPartDescription.Text;
                plobj.Revision = txtRevision.Text;
                plobj.iFGQtyPerBox = Convert.ToInt32(txtFGqtyperBox.Text.Trim());
                plobj.StartNo = txtStartno.Text.Trim();
                plobj.iLength = Convert.ToInt32(txtLength.Text.Trim());
                plobj.sResetPeriod = ddlRestPeriod.SelectedValue.ToString();
                plobj.sPrefix = txtPrefix.Text;
                plobj.sSufix = "";
                plobj.sprn = txtPRN.Text;
                plobj.sOtherValue = txtOtherValue.Text;
                plobj.sDesignerFormat = txtDesignerFormat.Text.Trim();
                plobj.iPageLabelCount = Convert.ToInt32(txtpageLabelCount.Text);
                if (gvFormatData.Rows.Count == 0)
                {
                    CommonHelper.ShowMessage("Please select at least one format.", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlSuffix.Focus();
                    return;
                }
                string[] sarr = txtDesignerFormat.Text.Split('$');
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add();
                Array.ForEach(sarr, c => dataTable.Rows.Add()[0] = c);
                DataView view = new DataView(dataTable);
                dataTable = view.ToTable(true, "Column1");
                if (sarr.Length < gvFormatData.Rows.Count)
                {
                    CommonHelper.ShowMessage("Please enter all the value of displayed data in proper format(value1&value2)", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtDesignerFormat.Text = string.Empty;
                    txtDesignerFormat.Focus();
                    return;
                }
                DataTable dtMerged = (from a in dtAdd.AsEnumerable()
                                      join b in dataTable.AsEnumerable()
                                      on a["FORMAT_NO"].ToString() equals b["COLUMN1"].ToString()
                                      into g
                                      where g.Count() > 0
                                      select a).CopyToDataTable();
                if (dtMerged.Rows.Count != dataTable.Rows.Count)
                {
                    CommonHelper.ShowMessage("Please enter all the value of displayed data in proper format(value1&value2)", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtDesignerFormat.Text = string.Empty;
                    txtDesignerFormat.Focus();
                    return;
                }

                plobj.dtRecord = dtAdd;
                plobj.iActive = chkActive.Checked;
                blobj = new BL_WIP_Serial_Generation();
                DataTable dt = new DataTable();
                if (btnSave.Text == "Save")
                {
                    dt = blobj.SaveData(plobj);
                }
                else
                {
                    plobj.iPID = Convert.ToInt32(hidPID.Value);
                    dt = blobj.UpdateData(plobj);
                }
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString().StartsWith("ERROR"))
                    {
                        CommonHelper.ShowMessage(dt.Rows[0][0].ToString(), msgerror, CommonHelper.MessageType.Error.ToString());
                        btnReset_Click(null, null);
                    }
                    else
                    {
                        CommonHelper.ShowMessage(dt.Rows[0][0].ToString(), msgsuccess, CommonHelper.MessageType.Success.ToString());

                        btnReset_Click(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {
                ddllBarcodeGenerater.Enabled = true;
                ddlPlant.Enabled = true;
                ddlCustomer.Enabled = true;
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                ddlCustomer.DataSource = null;
                if (ddlCustomer.Items.Count > 0)
                {
                    ddlCustomer.Items.Clear();
                }
                drpfgitemCode.DataSource = null;
                if (drpfgitemCode.Items.Count > 0)
                {
                    drpfgitemCode.Items.Clear();
                }
                if (ddlPlant.Items.Count > 0)
                {
                    ddlPlant.SelectedIndex = 0;
                }
                if (ddllBarcodeGenerater.Items.Count > 0)
                {
                    ddllBarcodeGenerater.SelectedIndex = 0;
                }
                ddlSuffix.DataSource = null;
                if (ddlSuffix.Items.Count > 0)
                {
                    ddlSuffix.Items.Clear();
                }
                ddlRestPeriod.DataSource = null;
                if (ddlRestPeriod.Items.Count > 0)
                {
                    ddlRestPeriod.Items.Clear();
                }
                ddlCustomer.Enabled = true;
                ddllBarcodeGenerater.Enabled = true;
                ddlPlant.Enabled = true;
                drpfgitemCode.Enabled = true;
                txtPartno.Enabled = true;
                ddlFormat.DataSource = null;
                if (ddlFormat.Items.Count > 0)
                {
                    ddlFormat.Items.Clear();
                }
                txtPRN.Text = string.Empty;
                gvFormatData.DataSource = null;
                gvFormatData.DataBind();
                ViewState["FORMATDATA"] = null;
                txtDesignerFormat.Text = string.Empty;
                txtFGqtyperBox.Text = string.Empty;
                txtLength.Text = string.Empty;
                txtPartDescription.Text = string.Empty;
                txtPartno.Text = string.Empty;
                txtPrefix.Text = string.Empty;
                txtRevision.Text = string.Empty;
                txtStartno.Text = string.Empty;
                chkActive.Checked = false;
                btnSave.Text = "Save";
                txtOtherValue.Text = string.Empty;
                txtpageLabelCount.Text = "1";
                ShowGridData();
                btnGetRunningSN.Enabled = false;
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                bool bEdit = false;
                foreach (GridViewRow gvrow in gvDetails.Rows)
                {
                    if (gvrow.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chk = (gvrow.FindControl("chkSelect") as CheckBox);
                        if (chk.Checked)
                        {
                            bEdit = true;
                            int sID = Convert.ToInt32(gvrow.Cells[1].Text);
                            EditData(sID);
                            break;
                        }
                    }
                }
                if (bEdit == false)
                {
                    CommonHelper.ShowMessage("Please select one item.", msgerror, CommonHelper.MessageType.Error.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    return;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                return;
            }
        }

        protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                string _SN = string.Empty;
                _SN = e.CommandArgument.ToString();
                if (e.CommandName == "EditRecords")
                {
                    if (btnSave.Text == "Save")
                    { btnSave.Text = "Update"; }
                    EditData(Convert.ToInt32(_SN));
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void gvDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvDetails.PageIndex = e.NewPageIndex;
                ShowGridData();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void gvFormatData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable dtAdd = (DataTable)ViewState["FORMATDATA"];
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
                ViewState["FORMATDATA"] = dtAdd;
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {
                gvFormatData.DataSource = ViewState["FORMATDATA"];
                gvFormatData.DataBind();
            }
        }

        protected void drpFilterFGItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpFilterFGItemCode.SelectedIndex > 0)
                {
                    DataTable dt = (DataTable)ViewState["Data"];
                    DataView dataView = dt.DefaultView;
                    dataView.RowFilter = "FG_ITEM_CODE = '" + drpFilterFGItemCode.SelectedValue + "'";
                    gvDetails.DataSource = dataView;
                    gvDetails.DataBind();
                }
                else
                {
                    DataTable dt = (DataTable)ViewState["Data"];
                    gvDetails.DataSource = dt;
                    gvDetails.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void btnGetRunningSN_Click(object sender, EventArgs e)
        {
            try
            {
                blobj = new BL_WIP_Serial_Generation();
                PL_WIP_SerialGeneration plobj = new PL_WIP_SerialGeneration();
                plobj.sBarcodeGenerationType = ddllBarcodeGenerater.SelectedValue.ToString();
                plobj.sSiteCode = ddlPlant.SelectedValue.ToString();
                plobj.sFGItemCode = drpfgitemCode.SelectedValue.ToString();
                plobj.sCustomer = ddlCustomer.Text.ToString();
                plobj.sDesignerFormat = txtDesignerFormat.Text;
                string sRunnignSN = blobj.GetRunningSN(plobj);
                if (sRunnignSN.Length > 0)
                {
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,
                        System.Reflection.MethodBase.GetCurrentMethod().Name,
                        "Get Running SN : FG Item Code :" + plobj.sFGItemCode
                        + ", Type : " + plobj.sBarcodeGenerationType
                        + ", Customer : " + plobj.sCustomer
                        + ", Generated SN : " + sRunnignSN
                        );
                    CommonHelper.ShowMessage("Running SN is:" + sRunnignSN, msgsuccess, CommonHelper.MessageType.Notification.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void txtPrefix_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtPrefix.Text.Contains("$"))
                {
                    CommonHelper.ShowMessage("Please do not use $ key word due to use in Design format.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPrefix.Text = string.Empty;
                    txtPrefix.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}