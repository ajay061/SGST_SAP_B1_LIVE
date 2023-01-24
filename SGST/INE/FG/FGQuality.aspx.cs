using BusinessLayer;
using Common;
using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace SGST.INE.Operation
{
    public partial class Quality : System.Web.UI.Page
    {
        BL_Common objBL_Common = new BL_Common();
        DataTable dtScanData;
        DataTable dtScanPCBBoxes;
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
                    string _strRights = CommonHelper.GetRights("DOC QUALITY", (DataTable)Session["USER_RIGHTS"]);
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
                    BindDefect();
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
                BL_FGQuality blobj = new BL_FGQuality();
                string sResult = string.Empty;
                DataTable dt = blobj.BindDefect(Session["SiteCode"].ToString());
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(drpDefect, dt, true);
                    drpDefect.SelectedIndex = 0;
                    drpDefect.Focus();
                }
                else
                {
                    CommonHelper.ShowMessage("Defect not found in defect master, Please contact admin", msgerror, CommonHelper.MessageType.Error.ToString());
                    return;
                }
            }
            catch (Exception ex)
            {
                //CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }

        private void BindFGItem()
        {
            try
            {
                drpFGItemCode.Items.Clear();
                BL_FGQuality blobj = new BL_FGQuality();
                string sResult = string.Empty;
                DataTable dtFGItemCode = blobj.BindFGItemCode(Session["SiteCode"].ToString());
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

        private void checkPCB()
        {
            btnParialOK.Visible = false;
            btnOK.Visible = false;
            btnReject.Visible = false;
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (drpFGItemCode.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select FG Item code.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtSecondaryBoxID.Text = "";
                    drpFGItemCode.Focus();
                    return;
                }
                if (gvBoxData.Rows.Count == 0)
                {
                    CommonHelper.ShowMessage("Please scan secondary box", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtSecondaryBoxID.Focus();
                    txtPcb.Text = string.Empty;
                    return;
                }
                if (txtPcb.Text.Trim() == string.Empty)
                {
                    CommonHelper.ShowMessage("Please scan PCB barcode.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPcb.Focus();
                    txtPcb.Text = string.Empty;
                    return;
                }
                else
                {
                    string PCBBARCODE = txtPcb.Text;
                    string FGITEMCODE = drpFGItemCode.Text;
                    BL_FGQuality blobj = new BL_FGQuality();
                    string _OuptPut = blobj.CheckPCBBarcode(PCBBARCODE, FGITEMCODE,Session["SiteCode"].ToString());
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
                        if (ViewState["Row"] != null && ViewState["BOX"] != null)
                        {
                            dtScanPCBBoxes = (DataTable)ViewState["BOX"];
                            DataRow[] foundAuthors = dtScanPCBBoxes.Select("SECONDAR_BOX_ID = '" + sSecondaryBox + "'");
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

        public void AddBoxPCBData(string sType)
        {
            try
            {
                if (ViewState["Row"] != null && ViewState["BOX"] != null)
                {
                    dtScanData = (DataTable)ViewState["Row"];
                    dtScanPCBBoxes = (DataTable)ViewState["BOX"];
                    DataRow dr = null;
                    DataRow dr2 = null;
                    if (dtScanData.Rows.Count > 0)
                    {
                        if (sType == "BOX")
                        {
                            bool BOXValidate = false;
                            BOXValidate = dtScanPCBBoxes.Select().ToList().Exists(row => row["SECONDAR_BOX_ID"].ToString().ToUpper() == txtSecondaryBoxID.Text);
                            if (BOXValidate)
                            {
                                CommonHelper.ShowMessage("Secondary barcode already scanned.", msgerror, CommonHelper.MessageType.Error.ToString());
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
                                exists = dtScanData.Select().ToList().Exists(row => row["PCB_ID"].ToString().ToUpper() == txtPcb.Text);
                                if (exists)
                                {
                                    CommonHelper.ShowMessage("PCB barcode already scanned.", msgerror, CommonHelper.MessageType.Error.ToString());
                                    txtPcb.Text = string.Empty;
                                    txtPcb.Focus();
                                    return;
                                }
                            }
                        }
                        dr = dtScanData.NewRow();
                        dr2 = dtScanPCBBoxes.NewRow();
                        dr["SECONDAR_BOX_ID"] = txtSecondaryBoxID.Text.Trim();
                        dr2["SECONDAR_BOX_ID"] = txtSecondaryBoxID.Text.Trim();
                        if (sType == "BOX")
                        {
                            dr["PRIMARY_BOX_ID"] = "";
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
                            dr["PRIMARY_BOX_ID"] = "";
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
                        dtScanPCBBoxes.Rows.Add(dr2);
                        dtScanPCBBoxes.AcceptChanges();
                        ViewState["Row"] = dtScanData;
                        gvQualityData.DataSource = ViewState["Row"];
                        gvQualityData.DataBind();
                        ViewState["BOX"] = dtScanPCBBoxes;
                        gvBoxData.DataSource = ViewState["BOX"];
                        gvBoxData.DataBind();
                    }
                }
                else
                {
                    dtScanData = new DataTable();
                    dtScanPCBBoxes = new DataTable();
                    dtScanPCBBoxes.Columns.Add("SECONDAR_BOX_ID", typeof(string));
                    dtScanData.Columns.Add("SECONDAR_BOX_ID", typeof(string));
                    dtScanData.Columns.Add("PRIMARY_BOX_ID", typeof(string));
                    dtScanData.Columns.Add("PCB_ID", typeof(string));
                    dtScanData.Columns.Add("REMARKS", typeof(string));
                    dtScanData.Columns.Add("DEFECT", typeof(string));
                    dtScanData.Columns.Add("STATUS", typeof(string));
                    dtScanData.Columns.Add("Code", typeof(string));
                    dtScanData.Columns.Add("Storagelocation", typeof(string));
                    dtScanData.Columns.Add("Type", typeof(string));

                    DataRow dr1 = dtScanData.NewRow();
                    DataRow dr2 = dtScanPCBBoxes.NewRow();
                    dr1 = dtScanData.NewRow();
                    dr2 = dtScanPCBBoxes.NewRow();
                    dr1["SECONDAR_BOX_ID"] = txtSecondaryBoxID.Text.Trim();
                    dr2["SECONDAR_BOX_ID"] = txtSecondaryBoxID.Text.Trim();
                    if (sType == "BOX")
                    {
                        dr1["PRIMARY_BOX_ID"] = "";
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
                        dr1["PRIMARY_BOX_ID"] = "";
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
                    dtScanPCBBoxes.Rows.Add(dr2);
                    ViewState["BOX"] = dtScanPCBBoxes;
                    ViewState["Row"] = dtScanData;
                    gvQualityData.DataSource = ViewState["Row"];
                    gvQualityData.DataBind();
                    gvBoxData.DataSource = ViewState["BOX"];
                    gvBoxData.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }

        public void createnewrow()
        {
            if (ViewState["Row"] != null)
            {
                dtScanData = (DataTable)ViewState["Row"];
                DataRow dr = null;
                if (dtScanData.Rows.Count > 0)
                {
                    bool exists = false;
                    if (rdPrimaryYes.Checked == true)
                    {
                        exists = dtScanData.Select().ToList().Exists(row => row["PCB_ID"].ToString().ToUpper() == txtPcb.Text);
                        if (exists)
                        {
                            CommonHelper.ShowMessage("PCB barcode already scanned.", msgerror, CommonHelper.MessageType.Error.ToString());
                            txtPcb.Text = string.Empty;
                            txtPcb.Focus();
                            return;
                        }
                    }
                    dr = dtScanData.NewRow();
                    dr["SECONDAR_BOX_ID"] = txtSecondaryBoxID.Text;
                    dr["PRIMARY_BOX_ID"] = "";
                    dr["PCB_ID"] = txtPcb.Text;
                    dr["REMARKS"] = txtRemarks.Text;
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
                        dr["STATUS"] = "2"; // 2 for rejected PCB
                    }
                    dtScanData.Rows.Add(dr);
                    ViewState["Row"] = dtScanData;
                    gvQualityData.DataSource = ViewState["Row"];
                    gvQualityData.DataBind();
                    ViewState["Row"] = dtScanData;
                }
            }
            else
            {
                dtScanData = new DataTable();
                dtScanData.Columns.Add("SECONDAR_BOX_ID", typeof(string));
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
                dr1["SECONDAR_BOX_ID"] = txtSecondaryBoxID.Text.Trim();
                dr1["PRIMARY_BOX_ID"] = "";
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
                dtScanData.Rows.Add(dr1);
                ViewState["Row"] = dtScanData;
                gvQualityData.DataSource = ViewState["Row"];
                gvQualityData.DataBind();
            }
        }
        protected void txtSecondaryBoxID_TextChanged(object sender, EventArgs e)
        {
            btnParialOK.Visible = false;
            btnOK.Visible = false;
            btnReject.Visible = false;
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (drpFGItemCode.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select FG Item code.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtSecondaryBoxID.Text = "";
                    drpFGItemCode.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtSecondaryBoxID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan Secondary barcode.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtSecondaryBoxID.Focus();
                    return;
                }
                else
                {
                    BL_FGQuality dlobj = new BL_FGQuality();
                    string _OuptPut = dlobj.ValidateSecondaryBarcode(txtSecondaryBoxID.Text.Trim(), drpFGItemCode.Text, Session["SiteCode"].ToString());
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
                        if (ViewState["BOX"] != null)
                        {
                            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                            dtScanPCBBoxes = (DataTable)ViewState["BOX"];
                            if (dtScanPCBBoxes.Rows.Count > 0)
                            {
                                bool BOXValidate = false;
                                BOXValidate = dtScanPCBBoxes.Select().ToList().Exists(row => row["SECONDAR_BOX_ID"].ToString().ToUpper() == txtSecondaryBoxID.Text);
                                if (BOXValidate)
                                {
                                    CommonHelper.ShowMessage("Secondary barcode already scanned.", msgerror, CommonHelper.MessageType.Error.ToString());
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
                        return;
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
                CommonHelper.ShowMessage("Lot has been created, Now scan pcb or quality of scanned boxes", msgsuccess, CommonHelper.MessageType.Success.ToString());
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
        }
        //protected void txtPrimary_TextChanged(object sender, EventArgs e)
        //{
        //    checkprimarybarcode();
        //}
        protected void txtPcb_TextChanged(object sender, EventArgs e)
        {
            checkPCB();
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
                    if (drpFGItemCode.SelectedIndex < 0)
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
                            CommonHelper.ShowMessage("Please scan PCB barcode", msgerror, CommonHelper.MessageType.Error.ToString());
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

        protected void Group1_CheckedChanged(Object sender, EventArgs e)
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
        private void ChangeStatus(string sType)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            //if (rdPrimaryYes.Checked)
            //{
            //    if (txtPcb.Text == "")
            //    {
            //        CommonHelper.ShowMessage("Please scan PCB barcode.", msgerror, CommonHelper.MessageType.Error.ToString());
            //        return;
            //    }
            //}

            if (gvQualityData.Rows.Count == 0)
            {
                CommonHelper.ShowMessage("Please select status.", msgerror, CommonHelper.MessageType.Error.ToString());
                return;
            }           
            try
            {
                string SCANBY = Session["UserID"].ToString();
                string SITECODE = Session["SITECODE"].ToString();
                DataTable dtData = (DataTable)ViewState["Row"];
                if (dtData.Rows.Count > 0)
                {
                    DataRow[] foundAuthors = dtData.Select("STATUS = '0'");
                    if (foundAuthors.Length > 0 && sType == "OK")
                    {
                        CommonHelper.ShowMessage("Scanned barcode contains one rejected PCB. Please press Reject or Partial OK button", msginfo, CommonHelper.MessageType.Info.ToString());
                        return;
                    }
                    BL_FGQuality blobj = new BL_FGQuality();
                    string _OuptPut = string.Empty;
                    string sStatus = "1";
                    if (sType == "OK")
                    {
                        sStatus = "1";
                    }
                    else if (sType == "PARTIALOK")
                    {
                        sStatus = "3"; // Partial OK
                    }
                    else
                    {
                        sStatus = "2"; // rejected status
                    }
                    DataTable dtResult = blobj.SaveQualityData(dtData, sStatus, "FG", SCANBY, drpFGItemCode.Text, SITECODE, "", 
                        hidCode.Value, "", Session["LINECODE"].ToString());
                    if (dtResult.Rows.Count > 0)
                    {
                        _OuptPut = dtResult.Rows[0][0].ToString();
                        string[] Message = _OuptPut.Split('~');
                        if (_OuptPut.StartsWith("N~"))
                        {
                            CommonHelper.ShowMessage(Message[1], msgerror, CommonHelper.MessageType.Error.ToString());
                            txtSecondaryBoxID.Text = string.Empty;
                            txtSecondaryBoxID.Focus();
                            btnOK.Visible = true;
                            btnReject.Visible = true;
                            btnParialOK.Visible = true;
                        }
                        if (_OuptPut.StartsWith("ERROR~"))
                        {
                            CommonHelper.ShowMessage(Message[1], msgerror, CommonHelper.MessageType.Error.ToString());
                            txtSecondaryBoxID.Text = string.Empty;
                            txtSecondaryBoxID.Focus();
                            btnOK.Visible = true;
                            btnReject.Visible = true;
                        }
                        else
                        {
                            CommonHelper.ShowMessage(Message[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                            txtSecondaryBoxID.Text = string.Empty;
                            txtSecondaryBoxID.Focus();
                            txtSecondaryBoxID.Enabled = true;
                            gvQualityData.DataSource = null;
                            gvQualityData.DataBind();
                            btnOK.Visible = true;
                            btnReject.Visible = true;
                            txtSecondaryBoxID.Enabled = true;
                            drpDefect.SelectedIndex = 0;
                            txtSecondaryBoxID.ReadOnly = false;
                            DataTable dt = new DataTable();
                            gvQualityData.DataSource = dt;
                            gvQualityData.DataBind();

                            ViewState["Row"] = false;
                            ViewState["Row"] = null;
                            ViewState["BOX"] = false;
                            ViewState["ROW"] = null;
                            gvBoxData.DataSource = null;
                            gvBoxData.DataBind();
                            dvPrimaryScan.Visible = false;
                        }
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

        protected void btnOk_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            ChangeStatus("OK");
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            ChangeStatus("REJECT");
        }

        protected void btnParialOK_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            ChangeStatus("PARTIALOK");
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
                ViewState["BOX"] = null;
                gvQualityData.DataSource = null;
                gvQualityData.DataBind();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void btnResetData_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/INE/WIP/WIPNewQuality.aspx");
        }              
    }
}