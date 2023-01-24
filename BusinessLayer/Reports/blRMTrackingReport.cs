using System;
using System.Data;
using Common;
using DataLayer.Reports;

namespace BusinessLayer.Reports
{
    public class blRMTrackingReport
    {
        DL_RM_Tracking_Report dlboj;
        public DataTable BindPartCodeForRMTrackingReport()
        {
            DataTable dtGRN = new DataTable();
            dlboj = new DL_RM_Tracking_Report();
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
        public DataTable GetReport(string sPartCode,string sLocationType,string sLocationCode 
            ,string sGRPONo,string sBatchNO 
            )
        {
            DataTable dtGRN = new DataTable();
            dlboj = new DL_RM_Tracking_Report();
            try
            {
                dtGRN = dlboj.GetReport(sPartCode, sLocationType, sLocationCode, sGRPONo
                    , sBatchNO
                    );
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
