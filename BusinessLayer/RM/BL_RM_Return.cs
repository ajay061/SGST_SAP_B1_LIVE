using System;
using DataLayer;
using System.Data;
using Common;

namespace BusinessLayer
{
    public class BL_RM_Return
    {
        DL_RMReturn dlobj = new DL_RMReturn();
        public string ValidateBarcodeFromWIPIssue(string _ReelID,string sWorkOrderNo,string sScanType
            ,string sSiteCode
            )
        {
            string sResult = string.Empty;
            try
            {
                dlobj = new DL_RMReturn();
                DataTable dt = dlobj.ValidateBarcodeFromWIPIssue(_ReelID,sWorkOrderNo, sScanType
                    , sSiteCode
                    );
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
                }
                else
                {
                    sResult = "N~No result found scanned barcode";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }
        public string PrintBarcode(string _MRNNo, string _Part_Barcode, string sScanBy, 
            decimal dReturnQty, string sPrinterIP,
            string sSiteCode, string _PartCode,string sWorkOrderNo,string sScanType, string sUserID, string sLineCode
            ,string sRemarks
)
        {
            string sResult = string.Empty;
            dlobj = new DL_RMReturn();
            BL_Common objBL_Common = new BL_Common();
            try
            {
                string sPRN = string.Empty;
                string chkPrinterStatus = string.Empty;
                chkPrinterStatus = objBL_Common.validPrinterConnection(sPrinterIP);
                if (chkPrinterStatus == "PRINTER READY")
                {
                    DataTable dtGetPRN = objBL_Common.GetPRN("RM");
                    if (dtGetPRN.Rows.Count > 0)
                    {
                        DataTable dtData = dlobj.PrintBarcodeLabel(
                            _MRNNo, _Part_Barcode, sScanBy, dReturnQty, sSiteCode, _PartCode
                            , sWorkOrderNo, sScanType,sLineCode, sRemarks);
                        if (dtData.Rows.Count > 0)
                        {
                            sResult = dtData.Rows[0][0].ToString();
                            if (sResult.StartsWith("SUCCESS~"))
                            {
                                if (sScanType == "RM")
                                {
                                    string sPRNPrintingResult = string.Empty;
                                    try
                                    {
                                        sPRN = dtGetPRN.Rows[0][0].ToString();
                                        sPRNPrintingResult = objBL_Common.GetReturnPrintingDetails(sPRN, _MRNNo, _PartCode, sResult.Split('~')[2],
                                            dReturnQty, _Part_Barcode);
                                        if (sPRNPrintingResult.Length == 0)
                                        {
                                            sResult = "N~Qty updated but PRN Printing data not found.";
                                        }
                                        else
                                        {
                                            sResult = objBL_Common.sPrintDataLabel(sPrinterIP, sPRNPrintingResult,
                                                sResult.Split('~')[2], "Rm Return"
                                                , sUserID, sLineCode

                                                );
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                                        sResult = "N~quantity updated but  " + ex.Message;
                                    }
                                }
                            }
                        }
                        else
                        {
                            sResult = "N~No result found table, Please try again.";
                        }
                    }
                    else
                    {
                        sResult = "N~Prn not found.";
                    }
                }
                else
                {
                    sResult = chkPrinterStatus; ;
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }
    }
}
