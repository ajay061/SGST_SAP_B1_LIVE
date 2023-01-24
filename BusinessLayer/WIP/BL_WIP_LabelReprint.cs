using System;
using System.Data;
using DataLayer.WIP;
using Common;
using System.Reflection;
using PL;

namespace BusinessLayer.WIP
{
    public class BL_WIP_LabelReprint
    {
        DL_WIP_LABEL_PRINTING dlobj = null;
        public DataTable BindINELPartNo(string sType)
        {
            DataTable dtINELPartNo = new DataTable();
            dlobj = new DL_WIP_LABEL_PRINTING();
            try
            {
                dtINELPartNo = dlobj.BINDINEL_PARTNO(sType);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtINELPartNo;
        }
        public DataTable BindReelBarcode(string sItemCode, string sType)
        {
            DataTable dtReelBarcode = new DataTable();
            dlobj = new DL_WIP_LABEL_PRINTING();
            try
            {
                dtReelBarcode = dlobj.BindReelBarcode(sType, sItemCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtReelBarcode;
        }
        public string Reprint(string _PartCode, string _ReelID, string sPrintedBy, decimal dUpdatedQty, string sPrinterIP,
        string _sPrintingType, string sCustomer, string sFGItemcode, string sPartCode, string sISMobilePCB
            , string sModelName, string sLineCode,string ddlReasonofReprint, string txtRemarks
            )
        {
            dlobj = new DL_WIP_LABEL_PRINTING();
            string sResult = string.Empty;
            string sPRN = string.Empty;
            BL_Common objBL_Common = new BL_Common();
            try
            {
                string chkPrinterStatus = string.Empty;
                chkPrinterStatus = objBL_Common.validPrinterConnection(sPrinterIP);
                if (chkPrinterStatus == "PRINTER READY")
                {
                    DataTable dt = new DataTable();
                    if (_sPrintingType == "COMPONENT")
                    {
                        dt = objBL_Common.GetPRN("RM");
                    }
                    else if (_sPrintingType == "FG ASSEMBLY")
                    {
                        dt = dlobj.GetPrn(PCommon.sSiteCode, sCustomer, sFGItemcode, "FG Assembly");
                    }
                    else if (_sPrintingType == "PRIMARY BOX")
                    {
                        dt = dlobj.GetPrn(PCommon.sSiteCode, sCustomer, sFGItemcode, "PRIMARY PACKING");
                    }
                    else if (_sPrintingType == "SECONDARY BOX")
                    {
                        dt = dlobj.GetPrn(PCommon.sSiteCode, sCustomer, sFGItemcode, "SECONDARY PACKING");
                    }
                    else if (_sPrintingType == "UNIT LABEL")
                    {
                        dt = dlobj.GetPrn(PCommon.sSiteCode, sCustomer, sFGItemcode, "UNIT LABEL");
                    }
                    else if (_sPrintingType == "STAND LABEL")
                    {
                        dt = dlobj.GetPrn(PCommon.sSiteCode, sCustomer, sFGItemcode, "STAND LABEL");
                    }
                    else if (_sPrintingType == "UNIT GB LABEL")
                    {
                        dt = dlobj.GetPrn(PCommon.sSiteCode, sCustomer, sFGItemcode, "GB LABEL");
                    }
                    else if (_sPrintingType == "SECONDARY BOX")
                    {
                        dt = dlobj.GetPrn(PCommon.sSiteCode, sCustomer, sFGItemcode, "SECONDARY PACKING");
                    }
                    if (dt.Rows.Count > 0)
                    {
                        try
                        {
                            sPRN = dt.Rows[0][0].ToString();
                            string sPRNPrintingResult = string.Empty;
                            if (_sPrintingType == "COMPONENT")
                            {
                                sPRNPrintingResult = objBL_Common.ComponentPrinting(sPRN, _PartCode, _ReelID, Convert.ToInt32(dUpdatedQty)
                                      , "", 0, "", _ReelID);
                            }
                            else if (_sPrintingType == "FG ASSEMBLY")
                            {
                                sPRNPrintingResult = objBL_Common.WIP_BARCODE_LABEL(sPRN, _ReelID);
                            }
                            else if (_sPrintingType == "STAND LABEL" || _sPrintingType == "UNIT LABEL"
                                || _sPrintingType == "UNIT GB LABEL"
                                )
                            {
                                BL_MobCommon obj = new BL_MobCommon();
                                PL_Printing plobj = new PL_Printing();
                                plobj.sModelName = sModelName;
                                plobj.sBarcodestring = _ReelID;
                                plobj.sUserID = sPrintedBy;
                                plobj.sLineCode = sLineCode;
                                sPRNPrintingResult = obj.RPrintingLogic(plobj, sPRN, 1, "Unit", ddlReasonofReprint, txtRemarks, _sPrintingType);
                            }
                            else if (_sPrintingType == "PRIMARY BOX")
                            {
                                for (int i = 0; i < 2; i++)
                                {
                                    if (sISMobilePCB == "1")
                                    {
                                        BL_MobCommon obj = new BL_MobCommon();
                                        PL_Printing plobj = new PL_Printing();
                                        plobj.sModelName = sModelName;
                                        plobj.sBoxId = _ReelID;
                                        plobj.sUserID = sPrintedBy;
                                        plobj.sLineCode = sLineCode;
                                        sPRNPrintingResult = obj.RPrintingLogic(plobj, sPRN, 1, "BOX", ddlReasonofReprint, txtRemarks, _sPrintingType);
                                    }
                                    else
                                    {
                                        sPRNPrintingResult = objBL_Common.PrimaryBoxPacking(sPRN, _PartCode, _ReelID.ToString(), _PartCode);
                                    }
                                }
                            }
                            else if (_sPrintingType == "SECONDARY BOX")
                            {
                                if (sISMobilePCB == "1")
                                {
                                    PL.PL_Printing plobj = new PL.PL_Printing();
                                    plobj.sModelName = sModelName;
                                    plobj.sPalletID = _ReelID;
                                    plobj.sUserID = sPrintedBy;
                                    plobj.sLineCode = sLineCode;
                                    BL_MobCommon obj = new BL_MobCommon();
                                    sPRNPrintingResult = obj.sRPalletPrint(plobj, sPRN, _ReelID, 1, ddlReasonofReprint, txtRemarks, _sPrintingType);
                                }
                                else
                                {
                                    sPRNPrintingResult = objBL_Common.SecondaryBoxPacking(
                                          sPRN, _PartCode, _ReelID.ToString());
                                }
                            }
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
                                if (sISMobilePCB == "1")
                                {
                                    sResult = sPRNPrintingResult;
                                }
                                else
                                {
                                    //sResult = objBL_Common.sPrintDataLabel(sPrinterIP, sPRNPrintingResult, _ReelID, _sPrintingType
                                    //    , sPrintedBy, sLineCode
                                    //    );
                                    //Added by Amit joshi
                                    sResult = objBL_Common.sRPrintDataLabel(sPrinterIP, sPRNPrintingResult, _ReelID, _sPrintingType
                                        , sPrintedBy, sLineCode, ddlReasonofReprint, txtRemarks, _sPrintingType
                                        );
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, Assembly.GetExecutingAssembly().GetName() + "::" + MethodBase.GetCurrentMethod().Name, ex.Message);
                            sResult = "N~Printing Failed";
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

        #region PCB Label printing
        public DataTable BindFGItemCode()
        {
            DataTable dtINELPartNo = new DataTable();
            dlobj = new DL_WIP_LABEL_PRINTING();
            try
            {
                dtINELPartNo = dlobj.BindFGItemCode();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtINELPartNo;
        }

        public DataTable GetPageLabelCount(string sFGItemCode)
        {
            DataTable dtPartNo = new DataTable();
            dlobj = new DL_WIP_LABEL_PRINTING();
            try
            {
                dtPartNo = dlobj.GetPageLabelCount(sFGItemCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtPartNo;
        }
        public DataTable BindPrintedWorkOrderNo(string sFGItemCode)
        {
            DataTable dtINELPartNo = new DataTable();
            dlobj = new DL_WIP_LABEL_PRINTING();
            try
            {
                dtINELPartNo = dlobj.BindWorkOrderNo(sFGItemCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtINELPartNo;
        }
        public DataTable BindPrintedReels(string sFGItemCode, string sWorkOrderNo)
        {
            DataTable dtINELPartNo = new DataTable();
            dlobj = new DL_WIP_LABEL_PRINTING();
            try
            {
                dtINELPartNo = dlobj.BindPrintedReels(sFGItemCode, sWorkOrderNo);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtINELPartNo;
        }
        public DataTable GetData(string sReelBarcode)
        {
            DataTable dtINELPartNo = new DataTable();
            dlobj = new DL_WIP_LABEL_PRINTING();
            try
            {
                dtINELPartNo = dlobj.GetPrintedData(sReelBarcode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtINELPartNo;
        }
        public string blPrintLabel(string _ReelID, string sPrinterIP, string sPartCode
            , string sCustomer, string sSiteCode, string sFGItemCode, string sUserID,
            string sLineCode, string sTMOProductNo
            )
        {
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
                                sResult = objBL_Common.sPrintDataLabel(sPrinterIP
                                       , sPRNPrintingResult, _ReelID.Replace('/', '~'), "WIPPCBLABEL"
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
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, MethodBase.GetCurrentMethod().Name, ex.Message);
                sResult = "ERROR~" + ex.Message;
            }
            return sResult;
        }
        #endregion
    }
}
