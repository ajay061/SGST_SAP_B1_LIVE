using Common;
using System;
using System.Data;
using DataLayer.WIP;
using System.Reflection;

namespace BusinessLayer.WIP
{
    public class BL_WIPSecondaryBoxPacking
    {
        DL_WIP_FG_Sec_Packing dlobj;
        public DataTable BindFGItemCode(out string sResult,string sSiteCode,string sLineCode)
        {
            DataTable dtFG = new DataTable();
            dlobj = new DL_WIP_FG_Sec_Packing();
            try
            {
                dtFG = dlobj.BindFGItemCode(sSiteCode, sLineCode);
                if (dtFG.Rows.Count > 0)
                {
                    sResult = "SUCCESS~";
                }
                else
                {
                    sResult = "N~No fg item code found against selected line id";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                sResult = "ERROR~" + ex.Message;
            }
            return dtFG;
        }
        public DataTable BindWorkOrderNo(string sFGItemCode, string sSiteCode)
        {
            DataTable dtFG = new DataTable();
            dlobj = new DL_WIP_FG_Sec_Packing();
            try
            {
                dtFG = dlobj.BindWorkOrderNo(sFGItemCode, sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtFG;
        }
        public DataTable BindPurchaseOrderNo(string sFGItemCode, string sSiteCode)
        {
            DataTable dtFG = new DataTable();
            dlobj = new DL_WIP_FG_Sec_Packing();
            try
            {
                dtFG = dlobj.BindPurchaseOrderNo(sFGItemCode,sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtFG;
        }
        public DataTable bindInvoiceNo(string sFGItemCode, string sSiteCode, string sPurcahseOrderNo)
        {
            DataTable dtFG = new DataTable();
            dlobj = new DL_WIP_FG_Sec_Packing();
            try
            {
                dtFG = dlobj.GetInvoiceNo(sFGItemCode, sSiteCode, sPurcahseOrderNo);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtFG;
        }
        public DataTable GetFGDetails(string sFGItemCode, string sCustomerCode, out string sResult
            ,string sSiteCode,string sLineCode,string sUserID
            )
        {
            DataTable dtFG = new DataTable();
            dlobj = new DL_WIP_FG_Sec_Packing();
            try
            {
                dtFG = dlobj.GetDetails(sFGItemCode, sCustomerCode
                    ,sSiteCode,sLineCode,sUserID                    
                    );
                if (dtFG.Rows.Count > 0)
                {
                    if (dtFG.Columns.Count > 2)
                    {
                        sResult = "SUCCESS~";
                    }
                    else
                    {
                        sResult = dtFG.Rows[0][0].ToString();
                    }
                }
                else
                {
                    sResult = "N~No data found against selected FG Item Code";
                }
            }
            catch (Exception ex)
            {
                sResult = "ERROR~" + ex.Message;
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            return dtFG;
        }
        public DataTable BindCustomerCode(string sFGItemCode, out string sResult
            ,string sSiteCode,string sLineCode
            )
        {
            DataTable dtFG = new DataTable();
            dlobj = new DL_WIP_FG_Sec_Packing();
            try
            {
                dtFG = dlobj.BindCustomerCode(sFGItemCode
                    ,sSiteCode, sLineCode
                    );
                if (dtFG.Rows.Count > 0)
                {
                    sResult = "SUCCESS~";
                }
                else
                {
                    sResult = "NOTFOUND~No customer found against selected fg item code";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                sResult = "ERROR~" + ex.Message;
            }
            return dtFG;
        }
        public DataTable GetCustomerDetails(string sFGItemCode, string sCustomerCode, out string sResult
            , string sSiteCode, string sLineCode)
        {
            DataTable dtFG = new DataTable();
            dlobj = new DL_WIP_FG_Sec_Packing();
            try
            {
                dtFG = dlobj.GetCustomerDetails(sFGItemCode, sCustomerCode, sSiteCode, sLineCode);
                if (dtFG.Rows.Count > 0)
                {
                    sResult = "SUCCESS~";
                }
                else
                {
                    sResult = "NOTFOUND~No customer details found against selected data";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                sResult = "ERROR~" + ex.Message;
            }
            return dtFG;
        }
        public string sScanBarcode(string sFGItemCode, string sBoxID, string sWorkOrderNo
             , string sSiteCode, string sLineCode,string sUserID,string sPONO

            )
        {
            dlobj = new DL_WIP_FG_Sec_Packing();
            string sResult = string.Empty;
            try
            {
                DataTable dt = dlobj.ScanBarcode(sFGItemCode, sBoxID, sWorkOrderNo,sSiteCode,sLineCode,sUserID, sPONO
                    );
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
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

        public string PrintBoxID(string sFGItemCode,
            string sCustomerName, string sCustomerPartNumber, string sCustomerLocation,
            string sFgLocType, string sFGLocationCode, string sPrinterIP, string sCustomeCode
            , string sInvoiceNo, decimal dWeight, string sWorkOrderNo
            , string sUserID, string sLineCode,string sSiteCode, DateTime dInvoiceDate, decimal dNetWeight
            ,string sPONO
            )
        {
            dlobj = new DL_WIP_FG_Sec_Packing();
            string sResult = string.Empty;
            string sPRN = string.Empty;
            BL_Common objBL_Common = new BL_Common();
            try
            {
                string chkPrinterStatus = string.Empty;
                chkPrinterStatus = objBL_Common.validPrinterConnection(sPrinterIP);
                if (chkPrinterStatus == "PRINTER READY")
                {
                    DataTable dt = dlobj.GetPrn(sFGItemCode, sCustomerPartNumber, sSiteCode);
                    if (dt.Rows.Count > 0)
                    {
                        string sBoxID = string.Empty;

                        DataTable dtData = dlobj.PrintBarcode(sFGItemCode, sCustomerName, sCustomerPartNumber,
                            sCustomerLocation, sFgLocType, sFGLocationCode, sCustomeCode
                            , sInvoiceNo, dWeight, sWorkOrderNo
                            , sSiteCode,sLineCode,sUserID, dInvoiceDate,dNetWeight,sPONO
                            );
                        if (dtData.Rows.Count > 0)
                        {
                            if (dtData.Rows[0][0].ToString().StartsWith("SUCCESS~"))
                            {
                                try
                                {
                                    sBoxID = dtData.Rows[0][0].ToString().Split('~')[2];
                                    sPRN = dt.Rows[0][0].ToString();
                                    int iNoOfLabels = 1;
                                    iNoOfLabels = Convert.ToInt32(dt.Rows[0][1].ToString());
                                    string sSNModel = dtData.Rows[0][0].ToString().Split('~')[3];
                                    if (sSNModel != "")
                                    {
                                        PL.PL_Printing plobj = new PL.PL_Printing();
                                        plobj.sModelName = sSNModel;
                                        plobj.sPalletID = sBoxID;
                                        plobj.sUserID = sUserID;
                                        plobj.sLineCode = sLineCode;
                                        plobj.sPrinterIP = sPrinterIP;
                                        BL_MobCommon obj = new BL_MobCommon();
                                        sResult = obj.sPalletPrint(plobj, sPRN, sBoxID, iNoOfLabels);
                                    }
                                    else
                                    {
                                        string sPRNPrintingResult = objBL_Common.SecondaryBoxPacking(
                                            sPRN, sFGItemCode, sBoxID.ToString());
                                        if (sPRNPrintingResult.Length == 0)
                                        {
                                            sResult = "PRINTERPRNNOTPRINT~PRN not print, Qty updated but printing failed, Box ID :" + sBoxID;
                                        }
                                        else if (sPRNPrintingResult.StartsWith("N~"))
                                        {
                                            sResult = sPRNPrintingResult + ", Box ID :" + sBoxID;
                                        }
                                        else
                                        {
                                            for (int i = 0; i < iNoOfLabels; i++)
                                            {
                                                sResult = objBL_Common.sPrintDataLabel(sPrinterIP, sPRNPrintingResult,
                                                    sBoxID + "_" + i.ToString(), "Pallet"
                                                    , sUserID, sLineCode
                                                    );
                                            }

                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                                    sResult = "N~Qty updated but printing data not found, Box ID :" + sBoxID;
                                }
                            }
                            else
                            {
                                sResult = dtData.Rows[0][0].ToString();
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
                    sResult = "PRINTERNOTCONNECTED~Printer Not connected";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, Assembly.GetExecutingAssembly().GetName() + "::" + MethodBase.GetCurrentMethod().Name, ex.Message);
                sResult = "ERROR~" + ex.Message;
            }
            return sResult;
        }
    }
}
