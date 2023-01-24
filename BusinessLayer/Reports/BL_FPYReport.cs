using System;
using System.Data;
using Common;
using DataLayer.Reports;


namespace BusinessLayer.Reports
{
    public class BL_FPYReport
    {
        DL_FPYReport dlboj = null;
        public DataTable BindFGItemCode(string sHeaderValue)
        {
            DataTable dtGRN = new DataTable();
            dlboj = new DL_FPYReport();
            try
            {
                dtGRN = dlboj.BindFGItemCode(sHeaderValue);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtGRN;
        }
        public DataTable GetReport(string sFromDate, string sTODate, string sFGitemCode,string sHeaderValue )
        {
            DataTable dtReport = new DataTable();
            dlboj = new DL_FPYReport();
            try
            {
                dtReport = dlboj.GetReport(sFromDate, sTODate, sFGitemCode, sHeaderValue);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtReport;
        }
    }
}
