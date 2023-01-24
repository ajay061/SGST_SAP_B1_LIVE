using Common;
using DataLayer.Reports;
using System;
using System.Data;

namespace BusinessLayer.Reports
{
    public class BL_WORK_ORDER_TRACEABILITY_REPORT
    {
        DL_WorkOrderTraceablityReport dlobj;
        public DataTable BindPartCode()
        {
            DataTable dtGRN = new DataTable();
            dlobj = new DL_WorkOrderTraceablityReport();
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
        public DataTable BindWorkOrderNo(string sFGItemCode)
        {
            DataTable dtGRN = new DataTable();
            dlobj = new DL_WorkOrderTraceablityReport();
            try
            {
                dtGRN = dlobj.BindWorkOrderNo(sFGItemCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtGRN;
        }
        public DataTable GetReport(string sFGItemCode,string sWorkOrderNo)
        {
            DataTable dtGRN = new DataTable();
            dlobj = new DL_WorkOrderTraceablityReport();
            try
            {
                dtGRN = dlobj.GetReport(sFGItemCode, sWorkOrderNo);
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
