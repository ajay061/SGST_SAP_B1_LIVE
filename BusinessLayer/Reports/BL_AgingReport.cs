using System;
using System.Data;
using Common;
using DataLayer.Reports;

namespace BusinessLayer.Reports
{
    public class BL_AgingReport
    {
        DL_AgingReport dlobj = new DL_AgingReport();
        public DataTable BindFGitemCode()
        {
            DataTable dtGRN = new DataTable();
            dlobj = new DL_AgingReport();
            try
            {
                dtGRN = dlobj.BindFGItemCode();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtGRN;
        }

        public DataTable getReport(string sFromDate, string sTODate, string sFGItemCode
            , string sReportType
            )
        {
            DataTable dtGRN = new DataTable();
            dlobj = new DL_AgingReport();
            try
            {
                dtGRN = dlobj.getReport(sFGItemCode, sFromDate, sTODate, sReportType);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtGRN;
        }
    }
}
