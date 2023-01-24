using System;
using DataLayer.WIP;
using System.Data;
using Common;

namespace BusinessLayer.WIP
{
    public class BL_WIP_AgingProcess
    {
        DL_WIP_Aging_Process dlobj = new DL_WIP_Aging_Process();
        public DataTable BindFGItemCode(string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                dlobj = new DL_WIP_Aging_Process();
                dt = dlobj.BindFGItemCode(sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError,  System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dt;
        }
        
        public DataTable SaveAgingData(string sPCBBarcode, string sType, string sFGItemCode
            , string sSiteCode, string sUserID, string sLineCode
            )
        {
            DataTable dt = new DataTable();
            try
            {
                dlobj = new DL_WIP_Aging_Process();
                dt = dlobj.AGINGPROCESS(sPCBBarcode, sType, sFGItemCode
                    , sSiteCode, sUserID, sLineCode
                    );
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dt;
        }       
    }
}
