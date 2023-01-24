using Common;
using System;
using System.Data;

namespace DataLayer.RM
{
    public class DL_SupplierReturn
    {
        DBManager oDbm;
        public DL_SupplierReturn()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable BindPONO(string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDPONO");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_RM_SUPPLIER_RETURN_SGST").Tables[0];
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
            return dt;

        }
        public DataTable BindPartCode(string sPONo,string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "BINDPARTCODE");
                oDbm.AddParameters(1, "@PONO", sPONo);
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_RM_SUPPLIER_RETURN_SGST").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message + ", Query " + "USP_GETLASERSERIAL");
                throw ex;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dt;

        }

        public DataTable BindLineno(string sPONo, string sPartCode,string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "BINDLINENO");
                oDbm.AddParameters(1, "@PONO", sPONo);
                oDbm.AddParameters(2, "@PARTCODE", sPartCode);
                oDbm.AddParameters(3, "@SITECODE", sSiteCode);
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_RM_SUPPLIER_RETURN_SGST").Tables[0];
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
            return dt;

        }

        public DataTable GetDetails(string sPONo, string sPartCode, string sItemLineNo
            ,string sSiteCode
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(5);
                oDbm.AddParameters(0, "@TYPE", "BINDDETAILS");
                oDbm.AddParameters(1, "@PONO", sPONo);
                oDbm.AddParameters(2, "@PARTCODE", sPartCode);
                oDbm.AddParameters(3, "@ITEMLINENO", sItemLineNo);
                oDbm.AddParameters(4, "@SITECODE", sSiteCode);
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_RM_SUPPLIER_RETURN_SGST").Tables[0];
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
            return dt;

        }

        public DataTable ValidateSupplierData(string sPONo, string sPartCode, string sItemLineNo
            , string sLocationCode, string sScannedBarcode, string UserID, string sSiteCode
            ,string sLineCode
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(9);
                oDbm.AddParameters(0, "@TYPE", "VALIDATEBARCODE");
                oDbm.AddParameters(1, "@PONO", sPONo);
                oDbm.AddParameters(2, "@PARTCODE", sPartCode);
                oDbm.AddParameters(3, "@ITEMLINENO", sItemLineNo);
                oDbm.AddParameters(4, "@LOCATION", sLocationCode);
                oDbm.AddParameters(5, "@PARTBARCODE", sScannedBarcode);
                oDbm.AddParameters(6, "@SCANNEDBY", UserID);
                oDbm.AddParameters(7, "@SITECODE", sSiteCode);
                oDbm.AddParameters(8, "@LINECODE", sLineCode);
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_RM_SUPPLIER_RETURN_SGST").Tables[0];
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
            return dt;
        }
    }
}
