using System;
using System.Data;
using System.Text;
using DataLayer;
using Common;
using System.Reflection;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Configuration;
using BusinessLayer.Masters;

namespace BusinessLayer
{
    public class BL_Common
    {
        StringBuilder sb = null;
        DL_Common dlboj = null;
        public DataTable BINDPRINTER(string sType)
        {
            DataTable dtBindPrinter = new DataTable();
            dlboj = new DL_Common();
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.AppendLine(" SELECT PRINTER_IP,PRINTER_PORT FROM mPRINTERMASTER ");
                sb.AppendLine(" WHERE TYPE = '" + sType + "' ");
                dtBindPrinter = dlboj.dtBindData(sb);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtBindPrinter;
        }

        public DataTable GetPRN(string sType)
        {
            DataTable dtGetPRN = new DataTable();
            dlboj = new DL_Common();
            sb = new StringBuilder();
            try
            {
                sb.AppendLine("select PRN_VALUE from mPRNMASTER WHERE TYPE = '" + sType + "' ");
                dtGetPRN = dlboj.dtGetPRN(sb);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtGetPRN;
        }

        public string sShowNotificationTool(string sTOOLID)
        {
            string sResult = string.Empty;
            dlboj = new DL_Common();
            sb = new StringBuilder();
            DataTable dt = new DataTable();
            try
            {
                sb.AppendLine(" SELECT COUNT(1) FROM WIP_SET_TOOLID  ");
                sb.AppendLine("  where TOOL_ID= '" + sTOOLID + "' ");
                dt = dlboj.dtBindData(sb);
                if (dt.Rows.Count == 0)
                {
                    sResult = "N~Scanned tool does not exist";
                }
                else
                {
                    sb.Length = 0;
                    sb.AppendLine(" SELECT (((USED_QTY) *100)/QTY) tool_per FROM WIP_SET_TOOLID ");
                    sb.AppendLine("  where TOOL_ID= '" + sTOOLID + "' ");
                    dt = dlboj.dtBindData(sb);
                    if (dt.Rows.Count > 0)
                    {
                        decimal d = Convert.ToDecimal(dt.Rows[0][0].ToString());
                        if (d > 80)
                        {
                            sResult = "N~Scanned tool already consumed more than 80%, Please increase the capacity";
                        }
                        else
                        {
                            sResult = "SUCCESS~you can use it";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                sResult = "ERROR~" + ex.Message;
            }
            return sResult;
        }
        public string sPalletGenerateSrNo(string sType)
        {
            string sSN = string.Empty;
            dlboj = new DL_Common();
            sb = new StringBuilder();
            try
            {

                sSN = dlboj.GenerateSN(sType);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sSN;
        }


        public string GetRMPrnPrintingDetails(string sPRN, string sPartCode, string sPartBarcode, string sType)
        {
            string sFinalPRN = string.Empty;
            StringBuilder sb = new StringBuilder();
            try
            {
                DL_Common dlobj = new DL_Common();
                sb.AppendLine(" SELECT TR.GRPONO,format(TR.GRPO_DATE,'dd/MM/yyyy')PO_DATE,TR.SUP_INV_NO,TRI.RI_ID, ");
                sb.AppendLine(" TR.SUP_INV_DATE,TR.MPN,TRI.PART_CODE,TR.PART_DESC, ");
                sb.AppendLine(" TRI.BATCHNO,TR.VENDOR_NAME,TRI.EXP_DATE,TRI.MFG_DATE ");
                sb.AppendLine(", TRI.APPROVED_QTY, TRI.REJECT_QTY,TRI.LOT_QTY ");
                sb.AppendLine(", TRI.LH_RH, TRI.MSL_INFO, TRI.PRINTED_BY ");
                sb.AppendLine(" from RM_RECEIVING TR ");
                sb.AppendLine(" INNER JOIN ");
                sb.AppendLine(" RM_INVENTORY TRI ON TRI.RM_ID = TR.RM_ID  ");
                sb.AppendLine(" WHERE PART_BARCODE ='" + sPartBarcode + "'");
                sb.AppendLine(" AND TRI.PART_CODE ='" + sPartCode + "'");
                sb.AppendLine(" ORDER BY TRI.RI_ID DESC ");
                DataTable dt = dlobj.dtBindData(sb);
                if (dt.Rows.Count > 0)
                {
                    string prn = sPRN;
                    prn = prn.Replace("{MPN}", dt.Rows[0]["MPN"].ToString());
                    prn = prn.Replace("{BATCHNO}", dt.Rows[0]["BATCHNO"].ToString());
                    prn = prn.Replace("{USERID}", dt.Rows[0]["PRINTED_BY"].ToString());
                    prn = prn.Replace("{BARCODE}", sPartBarcode);
                    prn = prn.Replace("{BARCODE_HR}", sPartBarcode.Split(',')[1] + "," + sPartBarcode.Split(',')[2] + "," + sPartBarcode.Split(',')[3]);
                    prn = prn.Replace("{SAPPARTCODE}", dt.Rows[0]["PART_CODE"].ToString());
                    prn = prn.Replace("{PARTDESC}", dt.Rows[0]["PART_DESC"].ToString());
                    prn = prn.Replace("{GRPO}", dt.Rows[0]["GRPONO"].ToString().Split('(')[0]);
                    prn = prn.Replace("{GRPODT}", dt.Rows[0]["PO_DATE"].ToString());
                    prn = prn.Replace("{LOT}", dt.Rows[0]["MFG_DATE"].ToString());
                    prn = prn.Replace("{LF}", dt.Rows[0]["LH_RH"].ToString());
                    prn = prn.Replace("{MSL}", dt.Rows[0]["MSL_INFO"].ToString());
                    prn = prn.Replace("{LOC}", PCommon.sSiteCode);
                    prn = prn.Replace("{Qty}", dt.Rows[0]["LOT_QTY"].ToString());
                    sFinalPRN = prn;
                }
                else
                {
                    sFinalPRN = "";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message + ", Query " + sb.ToString());
                throw ex;
            }
            return sFinalPRN;
        }

        public string PrintMaterialTransfer_ReprintBarcode(string sPRN, string sPartCode, string sPartBarcode, string sType)
        {
            string sFinalPRN = string.Empty;
            StringBuilder sb = new StringBuilder();
            try
            {
                DL_Common dlobj = new DL_Common();
                sb.AppendLine(" SELECT TR.GRPONO,format(TR.GRPO_DATE,'dd/MM/yyyy')PO_DATE,TR.SUP_INV_NO,TRI.RI_ID, ");
                sb.AppendLine(" TR.SUP_INV_DATE,TR.MPN,TRI.PART_CODE,MM.PART_DESC, ");
                sb.AppendLine(" TRI.BATCHNO,TR.VENDOR_NAME,TRI.EXP_DATE,TRI.MFG_DATE ");
                sb.AppendLine(", TRI.APPROVED_QTY, TRI.REJECT_QTY,TRI.LOT_QTY ");
                sb.AppendLine(", TRI.LH_RH, TRI.MSL_INFO, TRI.PRINTED_BY ");
                sb.AppendLine(" from RM_RECEIVING TR ");
                sb.AppendLine(" INNER JOIN ");
                sb.AppendLine(" RM_INVENTORY TRI ON TRI.RM_ID = TR.RM_ID  ");
                sb.AppendLine(" inner join  ");
                sb.AppendLine(" MPARTCODEMASTER MM ON MM.PART_CODE = TRI.PART_CODE ");
                sb.AppendLine(" WHERE PART_BARCODE ='" + sPartBarcode + "'");
                sb.AppendLine(" AND TRI.PART_CODE ='" + sPartCode + "'");
                sb.AppendLine(" ORDER BY TRI.RI_ID DESC ");
                DataTable dt = dlobj.dtBindData(sb);
                if (dt.Rows.Count > 0)
                {
                    string prn = sPRN;
                    string[] sPartBarcodePrint = sPartBarcode.Split(',');
                    if (sPartBarcodePrint.Length > 1)
                    {
                        prn = prn.Replace("{BARCODE_HR}", sPartBarcode.Split(',')[1] + "," + sPartBarcode.Split(',')[2] + "," + sPartBarcode.Split(',')[3]);
                    }
                    else
                    {
                        prn = prn.Replace("{BARCODE_HR}", sPartBarcode);
                    }
                    prn = prn.Replace("{MPN}", dt.Rows[0]["MPN"].ToString());
                    prn = prn.Replace("{BATCHNO}", dt.Rows[0]["BATCHNO"].ToString());
                    prn = prn.Replace("{USERID}", dt.Rows[0]["PRINTED_BY"].ToString());
                    prn = prn.Replace("{BARCODE}", sPartBarcode);

                    prn = prn.Replace("{SAPPARTCODE}", dt.Rows[0]["PART_CODE"].ToString());
                    prn = prn.Replace("{PARTDESC}", dt.Rows[0]["PART_DESC"].ToString());
                    prn = prn.Replace("{GRPO}", dt.Rows[0]["GRPONO"].ToString().Split('(')[0]);
                    prn = prn.Replace("{GRPODT}", dt.Rows[0]["PO_DATE"].ToString());
                    prn = prn.Replace("{LOT}", dt.Rows[0]["MFG_DATE"].ToString());
                    prn = prn.Replace("{LF}", dt.Rows[0]["LH_RH"].ToString());
                    prn = prn.Replace("{MSL}", dt.Rows[0]["MSL_INFO"].ToString());
                    prn = prn.Replace("{LOC}", PCommon.sSiteCode);
                    if (sType == "QualityRejectLabel" || sType == "QR") // Quality Reject Label Print
                    {
                        prn = prn.Replace("{Qty}", dt.Rows[0]["LOT_QTY"].ToString());
                    }
                    else if (sType == "RM") // RM Label Print
                    {
                        prn = prn.Replace("{Qty}", dt.Rows[0]["LOT_QTY"].ToString());
                    }
                    else if (sType == "CHILDLABEL") // Child Label Barcode Print
                    {
                        prn = prn.Replace("{Qty}", dt.Rows[0]["APPROVED_QTY"].ToString());
                    }
                    else
                    {
                        prn = prn.Replace("{Qty}", dt.Rows[0]["LOT_QTY"].ToString());
                    }
                    sFinalPRN = prn;
                }
                else
                {
                    sFinalPRN = "";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message + ", Query " + sb.ToString());
                throw ex;
            }
            return sFinalPRN;
        }


        public string WIPDryOutPrinting(string sPRN, string sPartCode, string sPartBarcode, string sType)
        {
            string sFinalPRN = string.Empty;
            StringBuilder sb = new StringBuilder();
            try
            {
                DL_Common dlobj = new DL_Common();
                sb.AppendLine(" SELECT TR.GRPONO,format(TR.GRPO_DATE,'dd/MM/yyyy')PO_DATE,TR.SUP_INV_NO,TRI.RI_ID, ");
                sb.AppendLine(" TR.SUP_INV_DATE,TR.MPN,TRI.PART_CODE,MM.PART_DESC, ");
                sb.AppendLine(" TRI.BATCHNO,TR.VENDOR_NAME,TRI.EXP_DATE,TRI.MFG_DATE ");
                sb.AppendLine(", TRI.APPROVED_QTY, TRI.REJECT_QTY,TRI.LOT_QTY ");
                sb.AppendLine(", TRI.LH_RH, TRI.MSL_INFO, TRI.PRINTED_BY ");
                sb.AppendLine(" from RM_RECEIVING TR ");
                sb.AppendLine(" INNER JOIN ");
                sb.AppendLine(" RM_INVENTORY_H TRI ON TRI.RM_ID = TR.RM_ID  ");
                sb.AppendLine(" inner join  ");
                sb.AppendLine(" MPARTCODEMASTER MM ON MM.PART_CODE = TRI.PART_CODE ");
                sb.AppendLine(" WHERE PART_BARCODE ='" + sPartBarcode + "'");
                sb.AppendLine(" AND TRI.PART_CODE ='" + sPartCode + "'");
                sb.AppendLine(" ORDER BY TRI.RI_ID DESC ");
                DataTable dt = dlobj.dtBindData(sb);
                if (dt.Rows.Count > 0)
                {
                    string prn = sPRN;
                    string[] sPartBarcodePrint = sPartBarcode.Split(',');
                    if (sPartBarcodePrint.Length > 1)
                    {
                        prn = prn.Replace("{BARCODE_HR}", sPartBarcode.Split(',')[1] + "," + sPartBarcode.Split(',')[2] + "," + sPartBarcode.Split(',')[3]);
                    }
                    else
                    {
                        prn = prn.Replace("{BARCODE_HR}", sPartBarcode);
                    }
                    prn = prn.Replace("{MPN}", dt.Rows[0]["MPN"].ToString());
                    prn = prn.Replace("{BATCHNO}", dt.Rows[0]["BATCHNO"].ToString());
                    prn = prn.Replace("{USERID}", dt.Rows[0]["PRINTED_BY"].ToString());
                    prn = prn.Replace("{BARCODE}", sPartBarcode);

                    prn = prn.Replace("{SAPPARTCODE}", dt.Rows[0]["PART_CODE"].ToString());
                    prn = prn.Replace("{PARTDESC}", dt.Rows[0]["PART_DESC"].ToString());
                    prn = prn.Replace("{GRPO}", dt.Rows[0]["GRPONO"].ToString().Split('(')[0]);
                    prn = prn.Replace("{GRPODT}", dt.Rows[0]["PO_DATE"].ToString());
                    prn = prn.Replace("{LOT}", dt.Rows[0]["MFG_DATE"].ToString());
                    prn = prn.Replace("{LF}", dt.Rows[0]["LH_RH"].ToString());
                    prn = prn.Replace("{MSL}", dt.Rows[0]["MSL_INFO"].ToString());
                    prn = prn.Replace("{LOC}", PCommon.sSiteCode);
                    prn = prn.Replace("{Qty}", dt.Rows[0]["LOT_QTY"].ToString());
                    sFinalPRN = prn;
                }
                else
                {
                    sFinalPRN = "";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message + ", Query " + sb.ToString());
                throw ex;
            }
            return sFinalPRN;
        }

        public string GetReturnPrintingDetails(string sPRN, string sMRNNo, string sPartCode,
            string sPartBarcode, decimal dQty
           , string sParentBarcode)
        {
            string sFinalPRN = string.Empty;
            StringBuilder sb = new StringBuilder();
            try
            {
                DL_Common dlobj = new DL_Common();
                sb.AppendLine(" SELECT Distinct TR.GRPONO,format(TR.GRPO_DATE,'dd/MM/yyyy')PO_DATE,TR.SUP_INV_NO,TRI.RI_ID, ");
                sb.AppendLine(" TR.SUP_INV_DATE,TR.MPN,TRI.PART_CODE,MM.PART_DESC, ");
                sb.AppendLine(" TRI.BATCHNO,TR.VENDOR_NAME,TRI.EXP_DATE,TRI.MFG_DATE ");
                sb.AppendLine(", TRI.APPROVED_QTY, TRI.REJECT_QTY,TRI.LOT_QTY ");
                sb.AppendLine(", TRI.LH_RH, TRI.MSL_INFO, TRI.PRINTED_BY ");
                sb.AppendLine(" from RM_RECEIVING TR ");
                sb.AppendLine(" INNER JOIN ");
                sb.AppendLine(" RM_INVENTORY_H TRI ON TRI.RM_ID = TR.RM_ID  ");
                sb.AppendLine(" inner join  ");
                sb.AppendLine(" MPARTCODEMASTER MM ON MM.PART_CODE = TRI.PART_CODE ");
                sb.AppendLine(" WHERE PART_BARCODE ='" + sParentBarcode + "'");
                sb.AppendLine(" AND TRI.PART_CODE ='" + sPartCode + "'");
                sb.AppendLine(" ORDER BY TRI.RI_ID DESC ");
                DataTable dt = dlobj.dtBindData(sb);
                if (dt.Rows.Count > 0)
                {
                    string prn = sPRN;
                    prn = prn.Replace("{MPN}", dt.Rows[0]["MPN"].ToString());
                    prn = prn.Replace("{BATCHNO}", dt.Rows[0]["BATCHNO"].ToString());
                    prn = prn.Replace("{USERID}", dt.Rows[0]["PRINTED_BY"].ToString());
                    prn = prn.Replace("{BARCODE}", sPartBarcode);
                    string[] sPartBarcodePrint = sPartBarcode.Split(',');
                    if (sPartBarcodePrint.Length > 1)
                    {
                        prn = prn.Replace("{BARCODE_HR}", sPartBarcode.Split(',')[1] + "," + sPartBarcode.Split(',')[2] + "," + sPartBarcode.Split(',')[3]);
                    }
                    else
                    {
                        prn = prn.Replace("{BARCODE_HR}", sPartBarcode);
                    }
                    prn = prn.Replace("{SAPPARTCODE}", dt.Rows[0]["PART_CODE"].ToString());
                    prn = prn.Replace("{PARTDESC}", dt.Rows[0]["PART_DESC"].ToString());
                    prn = prn.Replace("{GRPO}", dt.Rows[0]["GRPONO"].ToString().Split('(')[0]);
                    prn = prn.Replace("{GRPODT}", dt.Rows[0]["PO_DATE"].ToString());
                    prn = prn.Replace("{LOT}", dt.Rows[0]["MFG_DATE"].ToString());
                    prn = prn.Replace("{LF}", dt.Rows[0]["LH_RH"].ToString());
                    prn = prn.Replace("{MSL}", dt.Rows[0]["MSL_INFO"].ToString());
                    prn = prn.Replace("{LOC}", PCommon.sSiteCode);
                    prn = prn.Replace("{Qty}", dt.Rows[0]["LOT_QTY"].ToString());
                    sFinalPRN = prn;
                }
                else
                {
                    sFinalPRN = "";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message + ", Query " + sb.ToString());
                throw ex;
            }
            return sFinalPRN;
        }



        public string ComponentPrinting(string sPrn, string sPartCode, string sPartBarcode, int iQty,
            string sFormingToolID, int iSize, string sProductID, string sChildPartBarcode)
        {
            string sFinalPRN = string.Empty;
            sb = new StringBuilder();
            try
            {
                DL_Common dlobj = new DL_Common();
                sb.AppendLine(" SELECT Distinct TR.GRPONO,format(TR.GRPO_DATE,'dd/MM/yyyy')PO_DATE,TR.SUP_INV_NO,TRI.RI_ID, ");
                sb.AppendLine(" TR.SUP_INV_DATE,TR.MPN,TR.PART_CODE,MM.PART_DESC, ");
                sb.AppendLine(" TRI.BATCHNO,TR.VENDOR_NAME,TRI.EXP_DATE,TRI.MFG_DATE ");
                sb.AppendLine(", TRI.APPROVED_QTY, TRI.REJECT_QTY,TRI.LOT_QTY ");
                sb.AppendLine(", TRI.LH_RH, TRI.MSL_INFO, TRI.PRINTED_BY ");
                sb.AppendLine(" from RM_RECEIVING TR ");
                sb.AppendLine(" INNER JOIN ");
                sb.AppendLine(" RM_INVENTORY_H TRI ON TRI.RM_ID = TR.RM_ID  ");
                sb.AppendLine(" inner join  ");
                sb.AppendLine(" MPARTCODEMASTER MM ON MM.PART_CODE = TRI.PART_CODE ");
                sb.AppendLine(" WHERE PART_BARCODE ='" + sPartBarcode + "'");
                sb.AppendLine(" AND TRI.PART_CODE ='" + sPartCode + "'");
                sb.AppendLine(" ORDER BY TRI.RI_ID DESC ");
                DataTable dt = dlobj.dtBindData(sb);
                if (dt.Rows.Count > 0)
                {
                    string prn = sPrn;
                    prn = prn.Replace("{MPN}", dt.Rows[0]["MPN"].ToString());
                    prn = prn.Replace("{BATCHNO}", dt.Rows[0]["BATCHNO"].ToString());
                    prn = prn.Replace("{USERID}", dt.Rows[0]["PRINTED_BY"].ToString());
                    prn = prn.Replace("{BARCODE}", sPartBarcode);
                    string[] sPartBarcodePrint = sPartBarcode.Split(',');
                    if (sPartBarcodePrint.Length > 1)
                    {
                        prn = prn.Replace("{BARCODE_HR}", sPartBarcode.Split(',')[1] + "," + sPartBarcode.Split(',')[2] + "," + sPartBarcode.Split(',')[3]);
                    }
                    else
                    {
                        prn = prn.Replace("{BARCODE_HR}", sPartBarcode);
                    }
                    prn = prn.Replace("{SAPPARTCODE}", dt.Rows[0]["PART_CODE"].ToString());
                    prn = prn.Replace("{PARTDESC}", dt.Rows[0]["PART_DESC"].ToString());
                    prn = prn.Replace("{GRPO}", dt.Rows[0]["GRPONO"].ToString().Split('(')[0]);
                    prn = prn.Replace("{GRPODT}", dt.Rows[0]["PO_DATE"].ToString());
                    prn = prn.Replace("{LOT}", dt.Rows[0]["MFG_DATE"].ToString());
                    prn = prn.Replace("{LF}", dt.Rows[0]["LH_RH"].ToString());
                    prn = prn.Replace("{MSL}", dt.Rows[0]["MSL_INFO"].ToString());
                    prn = prn.Replace("{LOC}", PCommon.sSiteCode);
                    prn = prn.Replace("{Qty}", dt.Rows[0]["LOT_QTY"].ToString());
                    sFinalPRN = prn;
                }
                else
                {
                    sFinalPRN = "";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, Assembly.GetExecutingAssembly().GetName() + "::" + MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sFinalPRN;
        }

        public string WIP_PCB_LABEL(string sPrn, string sPartBarcode)
        {
            string sFinalPRN = string.Empty;
            try
            {
                string prn = sPrn;
                string[] arr = sPartBarcode.Split('#');
                for (int i = 0; i < arr.Length; i++)
                {
                    prn = prn.Replace("{PARTBARCODE_" + i + "}", arr[i].ToString());
                }
                sFinalPRN = prn;
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sFinalPRN;
        }

        public string sTMOLabelPrint(string sPrn, string sTMOBarcode)
        {
            string sFinalPRN = string.Empty;
            try
            {
                string prn = sPrn;
                string[] arr1 = sTMOBarcode.Split('#');
                for (int i = 0; i < arr1.Length; i++)
                {
                    prn = prn.Replace("{TMPPRODUCTCODE_" + i + "}", arr1[i].ToString());
                }
                sFinalPRN = prn;
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sFinalPRN;

        }

        public string WIP_BARCODE_LABEL(string sPrn, string sPartBarcode)
        {
            string sFinalPRN = string.Empty;
            try
            {
                string prn = sPrn;
                prn = prn.Replace("{PARTBARCODE}", sPartBarcode);
                sFinalPRN = prn;
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, Assembly.GetExecutingAssembly().GetName() + "::" + MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sFinalPRN;
        }

        public string PrimaryBoxPacking(string sPrn, string _FGItemCode, string _BoxID, string sProductID)
        {
            string sFinalPRN = string.Empty;
            sb = new StringBuilder();
            BL_MobCommon objMobCommon = new BL_MobCommon();
            try
            {
                DL_Common dlobj = new DL_Common();
                sb.AppendLine(" SELECT LINECODE,WFP.FG_ITEM_CODE,CUSTOMER_NAME,PACKSIZE, ");
                sb.AppendLine(" WISD.PART_NO,format(WFP.SCANNED_ON,'dd/MM/yyyy') SCANNED_ON,WISD.PART_DESC");
                sb.AppendLine("  from WIP_FG_PRI_PACKING WFP INNER JOIN  ");
                sb.AppendLine(" WIP_ITEM_SN_DATA WISD ON WISD.FG_ITEM_CODE = WFP.FG_ITEM_CODE ");
                sb.AppendLine(" AND WISD.CUSTOMER = WFP.CUSTOMER_PART_NUMBER");
                sb.AppendLine(" AND WISD.FIELD_VALUE = 'PRIMARY PACKING' ");
                sb.AppendLine(" WHERE BOX_ID ='" + _BoxID + "'");
                sb.AppendLine(" AND WFP.FG_ITEM_CODE ='" + _FGItemCode + "'");
                sb.AppendLine(" ");
                DataTable dt = dlobj.dtBindData(sb);
                if (dt.Rows.Count > 0)
                {
                    string prn = sPrn;
                    prn = prn.Replace("{VARLINEID}", dt.Rows[0]["LINECODE"].ToString());
                    prn = prn.Replace("{VARPACKINGDATE}", dt.Rows[0]["SCANNED_ON"].ToString());
                    prn = prn.Replace("{VARFGIETMCODE}", dt.Rows[0]["FG_ITEM_CODE"].ToString());
                    prn = prn.Replace("{VARPARTDESC}", dt.Rows[0]["PART_DESC"].ToString());
                    prn = prn.Replace("{VARPARTNO}", dt.Rows[0]["PART_NO"].ToString());
                    prn = prn.Replace("{VARCUSTNAME}", dt.Rows[0]["CUSTOMER_NAME"].ToString());
                    prn = prn.Replace("{QTY}", dt.Rows[0]["PACKSIZE"].ToString());
                    prn = prn.Replace("{BOXID}", _BoxID);
                    string sHRBoxID = objMobCommon.sFinalBarcodeValue(_BoxID);
                    prn = prn.Replace("{BOXID_H}", sHRBoxID);
                    prn = prn.Replace("{PRODUCTID}", _BoxID);
                    sFinalPRN = prn;
                }
                else
                {
                    sFinalPRN = "";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, Assembly.GetExecutingAssembly().GetName() + "::" + MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sFinalPRN;
        }

        public string SecondaryBoxPacking(string sPrn, string _FGItemCode, string _BoxID)
        {
            BL_MobCommon objMobCommon = new BL_MobCommon();
            string sFinalPRN = string.Empty;
            sb = new StringBuilder();
            try
            {
                dlboj = new DL_Common();
                sb.AppendLine(" SELECT LINECODE,WFS.FG_ITEM_CODE,CUSTOMER_NAME, WFP.PART_DESC FG_ITEM_DESC, ");
                sb.AppendLine(" CUSTOMER_LOC, ");
                sb.AppendLine(" NO_OF_CASE,WFS.QTY, FG_LOC_TYPE,LOCATIONCODE, wfs.CUSTOMER_PART_NUMBER,  ");
                sb.AppendLine(" WISD.PART_NO ");
                sb.AppendLine("  from WIP_FG_SEC_PACKING WFS ");
                sb.AppendLine(" INNER JOIN mPARTCODEMASTER WFP ON WFP.PART_CODE =  WFS.FG_ITEM_CODE ");
                sb.AppendLine("  INNER JOIN  ");
                sb.AppendLine(" WIP_ITEM_SN_DATA WISD ON WISD.FG_ITEM_CODE = WFS.FG_ITEM_CODE ");
                sb.AppendLine(" AND WISD.CUSTOMER = WFS.CUSTOMER_PART_NUMBER");
                sb.AppendLine(" AND WISD.FIELD_VALUE = 'SECONDARY PACKING' ");
                sb.AppendLine(" WHERE SECONDARY_BOX_ID ='" + _BoxID + "'");
                sb.AppendLine(" AND WFS.FG_ITEM_CODE ='" + _FGItemCode + "'");
                sb.AppendLine(" ");
                DataTable dt = dlboj.dtBindData(sb);
                if (dt.Rows.Count > 0)
                {
                    string prn = sPrn;
                    prn = prn.Replace("{VARPARTNO}", dt.Rows[0]["PART_NO"].ToString());
                    prn = prn.Replace("{LINEID}", dt.Rows[0]["LINECODE"].ToString());
                    prn = prn.Replace("{FGITEMCODE}", dt.Rows[0]["FG_ITEM_CODE"].ToString());
                    prn = prn.Replace("{CUSTNAME}", dt.Rows[0]["CUSTOMER_NAME"].ToString());
                    prn = prn.Replace("{DESC}", dt.Rows[0]["FG_ITEM_DESC"].ToString());
                    prn = prn.Replace("{CUSTLOC}", dt.Rows[0]["CUSTOMER_LOC"].ToString());
                    prn = prn.Replace("{CUSTPART}", dt.Rows[0]["CUSTOMER_PART_NUMBER"].ToString());
                    prn = prn.Replace("{NOC}", dt.Rows[0]["NO_OF_CASE"].ToString());
                    prn = prn.Replace("{QTY}", dt.Rows[0]["QTY"].ToString());
                    prn = prn.Replace("{LOCTYPE}", dt.Rows[0]["FG_LOC_TYPE"].ToString());
                    prn = prn.Replace("{LOCCODE}", dt.Rows[0]["LOCATIONCODE"].ToString());
                    prn = prn.Replace("{BOXID}", _BoxID);
                    string sHRBoxID = objMobCommon.sFinalBarcodeValue(_BoxID);
                    prn = prn.Replace("{BOXID_H}", sHRBoxID);
                    sFinalPRN = prn;
                }
                else
                {
                    sFinalPRN = "";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, Assembly.GetExecutingAssembly().GetName() + "::" + MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sFinalPRN;
        }



        public string ALLPrinting(string sPrn, string sID, string sDESC, string sPartCode,
            string sLabelType, string sLabelCode)
        {
            string sFinalPRN = string.Empty;
            sb = new StringBuilder();
            try
            {
                dlboj = new DL_Common();
                string prn = sPrn;
                if (sLabelCode == "Part Code")
                {
                    prn = prn.Replace("^BQN,2,3^FDLA,{MachineNo}^FS", "");
                }
                prn = prn.Replace("{Code}", sID);
                prn = prn.Replace("{QR}", sID);
                prn = prn.Replace("{NAME}", sDESC);
                prn = prn.Replace("{PARTCODE}", sPartCode);
                prn = prn.Replace("Machine Name", sLabelType);
                prn = prn.Replace("Machine No", sLabelCode);
                sFinalPRN = prn;
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, Assembly.GetExecutingAssembly().GetName() + "::" + MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sFinalPRN;
        }


        public bool DirectoryHasPermission(string DirectoryPath)
        {
            if (string.IsNullOrEmpty(DirectoryPath)) return false;
            try
            {
                using (StreamWriter sw = new StreamWriter(DirectoryPath + "\\1.csv"))
                {
                    sw.WriteLine();
                    sw.Flush();
                    sw.Close();
                    File.Delete(DirectoryPath + "\\1.csv");
                    return true;
                }
            }
            catch { }
            return false;
        }
        public string validPrinterConnection(string sPrinterIP)
        {
            string sResult = string.Empty;
            try
            {
                string chkPrinterStatus = string.Empty;
                if (PCommon.sUseNetworkPrinter == "1")
                {
                    if (CommonHelper.sBYPassLogin == "1")
                    {
                        chkPrinterStatus = "PRINTER READY";
                    }
                    else
                    {
                        BcilNetwork _BcilNetwork = new BcilNetwork();
                        _BcilNetwork.PrinterIP = sPrinterIP;
                        _BcilNetwork.PrinterPort = Convert.ToInt32(PCommon.sPrinterPort);
                        try
                        {
                            chkPrinterStatus = _BcilNetwork.NetworkPrinterStatus();
                        }
                        catch (Exception)
                        {
                            chkPrinterStatus = "PRINTERNOTCONNECTED~Printer Not connected";
                        }
                    }
                }
                else
                {
                    string sPath = AppDomain.CurrentDomain.BaseDirectory;
                    string sApplicationPrintingFilePath = sPath + PCommon.sServerSidePrintingPath;
                    if (!Directory.Exists(sApplicationPrintingFilePath))
                    {
                        Directory.CreateDirectory(sApplicationPrintingFilePath);
                    }
                    if (!DirectoryHasPermission(sApplicationPrintingFilePath))
                    {
                        chkPrinterStatus = "N~Selected directory have not permission";
                        return chkPrinterStatus;
                    }
                    chkPrinterStatus = "PRINTER READY";
                }
                sResult = chkPrinterStatus;
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }
        public string sPrintDataLabel(string sPrinterIP, string sPRN, string sBarcode, string sType
            , string sUserID, string sLineCode
            )
        {
            string sResult = string.Empty;
            try
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, MethodBase.GetCurrentMethod().Name,
                    "Label Printing Method Called for Type :" + sType + ", and Barcode : " + sBarcode +
                    ", User ID :" + sUserID + ", and Line Code : " + sLineCode
                    );
                sBarcode = sBarcode.Replace('/', '-');
                sBarcode = sBarcode.Replace(':', '-');
                sBarcode = sBarcode.Replace('@', '-');
                sBarcode = sBarcode.Replace(';', '-');
                sBarcode = sBarcode.Replace('#', '-');
                sBarcode = sBarcode.Replace('*', '-');
                sBarcode = sBarcode.Replace('{', '-');
                sBarcode = sBarcode.Replace('}', '-');
                sBarcode = sBarcode.Replace('?', '-');
                string chkPrinterStatus = string.Empty;
                if (PCommon.sUseNetworkPrinter == "1")
                {
                    if (CommonHelper.sBYPassLogin == "1")
                    {
                        chkPrinterStatus = "SUCCESS";
                    }
                    else
                    {
                        BcilNetwork _BcilNetwork = new BcilNetwork();
                        _BcilNetwork.PrinterIP = sPrinterIP;
                        _BcilNetwork.PrinterPort = Convert.ToInt32(PCommon.sPrinterPort);
                        _BcilNetwork.Prn = sPRN;
                        chkPrinterStatus = _BcilNetwork.NetworkPrint();
                    }
                }
                else
                {
                    string sPath = AppDomain.CurrentDomain.BaseDirectory;
                    string sFileName = string.Empty;
                    sFileName = "BCI_" + sUserID + "_" + sLineCode + "_" + PCommon.sSiteCode + "_" + sType.Trim() + "_" + sBarcode.Split('#')[0] + ".prn";
                    string sApplicationPrintingFullFilePath = sPath + PCommon.sServerSidePrintingPath + "\\" + sFileName;

                    if (sType.ToUpper() != "BOX")
                    {
                        if (File.Exists(sApplicationPrintingFullFilePath))
                        {
                            PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, MethodBase.GetCurrentMethod().Name, "Duplicate File Found at Path : " + sApplicationPrintingFullFilePath);
                            File.Delete(sApplicationPrintingFullFilePath);
                        }
                    }
                    PCommon.sFilePrintingPath = sPath + PCommon.sServerSidePrintingPath;
                    FileStream fParameter = new FileStream(sApplicationPrintingFullFilePath, FileMode.Create, FileAccess.Write);
                    StreamWriter m_WriterParameter = new StreamWriter(fParameter);
                    string sFileCreated = string.Empty;
                    try
                    {

                        m_WriterParameter.BaseStream.Seek(0, SeekOrigin.End);
                        m_WriterParameter.Write(sPRN);
                        sFileCreated = "1";
                    }
                    catch (Exception ex)
                    {
                        chkPrinterStatus = "ERROR~" + ex.Message.ToString();
                        sFileCreated = "0";
                    }
                    finally
                    {
                        m_WriterParameter.Flush();
                        m_WriterParameter.Close();
                    }
                    try
                    {
                        if (sFileCreated == "1")
                        {
                            System.IO.FileInfo file = new System.IO.FileInfo(sApplicationPrintingFullFilePath);
                            if (file.Exists)
                            {
                                PCommon.sFileNam = sFileName;
                                HttpResponse Response = HttpContext.Current.Response;
                                HttpRequest Request = HttpContext.Current.Request;
                                string sProjectPublishFolder = ConfigurationManager.AppSettings["PUBLISHFOLDER"].ToString();
                                string sPathData = string.Format("{0}://{1}", Request.Url.Scheme, Request.Url.Authority);
                                string sRedirectValue = sPathData + @"/INE/PrintLabel.aspx?Filename=" + PCommon.sFileNam;
                                if (!sPathData.Contains("localhost"))
                                {
                                    string sFinalPath = string.Format("{0}://{1}", Request.Url.Scheme, Request.Url.Authority);
                                    if (sProjectPublishFolder.Trim().Length > 0)
                                    {
                                        sRedirectValue = sFinalPath + @"/" + sProjectPublishFolder + "/INE/PrintLabel.aspx?Filename=" + PCommon.sFileNam;
                                    }
                                    else
                                    {
                                        sRedirectValue = sFinalPath + @"/INE/PrintLabel.aspx?Filename=" + PCommon.sFileNam;
                                    }
                                    PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, 
                                        MethodBase.GetCurrentMethod().Name, sRedirectValue);
                                }


                                StringBuilder sb = new StringBuilder();
                                sb.Append("<script type = 'text/javascript'>");
                                sb.Append(" var win = window.open('");
                                sb.Append(sRedirectValue);
                                sb.Append("'");
                                sb.Append(", '_blank'");
                                sb.Append(",'top=0, left=0, width=100, height=100, menubar=no,toolbar=no,status=1,resizable=no");
                                sb.Append("');");
                                //sb.Append("  setTimeout(function() { win.close();}, 4000); ");                               
                                sb.Append(" win.focus(); ");
                                sb.Append("  win.onblur = function() { win.close(); };");
                                sb.Append("</script>");
                                Page page = (Page)(HttpContext.Current.Handler);
                                page.ClientScript.RegisterStartupScript(this.GetType(),
                                        "script", sb.ToString());
                                chkPrinterStatus = "SUCCESS";
                            }
                            else
                            {
                                chkPrinterStatus = "ERROR~Printing Failed";
                            }
                        }
                        else
                        {
                            chkPrinterStatus = "ERROR~Printing Failed";
                        }
                    }
                    catch (Exception ex)
                    {
                        chkPrinterStatus = "ERROR~" + ex.Message.ToString();
                    }
                }
                if (chkPrinterStatus == "SUCCESS")
                {
                    
                    sType = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(sType.ToLower());
                    sResult = "SUCCESS~ " + sType + " label printed successfully.";
                }
                else if (chkPrinterStatus.Length == 0)
                {
                    sResult = "N~Printer not found, Printing failed but data saved, Reel Barcode : " + sBarcode;
                }
                else if (chkPrinterStatus == "PRINTER NOT IN NETWORK")
                {
                    sResult = "N~Printer not found, Printing failed but data saved, Reel Barcode : " + sBarcode;
                }
                else
                {
                    sResult = chkPrinterStatus;
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }
        public string sRPrintDataLabel(string sPrinterIP, string sPRN, string sBarcode, string sType
            , string sUserID, string sLineCode, string ddlReasonofReprint,string txtRemarks,string _sPrintingType
            )
        {
            string sResult = string.Empty;
            try
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, MethodBase.GetCurrentMethod().Name,
                    "Label Printing Method Called for Type :" + sType + ", and Barcode : " + sBarcode +
                    ", User ID :" + sUserID + ", and Line Code : " + sLineCode
                    );
                sBarcode = sBarcode.Replace('/', '-');
                sBarcode = sBarcode.Replace(':', '-');
                sBarcode = sBarcode.Replace('@', '-');
                sBarcode = sBarcode.Replace(';', '-');
                sBarcode = sBarcode.Replace('#', '-');
                sBarcode = sBarcode.Replace('*', '-');
                sBarcode = sBarcode.Replace('{', '-');
                sBarcode = sBarcode.Replace('}', '-');
                sBarcode = sBarcode.Replace('?', '-');
                string chkPrinterStatus = string.Empty;
                if (PCommon.sUseNetworkPrinter == "1")
                {
                    if (CommonHelper.sBYPassLogin == "1")
                    {
                        chkPrinterStatus = "SUCCESS";
                    }
                    else
                    {
                        BcilNetwork _BcilNetwork = new BcilNetwork();
                        _BcilNetwork.PrinterIP = sPrinterIP;
                        _BcilNetwork.PrinterPort = Convert.ToInt32(PCommon.sPrinterPort);
                        _BcilNetwork.Prn = sPRN;
                        chkPrinterStatus = _BcilNetwork.NetworkPrint();
                    }
                }
                else
                {
                    string sPath = AppDomain.CurrentDomain.BaseDirectory;
                    string sFileName = string.Empty;
                    sFileName = "BCI_" + sUserID + "_" + sLineCode + "_" + PCommon.sSiteCode + "_" + sType.Trim() + "_" + sBarcode.Split('#')[0] + ".prn";
                    string sApplicationPrintingFullFilePath = sPath + PCommon.sServerSidePrintingPath + "\\" + sFileName;

                    if (sType.ToUpper() != "BOX")
                    {
                        if (File.Exists(sApplicationPrintingFullFilePath))
                        {
                            PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, MethodBase.GetCurrentMethod().Name, "Duplicate File Found at Path : " + sApplicationPrintingFullFilePath);
                            File.Delete(sApplicationPrintingFullFilePath);
                        }
                    }
                    PCommon.sFilePrintingPath = sPath + PCommon.sServerSidePrintingPath;
                    FileStream fParameter = new FileStream(sApplicationPrintingFullFilePath, FileMode.Create, FileAccess.Write);
                    StreamWriter m_WriterParameter = new StreamWriter(fParameter);
                    string sFileCreated = string.Empty;
                    try
                    {

                        m_WriterParameter.BaseStream.Seek(0, SeekOrigin.End);
                        m_WriterParameter.Write(sPRN);
                        sFileCreated = "1";
                    }
                    catch (Exception ex)
                    {
                        chkPrinterStatus = "ERROR~" + ex.Message.ToString();
                        sFileCreated = "0";
                    }
                    finally
                    {
                        m_WriterParameter.Flush();
                        m_WriterParameter.Close();
                    }
                    try
                    {
                        if (sFileCreated == "1")
                        {
                            System.IO.FileInfo file = new System.IO.FileInfo(sApplicationPrintingFullFilePath);
                            if (file.Exists)
                            {
                                PCommon.sFileNam = sFileName;
                                HttpResponse Response = HttpContext.Current.Response;
                                HttpRequest Request = HttpContext.Current.Request;
                                string sProjectPublishFolder = ConfigurationManager.AppSettings["PUBLISHFOLDER"].ToString();
                                string sPathData = string.Format("{0}://{1}", Request.Url.Scheme, Request.Url.Authority);
                                string sRedirectValue = sPathData + @"/INE/PrintLabel.aspx?Filename=" + PCommon.sFileNam;
                                if (!sPathData.Contains("localhost"))
                                {
                                    string sFinalPath = string.Format("{0}://{1}", Request.Url.Scheme, Request.Url.Authority);
                                    if (sProjectPublishFolder.Trim().Length > 0)
                                    {
                                        sRedirectValue = sFinalPath + @"/" + sProjectPublishFolder + "/INE/PrintLabel.aspx?Filename=" + PCommon.sFileNam;
                                    }
                                    else
                                    {
                                        sRedirectValue = sFinalPath + @"/INE/PrintLabel.aspx?Filename=" + PCommon.sFileNam;
                                    }
                                    PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,
                                        MethodBase.GetCurrentMethod().Name, sRedirectValue);
                                }


                                StringBuilder sb = new StringBuilder();
                                sb.Append("<script type = 'text/javascript'>");
                                sb.Append(" var win = window.open('");
                                sb.Append(sRedirectValue);
                                sb.Append("'");
                                sb.Append(", '_blank'");
                                sb.Append(",'top=0, left=0, width=100, height=100, menubar=no,toolbar=no,status=1,resizable=no");
                                sb.Append("');");
                                //sb.Append("  setTimeout(function() { win.close();}, 4000); ");                               
                                sb.Append(" win.focus(); ");
                                sb.Append("  win.onblur = function() { win.close(); };");
                                sb.Append("</script>");
                                Page page = (Page)(HttpContext.Current.Handler);
                                page.ClientScript.RegisterStartupScript(this.GetType(),
                                        "script", sb.ToString());
                                chkPrinterStatus = "SUCCESS";
                            }
                            else
                            {
                                chkPrinterStatus = "ERROR~Printing Failed";
                            }
                        }
                        else
                        {
                            chkPrinterStatus = "ERROR~Printing Failed";
                        }
                    }
                    catch (Exception ex)
                    {
                        chkPrinterStatus = "ERROR~" + ex.Message.ToString();
                    }
                }
                if (chkPrinterStatus == "SUCCESS")
                {
                    BL_ReprintReasonMaster blobj = new BL_ReprintReasonMaster();
                    var data = blobj.SaveReprintData(_sPrintingType, sPrinterIP, sBarcode, sUserID, ddlReasonofReprint, txtRemarks, _sPrintingType);
                    sType = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(sType.ToLower());
                    sResult = "SUCCESS~ " + sType + " label printed successfully.";
                }
                else if (chkPrinterStatus.Length == 0)
                {
                    sResult = "N~Printer not found, Printing failed but data saved, Reel Barcode : " + sBarcode;
                }
                else if (chkPrinterStatus == "PRINTER NOT IN NETWORK")
                {
                    sResult = "N~Printer not found, Printing failed but data saved, Reel Barcode : " + sBarcode;
                }
                else
                {
                    sResult = chkPrinterStatus;
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }
    }
}
