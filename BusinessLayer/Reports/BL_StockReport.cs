using System;
using System.Data;
using Common;
using DataLayer.Reports;

namespace BusinessLayer.Reports
{
    public class BL_StockReport
    {
        DL_StockTakeReport dlboj = null;
        public DataTable GetReport(string sType,
            string sFromDate, string sTODate
            )
        {
            dlboj = new DL_StockTakeReport();
            try
            {
                return dlboj.GetReportData(sType,
                      sFromDate, sTODate);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
    }
}
