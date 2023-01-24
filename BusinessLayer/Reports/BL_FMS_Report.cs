using System;
using System.Data;
using Common;
using DataLayer.Reports;

namespace BusinessLayer.Reports
{
    public class BL_FMS_Report
    {
        DL_FMS_Report dlboj = null;
        public DataTable BINDMACHINEID()
        {
            DataTable dtGRN = new DataTable();           
            dlboj = new DL_FMS_Report();
            try
            {
                dtGRN = dlboj.dtBindMachineID();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtGRN;
        }
        public DataTable BindFGITEMCODE(string MACHINEID)
        {
            DataTable dtMCID = new DataTable();
            dlboj = new DL_FMS_Report();
            try
            {
                dtMCID = dlboj.BindFGItemCode(MACHINEID);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtMCID;
        }
        public DataTable GetFMSReport(string sMachineID,
   string sFromDate, string sTODate, string sFGItemCode, string sType)
        {
            DataTable dtMCID = new DataTable();
            dlboj = new DL_FMS_Report();
            try
            {
                dtMCID = dlboj.GetFMSReport(sMachineID, sFromDate, sTODate, sFGItemCode, sType);
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
