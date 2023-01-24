using Common;
using System;
using System.Data;
using System.Text;
using DataLayer;
using System.Configuration;

namespace BusinessLayer
{
    public class BL_UserLogin
    {
        DL_UserLogin dlobj = null;
        public DataSet GetSiteCode()
        {
            DataSet dtSiteCode = new DataSet();
            dlobj = new DL_UserLogin();
            try
            {
                dtSiteCode = dlobj.dlGetSiteCode();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtSiteCode;
        }

        public string UserLogin(string _sUserID, string _sPassword, string _sSiteCode)
        {
            string sResult = string.Empty;
            DataTable dtUserLogin = new DataTable();
            dlobj = new DL_UserLogin();
            try
            {
                string sFingerValidate = Convert.ToString(ConfigurationManager.AppSettings["_FINGERVALIDATE"].ToString());
                dtUserLogin = dlobj.UserLogin(_sUserID, _sPassword, _sSiteCode, sFingerValidate);
                if (dtUserLogin.Rows.Count > 0)
                {
                    sResult = dtUserLogin.Rows[0][0].ToString();
                    if (sResult.StartsWith("N~"))
                    {
                        return sResult;
                    }
                    else
                    {
                        string sDepartment = dtUserLogin.Rows[0].Field<string>("DEPARTMENT");
                        string sUser = dtUserLogin.Rows[0].Field<string>("USERID");
                        if (sUser.Length == 0)
                        {
                            sResult = "N~Invalid user id or password.";
                        }
                        else
                        {
                            if (sDepartment != "WIP")
                            {
                                sResult = "SUCCESS~Data Found";
                            }
                            else
                            {
                                if (sFingerValidate == "1")
                                {
                                    StringBuilder sb = new StringBuilder();
                                    sb.AppendLine("  SELECT TOP(1) PUNCHDATETIME, GETDATE() [CURRENT_TIME] FROM etimetracklite1.dbo.mBIOMETRICS WHERE EMPCODE  = '" + _sUserID + "'  ");
                                    sb.AppendLine(" order by PUNCHDATETIME desc ");
                                    dtUserLogin = new DataTable();
                                    dtUserLogin = dlobj.GetData(sb);
                                    if (dtUserLogin.Rows.Count > 0)
                                    {
                                        DateTime dtLoginTime = Convert.ToDateTime(dtUserLogin.Rows[0]["PUNCHDATETIME"].ToString());
                                        DateTime dtCurrentTime = Convert.ToDateTime(dtUserLogin.Rows[0]["CURRENT_TIME"].ToString());
                                        if (dtCurrentTime > dtLoginTime.AddMinutes(2))
                                        {
                                            sResult = "N~ Biometric punch time expired, please punch again";
                                        }
                                        else
                                        {
                                            sResult = "SUCCESS~Data Found";
                                        }
                                    }
                                    else
                                    {
                                        sResult = "N~ User ID is not configured in the Biometric m/c";
                                    }
                                }
                                else
                                {
                                    sResult = "SUCCESS~Data Found";
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }

        public DataTable GetuserDetails(string _sUserID)
        {
            DataTable dtUserDetails = new DataTable();
            dlobj = new DL_UserLogin();
            try
            {              
                dtUserDetails = dlobj.dlGetUserDetails(_sUserID);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtUserDetails;
        }


        public DataTable GetGroupRights(string userid, string sitecode)
        {
            DL_UserLogin _DL_UserMaster = new DL_UserLogin();
            return _DL_UserMaster.GetGroupRights(userid, sitecode);
        }
        public DataTable dtValidateCompleteButton(string userid, string sPassword, string sModule )
        {
            DL_UserLogin _DL_UserMaster = new DL_UserLogin();
            return _DL_UserMaster.dtValidteUserForSuperVisor(userid, sPassword, sModule);
        }
    }
}
