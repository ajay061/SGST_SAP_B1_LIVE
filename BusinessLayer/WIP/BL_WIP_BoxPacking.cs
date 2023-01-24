using Common;
using System;
using System.Data;
using DataLayer.WIP;
using PL;
using System.Reflection;

namespace BusinessLayer.WIP
{
    public class BL_WIP_BoxPacking
    {
        DL_WIP_FG_Packing dlobj;

        public DataTable BindFGItemCode(string sSiteCode,string sScanType)
        {
            DataTable dtFG = new DataTable();
            dlobj = new DL_WIP_FG_Packing();
            try
            {
                dtFG = dlobj.BindFGItemCode(sSiteCode, sScanType);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtFG;
        }
        public DataTable BindWorkOrderNo(string sSiteCode, string sFGItemCode)
        {
            DataTable dtFG = new DataTable();
            dlobj = new DL_WIP_FG_Packing();
            try
            {
                dtFG = dlobj.BindWorkOrderNo(sSiteCode, sFGItemCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtFG;
        }
        public DataTable blGetWODetails(PL_Printing plobj)
        {
            DataTable dt = new DataTable();
            try
            {
                dlobj = new DL_WIP_FG_Packing();
                dt = dlobj.GetWODetails(plobj);
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        public DataTable blGetModelDetails(PL_Printing plobj)
        {
            DataTable dt = new DataTable();
            try
            {
                dlobj = new DL_WIP_FG_Packing();
                dt = dlobj.GetModelDetails(plobj);
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        public DataTable blBindPurchaseOrder(PL_Printing plobj)
        {
            DataTable dt = new DataTable();
            try
            {
                dlobj = new DL_WIP_FG_Packing();
                dt = dlobj.BindPurchaseOrder(plobj);
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        public DataSet BindCustomerCode(string sFGItemCode, out string sResult, string sSiteCode)
        {
            DataSet dtFG = new DataSet();
            dlobj = new DL_WIP_FG_Packing();
            try
            {
                dtFG = dlobj.BindCustomerCode(sFGItemCode, sSiteCode);
                if (dtFG.Tables.Count > 0)
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
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtFG;
        }
        public DataTable GetFGDetails(string sFGItemCode, string sCustomerCode, string sSiteCode,
            string sLineCode, string sUserID)
        {
            DataTable dtFG = new DataTable();
            dlobj = new DL_WIP_FG_Packing();
            try
            {
                dtFG = dlobj.GetDetails(sFGItemCode, sCustomerCode
                    , sSiteCode, sLineCode, sUserID
                    );
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtFG;
        }
        public DataTable GetCustomerDetails(string sFGItemCode, string sCustomerCode, out string sResult
            , string sSiteCode
            )
        {
            DataTable dtFG = new DataTable();
            dlobj = new DL_WIP_FG_Packing();
            try
            {
                dtFG = dlobj.GetCustomerDetails(sFGItemCode, sCustomerCode, sSiteCode);
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
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtFG;
        }
        public string sScanBarcode(PL_Printing plobj)
        {
            dlobj = new DL_WIP_FG_Packing();
            string sResult = string.Empty;
            try
            {
                DataTable dt = dlobj.ScanBarcode(plobj);
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

        public DataTable blGetPickedImei(PL_Printing plobj)
        {
            DataTable _dsPickedImei = new DataTable();
            try
            {
                dlobj = new DL_WIP_FG_Packing();
                _dsPickedImei = dlobj.dlGetPickedImei(plobj);
            }
            catch (Exception)
            {
                throw;
            }
            return _dsPickedImei;
        }

        public string PrintBoxID(PL_Printing plobj)
        {
            dlobj = new DL_WIP_FG_Packing();
            string sResult = string.Empty;
            string sPRN = string.Empty;
            BL_Common objBL_Common = new BL_Common();
            try
            {
                string chkPrinterStatus = string.Empty;
                chkPrinterStatus = objBL_Common.validPrinterConnection(plobj.sPrinterIP);
                if (chkPrinterStatus == "PRINTER READY")
                {
                    DataTable dt = dlobj.GetPrn(plobj);
                    if (dt.Rows.Count > 0)
                    {
                        DataTable dtResult = dlobj.PrintBarcode(plobj);
                        if (dtResult.Rows.Count > 0)
                        {
                            if (dtResult.Rows[0][0].ToString().StartsWith("SUCCESS~"))
                            {
                                string sBoxID = string.Empty;
                                try
                                {
                                    sBoxID = dtResult.Rows[0][0].ToString().Split('~')[2];
                                    sPRN = dt.Rows[0][0].ToString();
                                    int iNoOfLabels = 1;
                                    iNoOfLabels = Convert.ToInt32(dt.Rows[0][1].ToString());
                                    PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,
                                        MethodBase.GetCurrentMethod().Name, "Box ID :" + sBoxID + ", and No Of Prints : " + iNoOfLabels.ToString());
                                    if (plobj.sScanType != "PCB")
                                    {
                                        BL_MobCommon obj = new BL_MobCommon();
                                        plobj.sModelName = dtResult.Rows[0][0].ToString().Split('~')[3];
                                        plobj.sBoxId = sBoxID;
                                        sResult = obj.PrintingLogic(plobj, sPRN, iNoOfLabels, "BOX");
                                    }
                                    else
                                    {

                                        string sPRNPrintingResult = objBL_Common.PrimaryBoxPacking(sPRN, plobj.sBOMCode, sBoxID, "BOX");
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
                                                sResult = objBL_Common.sPrintDataLabel(plobj.sPrinterIP, sPRNPrintingResult, sBoxID.Replace('/', 'P') + "_" + i.ToString(), "BOX"
                                                    , plobj.sUserID, plobj.sLineCode
                                                    );
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                                    sResult = "N~Label Printing Failed, Box ID :" + sBoxID;
                                }
                            }
                            else
                            {
                                sResult = dtResult.Rows[0][0].ToString();
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
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }



        #region WorkOrderClose
        public DataTable BindPendingFGItemForWOClose(string sSiteCode)
        {
            DataTable dtFG = new DataTable();
            dlobj = new DL_WIP_FG_Packing();
            try
            {
                dtFG = dlobj.BindPendingFGItemCodeForWOClose(sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtFG;
        }
        public DataTable BindPendingWorkOrderForWOClose(string sFGItemCode, string sSiteCodes)
        {
            DataTable dtFG = new DataTable();
            dlobj = new DL_WIP_FG_Packing();
            try
            {
                dtFG = dlobj.BindPendingWorkOrderForWOClose(sFGItemCode, sSiteCodes);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtFG;
        }

        public DataTable WorkOrderClose(string sFGItemCode
            , string sWONo, string sRemarks, string sSiteCode, string sUserID
            )
        {
            DataTable dtFG = new DataTable();
            dlobj = new DL_WIP_FG_Packing();
            try
            {
                dtFG = dlobj.WorkOrderClose(sFGItemCode, sWONo, sRemarks, sSiteCode, sUserID);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtFG;
        }

        #endregion


    }
}
