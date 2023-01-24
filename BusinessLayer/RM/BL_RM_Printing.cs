using System;
using System.Data;
using System.Text;
using DataLayer;
using Common;

namespace BusinessLayer
{
    public class BL_RM_Printing
    {
        DL_RM_Printing dlboj = null;
        public DataTable BindGRPODate(string sSiteCode)
        {
            DataTable dtGRN = new DataTable();
            dlboj = new DL_RM_Printing();
            try
            {
                dtGRN = dlboj.BindGRPODate(sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtGRN;
        }
        public DataTable BINDGRN(string sGRPODate,string sSiteCode)
        {
            DataTable dtGRN = new DataTable();
            dlboj = new DL_RM_Printing();
            try
            {
                dtGRN = dlboj.BindGRNno(sGRPODate, sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtGRN;
        }
        public DataTable BINDPART_CODE(string sPONO, string sGRPODate,string sSiteCode)
        {
            DataTable dtPartCode = new DataTable();
            dlboj = new DL_RM_Printing();
            try
            {
                dtPartCode = dlboj.BindPartCode(sPONO, sGRPODate, sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtPartCode;
        }
        public DataTable BINDPART_LINENO(string sPONO, string sPartCode, string sGRPODate,string sSiteCode)
        {
            DataTable dtPartCode = new DataTable();
            dlboj = new DL_RM_Printing();
            try
            {
                dtPartCode = dlboj.BindItemLineNo(sPONO, sPartCode, sGRPODate, sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtPartCode;
        }
        public DataTable BINDGrnDetails(string sPONO, string sMFRNo, string sPartCode, string sLineNO
            , string sGRPODate,string sSiteCode)
        {
            DataTable dtPartCode = new DataTable();
            dlboj = new DL_RM_Printing();
            try
            {
                dtPartCode = dlboj.FillDetails(sPONO, sPartCode, sLineNO, sGRPODate, sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtPartCode;
        }

        public DataTable BINDPrintDetails(int iRMID)
        {
            DataTable dtPartCode = new DataTable();
            dlboj = new DL_RM_Printing();
            try
            {
                dtPartCode = dlboj.FillDatatable(iRMID);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtPartCode;
        }

        public string GRNPrinting(string sPONO, string sMFRPartCode, string sPartCode, string SUP_INV_NO,
    DateTime MFG_DATE,
    decimal dPrintedQty, string sPrintedBy, string sPrinterIP, string sPrinterPort, string sBatchNo, int iRM_ID,
    DateTime EXP_DATE, DateTime Invoice_Date, string sSuppliername, decimal dReceivedQty, decimal dRRemainingQty,
    string sSiteCode, string sItemLineNo,
    string sSupplierCode, string sUOM, string sInbondDeliveryNo
            , string sLHRH, string sMSLValue, string sGRPODate, string sUserID, string sLineCode
    )
        {
            string sPrintingResult = string.Empty;
            dlboj = new DL_RM_Printing();
            BL_Common objBL_Common = new BL_Common();
            try
            {
                string sPart_Barcode = string.Empty;
                string sPRN = string.Empty;
                string chkPrinterStatus = string.Empty;
                chkPrinterStatus = objBL_Common.validPrinterConnection(sPrinterIP);
                if (chkPrinterStatus == "PRINTER READY")
                {
                    DataTable dt = objBL_Common.GetPRN("RM");
                    if (dt.Rows.Count > 0)
                    {
                        sPRN = dt.Rows[0][0].ToString();
                        DataTable dtResult = dlboj.GRNPrinting(
                          sPONO, sPartCode, sItemLineNo, sMFRPartCode, SUP_INV_NO, Invoice_Date.ToString("yyyy-MM-dd")
                          , sBatchNo, MFG_DATE.ToString("yyyy-MM-dd"), EXP_DATE.ToString("yyyy-MM-dd"), sSuppliername, sSiteCode, sSupplierCode,
                          sUOM, sInbondDeliveryNo,
                         dPrintedQty, sPrintedBy, iRM_ID, dReceivedQty, dRRemainingQty
                         , sLHRH, sMSLValue, sGRPODate, sLineCode
                            );
                        if (dtResult.Rows.Count > 0)
                        {
                            if (dtResult.Rows[0][0].ToString().StartsWith("SUCCESS~"))
                            {
                                sPart_Barcode = dtResult.Rows[0][0].ToString().Split('~')[2];
                                string sPRNPrintingResult = objBL_Common.GetRMPrnPrintingDetails(sPRN, sPartCode, sPart_Barcode, "RM");
                                if (sPRNPrintingResult.Trim().Length == 0)
                                {
                                    sPrintingResult = "N~Printer not found, RM label printing failed, but data is saved, Reel Barcode : " + sPart_Barcode;
                                }
                                else
                                {
                                    PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtMessage, System.Reflection.MethodBase.GetCurrentMethod().Name,"RM Label Printing Prn Page Called : Barcode  " + sPart_Barcode);
                                    sPrintingResult = objBL_Common.sPrintDataLabel(sPrinterIP
                                        , sPRNPrintingResult, sPart_Barcode, "RM"
                                        , sUserID, sLineCode);
                                }
                            }
                            else
                            {
                                sPrintingResult = dtResult.Rows[0][0].ToString();
                            }
                        }
                        else
                        {
                            sPrintingResult = "N~No result found for printing";
                        }
                    }
                    else
                    {
                        sPrintingResult = "PRNNOTFOUND~~Prn not found";
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
    }
}
