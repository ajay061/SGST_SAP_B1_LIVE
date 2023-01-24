using System;
using System.Data;
using Common;
using DataLayer.Reports;
namespace BusinessLayer.Reports
{
    public class BL_MachineFailureReport
    {
        DL_MachineFailureReport dlboj = null;
        public DataTable BindFGItemCode()
        {
            DataTable dtGRN = new DataTable();
            dlboj = new DL_MachineFailureReport();
            try
            {
                dtGRN = dlboj.BindFGitemCode();
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
            DataTable dtMCID = new DataTable();
            dlboj = new DL_MachineFailureReport();
            try
            {
                dtMCID = dlboj.BindMachineID(sFGItemCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtMCID;
        }

        public DataTable GetReport(string sWorkOrderNo,string sFGItemCode, string sMachineID)
        {
            DataTable dtMCID = new DataTable();
            dlboj = new DL_MachineFailureReport();
            try
            {
                dtMCID = dlboj.GetReport(sWorkOrderNo,sFGItemCode, sMachineID);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtMCID;
        }
    }
}
