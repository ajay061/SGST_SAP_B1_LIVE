using System;
using System.Data;
using Common;
using DataLayer.Reports;

namespace BusinessLayer.WIP
{
    public class BL_WIP_INVENTORY_REPORT
    {
        DL_WIP_InventoryReport dlobj;
        public DataTable BINDPARTCODE()
        {
            DataTable dtGRN = new DataTable();
            dlobj = new DL_WIP_InventoryReport();
            try
            {           
                dtGRN = dlobj.BindPartCode();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtGRN;
        }
        public DataTable GetReport(string sWORKOrderNo,string sPartCode,string sGRPONo,string sBatchNo)
        {
            DataTable dtGRN = new DataTable();
            dlobj = new DL_WIP_InventoryReport();
            try
            {
                dtGRN = dlobj.GetReport(sWORKOrderNo, sPartCode, sGRPONo,sBatchNo);
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
