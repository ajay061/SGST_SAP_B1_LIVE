using Common;
using System;
using System.Data;


namespace DataLayer.Reports
{
    public class DL_StockTakeReport
    {
        DBManager oDbm;
        public DL_StockTakeReport()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable GetReportData(string sType,
           string sFromDate, string sTODate)
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", sType);
                oDbm.AddParameters(1, "@FROMDATE", sFromDate);
                oDbm.AddParameters(2, "@TODATE", sTODate);
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_PHYSICAL_STOCK_REPORT").Tables[0];
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
