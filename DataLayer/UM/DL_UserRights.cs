using Common;
using System;
using System.Data;
using System.Text;
using PL;

namespace DataLayer
{
    public class DL_UserRights
    {
        DBManager oDbm;
        public DL_UserRights()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable dlGetUser(string sDepartement)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDUSER");
                oDbm.AddParameters(1, "@DEPARTMENT", sDepartement);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_USERRIGHTS").Tables[0];
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

        public DataTable dlBindModule(string sDepartement)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDMODULE");
                oDbm.AddParameters(1, "@DEPARTMENT", sDepartement);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_USERRIGHTS").Tables[0];
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

        public DataTable dlGetRightsDetails(string sUserID)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "GetAllUserRights");
                oDbm.AddParameters(1, "@USERID", sUserID);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_USERRIGHTS").Tables[0];
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
            
        public int SaveUserRights(StringBuilder sb)
        {
            int iResult = 0;
            try
            {
                oDbm.Open();
                oDbm.BeginTransaction();
                iResult = oDbm.ExecuteNonQuery(System.Data.CommandType.Text, sb.ToString());
                if (iResult > 0)
                {
                    oDbm.CommitTransaction();
                }
                else
                {
                    oDbm.RollbackTransaction();
                }
            }
            catch (Exception ex)
            {
                oDbm.RollbackTransaction();
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                oDbm.Close();
            }
            return iResult;
        }

    }
}
