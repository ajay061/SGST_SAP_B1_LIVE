using Common;
using DataLayer.Reports;
using System;
using System.Data;

namespace BusinessLayer.Reports
{
    public class BL_WIP_StageWiseReport
    {
        DL_StageWiseTrackingReport dlobj;
        public DataTable BindMachineID()
        {
            DataTable dtGRN = new DataTable();
            dlobj = new DL_StageWiseTrackingReport();
            try
            {
                dtGRN = dlobj.BindMachineiD();
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
            dlobj = new DL_StageWiseTrackingReport();
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
        public DataTable GetReport(string sMachineID,string sFGItemCode,string sWorkOrderNo)
        {
            DataTable dtGRN = new DataTable();
            dlobj = new DL_StageWiseTrackingReport();
            try
            {
                dtGRN = dlobj.GetReport(sMachineID, sFGItemCode, sWorkOrderNo);
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
