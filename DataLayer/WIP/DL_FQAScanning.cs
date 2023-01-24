using Common;
using System;
using System.Data;

namespace DataLayer.WIP
{
    public class DL_FQAScanning
    {
        DBManager oDbm;
        public DL_FQAScanning()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable ValidateMachine(string sMachineID,string sSiteCode,string sLineCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "VALIDATEMACHINE");
                oDbm.AddParameters(1, "@MACHINEID", sMachineID);
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                oDbm.AddParameters(3, "@LINECODE", sLineCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_FQA_SCANNING").Tables[0];
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

        public DataTable BindFGItemCode(string sMachineID, string sSiteCode, string sLineCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "BINDFGITEMCODE");
                oDbm.AddParameters(1, "@MACHINEID", sMachineID);
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                oDbm.AddParameters(3, "@LINECODE", sLineCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_FQA_SCANNING").Tables[0];
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
        public DataTable BindReworkstation(string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDREWORKSTATION");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_FQA_SCANNING").Tables[0];
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
        public DataTable BindDefect(string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDDEFECT");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_FQA_SCANNING").Tables[0];
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
        public DataTable BindSamplingRate(string sFGItemCode, string sSiteCode,string sMachineID)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "BINDFQASAMPLING");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.AddParameters(2, "@FGITEMCODE", sFGItemCode);
                oDbm.AddParameters(3, "@MACHINEID", sMachineID);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_FQA_SCANNING").Tables[0];
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
       
        public DataTable VaildateBarcode(string sPartBarcode, string sMachineID, string FGItemCode
            , string sRefNo, string sSiteCode, string sLineCode)
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(7);
                oDbm.AddParameters(0, "@PART_BARCODE", sPartBarcode);
                oDbm.AddParameters(1, "@FGITEMCODE", FGItemCode);
                oDbm.AddParameters(2, "@MACHINEID", sMachineID);
                oDbm.AddParameters(3, "@SITECODE", sSiteCode);
                oDbm.AddParameters(4, "@TYPE", "VALIDATEPCB");
                oDbm.AddParameters(5, "@LINECODE", sLineCode);
                oDbm.AddParameters(6, "@REFNO", sRefNo);
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_WIP_FQA_SCANNING").Tables[0];
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, 
                    System.Reflection.MethodBase.GetCurrentMethod().Name, "FQA scanning:Barcode:" + sPartBarcode +", Machine ID :" + sMachineID
                    +", Ref No :" +sRefNo + ", FG Item Code :" + FGItemCode
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
            return dtResult;
        }

        public DataTable SaveData(string sPartBarcode, string sMachineID, string FGItemCode
           , string sDefect, string sReworkStation, string sObservation, string sStatus
            , string sRefNo, DataTable dtQualityData, string sQualityFullBatch, string sSiteCode, string sLineCode
            ,string sUserID
            )
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(14);
                oDbm.AddParameters(0, "@PART_BARCODE", sPartBarcode);
                oDbm.AddParameters(1, "@FGITEMCODE", FGItemCode);
                oDbm.AddParameters(2, "@MACHINEID", sMachineID);
                oDbm.AddParameters(3, "@DEFECT", sDefect);
                oDbm.AddParameters(4, "@REWORKSTATIONID", sReworkStation);
                oDbm.AddParameters(5, "@OBSERVATION", sObservation);
                oDbm.AddParameters(6, "@STATUS", sStatus);
                oDbm.AddParameters(7, "@SITECODE", sSiteCode);
                oDbm.AddParameters(8, "@LINECODE", sLineCode);
                oDbm.AddParameters(9, "@QUALITYBY", sUserID);
                oDbm.AddParameters(10, "@TYPE", "SAVEQUALITY");
                oDbm.AddParameters(11, "@REFNO", sRefNo);
                oDbm.AddParameters(12, "@DTQUALITYDATA", dtQualityData);
                oDbm.AddParameters(13, "@QUALITYFULLBATCH", sQualityFullBatch);
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_WIP_FQA_SCANNING").Tables[0];
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
