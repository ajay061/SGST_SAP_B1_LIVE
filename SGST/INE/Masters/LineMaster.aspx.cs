using BusinessLayer;
using Common;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace SGST.INE.Masters
{
    public partial class LineMaster : System.Web.UI.Page
    {
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
                    string _strRights = CommonHelper.GetRights("LINE MASTER", (DataTable)Session["USER_RIGHTS"]);
                    CommonHelper._strRights = _strRights.Split('^');
                    if (CommonHelper._strRights[0] == "0")  //Check view rights
                    {
                        Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                    }
                }
                if (!IsPostBack)
                {
                    ShowGridData();
                    txtLineID.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                _ResetField();
                ShowGridData();
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
                DataTable dt = new DataTable();
                BL_LineMaster dlobj = new BL_LineMaster();
                dt = dlobj.GetLines();
                if (dt.Rows.Count > 0)
                {
                    gvLineMaster.DataSource = dt;
                    gvLineMaster.DataBind();
                    lblNumberofRecords.Text = dt.Rows.Count.ToString();
                }
                else
                {
                    gvLineMaster.DataSource = null; ;
                    gvLineMaster.DataBind();
                    lblNumberofRecords.Text = "0";
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (btnSave.Text == "Save")
                {
                    if (txtLineID.Text.Trim() == "")
                    {
                        CommonHelper.ShowMessage("Please enter line code", msgerror, CommonHelper.MessageType.Error.ToString());
                        txtLineID.Focus();
                        return;
                    }
                    if (txtLineName.Text.Trim() == "")
                    {
                        CommonHelper.ShowMessage("Please enter line name", msgerror, CommonHelper.MessageType.Error.ToString());
                        txtLineName.Focus();
                        return;
                    }
                    BL_LineMaster dlobj = new BL_LineMaster();
                    string sResult = dlobj.SaveLine(txtLineID.Text.Trim(), txtLineName.Text.Trim(), txtLineDesc.Text.Trim(), Session["UserID"].ToString());
                    if (sResult.Length > 0)
                    {
                        if (sResult.StartsWith("ERROR~"))
                        {
                            txtLineID.Text = "";
                            txtLineDesc.Text = "";
                            txtLineDesc.Text = "";
                            CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        }
                        if (sResult.StartsWith("N~"))
                        {
                            txtLineID.Text = "";
                            txtLineDesc.Text = "";
                            txtLineDesc.Text = "";
                            CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        }
                        else
                        {
                            ShowGridData();
                            _ResetField();
                            CommonHelper.ShowMessage(sResult.Split('~')[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                        }
                    }
                    else
                    {
                        txtLineID.Text = "";
                        txtLineDesc.Text = "";
                        txtLineDesc.Text = "";
                        CommonHelper.ShowMessage("No result found.", msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                }
                else
                {
                    if (txtLineName.Text == "")
                    {
                        CommonHelper.ShowMessage("Please enter line name.", msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                    BL_LineMaster blobj = new BL_LineMaster();
                    string sResult = blobj.UpdateLine(txtLineName.Text.Trim(), txtLineDesc.Text.Trim(), hidUID.Value.Trim());
                    if (sResult.Length > 0)
                    {
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
                            ShowGridData();
                            _ResetField();
                            CommonHelper.ShowMessage(sResult.Split('~')[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                        }
                    }
                    else
                    {
                        CommonHelper.ShowMessage("No result found.", msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                if (ex.Message.Contains("Violation of PRIMARY KEY constraint"))
                {
                    CommonHelper.ShowMessage("Line id already exists.", msgerror, CommonHelper.MessageType.Error.ToString());
                    return;
                }
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }

        }

        private void _ResetField()
        {
            txtLineID.Focus();
            txtLineID.Text = string.Empty;
            txtLineName.Text = string.Empty;
            txtLineDesc.Text = string.Empty;
            btnSave.Text = "Save";
            txtLineID.ReadOnly = false;
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvLineMaster.PageIndex = e.NewPageIndex;
                ShowGridData();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
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
                BL_LineMaster dlobj = new BL_LineMaster();
                DataTable dtUserDetails = dlobj.GetSeletedData(_SN);
                if (dtUserDetails.Rows.Count > 0)
                {
                    txtLineID.Text = dtUserDetails.Rows[0]["LINEID"].ToString();
                    txtLineDesc.Text = dtUserDetails.Rows[0]["LINEDESC"].ToString();
                    txtLineName.Text = dtUserDetails.Rows[0]["LINENAME"].ToString();
                    hidUpdate.Value = "Update";
                    hidUID.Value = _SN;
                    txtLineID.ReadOnly = true;
                }
                else
                {
                    CommonHelper.ShowMessage("No Line details found.", msgerror, CommonHelper.MessageType.Error.ToString());
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
                BL_LineMaster blobj = new BL_LineMaster();
                string sResult = blobj.DeleteLine(_SN);
                if (sResult.Length > 0)
                {
                    if (sResult.StartsWith("ERROR~"))
                    {
                        if (sResult.Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                        {
                            CommonHelper.ShowMessage("Line already in transaction, you can not delete it", msgerror, CommonHelper.MessageType.Error.ToString());
                        }
                        else
                        {
                            CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        }
                    }
                    else if (sResult.StartsWith("N~"))
                    {
                        CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    else
                    {
                        _ResetField();
                        ShowGridData();
                        CommonHelper.ShowMessage(sResult.Split('~')[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                    }
                }
                else
                {
                    CommonHelper.ShowMessage("No result found.", msgerror, CommonHelper.MessageType.Error.ToString());
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("voilation"))
                {
                    CommonHelper.ShowMessage("Line already in transaction, you can not delete it", msgerror, CommonHelper.MessageType.Error.ToString());
                }
                else
                {
                    CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                }
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);

            }
            finally
            {
                ShowGridData();
            }
        }
    }
}