using System;
using System.Data;
using DataLayer;
using Common;
using System.Reflection;

namespace BusinessLayer
{
    public class BL_MaterialToMaterialTransfer
    {
        DL_MaterialToMaterialTransfer dlobj = null;
        public DataTable BindMaterialRefNo(string sSiteCode)
        {
            DataTable dtLineData = new DataTable();
            try
            {
                dlobj = new DL_MaterialToMaterialTransfer();
                dtLineData = dlobj.BindMatererialRefNo(sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLineData;
        }
        public DataTable BindPARTCODE(string sMatNo,string sSiteCode)
        {
            DataTable dtLineData = new DataTable();
            try
            {
                dlobj = new DL_MaterialToMaterialTransfer();
                dtLineData = dlobj.BindPartCode(sMatNo, sSiteCode);               
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLineData;
        }
        public DataTable GetMatDetails(string sMatNo,string sNewPartCode,string sSiteCode)
        {
            DataTable dtLineData = new DataTable();
            try
            {
                dlobj = new DL_MaterialToMaterialTransfer();
                dtLineData = dlobj.GETMATTRANSFERDETAILS(sMatNo, sNewPartCode, sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLineData;
        }

        public string SCANPARTBARCODE(string sReelID,string sSiteCode)
        {
            string sResult = string.Empty;
            dlobj = new DL_MaterialToMaterialTransfer();
            DataTable dtValidate = new DataTable();
            try
            {
                dtValidate = dlobj.ValidateBarcode(sReelID, sSiteCode);
                if (dtValidate.Rows.Count > 0)
                {
                    if (dtValidate.Rows[0][0].ToString().StartsWith("ERROR~"))
                    {
                        sResult = dtValidate.Rows[0][0].ToString();
                    }
                    else
                    {

                        sResult = dtValidate.Rows[0][0].ToString();
                    }
                }
                else
                {
                    sResult = "N~No result found, Please try again";
                }

            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, MethodBase.GetCurrentMethod().Name, ex.Message);
                sResult = "ERROR~" + ex.Message;
            }
            return sResult;
        }

        public string SaveMTransfer(string _PartCode, string _ReelID, string sPrintedBy,
            decimal dUpdatedQty, string sPrinterIP,
            string SUP_INV_NO, DateTime MFGDate, DateTime EXPDate, DateTime InvoiceDate,
            string BATCH_NO, string Quntity,string sMatRefno
             , string sUserID, string sLineCode,string sSiteCode
            )
        {
            string sResult = string.Empty;
            DataTable dtValidate = new DataTable();
            string sPRN = string.Empty;
            BL_Common objBL_Common = new BL_Common();
            try
            {
                dlobj = new DL_MaterialToMaterialTransfer();
                dtValidate = new DataTable();
                string chkPrinterStatus = string.Empty;
                chkPrinterStatus = objBL_Common.validPrinterConnection(sPrinterIP);
                if (chkPrinterStatus == "PRINTER READY")
                {
                    DataTable dt = new DataTable();
                    dt = objBL_Common.GetPRN("RM");
                    if (dt.Rows.Count > 0)
                    {
                        dtValidate = dlobj.M_TOMTransferPrint(_PartCode, _ReelID, sPrintedBy,
                dUpdatedQty, sPrinterIP,
                SUP_INV_NO, MFGDate, EXPDate, InvoiceDate, BATCH_NO, Quntity, sMatRefno
                , sSiteCode,sLineCode
                );
                        if (dtValidate.Rows.Count > 0)
                        {
                            if (dtValidate.Rows[0][0].ToString().StartsWith("ERROR~"))
                            {
                                sResult = dtValidate.Rows[0][0].ToString();
                            }
                            else
                            {
                                sResult = dtValidate.Rows[0][0].ToString();
                                if (sResult.StartsWith("SUCCESS~"))
                                {
                                    sPRN = dt.Rows[0][0].ToString();
                                    string sPRNPrintingResult = string.Empty;
                                    string sReelBarcode = dtValidate.Rows[0][0].ToString().Split('~')[2];
                                    sPRNPrintingResult = objBL_Common.PrintMaterialTransfer_ReprintBarcode(sPRN, _PartCode, sReelBarcode, "RM");
                                    if (sPRNPrintingResult.Length == 0)
                                    {
                                        sResult = "N~Printer not found, RM label printing failed, but data is saved, Reel Barcode : " + sReelBarcode;
                                    }
                                    if (sPRNPrintingResult.StartsWith("N~"))
                                    {
                                        sResult = sPRNPrintingResult;
                                    }
                                    else
                                    {
                                        sResult = objBL_Common.sPrintDataLabel(sPrinterIP, sPRNPrintingResult, sReelBarcode, "Material Transfer"
                                            , sUserID, sLineCode
                                            );
                                    }
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
                    sResult = chkPrinterStatus; ;
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


