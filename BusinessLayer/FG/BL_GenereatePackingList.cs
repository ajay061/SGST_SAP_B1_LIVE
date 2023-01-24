using System;
using System.Data;
using Common;
using DataLayer.FG;

namespace BusinessLayer.FG
{
    public class BL_GenereatePackingList
    {
        DL_GENERATE_PACKING_SLIP dlboj = null;
        public DataTable BindData(string sType, string sCustomerCode,
            string sOutBondDeliveryNo, string sInvoiceNo)
        {
            DataTable dtBindData = new DataTable();            
            dlboj = new DL_GENERATE_PACKING_SLIP();
            try
            {
                dtBindData = dlboj.dlBindData(sType, sCustomerCode, sOutBondDeliveryNo, sInvoiceNo);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtBindData;
        }

        public DataTable blSavePackingData(
            string sType, string sCustomerCode,
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
            DataTable dtBindData = new DataTable();
            dlboj = new DL_GENERATE_PACKING_SLIP();
            try
            {
                dtBindData = dlboj.dlSavePackingSlipData(sType, sCustomerCode, sOutBondDeliveryNo, sInvoiceNo,
                     FLIGHT_NO,
                PORT_OF_LOADING,
                PLACE_OF_RECEIPT,
                PRE_CARRAGED_BY,
                PORT_OF_DISCHARGED,
                FINAL_DESTINATION,
                G_WT,
                N_WT,
                DIMESION_OF_CARGO
                    );
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtBindData;
        }

        public DataTable BindReportDeliveryNo()
        {
            DataTable dtBindData = new DataTable();            
            dlboj = new DL_GENERATE_PACKING_SLIP();
            try
            {
                dtBindData = dlboj.dlBindReportDeliveryNO();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtBindData;
        }       
        public DataTable BindReportInvoiceNo()
        {
            DataTable dtBindData = new DataTable();            
            dlboj = new DL_GENERATE_PACKING_SLIP();
            try
            {
                dtBindData = dlboj.dlBindReportInvoiceNO();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtBindData;
        }
        public DataTable GetReport(string sFromDate,string sToDate,string sDeliveryNo,string sInvoiceNo)
        {
            DataTable dtBindData = new DataTable();          
            dlboj = new DL_GENERATE_PACKING_SLIP();
            try
            {
                dtBindData = dlboj.dlGetPackingSlipReport(sFromDate, sToDate, sDeliveryNo
                    , sInvoiceNo
                    );
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtBindData;
        }
    }
}
