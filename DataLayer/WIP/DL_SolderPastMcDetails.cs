using System;
using Common;
using System.Data;
namespace DataLayer
{
    public class DL_SolderPastMcDetails
    {
        DBManager oDbm;
        public DL_SolderPastMcDetails()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }

        public DataTable ValidateMachine(string MACHINEID,string sSiteCode,string sLineCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "VALIDATEMACHINE");
                oDbm.AddParameters(1, "@MACHINEID", MACHINEID);
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                oDbm.AddParameters(3, "@LINECODE", sLineCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_SOLDER_PASTE_SCANNING").Tables[0];
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
        public DataTable BindFGItemCode(string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDFGITEMCODE");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_SOLDER_PASTE_SCANNING").Tables[0];
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
        public DataTable ValidateReel(string sMachineID,string sFGItemCode,string sReelBarcode
            ,string sProcess, int iMachineSeq, string sMachineName
            ,int iSolderPasteMaximumTime,string sSiteCode,string sLineCode,string sFIFORequired
            )
        {
            DataTable dt = new DataTable();
            try
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,
                  System.Reflection.MethodBase.GetCurrentMethod().Name, "Validate Solder Paste Module Data : " + sReelBarcode
                  + ", Process :  " + sProcess + ", Solder Paste Time :" + iSolderPasteMaximumTime.ToString()
                  + ",sFIFORequired" + sFIFORequired + ", Line Code : " + sLineCode
                  + ", Site Code : " + sSiteCode + ", Machine Seq : " + iMachineSeq.ToString()
                  );
                oDbm.CreateParameters(11);
                oDbm.AddParameters(0, "@TYPE", "VALIDATEREEL");
                oDbm.AddParameters(1, "@MACHINEID", sMachineID);
                oDbm.AddParameters(2, "@FGITEMCODE", sFGItemCode);
                oDbm.AddParameters(3, "@REELBARCODE", sReelBarcode);
                oDbm.AddParameters(4, "@PROCESS", sProcess);
                oDbm.AddParameters(5, "@MACHINESEQ", iMachineSeq);
                oDbm.AddParameters(6, "@MACHINENAME", sMachineName);
                oDbm.AddParameters(7, "@SOLDERPASTETIME", iSolderPasteMaximumTime);
                oDbm.AddParameters(8, "@SITECODE", sSiteCode);
                oDbm.AddParameters(9, "@LINECODE", sLineCode);
                oDbm.AddParameters(10, "@FIFOREQUIRED", sFIFORequired);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_SOLDER_PASTE_SCANNING").Tables[0];
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, "Validate Solder Paste Module Data : " + sReelBarcode
                    + ", Process :  " + sProcess + ", Solder Paste Time :" + iSolderPasteMaximumTime.ToString()
                    + ",sFIFORequired" + sFIFORequired + ", Line Code : " + sLineCode
                    + ", Site Code : " + sSiteCode + ", Result : " + dt.Rows[0][0].ToString()
                    );
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

        public DataTable SaveData(string sMachineID, string sFGItemCode, string sReelBarcode
            , string sProcess, string sMachineName
            ,string sPartCode,string sUserID,int iSolderPasteMaximumTime
            ,string sSiteCode,string sLineCode
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(10);
                oDbm.AddParameters(0, "@TYPE", "SAVEDATA");
                oDbm.AddParameters(1, "@MACHINEID", sMachineID);
                oDbm.AddParameters(2, "@FGITEMCODE", sFGItemCode);
                oDbm.AddParameters(3, "@REELBARCODE", sReelBarcode);
                oDbm.AddParameters(4, "@PROCESS", sProcess);
                oDbm.AddParameters(5, "@MACHINENAME", sMachineName);
                oDbm.AddParameters(6, "@PARTCODE", sPartCode);
                oDbm.AddParameters(7, "@USERID", sUserID);
                oDbm.AddParameters(8, "@SOLDERPASTETIME", iSolderPasteMaximumTime);
                oDbm.AddParameters(9, "@SITECODE", sSiteCode);
                oDbm.AddParameters(10, "@LINECODE", sLineCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_SOLDER_PASTE_SCANNING").Tables[0];
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
    }
}
