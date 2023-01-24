using Common;
using DataLayer.WIP;
using System;
using System.Data;


namespace BusinessLayer.WIP
{
    public class BL_WIPDryProcess
    {
        DL_WIP_DRYProcess dlboj;
        public string blDryProcess(string sType, string sModuleType, string sPartBarcode
            , int iExpiryDays, string sSiteCode, string sScannedBy, string sPrinterIP, string sPrinterPort
            , string sLineCode
            )
        {
            DataTable dtData = new DataTable();
            dlboj = new DL_WIP_DRYProcess();
            string sResult = string.Empty;
            string sPRN = string.Empty;
            BL_Common objBL_Common = new BL_Common();
            try
            {
                string chkPrinterStatus = string.Empty;
                chkPrinterStatus = objBL_Common.validPrinterConnection(sPrinterIP);
                if (chkPrinterStatus == "PRINTER READY")
                {
                    DataTable dt = objBL_Common.GetPRN("RM");
                    if (dt.Rows.Count > 0)
                    {
                        dtData = dlboj.dlDryProcess(sType, sModuleType, sPartBarcode
                    , iExpiryDays, sSiteCode, sScannedBy);
                        if (dtData.Rows.Count > 0)
                        {
                            sResult = dtData.Rows[0][0].ToString();
                            if (sResult.StartsWith("SUCCESS~") && sType == "OUT")
                            {
                                try
                                {
                                    PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtMessage, System.Reflection.MethodBase.GetCurrentMethod().Name, "Dry Process Result : " + sResult);
                                    sPRN = dt.Rows[0][0].ToString();
                                    string _PartCode = sResult.Split('~')[3].ToString();
                                    string sPRNPrintingResult = string.Empty;
                                    PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtMessage, System.Reflection.MethodBase.GetCurrentMethod().Name, 
                                        "Dry Process Type  : " + sModuleType
                                        + ", Part Code :" + _PartCode
                                        + ", Part Barcode  :" + sResult.Split('~')[2]
                                        );
                                    if (sModuleType == "RM")
                                    {                                        
                                        sPRNPrintingResult = objBL_Common.PrintMaterialTransfer_ReprintBarcode(sPRN,
                                        _PartCode, sResult.Split('~')[2], "RM");
                                    }
                                    else
                                    {
                                        sPRNPrintingResult = objBL_Common.WIPDryOutPrinting(sPRN,
                                       _PartCode, sResult.Split('~')[2], "WIP");
                                    }
                                    
                                    if (sPRNPrintingResult.Length == 0)
                                    {
                                        PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtMessage, System.Reflection.MethodBase.GetCurrentMethod().Name, "Dry Process Result : PRN Result not found ");
                                        sResult = "PRINTERPRNNOTPRINT~printing failed";
                                    }
                                    else if (sPRNPrintingResult.StartsWith("N~"))
                                    {
                                        sResult = sPRNPrintingResult;
                                    }
                                    else
                                    {
                                        sResult = objBL_Common.sPrintDataLabel(sPrinterIP, sPRNPrintingResult,
                                            sResult.Split('~')[2], "DRYPROCESS"
                                            , sScannedBy, sLineCode
                                            );
                                    }
                                }
                                catch (Exception ex)
                                {
                                    PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,  System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                                    sResult = "N~Qty updated but printing data not found";
                                }
                            }
                        }
                        else
                        {
                            sResult = "N~No result found, Please try again";
                        }
                    }
                    else
                    {
                        sResult = "PRNNOTFOUND~Prn not found";
                    }
                }
                else
                {
                    sResult = chkPrinterStatus;
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                sResult = "ERROR~" + ex.Message;
            }
            return sResult;
        }
    }
}
