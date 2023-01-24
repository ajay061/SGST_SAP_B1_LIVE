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

namespace SGST.INE.WIP
{
    public partial class WIPAccessoriesVerification : System.Web.UI.Page
    {
        BL_WIPAccessoriesVerification blobj = new BL_WIPAccessoriesVerification();
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
                string sHeaderName = string.Empty;
                if (Session["usertype"].ToString() != "ADMIN")
                {
                    string _strRights = string.Empty;
                    _strRights = CommonHelper.GetRights("Accessories Verification", (DataTable)Session["USER_RIGHTS"]);
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
                    blobj = new BL_WIPAccessoriesVerification();
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
                //if (string.IsNullOrEmpty(txtScanHere.Text.Trim()))
                //{
                //    CommonHelper.ShowMessage("Please Scan Here", msgerror, CommonHelper.MessageType.Error.ToString());
                //    txtScanHere.Focus();
                //    return;
                //}
                if (string.IsNullOrEmpty(txtAccessoriesBarcode.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please Scan accessories barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtAccessoriesBarcode.Focus();
                    return;
                }
                string sAccBarcode = txtAccessoriesBarcode.Text;
                //if(sAccBarcode.ToUpper().Contains("<SRNO>"))
                //{
                //    sAccBarcode=  Regex.Split(sAccBarcode.ToUpper(), "<SRNO>").Last().Split('<')[0];
                //}
                blobj = new BL_WIPAccessoriesVerification();
                DataTable dt = new DataTable();
                dt = blobj.blScanAccessories(ddlModel_Name.SelectedItem.Text.Trim(), Session["SiteCode"].ToString(), txtScanHere.Text.Trim()
                    , Session["LineCode"].ToString(), ddlModel_Name.SelectedValue.ToString(),
                    sAccBarcode
                    , "", Session["UserID"].ToString()
                    );
                if (dt.Rows.Count > 0)
                {
                    Message = dt.Rows[0][0].ToString();
                    if (Message.StartsWith("SUCCESS~"))
                    {
                        CommonHelper.ShowMessage("Accessories Verified successfully", msgsuccess, CommonHelper.MessageType.Success.ToString());
                        string sAccName = Message.Split('~')[1];
                        foreach (GridViewRow row in gvModel.Rows)
                        {
                            Label lblAccessoriesName = row.FindControl("lblAccessName") as Label;
                            if (lblAccessoriesName.Text == sAccName)
                            {
                                row.ControlStyle.BackColor = System.Drawing.Color.Green;
                            }
                        }
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
                    }
                    else
                    {
                        CommonHelper.ShowMessage(Message.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        txtAccessoriesBarcode.Text = ""; txtAccessoriesBarcode.Focus();
                        if (Message.StartsWith("0~"))
                        {
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
                blobj = new BL_WIPAccessoriesVerification();
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
                                        row.Cells[5].Text = sArr[i].ToString().Split('^')[1];
                                    }
                                }
                            }
                            if (sArr.Length != gvModel.Rows.Count)
                            {
                                CommonHelper.ShowMessage("All the accessories are not mapped, please mapped complete accessories", msgerror, CommonHelper.MessageType.Error.ToString());
                                txtAccessoriesBarcode.Enabled = false;
                            }
                            else
                            {
                                txtAccessoriesBarcode.Enabled = true;
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
    }
}