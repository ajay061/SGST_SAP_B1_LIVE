using System;
using System.Data;
using Common;
using DataLayer.Reports;

namespace BusinessLayer.Reports
{
    public class BL_Job_Work_Report
    {
        DL_Job_Work_Report dlobj = new DL_Job_Work_Report();
        public DataTable BindOrderNoForJobWorkReport()
        {
            DataTable dtGRN = new DataTable();
            dlobj = new DL_Job_Work_Report();
            try
            {
                dtGRN = dlobj.BindOrderNo();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtGRN;
        }
        public DataTable GetReport(string sFromDate,string sTODate,string sOrderNo)
        {
            DataTable dtGRN = new DataTable();
            dlobj = new DL_Job_Work_Report();
            try
            {
                dtGRN = dlobj.GetReport(sOrderNo, sFromDate, sTODate);
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
