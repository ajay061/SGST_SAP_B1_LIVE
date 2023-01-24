using Common;
using System;
using System.Data;
namespace DataLayer.FG
{
    public class DL_PackingList
    {
        DBManager oDbm;
        public DL_PackingList()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable BindOutbound(string sSiteCode,string sLineCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "BINDOUTBOUND");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.AddParameters(2, "@LINECODE", sLineCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_FG_CREATE_PACKINGLIST").Tables[0];
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
        public DataTable BindSalesOrder(string sOutBondDeliveryno, string sSiteCode, string sLineCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "BINDSALESORDER");
                oDbm.AddParameters(1, "@OUTBONDDELIVERYNO", sOutBondDeliveryno);
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                oDbm.AddParameters(3, "@LINECODE", sLineCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_FG_CREATE_PACKINGLIST").Tables[0];
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
     
        public DataTable BindPicklistNo(string sSalesordeNo, string sSiteCode, string sLineCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "BINDPICKLISTNO");
                oDbm.AddParameters(1, "@SALESORDERNO", sSalesordeNo);
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                oDbm.AddParameters(3, "@LINECODE", sLineCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_FG_CREATE_PACKINGLIST").Tables[0];
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
        public DataTable BindItemCode(string sSalesOrderNo, string sPicklistNo, string sSiteCode, string sLineCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(5);
                oDbm.AddParameters(0, "@TYPE", "BINDITEMCODE");
                oDbm.AddParameters(1, "@SALESORDERNO", sSalesOrderNo);
                oDbm.AddParameters(2, "@PICKLISTNO", sPicklistNo);
                oDbm.AddParameters(3, "@SITECODE", sSiteCode);
                oDbm.AddParameters(4, "@LINECODE", sLineCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_FG_CREATE_PACKINGLIST").Tables[0];
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
        public DataTable GetDetails(string sSalesOrderNo, string sPicklistNo, string sItemCode
            , string sSiteCode, string sLineCode
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(5);
                oDbm.AddParameters(0, "@TYPE", "GETDETAILS");
                oDbm.AddParameters(1, "@SALESORDERNO", sSalesOrderNo);
                oDbm.AddParameters(2, "@PICKLISTNO", sPicklistNo);
                oDbm.AddParameters(3, "@ITEMCODE", sItemCode);
                oDbm.AddParameters(3, "@SITECODE", sSiteCode);
                oDbm.AddParameters(4, "@LINECODE", sLineCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_FG_CREATE_PACKINGLIST").Tables[0];
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

        public DataTable sScanLocation(string sSalesOrderNo, string sItemCode, string sPicklistNo,
          string sLocation, string sScannedBy, string sSiteCode, string sLineCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(8);
                oDbm.AddParameters(0, "@TYPE", "SCANLOCATION");
                oDbm.AddParameters(1, "@SALESORDERNO", sSalesOrderNo);
                oDbm.AddParameters(2, "@PICKLISTNO", sPicklistNo);
                oDbm.AddParameters(3, "@ITEMCODE", sItemCode);
                oDbm.AddParameters(4, "@LOCATION", sLocation);
                oDbm.AddParameters(5, "@SCANNEDBY", sScannedBy);
                oDbm.AddParameters(6, "@SITECODE", sSiteCode);
                oDbm.AddParameters(7, "@LINECODE", sLineCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_FG_CREATE_PACKINGLIST").Tables[0];
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

        public DataTable ScanFGBarcode(string sSalesOrderNo, string sItemCode, string sPicklistNo, 
            string sBoxBarcode, string sScannedBy,string sLocation
            , string sSiteCode, string sLineCode
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(9);
                oDbm.AddParameters(0, "@TYPE", "SCANFGBARCODE");
                oDbm.AddParameters(1, "@SALESORDERNO", sSalesOrderNo);
                oDbm.AddParameters(2, "@PICKLISTNO", sPicklistNo);
                oDbm.AddParameters(3, "@ITEMCODE", sItemCode);
                oDbm.AddParameters(4, "@FGBARCODE", sBoxBarcode);
                oDbm.AddParameters(5, "@SCANNEDBY", sScannedBy);
                oDbm.AddParameters(6, "@SITECODE", sSiteCode);
                oDbm.AddParameters(7, "@LINECODE", sLineCode);
                oDbm.AddParameters(8, "@LOCATION", sLocation);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_FG_CREATE_PACKINGLIST").Tables[0];
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


        public DataTable CreatePackingList(string sSalesOrderNo, string sItemCode, string sPickListNo, string sPackingBy
            , string sSiteCode, string sLineCode
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(7);
                oDbm.AddParameters(0, "@TYPE", "GENERATEPACKINGLIST");
                oDbm.AddParameters(1, "@SALESORDERNO", sSalesOrderNo);
                oDbm.AddParameters(2, "@PICKLISTNO", sPickListNo);
                oDbm.AddParameters(3, "@ITEMCODE", sItemCode);
                oDbm.AddParameters(4, "@SCANNEDBY", sPackingBy);
                oDbm.AddParameters(5, "@SITECODE", sSiteCode);
                oDbm.AddParameters(6, "@LINECODE", sLineCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_FG_CREATE_PACKINGLIST").Tables[0];
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
