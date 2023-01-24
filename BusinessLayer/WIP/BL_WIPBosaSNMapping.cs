using Common;
using System;
using System.Data;
using DataLayer.WIP;


namespace BusinessLayer.WIP
{
    public class BL_WIPBosaSNMapping
    {

        DL_BosaSNMapping dlobj;
        public DataTable ValidateMachine(string sMachineID, string sSiteCode, string sLineCode)
        {
            DataTable dtLocation = new DataTable();
            dlobj = new DL_BosaSNMapping();
            try
            {
                dtLocation = dlobj.ValidateMachine(sMachineID, sSiteCode, sLineCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLocation;
        }
        public DataTable BindFGItemCode(string sMachineID, string sSiteCode, string sLineCode)
        {
            DataTable dtFGItemCode = new DataTable();
            try
            {
                dlobj = new DL_BosaSNMapping();
                dtFGItemCode = dlobj.BindFGItemCode(sMachineID, sSiteCode, sLineCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtFGItemCode;
        }
        public DataTable ValidateBosaBarcode(string sPartBarcode)
        {
            DataTable dtLineData = new DataTable();
            try
            {
                dlobj = new DL_BosaSNMapping();
                dtLineData = dlobj.VaildateBosaBarcode(sPartBarcode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLineData;
        }
        public DataTable SaveQualtiyData(string sPartBarcode, string sMachineID, string FGItemCode
       , string sSiteCode, string sLineCode, string sUseriD, string sBosaBarcode
            )
        {
            DataTable dtLineData = new DataTable();
            try
            {
                dlobj = new DL_BosaSNMapping();
                dtLineData = dlobj.SaveData(sPartBarcode, sMachineID, FGItemCode
                   , sSiteCode, sLineCode, sUseriD, sBosaBarcode
                   );
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLineData;
        }
    }
}
