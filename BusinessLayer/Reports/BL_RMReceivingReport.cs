using Common;
using DataLayer.Reports;
using System;
using System.Data;

namespace BusinessLayer.Reports
{
    public class BL_RMReceivingReport
    {
        DL_RMReceivingReport dlboj;
      
        public DataTable BindPartCode()
        {
            DataTable dtGRN = new DataTable();
            dlboj = new DL_RMReceivingReport();
            try
            {
                dtGRN = dlboj.BindPartCode();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtGRN;
        }
        public DataTable GetReport(string sGRPONo, string sPartCode, string sLocation,string sSearchData
            ,string sFromDate,string sToDate
            )
        {
            DataTable dtData = new DataTable();
            dlboj = new DL_RMReceivingReport();
            try
            {
                dtData = dlboj.GetReport(sPartCode, sGRPONo, sLocation, sSearchData
                    , sFromDate,sToDate
                    );
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
