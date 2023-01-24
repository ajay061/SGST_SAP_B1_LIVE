using Common;
using System;
using System.Data;

namespace DataLayer.Reports
{
    public class DL_MaterialIssuenceReport
    {
        DBManager oDbm;
        public DL_MaterialIssuenceReport()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable BindLine()
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(1);
                oDbm.AddParameters(0, "@TYPE", "BINDLINE");
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_MATERIAL_ISSUENCE_REPORT").Tables[0];
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
        public DataTable BindMacineID(string sLineID)
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDMACHINE");
                oDbm.AddParameters(1, "@LINEID", sLineID);
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_MATERIAL_ISSUENCE_REPORT").Tables[0];
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
        public DataTable BindFGItemCode(string sLineID, string sMachineID)
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "BINDFGITEMCODE");
                oDbm.AddParameters(1, "@LINEID", sLineID);
                oDbm.AddParameters(2, "@MACHINEID", sMachineID);
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_MATERIAL_ISSUENCE_REPORT").Tables[0];
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
        public DataTable GetReport(string sLineID, string sMachineID,string sFGItemCode,string sFromDate,string sToDate)
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(6);
                oDbm.AddParameters(0, "@TYPE", "GETREPORT");
                oDbm.AddParameters(1, "@LINEID", sLineID);
                oDbm.AddParameters(2, "@MACHINEID", sMachineID);
                oDbm.AddParameters(3, "@FGITEMCODE", sFGItemCode);
                oDbm.AddParameters(4, "@FROMDATE", sFromDate);
                oDbm.AddParameters(5, "@TODATE", sToDate);
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_MATERIAL_ISSUENCE_REPORT").Tables[0];
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
    }
}
