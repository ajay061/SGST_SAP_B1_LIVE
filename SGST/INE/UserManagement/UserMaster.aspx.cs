using BusinessLayer;
using Common;
using PL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI.WebControls;

namespace SGST.INE.UserManagement
{
    public partial class UserMaster : System.Web.UI.Page
    {
        string Message = string.Empty;
        string Department = "";
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Signin/v1/Login.aspx?Session=Null");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Department = Session["Department"].ToString();
            try
            {
                if (Session["usertype"].ToString().ToUpper() != "ADMIN")
                {
                    string sModuleName = "User Master";
                    string _strRights = CommonHelper.GetRights(sModuleName, (DataTable)Session["USER_RIGHTS"]);
                    CommonHelper._strRights = _strRights.Split('^');
                    if (CommonHelper._strRights[0] == "0")  //Check view rights
                    {
                        Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                    }
                }
                if (!IsPostBack)
                {
                    ShowGridDataForSuperAdmin(Department);
                }
                ChangeTypes();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        private void ChangeTypes()
        {
            if (Department == "RM")
            {
                drpDepartment.SelectedIndex = 1;
                drpDepartment.Enabled = false;
                drpDepartment.Height = 35;
            }
            if (Department == "WIP")
            {
                drpDepartment.SelectedIndex = 2;
                drpDepartment.Enabled = false;
                drpDepartment.Height = 35;
            }
            if (Department == "FG")
            {
                drpDepartment.SelectedIndex = 3;
                drpDepartment.Enabled = false;
                drpDepartment.Height = 35;
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (!IsPostBack)
                {
                    ShowGridDataForSuperAdmin(Department);
                }
                _ResetField();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        private void ShowGridDataForSuperAdmin(string Dept)
        {
            try
            {
                string sUsertID = Session["userid"].ToString();
                string sUserType = Session["UserType"].ToString();
                DataTable dt = new DataTable();
                BL_UserMaster dlobj = new BL_UserMaster();
                if (sUserType == "ADMIN")
                {
                    dt = dlobj.GetUsersForSuperAdmin(sUsertID, "");
                }
                else
                {
                    dt = dlobj.GetUsersForSuperAdmin(sUsertID, Dept);
                }                
                if (dt.Rows.Count > 0)
                {
                    gvUserMaster.DataSource = dt;
                    gvUserMaster.DataBind();
                    lblNumberofRecords.Text = dt.Rows.Count.ToString();
                    dt.TableName = "Table1";
                    dt.AcceptChanges();
                    System.Data.DataView view = new System.Data.DataView(dt);
                    drpUserFilter.Items.Clear();
                    System.Data.DataTable selected =
                            view.ToTable("Table1", false, "USERID");
                    if (selected.Rows.Count > 0)
                    {
                        clsCommon.FillComboBox(drpUserFilter, selected, true);
                    }                   
                    ViewState["Data"] = dt;
                }
                else
                {
                    gvUserMaster.DataSource = null;
                    gvUserMaster.DataBind();
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
                    if (txtUserID.Text.Trim() == string.Empty)
                    {
                        CommonHelper.ShowMessage("Please enter user id.", msgerror, CommonHelper.MessageType.Error.ToString());
                        txtUserID.Focus();
                        return;
                    }
                    if (txtUserName.Text.Trim() == "")
                    {
                        CommonHelper.ShowMessage("Please enter user name.", msgerror, CommonHelper.MessageType.Error.ToString());
                        txtUserName.Focus();
                        return;
                    }
                    if (txtPassword.Text.Trim() == "")
                    {
                        CommonHelper.ShowMessage("Please enter password.", msgerror, CommonHelper.MessageType.Error.ToString());
                        txtPassword.Focus();
                        return;
                    }
                    if (drpDepartment.SelectedIndex == 0)
                    {
                        string Password = txtPassword.Text;
                        txtPassword.Attributes.Add("value", Password);
                        CommonHelper.ShowMessage("Please select department.", msgerror, CommonHelper.MessageType.Error.ToString());
                        drpDepartment.Focus();
                        return;
                    }
                    if (drpUserType.SelectedIndex == 0)
                    {
                        string Password = txtPassword.Text;
                        txtPassword.Attributes.Add("value", Password);
                        CommonHelper.ShowMessage("Please select user type.", msgerror, CommonHelper.MessageType.Error.ToString());
                        drpUserType.Focus();
                        return;
                    }
                    hidPassword.Value = txtPassword.Text.ToString();
                    BL_UserMaster dlobj = new BL_UserMaster();
                    DataTable dt = dlobj.SaveUser(Session["SiteCode"].ToString(), txtUserID.Text.Trim(), drpUserType.Text.Trim(),
                        drpDepartment.Text.Trim(), txtUserName.Text.Trim(), Encrypt(txtPassword.Text), chkRememberMe.Checked, Session["UserID"].ToString(), txtContactNo.Text, txtemailid.Text);
                    if (dt.Rows.Count > 0)
                    {
                        string sResult = dt.Rows[0][0].ToString();
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
                            _ResetField();
                            string Department = Session["Department"].ToString();
                            ShowGridDataForSuperAdmin(Department);
                            ChangeTypes();
                            CommonHelper.ShowMessage(sResult.Split('~')[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                        }
                    }
                    else
                    {
                        CommonHelper.ShowMessage("No result found.", msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                }
                else
                {
                    BL_UserMaster blobj = new BL_UserMaster();
                    string sResult = blobj.UpdateUser(drpUserType.Text.Trim(), drpDepartment.Text.Trim(), txtUserName.Text.Trim(), Encrypt(txtPassword.Text), chkRememberMe.Checked, hidUID.Value.Trim(), txtContactNo.Text, txtemailid.Text);
                    if (sResult.Length > 0)
                    {
                        if (sResult.StartsWith("ERROR~"))
                        {
                            txtPassword.Text = hidPassword.Value;
                            CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        }
                        if (sResult.StartsWith("N~"))
                        {
                            txtPassword.Text = hidPassword.Value;
                            CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        }
                        else
                        {
                            _ResetField();
                            ShowGridDataForSuperAdmin(Department);
                            ChangeTypes();
                            txtPassword.Text = hidPassword.Value;
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
                if (ex.Message.Contains("Incorrect syntax near"))
                {
                    CommonHelper.ShowMessage("Please note that use of ' is not allowed.", msgerror, CommonHelper.MessageType.Error.ToString());
                    return;
                }
                if (ex.Message.Contains("Violation of PRIMARY KEY"))
                {
                    txtUserID.Text = "";
                    txtUserID.Focus();
                    CommonHelper.ShowMessage("User id already exists. Please enter different id.", msgerror, CommonHelper.MessageType.Error.ToString());
                }
                else
                {
                    txtPassword.Text = hidPassword.Value;
                    CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                }
            }
            finally
            {
                ShowGridDataForSuperAdmin(Department);
            }
        }

        private string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        private void _ResetField()
        {
            txtUserID.Text = "";
            txtPassword.Text = "";
            txtUserName.Text = "";
            txtemailid.Text = "";
            txtContactNo.Text = "";
            drpDepartment.SelectedIndex = 0;
            drpUserType.SelectedIndex = 0;
            txtUserID.ReadOnly = false;
            btnSave.Text = "Save";
            if (drpUserFilter.Items.Count > 0)
            {
                gvUserMaster.DataSource = null;
                gvUserMaster.DataBind();
                drpUserFilter.SelectedIndex = 0;
                DataTable dt = (DataTable)ViewState["Data"];
                gvUserMaster.DataSource = dt;
                gvUserMaster.DataBind();
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvUserMaster.PageIndex = e.NewPageIndex;
                ShowGridDataForSuperAdmin(Department);
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
                string _sUserID = string.Empty;
                string _SiteCode = string.Empty;
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
                PL_UserMaster entity = null;
                List<PL_UserMaster> UserDetailsList = new List<PL_UserMaster>();
                BL_UserMaster dlobj = new BL_UserMaster();
                DataTable dtUserDetails = dlobj.GetSeletedData(_SN);
                if (dtUserDetails.Rows.Count > 0)
                {
                    for (int i = 0; i < dtUserDetails.Rows.Count; i++)
                    {
                        entity = new PL_UserMaster()
                        {
                            USER_ID = dtUserDetails.Rows[i]["UserID"].ToString(),
                            USER_TYPE = dtUserDetails.Rows[i]["UserType"].ToString(),
                            DEPARTMENT = dtUserDetails.Rows[i]["Department"].ToString(),
                            USERNAME = dtUserDetails.Rows[i]["UserName"].ToString(),
                            PASSWORD = dtUserDetails.Rows[i]["Password"].ToString(),
                            ISACTIVE = Convert.ToBoolean(dtUserDetails.Rows[i]["Active"].ToString()),
                            EMAIL = dtUserDetails.Rows[i]["EMAIL"].ToString(),
                            CONTACTNO = dtUserDetails.Rows[i]["CONTACTNO"].ToString(),
                        }; UserDetailsList.Add(entity);
                    }
                }
                else
                {
                    CommonHelper.ShowMessage("No user details found", msgerror, CommonHelper.MessageType.Error.ToString());
                }
                if (UserDetailsList.Count > 0)
                {
                    foreach (PL_UserMaster c in UserDetailsList)
                    {
                        txtUserID.Text = c.USER_ID;
                        drpDepartment.Text = c.DEPARTMENT;
                        drpUserType.Text = c.USER_TYPE;
                        txtUserName.Text = c.USERNAME;
                        hidPassword.Value = c.PASSWORD;
                        txtPassword.Text = hidPassword.Value;
                        chkRememberMe.Checked = c.ISACTIVE;
                        txtContactNo.Text = c.CONTACTNO;
                        txtemailid.Text = c.EMAIL;

                        hidUpdate.Value = "Update";
                        hidUID.Value = _SN;
                        txtUserID.ReadOnly = true;
                    }
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
                BL_UserMaster blobj = new BL_UserMaster();
                string sResult = blobj.DeleteUser(_SN);
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
                        _ResetField();
                        CommonHelper.ShowMessage(sResult.Split('~')[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                        ChangeTypes();
                    }
                }
                else
                {
                    CommonHelper.ShowMessage("No result found", msgerror, CommonHelper.MessageType.Error.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
            finally
            {
                ShowGridDataForSuperAdmin(Department);
            }
        }

        protected void drpUserFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpUserFilter.SelectedIndex > 0)
                {
                    DataTable dt = (DataTable)ViewState["Data"];
                    DataView dataView = dt.DefaultView;
                    dataView.RowFilter = "USERID = '" + drpUserFilter.SelectedValue + "'";
                    gvUserMaster.DataSource = dataView;
                    gvUserMaster.DataBind();
                }
                else
                {
                    DataTable dt = (DataTable)ViewState["Data"];
                    gvUserMaster.DataSource = dt;
                    gvUserMaster.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}