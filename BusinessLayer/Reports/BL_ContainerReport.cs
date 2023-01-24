using Common;
using DataLayer.Reports;
using System;
using System.Data;

namespace BusinessLayer
{
    public class BL_ContainerReport
    {
        DL_ContainerReport dlobj;  
        public DataTable BINDPACKINGLIST()
        {
            DataTable dtGRN = new DataTable();
            dlobj = new DL_ContainerReport();
            try
            {
                dtGRN = dlobj.dtBindPackingListno();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError,  System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtGRN;
        }
        public DataTable GetShipperReport(string sPicklistNo, string sFromDate, string sToDate)
        {
            DataTable dtData = new DataTable();
            dlobj = new DL_ContainerReport();
            try
            {
                dtData = dlobj.GetShipperReport(sPicklistNo, sFromDate, sToDate);
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
