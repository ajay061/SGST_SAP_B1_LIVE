using Common;
using DataLayer;
using System;
using System.Data;
using System.Reflection;
using DataLayer.WIP;
using System.Text.RegularExpressions;

namespace BusinessLayer.WIP
{
    public class BL_WIP_LaserMachine
    {
        DL_WIPLaserMachinePrinting dlboj = null;

        public string ValidateMachineLabel(string sMachineCode, string sSiteCode)
        {
            DataTable dtBarcodeInfo = new DataTable();
            string sResult = string.Empty;
            dlboj = new DL_WIPLaserMachinePrinting();
            try
            {
                dtBarcodeInfo = dlboj.ValidateMachine(sMachineCode, sSiteCode);
                if (dtBarcodeInfo.Rows.Count > 0)
                {
                    sResult = "SUCCESS~Machine OK";
                }
                else
                {
                    sResult = "N~Scanned machine barcode not a valid barcode of PCB marking machine ";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }
        public DataTable BindProcess(string sSiteCode)
        {
            DataTable dtBarcodeInfo = new DataTable();
            dlboj = new DL_WIPLaserMachinePrinting();
            try
            {
                dtBarcodeInfo = dlboj.BindType(sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtBarcodeInfo;
        }
        public DataTable GetProcessDetails(string sSiteCode, string ProcessType)
        {
            DataTable dtBarcodeInfo = new DataTable();
            dlboj = new DL_WIPLaserMachinePrinting();
            try
            {
                dtBarcodeInfo = dlboj.GetProcssDetails(sSiteCode, ProcessType);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtBarcodeInfo;
        }

        public DataTable BindIssueslipno(string sSiteCode)
        {
            DataTable dtBarcodeInfo = new DataTable();
            dlboj = new DL_WIPLaserMachinePrinting();
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
        public DataTable BindFGItemCode(string sType, string sWorkOrderNo, string sSiteCode)
        {
            DataTable dtBarcodeInfo = new DataTable();
            dlboj = new DL_WIPLaserMachinePrinting();
            try
            {
                dtBarcodeInfo = dlboj.BindFGItemCode(sType, sWorkOrderNo, sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtBarcodeInfo;
        }
        public DataSet BindCustomerPartCode(string sFGItemCode, string sSiteCode)
        {
            DataSet dtBarcodeInfo = new DataSet();
            dlboj = new DL_WIPLaserMachinePrinting();
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
        public DataTable GetPurchaseOrderDetails(string sFGItemCode, string sSiteCode)
        {
            DataTable dtBarcodeInfo = new DataTable();
            dlboj = new DL_WIPLaserMachinePrinting();
            try
            {
                dtBarcodeInfo = dlboj.GetPurchaseOrderDetails(sSiteCode, sFGItemCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtBarcodeInfo;
        }
        public DataTable ScanReelBarcode(string sScanBarcode, string sFGItemCode, string sMachineCode
            , string sCustomerCode, string sWorkOrderNo, string sSiteCode
            )
        {
            DataTable dtBarcodeInfo = new DataTable();
            dlboj = new DL_WIPLaserMachinePrinting();
            try
            {
                dtBarcodeInfo = dlboj.ValidateBarcode(sScanBarcode, sFGItemCode, sMachineCode,
                    sCustomerCode, sWorkOrderNo, sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtBarcodeInfo;
        }
        public DataTable GetTopBarcode(string sPartCode, string sSiteCode)
        {
            DataTable dtBarcodeInfo = new DataTable();
            dlboj = new DL_WIPLaserMachinePrinting();
            try
            {
                dtBarcodeInfo = dlboj.GettopBarcode(sPartCode, sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtBarcodeInfo;
        }

        #region Type Base Logic Change       
        public string SinglePrintLasserSerailNosXLS(
             string sGRPONO, string sWorkOrderNo,
            string PartCode, string sBatchNo, int iQty,
          int iArraySize, string sReelBarcode, string CustomerPartCode, string sFGItemCode
            , string sSiteCode, string sUseriD, string sLineCode, string sDesignerFormat, string sPacketType
            , int iNOSCount, string sPrefix
            )
        {
            string sResult = string.Empty;
            try
            {
                dlboj = new DL_WIPLaserMachinePrinting();
                DataTable dtResult = new DataTable();
                DataTable dtStoreLaserFile = new DataTable();
                dtStoreLaserFile.Columns.Add("SITECODE", typeof(string));
                dtStoreLaserFile.Columns.Add("WORK_ORDER_NO", typeof(string));
                dtStoreLaserFile.Columns.Add("FG_ITEM_CODE", typeof(string));
                dtStoreLaserFile.Columns.Add("CUSTOMER_PART_CODE", typeof(string));
                dtStoreLaserFile.Columns.Add("PART_CODE", typeof(string));
                dtStoreLaserFile.Columns.Add("PART_BARCODE", typeof(string));
                dtStoreLaserFile.Columns.Add("Laser_Serial_Nos", typeof(string));
                dtStoreLaserFile.Columns.Add("PARENT_SN", typeof(string));
                int iStoreRecordCount = 0;
                string sStartPrefix = string.Empty;
                string sMainSN = string.Empty;
                string sPrintingSNNNo = string.Empty;
                string sBarcodeWithOutPrefix = string.Empty;
                string sAfterSNSuffix = string.Empty;
                string sBeforeSuffix = string.Empty;
                string sLaserSN = string.Empty;
                string sChildSN = string.Empty;
                int iActualArraySize = iArraySize;
                int iActualQty = iQty;
                iQty = iNOSCount * iQty;
                iArraySize = iNOSCount * iArraySize;
                int iLength = 0;
                int iPacketSize = iQty;
                bool bSNPassed = false;
                dtStoreLaserFile.Rows.Clear();
                if (iQty % iArraySize != 0)
                {
                    sResult = "N~Packet size is not divisible by array size,Please check the array size(" + iArraySize.ToString() + ") or packet size(" + iPacketSize.ToString() + ") for scanned barcode.";
                    return sResult;
                }
                iQty = iQty / iArraySize;

                sMainSN = "";
                sMainSN = dlboj.GenerateSN(sFGItemCode, CustomerPartCode, PCommon.sSiteCode);
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,
                     System.Reflection.MethodBase.GetCurrentMethod().Name, "Packet Barcode : " + sReelBarcode +
                     ",Array Size:" + iArraySize.ToString() + ",SN Generation : " + sMainSN.ToString()
                     + ", Work Order No :" + sWorkOrderNo + ", Part Code : " + PartCode +
                     ", Qty : " + iQty.ToString()
                     + ",Line Code : " + sLineCode + ",Prefix :" + sPrefix + ", Design Format : " + sDesignerFormat
                     );
                if (sMainSN.Contains("$"))
                {
                    int iCount = sMainSN.Split('$').Length;
                    if (sPrefix.Length > 0)
                    {
                        sStartPrefix = sPrefix;
                    }
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
                    PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, "Packet Barcode : " + sReelBarcode +
                    ",Array Size:" + iArraySize.ToString() + ",SN Generation : " + sMainSN.ToString()
                    + ", Work Order No :" + sWorkOrderNo + ", Part Code : " + PartCode +
                    ", Qty : " + iQty.ToString()
                    + ",Line Code : " + sLineCode + ", sBarcodePrefix:" + sBarcodeWithOutPrefix + ", Design Format :" + sDesignerFormat
                    );
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
                                if (sPrefix.Length > 0)
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
                                    if (sBeforeSuffix.Length > 0)
                                    {
                                        sBeforeSuffix = sBeforeSuffix + sArr[i].ToString();
                                    }
                                    else
                                    {
                                        sBeforeSuffix = sArr[i].ToString();
                                    }
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
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,
                  System.Reflection.MethodBase.GetCurrentMethod().Name, "Packet Barcode : " + sReelBarcode +
                  ",Array Size:" + iArraySize.ToString() + ",SN Generation : " + sMainSN.ToString()
                  + ", Work Order No :" + sWorkOrderNo + ", Part Code : " + PartCode +
                  ", Qty : " + iQty.ToString()
                  + ",Line Code : " + sLineCode + ", Final SN:" + sMainSN
                  );
                if (sPrintingSNNNo.Contains("*"))
                {
                    dtStoreLaserFile.Rows.Clear();
                    sResult = "N~Generated SN(" + Regex.Replace(sMainSN, "$", "") + ") Contains Wrong data, Please check serial Generation Master Logic";
                    iStoreRecordCount = 0;
                    return sResult;
                }
                for (int i = 1; i <= iQty; i++)
                {
                    if (sMainSN.Length > 0)
                    {
                        sMainSN = sStartPrefix + sBeforeSuffix + sPrintingSNNNo.PadLeft(iLength, '0') + "" + sAfterSNSuffix;
                        sLaserSN = sMainSN;
                        dtStoreLaserFile.Rows.Add(sSiteCode, sWorkOrderNo, sFGItemCode, CustomerPartCode,
                              PartCode, sReelBarcode, sLaserSN, sLaserSN);
                        for (int iLaserSN = 1; iLaserSN < iArraySize; iLaserSN++)
                        {
                            sPrintingSNNNo = Convert.ToString(Convert.ToInt32(sPrintingSNNNo) + 1);
                            sChildSN = sStartPrefix + sBeforeSuffix + sPrintingSNNNo.PadLeft(iLength, '0') + "" + sAfterSNSuffix;
                            dtStoreLaserFile.Rows.Add(sSiteCode, sWorkOrderNo, sFGItemCode, CustomerPartCode,
                                PartCode, sReelBarcode, sChildSN, sLaserSN);
                        }
                        sPrintingSNNNo = Convert.ToString(Convert.ToInt32(sPrintingSNNNo) + 1);
                    }
                    else
                    {
                        dtStoreLaserFile.Rows.Clear();
                        sResult = "N~SN generation fail, Please try again";
                        iStoreRecordCount = 0;
                        return sResult;
                    }
                    iStoreRecordCount++;
                }
                if (dtStoreLaserFile.Rows.Count > 0)
                {
                    sPrintingSNNNo = Convert.ToString(Convert.ToInt32(sPrintingSNNNo) - 1);
                    dtResult = dlboj.StoreData(sGRPONO, sWorkOrderNo, PartCode, sReelBarcode, dtStoreLaserFile,
                    iActualQty, iActualArraySize, sBatchNo, Convert.ToInt32(sPrintingSNNNo)
                    , sSiteCode, sUseriD, sLineCode, sPacketType
                    );
                    if (dtResult.Rows.Count > 0)
                    {
                        PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.MethodBase.GetCurrentMethod().Name, "PCB Generate Result : " + dtResult.Rows[0][0].ToString());
                        if (dtResult.Rows[0][0].ToString().StartsWith("ERROR~"))
                        {
                            sResult = dtResult.Rows[0][0].ToString();
                            PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, sResult);
                            return sResult;
                        }
                        else
                        {
                            iStoreRecordCount = 1;
                        }
                    }
                }
                if (iStoreRecordCount > 0)
                {
                    sResult = "SUCCESS~File generated successfully.";
                }
                else
                {
                    PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtMessage, System.Reflection.MethodBase.GetCurrentMethod().Name, "PCB Generate Final Result : N~File generation fail, Please try again ");
                    sResult = "N~File generation fail, Please try again";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }


        public string sLaserFileGenerate
            (
                int iQty,
                int iArraySize, string sReelBarcode, string CustomerPartCode, string sFGItemCode
                , string sSiteCode, string sUseriD, string sLineCode, string sDesignerFormat, string sPacketType
                , int iNOSCount, DataTable dtPCBLogicPrinting, string sPrefix
            )
        {
            string sResult = string.Empty;
            try
            {
                dlboj = new DL_WIPLaserMachinePrinting();
                DataTable dtResult = new DataTable();
                DataTable dtStoreLaserFile = new DataTable();
                dtStoreLaserFile.Columns.Add("SITECODE", typeof(string));
                dtStoreLaserFile.Columns.Add("WORK_ORDER_NO", typeof(string));
                dtStoreLaserFile.Columns.Add("FG_ITEM_CODE", typeof(string));
                dtStoreLaserFile.Columns.Add("CUSTOMER_PART_CODE", typeof(string));
                dtStoreLaserFile.Columns.Add("PART_CODE", typeof(string));
                dtStoreLaserFile.Columns.Add("PART_BARCODE", typeof(string));
                dtStoreLaserFile.Columns.Add("Laser_Serial_Nos", typeof(string));
                dtStoreLaserFile.Columns.Add("PARENT_SN", typeof(string));
                dtPCBLogicPrinting.Columns.Add("STARTPREFIX");
                dtPCBLogicPrinting.Columns.Add("BEFOREVALUE");
                dtPCBLogicPrinting.Columns.Add("PRINTINGVALUE");
                dtPCBLogicPrinting.Columns.Add("AFTERSUFFIX");
                dtPCBLogicPrinting.Columns.Add("LENGTH");

                int iStoreRecordCount = 0;
                string sStartPrefix = string.Empty;
                string sMainSN = string.Empty;
                string sPrintingSNNNo = string.Empty;
                string sBarcodeWithOutPrefix = string.Empty;
                string sAfterSNSuffix = string.Empty;
                string sBeforeSuffix = string.Empty;
                string sParentSN = string.Empty;
                string sChildSN = string.Empty;
                string sWorkOrderNo = string.Empty;
                string PartCode = string.Empty;
                string sGRPONO = string.Empty;
                string sBatchNo = string.Empty;
                string sPrefix1 = string.Empty;

                int iActualArraySize = iArraySize;
                int iActualQty = iQty;
                iQty = iNOSCount * iQty;
                iArraySize = iNOSCount * iArraySize;
                int iLength = 0;
                int iPacketSize = iQty;
                bool bSNPassed = false;
                dtStoreLaserFile.Rows.Clear();
                if (iQty % iArraySize != 0)
                {
                    sResult = "N~Packet size is not divisible by array size,Please check the array size(" + iArraySize.ToString() + ") or packet size(" + iPacketSize.ToString() + ") for scanned barcode.";
                    return sResult;
                }
                iQty = iQty / iArraySize;

                // Getting Parent SN from Database
                for (int iPCBLogic = 0; iPCBLogic < dtPCBLogicPrinting.Rows.Count; iPCBLogic++)
                {
                    sWorkOrderNo = dtPCBLogicPrinting.Rows[iPCBLogic]["ISSUE_SLIP_NO"].ToString();
                    PartCode = dtPCBLogicPrinting.Rows[iPCBLogic]["PART_CODE"].ToString();
                    sGRPONO = dtPCBLogicPrinting.Rows[iPCBLogic]["PONO"].ToString();
                    sBatchNo = dtPCBLogicPrinting.Rows[iPCBLogic]["BATCHNO"].ToString();
                    sReelBarcode = dtPCBLogicPrinting.Rows[iPCBLogic]["PARTBARCODE"].ToString();
                    sDesignerFormat = dtPCBLogicPrinting.Rows[iPCBLogic]["DESIGNFORMAT"].ToString();
                    CustomerPartCode = dtPCBLogicPrinting.Rows[iPCBLogic]["CUSTOMERCODE"].ToString();
                    sFGItemCode = dtPCBLogicPrinting.Rows[iPCBLogic]["FGITEMCODE"].ToString();
                    sPrefix1 = dtPCBLogicPrinting.Rows[iPCBLogic]["PREFIX"].ToString();
                    sMainSN = "";
                    sMainSN = dlboj.GenerateSN(sFGItemCode, CustomerPartCode, PCommon.sSiteCode);

                    PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,
                     System.Reflection.MethodBase.GetCurrentMethod().Name, "Packet Barcode : " + sReelBarcode +
                     ",Array Size:" + iArraySize.ToString() + ",SN Generation : " + sMainSN.ToString()
                     + ", Work Order No :" + sWorkOrderNo + ", Part Code : " + PartCode +
                     ", Qty : " + iQty.ToString() + ", FG ITEM CODE : " + sFGItemCode.ToString()
                     + ",Line Code : " + sLineCode + ", sPrefix :" + sPrefix1 + ",Designed Format:" + sDesignerFormat
                     );
                    #region Getting Prefix,Suffix and Running SN
                    if (sMainSN.Contains("$"))
                    {
                        int iCount = sMainSN.Split('$').Length;
                        if (sPrefix1.Length > 0)
                        {
                            sStartPrefix = sMainSN.Split('$')[0].ToString();
                        }
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
                        iStoreRecordCount = 0;
                        return sResult;
                    }
                    dtPCBLogicPrinting.Rows[iPCBLogic].SetField("STARTPREFIX", sStartPrefix);
                    dtPCBLogicPrinting.Rows[iPCBLogic].SetField("BEFOREVALUE", sBeforeSuffix);
                    dtPCBLogicPrinting.Rows[iPCBLogic].SetField("PRINTINGVALUE", sPrintingSNNNo);
                    dtPCBLogicPrinting.Rows[iPCBLogic].SetField("LENGTH", iLength);
                    dtPCBLogicPrinting.Rows[iPCBLogic].SetField("AFTERSUFFIX", sAfterSNSuffix);
                    dtPCBLogicPrinting.AcceptChanges();

                    #endregion
                }
                //IQty contains the value for how many masterid will generate
                // iArraySize contains the value for in one master id how many pcb will come
                for (int i = 1; i <= iQty; i++)
                {
                    for (int iSNCount = 0; iSNCount < dtPCBLogicPrinting.Rows.Count; iSNCount++)
                    {
                        sWorkOrderNo = dtPCBLogicPrinting.Rows[iSNCount]["ISSUE_SLIP_NO"].ToString();
                        PartCode = dtPCBLogicPrinting.Rows[iSNCount]["PART_CODE"].ToString();
                        sGRPONO = dtPCBLogicPrinting.Rows[iSNCount]["PONO"].ToString();
                        sBatchNo = dtPCBLogicPrinting.Rows[iSNCount]["BATCHNO"].ToString();
                        sReelBarcode = dtPCBLogicPrinting.Rows[iSNCount]["PARTBARCODE"].ToString();
                        sDesignerFormat = dtPCBLogicPrinting.Rows[iSNCount]["DESIGNFORMAT"].ToString();
                        CustomerPartCode = dtPCBLogicPrinting.Rows[iSNCount]["CUSTOMERCODE"].ToString();
                        sFGItemCode = dtPCBLogicPrinting.Rows[iSNCount]["FGITEMCODE"].ToString();

                        // new field getting the value on the basis of scan barcode
                        sStartPrefix = dtPCBLogicPrinting.Rows[iSNCount]["STARTPREFIX"].ToString();
                        sBeforeSuffix = dtPCBLogicPrinting.Rows[iSNCount]["BEFOREVALUE"].ToString();                                     
                        iLength = Convert.ToInt32(dtPCBLogicPrinting.Rows[iSNCount]["LENGTH"].ToString());
                        sAfterSNSuffix = dtPCBLogicPrinting.Rows[iSNCount]["AFTERSUFFIX"].ToString();
                        if (sPrintingSNNNo.Contains("*"))
                        {
                            dtStoreLaserFile.Rows.Clear();
                            sResult = "N~Generated SN(" + Regex.Replace(sMainSN, "$", "") + ") Contains Wrong data, Please check serial Generation Master Logic";
                            iStoreRecordCount = 0;
                            return sResult;
                        }
                        if (sMainSN.Length > 0)
                        {
                            sMainSN = sStartPrefix + sBeforeSuffix + sPrintingSNNNo.PadLeft(iLength, '0') + "" + sAfterSNSuffix;
                            if (sParentSN.Length == 0)
                            {
                                sParentSN = sMainSN;
                            }
                            dtStoreLaserFile.Rows.Add(sSiteCode, sWorkOrderNo, sFGItemCode, CustomerPartCode,
                                  PartCode, sReelBarcode, sMainSN, sParentSN);
                            for (int iLaserSN = 1; iLaserSN < iArraySize; iLaserSN++)
                            {
                                sPrintingSNNNo = Convert.ToString(Convert.ToInt32(sPrintingSNNNo) + 1);
                                sChildSN = sStartPrefix + sBeforeSuffix + sPrintingSNNNo.PadLeft(iLength, '0') + "" + sAfterSNSuffix;
                                dtStoreLaserFile.Rows.Add(sSiteCode, sWorkOrderNo, sFGItemCode, CustomerPartCode,
                                    PartCode, sReelBarcode, sChildSN, sParentSN);
                            }
                        }
                        else
                        {
                            dtStoreLaserFile.Rows.Clear();
                            sResult = "N~SN generation fail, Please try again";
                            iStoreRecordCount = 0;
                            return sResult;
                        }
                    }
                    sParentSN = "";
                    sPrintingSNNNo = Convert.ToString(Convert.ToInt32(sPrintingSNNNo) + 1);
                    iStoreRecordCount++;
                }
                if (dtStoreLaserFile.Rows.Count > 0)
                {
                    sPrintingSNNNo = Convert.ToString(Convert.ToInt32(sPrintingSNNNo) - 1);
                    dtResult = dlboj.StoreData(sGRPONO, sWorkOrderNo, PartCode, sReelBarcode, dtStoreLaserFile,
                    iActualQty, iActualArraySize, sBatchNo, Convert.ToInt32(sPrintingSNNNo)
                    , sSiteCode, sUseriD, sLineCode, sPacketType
                    );
                    if (dtResult.Rows.Count > 0)
                    {
                        PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.MethodBase.GetCurrentMethod().Name, "PCB Generate Result : " + dtResult.Rows[0][0].ToString());
                        if (dtResult.Rows[0][0].ToString().StartsWith("ERROR~"))
                        {
                            sResult = dtResult.Rows[0][0].ToString();
                            PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, sResult);
                            return sResult;
                        }
                        else
                        {
                            iStoreRecordCount = 1;
                        }
                    }
                }
                if (iStoreRecordCount > 0)
                {
                    sResult = "SUCCESS~File generated successfully.";
                }
                else
                {
                    PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtMessage, System.Reflection.MethodBase.GetCurrentMethod().Name, "PCB Generate Final Result : N~File generation fail, Please try again ");
                    sResult = "N~File generation fail, Please try again";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }



        public string TMOGeneration(
             string sGRPONO, string sWorkOrderNo,
            string PartCode, string sBatchNo, int iQty,
          int iArraySize, string sReelBarcode, string CustomerPartCode, string sFGItemCode
            , string sSiteCode, string sUseriD, string sLineCode, string sDesignerFormat, string sPacketType
            , int iNOSCount, string sPrefix, string sPurchaseOrder
            )
        {
            string sResult = string.Empty;
            try
            {
                dlboj = new DL_WIPLaserMachinePrinting();
                DataTable dtResult = new DataTable();
                int iStoreRecordCount = 0;
                int iActualArraySize = iArraySize;
                int iActualQty = iQty;
                iQty = iNOSCount * iQty;
                iArraySize = iNOSCount * iArraySize;
                int iPacketSize = iQty;
                if (iQty % iArraySize != 0)
                {
                    sResult = "N~Packet size is not divisible by array size,Please check the array size(" + iArraySize.ToString() + ") or packet size(" + iPacketSize.ToString() + ") for scanned barcode.";
                    return sResult;
                }
                iQty = iQty / iArraySize;
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,
                     System.Reflection.MethodBase.GetCurrentMethod().Name, "Packet Barcode : " + sReelBarcode +
                     ",Array Size:" + iArraySize.ToString()
                     + ", Work Order No :" + sWorkOrderNo + ", Part Code : " + PartCode +
                     ", Qty : " + iQty.ToString()
                     + ",Line Code : " + sLineCode + ",Prefix :" + sPrefix + ", Design Format : " + sDesignerFormat
                     );
                dtResult = dlboj.StoreTMOBarcode(sGRPONO, sWorkOrderNo, PartCode, sReelBarcode, sPurchaseOrder,
                iActualQty, iActualArraySize, CustomerPartCode, Convert.ToInt32(1)
                , sSiteCode, sUseriD, sLineCode, sFGItemCode, sPacketType
                );
                if (dtResult.Rows.Count > 0)
                {
                    PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.MethodBase.GetCurrentMethod().Name, "PCB Generate Result : " + dtResult.Rows[0][0].ToString());
                    if (dtResult.Rows[0][0].ToString().StartsWith("ERROR~"))
                    {
                        sResult = dtResult.Rows[0][0].ToString();
                        PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, sResult);
                        return sResult;
                    }
                    else
                    {
                        iStoreRecordCount = 1;
                    }
                }
                if (iStoreRecordCount > 0)
                {
                    sResult = "SUCCESS~File generated successfully.";
                }
                else
                {
                    PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtMessage, System.Reflection.MethodBase.GetCurrentMethod().Name, "PCB Generate Final Result : N~File generation fail, Please try again ");
                    sResult = "N~File generation fail, Please try again";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }

        #endregion


        #region PCB printing
        public DataTable BindLPFGItemCode(string sSiteCode)
        {
            DataTable dtINELPartNo = new DataTable();
            DL_WIPLaserMachinePrinting dlobj = new DL_WIPLaserMachinePrinting();
            try
            {
                dtINELPartNo = dlobj.BindLPFGItemCode(sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtINELPartNo;
        }
        public DataTable BindLPWorkOrderNo(string sFGItemCode, string sSiteCode)
        {
            DataTable dtINELPartNo = new DataTable();
            DL_WIPLaserMachinePrinting dlobj = new DL_WIPLaserMachinePrinting();
            try
            {
                dtINELPartNo = dlobj.BindLPWorkOrderno(sFGItemCode, sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtINELPartNo;
        }
        public DataTable BindPendingBarcode(string sFGItemCode, string sWorkOrderNo
            , string sSiteCode
            )
        {
            DataTable dtINELPartNo = new DataTable();
            DL_WIPLaserMachinePrinting dlobj = new DL_WIPLaserMachinePrinting();
            try
            {
                dtINELPartNo = dlobj.BindPendingBarcode(sFGItemCode, sWorkOrderNo, sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtINELPartNo;
        }
        public DataTable ValidatePCBBarcode(string sBarcode, string sWorkOrderNo, string sFGItemCode
            , string sSiteCode
            )
        {
            DataTable dtReelBarcode = new DataTable();
            DL_WIPLaserMachinePrinting dlobj = new DL_WIPLaserMachinePrinting();
            try
            {
                dtReelBarcode = dlobj.ValidatePCBBarcodeForLaserFilePrint(sBarcode, sWorkOrderNo, sFGItemCode
                    , sSiteCode
                    );
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtReelBarcode;
        }

        public string blPrintLabel(string _ReelID, string sPrinterIP,
       string sPrinterPort, string sPartCode
            , string sCustomer, string sSiteCode, string sFGItemCode, string sUserID, string sLineCode
            ,string sTMOProductNo
            )
        {
            DL_WIPLaserMachinePrinting dlobj = new DL_WIPLaserMachinePrinting();
            string sResult = string.Empty;
            string sPRN = string.Empty;
            BL_Common objBL_Common = new BL_Common();
            try
            {
                string chkPrinterStatus = string.Empty;
                chkPrinterStatus = objBL_Common.validPrinterConnection(sPrinterIP);
                if (chkPrinterStatus == "PRINTER READY")
                {
                    DL_WIP_LABEL_PRINTING dlWIPPrinting = new DL_WIP_LABEL_PRINTING();
                    DataTable dt = new DataTable();
                    dt = dlWIPPrinting.GetPrn(sSiteCode, sCustomer, sFGItemCode, "PCB");
                    if (dt.Rows.Count > 0)
                    {
                        try
                        {
                            sPRN = dt.Rows[0][0].ToString();
                            string sPRNPrintingResult = string.Empty;
                            sPRNPrintingResult = objBL_Common.WIP_PCB_LABEL(sPRN, _ReelID);
                            if (sTMOProductNo.Length > 0)
                            {
                                sPRNPrintingResult = objBL_Common.sTMOLabelPrint(sPRNPrintingResult, sTMOProductNo);
                            }
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
                                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, MethodBase.GetCurrentMethod().Name, "WIP PCB Label Printing Called :" + _ReelID);
                                sResult = objBL_Common.sPrintDataLabel(sPrinterIP
                                       , sPRNPrintingResult, _ReelID.Replace('/', '~').Replace(':', '~'), "WIPPCBLABEL"
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
                    else
                    {
                        sResult = "N~Prn not found";
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

        public DataTable dlUpdateData(string sPartBarcode, string sSiteCode, string sUseriD)
        {
            DataTable dt = new DataTable();
            try
            {
                DL_WIPLaserMachinePrinting dlobj = new DL_WIPLaserMachinePrinting();
                dt = dlobj.UpdateSNStatus(sPartBarcode, sSiteCode, sUseriD);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, Assembly.GetExecutingAssembly().GetName() + "::" + MethodBase.GetCurrentMethod().Name, ex.Message);

            }
            return dt;
        }


        #endregion


        #region PCB Mapping
        public DataTable BindMappingWorkOrder(string sSiteCode)
        {
            DataTable dtINELPartNo = new DataTable();
            DL_WIPLaserMachinePrinting dlobj = new DL_WIPLaserMachinePrinting();
            try
            {
                dtINELPartNo = dlobj.BindMappindWorkOrderNo(sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtINELPartNo;
        }
        public DataTable ValidateMappedPCBBarcode(string sSiteCode, string sWorkOrderNo, string sPCBBarcode)
        {
            DataTable dtINELPartNo = new DataTable();
            DL_WIPLaserMachinePrinting dlobj = new DL_WIPLaserMachinePrinting();
            try
            {
                dtINELPartNo = dlobj.ValidatePCBBarcode(sSiteCode, sWorkOrderNo, sPCBBarcode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtINELPartNo;
        }
        public DataTable UpdateMappingData(string sSiteCode, string sWorkOrderNo, string sPCBBarcode,
            DataTable dtLaserFile, string sMappedBy)
        {
            DataTable dtINELPartNo = new DataTable();
            DL_WIPLaserMachinePrinting dlobj = new DL_WIPLaserMachinePrinting();
            try
            {
                dtINELPartNo = dlobj.UpdatePCBMappingData(sSiteCode, sWorkOrderNo, sPCBBarcode, dtLaserFile, sMappedBy);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtINELPartNo;
        }

        #endregion
    }
}
