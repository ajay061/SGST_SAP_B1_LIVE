using System;
using System.Data;
using Common;

namespace DataLayer
{
    public class DL_FGPutaway
    {
        DBManager oDbm;
        public DL_FGPutaway()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }

        public DataTable ValidateBarcode(string sBarcode, string sSiteCode, string sLineCode,string sScanType,string sFIFOType)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(6);
                oDbm.AddParameters(0, "@TYPE", "VALIDATEBARCODE");
                oDbm.AddParameters(1, "@BARCODE", sBarcode);
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                oDbm.AddParameters(3, "@LINECODE", sLineCode);
                oDbm.AddParameters(4, "@SCANTYPE", sScanType.ToUpper());
                oDbm.AddParameters(5, "@FIFOTYPE", sFIFOType);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_FG_PUTWAY").Tables[0];
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
        public DataTable ValidateLocation(string sLocationBarcode, string sBarcode,string _sScanBy
            , string sSiteCode, string sLineCode,string sScanType
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(7);
                oDbm.AddParameters(0, "@TYPE", "VALIDATELOCATION");
                oDbm.AddParameters(1, "@LOCATION", sLocationBarcode);
                oDbm.AddParameters(2, "@BARCODE", sBarcode);
                oDbm.AddParameters(3, "@SCANNEDBY", _sScanBy);
                oDbm.AddParameters(4, "@SITECODE", sSiteCode);
                oDbm.AddParameters(5, "@LINECODE", sLineCode);
                oDbm.AddParameters(6, "@SCANTYPE", sScanType);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_FG_PUTWAY").Tables[0];
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
