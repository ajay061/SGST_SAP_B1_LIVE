using Common;
using System;
using System.Data;

namespace DataLayer.FG
{
    public class DL_GENERATE_PACKING_SLIP
    {
        DBManager oDbm;
        public DL_GENERATE_PACKING_SLIP()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable dlBindData(string sType, string sCustomerCode,
            string sOutBondDeliveryNo, string sInvoiceNo
            )
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", sType);
                oDbm.AddParameters(1, "@CUSTOMERCODE", sCustomerCode);
                oDbm.AddParameters(2, "@OUTBONDDELIVERYNO", sOutBondDeliveryNo);
                oDbm.AddParameters(3, "@INVOICENO", sInvoiceNo);
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_FG_SAVE_PACKING_SLIP").Tables[0];
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
            return dtResult;
        }
        public DataTable dlSavePackingSlipData(string sType, string sCustomerCode,
            string sOutBondDeliveryNo, string sInvoiceNo,
                string FLIGHT_NO,
                string PORT_OF_LOADING,
                string PLACE_OF_RECEIPT,
                string PRE_CARRAGED_BY,
                string PORT_OF_DISCHARGED,
                string FINAL_DESTINATION,
                decimal G_WT,
                decimal N_WT,
                string DIMESION_OF_CARGO
            )
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(13);
                oDbm.AddParameters(0, "@TYPE", sType);
                oDbm.AddParameters(1, "@CUSTOMERCODE", sCustomerCode);
                oDbm.AddParameters(2, "@OUTBONDDELIVERYNO", sOutBondDeliveryNo);
                oDbm.AddParameters(3, "@INVOICENO", sInvoiceNo);
                oDbm.AddParameters(4, "@FLIGHT_NO", FLIGHT_NO);
                oDbm.AddParameters(5, "@PORT_OF_LOADING", PORT_OF_LOADING);
                oDbm.AddParameters(6, "@PLACE_OF_RECEIPT", PLACE_OF_RECEIPT);
                oDbm.AddParameters(7, "@PRE_CARRAGED_BY", PRE_CARRAGED_BY);
                oDbm.AddParameters(8, "@PORT_OF_DISCHARGED", PORT_OF_DISCHARGED);
                oDbm.AddParameters(9, "@FINAL_DESTINATION", FINAL_DESTINATION);
                oDbm.AddParameters(10, "@G_WT", G_WT);
                oDbm.AddParameters(11, "@N_WT", N_WT);
                oDbm.AddParameters(12, "@DIMESION_OF_CARGO", DIMESION_OF_CARGO);
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_FG_SAVE_PACKING_SLIP").Tables[0];
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
            return dtResult;
        }

        public DataTable dlBindReportDeliveryNO()
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(1);
                oDbm.AddParameters(0, "@TYPE", "BINDREPORTORDERDELIVERYNO");
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_FG_SAVE_PACKING_SLIP").Tables[0];
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
            return dtResult;
        }

        public DataTable dlBindReportInvoiceNO()
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(1);
                oDbm.AddParameters(0, "@TYPE", "BINDREPORTINVOICENO");
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_FG_SAVE_PACKING_SLIP").Tables[0];
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
            return dtResult;
        }

        public DataTable dlGetPackingSlipReport(string sFromDate, string sTODate,
            string sOutBondDeliveryNo, string sInvoiceNo
          )
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(5);
                oDbm.AddParameters(0, "@TYPE", "GETREPORT");
                oDbm.AddParameters(1, "@FROMDATE", sFromDate);
                oDbm.AddParameters(2, "@TODATE", sTODate);
                oDbm.AddParameters(3, "@OUTBONDDELIVERYNO", sOutBondDeliveryNo);
                oDbm.AddParameters(4, "@INVOICENO", sInvoiceNo);
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_FG_SAVE_PACKING_SLIP").Tables[0];
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
            return dtResult;
        }
    }
}
