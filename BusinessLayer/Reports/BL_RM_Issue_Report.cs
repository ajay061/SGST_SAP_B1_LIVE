using Common;
using DataLayer.Reports;
using System;
using System.Data;

namespace BusinessLayer.Reports
{
    public class BL_RM_Issue_Report
    {
        DL_RM_Issue_Report dlboj = null;
        public DataTable BindPartCode()
        {
            DataTable dtGRN = new DataTable();
            dlboj = new DL_RM_Issue_Report();
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

        public DataTable BindFGITemCode()
        {
            DataTable dtGRN = new DataTable();
            dlboj = new DL_RM_Issue_Report();
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
        public DataTable GetReport(string sFromDate, string sTODate, string sIssueSlipNo
            ,string sItemCode,string sGRPONo,string sBatchNo
            )
        {
            DataTable dtGRN = new DataTable();
            dlboj = new DL_RM_Issue_Report();
            try
            {
                dtGRN = dlboj.GetReport(sFromDate, sTODate, sIssueSlipNo, sItemCode,sGRPONo,sBatchNo);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtGRN;
        }

        public DataTable MasterDataReport(string sFromDate, string sTODate, string sIssueSlipNo
            ,string sReportType,string sFGItemCode
            )
        {
            DataTable dtGRN = new DataTable();
            dlboj = new DL_RM_Issue_Report();
            try
            {
                dtGRN = dlboj.GetMasterDataReport(sFromDate, sTODate, sIssueSlipNo, sReportType, sFGItemCode);
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
