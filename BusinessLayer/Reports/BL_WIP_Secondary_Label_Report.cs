using System;
using System.Data;
using Common;
using DataLayer.Reports;

namespace BusinessLayer.Reports
{
    public class BL_WIP_Secondary_Label_Report
    {
        DL_WIP_Secondary_Packing_Report dlboj = null;
        public DataTable BindFGItemCode()
        {
            DataTable dtGRN = new DataTable();
            dlboj = new DL_WIP_Secondary_Packing_Report();
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
        public DataTable GetReport(string sFromDate, string sTODate, string sFGitemCode,string sBoxID)
        {
            DataTable dtGRN = new DataTable();
            dlboj = new DL_WIP_Secondary_Packing_Report();
            try
            {
                dtGRN = dlboj.GetReport(sFromDate, sTODate, sFGitemCode, sBoxID);
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
