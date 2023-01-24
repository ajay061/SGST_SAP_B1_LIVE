using System;
using System.Text;
using Common;
using System.Data;

namespace DataLayer
{
    public class DL_SolderPastConfig
    {
        DBManager oDbm;
        public DL_SolderPastConfig()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable BIND_MACHINEID()
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDMACHINE");
                oDbm.AddParameters(1, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_MC_CONFIG_MASTER").Tables[0];
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
        public DataTable BindMachineDetails()
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "FILLMACHINEDETAILS");
                oDbm.AddParameters(1, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_MC_CONFIG_MASTER").Tables[0];
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

        public DataTable EditMachineData(string sMachineID)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "EDITMACHINEDETAILS");
                oDbm.AddParameters(1, "@MACHINEID", sMachineID);
                oDbm.AddParameters(2, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_MC_CONFIG_MASTER").Tables[0];
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

        public DataTable SaveMachineData(string MACHINEID, string MACHINENAME
            , string processtime, string sprocesstimeEnable
             , string Nextprocesstime, string NextprocesstimeEnable)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(8);
                oDbm.AddParameters(0, "@TYPE", "SAVEMACHINE");
                oDbm.AddParameters(1, "@MACHINEID", MACHINEID);
                oDbm.AddParameters(2, "@MACHINENAME", MACHINENAME);
                oDbm.AddParameters(3, "@PROCESS_TIME", processtime);
                oDbm.AddParameters(4, "@PROCESS_TIME_ENABLE", sprocesstimeEnable);
                oDbm.AddParameters(5, "@NEXT_PROCESS_TIME", Nextprocesstime);
                oDbm.AddParameters(6, "@NEXT_PROCESS_TIME_ENABLE", NextprocesstimeEnable);
                oDbm.AddParameters(7, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_MC_CONFIG_MASTER").Tables[0];
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
        public DataTable UpdateMachine(string MACHINEID, string MACHINENAME
          , string processtime, string sprocesstimeEnable
           , string Nextprocesstime, string NextprocesstimeEnable)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(8);
                oDbm.AddParameters(0, "@TYPE", "UpdateMachine");
                oDbm.AddParameters(1, "@MACHINEID", MACHINEID);
                oDbm.AddParameters(2, "@MACHINENAME", MACHINENAME);
                oDbm.AddParameters(3, "@PROCESS_TIME", processtime);
                oDbm.AddParameters(4, "@PROCESS_TIME_ENABLE", sprocesstimeEnable);
                oDbm.AddParameters(5, "@NEXT_PROCESS_TIME", Nextprocesstime);
                oDbm.AddParameters(6, "@NEXT_PROCESS_TIME_ENABLE", NextprocesstimeEnable);
                oDbm.AddParameters(7, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_MC_CONFIG_MASTER").Tables[0];
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
        public DataTable DeleteMachine(string MACHINEID)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "DELETEMACHINE");
                oDbm.AddParameters(1, "@MACHINEID", MACHINEID);
                oDbm.AddParameters(2, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_MC_CONFIG_MASTER").Tables[0];
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
