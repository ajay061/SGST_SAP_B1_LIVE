using Common;
using DataLayer;
using DataLayer.WIP;
using System;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;

namespace BusinessLayer.WIP
{
    public class BL_WIP_FGAssembly
    {
        DL_WIP_FGAssembly dlboj = null;
        public DataTable BindWorkOrderNo(string sSiteCode)
        {
            DataTable dtBarcodeInfo = new DataTable();
            dlboj = new DL_WIP_FGAssembly();
            try
            {
                dtBarcodeInfo = dlboj.BindWorkOrderNo(sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtBarcodeInfo;
        }
        public DataTable ValidateMachine(string sMachineID, string sLineID, string sSiteCode)
        {
            DataTable dtLocation = new DataTable();
            dlboj = new DL_WIP_FGAssembly();
            try
            {
                dtLocation = dlboj.ValidateMachine(sMachineID, sLineID, sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLocation;
        }
        public DataTable BindFGItemCode(string sWorkOrderNo, string sSiteCode, string sMachineID, string sLineCode)
        {
            DataTable dtBarcodeInfo = new DataTable();
            dlboj = new DL_WIP_FGAssembly();
            try
            {
                dtBarcodeInfo = dlboj.BindFGItemCode(sWorkOrderNo, sSiteCode, sMachineID, sLineCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtBarcodeInfo;
        }
        public DataTable GetCustomerCode(string sFGItemCode, string sSiteCode)
        {
            DataTable dtBarcodeInfo = new DataTable();
            dlboj = new DL_WIP_FGAssembly();
            try
            {
                dtBarcodeInfo = dlboj.BindCustomerCode(sFGItemCode, sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtBarcodeInfo;
        }
        public DataTable GetProgramDetails(string sFGItemCode, string sMachineID, string sSiteCode)
        {
            DataTable dtBarcodeInfo = new DataTable();
            dlboj = new DL_WIP_FGAssembly();
            try
            {
                dtBarcodeInfo = dlboj.GetProgramDetails(sFGItemCode, sMachineID, sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtBarcodeInfo;
        }
        public DataTable ScanReelBarcode(string sScanBarcode, string sFGItemCode, string sCustomerPartCode
            , string sSiteCode, string sLineCode, string sWorkOrderNo
            )
        {
            DataTable dtBarcodeInfo = new DataTable();
            dlboj = new DL_WIP_FGAssembly();
            try
            {
                dtBarcodeInfo = dlboj.ValidateBarcode(sScanBarcode, sFGItemCode, sCustomerPartCode
                    , sSiteCode, sLineCode, sWorkOrderNo
                    );
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtBarcodeInfo;
        }

        public string FG_Assembly_PCBPrinting(string sPartCode, int iQty,
        int iArraySize, string sBatchNo, string sPONO, string CustomerPartCode
            , DataTable dtScannedData, string sFGItemCode, string sUserID,
        string sLineCode, string sSiteCode, string sMachineID, string sDesignerFormat)
        {
            string sResult = string.Empty;
            string sStartPrefix = string.Empty;
            string sMainSN = string.Empty;
            string sPrintingSNNNo = string.Empty;
            string sBarcodeWithOutPrefix = string.Empty;
            string sAfterSNSuffix = string.Empty;
            string sBeforeSuffix = string.Empty;
            string sLaserSN = string.Empty;
            string sChildSN = string.Empty;
            int iLength = 0;
            bool bSNPassed = false;

            try
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,
                           System.Reflection.MethodBase.GetCurrentMethod().Name, "FG Assembly Module : " +
                           "Work Order No :" + sPONO + ",FG item code : " + sFGItemCode +
                           ", User ID :" + sUserID);
                dlboj = new DL_WIP_FGAssembly();
                DataTable dtResult = new DataTable();
                DataTable dtStoreLaserFile = new DataTable();
                dtStoreLaserFile.Columns.Add("FG_ITEM_CODE", typeof(string));
                dtStoreLaserFile.Columns.Add("PART_CODE", typeof(string));
                dtStoreLaserFile.Columns.Add("PART_BARCODE", typeof(string));
                dtStoreLaserFile.Columns.Add("Laser_Serial_Nos", typeof(string));
                dtStoreLaserFile.Columns.Add("PARENT_SN", typeof(string));
                dtStoreLaserFile.Columns.Add("ALTER_FG_ITEM_CODE", typeof(string));
                dtStoreLaserFile.Columns.Add("CUSTOMER_PART_CODE", typeof(string));

                DL_WIPLaserMachinePrinting dlboj_Dl = new DL_WIPLaserMachinePrinting();
                dtStoreLaserFile.Rows.Clear();
                string sReelBarcode = string.Empty;
                for (int i = 0; i < dtScannedData.Rows.Count; i++)
                {
                    if (sReelBarcode.Length > 0)
                    {
                        sReelBarcode = sReelBarcode + "~" + dtScannedData.Rows[i][2].ToString();
                    }
                    else
                    {
                        sReelBarcode = dtScannedData.Rows[i][2].ToString();
                    }
                }
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,
                          System.Reflection.MethodBase.GetCurrentMethod().Name, "FG Assembly Module : " +
                          "Work Order No :" + sPONO + ",FG item code : " + sFGItemCode +
                          ",Customer Code  : " + CustomerPartCode +
                          ", User ID :" + sUserID + ", Calling SN Generation Event for Assembly Module : Barcode : " + sReelBarcode);
                sMainSN = "";
                try
                {
                    sMainSN = dlboj_Dl.GenerateSN_FGASSEMBLY(sFGItemCode, CustomerPartCode, PCommon.sSiteCode);
                }
                catch (Exception ex)
                {
                    PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError,
                            System.Reflection.MethodBase.GetCurrentMethod().Name, "FG Assembly Module : " +
                            "Work Order No :" + sPONO + ",FG item code : " + sFGItemCode +
                            ",Customer Code  : " + CustomerPartCode +
                            ", User ID :" + sUserID + ", Error Coming from SN Generation Event for Assembly Module : Error Is  : " + ex.Message);
                    sResult = ex.Message;
                    return sResult;
                }

                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,
                        System.Reflection.MethodBase.GetCurrentMethod().Name, "FG Assembly Module : " +
                        "Work Order No :" + sPONO + ",Scanned Barcode :" + sReelBarcode
                        + ",FG item code : " + sFGItemCode + ",SN generated:" + sMainSN
                        + ",Design Format :" + sDesignerFormat + ", User ID :" + sUserID
                        );
                if (sMainSN.Contains("$"))
                {
                    int iCount = sMainSN.Split('$').Length;
                    sStartPrefix = sMainSN.Split('$')[0].ToString();
                    for (int i = 1; i < iCount; i++)
                    {
                        if (sBarcodeWithOutPrefix.Length > 0)
                        {
                            sBarcodeWithOutPrefix = sBarcodeWithOutPrefix + "$" + sMainSN.Split('$')[i];
                            if (sMainSN.Split('$')[i] == " " && sMainSN.Split('$')[i + 1] == "")
                            {
                                i = i + 1;
                            }
                        }
                        else
                        {
                            sBarcodeWithOutPrefix = sMainSN.Split('$')[i];
                            if (sMainSN.Split('$')[i] == " " && sMainSN.Split('$')[i + 1] == "")
                            {
                                i = i + 1;
                            }
                        }
                    }
                    string[] sArr = sBarcodeWithOutPrefix.Split('$');
                    string[] sArr2 = sDesignerFormat.Split('$');

                    for (int i = 0; i < sArr2.Length; i++)
                    {
                        if (sArr2[i] == "8")
                        {
                            sPrintingSNNNo = sArr[i].ToString();
                            bSNPassed = true;
                        }
                        else
                        {
                            if (bSNPassed == false)
                            {
                                if (sBeforeSuffix.Length > 0)
                                {
                                    sBeforeSuffix = sBeforeSuffix + sArr[i].ToString();
                                }
                                else
                                {
                                    sBeforeSuffix = sArr[i].ToString();
                                }
                            }
                            else
                            {
                                if (sAfterSNSuffix.Length > 0)
                                {
                                    sAfterSNSuffix = sAfterSNSuffix + sArr[i].ToString();
                                }
                                else
                                {
                                    sAfterSNSuffix = sArr[i].ToString();
                                }
                            }
                        }
                    }
                    iLength = sPrintingSNNNo.Length;
                }
                if (sPrintingSNNNo.Contains("*"))
                {
                    dtStoreLaserFile.Rows.Clear();
                    sResult = "N~Generated SN(" + Regex.Replace(sMainSN, "$", "") + ") Contains Wrong data, Please check serial Generation Master Logic";
                    return sResult;
                }
                sMainSN = sStartPrefix + sBeforeSuffix + sPrintingSNNNo.PadLeft(iLength, '0') + "" + sAfterSNSuffix;
                sLaserSN = sMainSN.Replace("$", "");

                dtStoreLaserFile.Rows.Add(sFGItemCode, sPartCode, sReelBarcode, sLaserSN,
sLaserSN, sFGItemCode, CustomerPartCode);
                if (dtStoreLaserFile.Rows.Count > 0)
                {
                    PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,
                        System.Reflection.MethodBase.GetCurrentMethod().Name,
                        "FG Assembly Module : Work Order No. :" + sPONO + ",Scanned Barcode :" + sReelBarcode
                        + ",FG item code : " + sFGItemCode + ",Final Store Barcode : " + sLaserSN +
                        ",Prining SN :" + sPrintingSNNNo
                        );
                    dtResult = dlboj.StorePCBData(dtStoreLaserFile,
                       sFGItemCode, 1, iArraySize, sBatchNo, sPONO, Convert.ToInt32(sPrintingSNNNo)
                       , dtScannedData
                       , sReelBarcode, sPartCode, sSiteCode, sLineCode, sUserID, sMachineID, sLaserSN
                       );
                    if (dtResult.Rows.Count > 0)
                    {
                        if (dtResult.Rows[0][0].ToString().StartsWith("ERROR~"))
                        {
                            sResult = dtResult.Rows[0][0].ToString();
                            PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError,
                                System.Reflection.MethodBase.GetCurrentMethod().Name, sResult);
                            return sResult;
                        }
                        else
                        {
                            PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,
                          System.Reflection.MethodBase.GetCurrentMethod().Name, "FG Assembly Module : PRN Called :" +
                          "PO No :" + sPONO + ",Scanned Barcode :" + sReelBarcode
                          + ",FG item code : " + sFGItemCode + ",Final Store Barcode : " + sLaserSN +
                          ",Result :" + dtResult.Rows[0][0].ToString().StartsWith("OKAY~"));
                            if (dtResult.Rows[0][0].ToString().StartsWith("OKAY~"))
                            {
                                sLaserSN = dtResult.Rows[0][0].ToString().Split('~')[2];
                            }
                           
                            DL_WIP_LABEL_PRINTING dlWIPPrinting = new DL_WIP_LABEL_PRINTING();
                            DataTable dt = new DataTable();
                            dt = dlWIPPrinting.GetPrn(PCommon.sSiteCode, CustomerPartCode, sFGItemCode, "PCB");
                            if (dt.Rows.Count > 0)
                            {
                                try
                                {
                                    BL_Common objBL_Common = new BL_Common();
                                    string sPRN = dt.Rows[0][0].ToString();
                                    string sPRNPrintingResult = string.Empty;
                                    sPRNPrintingResult = objBL_Common.WIP_PCB_LABEL(sPRN, sLaserSN);
                                    if (sPRNPrintingResult.Length == 0)
                                    {
                                        sResult = "PRINTERPRNNOTPRINT~ Barcode Printing failed, Please try again";
                                    }
                                    if (sPRNPrintingResult.StartsWith("N~"))
                                    {
                                        sResult = sPRNPrintingResult;
                                    }
                                    else
                                    {
                                        sResult = objBL_Common.sPrintDataLabel(""
                                               , sPRNPrintingResult, sLaserSN, "WIPPCBLABELFAS"
                                              , sUserID, sLineCode
                                               );
                                    }
                                }
                                catch (Exception ex)
                                {
                                    PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, MethodBase.GetCurrentMethod().Name, ex.Message);
                                    sResult = "N~" + ex.Message;
                                }
                            }
                        }
                    }
                }
                sResult = "SUCCESS~File generated successfully with barcode : " + sLaserSN;
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError,
                             System.Reflection.MethodBase.GetCurrentMethod().Name, "FG Assembly Module : " +
                             "Work Order No :" + sPONO + ",FG item code : " + sFGItemCode +
                             ",Customer Code  : " + CustomerPartCode + " , Error :" + ex.Message);
                throw ex;
            }
            return sResult;
        }
    }
}
