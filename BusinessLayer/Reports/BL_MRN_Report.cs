using System;
using System.Data;
using Common;
using DataLayer.Reports;
namespace BusinessLayer.Reports
{
    public class BL_MRN_Report
    {
        DL_MRN_Report dlobj;
        public DataTable BindPartCode()
        {
            DataTable dtGRN = new DataTable();
            dlobj = new DL_MRN_Report();
            try
            {
                dtGRN = dlobj.BindPartCode();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtGRN;
        }
        public DataTable GetReport(string sPartCode, string sFromDate, string sToDate)
        {
            DataTable dtGRN = new DataTable();
            dlobj = new DL_MRN_Report();
            try
            {
                dtGRN = dlobj.GetReport(sPartCode, sFromDate, sToDate);
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
