using Common;
using System;
using System.Data;
namespace DataLayer
{
    public class DL_UserMaster
    {
        DBManager oDbm;
        public DL_UserMaster()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }    

        public DataTable dlGetUsersForSuperAdmin(string sUserID,string sDepartMent)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@USERID", sUserID);
                oDbm.AddParameters(1, "@TYPE", "GETUSERFORSUPERADMIN");
                oDbm.AddParameters(2, "@SITECODE", PCommon.sSiteCode);
                oDbm.AddParameters(3, "@DEPARTMENT", sDepartMent);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_USERMASTER").Tables[0];
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

        public DataTable dlGetSeletedData(string _SN)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@SN", _SN);
                oDbm.AddParameters(1, "@TYPE", "BINDATA");
                oDbm.AddParameters(2, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_USERMASTER").Tables[0];
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

        public DataTable dlSaveUserData(string _SiteCode, string _UserID, string _UserType, 
            string _Department, string _UserName, string _Password, 
            bool _active, string _CreatedBy, string ContactNo, string emailid)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(11);
                oDbm.AddParameters(0, "@TYPE", "SAVE");
                oDbm.AddParameters(1, "@SITECODE", _SiteCode);
                oDbm.AddParameters(2, "@USERID", _UserID);
                oDbm.AddParameters(3, "@USERNAME", _UserName);
                oDbm.AddParameters(4, "@PASSWORD", _Password);
                oDbm.AddParameters(5, "@USERTYPE", _UserType);
                oDbm.AddParameters(6, "@DEPARTMENT", _Department);
                oDbm.AddParameters(7, "@ACTIVE", _active);
                oDbm.AddParameters(8, "@CREATED_BY", _CreatedBy);
                oDbm.AddParameters(9, "@CONTACTNO", ContactNo);
                oDbm.AddParameters(10, "@EMAIL", emailid);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_USERMASTER").Tables[0];
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

        public DataTable dlUpdateUser(string _UserType,
           string _Department, string _UserName, string _Password,
           bool _active, string iContactNo, string emailid,
            int sn )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(10);
                oDbm.AddParameters(0, "@TYPE", "UPDATE");
                oDbm.AddParameters(1, "@USERNAME", _UserName);
                oDbm.AddParameters(2, "@PASSWORD", _Password);
                oDbm.AddParameters(3, "@USERTYPE", _UserType);
                oDbm.AddParameters(4, "@DEPARTMENT", _Department);
                oDbm.AddParameters(5, "@ACTIVE", _active);
                oDbm.AddParameters(6, "@CONTACTNO", iContactNo);
                oDbm.AddParameters(7, "@EMAIL", emailid);
                oDbm.AddParameters(8, "@SN", sn);
                oDbm.AddParameters(9, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_USERMASTER").Tables[0];
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

        public DataTable dlDeleteUser(string _SN)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@SN", _SN);
                oDbm.AddParameters(1, "@TYPE", "DELETEUSER");
                oDbm.AddParameters(2, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_USERMASTER").Tables[0];
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
    }
}
