using System;
using Common;
using System.Data;
namespace DataLayer
{
    public class DL_FmsMaster
    {
        DBManager oDbm;
        public DL_FmsMaster()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable BindLine()
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BindLine");
                oDbm.AddParameters(1, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_FMS_MC_MASTER").Tables[0];
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
        public DataTable BindMachine(string sLineID)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "BINDMACHINE");
                oDbm.AddParameters(1, "@LINEID", sLineID);
                oDbm.AddParameters(2, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_FMS_MC_MASTER").Tables[0];
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

        public DataTable FillMachineDetails()
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "FILLMACHINEDETAILS");
                oDbm.AddParameters(1, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_FMS_MC_MASTER").Tables[0];
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

        public DataTable SaveFMSDetails(string Machineid, string FMS_TOP_IP, string FMS_TOP_IP_ENABLE,
            string LINE_ID, string FMS_LOCATION, string FMSPort)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(8);
                oDbm.AddParameters(0, "@TYPE", "SAVEMACHINE");
                oDbm.AddParameters(1, "@MACHINEID", Machineid);
                oDbm.AddParameters(2, "@FMSIP", FMS_TOP_IP);
                oDbm.AddParameters(3, "@FMSPORT", FMSPort);
                oDbm.AddParameters(4, "@ENABLE", FMS_TOP_IP_ENABLE);
                oDbm.AddParameters(5, "@FMSLOC", FMS_LOCATION);
                oDbm.AddParameters(6, "@LINEID", LINE_ID);
                oDbm.AddParameters(7, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_FMS_MC_MASTER").Tables[0];
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

        public DataTable UpdateFMS(string Machineid, string FMS_TOP_IP, string FMS_TOP_IP_ENABLE,
          string LINE_ID, string FMS_LOCATION, string FMSPort)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(8);
                oDbm.AddParameters(0, "@TYPE", "UpdateMachine");
                oDbm.AddParameters(1, "@MACHINEID", Machineid);
                oDbm.AddParameters(2, "@FMSIP", FMS_TOP_IP);
                oDbm.AddParameters(3, "@FMSPORT", FMSPort);
                oDbm.AddParameters(4, "@ENABLE", FMS_TOP_IP_ENABLE);
                oDbm.AddParameters(5, "@FMSLOC", FMS_LOCATION);
                oDbm.AddParameters(6, "@LINEID", LINE_ID);
                oDbm.AddParameters(7, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_FMS_MC_MASTER").Tables[0];
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

        public DataTable DeleteFMS(string Machineid, string FMS_TOP_IP)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "DELETEMACHINE");
                oDbm.AddParameters(1, "@MACHINEID", Machineid);
                oDbm.AddParameters(2, "@FMSIP", FMS_TOP_IP);
                oDbm.AddParameters(3, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_FMS_MC_MASTER").Tables[0];
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
