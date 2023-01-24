using System;
using DataLayer.WIP;
using System.Data;
using Common;

namespace BusinessLayer.WIP
{
    public class BL_WIP_BakingProcess
    {
        DL_WIP_BackingProcess dlobj = new DL_WIP_BackingProcess();
        public DataTable BindFGItemCode(string sSiteCode, string sUserID, string sLineCode)
        {
            DataTable dt = new DataTable();
            try
            {
                dlobj = new DL_WIP_BackingProcess();
                dt = dlobj.BindFGItemCode(sSiteCode, sUserID, sLineCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dt;
        }
        public DataTable BindWorkOrderNo(string sFGItemCode, string sSiteCode, string sLineCode)
        {
            DataTable dt = new DataTable();
            try
            {
                dlobj = new DL_WIP_BackingProcess();
                dt = dlobj.BindWorkOrderNo(sFGItemCode, sSiteCode, sLineCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dt;
        }
        public DataTable dtValidatePCB(string sPCBBarcode, string sType, string sFGItemCode, string sWorkOrderNo
            , string sSiteCode, string sUserID, string sLineCode
            )
        {
            DataTable dt = new DataTable();
            try
            {
                dlobj = new DL_WIP_BackingProcess();
                dt = dlobj.ValidateReel(sPCBBarcode, sType, sFGItemCode, sWorkOrderNo
                    , sSiteCode, sUserID, sLineCode
                    );
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dt;
        }
        public DataTable BindPendingBarcode(string sFGItemCode, string sWorkOrderNo
            , string sSiteCode, string sLineCode
            )
        {
            DataTable dt = new DataTable();
            try
            {
                dlobj = new DL_WIP_BackingProcess();
                dt = dlobj.BindPendingBarcode(sFGItemCode, sWorkOrderNo
                    ,sSiteCode, sLineCode
                    );
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dt;
        }
    }
}
