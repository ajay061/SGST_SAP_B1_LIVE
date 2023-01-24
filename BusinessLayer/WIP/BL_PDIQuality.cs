using Common;
using System;
using System.Data;
using DataLayer.WIP;

namespace BusinessLayer.WIP
{
    public class BL_PDIQuality
    {
        DL_WIP_PDIQuality dlobj;
        public DataTable BindFGItemCode(string sSiteCode)
        {
            DataTable dtFGItemCode = new DataTable();
            try
            {
                dlobj = new DL_WIP_PDIQuality();
                dtFGItemCode = dlobj.BindFGItemCode(sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtFGItemCode;
        }
        public DataTable BindReWorkStationID(string sSiteCode)
        {
            DataTable dtLineData = new DataTable();
            try
            {
                dlobj = new DL_WIP_PDIQuality();
                dtLineData = dlobj.BindReworkstation(sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLineData;
        }

        public DataTable BindDefect(string sSiteCode)
        {
            DataTable dtLineData = new DataTable();
            try
            {
                dlobj = new DL_WIP_PDIQuality();
                dtLineData = dlobj.BindDefect(sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLineData;
        }
        public DataTable ValidatePCB(string sPartBarcode,string FGItemCode
            , string sSiteCode, string sLineCode
            )
        {
            DataTable dtLineData = new DataTable();
            try
            {
                dlobj = new DL_WIP_PDIQuality();
                dtLineData = dlobj.VaildateBarcode(sPartBarcode,  FGItemCode
                    , sSiteCode, sLineCode
                    );
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLineData;
        }
        public DataTable SaveQualtiyData(string sPartBarcode,string FGItemCode
       , string sDefect, string sReworkStation, string sObservation, string sStatus
            , string sSiteCode, string sLineCode, string sUseriD
            )
        {
            DataTable dtLineData = new DataTable();
            try
            {
                dlobj = new DL_WIP_PDIQuality();
                dtLineData = dlobj.SaveData(sPartBarcode, FGItemCode
                   , sDefect, sReworkStation, sObservation, sStatus
                   , sSiteCode, sLineCode, sUseriD
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
