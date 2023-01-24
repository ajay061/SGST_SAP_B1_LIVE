using BusinessLayer;
using Common;
using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using BusinessLayer.WIP;

namespace SGST.INE.WIP
{
    public partial class WIPNewQuality : System.Web.UI.Page
    {
        BL_Common objBL_Common = new BL_Common();
        DataTable dtScanData;
        DataTable dtAccessoriesData;
        static string sAccessoriesBoxID = string.Empty;

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
                    string _strRights = CommonHelper.GetRights("FG QUALITY", (DataTable)Session["USER_RIGHTS"]);
                    CommonHelper._strRights = _strRights.Split('^');
                    if (CommonHelper._strRights[0] == "0")  //Check view rights
                    {
                        Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                    }
                }
                Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
                Response.AddHeader("Pragma", "no-cache");
                Response.AddHeader("Expires", "0");
                txtSecondaryBoxID.Focus();
                if (!this.IsPostBack)
                {
                    BindFGItem();
                    BL_WipQualityNew blobj = new BL_WipQualityNew();
                    DataTable dt = blobj.BindDefect();
                    if (dt.Rows.Count > 0)
                    {
                        clsCommon.FillComboBox(drpDefect, dt, true);
                        drpDefect.SelectedIndex = 0;

                        clsCommon.FillComboBox(drpDefectAcc,dt,true);
                        drpDefectAcc.SelectedIndex = 0;
                    }
                    else
                    {
                        CommonHelper.ShowMessage("Defect not found in defect master, Please contact admin", msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void BindFGItem()
        {
            try
            {
                drpFGItemCode.Items.Clear();
                BL_WipQualityNew blobj = new BL_WipQualityNew();
                string sResult = string.Empty;
                DataTable dtFGItemCode = blobj.BindFGItemCode();
                if (dtFGItemCode.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(drpFGItemCode, dtFGItemCode, true);
                    drpFGItemCode.SelectedIndex = 0;
                    drpFGItemCode.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        protected void drpFGItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpFGItemCode.SelectedIndex > 0)
                {
                    BindFGQualityParameterList(drpFGItemCode.SelectedItem.Text);
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void BindFGQualityParameterList(string sPartCode)
        {
            try
            {
                gvQualityParameterData.DataSource = null;
                gvQualityParameterData.DataBind();
                ViewState["dtDefectData"] = null;
                BL_WipQualityNew blobj = new BL_WipQualityNew();
                DataTable dtpartCode = blobj.BindFGQualityParameterList(sPartCode);
                if (dtpartCode.Rows.Count > 0)
                {
                    ViewState["dtDefectData"] = dtpartCode;
                    CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                    gvQualityParameterData.DataSource = dtpartCode;
                    gvQualityParameterData.DataBind();
                }
                else
                {
                    CommonHelper.ShowMessage("FG Quality Parameter list not found for selected fg item code, Please select different fg item code", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpFGItemCode.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }   
        public void AddBoxPCBData(string sType)
        {
            try
            {
                if (ViewState["Row"] != null)
                {
                    dtScanData = (DataTable)ViewState["Row"];
                    DataRow dr = null;
                    if (dtScanData.Rows.Count > 0)
                    {
                        if (sType == "BOX")
                        {
                            bool BOXValidate = false;
                            BOXValidate = dtScanData.Select().ToList().Exists(row => row["PRIMARY_BOX_ID"].ToString().ToUpper() == txtSecondaryBoxID.Text);
                            if (BOXValidate)
                            {
                                CommonHelper.ShowMessage("Box barcode already scanned.", msgerror, CommonHelper.MessageType.Error.ToString());
                                txtSecondaryBoxID.Text = string.Empty;
                                txtSecondaryBoxID.Focus();
                                return;
                            }
                        }
                        else
                        {
                            bool exists = false;
                            if (rdPrimaryYes.Checked == true)
                            {
                                exists = dtScanData.Select().ToList().Exists(row => row["PCB_ID"].ToString().ToUpper() == txtPcb.Text.ToUpper());
                                if (exists)
                                {
                                    CommonHelper.ShowMessage("Child barcode already scanned.", msgerror, CommonHelper.MessageType.Error.ToString());
                                    txtPcb.Text = string.Empty;
                                    txtPcb.Focus();
                                    return;
                                }
                            }
                        }
                        dr = dtScanData.NewRow();
                        dr["PRIMARY_BOX_ID"] = txtSecondaryBoxID.Text.Trim();
                        if (sType == "BOX")
                        {
                            dr["PCB_ID"] = "";
                            dr["REMARKS"] = "";
                            dr["Code"] = "";
                            dr["Storagelocation"] = "";
                            dr["Type"] = "";
                            if (drpDefect.SelectedIndex > 0)
                            {
                                dr["DEFECT"] = drpDefect.Text;
                            }
                            else
                            {
                                dr["DEFECT"] = "";
                            }
                            dr["STATUS"] = "1";
                        }
                        else
                        {
                            dr["PCB_ID"] = txtPcb.Text.Trim();
                            dr["REMARKS"] = txtRemarks.Text;
                            dr["Code"] = "";
                            dr["Storagelocation"] = "";
                            dr["Type"] = "";
                            if (drpDefect.SelectedIndex > 0)
                            {
                                dr["DEFECT"] = drpDefect.Text;
                            }
                            else
                            {
                                dr["DEFECT"] = "";
                            }
                            if (drpStatus.Text == "OK")
                            {
                                dr["STATUS"] = "1";
                            }
                            else
                            {
                                dr["STATUS"] = "0";
                            }
                        }
                        dtScanData.Rows.Add(dr);
                        dtScanData.AcceptChanges();
                        ViewState["Row"] = dtScanData;
                        gvQualityData.DataSource = ViewState["Row"];
                        gvQualityData.DataBind();
                        DataView dtView = new DataView(dtScanData);
                        DataTable dtTableWithOneColumn = dtView.ToTable(true, "PRIMARY_BOX_ID");
                        gvBoxData.DataSource = dtTableWithOneColumn;
                        gvBoxData.DataBind();
                    }
                }
                else
                {
                    dtScanData = new DataTable();
                    dtScanData.Columns.Add("PRIMARY_BOX_ID", typeof(string));
                    dtScanData.Columns.Add("PCB_ID", typeof(string));
                    dtScanData.Columns.Add("REMARKS", typeof(string));
                    dtScanData.Columns.Add("DEFECT", typeof(string));
                    dtScanData.Columns.Add("STATUS", typeof(string));
                    dtScanData.Columns.Add("Code", typeof(string));
                    dtScanData.Columns.Add("Storagelocation", typeof(string));
                    dtScanData.Columns.Add("Type", typeof(string));

                    DataRow dr1 = dtScanData.NewRow();
                    dr1 = dtScanData.NewRow();
                    dr1["PRIMARY_BOX_ID"] = txtSecondaryBoxID.Text.Trim();
                    if (sType == "BOX")
                    {
                        dr1["PCB_ID"] = "";
                        dr1["REMARKS"] = "";
                        dr1["Code"] = "";
                        dr1["Storagelocation"] = "";
                        dr1["Type"] = "";
                        if (drpDefect.SelectedIndex > 0)
                        {
                            dr1["DEFECT"] = drpDefect.Text;
                        }
                        else
                        {
                            dr1["DEFECT"] = "";
                        }
                        dr1["STATUS"] = "1";
                    }
                    else
                    {
                        dr1["PCB_ID"] = txtPcb.Text.Trim();
                        dr1["REMARKS"] = txtRemarks.Text;
                        dr1["Code"] = "";
                        dr1["Storagelocation"] = "";
                        dr1["Type"] = "";
                        if (drpDefect.SelectedIndex > 0)
                        {
                            dr1["DEFECT"] = drpDefect.Text;
                        }
                        else
                        {
                            dr1["DEFECT"] = "";
                        }
                        if (drpStatus.Text == "OK")
                        {
                            dr1["STATUS"] = "1";
                        }
                        else
                        {
                            dr1["STATUS"] = "0";
                        }
                    }
                    dtScanData.Rows.Add(dr1);
                    ViewState["Row"] = dtScanData;
                    gvQualityData.DataSource = ViewState["Row"];
                    gvQualityData.DataBind();
                    DataView dtView = new DataView(dtScanData);
                    DataTable dtTableWithOneColumn = dtView.ToTable(true, "PRIMARY_BOX_ID");
                    gvBoxData.DataSource = dtTableWithOneColumn;
                    gvBoxData.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        protected void txtSecondaryBoxID_TextChanged(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (drpFGItemCode.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select FG Item code.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtSecondaryBoxID.Text = "";
                    drpFGItemCode.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtSecondaryBoxID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan barcode.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtSecondaryBoxID.Focus();
                    return;
                }
                else
                {
                    PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,
                         System.Reflection.MethodBase.GetCurrentMethod().Name, "WIP Quality Module : Scan Box Barcode : Box ID :  " + txtSecondaryBoxID.Text.Trim()
                         + ", FG Item Code : " + drpFGItemCode.Text
                         );
                    BL_WipQualityNew dlobj = new BL_WipQualityNew();
                    string _OuptPut = dlobj.ValidateSecondaryBarcode(txtSecondaryBoxID.Text.Trim(), drpFGItemCode.Text);
                    string[] Message = _OuptPut.Split('~');
                    if (_OuptPut.StartsWith("N~"))
                    {
                        CommonHelper.ShowMessage(Message[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        txtSecondaryBoxID.Text = string.Empty;
                        txtSecondaryBoxID.Focus();
                        return;
                    }
                    if (_OuptPut.StartsWith("ERROR~"))
                    {
                        CommonHelper.ShowMessage(Message[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        txtSecondaryBoxID.Text = string.Empty;
                        txtSecondaryBoxID.Focus();
                        return;
                    }
                    else
                    {
                        CommonHelper.ShowMessage(Message[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                        if (ViewState["Row"] != null)
                        {
                            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                            DataTable dtScanPCBBoxes = (DataTable)ViewState["Row"];
                            if (dtScanPCBBoxes.Rows.Count > 0)
                            {
                                bool BOXValidate = false;
                                BOXValidate = dtScanPCBBoxes.Select().ToList().Exists(row => row["PRIMARY_BOX_ID"].ToString().ToUpper() == txtSecondaryBoxID.Text.ToUpper());
                                if (BOXValidate)
                                {
                                    CommonHelper.ShowMessage("Box barcode already scanned.", msgerror, CommonHelper.MessageType.Error.ToString());
                                    txtSecondaryBoxID.Text = string.Empty;
                                    txtSecondaryBoxID.Focus();
                                    return;
                                }
                            }
                        }
                        CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                        AddBoxPCBData("BOX");
                        txtSecondaryBoxID.Text = string.Empty;
                        txtSecondaryBoxID.Focus();
                        rdPrimaryYes.Checked = true;
                        return;
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
        protected void btnCreateLot_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (gvBoxData.Rows.Count == 0)
                {
                    CommonHelper.ShowMessage("Please scan atleast one box", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtSecondaryBoxID.Text = string.Empty;
                    txtSecondaryBoxID.Focus();
                    return;
                }
                txtSecondaryBoxID.Enabled = false;
                txtSecondaryBoxID.Height = 35;
                if (rdPrimaryYes.Checked)
                {
                    dvPrimaryScan.Visible = true;
                    txtPcb.Focus();
                }
                else if (rdPrimaryYes.Checked == false)
                {
                    dvPrimaryScan.Visible = false;
                    btnParialOK.Visible = true;
                    btnOK.Visible = true;
                    btnReject.Visible = true;
                }
                CommonHelper.ShowMessage("Lot has been created successfully.Please scan PCB barcode or else select No for complete the quality  ", msgsuccess, CommonHelper.MessageType.Success.ToString());
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
        }

      
        private void ChangeStatus(string sStatus)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            if (gvQualityData.Rows.Count == 0)
            {
                CommonHelper.ShowMessage("Please select status.", msgerror, CommonHelper.MessageType.Error.ToString());
                return;
            }
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "No")
            {
                return;
            }
            try
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,
                       System.Reflection.MethodBase.GetCurrentMethod().Name, "WIP Quality Module : Check View State for Barcode Scanned ");
                if (ViewState["Row"] == null)
                {
                    CommonHelper.ShowMessage("Please scan at least one box", msgerror, CommonHelper.MessageType.Error.ToString());
                    return;
                }
                DataTable dtScannedBoxPCB = (DataTable)ViewState["Row"];
                if (dtScannedBoxPCB.Rows.Count > 0)
                {
                    DataRow[] foundAuthors = dtScannedBoxPCB.Select("STATUS = '0'");
                    if (foundAuthors.Length == 0)
                    {
                        if (sStatus == "3")
                        {
                            CommonHelper.ShowMessage("Scanned boxes does not contains any rejected PCB, Please select either OK or Reject", msgerror, CommonHelper.MessageType.Error.ToString());
                            return;
                        }
                    }
                    PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,
                      System.Reflection.MethodBase.GetCurrentMethod().Name, "WIP Quality Module : Check Quality Parameter List ");
                    if (ViewState["dtDefectData"] == null)
                    {
                        CommonHelper.ShowMessage("Quality Parameter List not found", msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                    #region Quality Parameter Added while save in database           
                    DataRow dr;
                    DataTable dt = (DataTable)ViewState["dtDefectData"]; // Defect Quality Parameters
                    DataTable dtQualityParameter = new DataTable();
                    dtQualityParameter.Columns.Add(new System.Data.DataColumn("ITEM_CODE", typeof(String)));
                    dtQualityParameter.Columns.Add(new System.Data.DataColumn("ITEM_DESC", typeof(String)));
                    dtQualityParameter.Columns.Add(new System.Data.DataColumn("SPECIFICATION", typeof(String)));
                    dtQualityParameter.Columns.Add(new System.Data.DataColumn("ZONE", typeof(String)));
                    dtQualityParameter.Columns.Add(new System.Data.DataColumn("METHOD", typeof(String)));
                    dtQualityParameter.Columns.Add(new System.Data.DataColumn("RESULT_1", typeof(String)));
                    dtQualityParameter.Columns.Add(new System.Data.DataColumn("RESULT_2", typeof(String)));
                    dtQualityParameter.Columns.Add(new System.Data.DataColumn("RESULT_3", typeof(String)));
                    dtQualityParameter.Columns.Add(new System.Data.DataColumn("RESULT_4", typeof(String)));
                    dtQualityParameter.Columns.Add(new System.Data.DataColumn("RESULT_5", typeof(String)));
                    dtQualityParameter.Columns.Add(new System.Data.DataColumn("REMARKS", typeof(String)));
                    foreach (DataRow dr1 in dt.Rows)
                    {
                        dr = dtQualityParameter.NewRow();
                        dr[0] = drpFGItemCode.SelectedItem.Text;
                        dr[1] = dr1.ItemArray[3].ToString();
                        dr[2] = dr1.ItemArray[5].ToString();
                        dr[3] = dr1.ItemArray[4].ToString();
                        dr[4] = dr1.ItemArray[6].ToString();
                        dr[5] = dr1.ItemArray[7].ToString();
                        dr[6] = dr1.ItemArray[8].ToString();
                        dr[7] = dr1.ItemArray[9].ToString();
                        dr[8] = dr1.ItemArray[10].ToString();
                        dr[9] = dr1.ItemArray[11].ToString();
                        dr[10] = txtRemarks.Text;
                        dtQualityParameter.Rows.Add(dr);
                    }
                    if (dtQualityParameter.Rows.Count == 0)
                    {
                        CommonHelper.ShowMessage("Please enter parameters, Please contact admin.", msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                    #endregion
                    DataTable dtAccessoriesData = new DataTable();
                    if (ViewState["ACCESSORIESDATA"] != null)
                    {
                        dtAccessoriesData = (DataTable)ViewState["ACCESSORIESDATA"];
                    }
                    else
                    {
                        dtAccessoriesData.Columns.Add("PRIMARY_BOX_ID", typeof(string));
                        dtAccessoriesData.Columns.Add("PART_BARCODE", typeof(string));
                        dtAccessoriesData.Columns.Add("ACC_BARCODE", typeof(string));
                        dtAccessoriesData.Columns.Add("REMARKS", typeof(string));
                        dtAccessoriesData.Columns.Add("DEFECT", typeof(string));
                        dtAccessoriesData.Columns.Add("STATUS", typeof(string));
                    }

                    string SCANBY = Session["UserID"].ToString();
                    string SITECODE = Session["SITECODE"].ToString();
                    BL_WipQualityNew blobj = new BL_WipQualityNew();
                    string _OuptPut = string.Empty;
                    PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,
                      System.Reflection.MethodBase.GetCurrentMethod().Name, "WIP Quality Module : Save Quality Data ");
                    DataTable dtResult = blobj.SaveQualityData(dtScannedBoxPCB, sStatus, "WIP", SCANBY, drpFGItemCode.Text,
                        SITECODE, dtQualityParameter, Session["LINECODE"].ToString(), txtFinalRemarks.Text, dtAccessoriesData);
                    if (dtResult.Rows.Count > 0)
                    {
                        _OuptPut = dtResult.Rows[0][0].ToString();
                        PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,
                        System.Reflection.MethodBase.GetCurrentMethod().Name, "WIP Quality Module : Quality Result found : " + _OuptPut);
                        string[] Message = _OuptPut.Split('~');
                        if (_OuptPut.StartsWith("N~"))
                        {
                            CommonHelper.ShowMessage(Message[1], msgerror, CommonHelper.MessageType.Error.ToString());
                            txtSecondaryBoxID.Text = string.Empty;
                            txtSecondaryBoxID.Focus();
                            btnOK.Visible = true;
                            btnReject.Visible = true;
                            btnParialOK.Visible = true;
                            btnRework.Visible = true;
                        }
                        if (_OuptPut.StartsWith("ERROR~"))
                        {
                            CommonHelper.ShowMessage(Message[1], msgerror, CommonHelper.MessageType.Error.ToString());
                            txtSecondaryBoxID.Text = string.Empty;
                            txtSecondaryBoxID.Focus();
                            btnOK.Visible = true;
                            btnReject.Visible = true;
                            btnParialOK.Visible = true;
                            btnRework.Visible = true;
                        }
                        else
                        {
                            CommonHelper.ShowMessage(Message[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                            txtSecondaryBoxID.Text = string.Empty;
                            txtSecondaryBoxID.Focus();
                            txtSecondaryBoxID.Enabled = true;
                            txtSecondaryBoxID.ReadOnly = false;
                            DataTable dtReset = new DataTable();
                            gvQualityData.DataSource = dtReset;
                            gvQualityData.DataBind();
                            gvBoxData.DataSource = null;
                            gvBoxData.DataBind();
                            gvAccessoreisData.DataSource = null;
                            gvAccessoreisData.DataBind();
                            btnOK.Visible = true;
                            btnReject.Visible = true;
                            btnParialOK.Visible = true;
                            btnRework.Visible = true;
                            drpDefect.SelectedIndex = 0;   

                            ViewState["Row"] = false;
                            ViewState["Row"] = null;
                            ViewState["ACCESSORIESDATA"] = false;
                            ViewState["ACCESSORIESDATA"] = null;

                            
                            dvPrimaryScan.Visible = false;
                            dvAccessoriesBarcode.Visible = false;
                            hidCode.Value = "";
                            BindFGQualityParameterList(drpFGItemCode.Text);
                            txtRemarks.Text = string.Empty;
                            txtAccRemarks.Text = string.Empty;
                            drpAccResult.SelectedIndex = 0;
                            drpDefectAcc.SelectedIndex = 0;
                            rdAccBarcodeNo.Checked = true;
                            rdPrimaryNo.Checked = true;
                        }
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

        protected void btnOk_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);            
            ChangeStatus("1");
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            ChangeStatus("2");
        }
        protected void btnParialOK_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            ChangeStatus("3");
        }
        protected void btnRework_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            ChangeStatus("4");
        }


        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                txtSecondaryBoxID.Enabled = true;
                txtSecondaryBoxID.ReadOnly = false;
                txtSecondaryBoxID.Focus();
                txtSecondaryBoxID.Text = "";
                drpDefect.SelectedIndex = 0;
                txtRemarks.Text = "";
                drpStatus.SelectedIndex = 0;
                gvBoxData.DataSource = null;
                gvBoxData.DataBind();
                ViewState["Row"] = null;
                ViewState["ACCESSORIESDATA"] = false;
                ViewState["ACCESSORIESDATA"] = null;
                gvQualityData.DataSource = null;
                gvQualityData.DataBind();
                ViewState["dtDefectData"] = null;
                gvQualityParameterData.DataSource = null;
                gvQualityParameterData.DataBind();
                gvAccessoreisData.DataSource = null;
                gvAccessoreisData.DataBind();
                txtRemarks.Text = string.Empty;
                txtAccessoriesbarcode.Text = string.Empty;
                txtAccRemarks.Text = string.Empty;
                drpDefectAcc.SelectedIndex = 0;
                drpAccResult.SelectedIndex = 0;
                dvAccessoriesBarcode.Visible = false;
                rdAccBarcodeNo.Checked = true;
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void btnResetData_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/INE/WIP/WIPNewQuality.aspx");
        }

       

        #region Defect Paramter GridView Methods

        public void DatabindQualityParameter()
        {
            DataTable dt = (DataTable)ViewState["dtDefectData"];
            gvQualityParameterData.DataSource = dt;
            gvQualityParameterData.DataBind();
        }
        protected void gvMultiDefect_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                gvQualityParameterData.EditIndex = -1;
                DatabindQualityParameter();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void gvMultiDefect_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                gvQualityParameterData.EditIndex = e.NewEditIndex;
                DatabindQualityParameter();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void gvMultiDefect_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)gvQualityParameterData.Rows[e.RowIndex];
                string Zone = ((Label)gvQualityParameterData.Rows[e.RowIndex].FindControl("ZONE")).Text;
                string T1 = ((TextBox)gvQualityParameterData.Rows[e.RowIndex].FindControl("txtt1")).Text;
                string T2 = ((TextBox)gvQualityParameterData.Rows[e.RowIndex].FindControl("txtt2")).Text;
                string T3 = ((TextBox)gvQualityParameterData.Rows[e.RowIndex].FindControl("txtT3")).Text;
                string T4 = ((TextBox)gvQualityParameterData.Rows[e.RowIndex].FindControl("txtt4")).Text;
                string T5 = ((TextBox)gvQualityParameterData.Rows[e.RowIndex].FindControl("txtT5")).Text;
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["dtDefectData"];
                DataRow[] datarow = dt.Select("ZONE='" + Zone + "'");
                dt.Rows[e.RowIndex].BeginEdit();
                dt.Rows[e.RowIndex]["T1"] = T1;
                dt.Rows[e.RowIndex]["T2"] = T2;
                dt.Rows[e.RowIndex]["T3"] = T3;
                dt.Rows[e.RowIndex]["T4"] = T4;
                dt.Rows[e.RowIndex]["T5"] = T5;
                dt.Rows[e.RowIndex].EndEdit();
                dt.AcceptChanges();
                gvQualityParameterData.EditIndex = -1;
                gvQualityParameterData.DataSource = dt;
                gvQualityParameterData.DataBind();
                ViewState["dtDefectData"] = dt;
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        #endregion

        #region PCB Panel 

        private void checkPCBBarcode()
        {
            btnParialOK.Visible = false;
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (drpFGItemCode.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select fg item code.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtSecondaryBoxID.Text = "";
                    drpFGItemCode.Focus();
                    return;
                }
                if (gvBoxData.Rows.Count == 0)
                {
                    CommonHelper.ShowMessage("Please scan box", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtSecondaryBoxID.Focus();
                    txtPcb.Text = string.Empty;
                    return;
                }
                if (txtPcb.Text.Trim() == string.Empty)
                {
                    CommonHelper.ShowMessage("Please scan pcb/IMEI barcode.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPcb.Focus();
                    txtPcb.Text = string.Empty;
                    return;
                }
                else
                {
                    string PCBBARCODE = txtPcb.Text;
                    string FGITEMCODE = drpFGItemCode.Text;
                    BL_WipQualityNew dlobj = new BL_WipQualityNew();
                    string _OuptPut = dlobj.CheckPCBBarcode(PCBBARCODE, FGITEMCODE, "");
                    string[] Message = _OuptPut.Split('~');
                    if (_OuptPut.StartsWith("N~"))
                    {
                        CommonHelper.ShowMessage(Message[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        txtPcb.Text = "";
                        txtPcb.Focus();
                        return;
                    }
                    if (_OuptPut.StartsWith("ERROR~"))
                    {
                        CommonHelper.ShowMessage(Message[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        txtPcb.Text = "";
                        txtPcb.Focus();
                        return;
                    }
                    else
                    {
                        string sSecondaryBox = _OuptPut.Split('~')[2].ToString();
                        if (ViewState["Row"] != null)
                        {
                            DataTable dtScanPCBBoxes = (DataTable)ViewState["Row"];
                            DataRow[] foundAuthors = dtScanPCBBoxes.Select("PRIMARY_BOX_ID = '" + sSecondaryBox + "'");
                            if (foundAuthors.Length == 0)
                            {
                                CommonHelper.ShowMessage("Scanned PCB barcode not found in scanned boxes.", msgerror, CommonHelper.MessageType.Error.ToString());
                                txtPcb.Text = string.Empty;
                                txtPcb.Focus();
                                return;
                            }
                        }
                        CommonHelper.ShowMessage(Message[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                        btnOK.Visible = true;
                        btnReject.Visible = true;
                        btnParialOK.Visible = true;
                        txtRemarks.Focus();
                        txtRemarks.Text = "";
                        txtSecondaryBoxID.Text = sSecondaryBox;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                txtPcb.Text = "";
                txtPcb.Focus();
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
        }
        protected void txtPcb_TextChanged(object sender, EventArgs e)
        {
            checkPCBBarcode();
        }

        protected void drpStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpStatus.SelectedIndex > 0)
                {
                    btnOK.Visible = true;
                    btnReject.Visible = true;
                    btnParialOK.Visible = true;
                    CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                    if (drpFGItemCode.SelectedIndex <= 0)
                    {
                        CommonHelper.ShowMessage("Please select FG item Code", msgerror, CommonHelper.MessageType.Error.ToString());
                        drpStatus.SelectedIndex = 0;
                        return;
                    }
                    if (gvBoxData.Rows.Count == 0)
                    {
                        CommonHelper.ShowMessage("Please scan secondary box", msgerror, CommonHelper.MessageType.Error.ToString());
                        txtSecondaryBoxID.Focus();
                        txtSecondaryBoxID.Text = string.Empty;
                        txtPcb.Text = string.Empty;
                        return;
                    }
                    if (rdPrimaryYes.Checked)
                    {
                        if (txtPcb.Text.Trim() == string.Empty)
                        {
                            CommonHelper.ShowMessage("Please scan child barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                            drpStatus.SelectedIndex = 0;
                            txtPcb.Focus();
                            return;
                        }
                        AddBoxPCBData("");
                        txtSecondaryBoxID.Text = string.Empty;
                        txtPcb.Text = "";
                        txtPcb.Focus();
                        txtRemarks.Text = string.Empty;
                        drpDefect.SelectedIndex = 0;
                        drpStatus.SelectedIndex = 0;
                        btnOK.Enabled = true;
                        btnReject.Enabled = true;
                        btnParialOK.Enabled = true;
                        btnOK.Visible = true;
                        btnReject.Visible = true;
                        btnParialOK.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
        }

        protected void Group1_CheckedChanged(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            if (gvBoxData.Rows.Count == 0)
            {
                CommonHelper.ShowMessage("Please scan atleast one box", msgerror, CommonHelper.MessageType.Error.ToString());
                txtSecondaryBoxID.Text = string.Empty;
                txtSecondaryBoxID.Focus();
                return;
            }
            if (txtSecondaryBoxID.Enabled == true)
            {
                CommonHelper.ShowMessage("Please create the Lot first", msgerror, CommonHelper.MessageType.Error.ToString());
                btnCreateLot.Focus();
                return;
            }
            txtPcb.Text = string.Empty;
            if (rdPrimaryYes.Checked)
            {
                txtPcb.Focus();
                dvPrimaryScan.Visible = true;
            }
            if (rdPrimaryNo.Checked)
            {
                dvPrimaryScan.Visible = false;
                txtSecondaryBoxID.Focus();
            }
            btnOK.Visible = true;
            btnReject.Visible = true;
            btnParialOK.Visible = true;
            btnResetbarcode.Visible = true;
        }

        #endregion

        #region Accessories Group Box

        public void AddAccessoriesData()
        {
            try
            {
                if (ViewState["ACCESSORIESDATA"] != null)
                {
                    dtAccessoriesData = (DataTable)ViewState["ACCESSORIESDATA"];
                    DataRow dr = null;
                    if (dtAccessoriesData.Rows.Count > 0)
                    {
                        bool exists = false;
                        if (rdAccBarcodeYes.Checked == true)
                        {
                            exists = dtAccessoriesData.Select().ToList().Exists(row => row["ACC_BARCODE"].ToString().ToUpper() == txtAccessoriesbarcode.Text.ToUpper());
                            if (exists)
                            {
                                CommonHelper.ShowMessage("Accessories barcode already scanned.", msgerror, CommonHelper.MessageType.Error.ToString());
                                txtAccessoriesbarcode.Text = string.Empty;
                                txtAccessoriesbarcode.Focus();
                                return;
                            }
                        }
                        dr = dtAccessoriesData.NewRow();
                        dr["PRIMARY_BOX_ID"] = sAccessoriesBoxID;
                        dr["PART_BARCODE"] = "";
                        dr["ACC_BARCODE"] = txtAccessoriesbarcode.Text.Trim();
                        dr["REMARKS"] = txtAccRemarks.Text;                     
                        if (drpDefectAcc.SelectedIndex > 0)
                        {
                            dr["DEFECT"] = drpDefectAcc.Text;
                        }
                        else
                        {
                            dr["DEFECT"] = "";
                        }
                        if (drpAccResult.Text == "OK")
                        {
                            dr["STATUS"] = "1";
                        }
                        else
                        {
                            dr["STATUS"] = "0";
                        }
                        dtAccessoriesData.Rows.Add(dr);
                        dtAccessoriesData.AcceptChanges();
                        ViewState["ACCESSORIESDATA"] = dtAccessoriesData;
                        gvAccessoreisData.DataSource = dtAccessoriesData;
                        gvAccessoreisData.DataBind();
                    }
                }
                else
                {
                    dtAccessoriesData = new DataTable();
                    dtAccessoriesData.Columns.Add("PRIMARY_BOX_ID", typeof(string));
                    dtAccessoriesData.Columns.Add("PART_BARCODE", typeof(string));
                    dtAccessoriesData.Columns.Add("ACC_BARCODE", typeof(string));
                    dtAccessoriesData.Columns.Add("REMARKS", typeof(string));
                    dtAccessoriesData.Columns.Add("DEFECT", typeof(string));
                    dtAccessoriesData.Columns.Add("STATUS", typeof(string));

                    DataRow dr1 = dtAccessoriesData.NewRow();
                    dr1 = dtAccessoriesData.NewRow();
                    dr1["PRIMARY_BOX_ID"] = sAccessoriesBoxID;
                    dr1["PART_BARCODE"] = "";
                    dr1["ACC_BARCODE"] = txtAccessoriesbarcode.Text.Trim();
                    dr1["REMARKS"] = txtAccRemarks.Text;                   
                    if (drpDefectAcc.SelectedIndex > 0)
                    {
                        dr1["DEFECT"] = drpDefectAcc.Text;
                    }
                    else
                    {
                        dr1["DEFECT"] = "";
                    }
                    if (drpAccResult.Text == "OK")
                    {
                        dr1["STATUS"] = "1";
                    }
                    else
                    {
                        dr1["STATUS"] = "0";
                    }
                    dtAccessoriesData.Rows.Add(dr1);
                    dtAccessoriesData.AcceptChanges();
                    ViewState["ACCESSORIESDATA"] = dtAccessoriesData;
                    gvAccessoreisData.DataSource = dtAccessoriesData;
                    gvAccessoreisData.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        private void checkAccessories()
        {
            btnParialOK.Visible = false;
            sAccessoriesBoxID = string.Empty;
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (drpFGItemCode.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select fg item code.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtSecondaryBoxID.Text = "";
                    drpFGItemCode.Focus();
                    return;
                }
                if (gvBoxData.Rows.Count == 0)
                {
                    CommonHelper.ShowMessage("Please scan box", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtSecondaryBoxID.Focus();
                    txtPcb.Text = string.Empty;
                    return;
                }
                if (txtAccessoriesbarcode.Text.Trim() == string.Empty)
                {
                    CommonHelper.ShowMessage("Please scan Accessories barcode.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtAccessoriesbarcode.Focus();
                    txtAccessoriesbarcode.Text = string.Empty;
                    return;
                }
                else
                {
                    string PCBBARCODE = txtAccessoriesbarcode.Text;
                    string FGITEMCODE = drpFGItemCode.Text;
                    BL_WipQualityNew dlobj = new BL_WipQualityNew();
                    DataTable dtAccessoriesBarcodeResult = dlobj.CheckAccessoriesBarcode(PCBBARCODE, FGITEMCODE);
                    if (dtAccessoriesBarcodeResult.Rows.Count > 0)
                    {
                        string _OuptPut = dtAccessoriesBarcodeResult.Rows[0][0].ToString();
                        if (_OuptPut.StartsWith("N~") || _OuptPut.StartsWith("ERROR~"))
                        {
                            CommonHelper.ShowMessage(_OuptPut.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                            txtAccessoriesbarcode.Text = "";
                            txtAccessoriesbarcode.Focus();
                            return;
                        }
                        else
                        {
                            DataRow[] foundAuthors = null;
                            string sSecondaryBox = string.Empty;
                            if (ViewState["Row"] != null)
                            {
                                DataTable dtScanPCBBoxes = (DataTable)ViewState["Row"];
                                for (int i = 0; i < dtAccessoriesBarcodeResult.Rows.Count; i++)
                                {
                                    sSecondaryBox = dtAccessoriesBarcodeResult.Rows[0][0].ToString();
                                    foundAuthors = dtScanPCBBoxes.Select("PRIMARY_BOX_ID = '" + sSecondaryBox + "'");
                                    if (foundAuthors.Length > 0)
                                    {
                                        sAccessoriesBoxID = sSecondaryBox;
                                        break;
                                    }
                                }
                            }
                            if (foundAuthors.Length == 0)
                            {
                                CommonHelper.ShowMessage("Scanned Accessories barcode not found in scanned boxes.", msgerror, CommonHelper.MessageType.Error.ToString());
                                txtAccessoriesbarcode.Text = string.Empty;
                                txtAccessoriesbarcode.Focus();
                                return;
                            }
                            CommonHelper.ShowMessage("Accessories Barcode is ok", msgsuccess, CommonHelper.MessageType.Success.ToString());
                            btnOK.Visible = true;
                            btnReject.Visible = true;
                            btnParialOK.Visible = true;
                            txtAccRemarks.Focus();
                            txtAccRemarks.Text = "";
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                txtAccessoriesbarcode.Text = "";
                txtAccessoriesbarcode.Focus();
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
        }
        protected void rdAccBarcode_CheckedChanged(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            if (gvBoxData.Rows.Count == 0)
            {
                CommonHelper.ShowMessage("Please scan atleast one box", msgerror, CommonHelper.MessageType.Error.ToString());
                txtSecondaryBoxID.Text = string.Empty;
                txtSecondaryBoxID.Focus();
                return;
            }
            if (txtSecondaryBoxID.Enabled == true)
            {
                CommonHelper.ShowMessage("Please create the Lot first", msgerror, CommonHelper.MessageType.Error.ToString());
                btnCreateLot.Focus();
                return;
            }
            txtAccRemarks.Text = string.Empty;
            if (rdAccBarcodeYes.Checked)
            {
                txtAccRemarks.Focus();
                dvAccessoriesBarcode.Visible = true;
            }
            if (rdAccBarcodeNo.Checked)
            {
                txtAccRemarks.Focus();
                dvAccessoriesBarcode.Visible = false;
            }
            btnOK.Visible = true;
            btnReject.Visible = true;
            btnParialOK.Visible = true;
            btnResetbarcode.Visible = true;
        }


        protected void txtAccessoriesbarcode_TextChanged(object sender, EventArgs e)
        {
            checkAccessories();
        }

        protected void drpAccResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpAccResult.SelectedIndex > 0)
                {
                    btnOK.Visible = true;
                    btnReject.Visible = true;
                    btnParialOK.Visible = true;
                    CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                    if (drpFGItemCode.SelectedIndex <= 0)
                    {
                        CommonHelper.ShowMessage("Please select FG item Code", msgerror, CommonHelper.MessageType.Error.ToString());
                        drpStatus.SelectedIndex = 0;
                        return;
                    }
                    if (gvBoxData.Rows.Count == 0)
                    {
                        CommonHelper.ShowMessage("Please scan secondary box", msgerror, CommonHelper.MessageType.Error.ToString());
                        txtSecondaryBoxID.Focus();
                        txtSecondaryBoxID.Text = string.Empty;
                        txtPcb.Text = string.Empty;
                        return;
                    }
                    if (rdAccBarcodeYes.Checked)
                    {
                        if (txtAccessoriesbarcode.Text.Trim() == string.Empty)
                        {
                            CommonHelper.ShowMessage("Please scan accessories barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                            drpAccResult.SelectedIndex = 0;
                            txtAccessoriesbarcode.Focus();
                            return;
                        }
                        AddAccessoriesData();
                        txtSecondaryBoxID.Text = string.Empty;
                        txtAccessoriesbarcode.Text = "";
                        txtAccessoriesbarcode.Focus();
                        txtAccRemarks.Text = string.Empty;
                        drpAccResult.SelectedIndex = 0;
                        drpDefectAcc.SelectedIndex = 0;
                        btnOK.Enabled = true;
                        btnReject.Enabled = true;
                        btnParialOK.Enabled = true;
                        btnOK.Visible = true;
                        btnReject.Visible = true;
                        btnParialOK.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
        }

        #endregion
    }
}