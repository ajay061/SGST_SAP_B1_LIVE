using BusinessLayer;
using Common;
using System;
using System.Data;
using PL;
using BusinessLayer.MES.PRINTING;
using System.Reflection;

namespace SGST.INE.MOB
{
    public partial class GBWTPrinting : System.Web.UI.Page
    {
        BL_GB_WT_Printing blobj = new BL_GB_WT_Printing();
        string Message = string.Empty;
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
                    string _strRights = CommonHelper.GetRights("GB WEIGHT", (DataTable)Session["USER_RIGHTS"]);
                    CommonHelper._strRights = _strRights.Split('^');
                    if (CommonHelper._strRights[0] == "0")  //Check view rights
                    {
                        Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                    }
                }
                if (!IsPostBack)
                {
                    BindModelName();
                    lblWT.Text = "0";
                    lblTP.Text = "0";
                    lblTM.Text = "0";
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
        public void GetData()
        {
            try
            {
                if (ddlModel_Name.SelectedIndex > 0)
                {
                    lbllastscanned.Text = string.Empty;
                    BL_MobCommon obj = new BL_MobCommon();
                    PL_Printing plobj = new PL_Printing();
                    plobj.sModelName = ddlModel_Name.SelectedValue.ToString();
                    DataTable dt = new DataTable();
                    dt = obj.DisplayedData(plobj);
                    if (dt.Rows.Count > 0)
                    {
                        lblWT.Text = dt.Rows[0]["GrossWt"].ToString();
                        lblTP.Text = dt.Rows[0]["TolPlus"].ToString();
                        lblTM.Text = dt.Rows[0]["TolMinus"].ToString();
                        lblModelName.Text = ddlModel_Name.SelectedValue.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }

        private void Scan_Barcode(string sBarcode)
        {
            try
            {
                BL_GB_WT_Printing blobj = new BL_GB_WT_Printing();
                DataTable dt = new DataTable();
                PL.PL_Printing plob = new PL.PL_Printing();
                plob.sBarcodestring = txtScanHere.Text.Trim();
                plob.sSiteCode = Session["SiteCode"].ToString();
                plob.sUserID = Session["UserID"].ToString();
                plob.sLineCode = Session["LineCode"].ToString();
                plob.sBOMCode = ddlModel_Name.SelectedItem.Text.Trim();
                plob.sStageCode = "WTMACHINE";
                DataTable dtResult = blobj.blValidateWeight(plob);
                Message = dtResult.Rows[0][0].ToString();
                if (Message.StartsWith("SUCCESS~"))
                {
                    CommonHelper.ShowMessage(Message.Split('~')[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                    dvMessage.Visible = true;
                    GetData();
                }
                else
                {
                    CommonHelper.ShowMessage(Message.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    lbllastscanned.Text = txtScanHere.Text.Trim();
                    txtScanHere.Text = ""; txtScanHere.Focus();
                    dvMessage.Visible = false;
                    GetData();
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        protected void txtScanHere_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (ddlModel_Name.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select model", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlModel_Name.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtScanHere.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please Scan Here", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtScanHere.Focus();
                    return;
                }
                Scan_Barcode(txtScanHere.Text.Trim());
            }
            catch (Exception ex)
            {
                txtScanHere.Text = ""; txtScanHere.Focus();
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
                lbllastscanned.Text = string.Empty;
                txtScanHere.Text = string.Empty;
                lbllastscanned.Text = string.Empty;
                lblWT.Text = "0";
                lblTP.Text = "0";
                lblTM.Text = "0";
                lblModelName.Text = "";
                txtCapWT.Text = string.Empty;
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void btnValidate_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (ddlModel_Name.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select model", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlModel_Name.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtScanHere.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please Scan Here", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtScanHere.Focus();
                    return;
                }
                BL_GB_WT_Printing blobj = new BL_GB_WT_Printing();
                DataTable dt = new DataTable();
                PL.PL_Printing plob = new PL.PL_Printing();
                plob.sBarcodestring = txtScanHere.Text.Trim();
                plob.sSiteCode = Session["SiteCode"].ToString();
                plob.sUserID = Session["UserID"].ToString();
                plob.sLineCode = Session["LineCode"].ToString();
                plob.sBOMCode = ddlModel_Name.Text.Trim();
                DataTable dtResult = blobj.blGetCaptureWeight(plob);
                if (dtResult.Rows.Count == 0)
                {
                    CommonHelper.ShowMessage("Weight not found, Please try again", msgerror, CommonHelper.MessageType.Error.ToString());
                    btnValidate.Focus();
                    return;
                }
                else
                {
                    Message = dtResult.Rows[0][0].ToString();
                    CommonHelper.ShowMessage(Message.Split('~')[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                    txtCapWT.Text = Message.Split('~')[1].ToString();                  
                }
                if (string.IsNullOrEmpty(txtCapWT.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Wt not found, Please try again", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtScanHere.Focus();
                    return;
                }
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, "Weight result found, Capture weight :" + txtCapWT.Text.Trim()
                    + ", Tol + " + lblTM.Text + ", Tol -" + lblTP.Text + ", GB Wt : " + lblWT.Text
                    + ", Barcode : " + txtScanHere.Text.Trim()
                    );
                double capWt = Convert.ToDouble(txtCapWT.Text.Trim());
                double tMinus = Convert.ToDouble(lblTM.Text);
                double tPlus = Convert.ToDouble(lblTP.Text);
                double GBwt = Convert.ToDouble(lblWT.Text);
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (capWt >= GBwt - tMinus && capWt <= GBwt + tPlus)
                {
                    CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                    dt = new DataTable();
                    plob = new PL.PL_Printing();
                    plob.sBarcodestring = txtScanHere.Text.Trim();
                    plob.sSiteCode = Session["SiteCode"].ToString();
                    plob.sUserID = Session["UserID"].ToString();
                    plob.sLineCode = Session["LineCode"].ToString();
                    plob.dBoxWT = Convert.ToDecimal(txtCapWT.Text.Trim());
                    plob.sStageCode = "WTMACHINE";
                    plob.sBOMCode = ddlModel_Name.SelectedItem.Text.Trim();
                    dtResult = blobj.blSaveWeight(plob);
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,
                  System.Reflection.MethodBase.GetCurrentMethod().Name, "Weight result found, Save data in DB, Capture weight :" + txtCapWT.Text.Trim()
                  + ", Tol + " + lblTM.Text + ", Tol -" + lblTP.Text + ", GB Wt : " + lblWT.Text
                  + ", Barcode : " + txtScanHere.Text.Trim()
                  );
                    if (dtResult.Rows.Count > 0)
                    {
                        Message = dtResult.Rows[0][0].ToString();
                        if (Message.StartsWith("SUCCESS~"))
                        {
                            CommonHelper.ShowMessage(Message.Split('~')[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                            dvMessage.Visible = false;
                            lbllastscanned.Text = txtScanHere.Text.Trim();
                            txtScanHere.Text = ""; txtScanHere.Focus();
                        }
                        else
                        {
                            CommonHelper.ShowMessage(Message.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                            lbllastscanned.Text = txtScanHere.Text.Trim();
                            txtScanHere.Text = ""; txtScanHere.Focus();
                        }
                    }
                    else
                    {
                        CommonHelper.ShowMessage("No result found, Please put weight again", msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                }
                else
                {
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,
                     System.Reflection.MethodBase.GetCurrentMethod().Name, "WT is not acureate, Capture weight :" + txtCapWT.Text.Trim()
                     + ", Tol + " + lblTM.Text + ", Tol -" + lblTP.Text + ", GB Wt : " + lblWT.Text
                     + ", Barcode : " + txtScanHere.Text.Trim()
                     );
                    CommonHelper.ShowMessage("Wt is not accureate, Please put weight again", msgerror, CommonHelper.MessageType.Error.ToString());
                    return;
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