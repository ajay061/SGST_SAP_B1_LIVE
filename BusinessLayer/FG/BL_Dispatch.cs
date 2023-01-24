using System;
using System.Data;
using Common;

namespace BusinessLayer.FG
{
    public class BL_Dispatch
    {
        DataLayer.FG.DL_Dispatch dlboj = null;
        public DataTable BindCustomers(string sSiteCode, string sLineCode)
        {
            DataTable dtSalesOrderNo = new DataTable();
            dlboj = new DataLayer.FG.DL_Dispatch();
            try
            {
                dtSalesOrderNo = dlboj.BindCustomerCode(sSiteCode, sLineCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtSalesOrderNo;
        }

        public DataTable BINDOUTBOUND(string sCustomerName, string sSiteCode, string sLineCode)
        {
            DataTable dtSalesOrderNo = new DataTable();
            dlboj = new DataLayer.FG.DL_Dispatch();
            try
            {
                dtSalesOrderNo = dlboj.BindOutbondDeliveryNo(sCustomerName
                    , sSiteCode, sLineCode
                    );
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtSalesOrderNo;
        }
        public DataTable Bind_Sales_Order(string sCustomerName, string sOutboundDelivery
           , string sSiteCode, string sLineCode
            )
        {
            DataTable dtSalesOrderNo = new DataTable();
            dlboj = new DataLayer.FG.DL_Dispatch();
            try
            {
                dtSalesOrderNo = dlboj.BindSalesOrder(sCustomerName, sOutboundDelivery
                    , sSiteCode, sLineCode
                    );
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtSalesOrderNo;
        }
        public DataTable BINDDETAILS(string sOutboundDelivery, string sCustomerName, string sSalesOrderNo
             , string sSiteCode, string sLineCode
            )
        {
            DataTable dtSalesOrderNo = new DataTable();
            dlboj = new DataLayer.FG.DL_Dispatch();
            try
            {
                dtSalesOrderNo = dlboj.GETDETAILS(sCustomerName, sOutboundDelivery, sSalesOrderNo
                    , sSiteCode, sLineCode
                    );
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtSalesOrderNo;
        }
        public string GeneratePickListNo(string sSalesOrderNo, string sPicklistBy
            , string customername, string outboundnumber
            , string sSiteCode, string sLineCode
            )
        {
            dlboj = new DataLayer.FG.DL_Dispatch();
            string sResult = string.Empty;
            try
            {
                DataTable dt = dlboj.GenereatePickList(customername, outboundnumber, sSalesOrderNo, sPicklistBy
                    ,sSiteCode, sLineCode
                    );
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
                }
                else
                {
                    sResult = "N~Picklist No generation failed";
                }

            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }
    }
}
