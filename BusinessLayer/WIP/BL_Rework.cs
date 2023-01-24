using Common;
using DataLayer.WIP;
using System;
using System.Data;
namespace BusinessLayer
{
    public class BL_Rework
    {
        DL_WIPRework dlboj = null;
        public DataTable BindReWorkStationID(out string result,string sSiteCode)
        {
            DataTable dtLineData = new DataTable();
            try
            {
                dlboj = new DL_WIPRework();
                dtLineData = dlboj.BindReworkStation(sSiteCode);
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
        public DataTable BindDefectMaster(out string result, string sType, string sMachineID
            ,string sSiteCode
            )
        {
            DataTable dtLineData = new DataTable();
            try
            {
                dlboj = new DL_WIPRework();
                dtLineData = dlboj.BindDefect(sMachineID, sSiteCode);
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

        public DataTable BindRejectedPCB(out string result, string sPartBarcode,string sSiteCode)
        {
            DataTable dtLineData = new DataTable();
            try
            {
                dlboj = new DL_WIPRework();
                dtLineData = dlboj.ValidateRejectedPCB(sPartBarcode, sSiteCode);
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

        public DataTable ValidateJioFG(string sFGItemCode, string sSiteCode)
        {
            DataTable dtLineData = new DataTable();
            try
            {
                dlboj = new DL_WIPRework();
                dtLineData = dlboj.ValidateJioFGItem(sFGItemCode, sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLineData;
        }

        public string checkandSave(string _sPartBarcode, string _sReworkStation,
            string sType, string _sScannedby,string sSiteCode,string sLineCode
            )
        {
            dlboj = new DL_WIPRework();
            DataTable dt;
            string sResult = string.Empty;
            try
            {
                dt = dlboj.ValidateBarcode(_sPartBarcode, _sReworkStation,
            sType, _sScannedby, sSiteCode,sLineCode);
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
                }
                else
                {
                    sResult = "N~No result found for scanned barcode";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }


        public string RepairOut(string _sPartBarcode, string _sReworkStation,
           string _sobser, string _sRemrk, string sType, string _sScannedby, string sDefect, string IS_Scraped
           , string sPCBType, string sSiteCode, string sLineCode, DataTable dtReworkData, string sMoveingStage,
           string sJIORepairType)
        {
            dlboj = new DL_WIPRework();
            DataTable dt;
            string sResult = string.Empty;
            try
            {
                dt = dlboj.RepairOut(_sPartBarcode, _sReworkStation, sType, _sScannedby,
           _sobser, _sRemrk, sDefect, sPCBType, IS_Scraped, sSiteCode,sLineCode, dtReworkData, sMoveingStage
           , sJIORepairType
           );
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
                }
                else
                {
                    sResult = "N~Scanned PCB barcode does not exist in database for selected data";
                    return sResult;
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }
    }
}
