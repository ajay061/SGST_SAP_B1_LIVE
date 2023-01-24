using Common;
using System;
using System.Data;
namespace DataLayer.WIP
{
    public class DL_WIPPCBScanning
    {
        DBManager oDbm;
        public DL_WIPPCBScanning()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }

        public DataTable ValidateMachine(string sMachineID, string sLineiD, string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "VALIDATEMACHINE");
                oDbm.AddParameters(1, "@MACHINEID", sMachineID);
                oDbm.AddParameters(2, "@LINEID", sLineiD);
                oDbm.AddParameters(3, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_BIND_PCB_SCANNING").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dt;
        }

        public DataTable BindFGItemCode(string sMachineID, string sLineiD, string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "BINDFGITEMCODE");
                oDbm.AddParameters(1, "@MACHINEID", sMachineID);
                oDbm.AddParameters(2, "@LINEID", sLineiD);
                oDbm.AddParameters(3, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_BIND_PCB_SCANNING").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dt;
        }

        public DataTable BindReworkstationID(string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDREWORKSTATION");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_BIND_PCB_SCANNING").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dt;
        }

        public DataTable BindDefect(string sMachineID, string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "BINDDEFECT");
                oDbm.AddParameters(1, "@MACHINEID", sMachineID);
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_BIND_PCB_SCANNING").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dt;
        }
        public DataTable BindDefectQClevelVerification(string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDDEFECT");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_BIND_PCB_SCANNING").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dt;
        }

        public DataTable BindINoutProcess(string sFGItemCode, string sMachineID
            , string sSiteCode, string sLineCode
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(5);
                oDbm.AddParameters(0, "@TYPE", "BINDINOUTPROCESS");
                oDbm.AddParameters(1, "@MACHINEID", sMachineID);
                oDbm.AddParameters(2, "@FGITEMCODE", sFGItemCode);
                oDbm.AddParameters(3, "@SITECODE", sSiteCode);
                oDbm.AddParameters(4, "@LINEID", sLineCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_BIND_PCB_SCANNING").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dt;
        }

        public DataTable GetWaveTool(string sFGItemCode, string sSiteCode, string sLineCode
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "GETWAVETOOL");
                oDbm.AddParameters(1, "@FGITEMCODE", sFGItemCode);
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                oDbm.AddParameters(3, "@LINEID", sLineCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_BIND_PCB_SCANNING").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dt;
        }
        public DataTable GetWaveToolCount(string sFGItemCode, string sSiteCode, string sLineCode
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "GETWAVETOOLCOUNT");
                oDbm.AddParameters(1, "@FGITEMCODE", sFGItemCode);
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                oDbm.AddParameters(3, "@LINEID", sLineCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_BIND_PCB_SCANNING").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dt;
        }

        public DataTable ValidateLoadedMaterail(string sMachineID, string sLineiD, string sSiteCode, string sFGItemCode
            , string sProgram)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(6);
                oDbm.AddParameters(0, "@TYPE", "VALIDATEMATERAIL");
                oDbm.AddParameters(1, "@MACHINEID", sMachineID);
                oDbm.AddParameters(2, "@LINEID", sLineiD);
                oDbm.AddParameters(3, "@SITECODE", sSiteCode);
                oDbm.AddParameters(4, "@FGITEMCODE", sFGItemCode);
                oDbm.AddParameters(5, "@PROGRAMSIDE", sProgram);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_BIND_PCB_SCANNING").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dt;
        }


        public DataTable ValidateToolBarcode(string sToolID, string sSiteCode, string sMachineID, string sFGItemCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(5);
                oDbm.AddParameters(0, "@TYPE", "VALIDATETOOL");
                oDbm.AddParameters(1, "@TOOLID", sToolID);
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                oDbm.AddParameters(3, "@MACHINEID", sMachineID);
                oDbm.AddParameters(4, "@FGITEMCODE", sFGItemCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_BIND_PCB_SCANNING").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dt;
        }

        public DataTable ValidatePCBBarcode(string sPartBarcode, string sLineID, string sMachineID, string FGItemCode
            , string sRepairedRequired
    , string sMachineFailureValidate, string sProcessType, string sAOIScanned, string sOutScanRequired
            , string sSiteCode,string sAgingProcess 
    )
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(10);
                oDbm.AddParameters(0, "@PART_BARCODE", sPartBarcode);
                oDbm.AddParameters(1, "@FGITEMCODE", FGItemCode);
                oDbm.AddParameters(2, "@MACHINEID", sMachineID);
                oDbm.AddParameters(3, "@PROCESSTYPE", sProcessType);
                oDbm.AddParameters(4, "@LINEID", sLineID);
                oDbm.AddParameters(5, "@MACHINEFAILUREVALIDATE", sMachineFailureValidate);
                oDbm.AddParameters(6, "@REPAIRREQUIRED", sRepairedRequired);
                oDbm.AddParameters(7, "@AOIPCBSCANNED", sAOIScanned);
                oDbm.AddParameters(8, "@SITECODE", sSiteCode);
                oDbm.AddParameters(9, "@AGINGVALIDATE", sAgingProcess);
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_WIP_VALIDATE_PCB_INOUT").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dtResult;
        }
        public DataTable SavePCBBarcode(string sScannedBarcode, string sLineID, string sMachineID, string FGItemCode
            , string sScannedBy, string sFirstMachineScanned, string PCBMasterID, string sPartCode,
             string sScannedStage, string sReworkStationID, string sRepairRequired, string sProcessType
            , int iArraySize, string sAOIPCBScanned, string sSiteCode, string sOutScanRequired, string sDefect
            , string sRefBarcode, string sRemarks, string sToolBarcode, string sPCBBarcode,string sValidateWaveTool
           
            )
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(20);
                oDbm.AddParameters(0, "@PART_BARCODE", sPCBBarcode);
                oDbm.AddParameters(1, "@FGITEMCODE", FGItemCode);
                oDbm.AddParameters(2, "@MACHINEID", sMachineID);
                oDbm.AddParameters(3, "@LINEID", sLineID);
                oDbm.AddParameters(4, "@SCANNEDBY", sScannedBy);
                oDbm.AddParameters(5, "@FIRSTMACHINESCANNED", sFirstMachineScanned);
                oDbm.AddParameters(6, "@PCB_MASTER_ID", PCBMasterID);
                oDbm.AddParameters(7, "@SCANNEDSTATGE", sScannedStage);
                oDbm.AddParameters(8, "@REWORKSTATIONID", sReworkStationID);
                oDbm.AddParameters(9, "@TOOLBARCODE", sToolBarcode);
                oDbm.AddParameters(10, "@FAILURESTATUS", sRepairRequired);
                oDbm.AddParameters(11, "@PROCESSTYPE", sProcessType);
                oDbm.AddParameters(12, "@ARRAYSIZE", iArraySize);
                oDbm.AddParameters(13, "@AOIPCBSCANNED", sAOIPCBScanned);
                oDbm.AddParameters(14, "@SITECODE", sSiteCode);
                oDbm.AddParameters(15, "@DEFECT", sDefect);
                oDbm.AddParameters(16, "@REFBARCODE", sRefBarcode);
                oDbm.AddParameters(17, "@REMARKS", sRemarks);
                oDbm.AddParameters(18, "@SCANNEDSRNO", sScannedBarcode);
                oDbm.AddParameters(19, "@WAVETOOLSTATUS", sValidateWaveTool);                
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.MethodBase.GetCurrentMethod().Name,
                   "PCB Barcode : " + sScannedBarcode + ", Ref Barcode : " + sRefBarcode + ", Machine ID :" + sMachineID);
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_WIP_SAVE_PCB_INOUT").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dtResult;
        }
        public DataTable QCLevelVerificationPCBbarcode(string sPartBarcode, string sLineID, string FGItemCode,string sDefect,string sReworkStationID,
           string sScannedBy, string sSiteCode
   )
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(7);
                oDbm.AddParameters(0, "@PART_BARCODE", sPartBarcode);
                oDbm.AddParameters(1, "@FGITEMCODE", FGItemCode);
                oDbm.AddParameters(2, "@LINEID", sLineID);
                oDbm.AddParameters(3, "@SITECODE", sSiteCode);
                oDbm.AddParameters(4, "@sScannedBy", sScannedBy);
                oDbm.AddParameters(5, "@sDefect", sDefect);
                oDbm.AddParameters(6, "@sReworkStationID", sReworkStationID);

                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_WIP_QCLevelVerification_Reject").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dtResult;
        }
        public DataTable ValidateLoaderSamplePCB(string sPartBarcode, string sLineID, string sMachineID, string FGItemCode
       , string sRepairedRequired
, string sMachineFailureValidate, string sProcessType, string sAOIScanned, string sOutScanRequired
       , string sSiteCode, string sIsSamplePCBChecked
)
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(10);
                oDbm.AddParameters(0, "@PART_BARCODE", sPartBarcode);
                oDbm.AddParameters(1, "@FGITEMCODE", FGItemCode);
                oDbm.AddParameters(2, "@MACHINEID", sMachineID);
                oDbm.AddParameters(3, "@PROCESSTYPE", sProcessType);
                oDbm.AddParameters(4, "@LINEID", sLineID);
                oDbm.AddParameters(5, "@MACHINEFAILUREVALIDATE", sMachineFailureValidate);
                oDbm.AddParameters(6, "@REPAIRREQUIRED", sRepairedRequired);
                oDbm.AddParameters(7, "@AOIPCBSCANNED", sAOIScanned);
                oDbm.AddParameters(8, "@SITECODE", sSiteCode);
                oDbm.AddParameters(9, "@sIsSamplePCBChecked", sIsSamplePCBChecked);
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_WIP_VALIDATE_LOADER_SAMPLE_PCB").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dtResult;
        }

    }
}
