using Common;
using DataLayer.WIP;
using PL;
using System;
using System.Data;

namespace BusinessLayer.WIP
{
    public class BL_WIPFGRollback
    {
        DL_WIPFGRollback dlobj;
        public DataTable ScannedBox(string sScannedBox)
        {
            dlobj = new DL_WIPFGRollback();
            return dlobj.ScanBoxID(sScannedBox);
        }
        public string RollbackBox(PL_Printing plobj)
        {
            DL_WIP_FG_Packing dlobjPacking = new DL_WIP_FG_Packing();
            dlobj = new DL_WIPFGRollback();
            string sResult = string.Empty;
            string sPRN = string.Empty;
            BL_Common objBL_Common = new BL_Common();
            try
            {
                string chkPrinterStatus = string.Empty;
                chkPrinterStatus = objBL_Common.validPrinterConnection(plobj.sPrinterIP);
                if (chkPrinterStatus == "PRINTER READY")
                {
                    DataTable dt = dlobjPacking.GetPrn(plobj);
                    if (dt.Rows.Count > 0)
                    {
                        DataTable dtResult = dlobj.RollbackBox(plobj.sBoxId, plobj.sUserID, plobj.dPackingDetail,plobj.sLineCode);
                        if (dtResult.Rows.Count > 0)
                        {
                            if (dtResult.Rows[0][0].ToString().StartsWith("SUCCESS~"))
                            {
                                string sBoxID = string.Empty;
                                try
                                {
                                    sBoxID = dtResult.Rows[0][0].ToString().Split('~')[2];
                                    sPRN = dt.Rows[0][0].ToString();
                                    if (plobj.sScanType != "PCB")
                                    {
                                        BL_MobCommon obj = new BL_MobCommon();
                                        plobj.sModelName = dtResult.Rows[0][0].ToString().Split('~')[3];
                                        plobj.sBoxId = sBoxID;
                                        sResult = obj.PrintingLogic(plobj, sPRN, 1, "BOX");
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
                                            for (int i = 0; i < 2; i++)
                                            {
                                                sResult = objBL_Common.sPrintDataLabel(plobj.sPrinterIP, sPRNPrintingResult, sBoxID, "BOX"
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
    }
}
