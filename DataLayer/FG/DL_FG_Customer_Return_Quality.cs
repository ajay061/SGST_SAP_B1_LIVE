using Common;
using System;
using System.Data;

namespace DataLayer.FG
{
    public class DL_FG_Customer_Return_Quality
    {
        DBManager oDbm;
        public DL_FG_Customer_Return_Quality()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable ValidateFGBarcode(string _sBarcode,string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "VALIDATEFGBARCODE");
                oDbm.AddParameters(1, "@FGBARCODE", _sBarcode);
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_FG_MARKET_RETURN_QUALITY").Tables[0];
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

        public DataTable SaveReturnQualityData(string _sBarcode, string _val, string _ScanBy,string sRemarks,string sObservation
            ,string sSiteCode,string sLineCode
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(8);
                oDbm.AddParameters(0, "@TYPE", "SAVERETURNQUALITY");
                oDbm.AddParameters(1, "@FGBARCODE", _sBarcode);
                oDbm.AddParameters(2, "@RETURNTYPE", _val);
                oDbm.AddParameters(3, "@SCANNEDBY", _ScanBy);
                oDbm.AddParameters(4, "@SITECODE", sSiteCode);
                oDbm.AddParameters(5, "@LINECODE", sLineCode);
                oDbm.AddParameters(6, "@REMARKS", sRemarks);
                oDbm.AddParameters(7, "@OBSERVATION", sObservation);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_FG_MARKET_RETURN_QUALITY").Tables[0];
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
