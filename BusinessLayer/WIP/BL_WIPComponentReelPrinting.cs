using Common;
using System;
using System.Data;
using System.Text;
using DataLayer.WIP;

namespace BusinessLayer.WIP
{
    public class BL_WIPComponentReelPrinting
    {
        DL_ComponentForming dlobj;
        public DataTable dtBindSFGItemCode()
        {
            DataTable dt = new DataTable();
            dlobj = new DL_ComponentForming();
            try
            {
                dt = dlobj.BindSFGItemCode();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dt;
        }

        public string ValidateFormingTool(string sFORMING_TOOL_ID, string sSFGItemCode)
        {
            string sResult = string.Empty;
            dlobj = new DL_ComponentForming();
            try
            {
                DataTable dt = new DataTable();
                dt = dlobj.ValidateTool(sFORMING_TOOL_ID, sSFGItemCode);
                if (dt.Rows.Count == 0)
                {
                    sResult = "N~Scanned tool not mapped with selected SFG Item code";
                }
                else
                {
                    sResult = dt.Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }

        public DataTable ValidateReelBarcode(string _Part_Barcode, string sSFGItemCode, out string sResult)
        {
            DataTable dt = new DataTable();
            dlobj = new DL_ComponentForming();
            try
            {
                dt = dlobj.ValidateReel(_Part_Barcode, sSFGItemCode);
                if (dt.Rows.Count == 0)
                {
                    sResult = "N~No result found";
                }
                else
                {
                    if (dt.Columns.Count > 1)
                    {
                        sResult = "SUCCESS~Barcode data found~" + dt.Rows[0][0].ToString();
                    }
                    else
                    {
                        sResult = dt.Rows[0][0].ToString();
                    }

                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dt;
        }

        public string ComponentReelPrinting(string _PartCode, string _PartBarcode, string sPrintedBy, int iTotalQty,
            string sPrinterIP, string sFORMING_TOOL_ID, string sToolDesc,
            int iLotSize, string _PartDesc, string sBatchNo, string sPONO,
            int iNoOfLabels, string sSFGItemCode, string sIssueSlipNo, string sUserID, string sLineCode,string sSiteCode
            )
        {
            StringBuilder sb = new StringBuilder();
            dlobj = new DL_ComponentForming();
            string sResult = string.Empty;
            int iResult = 0; string sPRN = string.Empty;
            BL_Common objBL_Common = new BL_Common();
            try
            {
                int iLastNOPrint = 0;
                string chkPrinterStatus = string.Empty;
                chkPrinterStatus = objBL_Common.validPrinterConnection(sPrinterIP);
                if (chkPrinterStatus == "PRINTER READY")
                {
                    DataTable dt = objBL_Common.GetPRN("RM");
                    if (dt.Rows.Count > 0)
                    {
                        string sChildReelID = string.Empty;
                        int iPrintqty = 1;
                        int iChildQty = iTotalQty / iLotSize;
                        if (iChildQty < iNoOfLabels)
                        {
                            sResult = "N~Please enter valid no of lables";
                            return sResult;
                        }
                        if (iChildQty > iNoOfLabels)
                        {
                            iPrintqty = iNoOfLabels;
                        }
                        else
                        {
                            iPrintqty = iChildQty;
                        }
                        int iFTID = 0;
                        int iWIPIQty = 0;
                        sb.Length = 0;
                        sb.AppendLine(" SELECT COUNT(1) FROM WIP_FORMING_TOOL WHERE PART_BARCODE = '" + _PartBarcode + "'     ");
                        sb.AppendLine(" AND SITECODE=  '" + sSiteCode + "'");
                        iResult = dlobj.iValidateBarcode(sb);
                        if (iResult == 0)
                        {
                            sb.Length = 0;
                            sb.AppendLine(" INSERT INTO WIP_FORMING_TOOL  ");
                            sb.AppendLine(" ( ");
                            sb.AppendLine(" FORMING_TOOL_ID,TOOL_DESC,PART_CODE,PART_BARCODE,QTY,PART_DESC,SCANNED_BY,SITECODE,LINECODE ");
                            sb.AppendLine(" ) ");
                            sb.AppendLine(" VALUES ");
                            sb.AppendLine(" ( ");
                            sb.AppendLine(" '" + sFORMING_TOOL_ID + "','" + sToolDesc + "','" + sSFGItemCode + "','" + _PartBarcode + "','" + iTotalQty + "','" + _PartDesc + "', ");
                            sb.AppendLine(" '" + sPrintedBy + "','" + sSiteCode + "','" + sLineCode + "' ");
                            sb.AppendLine(" ) ");
                            for (int i = 0; i < iPrintqty; i++)
                            {
                                sChildReelID = string.Empty;
                                sChildReelID = _PartBarcode + '_' + i.ToString();//.PadLeft(2, '1');
                                sb.AppendLine(" INSERT INTO WIP_FORMING_DETAILS  ");
                                sb.AppendLine(" ( ");
                                sb.AppendLine(" FT_ID,PART_CODE,PART_BARCODE,QTY,STATUS,PRINTED_BY,SITECODE,LINECODE ");
                                sb.AppendLine(" ) ");
                                sb.AppendLine(" VALUES ");
                                sb.AppendLine(" ( ");
                                sb.AppendLine(" (SELECT IDENT_CURRENT('WIP_FORMING_TOOL')),'" + sSFGItemCode + "','" + sChildReelID + "','" + iLotSize + "',0, ");
                                sb.AppendLine(" '" + sPrintedBy + "','" + sSiteCode + "','" + sLineCode + "' ");
                                sb.AppendLine(" ); ");
                                sb.AppendLine(" INSERT INTO WIP_INVENTORY ");
                                sb.AppendLine(" ( ");
                                sb.AppendLine(" PART_CODE,PART_BARCODE,QTY,STATUS,BATCHNO,GRPONO,SITECODE,LINECODE,INSERTED_BY,INVENTORY_TYPE,WORK_ORDER_NO ");
                                sb.AppendLine(" ) ");
                                sb.AppendLine(" VALUES ");
                                sb.AppendLine(" ( ");
                                sb.AppendLine(" '" + sSFGItemCode + "','" + sChildReelID + "','" + iLotSize + "',0, ");
                                sb.AppendLine(" '" + sBatchNo + "','" + sPONO + "', ");
                                sb.AppendLine(" '" + sSiteCode + "','" + sLineCode + "', ");
                                sb.AppendLine(" '" + sUserID + "',2,'" + sIssueSlipNo + "' ");
                                sb.AppendLine(" ); ");
                                iWIPIQty = iWIPIQty + iLotSize;
                                try
                                {
                                    sPRN = dt.Rows[0][0].ToString();
                                    string sPRNPrintingResult = objBL_Common.ComponentPrinting(sPRN, _PartCode, _PartBarcode, iChildQty,
                                        sFORMING_TOOL_ID, iLotSize, "", sChildReelID);
                                    if (sPRNPrintingResult.Length == 0)
                                    {
                                        sResult = "N~ Qty updated but printing failed";
                                    }
                                    else if (sPRNPrintingResult.StartsWith("N~"))
                                    {
                                        sResult = sPRNPrintingResult;
                                    }
                                    else
                                    {
                                        sResult = objBL_Common.sPrintDataLabel(sPrinterIP, sPRNPrintingResult, sChildReelID,
                                            "Component Reel "
                                            , sUserID, sLineCode

                                            );
                                        if (sResult == "SUCCESS")
                                        {
                                            sResult = "SUCCESS~Scanned barcode successfully print~" + Convert.ToString(iTotalQty);
                                            iLastNOPrint = iLastNOPrint + 1;
                                        }
                                        else if (sResult.Length == 0)
                                        {
                                            sResult = "N~ Qty updated but printing failed";
                                        }
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
                            sb.Length = 0;
                            sb.AppendLine(" SELECT FT_ID,ISNULL(TOTAL_BARCODE_COUNT,0) FROM WIP_FORMING_TOOL WHERE FORMING_TOOL_ID = '" + sFORMING_TOOL_ID + "'  ");
                            DataTable dtData = dlobj.BindData(sb);
                            int iCount = 0;
                            if (dtData.Rows.Count > 0)
                            {
                                iFTID = Convert.ToInt32(dtData.Rows[0][0].ToString());
                                iLastNOPrint = Convert.ToInt32(dtData.Rows[0][1].ToString());
                                iCount = iLastNOPrint;
                            }
                            sb.Length = 0;
                            for (int i = iCount + 1; i <= iPrintqty + iCount; i++)
                            {
                                sChildReelID = string.Empty;
                                sChildReelID = _PartBarcode + '_' + i.ToString();//.PadLeft(2, '1');
                                sb.AppendLine(" INSERT INTO WIP_FORMING_DETAILS  ");
                                sb.AppendLine(" ( ");
                                sb.AppendLine(" FT_ID,PART_CODE,PART_BARCODE,QTY,STATUS,PRINTED_BY,SITECODE,LINECODE ");
                                sb.AppendLine(" ) ");
                                sb.AppendLine(" VALUES ");
                                sb.AppendLine(" ( ");
                                sb.AppendLine(" '" + iFTID + "','" + sSFGItemCode + "','" + sChildReelID + "','" + iLotSize + "',0, ");
                                sb.AppendLine(" '" + sPrintedBy + "','" + sSiteCode + "','" + sLineCode + "' ");
                                sb.AppendLine(" ); ");

                                sb.AppendLine(" INSERT INTO WIP_INVENTORY ");
                                sb.AppendLine(" ( ");
                                sb.AppendLine(" PART_CODE,PART_BARCODE,QTY,STATUS,BATCHNO,GRPONO,SITECODE,LINECODE,INSERTED_BY,INVENTORY_TYPE,WORK_ORDER_NO ");
                                sb.AppendLine(" ) ");
                                sb.AppendLine(" VALUES ");
                                sb.AppendLine(" ( ");
                                sb.AppendLine(" '" + sSFGItemCode + "','" + sChildReelID + "','" + iLotSize + "',0, ");
                                sb.AppendLine(" '" + sBatchNo + "','" + sPONO + "', ");
                                sb.AppendLine(" '" + sSiteCode + "','" + sLineCode + "', ");
                                sb.AppendLine(" '" + sUserID + "',2,'" + sIssueSlipNo + "' ");
                                sb.AppendLine(" ); ");
                               iWIPIQty = iWIPIQty + iLotSize;
                                try
                                {
                                    sPRN = dt.Rows[0][0].ToString();
                                    string sPRNPrintingResult = objBL_Common.ComponentPrinting(sPRN, _PartCode, _PartBarcode, iChildQty,
                                      sFORMING_TOOL_ID, iLotSize, "", sChildReelID);
                                    if (sPRNPrintingResult.Length == 0)
                                    {
                                        sResult = "N~ Qty updated but printing failed";
                                    }
                                    else if (sPRNPrintingResult.StartsWith("N~"))
                                    {
                                        sResult = sPRNPrintingResult;
                                    }
                                    else
                                    {
                                        sResult = objBL_Common.sPrintDataLabel(sPrinterIP, sPRNPrintingResult, sChildReelID,
                                            "Component Reel "
                                            , sUserID, sLineCode
                                            );
                                        if (sResult == "SUCCESS")
                                        {
                                            sResult = "SUCCESS~Scanned barcode successfully print~" + Convert.ToString(iTotalQty);
                                            iLastNOPrint = iLastNOPrint + 1;
                                        }
                                        else if (sResult.Length == 0)
                                        {
                                            sResult = "N~Printing Failed";
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                                    sResult = "N~Qty updated but printing data not found";
                                }
                            }
                        }
                        sb.AppendLine(" UPDATE WIP_INVENTORY SET CONSUMED_QTY = CONSUMED_QTY + '" + iWIPIQty + "',STATUS = 1 WHERE PART_BARCODE = '" + _PartBarcode + "' AND SITECODE = '" + sSiteCode+"' ; ");
                        sb.AppendLine(" UPDATE WIP_SET_TOOLID SET USED_QTY = USED_QTY + '" + iPrintqty + "' WHERE TOOL_ID = '" + sFORMING_TOOL_ID + "' AND SITECODE = '" + sSiteCode + "'  ; ");
                        sb.AppendLine(" UPDATE  WIP_INVENTORY SET STATUS = 2 WHERE  PART_BARCODE = '" + _PartBarcode + "' AND CONSUMED_QTY = QTY AND REM_QTY = 0 AND SITECODE = '" + sSiteCode + "' ;");
                        sb.AppendLine(" UPDATE WIP_FORMING_TOOL SET TOTAL_BARCODE_COUNT = ISNULL(TOTAL_BARCODE_COUNT,0)+ '" + iPrintqty + "' WHERE PART_BARCODE = '" + _PartBarcode + "' AND SITECODE = '" + sSiteCode + "'  ; ");
                        iResult = dlobj.SaveBarcode(sb);
                        if (iResult > 0)
                        {
                            sb.Length = 0;
                            sb.AppendLine(" INSERT INTO WIP_INVENTORY_H  ");
                            sb.AppendLine(" ([WINO],[PART_CODE],[PART_BARCODE],[BATCHNO],[QTY],[CONSUMED_QTY] ");
                            sb.AppendLine(" ,[REM_QTY],[STATUS],[INSERTED_ON] ");
                            sb.AppendLine("  ,[GRPONO],[WORK_ORDER_NO],[SITECODE],[LINECODE],[INSERTED_BY] ");
                            sb.AppendLine(" ,[LOCATION],[PUTWAY_ON],[PUTWAY_BY],[INVENTORY_TYPE]) ");
                            sb.AppendLine(" SELECT[WINO],[PART_CODE],[PART_BARCODE],[BATCHNO],[QTY],[CONSUMED_QTY] ");
                            sb.AppendLine(" ,[REM_QTY],[STATUS],[INSERTED_ON] ");
                            sb.AppendLine(" ,[GRPONO],[WORK_ORDER_NO],[SITECODE],[LINECODE],[INSERTED_BY] ");
                            sb.AppendLine(" ,[LOCATION],[PUTWAY_ON],[PUTWAY_BY],[INVENTORY_TYPE] ");
                            sb.AppendLine("  FROM WIP_INVENTORY WHERE PART_BARCODE = '" + _PartBarcode + "' AND STATUS = 2 AND SITECODE = '" + sSiteCode + "' ; ");
                            sb.AppendLine("  DELETE FROM WIP_INVENTORY WHERE PART_BARCODE = '" + _PartBarcode + "' AND STATUS = 2 AND SITECODE = '" + sSiteCode + "'  ; ");
                            iResult = 0;
                            iResult = dlobj.SaveBarcode(sb);
                            sResult = "SUCCESS~Scanned barcode successfully print~" + Convert.ToString(iTotalQty);
                        }
                        else
                        {
                            sResult = "N~No result found, Please try again";
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
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }
    }
}
