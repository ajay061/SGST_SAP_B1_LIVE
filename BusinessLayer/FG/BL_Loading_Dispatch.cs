using System;
using System.Data;
using System.Text;
using Common;
using DataLayer.FG;

namespace BusinessLayer.FG
{
    public class BL_Loading_Dispatch
    {
        DL_LoadingandDispatch dlboj = null;
        public DataTable BindPickList(string sSiteCode, string sLineCode)
        {
            DataTable dtPickList = new DataTable();
            dlboj = new DL_LoadingandDispatch();
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

        public DataTable BindPackingList(string sPickListNo, string sSiteCode, string sLineCode)
        {
            DataTable dtPackingList = new DataTable();
            dlboj = new DL_LoadingandDispatch();
            try
            {
                dtPackingList = dlboj.BindPackingListno(sPickListNo, sSiteCode, sLineCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtPackingList;
        }

        public DataTable BindDetails(string sPickListNo, string sPackingListNo,string sSiteCode,string sLineCode)
        {
            DataTable dtPackingList = new DataTable();
            dlboj = new DL_LoadingandDispatch();
            try
            {
                dtPackingList = dlboj.GetDetails(sPickListNo, sPackingListNo, sSiteCode,sLineCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtPackingList;
        }

        public string LoadingDispatch(string sPackingListNo, string sPicklistNo, string sBarcode, string sScannedBy, string sType
            , string sSiteCode, string sLineCode
            )
        {
            DataTable dtPackingList = new DataTable();
            dlboj = new DL_LoadingandDispatch();
            string sResult = string.Empty;
            try
            {
                dtPackingList = dlboj.LoadingAndDispatch(sPackingListNo, sPicklistNo,
                    sBarcode, sScannedBy, sType, sSiteCode, sLineCode);
                if (dtPackingList.Rows.Count > 0)
                {
                    sResult = dtPackingList.Rows[0][0].ToString();
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
    }
}
