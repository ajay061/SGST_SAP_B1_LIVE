using Common;
using System;
using System.Data;
namespace DataLayer.WIP
{
    public class DL_BosaSNMapping
    {
        DBManager oDbm;
        public DL_BosaSNMapping()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable ValidateMachine(string sMachineID, string sSiteCode, string sLineCode)
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
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_BOSA_SN_VALIDATE").Tables[0];
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
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_BOSA_SN_VALIDATE").Tables[0];
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

        public DataTable VaildateBosaBarcode(string sBosaBarcode)
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@BOSASNBARCODE", sBosaBarcode);
                oDbm.AddParameters(1, "@TYPE", "VALIDATEBOSABARCODE");
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_WIP_BOSA_SN_VALIDATE").Tables[0];
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
                    , string sSiteCode, string sLineCode, string sUserID,string sBosaBarcode
            )
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(8);
                oDbm.AddParameters(0, "@PART_BARCODE", sPartBarcode);
                oDbm.AddParameters(1, "@FGITEMCODE", FGItemCode);
                oDbm.AddParameters(2, "@MACHINEID", sMachineID);
                oDbm.AddParameters(3, "@BOSASNBARCODE", sBosaBarcode);              
                oDbm.AddParameters(4, "@SITECODE", sSiteCode);
                oDbm.AddParameters(5, "@LINECODE", sLineCode);
                oDbm.AddParameters(6, "@SCANNEDBY", sUserID);
                oDbm.AddParameters(7, "@TYPE", "VALIDATEPCB");
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_WIP_BOSA_SN_VALIDATE").Tables[0];
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
    }
}
