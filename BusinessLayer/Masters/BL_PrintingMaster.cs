using Common;
using System;
using System.Data;
using System.Text;
using DataLayer.Masters;

namespace BusinessLayer.Masters
{
    public class BL_PrintingMaster
    {
        DL_Label_Printing dlobj = null;
        StringBuilder sb = null;

        #region LabelPrinting
        public DataTable GetData(string type)
        {
            DataTable dt = new DataTable();
            dlobj = new DL_Label_Printing();
            try
            {
                if (type == "PALLET")
                {
                    sb = new StringBuilder("select DISTINCT PALLETCODE AS  BIN_ID , PALLETCODE AS BIN_DESC from mPALLETMASTER");                 
                }
                if (type == "Bin")
                {
                    sb = new StringBuilder("select DISTINCT BIN_ID AS  BIN_ID , BIN_DESC AS BIN_DESC from WIP_BIN_MASTER");
                    sb.AppendLine(" WHERE SITECODE = " + PCommon.sSiteCode);
                }
                else if (type == "Part Code")
                {
                    sb = new StringBuilder("select DISTINCT PART_CODE AS  BIN_ID , PART_DESC AS BIN_DESC from mPARTCODEMASTER");
                    sb.AppendLine(" WHERE SITECODE = " + PCommon.sSiteCode);
                }
               
                dt = dlobj.GetSeletedData(sb);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message + ", Query " + sb.ToString());
                throw ex;
            }
            return dt;
        }

        public string ToolBinPrinting(string ID, String PrinterIP, string sDesc, string sType
             , string sUserID, string sLineCode
            )
        {
            string sPrintingResult = string.Empty;
            sb = new StringBuilder();
            dlobj = new DL_Label_Printing();
            BL_Common objBL_Common = new BL_Common();
            try
            {
                string chkPrinterStatus = string.Empty;
                chkPrinterStatus = objBL_Common.validPrinterConnection(PrinterIP);
                string sPart_Barcode = string.Empty;
                string sPRN = string.Empty;
                if (chkPrinterStatus == "PRINTER READY")
                {
                    DataTable dt = new DataTable();
                    if (sType == "PALLET")
                    {
                        dt = objBL_Common.GetPRN("ID");
                    }
                    else
                    {
                        dt = objBL_Common.GetPRN("ID");
                    }

                    if (dt.Rows.Count > 0)
                    {
                        sb.Length = 0;
                        sPRN = dt.Rows[0][0].ToString();
                        string sPRNPrintingResult = string.Empty;
                        if (sType == "Bin")
                        {
                            sPRNPrintingResult = objBL_Common.ALLPrinting(sPRN, ID, sDesc,"", "Bin Name", "Bin Code");
                        }
                        else if (sType == "Part Code")
                        {
                            sPRNPrintingResult = objBL_Common.ALLPrinting(sPRN, ID, sDesc, "", "Part Desc", "Part Code");
                        }
                        else if (sType == "PALLET")
                        {
                            sPRNPrintingResult = objBL_Common.WIP_BARCODE_LABEL(sPRN, ID);
                        }
                        if (sPRNPrintingResult.Trim().Length == 0)
                        {
                            sPrintingResult = "N~Printer not found, Location printing failed.";
                        }
                        else
                        {
                            if (sType == "Part Code")
                            {
                                sType = "PARTCODE";
                            }
                            sPrintingResult = objBL_Common.sPrintDataLabel(PrinterIP, sPRNPrintingResult, ID, sType
                                , sUserID, sLineCode
                                );
                        }
                    }
                    else
                    {
                        sPrintingResult = "PRNNOTFOUND~Prn for printing is not available.";
                    }

                }
                else
                {
                    sPrintingResult = chkPrinterStatus;
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message + ", Query " + sb.ToString());
                throw ex;
            }
            return sPrintingResult;
        }

        #endregion

        public DataTable GetDataForPallet()
        {
            DataTable dt = new DataTable();
            dlobj = new DL_Label_Printing();
            try
            {
                sb = new StringBuilder("select * from mPALLETMASTER");
                dt = dlobj.GetSeletedData(sb);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message + ", Query " + sb.ToString());
                throw ex;
            }
            return dt;
        }

        public string PalletPrinting(string sPrinterIP, string sPrinterPort, string sUserID, string sLineCode)
        {
            int iResult = 0;
            string sPrintingResult = string.Empty;
            sb = new StringBuilder();
            dlobj = new DL_Label_Printing();
            BL_Common objBL_Common = new BL_Common();
            try
            {
                string chkPrinterStatus = string.Empty;
                chkPrinterStatus = objBL_Common.validPrinterConnection(sPrinterIP);
                if (chkPrinterStatus == "PRINTER READY")
                {
                    DataTable dt = objBL_Common.GetPRN("ID");
                    if (dt.Rows.Count > 0)
                    {
                        sb.Length = 0;
                        string sSNNo = objBL_Common.sPalletGenerateSrNo("PL");
                        if (sSNNo.Length > 0)
                        {
                            sb.AppendLine("INSERT INTO mPALLETMASTER([PALLETCODE])");
                            sb.AppendLine("VALUES ('" + sSNNo + "')");
                            iResult = dlobj.SavePalletPrinting(sb);
                            if (iResult > 0)
                            {
                                try
                                {
                                    string sPRN = dt.Rows[0][0].ToString();
                                    string sPRNPrintingResult = objBL_Common.WIP_BARCODE_LABEL(sPRN, sSNNo);
                                    if (sPRNPrintingResult.Trim().Length == 0)
                                    {
                                        sPrintingResult = "N~Printer not found, label printing failed, but data is saved, Reel Barcode : " + sSNNo;
                                    }
                                    else
                                    {
                                        sPrintingResult = objBL_Common.sPrintDataLabel(sPrinterIP, sPRN, sSNNo, "PALLET"
                                            , sUserID, sLineCode
                                            );
                                    }
                                }
                                catch (Exception ex)
                                {
                                    PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message + ", Query " + sb.ToString());
                                    sPrintingResult = "N~PRN Printing failed but data saved, Reel Barcode" + sSNNo;
                                }
                            }
                            else
                            {
                                sPrintingResult = "N~No result found for printing";
                            }
                        }
                        else
                        {
                            sPrintingResult = "N~Serial No generation failed";
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
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message + ", Query " + sb.ToString());
                throw ex;
            }
            return sPrintingResult;
        }
    }
}
