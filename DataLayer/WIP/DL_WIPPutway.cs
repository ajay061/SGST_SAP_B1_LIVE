using Common;
using System;
using System.Data;
namespace DataLayer.WIP
{
    public class DL_WIPPutway
    { 
        DBManager oDbm;
        public DL_WIPPutway()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable ValidateLocation(string sLocationBarcode,string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "VALIDATELOCATION");
                oDbm.AddParameters(1, "@LOCATION", sLocationBarcode);
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_PUTWAY").Tables[0];
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
        public DataTable ValidateBarcode(string sLocationBarcode, string sBarcode, string sSiteCode
            ,string sUserID,string sLineCode,string sFIFORequired
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(7);
                oDbm.AddParameters(0, "@TYPE", "VALIDATEBARCODE");
                oDbm.AddParameters(1, "@LOCATION", sLocationBarcode);
                oDbm.AddParameters(2, "@BARCODE", sBarcode);
                oDbm.AddParameters(3, "@LINECODE", sLineCode);
                oDbm.AddParameters(4, "@SITECODE", sSiteCode);
                oDbm.AddParameters(5, "@USERID", sUserID);
                oDbm.AddParameters(6, "@FIFOREQUIRED", sFIFORequired);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_PUTWAY").Tables[0];
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
