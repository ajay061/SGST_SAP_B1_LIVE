using Common;
using DataLayer.Reports;
using System;
using System.Data;
namespace BusinessLayer.Reports
{
    public class BL_WIP_ProcessTimeReport
    {
        DL_WIP_Process_Time_Report dlobj;
        public DataTable BindProcessID()
        {
            DataTable dtGRN = new DataTable();
            dlobj = new DL_WIP_Process_Time_Report();
            try
            {
                dtGRN = dlobj.BindProcessID();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtGRN;
        }
        public DataTable BindFGItemCode()
        {
            DataTable dtGRN = new DataTable();
            dlobj = new DL_WIP_Process_Time_Report();
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
        public DataTable GetReport(string sProcessiD, string sFGItemCode, string sFromDate, string sToDate)
        {
            DataTable dtGRN = new DataTable();
            dlobj = new DL_WIP_Process_Time_Report();
            try
            {
                dtGRN = dlobj.GetReport(sProcessiD, sFGItemCode, sFromDate, sToDate);
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
