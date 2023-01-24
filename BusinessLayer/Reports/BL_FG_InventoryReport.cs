using System;
using System.Data;
using Common;
using DataLayer.Reports;

namespace BusinessLayer.Reports
{
    public class BL_FG_InventoryReport
    {
        DL_FG_InventoryReport dlboj = null;
        public DataTable BindFGItemCode()
        {
            DataTable dtGRN = new DataTable();
            dlboj = new DL_FG_InventoryReport();
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
        public DataTable GetReport(string sFromDate,string sTODate,string sFGitemCode)
        {
            DataTable dtGRN = new DataTable();
            dlboj = new DL_FG_InventoryReport();
            try
            {
                dtGRN = dlboj.GetReport(sFromDate, sTODate, sFGitemCode);
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
