using System;
using System.Data;
using DataLayer;
using Common;
namespace BusinessLayer.WIP
{
    public class BL_SolderPastMcDetails
    {
        DL_SolderPastMcDetails dlboj = null;
        public DataTable ScanMachineBarcode(string sScanBarcode,string sSiteCode,string sLineCode)
        {
            DataTable dtScanMachine = new DataTable();
            dlboj = new DataLayer.DL_SolderPastMcDetails();
            try
            {
                dtScanMachine = dlboj.ValidateMachine(sScanBarcode, sSiteCode, sLineCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtScanMachine;
        }
        public DataTable dtBindFGItemCode(string sSiteCode)
        {
            DataTable dtProcess = new DataTable();
            try
            {
                dlboj = new DL_SolderPastMcDetails();
                dtProcess = dlboj.BindFGItemCode(sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtProcess;
        }
        public string Get_REELIDS(string _PartBarcode, string sMachineName, string sType, string MachineID
            , int iMachineSeq, string sFGitemCode, int iSolderPasteMaximumTime
            ,string sSiteCode,string sLineCode, string sFIFORequired
            )
        {
            string sResult = string.Empty;
            DataTable dt = new DataTable();
            dlboj = new DataLayer.DL_SolderPastMcDetails();
            try
            {
                dlboj = new DL_SolderPastMcDetails();
                dt = dlboj.ValidateReel(MachineID, sFGitemCode, _PartBarcode, sType, iMachineSeq, sMachineName, iSolderPasteMaximumTime
                    , sSiteCode, sLineCode, sFIFORequired
                    );
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
                }
                else
                {
                    sResult = "N~No result found, Please try again";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }
        public string SaveMachineData(string _PartBarcode, string PART_CODE,
string sScannedBy, string Type, string sMachineName, string sMachineID, string sFGitemCode, int iSolderPasteMaximumTime
            ,string sSiteCode,string sLineCode
            )
        {
            DataTable dt = new DataTable();
            string sResult = string.Empty;
            dlboj = new DataLayer.DL_SolderPastMcDetails();
            try
            {
                dlboj = new DL_SolderPastMcDetails();
                dt = dlboj.SaveData(sMachineID, sFGitemCode, _PartBarcode, Type, sMachineName, PART_CODE, sScannedBy, 
                    iSolderPasteMaximumTime, sSiteCode, sLineCode);
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
                }
                else
                {
                    sResult = "N~No result found, Please try again";
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
