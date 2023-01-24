using BusinessLayer.WIP;
using Common;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace SGST.INE.WIP
{
    public partial class WIPFGRollback : System.Web.UI.Page
    {
        BL_WIPFGRollback blobj = new BL_WIPFGRollback();
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
                    string _strRights = string.Empty;
                    _strRights = CommonHelper.GetRights("FG BOX REPACKING", (DataTable)Session["USER_RIGHTS"]);
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

                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }

        }

        protected void txtBoxID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (string.IsNullOrEmpty(txtBoxID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan box ID.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBoxID.Focus();
                    txtBoxID.Text = string.Empty;
                    return;
                }
                string sUserID = Session["UserID"].ToString();
                blobj = new BL_WIPFGRollback();
                DataTable dt = blobj.ScannedBox(txtBoxID.Text.Trim());
                if (dt.Rows.Count > 0)
                {
                    string sResult = dt.Rows[0][0].ToString();
                    if (sResult.StartsWith("N~"))
                    {
                        CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        txtBoxID.Focus();
                        txtBoxID.Text = string.Empty;
                        gvPCBData.DataSource = null;
                        gvPCBData.DataBind();
                        return;
                    }
                    else
                    {
                        gvPCBData.DataSource = dt;
                        gvPCBData.DataBind();
                    }
                }
                else
                {
                    CommonHelper.ShowMessage("No result found, Please try again", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBoxID.Focus();
                    txtBoxID.Text = string.Empty;
                    gvPCBData.DataSource = null;
                    gvPCBData.DataBind();
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
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                txtBoxID.Text = string.Empty;
                gvPCBData.DataSource = null;
                gvPCBData.DataBind();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void btnRollback_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (string.IsNullOrEmpty(txtBoxID.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan box ID.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBoxID.Focus();
                    txtBoxID.Text = string.Empty;
                    return;
                }
                if (gvPCBData.Rows.Count == 0)
                {
                    CommonHelper.ShowMessage("No data found for rollback.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBoxID.Focus();
                    txtBoxID.Text = string.Empty;
                    return;
                }
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "No")
                {
                    return;
                }
                string sFGItemCode = string.Empty;
                string sCustomerCode = string.Empty;
                DataTable dtRollbackData = new DataTable();
                dtRollbackData.Columns.Add("ROLLBACKPCB");
                foreach (GridViewRow item in gvPCBData.Rows)
                {
                    if (item.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chk = (item.FindControl("chkSelect") as CheckBox);
                        if (chk.Checked)
                        {
                            string sRollbackPCB = gvPCBData.Rows[item.RowIndex].Cells[2].Text;
                            sFGItemCode = gvPCBData.Rows[item.RowIndex].Cells[4].Text;
                            sCustomerCode = gvPCBData.Rows[item.RowIndex].Cells[5].Text;
                            dtRollbackData.Rows.Add(sRollbackPCB);
                        }
                    }
                }
                if (dtRollbackData.Rows.Count == 0)
                {
                    CommonHelper.ShowMessage("No data found for rollback. Please select at least one PCB", msgerror, CommonHelper.MessageType.Error.ToString());
                    return;
                }
                PL.PL_Printing plobj = new PL.PL_Printing();
                plobj.sSiteCode = Session["SiteCode"].ToString();
                plobj.sLineCode = Session["LINECODE"].ToString();
                plobj.sUserID = Session["UserID"].ToString();
                plobj.sBOMCode = sFGItemCode;
                plobj.sCustomerCode = sCustomerCode;
                plobj.dPackingDetail = dtRollbackData;
                plobj.sBoxId = txtBoxID.Text.Trim();
                plobj.sScanType = "PCB";
                blobj = new BL_WIPFGRollback();
                string sResult = blobj.RollbackBox(plobj);
                if (sResult.Length > 0)
                {
                    if (sResult.StartsWith("SUCCESS~"))
                    {
                        CommonHelper.ShowMessage(sResult.Split('~')[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                        txtBoxID.Focus();
                        txtBoxID.Text = string.Empty;
                        gvPCBData.DataSource = null;
                        gvPCBData.DataBind();
                        return;
                    }
                    else
                    {
                        CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
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