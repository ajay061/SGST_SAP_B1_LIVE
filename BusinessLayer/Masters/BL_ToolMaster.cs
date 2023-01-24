using BusinessLayer;
using Common;
using DataLayer;
using System;
using System.Data;
using System.Text;
public class BL_ToolMaster
{
    DL_ToolMaster dlboj = null;
    public DataTable GetToolID(string sSiteCode)
    {
        DataTable dtTools = new DataTable();
        dlboj = new DL_ToolMaster();
        try
        {
            dtTools = dlboj.BindTool(sSiteCode);
        }
        catch (Exception ex)
        {
            PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            throw ex;
        }
        return dtTools;
    }
    public string SaveToolID(string toolid, string decription, string type, string qty, string createdBy, string sSiteCode
        ,string sMake,string sModel,string sEquipSrno,string sUsageRange,string sAccuracy,DateTime dCalibrationDate,DateTime dAlertDate
        )
    {
        string sResult = string.Empty;
        dlboj = new DL_ToolMaster();
        try
        {
            DataTable dtTools = dlboj.SaveTool(toolid, decription, type, qty, createdBy, sSiteCode
                , sMake,sModel,sEquipSrno,sUsageRange,sAccuracy,dCalibrationDate,dAlertDate
                );
            if (dtTools.Rows.Count > 0)
            {
                sResult = dtTools.Rows[0][0].ToString();
            }
            else
            {
                sResult = "N~Data Saved Failed";
            }
        }
        catch (Exception ex)
        {
            PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            throw ex;
        }
        return sResult;
    }
    public string UpdateTool(string toolid, string decription, string type, string qty, string createdBy, string sSiteCode,
        string sMake, string sModel, string sEquipSrno, string sUsageRange, string sAccuracy, DateTime dCalibrationDate, DateTime dAlertDate)
    {
        string sResult = string.Empty;
        dlboj = new DL_ToolMaster();
        try
        {
            DataTable dtTools = dlboj.UpdateTool(toolid, decription, type, qty, createdBy,sSiteCode
                 , sMake, sModel, sEquipSrno, sUsageRange, sAccuracy, dCalibrationDate, dAlertDate
                );
            if (dtTools.Rows.Count > 0)
            {
                sResult = dtTools.Rows[0][0].ToString();
            }
            else
            {
                sResult = "N~Data updatation Failed";
            }
        }
        catch (Exception ex)
        {
            PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            throw ex;
        }
        return sResult;
    }
    public string DeleteTool(string _SN, string sSiteCode)
    {
        string sResult = string.Empty;
        dlboj = new DL_ToolMaster();
        try
        {
            DataTable dtTools = dlboj.DeleteTool(_SN,sSiteCode);
            if (dtTools.Rows.Count > 0)
            {
                sResult = dtTools.Rows[0][0].ToString();
            }
            else
            {
                sResult = "N~Data deletion Failed";
            }
        }
        catch (Exception ex)
        {
            PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            throw ex;
        }
        return sResult;
    }
    public DataTable GetSeletedData(string _SN, string sSiteCode)
    {
        DataTable dt = new DataTable();
        dlboj = new DL_ToolMaster();
        try
        {
            dt = dlboj.FillToolDetails(_SN, sSiteCode);
        }
        catch (Exception ex)
        {
            PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            throw ex;
        }
        return dt;
    }
    public string ToolBinPrinting(string ID, string PortNo, string sPrinterIP, string sDesc, string sType,
        string bQtyUpdateRequired, string sUserID, string sLineCode, string sSiteCode

        )
    {
        string sPrintingResult = string.Empty;
        dlboj = new DL_ToolMaster();
        BL_Common objBL_Common = new BL_Common();
        try
        {
            string chkPrinterStatus = string.Empty;
            chkPrinterStatus = objBL_Common.validPrinterConnection(sPrinterIP);
            string sPart_Barcode = string.Empty;
            string sPRN = string.Empty;
            if (chkPrinterStatus == "PRINTER READY")
            {
                DataTable dt = objBL_Common.GetPRN("ID");
                if (dt.Rows.Count > 0)
                {
                    sPRN = dt.Rows[0][0].ToString();
                    string sPRNPrintingResult = string.Empty;
                    if (sType == "Tool")
                    {
                        if (bQtyUpdateRequired == "Yes")
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine(" UPDATE WIP_SET_TOOLID SET USED_QTY = 0 WHERE TOOL_ID = '" + ID + "' AND SITECODE  = '" + sSiteCode + "'");
                            int iToolPrintResult = dlboj.SaveTool(sb);
                            if (iToolPrintResult > 0)
                            {
                                sPRNPrintingResult = objBL_Common.ALLPrinting(sPRN, ID, sDesc, "", "Tool Name", "Tool Code");
                            }
                            else
                            {
                                sPrintingResult = "N~Data Not updated, Tool printing failed.";
                            }
                        }
                        else
                        {
                            sPRNPrintingResult = objBL_Common.ALLPrinting(sPRN, ID, sDesc, "", "Tool Name", "Tool Code");
                        }
                    }
                    if (sPRNPrintingResult.Trim().Length == 0)
                    {
                        sPrintingResult = "N~Printer not found, Tool printing failed.";
                    }
                    else
                    {
                        sPrintingResult = objBL_Common.sPrintDataLabel(sPrinterIP, sPRNPrintingResult, ID, "Tool"
                           , sUserID, sLineCode
                            );
                    }
                }
                else
                {
                    sPrintingResult = "PRNNOTFOUND~Prn for Location printing is not available.";
                }

            }
            else
            {
                sPrintingResult = chkPrinterStatus;
            }
        }
        catch (Exception ex)
        {
            PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            throw ex;
        }
        return sPrintingResult;
    }

    public DataTable UploadTool(DataTable dtUpload,string sUserID)
    {
        DataTable dt = new DataTable();
        dlboj = new DL_ToolMaster();
        try
        {
            dt = dlboj.UploadData(dtUpload, sUserID);
        }
        catch (Exception ex)
        {
            PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            throw ex;
        }
        return dt;
    }
}

