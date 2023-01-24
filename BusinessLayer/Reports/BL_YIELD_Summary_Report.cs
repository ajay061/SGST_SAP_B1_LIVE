using Common;
using DataLayer.Reports;
using System;
using System.Data;

namespace BusinessLayer.Reports
{
    public class BL_YIELD_Summary_Report
    {
        DL_Yield_Summary_Report dlobj;
        public DataTable BindPartCode()
        {
            DataTable dtGRN = new DataTable();
            dlobj = new DL_Yield_Summary_Report();
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
        public DataTable BindMachineID(string sFGItemCode)
        {
            DataTable dtGRN = new DataTable();
            dlobj = new DL_Yield_Summary_Report();
            try
            {
                dtGRN = dlobj.BindMachine(sFGItemCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtGRN;
        }
        public DataTable GetReport(string sPartCode,string sFromDate,string sToDate,string sReportType,string sMachineID
            ,string sWorkOrderNo
            )
        {
            DataTable dtGRN = new DataTable();
            dlobj = new DL_Yield_Summary_Report();
            try
            {
                dtGRN = dlobj.GetReport(sPartCode, sFromDate,sToDate, sReportType, sMachineID, sWorkOrderNo);
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
