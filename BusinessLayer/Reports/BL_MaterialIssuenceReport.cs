using System;
using System.Data;
using Common;
using DataLayer.Reports;

namespace BusinessLayer.Reports
{
    public class BL_MaterialIssuenceReport
    {
        DL_MaterialIssuenceReport dlboj;
        public DataTable BINDLINES()
        {
            DataTable dtGRN = new DataTable();
            dlboj = new DL_MaterialIssuenceReport();
            try
            {
                dtGRN = dlboj.BindLine();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtGRN;
        }

        public DataTable GetMachineRecord(string sLineID)
        {
            DataTable dtMachine = new DataTable();
            try
            {
                DL_MaterialIssuenceReport dlboj = new DL_MaterialIssuenceReport();
                dtMachine = dlboj.BindMacineID(sLineID);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtMachine;
        }

        public DataTable BindFGItemCode(string sLineID, string sMachineID)
        {
            DataTable dtMachine = new DataTable();
            try
            {
                dlboj = new DL_MaterialIssuenceReport();
                dtMachine = dlboj.BindFGItemCode(sLineID, sMachineID);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtMachine;
        }
        public DataTable GetReport(string sLineID, string sMachineID, string sFGItemCode, string sFromDate, string sToDate)
        {
            DataTable dtMachine = new DataTable();
            try
            {
                dlboj = new DL_MaterialIssuenceReport();
                dtMachine = dlboj.GetReport(sLineID, sMachineID, sFGItemCode, sFromDate, sToDate);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtMachine;
        }
    }
}
