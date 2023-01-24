using Common;
using System;
using System.Data;
using DataLayer.RM;

namespace BusinessLayer.RM
{
    public class BL_RMUWGoodsIssueToShopFlor_StockTransfer
    {
        DL_RMUWGoodsIssueToShopFlor_StockTransfer dlboj = null;
        public DataTable BindOrderNo(string sSiteCode)
        {
            DataTable dtOrderNo = new DataTable();
            try
            {
                dlboj = new DL_RMUWGoodsIssueToShopFlor_StockTransfer();
                dtOrderNo = dlboj.BindPONO(sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtOrderNo;
        }
        public DataTable BindPartCode(string sOrderno,string sSiteCode)
        {
            DataTable dtOrderNo = new DataTable();
            try
            {
                dlboj = new DL_RMUWGoodsIssueToShopFlor_StockTransfer();
                dtOrderNo = dlboj.BindPartCode(sOrderno, sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtOrderNo;
        }
        public DataTable BindItemLineNo(string sOrderNo, string sPartCode,string sSiteCode)
        {
            DataTable dtOrderNo = new DataTable();
            try
            {
                dlboj = new DL_RMUWGoodsIssueToShopFlor_StockTransfer();
                dtOrderNo = dlboj.BindLineno(sOrderNo, sPartCode, sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtOrderNo;
        }
        public DataTable GetDetails(string sOrderNo, string sPartCode, string sLineNo,string sSiteCode)
        {
            DataTable dtOrderNo = new DataTable();
            try
            {
                dlboj = new DL_RMUWGoodsIssueToShopFlor_StockTransfer();
                dtOrderNo = dlboj.GetDetails(sOrderNo, sPartCode, sLineNo, sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtOrderNo;
        }

        public DataTable SaveCustomerReturnData(string sOrderNo, string sPartCode, string sLineNo
            , string sLocationCode, string sScannedBarcode, string UserID, string sSiteCode
            ,string sLineCode
            )
        {
            DataTable dtOrderNo = new DataTable();
            try
            {
                dlboj = new DL_RMUWGoodsIssueToShopFlor_StockTransfer();
                dtOrderNo = dlboj.ValidateCustomerData(sOrderNo, sPartCode, sLineNo
                    , sLocationCode, sScannedBarcode, UserID, sSiteCode, sLineCode
                    );
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtOrderNo;
        }
    }
}
