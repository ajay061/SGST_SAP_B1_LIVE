using Common;
using DataLayer;
using System;
using System.Data;

namespace BusinessLayer.WIP
{
    public class BL_WIP_Issue
    {
        DL_WIP_Issue dlboj = null;
        public DataTable BINDLineID(string sSiteCode)
        {
            DataTable dtGRN = new DataTable();
            dlboj = new DL_WIP_Issue();
            try
            {
                dtGRN = dlboj.BindLine(sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtGRN;
        }

        public DataTable ValidateMachine(string sMahcineID, string sSiteCode)
        {
            DataTable dtLocation = new DataTable();
            dlboj = new DL_WIP_Issue();
            try
            {
                dtLocation = dlboj.ValidateMachine(sMahcineID, sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLocation;
        }

        public DataTable BINDFGItemCode(string lineid, string Machineid, string sSiteCode)
        {
            DataTable dtGRN = new DataTable();
            dlboj = new DL_WIP_Issue();
            try
            {
                dtGRN = dlboj.BindFGItemCode(lineid, Machineid, sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtGRN;
        }

        public DataTable BindProfileID(string lINEID, string machineID, string fGITEMCODE)
        {
            DataTable dtGRN = new DataTable();
            dlboj = new DL_WIP_Issue();
            try
            {
                dtGRN = dlboj.BindProgramID(lINEID, machineID, fGITEMCODE);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtGRN;
        }

        public DataTable GetProgramDetailsForWipIssue(string _sProgramID, string sMachineID)
        {
            DataTable dtProfiledata = new DataTable();
            dlboj = new DL_WIP_Issue();
            try
            {
                dtProfiledata = dlboj.GetProgramDetailsForWIPIssue(_sProgramID, sMachineID);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtProfiledata;
        }

        public DataTable GetBin(string BinID)
        {
            DataTable dtGRN = new DataTable();
            dlboj = new DL_WIP_Issue();
            try
            {
                dtGRN = dlboj.GetBinDetails(BinID);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtGRN;
        }

        #region Feeder Scanning
        public DataTable ValidateFeederLocation(string sFeederLocation, string sProgramID, string sMachineID)
        {
            DataTable dtLocation = new DataTable();
            dlboj = new DL_WIP_Issue();
            try
            {
                dtLocation = dlboj.ValidateFeederLocation(sFeederLocation, sProgramID, sMachineID);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLocation;
        }

        public DataTable ValidateFeeder(string sFeederNo, string sProgramID, string sMachineiD,
            string sFeederID, string FeederLocation)
        {
            DataTable dtLocation = new DataTable();
            dlboj = new DL_WIP_Issue();
            try
            {
                dtLocation = dlboj.ValidateFeeder(sFeederNo, sProgramID, sMachineiD, sFeederID, FeederLocation);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLocation;
        }

        #endregion

        #region TOOLScanning and Save of Tool Information
        public DataTable ValidateTool(string sTool, string sProgramID, string sMachineID)
        {
            DataTable dtLocation = new DataTable();
            dlboj = new DL_WIP_Issue();
            try
            {
                dtLocation = dlboj.ValidateTool(sTool, sProgramID, sMachineID);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLocation;
        }

        public string sSaveToolInformation(string sTool, string sProgramID, string sMachineID, string sUserID
            , string sMPID,string sFGItemCode,string sLineCode)
        {
            dlboj = new DL_WIP_Issue();
            string sResult = string.Empty;
            try
            {
                DataTable dt = dlboj.SaveTool(sTool, sProgramID, sMachineID, sUserID, sMPID, sFGItemCode, sLineCode);
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
                }
                else
                {
                    sResult = "N~Scanned barcode not Issued, Please try again.";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }
        #endregion


        #region On Scanning of PartBarcode
        public DataTable ValidateBarcode(string Barcode, string FgItemCode, string sMachineID, string sProgramID)
        {
            DataTable dtLocation = new DataTable();
            dlboj = new DL_WIP_Issue();
            try
            {
                dtLocation = dlboj.ValidateBarcode(Barcode, FgItemCode, sMachineID, sProgramID);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLocation;
        }
        public DataTable ValidateFeederBarcode(string Barcode, string FgItemCode, string sParcode, string sFeederLocation, string sMachineID
            , string sProgramID
            )
        {
            DataTable dtLocation = new DataTable();
            dlboj = new DL_WIP_Issue();
            try
            {
                dtLocation = dlboj.ValidateFeederBarcode(Barcode, FgItemCode,
                    sParcode, sFeederLocation, sMachineID, sProgramID);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLocation;
        }

        public DataTable ValidatePCBbarcodepartcode(string Barcode, string FgItemCode, string sParcode, string sFeederNo, string sMachineID
            , string sProgramID
            )
        {
            DataTable dtLocation = new DataTable();
            dlboj = new DL_WIP_Issue();
            try
            {
                dtLocation = dlboj.ValidatePCBBarcode(Barcode, FgItemCode, sParcode, sFeederNo, sMachineID
            , sProgramID);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLocation;
        }

        public DataTable ValidateSolderBarcode(string bARCODE, int iWIPIssueTime, int iSolderPasteMaximumTime)
        {
            DataTable dtLocation = new DataTable();
            dlboj = new DL_WIP_Issue();
            try
            {
                dtLocation = dlboj.ValidateSolderBarcode(bARCODE, iWIPIssueTime, iSolderPasteMaximumTime);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLocation;
        }
        public DataTable GetPendingBarcode(string Partcode, string sPartType, decimal dShelfLife,string sPartBarcode)
        {
            DataTable dtLocation = new DataTable();
            dlboj = new DL_WIP_Issue();
            try
            {
                dtLocation = dlboj.GetFIFOBarcode(Partcode, sPartType, dShelfLife, sPartBarcode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLocation;
        }

        public string SaveData(string mP_ID, string mMachineID, string mPartBarcode,
         string mBatchNo, string mbin, string mQty, string mCreatedBy, string sPartCode, string stype
         , string sFeederID, string sFeederLocation, string sFeederNo, string sToolID, string sProgramID
            , string sSiteCode, string sLineCode,string sFGItemCode
         )
        {
            string sResult = string.Empty;
            DataTable dtSolderFirstIssueTime = new DataTable();
            dlboj = new DL_WIP_Issue();
            try
            {
                dtSolderFirstIssueTime = dlboj.SaveRMData(mP_ID, mMachineID, mPartBarcode,
          mBatchNo, mbin, mQty, mCreatedBy, sPartCode, stype
         , sFeederID, sFeederLocation, sFeederNo, sToolID, sProgramID
         , sSiteCode, sLineCode, sFGItemCode
         );
                if (dtSolderFirstIssueTime.Rows.Count > 0)
                {
                    sResult = dtSolderFirstIssueTime.Rows[0][0].ToString();
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

        #endregion
    }
}
