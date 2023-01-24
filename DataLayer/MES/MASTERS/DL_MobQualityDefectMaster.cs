using Common;
using System;
using System.Data;

namespace DataLayer.Masters
{
    public class DL_MobQualityDefectMaster
    {
        DBManager oDbm;
        public DL_MobQualityDefectMaster()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable BindFilterDefect(string sSiteCode,string sLineCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "BINDDEFECT");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.AddParameters(2, "@LINECODE", sLineCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_MOB_QUALITY_FAILURE_MASTER").Tables[0];
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
        public DataTable SearchDetailsByID(string sQSDCode,string sSiteCode, string sLineCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "SEARCH");
                oDbm.AddParameters(1, "@QDSCODE", sQSDCode);
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                oDbm.AddParameters(3, "@LINECODE", sLineCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_MOB_QUALITY_FAILURE_MASTER").Tables[0];
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
        public DataTable SearchDetails(string sSiteCode, string sLineCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "GETALL");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.AddParameters(2, "@LINECODE", sLineCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_MOB_QUALITY_FAILURE_MASTER").Tables[0];
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
        public DataTable SaveData(DataTable dtData,string sSiteCode, string sUserID,string sLineCode,string sArea)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(6);
                oDbm.AddParameters(0, "@TYPE", "INSERT");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.AddParameters(2, "@LINECODE", sLineCode);
                oDbm.AddParameters(3, "@DETAIL", dtData);
                oDbm.AddParameters(4, "@CREATED_BY", sUserID);
                oDbm.AddParameters(5, "@AREA", sArea);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_MOB_QUALITY_FAILURE_MASTER").Tables[0];
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
        public DataTable UpdateData(DataTable dtData,string sMachineID,
            string sSiteCode, string sUserID, string sLineCode,string sArea)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(7);
                oDbm.AddParameters(0, "@TYPE", "UPDATE");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.AddParameters(2, "@LINECODE", sLineCode);
                oDbm.AddParameters(3, "@DETAIL", dtData);
                oDbm.AddParameters(4, "@CREATED_BY", sUserID);
                oDbm.AddParameters(5, "@MACHINEID", sMachineID);
                oDbm.AddParameters(6, "@AREA", sArea);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_MOB_QUALITY_FAILURE_MASTER").Tables[0];
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
