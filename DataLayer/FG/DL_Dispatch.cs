using Common;
using System;
using System.Data;

namespace DataLayer.FG
{
    public class DL_Dispatch
    {
        DBManager oDbm;
        public DL_Dispatch()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable BindCustomerCode(string sSiteCode,string sLineCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "BINDCUSTOMER");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.AddParameters(2, "@LINECODE", sLineCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_FG_CREATE_PICKLIST").Tables[0];
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
        public DataTable BindOutbondDeliveryNo(string sCustomerCode, string sSiteCode, string sLineCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "BINDOUTBOUND");
                oDbm.AddParameters(1, "@CUSTOMERCODE", sCustomerCode);
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                oDbm.AddParameters(3, "@LINECODE", sLineCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_FG_CREATE_PICKLIST").Tables[0];
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

        public DataTable BindSalesOrder(string sCustomerCode,string sOutBondDeliveryNo,string sSiteCode,string sLineCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(5);
                oDbm.AddParameters(0, "@TYPE", "BINDSALESORDER");
                oDbm.AddParameters(1, "@CUSTOMERCODE", sCustomerCode);
                oDbm.AddParameters(2, "@OUTBONDDELIVERYNO", sOutBondDeliveryNo);
                oDbm.AddParameters(3, "@SITECODE", sSiteCode);
                oDbm.AddParameters(4, "@LINECODE", sLineCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_FG_CREATE_PICKLIST").Tables[0];
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

        public DataTable GETDETAILS(string sCustomerCode, string sOutBondDeliveryNo, string sSalesordernO
            , string sSiteCode, string sLineCode
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(6);
                oDbm.AddParameters(0, "@TYPE", "GETDETAILS");
                oDbm.AddParameters(1, "@CUSTOMERCODE", sCustomerCode);
                oDbm.AddParameters(2, "@OUTBONDDELIVERYNO", sOutBondDeliveryNo);
                oDbm.AddParameters(3, "@SALESORDERNO", sSalesordernO);
                oDbm.AddParameters(4, "@SITECODE", sSiteCode);
                oDbm.AddParameters(5, "@LINECODE", sLineCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_FG_CREATE_PICKLIST").Tables[0];
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

        public DataTable GenereatePickList(string sCustomerCode, string sOutBondDeliveryNo, string sSalesordernO
            ,string sCreatedBy, string sSiteCode, string sLineCode
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(7);
                oDbm.AddParameters(0, "@TYPE", "GENERATEPICKLIST");
                oDbm.AddParameters(1, "@CUSTOMERCODE", sCustomerCode);
                oDbm.AddParameters(2, "@OUTBONDDELIVERYNO", sOutBondDeliveryNo);
                oDbm.AddParameters(3, "@SALESORDERNO", sSalesordernO);
                oDbm.AddParameters(4, "@PRINTEDBY", sCreatedBy);
                oDbm.AddParameters(5, "@SITECODE", sSiteCode);
                oDbm.AddParameters(6, "@LINECODE", sLineCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_FG_CREATE_PICKLIST").Tables[0];
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
