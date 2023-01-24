using System;
using System.Data;
using DataLayer;
using Common;
using System.Reflection;

namespace BusinessLayer
{
    public class BL_ChildLabelPrinting
    {
        DL_ChildLabelPrinting dlobj = null;
        public DataTable BindINELPartNo(string sType,string sSiteCode)
        {
            DataTable dtINELPartNo = new DataTable();
            dlobj = new DL_ChildLabelPrinting();
            try
            {
                dtINELPartNo = dlobj.BINDINEL_PARTNO(sType, sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtINELPartNo;
        }
        public DataTable BindReelBarcode(string sItemCode, string sType,string sSiteCode)
        {
            DataTable dtReelBarcode = new DataTable();
            dlobj = new DL_ChildLabelPrinting();
            try
            {
                dtReelBarcode = dlobj.BindReelBarcode(sType, sItemCode, sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtReelBarcode;
        }
        public DataTable SCANBARCODE(string sINELPartNo, string sType, string sReelID,string sSiteCode)
        {
            DataTable dtReelBarcode = new DataTable();
            dlobj = new DL_ChildLabelPrinting();
            try
            {
                dtReelBarcode = dlobj.ValidateReelBarcode(sType, sINELPartNo, sReelID, sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtReelBarcode;
        }

        public string ChildLabelPrint(string _PartCode, string _ReelID, string sPrintedBy,
            decimal dUpdatedQty, string sPrinterIP, string sPrinterPort, string sUserID, string sLineCode
            ,string sSiteCode
            )
        {
            dlobj = new DL_ChildLabelPrinting();
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
                        DataTable dtGetdetails = dlobj.ChildLabelPrinting(_PartCode, _ReelID
             , dUpdatedQty, sPrintedBy, sSiteCode,sLineCode);
                        if (dtGetdetails.Rows.Count > 0)
                        {
                            sResult = dtGetdetails.Rows[0][0].ToString();
                            if (sResult.StartsWith("SUCCESS~"))
                            {
                                try
                                {
                                    sPRN = dt.Rows[0][0].ToString();
                                    PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtMessage, System.Reflection.MethodBase.GetCurrentMethod().Name, "Kitting Label Printing Prn Page Called : Barcode  " + sResult.Split('~')[2]);
                                    string sPRNPrintingResult = objBL_Common.GetRMPrnPrintingDetails(sPRN, _PartCode, sResult.Split('~')[2], "CHILDLABEL");
                                    if (sPRNPrintingResult.Length == 0)
                                    {
                                        sResult = "PRINTERPRNNOTPRINT~ Qty updated but printing failed";
                                    }
                                    else if (sPRNPrintingResult.StartsWith("N~"))
                                    {
                                        sResult = sPRNPrintingResult;
                                    }
                                    else
                                    {
                                        sResult = objBL_Common.sPrintDataLabel(sPrinterIP, sPRNPrintingResult, _ReelID, "Kitting"
                                            , sUserID, sLineCode
                                            );
                                    }
                                }
                                catch (Exception ex)
                                {
                                    PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
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
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, Assembly.GetExecutingAssembly().GetName() + "::" + MethodBase.GetCurrentMethod().Name, ex.Message);
                sResult = "ERROR~" + ex.Message;
            }
            return sResult;
        }

        public string Labelprint(string _PartCode, string _ReelID, string sPrintedBy, decimal dUpdatedQty, string sPrinterIP,
            string sPrinterPort, string _sPrintingType, string sUserID, string sLineCode)
        {
            dlobj = new DL_ChildLabelPrinting();
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
                        try
                        {
                            sPRN = dt.Rows[0][0].ToString();
                            string sPRNPrintingResult = objBL_Common.PrintMaterialTransfer_ReprintBarcode(sPRN, _PartCode, _ReelID, _sPrintingType);
                            if (sPRNPrintingResult.Length == 0)
                            {
                                sResult = "PRINTERPRNNOTPRINT~ Qty updated but printing failed";
                            }
                            if (sPRNPrintingResult.StartsWith("N~"))
                            {
                                sResult = sPRNPrintingResult;
                            }
                            else
                            {
                                sResult = objBL_Common.sPrintDataLabel(sPrinterIP, sPRNPrintingResult, _ReelID, ""
                                    , sUserID, sLineCode
                                    );
                            }
                        }
                        catch (Exception ex)
                        {
                            PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, Assembly.GetExecutingAssembly().GetName() + "::" + MethodBase.GetCurrentMethod().Name, ex.Message);
                            sResult = ex.Message;
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
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,  MethodBase.GetCurrentMethod().Name, ex.Message);
                sResult = "ERROR~" + ex.Message;
            }
            return sResult;
        }
    }
}
