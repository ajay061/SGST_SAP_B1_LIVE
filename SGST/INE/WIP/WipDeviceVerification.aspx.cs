using BusinessLayer.WIP;
using Common;
using System;
using System.Data;
using PL;
using BusinessLayer.MES.PRINTING;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Xml;
using System.Text.RegularExpressions;
using System.Linq;
using BusinessLayer;
using System.IO;

namespace SGST.INE.WIP
{
    public partial class WipDeviceVerification : System.Web.UI.Page
    {
        BL_WIPAccessoriesVerification blobj;
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
                    _strRights = CommonHelper.GetRights("Device vs GB Comparison", (DataTable)Session["USER_RIGHTS"]);
                    CommonHelper._strRights = _strRights.Split('^');
                    if (CommonHelper._strRights[0] == "0")  //Check view rights
                    {
                        Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                    }
                }
                if (!IsPostBack)
                {
                    BindFGItemCode();
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
        protected void txtScanDeviceQR_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                string text = txtDeviceQR.Text.Replace("\n", "").Replace("\r","");
                text = text + "\n";
                if (ddlModel_Name.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select fg item code", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlModel_Name.Focus();
                    return;
                }
                if (txtDeviceQR.Text.Trim().ToUpper().Contains("<MRP>"))
                {
                    CommonHelper.ShowMessage("Please scan correct barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtDeviceQR.Text = "";
                    txtDeviceQR.Focus();
                    return;
                }
                string sSrNo = string.Empty;
                string sEanNo = string.Empty;
                string sMacID = string.Empty;
                string sAccBarcode = txtDeviceQR.Text;
                if (sAccBarcode.ToUpper().Contains("<SRNO>"))
                {
                    sSrNo = Regex.Split(sAccBarcode.ToUpper(), "<SRNO>").Last().Split('<')[0];
                    sMacID = Regex.Split(sAccBarcode.ToUpper(), "<MACID>").Last().Split('<')[0];
                }
                else
                {
                    CommonHelper.ShowMessage("There is no root element for keyword <SRNO> in the scanned barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtDeviceQR.Text = string.Empty;
                    txtDeviceQR.Focus();
                    return;
                }

                //string[] arr = txtDeviceQR.Text.Split('>');
                //string sSrNo = arr[7].ToString().Split('<')[0];
                //string sEanNo = arr[9].ToString().Split('<')[0];
                //string sMacID = arr[11].ToString().Split('<')[0];
                blobj = new BL_WIPAccessoriesVerification();
                DataTable dt = new DataTable();
                dt = blobj.blPcbScanDeviceVerify(ddlModel_Name.SelectedItem.Text.Trim(), Session["SiteCode"].ToString(), sSrNo
                    , Session["LineCode"].ToString(), ddlModel_Name.SelectedValue.ToString(), Session["UserID"].ToString(), sMacID, "VALIDATEDEVICE",
                    txtScanHere.Text.Trim()
                    );
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString().StartsWith("0~"))
                    {
                        CommonHelper.ShowMessage("PCB scanned successfully", msgsuccess, CommonHelper.MessageType.Success.ToString());
                        txtScanHere.Enabled = false;
                        txtDeviceQR.Enabled = false;
                        txtGBQRCode.Focus();
                    }
                    else
                    {
                        CommonHelper.ShowMessage(dt.Rows[0][0].ToString().Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        //txtDeviceQR.Enabled = false;
                        txtDeviceQR.Text = "";
                        txtDeviceQR.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError,
               System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                txtDeviceQR.Text = "";
                txtDeviceQR.Focus();
            }

        }
        protected void txtScanGBQR_TextChanged(object sender, EventArgs e)
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
                
                if (string.IsNullOrEmpty(txtScanHere.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan PCB barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtScanHere.Text = "";
                    txtGBQRCode.Text = "";
                    txtScanHere.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtDeviceQR.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan device barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtDeviceQR.Text = "";
                    txtGBQRCode.Text = "";
                    txtDeviceQR.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtGBQRCode.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan GB barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtGBQRCode.Text = "";
                    txtGBQRCode.Focus();
                    return;
                }
                if (txtGBQRCode.Text.Trim().Contains("<MRP>"))
                {
                    string sSrNo = string.Empty;
                    string sEanNo = string.Empty;
                    string sMacID = string.Empty;
                    string sAccBarcode = txtGBQRCode.Text;
                    if (sAccBarcode.ToUpper().Contains("<SRNO>"))
                    {
                        sSrNo = Regex.Split(sAccBarcode.ToUpper(), "<SRNO>").Last().Split('<')[0];
                        sMacID = Regex.Split(sAccBarcode.ToUpper(), "<MACID>").Last().Split('<')[0];
                    }
                    else
                    {
                        CommonHelper.ShowMessage("There is no root element for keyword <SRNO> in the scanned barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                        txtDeviceQR.Text = string.Empty;
                        txtDeviceQR.Focus();
                        return;
                    }
                    blobj = new BL_WIPAccessoriesVerification();
                    DataTable dt = new DataTable();
                    dt = blobj.blPcbScanGBVerify(ddlModel_Name.SelectedItem.Text.Trim(), Session["SiteCode"].ToString(), sSrNo
                        , Session["LineCode"].ToString(), ddlModel_Name.SelectedValue.ToString(), Session["UserID"].ToString(), sMacID, "VALIDATEGB",
                        txtDeviceQR.Text.Trim(),txtGBQRCode.Text.Trim(), txtScanHere.Text.Trim()
                        );
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString().StartsWith("0~"))
                        {
                            CommonHelper.ShowMessage("GB verified successfully", msgsuccess, CommonHelper.MessageType.Success.ToString());
                            Reset();
                            txtScanHere.Focus();
                        }
                        else
                        {
                            CommonHelper.ShowMessage(dt.Rows[0][0].ToString().Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                            //txtDeviceQR.Enabled = false;
                           
                            txtGBQRCode.Text = string.Empty;
                            txtGBQRCode.Focus();
                            return;
                        }
                    }
                }
                else
                {
                    CommonHelper.ShowMessage("Please scan correct barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtGBQRCode.Text = string.Empty;
                    txtGBQRCode.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError,
               System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                txtGBQRCode.Text = string.Empty;
                txtGBQRCode.Focus();
            }
        }
        protected void txtScanHere_TextChanged(object sender, EventArgs e)
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
                blobj = new BL_WIPAccessoriesVerification();
                DataTable dt = new DataTable();
                dt = blobj.blPcbScanDeviceVerify(ddlModel_Name.SelectedItem.Text.Trim(), Session["SiteCode"].ToString(), txtScanHere.Text.Trim()
                    , Session["LineCode"].ToString(), ddlModel_Name.SelectedValue.ToString(), Session["UserID"].ToString(), "", "PCBSCANDEVICEVERIFY",""
                    );
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString().StartsWith("0~"))
                    {
                        CommonHelper.ShowMessage("PCB scanned successfully", msgsuccess, CommonHelper.MessageType.Success.ToString());
                        txtScanHere.Enabled = false;
                        txtDeviceQR.Focus();
                    }
                    else
                    {
                        CommonHelper.ShowMessage(dt.Rows[0][0].ToString().Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        //txtDeviceQR.Enabled = false;
                        txtScanHere.Enabled = true;
                        txtScanHere.Text = "";
                        txtScanHere.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError,
               System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }
        private void Reset()
        {
            txtScanHere.Text = "";
            txtDeviceQR.Text = "";
            txtGBQRCode.Text = "";
            txtScanHere.Enabled = true;
            txtDeviceQR.Enabled = true;
            txtGBQRCode.Enabled = true;
           

        }
    }


}