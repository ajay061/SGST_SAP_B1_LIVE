using Common;
using System;
using System.Data;
namespace DataLayer.WIP
{
    public class DL_WIP_FGAssembly
    {
        DBManager oDbm;
        public DL_WIP_FGAssembly()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable BindWorkOrderNo(string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDWONO");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_FG_ASSEMBLY").Tables[0];
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
        public DataTable ValidateMachine(string sMachineID, string sLineiD, string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "VALIDATEMACHINE");
                oDbm.AddParameters(1, "@MACHINEID", sMachineID);
                oDbm.AddParameters(2, "@LINECODE", sLineiD);
                oDbm.AddParameters(3, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_FG_ASSEMBLY").Tables[0];
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
        public DataTable BindFGItemCode(string sWorkOrderNo, string sSiteCode, string sMachineID
            , string sLineCode
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(5);
                oDbm.AddParameters(0, "@TYPE", "BINDFGITEMCODE");
                oDbm.AddParameters(1, "@WORKORDERNO", sWorkOrderNo);
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                oDbm.AddParameters(3, "@MACHINEID", sMachineID);
                oDbm.AddParameters(4, "@LINECODE", sLineCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_FG_ASSEMBLY").Tables[0];
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
        public DataTable BindCustomerCode(string sFGItemCode, string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "BINDCUSTOMERCODE");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.AddParameters(2, "@FGITEMCODE", sFGItemCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_FG_ASSEMBLY").Tables[0];
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

        public DataTable GetProgramDetails(string sFGItemCode, string sMachineID, string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "GETPROGRAMDETAILS");
                oDbm.AddParameters(1, "@FGITEMCODE", sFGItemCode);
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                oDbm.AddParameters(3, "@MACHINEID", sMachineID);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_FG_ASSEMBLY").Tables[0];
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

        public DataTable ValidateBarcode(string sScanBarcode, string sFGItemCode, string sCustomerPartCode
            , string sSiteCode, string sLineCode, string sWorkOrderNo
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(7);
                oDbm.AddParameters(0, "@TYPE", "VALIDATEPARTBARCODE");
                oDbm.AddParameters(1, "@FGITEMCODE", sFGItemCode);
                oDbm.AddParameters(2, "@PARTBARCODE", sScanBarcode);
                oDbm.AddParameters(3, "@SITECODE", sSiteCode);
                oDbm.AddParameters(4, "@LINECODE", sLineCode);
                oDbm.AddParameters(5, "@CUSTOMERPARTCODE", sCustomerPartCode);
                oDbm.AddParameters(6, "@WORKORDERNO", sWorkOrderNo);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_FG_ASSEMBLY").Tables[0];
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


        public DataTable StorePCBData(DataTable dtLaserFile,
        string sFGItemCode, int iQty, int iArraySize, string sBatchNo
      , string sPONO, int iSNNO, DataTable dtMappedData, string sReelBarcode, string sPartCode
             , string sSiteCode, string sLineCode, string sUserID, string sMachineID,string sPCBBarcode 
      )
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(16);
                oDbm.AddParameters(0, "@FGITEMCODE", sFGItemCode);
                oDbm.AddParameters(1, "@USER_ID", sUserID);
                oDbm.AddParameters(2, "@DTCHILSN_L", dtLaserFile);
                oDbm.AddParameters(3, "@QTY", iQty);
                oDbm.AddParameters(4, "@ARRAYSIZE", iArraySize);
                oDbm.AddParameters(5, "@BATCHNO", sBatchNo);
                oDbm.AddParameters(6, "@WORKORDERNO", sPONO);
                oDbm.AddParameters(7, "@TYPE", "STORELASERFILE");
                oDbm.AddParameters(8, "@SITECODE", sSiteCode);
                oDbm.AddParameters(9, "@LINECODE", sLineCode);
                oDbm.AddParameters(10, "@iSNNO", iSNNO);
                oDbm.AddParameters(11, "@DTCHILSN_A", dtMappedData);
                oDbm.AddParameters(12, "@PARTBARCODE", sReelBarcode);
                oDbm.AddParameters(13, "@PARTCODE", sPartCode);
                oDbm.AddParameters(14, "@MACHINEID", sMachineID);
                oDbm.AddParameters(15, "@PCBBARCODE", sPCBBarcode);
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,
                        System.Reflection.MethodBase.GetCurrentMethod().Name,
                        "FG Assembly Module : Work Order No. :" + sPONO + ",Scanned Barcode :" + sReelBarcode
                        + ",FG item code : " + sFGItemCode + ",User ID  : " + sUserID + ", PCB barcode : " + sPCBBarcode + 
                        ",Prining SN :" + iSNNO.ToString()
                        );
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_WIP_FG_ASSEMBLY").Tables[0];
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
            return dtResult;
        }
    }
}
