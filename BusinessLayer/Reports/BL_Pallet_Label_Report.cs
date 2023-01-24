using Common;
using DataLayer.Reports;
using System;
using System.Data;
namespace BusinessLayer.Reports
{
    public class BL_Pallet_Label_Report
    {
        DL_PALLET_Label_Report dlobj;
        public DataTable BINDPACKINGLIST()
        {
            DataTable dtGRN = new DataTable();
            dlobj = new DL_PALLET_Label_Report();
            try
            {
                dtGRN = dlobj.dtBindPackingListno();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtGRN;
        }
        public DataTable GetReport(string sPicklistNo)
        {
            DataTable dtData = new DataTable();
            dlobj = new DL_PALLET_Label_Report();
            try
            {
                dtData = dlobj.GetReport(sPicklistNo);
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
