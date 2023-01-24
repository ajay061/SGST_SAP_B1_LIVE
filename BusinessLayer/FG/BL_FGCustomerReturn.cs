using System;
using System.Data;
using DataLayer.FG;
using Common;
namespace BusinessLayer.FG
{
    public class BL_FGCustomerReturn
    {
        DL_FGCustomerReturn dlboj = null;
        public DataTable BindReturnSlipNo(string sSiteCode)
        {
            DataTable dtPickList = new DataTable();
            dlboj = new DL_FGCustomerReturn();
            try
            {
                dtPickList = dlboj.BindReturnSlipNo(sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtPickList;
        }

        public DataTable GetSlipDetails(string sReturnSlipNo, string sSiteCode)
        {
            DataTable dtPickList = new DataTable();
            dlboj = new DL_FGCustomerReturn();
            try
            {
                dtPickList = dlboj.GetReturnSlipNo(sReturnSlipNo, sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtPickList;
        }

        public string sScanFGReturnBarcode(string sReturnSlipNo, string ITEM_CODE, string sBoxBarcode, string sLocation
          , string sUserID, string sLineCode, string sSiteCode
            )
        {
            string sResult = string.Empty;
            dlboj = new DL_FGCustomerReturn();
            DataTable dt = new DataTable();
            try
            {
                dt = dlboj.SaveRetrunData(sReturnSlipNo, ITEM_CODE, sBoxBarcode, sLocation
                    , sUserID, sLineCode, sSiteCode
                    );
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
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
            PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtMessage, System.Reflection.MethodBase.GetCurrentMethod().Name, "Result : " + sResult);
            return sResult;
        }




        public string CheckFGBarcode(string _sBarcode, string sSiteCode)
        {
            DataTable dtPickList = new DataTable();
            string sReturn = string.Empty;
            DL_FG_Customer_Return_Quality dlboj = new DL_FG_Customer_Return_Quality();
            try
            {
                dtPickList = dlboj.ValidateFGBarcode(_sBarcode, sSiteCode);
                if (dtPickList.Rows.Count > 0)
                {
                    sReturn = dtPickList.Rows[0][0].ToString();
                }
                else
                {
                    sReturn = "N~No result found for scanned barcode";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sReturn;
        }
        public string UpdateCustomerReturnQuality(string _sBarcode, string _val, string _ScanBy, string sRemarks, string sObservation
            , string sSiteCode, string sLineCode
            )
        {
            DataTable dtPickList = new DataTable();
            string sReturn = string.Empty;
            DL_FG_Customer_Return_Quality dlboj = new DL_FG_Customer_Return_Quality();
            try
            {
                dtPickList = dlboj.SaveReturnQualityData(_sBarcode, _val, _ScanBy, sRemarks, sObservation
                    , sSiteCode, sLineCode
                    );
                if (dtPickList.Rows.Count > 0)
                {
                    sReturn = dtPickList.Rows[0][0].ToString();
                }
                else
                {
                    sReturn = "N~No result found for scanned barcode";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sReturn;
        }
    }
}
