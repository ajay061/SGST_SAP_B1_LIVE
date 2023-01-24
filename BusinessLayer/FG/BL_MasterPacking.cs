using System;
using System.Data;
using System.Text;
using DataLayer.FG;
using Common;
using DataLayer;

namespace BusinessLayer.FG
{
    public class BL_MasterPacking
    {
        DL_MasterPacking dlboj = null;
        public DataTable BindPickList(string sSiteCode,string sLineCode)
        {
            DataTable dtPickList = new DataTable();
            dlboj = new DL_MasterPacking();
            try
            {
                dtPickList = dlboj.BindPickListNo(sSiteCode, sLineCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtPickList;
        }

        public DataTable BindPackingList(string sPickListNo,string sSiteCode,string sLineCode)
        {
            DataTable dtPickList = new DataTable();
            dlboj = new DL_MasterPacking();
            try
            {
                dtPickList = dlboj.BindPackingListNo(sPickListNo, sSiteCode,sLineCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtPickList;
        }

        public DataTable BindScanItemDetails(string sPickListNo, string sPackingListNo,string sSiteCode,string sLineCode)
        {
            DataTable dtPickList = new DataTable();
            dlboj = new DL_MasterPacking();
            try
            {
                dtPickList = dlboj.BindDetails(sPickListNo, sPackingListNo, sSiteCode,sLineCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtPickList;
        }
        public string CheckpalletBarcode(string _spallet,string sSiteCode,string sLineCode)
        {
            dlboj = new DL_MasterPacking();
            string sResult = string.Empty;
            try
            {
                DataTable dtPickList = dlboj.ValidatePalletBarcode(_spallet, sSiteCode,sLineCode);
                if (dtPickList.Rows.Count > 0)
                {
                    sResult = "Success~ Pallet found";
                }
                else
                {
                    sResult = "N~ Scanned pallet barcode : " + _spallet + " does not exists.";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }
        public string FGPalletPacking(string sPickListNo, string sPackingListNo,
            string sPrinted_By, DataTable dtBarcodes, string _PalletID
            ,string sSiteCode,string sLineCode
            )
        {
            dlboj = new DL_MasterPacking();
            string sResult = string.Empty;
            try
            {
                DataTable dtPickList = dlboj.PalletAllocation(sPickListNo, sPackingListNo,
            sPrinted_By, dtBarcodes, _PalletID, sSiteCode,sLineCode);
                if (dtPickList.Rows.Count > 0)
                {
                    sResult = dtPickList.Rows[0][0].ToString();
                }
                else
                {
                    sResult = "N~No result found";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }
    }
}
