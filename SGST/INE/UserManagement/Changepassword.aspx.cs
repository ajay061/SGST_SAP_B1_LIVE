using Common;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using BusinessLayer;

namespace SGST.INE.UserManagement
{
    public partial class Changepassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtUserId.Text = Session["Userid"].ToString();
            txtUserId.ReadOnly = true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNewPassword.Text.Trim() == "")
                {
                    CommonHelper.ShowMessage("Please enter new password", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtNewPassword.Focus();
                    return;
                }
                if (txtConfirmPassword.Text.Trim() == "")
                {
                    CommonHelper.ShowMessage("Please enter confirmation password", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtConfirmPassword.Focus();
                    return;
                }
                if (txtNewPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
                {
                    CommonHelper.ShowMessage("Password does not match.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtConfirmPassword.Focus();
                    txtConfirmPassword.Text = string.Empty;
                    return;
                }
                else
                {
                    BL_ChangePassword blobj = new BL_ChangePassword();
                    string sResult = blobj.ChangePasswords(txtUserId.Text.Trim(), Encrypt(txtNewPassword.Text.Trim()));
                    CommonHelper.ShowMessage(sResult, msgsuccess, CommonHelper.MessageType.Success.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowMessage(ex.Message, msgerror, CommonHelper.MessageType.Error.ToString());
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
        }

        private string Encrypt(string clearText)
        {
            try
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
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return clearText;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtConfirmPassword.Text = "";
            txtNewPassword.Text = "";
        }
    }
}