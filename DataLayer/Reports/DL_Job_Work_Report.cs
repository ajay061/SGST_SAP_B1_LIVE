using Common;
using System;
using System.Data;

namespace DataLayer.Reports
{
    public class DL_Job_Work_Report
    {
        DBManager oDbm;
        public DL_Job_Work_Report()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable BindOrderNo()
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(1);
                oDbm.AddParameters(0, "@TYPE", "BINDORDERNO");
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_JOB_WORK_REPORT").Tables[0];
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
        public DataTable GetReport(string sOrderNo,
   string sFromDate, string sTODate)
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@ORDERNO", sOrderNo);
                oDbm.AddParameters(1, "@FROMDATE", sFromDate);
                oDbm.AddParameters(2, "@TODATE", sTODate);
                oDbm.AddParameters(3, "@TYPE", "GETREPORT");
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_JOB_WORK_REPORT").Tables[0];
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
