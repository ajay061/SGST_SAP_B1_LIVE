using Common;
using System;
using System.Data;

namespace DataLayer
{
    public class DL_WIP_Issue
    {
        DBManager oDbm;

        public DL_WIP_Issue()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable BindLine(string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDLINE");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_ISSUE").Tables[0];
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
        public DataTable ValidateMachine(string sMachineID, string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "VALIDATEMACHINE");
                oDbm.AddParameters(1, "@MACHINEID", sMachineID);
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_ISSUE").Tables[0];
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

        public DataTable BindFGItemCode(string lineid, string sMachineID
            , string sSiteCode
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "BINDFGITEMCODE");
                oDbm.AddParameters(1, "@MACHINEID", sMachineID);
                oDbm.AddParameters(2, "@LINEID", lineid);
                oDbm.AddParameters(3, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_ISSUE").Tables[0];
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
        public DataTable BindProgramID(string lineid, string sMachineID, string fGITEMCODE)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(5);
                oDbm.AddParameters(0, "@TYPE", "BINDPROGRAMID");
                oDbm.AddParameters(1, "@MACHINEID", sMachineID);
                oDbm.AddParameters(2, "@LINEID", lineid);
                oDbm.AddParameters(3, "@FGITEMCODE", fGITEMCODE);
                oDbm.AddParameters(4, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_ISSUE").Tables[0];
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

        public DataTable GetProgramDetailsForWIPIssue(string _sProgramID, string sMachineID)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "GETPROGRAMDETAILS");
                oDbm.AddParameters(1, "@MACHINEID", sMachineID);
                oDbm.AddParameters(2, "@PROGRAMID", _sProgramID);
                oDbm.AddParameters(3, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_ISSUE").Tables[0];
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

        public DataTable GetBinDetails(string BINS)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "GETBIN");
                oDbm.AddParameters(1, "@BINBARCODE", BINS);
                oDbm.AddParameters(2, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_ISSUE").Tables[0];
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
        public DataTable ValidateFeederLocation(string sFeederLocation, string sProgramID, string sMachineID)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(5);
                oDbm.AddParameters(0, "@TYPE", "VALIDATEFEEDERLOCATION");
                oDbm.AddParameters(1, "@FEEDERLOCATION", sFeederLocation);
                oDbm.AddParameters(2, "@PROGRAMID", sProgramID);
                oDbm.AddParameters(3, "@MACHINEID", sMachineID);
                oDbm.AddParameters(4, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_ISSUE").Tables[0];
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
        public DataTable ValidateFeeder(string sFeederNo, string sProgramID, string sMachineiD,
            string sFeederID, string FeederLocation)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(7);
                oDbm.AddParameters(0, "@TYPE", "VALIDATEFEEDER");
                oDbm.AddParameters(1, "@FEEDERLOCATION", FeederLocation);
                oDbm.AddParameters(2, "@PROGRAMID", sProgramID);
                oDbm.AddParameters(3, "@MACHINEID", sMachineiD);
                oDbm.AddParameters(4, "@FEEDERNO", sFeederNo);
                oDbm.AddParameters(5, "@FEEDERID", sFeederID);
                oDbm.AddParameters(6, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_ISSUE").Tables[0];
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

        public DataTable ValidateTool(string sTool, string sProgramID, string sMachineID)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(5);
                oDbm.AddParameters(0, "@TYPE", "VALIDATETOOL");
                oDbm.AddParameters(1, "@MACHINEID", sMachineID);
                oDbm.AddParameters(2, "@PROGRAMID", sProgramID);
                oDbm.AddParameters(3, "@TOOLID", sTool);
                oDbm.AddParameters(4, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_ISSUE").Tables[0];
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
        public DataTable SaveTool(string sTool, string sProgramID, string sMachineID, string sUserID, string sMPID
            ,string sFGItemCode,string sLineCode)
        {
            DataTable dt = new DataTable();
            try
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtMessage, System.Reflection.MethodBase.GetCurrentMethod().Name,
                    "Save Tool-USP_WIP_ISSUE, Machine ID : " + sMachineID
                    + ", Program ID : " + sProgramID
                    + ", Tool ID :" + sTool
                    + ", FG Item Code :" + sFGItemCode
                    );
                oDbm.CreateParameters(9);
                oDbm.AddParameters(0, "@TYPE", "SAVETOOL");
                oDbm.AddParameters(1, "@MACHINEID", sMachineID);
                oDbm.AddParameters(2, "@PROGRAMID", sProgramID);
                oDbm.AddParameters(3, "@TOOLID", sTool);
                oDbm.AddParameters(4, "@USERID", sUserID);
                oDbm.AddParameters(5, "@MPID", sMPID);
                oDbm.AddParameters(6, "@SITECODE", PCommon.sSiteCode);
                oDbm.AddParameters(7, "@FGITEMCODE", sFGItemCode);
                oDbm.AddParameters(8, "@LINECODE", sLineCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_ISSUE").Tables[0];
                
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
        public DataTable ValidateBarcode(string Barcode, string FgItemCode, string sMachineID, string sProgramID)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(6);
                oDbm.AddParameters(0, "@TYPE", "VALIDATEBARCODE");
                oDbm.AddParameters(1, "@MACHINEID", sMachineID);
                oDbm.AddParameters(2, "@PROGRAMID", sProgramID);
                oDbm.AddParameters(3, "@RMPARTBARCODE", Barcode);
                oDbm.AddParameters(4, "@FGITEMCODE", FgItemCode);
                oDbm.AddParameters(5, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_ISSUE_SCAN_RM").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dt;
        }
        public DataTable ValidateFeederBarcode(string Barcode, string FgItemCode, string sParcode,
            string sFeederLocation, string sMachineID
            , string sProgramID)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(6);
                oDbm.AddParameters(0, "@TYPE", "VALIDATEFEEDERBARCODE");
                oDbm.AddParameters(1, "@FEEDERLOCATION", sFeederLocation);
                oDbm.AddParameters(2, "@PROGRAMID", sProgramID);
                oDbm.AddParameters(3, "@MACHINEID", sMachineID);
                oDbm.AddParameters(4, "@RMPARTCODE", sParcode);
                oDbm.AddParameters(5, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_ISSUE_SCAN_RM").Tables[0];
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

        public DataTable ValidatePCBBarcode(string Barcode, string FgItemCode, string sParcode,
            string sFeederNo, string sMachineID
            , string sProgramID)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(6);
                oDbm.AddParameters(0, "@TYPE", "VALIDATEPCBBARCODE");
                oDbm.AddParameters(1, "@PROGRAMID", sProgramID);
                oDbm.AddParameters(2, "@MACHINEID", sMachineID);
                oDbm.AddParameters(3, "@RMPARTCODE", sParcode);
                oDbm.AddParameters(4, "@SITECODE", PCommon.sSiteCode);
                oDbm.AddParameters(5, "@RMPARTBARCODE", Barcode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_ISSUE_SCAN_RM").Tables[0];
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


        public DataTable ValidateSolderBarcode(string Barcode, int iWIPIssueTime, int iSolderPasteMaximumTime)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(5);
                oDbm.AddParameters(0, "@TYPE", "VALIDATESOLDERBARCODE");
                oDbm.AddParameters(1, "@RMPARTBARCODE", Barcode);
                oDbm.AddParameters(2, "@WIPSOLDERTIME", iWIPIssueTime);
                oDbm.AddParameters(3, "@SOLDERPASTETOTALTIME", iSolderPasteMaximumTime);
                oDbm.AddParameters(4, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_ISSUE_SCAN_RM").Tables[0];
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

        public DataTable GetFIFOBarcode(string Partcode, string sPartType, decimal dShelfLife,string sPartBarcode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(6);
                oDbm.AddParameters(0, "@TYPE", "GETPENDINGSOLDERBARCODE");
                oDbm.AddParameters(1, "@RMPARTCODE", Partcode);
                oDbm.AddParameters(2, "@SITECODE", PCommon.sSiteCode);
                oDbm.AddParameters(3, "@PARTTYPE", sPartType);
                oDbm.AddParameters(4, "@SHELFLIFE", dShelfLife);
                oDbm.AddParameters(5, "@RMPARTBARCODE", sPartBarcode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_ISSUE_SCAN_RM").Tables[0];
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
        public DataTable ValidateFIFOBarcode(string sPartCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "GETPENDINGOTHERBARCODE");
                oDbm.AddParameters(1, "@RMPARTCODE", sPartCode);
                oDbm.AddParameters(2, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_ISSUE_SCAN_RM").Tables[0];
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

        public DataTable SaveRMData(string mP_ID, string mMachineID, string mPartBarcode,
         string mBatchNo, string mbin, string mQty, string mCreatedBy, string sPartCode, string stype
         , string sFeederID, string sFeederLocation, string sFeederNo, string sToolID, string sProgramID
            , string sSiteCode, string sLineCode,string sFGItemCode
            )
        {
            DataTable dt = new DataTable();
            try
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtMessage, System.Reflection.MethodBase.GetCurrentMethod().Name,
                      "Save Barcode-USP_WIP_ISSUE, Machine ID : " + mMachineID
                      + ", Program ID : " + sProgramID
                      + ", Barcode :" + mPartBarcode
                      + ", FG Item Code :" + sFGItemCode
                      );
                oDbm.CreateParameters(18);
                oDbm.AddParameters(0, "@TYPE", "SAVEDATA");
                oDbm.AddParameters(1, "@MPID", mP_ID);
                oDbm.AddParameters(2, "@MACHINEID", mMachineID);
                oDbm.AddParameters(3, "@RMPARTBARCODE", mPartBarcode);
                oDbm.AddParameters(4, "@BATCHNO", mBatchNo);
                oDbm.AddParameters(5, "@BINBARCODE", mbin);
                oDbm.AddParameters(6, "@QTY", mQty);
                oDbm.AddParameters(7, "@PROGRAMID", sProgramID);
                oDbm.AddParameters(8, "@RMPARTCODE", sPartCode);
                oDbm.AddParameters(9, "@USERID", mCreatedBy);
                oDbm.AddParameters(10, "@FEEDERLOCATION", sFeederLocation);
                oDbm.AddParameters(11, "@FEEDERNO", sFeederNo);
                oDbm.AddParameters(12, "@FEEDERID", sFeederID);
                oDbm.AddParameters(13, "@TOOLID", sToolID);
                oDbm.AddParameters(14, "@PROCESSTYPE", stype);
                oDbm.AddParameters(15, "@SITECODE", sSiteCode);
                oDbm.AddParameters(16, "@LINECODE", sLineCode);
                oDbm.AddParameters(17, "@FGITEMCODE", sFGItemCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_ISSUE_SCAN_RM").Tables[0];
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
