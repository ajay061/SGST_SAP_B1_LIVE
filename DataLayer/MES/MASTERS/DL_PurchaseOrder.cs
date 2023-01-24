using System;
using System.Data;
using PL;
using BcilLib;
using System.Reflection;
using Common;

namespace DataLayer
{
    public class DL_Purchase_Order
    {
        DBManager oDbm;
        public DL_Purchase_Order()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable BindFGItemCode()
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(1);
                oDbm.AddParameters(0, "@TYPE", "BINDFGITEMCODE");
                dt = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_PURCHASE_ORDER_MASTER").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dt;
        }
        public DataTable BindGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(1);
                oDbm.AddParameters(0, "@TYPE", "SELECT");               
                dt = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_PURCHASE_ORDER_MASTER").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dt;
        }

        public DataTable SearchPurchaseOrder(string sPurchaseOrder)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "SEARCH_PO");
                oDbm.AddParameters(1, "@PO_ID", sPurchaseOrder);
                dt = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_PURCHASE_ORDER_MASTER").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dt;
        }

        public DataTable SavePurChaseOrder(PL_PurchaseOrder plObj)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(12);
                oDbm.AddParameters(0, "@TYPE", "INSERT");
                oDbm.AddParameters(1, "@PURCHASE_ORDER", plObj.sPurchaseOrderNo);
                oDbm.AddParameters(2, "@MODELCODE", plObj.sModelCode);
                oDbm.AddParameters(3, "@PURCHASE_DATE", Convert.ToDateTime(plObj.sPurchaseDate));
                oDbm.AddParameters(4, "@PO_QTY", plObj.iPO_QTY);
                oDbm.AddParameters(5, "@CREATED_BY", plObj.CREATED_BY);
                oDbm.AddParameters(6, "@ADDRESS1", plObj.Address1);
                oDbm.AddParameters(7, "@ADDRESS2", plObj.Address2);
                oDbm.AddParameters(8, "@ADDRESS3", plObj.Address3);
                oDbm.AddParameters(9, "@ADDRESS4", plObj.Address4);
                oDbm.AddParameters(10, "@ACTIVE", plObj.Active);
                oDbm.AddParameters(11, "@SITECODE", plObj.sSiteCode);                
                dt = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_PURCHASE_ORDER_MASTER").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dt;
        }

        public DataTable UpdatePurChaseOrder(PL_PurchaseOrder plObj)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(12);
                oDbm.AddParameters(0, "@TYPE", "UPDATE");
                oDbm.AddParameters(1, "@PURCHASE_ORDER", plObj.sPurchaseOrderNo);
                oDbm.AddParameters(2, "@MODELCODE", plObj.sModelCode);
                oDbm.AddParameters(3, "@PURCHASE_DATE", Convert.ToDateTime(plObj.sPurchaseDate));
                oDbm.AddParameters(4, "@PO_QTY", plObj.iPO_QTY);
                oDbm.AddParameters(5, "@CREATED_BY", plObj.CREATED_BY);
                oDbm.AddParameters(6, "@ADDRESS1", plObj.Address1);
                oDbm.AddParameters(7, "@ADDRESS2", plObj.Address2);
                oDbm.AddParameters(8, "@ADDRESS3", plObj.Address3);
                oDbm.AddParameters(9, "@ADDRESS4", plObj.Address4);
                oDbm.AddParameters(10, "@ACTIVE", plObj.Active);
                oDbm.AddParameters(11, "@SITECODE", plObj.sSiteCode);
                dt = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_PURCHASE_ORDER_MASTER").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dt;
        }

        public DataTable DeletePurchaseOrder(string sPurchaseOrder)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "DELETE");
                oDbm.AddParameters(1, "@PO_ID", sPurchaseOrder);
                dt = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_PURCHASE_ORDER_MASTER").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtError, MethodBase.GetCurrentMethod().Name, ex.Message);
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
