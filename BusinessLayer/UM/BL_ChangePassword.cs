using Common;
using DataLayer;
using System;
using System.Text;
using System.Data;

namespace BusinessLayer
{
    public class BL_ChangePassword
    {
        DL_ChangePassword dlboj = null;
        public string ChangePasswords(string _sUserID, string _sNewPassword)
        {
            string sResult = string.Empty;
            dlboj = new DL_ChangePassword();
            try
            {
                DataTable dt = dlboj.dlChangePassword(_sUserID, _sNewPassword);
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }
    }
}
