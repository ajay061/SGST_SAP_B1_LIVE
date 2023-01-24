using Common;
using System;
using System.Data;
using System.Text;

namespace DataLayer
{
    public class DL_UserLogin
    {
        DBManager oDbm;
        public DL_UserLogin()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataSet dlGetSiteCode()
        {
            DataSet ds = new DataSet();
            try
            {
                oDbm.CreateParameters(1);
                oDbm.AddParameters(0, "@TYPE", "GETSITECODE");
                oDbm.Open();
                ds = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_USERLOGIN");
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return ds;
        }
        public DataTable GetData(StringBuilder sb)
        {
            DataTable dt = new DataTable();          
            try
            {
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.Text,sb.ToString()).Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                oDbm.Close();
            }
            return dt;
        }

        public DataTable UserLogin(string _sUserID, string _sPassword, string _sSiteCode, string sFingerValidate)
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(5);
                oDbm.AddParameters(0, "@Type", "VALIDATELOGIN");
                oDbm.AddParameters(1, "@USERID", _sUserID);
                oDbm.AddParameters(2, "@SITECODE", _sSiteCode);
                oDbm.AddParameters(3, "@PASSWORD", _sPassword);
                oDbm.AddParameters(4, "@FingerValidate", sFingerValidate);
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_USERLOGIN").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dtResult;
        }

        public DataTable GetGroupRights(string userid, string sitecode)
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@Type", "GET_RIGHTS_USER");
                oDbm.AddParameters(1, "@USERID", userid);
                oDbm.AddParameters(2, "@SITECODE", sitecode);
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_USERLOGIN").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dtResult;
        }

        public DataTable dlGetUserDetails(string sUserID)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "GETUSERDETAILS");
                oDbm.AddParameters(1, "@USERID", sUserID);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_USERLOGIN").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dt;
        }

        #region ValidateLoginOncompleteCartonPallet
        public DataTable dtValidteUserForSuperVisor(string sUserID,string sPassword,string sModuleName)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "VALIDATEUSERCOMPLETEBOXPALLET");
                oDbm.AddParameters(1, "@USERID", sUserID);
                oDbm.AddParameters(2, "@PASSWORD", sPassword);
                oDbm.AddParameters(3, "@MODULENAME", sModuleName);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_USERLOGIN").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dt;
        }

        #endregion
    }
}
