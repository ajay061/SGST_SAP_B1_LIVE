using Common;
using System;
using System.Data;

namespace DataLayer.WIP
{
    public class DL_WIP_BackingProcess
    {
        DBManager oDbm;
        public DL_WIP_BackingProcess()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable BindFGItemCode(string sSiteCode,string sUserID,string sLineCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "BINDFGITEMCODE");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.AddParameters(2, "@LINECODE", sLineCode);
                oDbm.AddParameters(3, "@USERID", sUserID);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_BAKING").Tables[0];
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
        public DataTable BindWorkOrderNo(string sFGItemCode,string sSiteCode,string sLineCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "BINDWORKORDERNO");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.AddParameters(2, "@LINECODE", sLineCode);
                oDbm.AddParameters(3, "@FGITEMCODE", sFGItemCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_BAKING").Tables[0];
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
        public DataTable ValidateReel(string _Part_Barcode,string sType,string sFGItemCode,string sWorkOrderNo
            ,string sSiteCode,string sUserID,string sLineCode
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(7);
                oDbm.AddParameters(0, "@BARCODE", _Part_Barcode);
                oDbm.AddParameters(1, "@TYPE", sType);
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                oDbm.AddParameters(3, "@LINECODE", sLineCode);
                oDbm.AddParameters(4, "@USERID", sUserID);
                oDbm.AddParameters(5, "@FGITEMCODE", sFGItemCode);
                oDbm.AddParameters(6, "@WORKORDERNO", sWorkOrderNo);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_BAKING").Tables[0];
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
        public DataTable BindPendingBarcode(string sFGItemCode,string sWorkOrderNo
             , string sSiteCode, string sLineCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(5);
                oDbm.AddParameters(0, "@TYPE", "BINDPENDINGBARCODE");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.AddParameters(2, "@LINECODE", sLineCode);
                oDbm.AddParameters(3, "@FGITEMCODE", sFGItemCode);
                oDbm.AddParameters(4, "@WORKORDERNO", sWorkOrderNo);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_BAKING").Tables[0];
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
