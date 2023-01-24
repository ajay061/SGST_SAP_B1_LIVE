using System;
using System.Data;
using Common;
using DataLayer.Reports;

namespace BusinessLayer.Reports
{
    public class BL_SAPPostingDataReport
    {
        DL_SAPPostingDataReport dlboj = null;
        public DataTable GetReport(string sType, string sOrderNo)
        {
            dlboj = new DL_SAPPostingDataReport();
            try
            {
                return dlboj.GetReportData(sType, sOrderNo);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        public DataTable GetReprintReport(string sType, string sFromDate, string sTODate)
        {
            dlboj = new DL_SAPPostingDataReport();
            try
            {
                return dlboj.GetReprintData(sType, sFromDate, sTODate);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        public DataTable GetASNWifiAPSAPData(string txtInvoiceNo, string TextModelNo)
        {
            dlboj = new DL_SAPPostingDataReport();
            try
            {
                return dlboj.GetASNWifiAPSAPData(txtInvoiceNo, TextModelNo);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
    }
}
