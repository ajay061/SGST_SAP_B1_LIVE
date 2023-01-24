using Common;
using DataLayer.Reports;
using System;
using System.Data;
namespace BusinessLayer.Reports
{
    public class BL_PCB_Serial_Gen_Report
    {
        DL_PCB_Serial_Gen_Report dlboj;
        public DataTable BindFGItemCode()
        {
            DataTable dtGRN = new DataTable();
            dlboj = new DL_PCB_Serial_Gen_Report();
            try
            {
                dtGRN = dlboj.BindFGItemCode();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtGRN;
        }
        public DataTable GetReport(string sFGItemCode, string sFromDate, string sToDate,string sWorkOrderNo)
        {
            DataTable dtData = new DataTable();
            dlboj = new DL_PCB_Serial_Gen_Report();
            try
            {
                dtData = dlboj.GetReport(sFGItemCode, sFromDate, sToDate, sWorkOrderNo);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtData;
        }
    }
}
