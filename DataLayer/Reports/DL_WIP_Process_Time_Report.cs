using System;
using Common;
using System.Data;
namespace DataLayer.Reports
{
    public class DL_WIP_Process_Time_Report
    {
        DBManager oDbm;
        public DL_WIP_Process_Time_Report()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable BindProcessID()
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(1);
                oDbm.AddParameters(0, "@TYPE", "BINDPROCESS");
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_WIP_PROCESS_TIME_REPORT").Tables[0];
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

        public DataTable BindFGItemCode()
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(1);
                oDbm.AddParameters(0, "@TYPE", "BINDFGITEMCODE");
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_WIP_PROCESS_TIME_REPORT").Tables[0];
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
        public DataTable GetReport(string sProcessID, string sFGItemCode,string sFromDate,string sToDate)
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(5);
                oDbm.AddParameters(0, "@TYPE", "GETREPORT");
                oDbm.AddParameters(1, "@PROCESSID", sProcessID);
                oDbm.AddParameters(2, "@FGITEMCODE", sFGItemCode);
                oDbm.AddParameters(3, "@FROMDATE", sFromDate);
                oDbm.AddParameters(4, "@TODATE", sToDate);
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_WIP_PROCESS_TIME_REPORT").Tables[0];
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
