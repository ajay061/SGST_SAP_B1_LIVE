using Common;
using System;
using System.Data;

namespace DataLayer.WIP
{
    public class DL_ProfileMaster
    {
        DBManager oDbm;
        public DL_ProfileMaster()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }

        public DataTable GetPartCode()
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDPARTCODE");
                oDbm.AddParameters(1, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_PROFILE_MASTER").Tables[0];
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
        public DataTable BindMachine()
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDMACHINE");
                oDbm.AddParameters(1, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_PROFILE_MASTER").Tables[0];
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
        public DataTable BindProgram()
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "GETPROGRAMID");
                oDbm.AddParameters(1, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_PROFILE_MASTER").Tables[0];
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
        public DataTable GetProgramDetails(string _sProgramID, string sMachineID)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "GETPROGRAMDETAILS");
                oDbm.AddParameters(1, "@PROGRAMID", _sProgramID);
                oDbm.AddParameters(2, "@MACHINEID", sMachineID);
                oDbm.AddParameters(3, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_PROFILE_MASTER").Tables[0];
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

        public DataTable ValidateTool(string sToolID)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "VALIDATETOOL");
                oDbm.AddParameters(1, "@TOOLID", sToolID);
                oDbm.AddParameters(2, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_PROFILE_MASTER").Tables[0];
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

        public DataTable SaveProgram(DataTable _profilemst, bool ProgramEnable,string sUserID)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(5);
                oDbm.AddParameters(0, "@TYPE", "SAVEPROGRAM");
                oDbm.AddParameters(1, "@DETAILS", _profilemst);
                oDbm.AddParameters(2, "@ISPROGRAMENABLE", ProgramEnable);
                oDbm.AddParameters(3, "@SITECODE", PCommon.sSiteCode);
                oDbm.AddParameters(4, "@DOWNLOADEDBY", sUserID);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_UPLOAD_PROGRAM_MASTER").Tables[0];
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
        public DataTable DeleteProgram(string sProgramID,string sPartCode,string sMachineID)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(5);
                oDbm.AddParameters(0, "@TYPE", "DELETEPROGRAM");
                oDbm.AddParameters(1, "@PROGRAMID", sProgramID);
                oDbm.AddParameters(2, "@PARTCODE", sPartCode);
                oDbm.AddParameters(3, "@MACHINEID", sMachineID);
                oDbm.AddParameters(4, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_PROFILE_MASTER").Tables[0];
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
        public DataTable DeleteCompleteProgram(string sProgramID)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "DELETECOMPLETEPROGRAM");
                oDbm.AddParameters(1, "@PROGRAMID", sProgramID);
                oDbm.AddParameters(2, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_PROFILE_MASTER").Tables[0];
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

        public DataTable ValidateData(DataTable dt, string type)
        {
            DataTable dt1 = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@DETAILS", dt);
                oDbm.AddParameters(1, "@TYPE", type);
                oDbm.AddParameters(2, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt1 = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_UPLOAD_PROGRAM_MASTER").Tables[0];
                oDbm.Close();
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
            return dt1;
        }
    }
}
