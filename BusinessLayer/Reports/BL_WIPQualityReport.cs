using System;
using System.Data;
using Common;
using DataLayer.Reports;

namespace BusinessLayer.Reports
{
    public class BL_WIPQualityReport
    {
        WIPQualityReport dlobj;
        public DataTable BindPartCode()
        {
            DataTable dtGRN = new DataTable();
            dlobj = new WIPQualityReport();
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

        public DataTable GetReport(string sPartCode, string sFromDate, string sToDate, string sReportType,string sWOrkOrderNo)
        {
            DataTable dtGRN = new DataTable();
            dlobj = new WIPQualityReport();
            try
            {
                dtGRN = dlobj.GetReport(sPartCode, sFromDate, sToDate, sReportType, sWOrkOrderNo);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtGRN;
        }

        public DataTable BindReworkFGItemCode()
        {
            DataTable dtGRN = new DataTable();
            dlobj = new WIPQualityReport();
            try
            {
                dtGRN = dlobj.BindReworkFGItemCode();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtGRN;
        }

        public DataTable GetReworkData(string sPartCode, string sFromDate, string sToDate
            , string sWorkOrderNo,string sPartBarcode)
        {
            DataTable dtGRN = new DataTable();
            dlobj = new WIPQualityReport();
            try
            {
                dtGRN = dlobj.GetREWORKReport(sPartCode, sFromDate, sToDate, sWorkOrderNo,sPartBarcode);
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
