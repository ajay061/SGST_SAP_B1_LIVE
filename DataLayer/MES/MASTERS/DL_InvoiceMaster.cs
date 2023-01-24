using System;
using System.Data;
using PL;
using BcilLib;
using System.Reflection;
using Common;

namespace DataLayer.MES.MASTERS
{
    public class DL_InvoiceMaster
    {
        DBManager oDbm;
        public DL_InvoiceMaster()
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
                dt = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_INVOICE_MASTER").Tables[0];
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
        public DataTable BindPurchaseOrerNo(string sModel)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDPURCHASEORDERNO");
                oDbm.AddParameters(1, "@MODEL", sModel);
                dt = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_INVOICE_MASTER").Tables[0];
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
        public DataTable BindShipToAddress(string sModel)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDSHIPTOADDRESS");
                oDbm.AddParameters(1, "@MODEL", sModel);
                dt = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_INVOICE_MASTER").Tables[0];
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
                dt = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_INVOICE_MASTER").Tables[0];
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

        public DataTable SearchInvoiceNo(string sInvoiceNo)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "SEARCH_INVOICE");
                oDbm.AddParameters(1, "@INVOICE_NO", sInvoiceNo);
                dt = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_INVOICE_MASTER").Tables[0];
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

        public DataTable SaveInvoice(PL_invoiceMaster plObj)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(13);
                oDbm.AddParameters(0, "@TYPE", "INSERT");
                oDbm.AddParameters(1, "@PURCHASE_ORDER", plObj.sPurchaseOrderNo);
                oDbm.AddParameters(2, "@MODEL", plObj.sModelCode);
                oDbm.AddParameters(3, "@INVOICE_DATE", Convert.ToDateTime(plObj.sInvoiceDate));
                oDbm.AddParameters(4, "@INVOICE_QTY", plObj.iInvoice_QTY);
                oDbm.AddParameters(5, "@CREATED_BY", plObj.CREATED_BY);
                oDbm.AddParameters(6, "@MSID", plObj.MSMID);
                oDbm.AddParameters(7, "@INVOICE_NO", plObj.sInvoiceNo);
                oDbm.AddParameters(8, "@SITECODE", plObj.sSiteCode);
                oDbm.AddParameters(9, "@INVOICE_BOX_SIZE", plObj.iInvoiceBoxSize);
                oDbm.AddParameters(10, "@STOCK_POINT_NOTE", plObj.STOCK_POINT_NOTE);
                oDbm.AddParameters(11, "@SUPPLIER_CODE", plObj.SUPPLIER_CODE);
                oDbm.AddParameters(12, "@SHIPMENT_DATE", plObj.SHIPMENT_DATE);
                dt = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_INVOICE_MASTER").Tables[0];
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

        public DataTable UpdateInvoice(PL_invoiceMaster plObj)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(13);
                oDbm.AddParameters(0, "@TYPE", "UPDATE");
                oDbm.AddParameters(1, "@PURCHASE_ORDER", plObj.sPurchaseOrderNo);
                oDbm.AddParameters(2, "@MODEL", plObj.sModelCode);
                oDbm.AddParameters(3, "@INVOICE_DATE", Convert.ToDateTime(plObj.sInvoiceDate));
                oDbm.AddParameters(4, "@INVOICE_QTY", plObj.iInvoice_QTY);
                oDbm.AddParameters(5, "@CREATED_BY", plObj.CREATED_BY);
                oDbm.AddParameters(6, "@MSID", plObj.MSMID);
                oDbm.AddParameters(7, "@INVOICE_NO", plObj.sInvoiceNo);
                oDbm.AddParameters(8, "@SITECODE", plObj.sSiteCode);
                oDbm.AddParameters(9, "@INVOICE_BOX_SIZE", plObj.iInvoiceBoxSize);
                oDbm.AddParameters(10, "@STOCK_POINT_NOTE", plObj.STOCK_POINT_NOTE);
                oDbm.AddParameters(11, "@SUPPLIER_CODE", plObj.SUPPLIER_CODE);
                oDbm.AddParameters(12, "@SHIPMENT_DATE", plObj.SHIPMENT_DATE);
                dt = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_INVOICE_MASTER").Tables[0];
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

        public DataTable DeleteInvoice(string sInvoice)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "DELETE");
                oDbm.AddParameters(1, "@INVOICE_NO", sInvoice);
                dt = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_INVOICE_MASTER").Tables[0];
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



        public DataTable dtSaveShippingAddress(PL_invoiceMaster plObj) 
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(11);
                oDbm.AddParameters(0, "@TYPE", "INSERT");                
                oDbm.AddParameters(1, "@MODEL", plObj.sModelCode);                              
                oDbm.AddParameters(2, "@CREATED_BY", plObj.CREATED_BY);                
                oDbm.AddParameters(3, "@SITECODE", plObj.sSiteCode);
                oDbm.AddParameters(4, "@ADDRESS1", plObj.Address1);
                oDbm.AddParameters(5, "@ADDRESS2", plObj.Address2);
                oDbm.AddParameters(6, "@ADDRESS3", plObj.Address3);
                oDbm.AddParameters(7, "@ADDRESS4", plObj.Address4);
                oDbm.AddParameters(8, "@ADDRESS5", plObj.Address5);
                oDbm.AddParameters(9, "@ADDRESS6", plObj.Address6);
                oDbm.AddParameters(10, "@ADDRESS7", plObj.Address7);
                dt = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_ADDRESS_MASTER").Tables[0];
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
        public DataTable dtUpdateShippingAddress(PL_invoiceMaster plObj)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(12);
                oDbm.AddParameters(0, "@TYPE", "UPDATE");
                oDbm.AddParameters(1, "@MODEL", plObj.sModelCode);
                oDbm.AddParameters(2, "@CREATED_BY", plObj.CREATED_BY);
                oDbm.AddParameters(3, "@SITECODE", plObj.sSiteCode);
                oDbm.AddParameters(4, "@ADDRESS1", plObj.Address1);
                oDbm.AddParameters(5, "@ADDRESS2", plObj.Address2);
                oDbm.AddParameters(6, "@ADDRESS3", plObj.Address3);
                oDbm.AddParameters(7, "@ADDRESS4", plObj.Address4);
                oDbm.AddParameters(8, "@ADDRESS5", plObj.Address5);
                oDbm.AddParameters(9, "@ADDRESS6", plObj.Address6);
                oDbm.AddParameters(10, "@ADDRESS7", plObj.Address7);
                oDbm.AddParameters(11, "@MSID", plObj.MSMID);
                dt = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_ADDRESS_MASTER").Tables[0];
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

        public DataTable DeleteAddress(string msmID)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "DELETE");
                oDbm.AddParameters(1, "@MSID", msmID);
                dt = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_ADDRESS_MASTER").Tables[0];
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

        public DataTable BindAddress()
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(1);
                oDbm.AddParameters(0, "@TYPE", "SELECT");
                dt = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_ADDRESS_MASTER").Tables[0];
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

        public DataTable Searchaddress(string msmid)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "SEARCH_ADDRESS");
                oDbm.AddParameters(1, "@MSID", msmid);
                dt = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_ADDRESS_MASTER").Tables[0];
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

    }
}
