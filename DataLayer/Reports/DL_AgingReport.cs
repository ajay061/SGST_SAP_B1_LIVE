using Common;
using System;
using System.Data;

namespace DataLayer.Reports
{
    public class DL_AgingReport
    {
        DBManager oDbm;
        public DL_AgingReport()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable BindFGItemCode()
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(1);
                oDbm.AddParameters(0, "@TYPE", "BINDFGITEMCODE");
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_AGING_REPORT").Tables[0];
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

        public DataTable getReport(string sFGitemCode,
   string sFromDate, string sTODate, string sReportType)
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(5);
                oDbm.AddParameters(0, "@FGITEMCODE", sFGitemCode);
                oDbm.AddParameters(1, "@FROMDATE", sFromDate);
                oDbm.AddParameters(2, "@TODATE", sTODate);
                oDbm.AddParameters(3, "@TYPE", "GETREPORT");
                oDbm.AddParameters(4, "@REPORTTYPE", sReportType);
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_AGING_REPORT").Tables[0];
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
