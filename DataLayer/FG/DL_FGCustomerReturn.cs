using Common;
using System;
using System.Data;
namespace DataLayer.FG
{
    public class DL_FGCustomerReturn
    {
        DBManager oDbm;
        public DL_FGCustomerReturn()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable BindReturnSlipNo(string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDRETURNSLIPNO");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_FG_MARKET_RETURN").Tables[0];
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
        public DataTable GetReturnSlipNo(string sReturnSlipNo,string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "GETDETAILS");
                oDbm.AddParameters(1, "@RETURNSLIPNO", sReturnSlipNo);
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_FG_MARKET_RETURN").Tables[0];
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

        public DataTable SaveRetrunData(
            string sReturnSlipNo, string ITEM_CODE, string sBoxBarcode, string sLocation
            ,string sUserID,string sLineCode,string sSiteCode
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(8);
                oDbm.AddParameters(0, "@TYPE", "SAVERETURN");
                oDbm.AddParameters(1, "@RETURNSLIPNO", sReturnSlipNo);
                oDbm.AddParameters(2, "@ITEMCODE", ITEM_CODE);
                oDbm.AddParameters(3, "@FGBARCODE", sBoxBarcode);
                oDbm.AddParameters(4, "@SCANLOCATION", sLocation);
                oDbm.AddParameters(5, "@SCANNEDBY", sUserID);
                oDbm.AddParameters(6, "@SITECODE", sSiteCode);
                oDbm.AddParameters(7, "@LINECODE", sLineCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_FG_MARKET_RETURN").Tables[0];
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
