using BusinessLayer;
using BusinessLayer.WIP;
using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGST.INE.WIP
{
    public partial class QCLevelVerification : System.Web.UI.Page
    {
        string Message = "";
        BL_WIP_PCBScanning blobj = new BL_WIP_PCBScanning();
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
                string sHeaderName = string.Empty;
                if (Session["usertype"].ToString() != "ADMIN")
                {
                    string _strRights = string.Empty;
                    _strRights = CommonHelper.GetRights("QC Level Verification", (DataTable)Session["USER_RIGHTS"]);
                    CommonHelper._strRights = _strRights.Split('^');
                    if (CommonHelper._strRights[0] == "0")  //Check view rights
                    {
                        Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                    }
                }
                if (!IsPostBack)
                {
                    BindFGItemCode();
                    BindDefect();
                    //BindStationID();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        private void BindFGItemCode()
        {
            try
            {

                //gvModel.DataSource = null;
                //gvModel.DataBind();
                ddlModel_Name.Items.Clear();
                //txtScanHere.Text = string.Empty;
                //txtAccessoriesBarcode.Text = string.Empty;
                BL_MobCommon blobj = new BL_MobCommon();
                string sResult = string.Empty;
                DataTable dtFGItemCode = blobj.BindModel();
                if (dtFGItemCode.Rows.Count > 0)
                {
                    CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                    clsCommon.FillMultiColumnsCombo(ddlModel_Name, dtFGItemCode, true);
                    ddlModel_Name.SelectedIndex = 0;
                    ddlModel_Name.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError,
               System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void BindDefect()
        {
            try
            {
                drpDefect.Items.Clear();
                string sResult = string.Empty;
                blobj = new BL_WIP_PCBScanning();
                DataTable dtStationID = blobj.BindDefectQClevelVerification( out sResult
                    , Session["SiteCode"].ToString());
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

       
        protected void btnQcVerification_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);



                if (ddlModel_Name.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select fg item code", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlModel_Name.Focus();
                    return;
                }
                else if (drpDefect.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select defect", msginfo, CommonHelper.MessageType.Info.ToString());
                    drpDefect.Focus();
                    txtPCBID.Text = string.Empty;
                    return;
                }
                else if (string.IsNullOrEmpty(drpstation.Text.Trim()))
                 {
                        CommonHelper.ShowMessage("Please select repair station", msginfo, CommonHelper.MessageType.Info.ToString());
                        drpstation.Focus();
                        txtPCBID.Text = string.Empty;
                        return;
                }
                
                else if (string.IsNullOrEmpty(txtPCBID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please Enter data   ", msginfo, CommonHelper.MessageType.Info.ToString());
                    txtPCBID.Focus();
                    txtPCBID.Text = string.Empty;
                    return;
                }

                blobj = new BL_WIP_PCBScanning();
                string sResult = blobj.QCLevelVerificationPCBbarcode(Session["LINECODE"].ToString(),
                   ddlModel_Name.SelectedItem.Text.Trim(),
                   drpDefect.Text,
                   drpstation.Text,
                    txtPCBID.Text.Trim().ToUpper()
                    , Session["UserID"].ToString(), 
                    Session["SiteCode"].ToString()
                   
                    );
                Message = sResult.Split('~')[1];
               if (sResult.StartsWith("SUCCESS"))
              {
                    CommonHelper.ShowMessage(Message, msgsuccess, CommonHelper.MessageType.Success.ToString());
                    txtPCBID.Text = string.Empty;
                    //drpDefect.SelectedIndex = '0';
                    return;
              }
              
               else if (sResult.StartsWith("N~"))
                {
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                        txtPCBID.Text = string.Empty;
                       txtPCBID.Focus();
                       //drpDefect.SelectedIndex = '0';
                    return;
                }
                else if (sResult.StartsWith("ERROR~"))
                {
                        txtPCBID.Text = string.Empty;
                        //drpDefect.SelectedIndex = '0';
                        return;

                }
                else if (sResult.StartsWith("NOTFOUND~"))
                {
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                        txtPCBID.Text = string.Empty;
                        txtPCBID.Focus();
                        return;

                }
                else if (sResult.StartsWith("MACHINEFAILED~"))
                {
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                        txtPCBID.Text = string.Empty;
                        txtPCBID.Focus();
                        return;

                }

            }
            catch (Exception ex)
            {

                txtPCBID.Text = string.Empty;
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void BindStationID()
        {
            try
            {
                drpstation.Items.Clear();
                string sResult = string.Empty;
                blobj = new BL_WIP_PCBScanning();
                DataTable dtStationID = blobj.BindReWorkStationID(out sResult, Session["SiteCode"].ToString());
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

    }
}