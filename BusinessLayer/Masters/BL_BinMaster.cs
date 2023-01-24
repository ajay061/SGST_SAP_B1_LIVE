using Common;
using DataLayer;
using System;
using System.Data;

namespace BusinessLayer
{
    public class BL_BinMaster
    {
        DL_BinMaster dlobj = null;
        public DataTable GetBins(string sSiteCode)
        {
            DataTable dtBins = new DataTable();
            dlobj = new DL_BinMaster();
            try
            {
                dtBins = dlobj.GetBins(sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtBins;
        }

        public DataTable GetSeletedData(string _SN)
        {
            DataTable dt = new DataTable();
            dlobj = new DL_BinMaster();
            try
            {
                dt = dlobj.GetBinsByID(_SN);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dt;
        }
        public string SaveBin(string _BinID, string _BinDesc, string _StorageArea, string _Capacity
            , string sPartCode, string sUserID, string sSiteCode
            )
        {
            DataTable dt = new DataTable();
            dlobj = new DL_BinMaster();
            string sResult = string.Empty;
            try
            {
                dt = dlobj.SaveDeleteBin(_BinID, _BinDesc, _StorageArea, Convert.ToInt32(_Capacity), "1", "SAVEBINDETAILS"
                    , sPartCode, sUserID, sSiteCode
                    );
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
                }
                else
                {
                    sResult = "N~No Result found";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }

        public string DeleteBin(string _BinID, string sUserID, string sSiteCode)
        {
            string sResult = string.Empty;
            dlobj = new DL_BinMaster();
            try
            {
                DataTable dt = dlobj.SaveDeleteBin(_BinID, "", "", 0, "1", "DELETEBIN", ""
                    , sUserID, sSiteCode
                    );
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
                }
                else
                {
                    sResult = "N~No Result found";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }
        public string UpdateBin(string _BinDescription, string _StorageArea, string _Capacity, string sN
             , string sPartCode, string sUserID, string sSiteCode)
        {
            string sResult = string.Empty;
            dlobj = new DL_BinMaster();
            try
            {
                DataTable dt = dlobj.SaveDeleteBin(sN, _BinDescription, _StorageArea, Convert.ToInt32(_Capacity), "1",
                    "UPDATEBINDETAILS", sPartCode, sUserID, sSiteCode);
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
                }
                else
                {
                    sResult = "N~No Result found";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }

        public string BinPrinting(string BinID,
           string BinDesc, string sPrinterIP, string sPartCode, string sUserID
            , string sLineCode
           )
        {
            string sPrintingResult = string.Empty;
            BL_Common objBL_Common = new BL_Common();
            try
            {
                string chkPrinterStatus = string.Empty;
                chkPrinterStatus = objBL_Common.validPrinterConnection(sPrinterIP);
                string sPRN = string.Empty;
                if (chkPrinterStatus == "PRINTER READY")
                {
                    DataTable dt = objBL_Common.GetPRN("BIN");
                    if (dt.Rows.Count > 0)
                    {
                        sPRN = dt.Rows[0][0].ToString();
                        if (sPartCode.Length > 0)
                        {
                            if (sPartCode == "--Select--")
                            {
                                sPartCode = "";
                            }
                        }
                        string sName = string.Empty;
                        sName = "Name";
                        string sPRNPrintingResult = objBL_Common.ALLPrinting(sPRN, BinID, BinDesc, sPartCode, sName, "Bin");
                        if (sPRNPrintingResult.Trim().Length == 0)
                        {
                            sPrintingResult = "N~Printer not found, Location printing failed.";
                        }
                        else
                        {
                            sPrintingResult = objBL_Common.sPrintDataLabel(sPrinterIP, sPRNPrintingResult, BinID, "Bin"
                                , sUserID, sLineCode
                                );
                        }
                    }
                    else
                    {
                        sPrintingResult = "PRNNOTFOUND~Prn for bin printing is not available.";
                    }
                }
                else
                {
                    sPrintingResult = chkPrinterStatus;
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sPrintingResult;
        }

        public string UploadBin(DataTable dtBin,string sUserID)
        {
            DataTable dt = new DataTable();
            dlobj = new DL_BinMaster();
            string sResult = string.Empty;
            try
            {
                dt = dlobj.UploadBinData(dtBin, sUserID);
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
            return sResult;
        }
    }
}
