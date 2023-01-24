using BusinessLayer;
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

namespace SGST.INE.Masters
{
    public partial class AccessoresScanning : System.Web.UI.Page
    {
        BL_Acc_Scanning blobj = new BL_Acc_Scanning();
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
                //// string s = "<SRNO>RWSPAJHJ255526H</SRNO><EAN>8906110060052</EAN>".Substring(0, 14);

                Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
                Response.AddHeader("Pragma", "no-cache");
                Response.AddHeader("Expires", "0");
                string sHeaderName = string.Empty;
                if (Session["usertype"].ToString() != "ADMIN")
                {
                    string _strRights = string.Empty;
                    _strRights = CommonHelper.GetRights("Accessories Scanning", (DataTable)Session["USER_RIGHTS"]);
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
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        private void BindFGItemCode()
        {
            try
            {
                if (gvModel.Rows.Count > 0)
                {
                    foreach (GridViewRow row in gvModel.Rows)
                    {
                        row.ControlStyle.BackColor = System.Drawing.Color.White;
                    }
                }
                gvModel.DataSource = null;
                gvModel.DataBind();
                ddlModel_Name.Items.Clear();
                txtScanHere.Text = string.Empty;
                txtAccessoriesBarcode.Text = string.Empty;
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
        private void bindModelKeys()
        {
            try
            {
                if (ddlModel_Name.SelectedIndex > 0)
                {
                    blobj = new BL_Acc_Scanning();
                    DataTable dt = blobj.Bind_Model_Mapping_Accessories(
                        Convert.ToString(ddlModel_Name.SelectedItem.Text), Session["SiteCode"].ToString());
                    if (dt.Rows.Count > 0)
                    {
                        gvModel.DataSource = dt;
                        gvModel.DataBind();
                        ViewState["DATA"] = dt;
                    }
                    else
                    {
                        CommonHelper.ShowMessage("No key parts found for mapped against model or color, Please mapped in model key mapping module."
                            , msgerror, CommonHelper.MessageType.Error.ToString());
                        ddlModel_Name.SelectedIndex = 0;
                        ddlModel_Name.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void ddlModel_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlModel_Name.SelectedIndex > 0)
                {
                    lblModelName.Text = ddlModel_Name.SelectedValue.ToString();
                    txtScanHere.Focus();
                    bindModelKeys();
                }
                else
                {
                    txtScanHere.Text = string.Empty;
                    txtAccessoriesBarcode.Text = string.Empty;
                    lblModelName.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void txtScanHere_TextChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in gvModel.Rows)
                {
                    row.ControlStyle.BackColor = System.Drawing.Color.White;
                }
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (ddlModel_Name.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select fg item code", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlModel_Name.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtScanHere.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please Scan Here", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtScanHere.Focus();
                    return;
                }
                blobj = new BL_Acc_Scanning();
                DataTable dt = new DataTable();
                dt = blobj.blScanIMEI(ddlModel_Name.SelectedItem.Text.ToString().Trim(), Session["SiteCode"].ToString(), txtScanHere.Text.Trim());
                if (dt.Rows.Count > 0)
                {
                    Message = dt.Rows[0][0].ToString();
                    if (Message.StartsWith("SUCCESS~"))
                    {
                        CommonHelper.ShowMessage("Valid barcode", msgsuccess, CommonHelper.MessageType.Success.ToString());
                        txtAccessoriesBarcode.Focus();
                    }
                    else
                    {
                        if (Message.StartsWith("2~"))
                        {
                            gvModel.DataSource = (DataTable)ViewState["DATA"];
                            DataTable dtLoadData = (DataTable)ViewState["DATA"];
                            string[] sArr = dt.Rows[0][0].ToString().Split('~')[1].Split(',');
                            foreach (GridViewRow row in gvModel.Rows)
                            {
                                Label lblAccessoriesName = row.FindControl("lblAccessName") as Label;
                                for (int i = 0; i < sArr.Length; i++)
                                {
                                    if (lblAccessoriesName.Text == sArr[i].ToString().Split('^')[0].Trim())
                                    {
                                        row.ControlStyle.BackColor = System.Drawing.Color.Green;
                                        row.Cells[5].Text = sArr[i].ToString().Split('^')[1];
                                    }
                                }
                            }
                        }
                        else
                        {
                            CommonHelper.ShowMessage(Message.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                            txtScanHere.Text = ""; txtScanHere.Focus();
                        }
                    }
                }
                else
                {
                    CommonHelper.ShowMessage("No result found,Please try again", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtScanHere.Text = ""; txtScanHere.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void txtAccessoriesBarcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int iCountTotalScan = 0;
                int iScannedCount = 0;
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (ddlModel_Name.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select fg item code", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlModel_Name.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtScanHere.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please Scan Here", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtScanHere.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtAccessoriesBarcode.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please Scan accessories barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtAccessoriesBarcode.Focus();
                    return;
                }
                string sAccessoriesName = string.Empty;
                string sAccBarcode = txtAccessoriesBarcode.Text;
                string ADAPTOR_SN = string.Empty;
                string sMacID = string.Empty;
                string GponMacID = string.Empty;
                foreach (GridViewRow row in gvModel.Rows)
                {
                    if (row.ControlStyle.BackColor == System.Drawing.Color.Green)
                    {
                        continue;
                    }
                    Label lblAccessoriesName = row.FindControl("lblAccessName") as Label;
                    if (lblAccessoriesName.Text.ToUpper() != "STAND LABEL" && chkStandLabel.Checked == true)
                    {
                        continue;
                    }
                    Label lblStartDigit = row.FindControl("StartDigit") as Label;
                    Label lblEndDigit = row.FindControl("EndDigit") as Label;
                    Label lblKeyValue = row.FindControl("Value") as Label;
                    if (!txtAccessoriesBarcode.Text.Contains(lblKeyValue.Text))
                    {
                        continue;
                    }
                    if (lblAccessoriesName.Text.ToUpper() != "STAND LABEL")
                    {
                        if (lblStartDigit.Text == "0" && lblEndDigit.Text == "0")
                        {
                            CommonHelper.ShowMessage("Validation not found", msgerror, CommonHelper.MessageType.Error.ToString());
                            txtAccessoriesBarcode.Text = string.Empty;
                            txtAccessoriesBarcode.Focus();
                            return;
                        }
                        if (Convert.ToInt32(lblEndDigit.Text) > txtAccessoriesBarcode.Text.Length)
                        {
                            CommonHelper.ShowMessage("Scan key barcode length not matched with maximum length of key item,\r\n Please change max length of key value", msgerror, CommonHelper.MessageType.Error.ToString());
                            return;
                        }
                        if (lblKeyValue.Text.Length != 0)
                        {
                            string sEanNo = string.Empty;
                            if (lblAccessoriesName.Text.ToUpper() == "ADAPTOR")
                            {
                                //sAccBarcode1 = txtAccessoriesBarcode.Text;

                                if (sAccBarcode.ToUpper().Contains("<SRNO>"))
                                {
                                    ADAPTOR_SN = Regex.Split(sAccBarcode.ToUpper(), "<SRNO>").Last().Split('<')[0];
                                    sEanNo = Regex.Split(txtAccessoriesBarcode.Text.ToUpper(), "<EAN>").Last().Split('<')[0];
                                }
                                else
                                {
                                    CommonHelper.ShowMessage("There is no root element for keyword <SRNO> in the scanned barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                                    txtAccessoriesBarcode.Text = string.Empty;
                                    txtAccessoriesBarcode.Focus();
                                    return;
                                }
                            }
                            //Added by Amit joshi 28-11-2022
                            //if (lblAccessoriesName.Text.ToUpper() == "GBLABEL")
                            //{

                            //    if (sAccBarcode.ToUpper().Contains("<SRNO>"))
                            //    {

                            //        sMacID = Regex.Split(sAccBarcode.ToUpper(), "<MACID>").Last().Split('<')[0];
                            //    }
                            //    else
                            //    {
                            //        CommonHelper.ShowMessage("There is no root element for keyword <SRNO> in the scanned barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                            //        txtAccessoriesBarcode.Text = string.Empty;
                            //        txtAccessoriesBarcode.Focus();
                            //        return;
                            //    }
                            //}
                            //if (lblAccessoriesName.Text.ToUpper() == "GPON")
                            //{

                            //    if (sAccBarcode.ToUpper().Contains("<SRNO>"))
                            //    {

                            //        sMacID = Regex.Split(sAccBarcode.ToUpper(), "<MACID>").Last().Split('<')[0];
                            //    }
                            //    else
                            //    {
                            //        CommonHelper.ShowMessage("There is no root element for keyword <SRNO> in the scanned barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                            //        txtAccessoriesBarcode.Text = string.Empty;
                            //        txtAccessoriesBarcode.Focus();
                            //        return;
                            //    }
                            //}

                            if (lblAccessoriesName.Text.ToUpper() == "ADAPTOR")
                            {
                                if (sEanNo.ToUpper().Substring(Convert.ToInt32(lblStartDigit.Text),
                              Convert.ToInt32(Convert.ToInt32(lblEndDigit.Text)))
                              != lblKeyValue.Text.ToUpper())
                                {
                                    CommonHelper.ShowMessage("Scanned barcode not matched with the defined value", msgerror, CommonHelper.MessageType.Error.ToString());
                                    txtAccessoriesBarcode.Text = string.Empty;
                                    txtAccessoriesBarcode.Focus();
                                    return;
                                }
                            }
                            //if (txtAccessoriesBarcode.Text.Trim().ToUpper().Substring(Convert.ToInt32(lblStartDigit.Text),
                            //    Convert.ToInt32(Convert.ToInt32(lblEndDigit.Text)))
                            //    != lblKeyValue.Text.ToUpper())
                            else if (sAccBarcode.ToUpper().Substring(Convert.ToInt32(lblStartDigit.Text),
                              Convert.ToInt32(Convert.ToInt32(lblEndDigit.Text)))
                              != lblKeyValue.Text.ToUpper())
                            {
                                CommonHelper.ShowMessage("Scanned barcode not matched with the defined value", msgerror, CommonHelper.MessageType.Error.ToString());
                                txtAccessoriesBarcode.Text = string.Empty;
                                txtAccessoriesBarcode.Focus();
                                return;
                            }
                        }
                        iCountTotalScan = 1;
                        if (iCountTotalScan == 0)
                        {
                            CommonHelper.ShowMessage("Scanned barcode not matched with the key value", msgerror, CommonHelper.MessageType.Error.ToString());
                            txtAccessoriesBarcode.Text = string.Empty;
                            txtAccessoriesBarcode.Focus();
                            return;
                        }
                    }
                    string sIsStandLabel = "0";
                    if (chkStandLabel.Checked == true)
                    {
                        try
                        {
                            sIsStandLabel = "1";
                            if (sAccBarcode.Contains("<SRNO>"))
                            {
                                sAccBarcode = Regex.Split(sAccBarcode, "<SRNO>").Last().Split('<')[0];
                            }
                            else
                            {
                                CommonHelper.ShowMessage("There is no root element for keyword <SRNO> in the scanned barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                                txtAccessoriesBarcode.Text = string.Empty;
                                txtAccessoriesBarcode.Focus();
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            CommonHelper.ShowMessage("XML design is not accurate, Please check the data", msgerror, CommonHelper.MessageType.Error.ToString());
                            return;
                        }
                    }
                    blobj = new BL_Acc_Scanning();
                    DataTable dt = new DataTable();
                    dt = blobj.blScanAccessories(ddlModel_Name.SelectedItem.Text.Trim(), Session["SiteCode"].ToString(), txtScanHere.Text.Trim()
                        , Session["LineCode"].ToString(), ddlModel_Name.SelectedValue.ToString(),
                        sAccBarcode
                        , lblAccessoriesName.Text, Session["UserID"].ToString(), sIsStandLabel, txtAccessoriesBarcode.Text, ADAPTOR_SN, sMacID
                        );
                    if (dt.Rows.Count > 0)
                    {
                        Message = dt.Rows[0][0].ToString();
                        if (Message.StartsWith("SUCCESS~"))
                        {
                            CommonHelper.ShowMessage("Data saved successfully", msgsuccess, CommonHelper.MessageType.Success.ToString());
                            row.ControlStyle.BackColor = System.Drawing.Color.Green;
                            row.Cells[5].Text = txtAccessoriesBarcode.Text.Trim();
                            txtAccessoriesBarcode.Text = string.Empty;
                            txtAccessoriesBarcode.Focus();
                            foreach (GridViewRow row1 in gvModel.Rows)
                            {
                                if (row1.ControlStyle.BackColor == System.Drawing.Color.Green)
                                {
                                    iScannedCount++;
                                }
                            }
                            if (iScannedCount == gvModel.Rows.Count)
                            {
                                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                                CommonHelper.ShowMessage("All the accessories barcode are scanned", msgsuccess, CommonHelper.MessageType.Success.ToString());
                                bindModelKeys();
                                txtAccessoriesBarcode.Text = string.Empty;
                                txtScanHere.Text = string.Empty;
                                txtScanHere.Focus();
                            }
                            else
                                if (chkStandLabel.Checked == true)
                            {
                                txtAccessoriesBarcode.Text = string.Empty;
                                txtScanHere.Text = string.Empty;
                                txtScanHere.Focus();
                            }
                        }
                        else
                        {
                            CommonHelper.ShowMessage(Message.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                            txtAccessoriesBarcode.Text = ""; txtAccessoriesBarcode.Focus();
                            if (Message.StartsWith("0~"))
                            {
                                row.ControlStyle.BackColor = System.Drawing.Color.Green;
                                row.Cells[5].Text = Message.Split('~')[2];
                                txtAccessoriesBarcode.Text = string.Empty;
                                txtAccessoriesBarcode.Focus();
                            }
                        }
                    }
                    else
                    {
                        CommonHelper.ShowMessage("No result found,Please try again", msgerror, CommonHelper.MessageType.Error.ToString());
                        txtAccessoriesBarcode.Text = ""; txtAccessoriesBarcode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                BindFGItemCode();
                txtScanHere.Text = string.Empty;
                txtAccessoriesBarcode.Text = string.Empty;
                lblModelName.Text = string.Empty;
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }

        }

        protected void chkStandLabel_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // txtAccessoriesBarcode.TextMode = TextBoxMode.SingleLine;
                if (chkStandLabel.Checked == true)
                {
                    bool bStandLabelAvailable = false;
                    foreach (GridViewRow row in gvModel.Rows)
                    {
                        Label lblAccessoriesName = row.FindControl("lblAccessName") as Label;
                        if (lblAccessoriesName.Text.ToUpper() == "STAND LABEL")
                        {
                            // txtAccessoriesBarcode.TextMode = TextBoxMode.MultiLine;
                            bStandLabelAvailable = true;
                            break;
                        }
                    }
                    if (bStandLabelAvailable == false)
                    {
                        CommonHelper.ShowMessage("There is no stand label mapped against selected fg item code", msgerror, CommonHelper.MessageType.Error.ToString());
                        chkStandLabel.Checked = false;
                    }
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