using Common;
using System;
using System.Data;
using System.Reflection;
using System.Text;

namespace DataLayer
{
    public class DL_WIPMachineMaster
    {
        DBManager oDbm;
        public DL_WIPMachineMaster()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable BindLine(string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BindLine");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_MACHINE_MASTER").Tables[0];
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

        public DataTable BindMachine(string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDMACHINE");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_MACHINE_MASTER").Tables[0];
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

        public DataTable FillMachineDetails(string sMachineID, string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "FILLMACHINEDETAILS");
                oDbm.AddParameters(1, "@MACHINEID", sMachineID);
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_MACHINE_MASTER").Tables[0];
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

        public DataTable SaveMachine(string sMachineCode, string sMachineName, string sMachineDesc,
            string _sLineID, string _sCreBy, string type, int iMachineSeq, string sMachineFileValidate
            ,string sSiteCode
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(10);
                oDbm.AddParameters(0, "@TYPE", "SAVEMACHINE");
                oDbm.AddParameters(1, "@MACHINEID", sMachineCode);
                oDbm.AddParameters(2, "@MACHINENAME", sMachineName);
                oDbm.AddParameters(3, "@DESCRIPTION", sMachineDesc);
                oDbm.AddParameters(4, "@LINEID", _sLineID);
                oDbm.AddParameters(5, "@CREATED_BY", _sCreBy);
                oDbm.AddParameters(6, "@MACHINETYPE", type);
                oDbm.AddParameters(7, "@MACHINESEQ", iMachineSeq);
                oDbm.AddParameters(8, "@MACHINE_VALIDATION", sMachineFileValidate);
                oDbm.AddParameters(9, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_MACHINE_MASTER").Tables[0];
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

        public DataTable UpdateMachine(string _sMachineID, string _sMName, string _sMachinedes, string _sLineID,
            string _sCreby, string type, int iMachineSeq, string sMachineFileValidate
            , string sSiteCode
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(10);
                oDbm.AddParameters(0, "@TYPE", "UpdateMachine");
                oDbm.AddParameters(1, "@MACHINEID", _sMachineID);
                oDbm.AddParameters(2, "@MACHINENAME", _sMName);
                oDbm.AddParameters(3, "@DESCRIPTION", _sMachinedes);
                oDbm.AddParameters(4, "@LINEID", _sLineID);
                oDbm.AddParameters(5, "@CREATED_BY", _sCreby);
                oDbm.AddParameters(6, "@MACHINETYPE", type);
                oDbm.AddParameters(7, "@MACHINESEQ", iMachineSeq);
                oDbm.AddParameters(8, "@MACHINE_VALIDATION", sMachineFileValidate);
                oDbm.AddParameters(9, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_MACHINE_MASTER").Tables[0];
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

        public DataTable DeleteMachine(string machineid, string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "DELETEMACHINE");
                oDbm.AddParameters(1, "@MACHINEID", machineid);
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_MACHINE_MASTER").Tables[0];
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

        public DataTable UploadData(DataTable dtUpload, string sUserID)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "UPLOADDETAILS");
                oDbm.AddParameters(1, "@DETAILS", dtUpload);
                oDbm.AddParameters(2, "@CREATED_BY", sUserID);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_MACHINE_MASTER").Tables[0];
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
