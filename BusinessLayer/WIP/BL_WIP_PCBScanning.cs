using Common;
using System;
using System.Data;
using DataLayer.WIP;

namespace BusinessLayer.WIP
{
    public class BL_WIP_PCBScanning
    {
        DL_WIPPCBScanning dlobj;
        public DataTable ValidateMachine(string sMachineID, string sLineID, string sSiteCode)
        {
            DataTable dtLocation = new DataTable();
            dlobj = new DL_WIPPCBScanning();
            try
            {
                dtLocation = dlobj.ValidateMachine(sMachineID, sLineID, sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLocation;
        }
        public DataTable BindFGItemCode(string sLineID, string sMachineID, out string result, string sSiteCode)
        {
            DataTable dtFGItemCode = new DataTable();
            try
            {
                dlobj = new DL_WIPPCBScanning();
                dtFGItemCode = dlobj.BindFGItemCode(sMachineID, sLineID, sSiteCode);
                if (dtFGItemCode.Rows.Count > 0)
                {
                    result = "SUCCESS~";
                }
                else
                {
                    result = "N~No FG item code found";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtFGItemCode;
        }

        public DataTable BindReWorkStationID(out string result, string sSiteCode)
        {
            DataTable dtLineData = new DataTable();
            try
            {
                dlobj = new DL_WIPPCBScanning();
                dtLineData = dlobj.BindReworkstationID(sSiteCode);
                if (dtLineData.Rows.Count > 0)
                {
                    result = "SUCCESS~";
                }
                else
                {
                    result = "N~No details found";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLineData;
        }

        public DataTable BindDefect(string sMachineID, out string result, string sSiteCode)
        {
            DataTable dtLineData = new DataTable();
            try
            {
                dlobj = new DL_WIPPCBScanning();
                dtLineData = dlobj.BindDefect(sMachineID, sSiteCode);
                if (dtLineData.Rows.Count > 0)
                {
                    result = "SUCCESS~";
                }
                else
                {
                    result = "N~No details found";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLineData;
        }
        public DataTable BindDefectQClevelVerification(out string result,string sSiteCode)
        {
            DataTable dtLineData = new DataTable();
            try
            {
                dlobj = new DL_WIPPCBScanning();
                dtLineData = dlobj.BindDefectQClevelVerification(sSiteCode);
                if (dtLineData.Rows.Count > 0)
                {
                    result = "SUCCESS~";
                }
                else
                {
                    result = "N~No details found";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLineData;
        }
        public DataTable GetOutProces(out string result, string sFGItemCode, string sMachineID
            , string sSiteCode, string sLineCode
            )
        {
            DataTable dtLineData = new DataTable();
            try
            {
                dlobj = new DL_WIPPCBScanning();
                dtLineData = dlobj.BindINoutProcess(sFGItemCode, sMachineID
                    , sSiteCode, sLineCode
                    );
                if (dtLineData.Rows.Count > 0)
                {
                    result = "SUCCESS~";
                }
                else
                {
                    result = "N~No details found";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLineData;
        }

        public DataTable GetWaveTool(string sFGItemCode, string sSiteCode, string sLineCode
          )
        {
            DataTable dtLineData = new DataTable();
            try
            {
                dlobj = new DL_WIPPCBScanning();
                dtLineData = dlobj.GetWaveTool(sFGItemCode, sSiteCode, sLineCode);                
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLineData;
        }
        public DataTable GetWaveToolCount(string sFGItemCode, string sSiteCode, string sLineCode
         )
        {
            DataTable dtLineData = new DataTable();
            try
            {
                dlobj = new DL_WIPPCBScanning();
                dtLineData = dlobj.GetWaveToolCount(sFGItemCode, sSiteCode, sLineCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLineData;
        }

        public DataTable ValidateMaterail(string sMachineID, string sLineID, string sSiteCode, string sFGItemCode
            , string sProgram)
        {
            DataTable dtLocation = new DataTable();
            dlobj = new DL_WIPPCBScanning();
            try
            {
                dtLocation = dlobj.ValidateLoadedMaterail(sMachineID, sLineID, sSiteCode, sFGItemCode, sProgram);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLocation;
        }

        public DataTable ValidateTool(string sToolID, string sSiteCode, string sMachineID, string sFGItemCode)
        {
            DataTable dtLocation = new DataTable();
            dlobj = new DL_WIPPCBScanning();
            try
            {
                dtLocation = dlobj.ValidateToolBarcode(sToolID, sSiteCode, sMachineID, sFGItemCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLocation;
        }

        public string SavePCBbarcode(string sLineID, string sMachineID, string sFGItemCode,
            string sScannedPCBBarcode, string sScannedBy, string sReworkStationID, string sRepairRequired, string sMachineFailureValidate,
            string sProcessType, string sAOIPCBScanned, string sSiteCode, string sOutRequired, string sDefect
            , string sRefBarcode, string sRemarks, string sToolBarcode,string sValidateWaveTool,string sAgingProcess, string sIsSamplePCBChecked
            )
        {
            string sResult = string.Empty;
            DataTable dtValidatePCB = new DataTable();
            try
            {
                dlobj = new DL_WIPPCBScanning();
                if (sIsSamplePCBChecked == "1")
                {
                    dtValidatePCB = dlobj.ValidatePCBBarcode(sScannedPCBBarcode, sLineID, sMachineID, sFGItemCode, sRepairRequired, sMachineFailureValidate, sProcessType
                        , sAOIPCBScanned, sOutRequired, sSiteCode, sAgingProcess
                        );
                    if (dtValidatePCB.Rows.Count > 0)
                    {
                        PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.MethodBase.GetCurrentMethod().Name, "Scanned Barcode  :" + sScannedPCBBarcode + ", and result is :" + dtValidatePCB.Rows[0][0].ToString());
                        if (dtValidatePCB.Rows[0][0].ToString().StartsWith("ERROR~") || dtValidatePCB.Rows[0][0].ToString().StartsWith("N~"))
                        {
                            sResult = dtValidatePCB.Rows[0][0].ToString();
                        }
                        else
                        {
                            if (dtValidatePCB.Rows[0][0].ToString().StartsWith("MACHINEFAILED~"))
                            {
                                sResult = dtValidatePCB.Rows[0][0].ToString();
                                return sResult;
                            }
                            sResult = dtValidatePCB.Rows[0][0].ToString();
                            string sIsFirstTimeScanned = string.Empty;
                            string sPCBMasterID = string.Empty;
                            //string sPCBBarcode = string.Empty;
                            string sScannedStage = string.Empty;

                            int iArraySize = 1;
                            if (sResult.Split('~').Length < 4)
                            {
                                sResult = dtValidatePCB.Rows[0][0].ToString();
                                return sResult;
                            }
                            PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.MethodBase.GetCurrentMethod().Name, "Validate PCB Barcode Result :" + sResult);
                            if(sResult.StartsWith("SUCCESS~"))
                            {

                                sResult = dtValidatePCB.Rows[0][0].ToString();
                            }



                            // Commented by Ajay Patel 0912022

                            //sIsFirstTimeScanned = sResult.Split('~')[1].ToString();
                            //sPCBMasterID = sResult.Split('~')[2].ToString();
                            //sScannedStage = sResult.Split('~')[3].ToString();
                            //iArraySize = Convert.ToInt32(sResult.Split('~')[4].ToString());
                            //sPCBBarcode = sResult.Split('~')[5].ToString();
                            //if (sReworkStationID == "--Select--")
                            //{
                            //    sReworkStationID = "";
                            //}
                            //dtValidatePCB = new DataTable();
                            //dtValidatePCB = dlobj.SavePCBBarcode(sScannedPCBBarcode, sLineID, sMachineID, sFGItemCode, sScannedBy
                            //    , sIsFirstTimeScanned, sPCBMasterID, "", sScannedStage, sReworkStationID, sRepairRequired
                            //    , sProcessType, iArraySize, sAOIPCBScanned, sSiteCode, sOutRequired, sDefect
                            //    , sRefBarcode, sRemarks, sToolBarcode, sPCBBarcode, sValidateWaveTool
                            //    );
                            //if (dtValidatePCB.Rows.Count > 0)
                            //{
                            //    if (dtValidatePCB.Rows[0][0].ToString().StartsWith("ERROR~"))
                            //    {
                            //        sResult = dtValidatePCB.Rows[0][0].ToString();
                            //    }
                            //    else
                            //    {
                            //        sResult = dtValidatePCB.Rows[0][0].ToString();
                            //    }
                            //}
                            //else
                            //{
                            //    sResult = "N~No result found, Please try again";
                            //}
                        }
                    }
                    else
                    {
                        sResult = "N~No Validation result found, Please try again";
                    }
                }
                else
                {
                    dtValidatePCB = null;
                    dtValidatePCB = dlobj.ValidateLoaderSamplePCB(sScannedPCBBarcode, sLineID, sMachineID, sFGItemCode, sRepairRequired, sMachineFailureValidate, sProcessType
                        , sAOIPCBScanned, sOutRequired, sSiteCode, sIsSamplePCBChecked
                        );
                    if (dtValidatePCB.Rows[0][0].ToString().StartsWith("N~"))
                    {
                        sResult = dtValidatePCB.Rows[0][0].ToString();
                    }
                    else
                    {
                        sResult = dtValidatePCB.Rows[0][0].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }
        public string SaveRejectPCBbarcode(string sLineID, string sMachineID, string sFGItemCode,
            string sScannedPCBBarcode, string sScannedBy, string sReworkStationID, string sRepairRequired, string sMachineFailureValidate,
            string sProcessType, string sAOIPCBScanned, string sSiteCode, string sOutRequired, string sDefect
            , string sRefBarcode, string sRemarks, string sToolBarcode, string sValidateWaveTool, string sAgingProcess, string sIsSamplePCBChecked
            )
        {
            string sResult = string.Empty;
            DataTable dtValidatePCB = new DataTable();
            try
            {
                dlobj = new DL_WIPPCBScanning();
                if (sIsSamplePCBChecked == "1")
                {
                    dtValidatePCB = dlobj.ValidatePCBBarcode(sScannedPCBBarcode, sLineID, sMachineID, sFGItemCode, sRepairRequired, sMachineFailureValidate, sProcessType
                        , sAOIPCBScanned, sOutRequired, sSiteCode, sAgingProcess
                        );
                    if (dtValidatePCB.Rows.Count > 0)
                    {
                        PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.MethodBase.GetCurrentMethod().Name, "Scanned Barcode  :" + sScannedPCBBarcode + ", and result is :" + dtValidatePCB.Rows[0][0].ToString());
                        if (dtValidatePCB.Rows[0][0].ToString().StartsWith("ERROR~") || dtValidatePCB.Rows[0][0].ToString().StartsWith("N~"))
                        {
                            sResult = dtValidatePCB.Rows[0][0].ToString();
                        }
                        else
                        {
                            if (dtValidatePCB.Rows[0][0].ToString().StartsWith("MACHINEFAILED~"))
                            {
                                sResult = dtValidatePCB.Rows[0][0].ToString();
                                return sResult;
                            }
                            sResult = dtValidatePCB.Rows[0][0].ToString();
                            string sIsFirstTimeScanned = string.Empty;
                            string sPCBMasterID = string.Empty;
                            //string sPCBBarcode = string.Empty;
                            string sScannedStage = string.Empty;

                            int iArraySize = 1;
                            if (sResult.Split('~').Length < 4)
                            {
                                sResult = dtValidatePCB.Rows[0][0].ToString();
                                return sResult;
                            }
                            PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.MethodBase.GetCurrentMethod().Name, "Validate PCB Barcode Result :" + sResult);
                            //if (sResult.StartsWith("SUCCESS~"))
                            //{
                            //    return sResult.Split('~')[0].ToString();
                            //}



                            // Commented by Ajay Patel 0912022
                            sResult = dtValidatePCB.Rows[0][0].ToString();
                            string sPCBBarcode = string.Empty;
                            sIsFirstTimeScanned = sResult.Split('~')[1].ToString();
                            sPCBMasterID = sResult.Split('~')[2].ToString();
                            sScannedStage = sResult.Split('~')[3].ToString();
                            iArraySize = Convert.ToInt32(sResult.Split('~')[4].ToString());
                            sPCBBarcode = sResult.Split('~')[5].ToString();
                            if (sReworkStationID == "--Select--")
                            {
                                sReworkStationID = "";
                            }
                            dtValidatePCB = new DataTable();
                            dtValidatePCB = dlobj.SavePCBBarcode(sScannedPCBBarcode, sLineID, sMachineID, sFGItemCode, sScannedBy
                                , sIsFirstTimeScanned, sPCBMasterID, "", sScannedStage, sReworkStationID, sRepairRequired
                                , sProcessType, iArraySize, sAOIPCBScanned, sSiteCode, sOutRequired, sDefect
                                , sRefBarcode, sRemarks, sToolBarcode, sPCBBarcode, sValidateWaveTool
                                );
                            if (dtValidatePCB.Rows.Count > 0)
                            {
                                if (dtValidatePCB.Rows[0][0].ToString().StartsWith("ERROR~"))
                              {
                                   sResult = dtValidatePCB.Rows[0][0].ToString();
                               }
                               else
                               {
                                   sResult = dtValidatePCB.Rows[0][0].ToString();
                               }
                            }
                            else
                            {
                                sResult = "N~No result found, Please try again";
                            }
                        }
                    }
                    else
                    {
                        sResult = "N~No Validation result found, Please try again";
                    }
                }
                else
                {
                    dtValidatePCB = null;
                    dtValidatePCB = dlobj.ValidateLoaderSamplePCB(sScannedPCBBarcode, sLineID, sMachineID, sFGItemCode, sRepairRequired, sMachineFailureValidate, sProcessType
                        , sAOIPCBScanned, sOutRequired, sSiteCode, sIsSamplePCBChecked
                        );
                    if (dtValidatePCB.Rows[0][0].ToString().StartsWith("N~"))
                    {
                        sResult = dtValidatePCB.Rows[0][0].ToString();
                    }
                    else
                    {
                        sResult = dtValidatePCB.Rows[0][0].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }

        public string QCLevelVerificationPCBbarcode(string sLineID,  string sFGItemCode, string sDefect,
            string sReworkStationID, string sScannedPCBBarcode, string sScannedBy, string sSiteCode

           )
        {
            string sResult = string.Empty;
            DataTable dtRejectPCB = new DataTable();
            try
            {
                dlobj = new DL_WIPPCBScanning();
               
                    dtRejectPCB = dlobj.QCLevelVerificationPCBbarcode(sScannedPCBBarcode, sLineID, sFGItemCode, sDefect, sReworkStationID, sScannedBy,
                        sSiteCode
                        );
                    if (dtRejectPCB.Rows.Count > 0)
                    {
                        PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.MethodBase.GetCurrentMethod().Name, "Scanned Barcode  :" + sScannedPCBBarcode + ", and result is :" + dtRejectPCB.Rows[0][0].ToString());
                        if (dtRejectPCB.Rows[0][0].ToString().StartsWith("ERROR~") || dtRejectPCB.Rows[0][0].ToString().StartsWith("N~"))
                        {
                            sResult = dtRejectPCB.Rows[0][0].ToString();
                        }
                        else
                        {
                            
                            sResult = dtRejectPCB.Rows[0][0].ToString();
                            if (sResult.Split('~').Length < 4)
                            {
                                sResult = dtRejectPCB.Rows[0][0].ToString();
                                return sResult;
                            }
                            
                        }
                    }
                    else
                    {
                        sResult = "N~No Validation result found, Please try again";
                    }
                
                
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }

        // Ajay Patel 09122022
        public string SaveOKPCBbarcode(string sLineID, string sMachineID, string sFGItemCode,
         string sScannedPCBBarcode, string sScannedBy, string sReworkStationID, string sRepairRequired, string sMachineFailureValidate,
         string sProcessType, string sAOIPCBScanned, string sSiteCode, string sOutRequired, string sDefect
         , string sRefBarcode, string sRemarks, string sToolBarcode, string sValidateWaveTool, string sAgingProcess, string sIsSamplePCBChecked
         )
        {
            string sResult = string.Empty;
            DataTable dtValidatePCB = new DataTable();
            try
            {
                dlobj = new DL_WIPPCBScanning();
                if (sIsSamplePCBChecked == "1")
                {
                    dtValidatePCB = dlobj.ValidatePCBBarcode(sScannedPCBBarcode, sLineID, sMachineID, sFGItemCode, sRepairRequired, sMachineFailureValidate, sProcessType
                        , sAOIPCBScanned, sOutRequired, sSiteCode, sAgingProcess
                        );
                    if (dtValidatePCB.Rows.Count > 0)
                    {
                        PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.MethodBase.GetCurrentMethod().Name, "Scanned Barcode  :" + sScannedPCBBarcode + ", and result is :" + dtValidatePCB.Rows[0][0].ToString());
                        if (dtValidatePCB.Rows[0][0].ToString().StartsWith("ERROR~") || dtValidatePCB.Rows[0][0].ToString().StartsWith("N~"))
                        {
                            sResult = dtValidatePCB.Rows[0][0].ToString();
                        }
                        else
                        {
                            if (dtValidatePCB.Rows[0][0].ToString().StartsWith("MACHINEFAILED~"))
                            {
                                sResult = dtValidatePCB.Rows[0][0].ToString();
                                return sResult;
                            }
                            sResult = dtValidatePCB.Rows[0][0].ToString();
                            string sIsFirstTimeScanned = string.Empty;
                            string sPCBMasterID = string.Empty;
                            string sPCBBarcode = string.Empty;
                            string sScannedStage = string.Empty;

                            int iArraySize = 1;
                            if (sResult.Split('~').Length < 4)
                            {
                                sResult = dtValidatePCB.Rows[0][0].ToString();
                                return sResult;
                            }
                            PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.MethodBase.GetCurrentMethod().Name, "Validate PCB Barcode Result :" + sResult);
                            sIsFirstTimeScanned = sResult.Split('~')[1].ToString();
                            sPCBMasterID = sResult.Split('~')[2].ToString();
                            sScannedStage = sResult.Split('~')[3].ToString();
                            iArraySize = Convert.ToInt32(sResult.Split('~')[4].ToString());
                            sPCBBarcode = sResult.Split('~')[5].ToString();
                            if (sReworkStationID == "--Select--")
                            {
                                sReworkStationID = "";
                            }
                            dtValidatePCB = new DataTable();
                            dtValidatePCB = dlobj.SavePCBBarcode(sScannedPCBBarcode, sLineID, sMachineID, sFGItemCode, sScannedBy
                                , sIsFirstTimeScanned, sPCBMasterID, "", sScannedStage, sReworkStationID, sRepairRequired
                                , sProcessType, iArraySize, sAOIPCBScanned, sSiteCode, sOutRequired, sDefect
                                , sRefBarcode, sRemarks, sToolBarcode, sPCBBarcode, sValidateWaveTool
                                );
                            if (dtValidatePCB.Rows.Count > 0)
                            {
                                if (dtValidatePCB.Rows[0][0].ToString().StartsWith("ERROR~"))
                                {
                                    sResult = dtValidatePCB.Rows[0][0].ToString();
                                }
                                else
                                {
                                    sResult = dtValidatePCB.Rows[0][0].ToString();
                                }
                            }
                            else
                            {
                                sResult = "N~No result found, Please try again";
                            }
                        }
                    }
                    else
                    {
                        sResult = "N~No Validation result found, Please try again";
                    }
                }
                else
                {
                    dtValidatePCB = null;
                    dtValidatePCB = dlobj.ValidateLoaderSamplePCB(sScannedPCBBarcode, sLineID, sMachineID, sFGItemCode, sRepairRequired, sMachineFailureValidate, sProcessType
                        , sAOIPCBScanned, sOutRequired, sSiteCode, sIsSamplePCBChecked
                        );
                    if (dtValidatePCB.Rows[0][0].ToString().StartsWith("N~"))
                    {
                        sResult = dtValidatePCB.Rows[0][0].ToString();
                    }
                    else
                    {
                        sResult = dtValidatePCB.Rows[0][0].ToString();
                    }
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
