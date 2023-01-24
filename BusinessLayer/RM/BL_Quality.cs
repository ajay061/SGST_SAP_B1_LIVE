using Common;
using DataLayer;
using System;
using System.Data;
using System.Text;

namespace BusinessLayer
{
    public class BL_Quality
    {
        DL_Quality dlboj = null;
        public string GetBarcodeCode(string _PartBarcode, bool isMRN, string sSiteCode)
        {
            string sBarcodeResult = string.Empty;
            dlboj = new DL_Quality();
            try
            {
                DataTable dt = dlboj.ScanPCBBarcode(_PartBarcode, isMRN, sSiteCode);
                if (dt.Rows.Count > 0)
                {
                    sBarcodeResult = dt.Rows[0][0].ToString();
                }
                else
                {
                    sBarcodeResult = "N~No result found for scanned barcode";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                sBarcodeResult = "ERROR~" + ex.Message.ToString();
            }
            return sBarcodeResult;
        }
        public DataSet BindDefect(string sItemCode, string sSiteCode)
        {
            DataSet dtPartCode = new DataSet();
            dlboj = new DL_Quality();
            try
            {
                dtPartCode = dlboj.BindDefect(sItemCode, sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtPartCode;
        }
        public string QualityUpdate(string _qStatus, decimal _dApprovedQty, string _PartBarcode, string sGRNNo, string sItemLineNo
            , string _PartCode
            , DataTable dtDefect, string sRemarks
            , string sSiteCode, string sUseriD, string sLineCode,string sQualityResult
            )
        {
            DataTable dt = new DataTable();
            string sResult = string.Empty;
            dlboj = new DL_Quality();
            try
            {
                dt = dlboj.dtQuality(_qStatus, _dApprovedQty, _PartBarcode, sGRNNo, sItemLineNo
            , _PartCode, dtDefect, sRemarks
            , sSiteCode, sUseriD, sLineCode, sQualityResult
            );
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
                }
                else
                {
                    sResult = "N~Quality updated failed";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }
        public string QualityUpdateFullBatch(string _qStatus, decimal _dApprovedQty, string _PartBarcode,
            string _Batch, string _PartCode
            , string sGRNNo, string sItemLineNo
            , DataTable dtDefect, string sRemarks
             , string sSiteCode, string sUseriD, string sLineCode, string sQualityResult
            )
        {
            DataTable dt = new DataTable();
            string sResult = string.Empty;
            dlboj = new DL_Quality();
            try
            {
                dt = dlboj.dtQualityFullBatch(_qStatus, _dApprovedQty, _PartBarcode, sGRNNo, sItemLineNo
           , _PartCode, _Batch, dtDefect, sRemarks, sSiteCode, sUseriD, sLineCode
           , sQualityResult
           );
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
                }
                else
                {
                    sResult = "N~Quality updated failed";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }



        public string QualityMRNBarcode(string _qStatus, decimal _dApprovedQty, string _PartBarcode,
            string sGRNNo, string sItemLineNo
            , string _PartCode, string sScannedBy
            , DataTable dtDefect, string sRemarks
              , string sSiteCode, string sLineCode, string sQualityResult
            )
        {
            DataTable dt = new DataTable();
            string sResult = string.Empty;
            dlboj = new DL_Quality();
            try
            {
                dt = dlboj.dtMRNQuality(_qStatus, _dApprovedQty, _PartBarcode, sGRNNo, sItemLineNo
           , _PartCode, sScannedBy, dtDefect, sRemarks
           , sSiteCode, sLineCode, sQualityResult
           );
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
                }
                else
                {
                    sResult = "N~Quality updated failed";
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
