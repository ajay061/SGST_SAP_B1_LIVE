using System;
using System.Data;
using Common;
using DataLayer.Reports;

namespace BusinessLayer.Reports
{
    public class BL_WIPReworkReport
    {
        DL_WIP_ReworkReport dlboj = null;
        public DataTable GetMachineID()
        {
            dlboj = new DL_WIP_ReworkReport();
            try
            {
                return dlboj.dtBindMachineID();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        public DataTable GetFGItemCode()
        {
            dlboj = new DL_WIP_ReworkReport();
            try
            {
                return dlboj.dtBindFGItemCode();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        public DataTable GetReworkSationId()
        {
            dlboj = new DL_WIP_ReworkReport();
            try
            {
                return dlboj.dtReworkStationId();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }

        public DataTable GetReport(string sMachineID,
            string sFromDate, string sTODate
            , string sFgItemCode, string sType,string sWorkOrderNo,string sPCBID, string sReworkStID
            )
        {
            dlboj = new DL_WIP_ReworkReport();
            try
            {
                return dlboj.GetReportData(sMachineID,
                      sFromDate, sTODate
            , sFgItemCode, sType, sWorkOrderNo, sPCBID,sReworkStID
                    );
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
    }
}
