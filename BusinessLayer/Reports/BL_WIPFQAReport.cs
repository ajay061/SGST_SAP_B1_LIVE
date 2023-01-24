using System;
using System.Data;
using Common;
using DataLayer.Reports;

namespace BusinessLayer.Reports
{
    public class BL_WIPFQAReport
    {
        DL_WIPFQAReport dlobj;
        public DataTable BindPartCode()
        {
            DataTable dtGRN = new DataTable();
            dlobj = new DL_WIPFQAReport();
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

        public DataTable GetReport(string sPartCode,string sWorkOrderNo, string sFromDate, string sToDate,string sReportType)
        {
            DataTable dtGRN = new DataTable();
            dlobj = new DL_WIPFQAReport();
            try
            {
                dtGRN = dlobj.GetReport(sPartCode, sWorkOrderNo, sFromDate, sToDate, sReportType);
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
