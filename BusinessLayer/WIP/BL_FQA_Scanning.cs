using Common;
using System;
using System.Data;
using DataLayer.WIP;
namespace BusinessLayer.WIP
{
    public class BL_FQA_Scanning
    {
        DL_FQAScanning dlobj;
        public DataTable ValidateMachine(string sMachineID,string sSiteCode,string sLineCode)
        {
            DataTable dtLocation = new DataTable();
            dlobj = new DL_FQAScanning();
            try
            {
                dtLocation = dlobj.ValidateMachine(sMachineID, sSiteCode,sLineCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLocation;
        }
        public DataTable BindFGItemCode(string sMachineID, out string result, string sSiteCode, string sLineCode)
        {
            DataTable dtFGItemCode = new DataTable();
            try
            {
                dlobj = new DL_FQAScanning();
                dtFGItemCode = dlobj.BindFGItemCode(sMachineID, sSiteCode, sLineCode);
                if (dtFGItemCode.Rows.Count > 0)
                {
                    result = "SUCCESS~";
                }
                else
                {
                    result = "N~No FG item code found";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtFGItemCode;
        }

        public DataTable BindReWorkStationID(out string result,string sSiteCode)
        {
            DataTable dtLineData = new DataTable();
            try
            {
                dlobj = new DL_FQAScanning();
                dtLineData = dlobj.BindReworkstation(sSiteCode);
                if (dtLineData.Rows.Count > 0)
                {
                    result = "SUCCESS~";
                }
                else
                {
                    result = "N~No details found";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLineData;
        }

        public DataTable BindDefect(out string result,string sSiteCode)
        {
            DataTable dtLineData = new DataTable();
            try
            {
                dlobj = new DL_FQAScanning();
                dtLineData = dlobj.BindDefect(sSiteCode);
                if (dtLineData.Rows.Count > 0)
                {
                    result = "SUCCESS~";
                }
                else
                {
                    result = "N~No details found";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLineData;
        }
        
        public DataTable BindFQASampling(string sFGItemCode,string sSiteCode, string sMachineID)
        {
            DataTable dtLineData = new DataTable();
            try
            {
                dlobj = new DL_FQAScanning();
                dtLineData = dlobj.BindSamplingRate(sFGItemCode, sSiteCode, sMachineID);                
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLineData;
        }
        public DataTable ValidatePCB(string sPartBarcode, string sMachineID, string FGItemCode
            , string sRefNo,string sSiteCode,string sLineCode)
        {
            DataTable dtLineData = new DataTable();
            try
            {
                dlobj = new DL_FQAScanning();
                dtLineData = dlobj.VaildateBarcode(sPartBarcode, sMachineID, FGItemCode, sRefNo
                    , sSiteCode,sLineCode
                    );
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLineData;
        }
        public DataTable SaveFQAResult(string sPartBarcode, string sMachineID, string FGItemCode
           , string sDefect, string sReworkStation, string sObservation, string sStatus
             , string sRefNo, DataTable dtQualityData, string sQualityFullBatch
            ,string sSiteCode,string sLineCode,string sUserID
            )
        {
            DataTable dtLineData = new DataTable();
            try
            {
                dlobj = new DL_FQAScanning();
                dtLineData = dlobj.SaveData(sPartBarcode, sMachineID, FGItemCode
                   , sDefect, sReworkStation, sObservation, sStatus
                   , sRefNo, dtQualityData, sQualityFullBatch
                   , sSiteCode,sLineCode,sUserID
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
