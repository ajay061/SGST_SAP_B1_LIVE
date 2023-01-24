using System;
using System.Data;
using System.Text;
using DataLayer.FG;
using Common;

namespace BusinessLayer.FG
{
    public class BL_PackingList
    {
        DL_PackingList dlboj = null;
        public DataTable BINDOUTBOUND(string sSiteCode,string sLineCode)
        {
            DataTable dtBindDetails = new DataTable();
            dlboj = new DL_PackingList();
            try
            {
                dtBindDetails = dlboj.BindOutbound(sSiteCode, sLineCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtBindDetails;
        }

        public DataTable Bind_Sales_Order(string sOutboundDelivery,string sSiteCode,string sLineCode)
        {
            DataTable dtBindDetails = new DataTable();
            dlboj = new DL_PackingList();
            try
            {
                dtBindDetails = dlboj.BindSalesOrder(sOutboundDelivery, sSiteCode,sLineCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtBindDetails;
        }
        public DataTable BindPickList(string sSalesOrderNo,string sSiteCode,string sLineCode)
        {
            DataTable dtBindDetails = new DataTable();
            dlboj = new DL_PackingList();
            try
            {
                dtBindDetails = dlboj.BindPicklistNo(sSalesOrderNo, sSiteCode,sLineCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtBindDetails;
        }

        public DataTable BindItemCode(string sSalesOrderNo, string sPicklistNo
            ,string sSiteCode,string sLineCode
            )
        {
            DataTable dtSalesOrderNo = new DataTable();
            dlboj = new DL_PackingList();
            try
            {
                dtSalesOrderNo = dlboj.BindItemCode(sSalesOrderNo, sPicklistNo
                    ,sSiteCode,sLineCode
                    );
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtSalesOrderNo;
        }

        public DataTable BINDDETAILS(string sSalesOrderNo, string sPicklistNo, string sItemCode
            ,string sSiteCode,string sLineCode
            )
        {
            DataTable dtBindDetails = new DataTable();
            dlboj = new DL_PackingList();
            try
            {
                dtBindDetails = dlboj.GetDetails(sSalesOrderNo, sPicklistNo, sItemCode, sSiteCode,sLineCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtBindDetails;
        }

        public string sScanLocation(string sSalesOrderNo, string sItemCode, string sPicklistNo, string sLocationBarcode,
           string sScannedBy,string sSiteCode,string sLineCode)
        {
            string sResult = string.Empty;
            DataTable dtBindDetails = new DataTable();
            dlboj = new DL_PackingList();
            try
            {
                dtBindDetails = dlboj.sScanLocation(sSalesOrderNo, sItemCode, sPicklistNo, sLocationBarcode, sScannedBy
                    , sSiteCode,sLineCode
                    );
                if (dtBindDetails.Rows.Count > 0)
                {
                    sResult = dtBindDetails.Rows[0][0].ToString();
                }
                else
                {
                    sResult = "N~No result found for scanned barcode";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }

        public string sScanFGBarcode(string sSalesOrderNo, string sItemCode, string sPicklistNo, string sBoxBarcode,
            string sScannedBy,string sLocationBarcode
            ,string sSiteCode,string sLineCode
            )
        {
            string sResult = string.Empty;
            DataTable dtBindDetails = new DataTable();
            dlboj = new DL_PackingList();
            try
            {
                dtBindDetails = dlboj.ScanFGBarcode(sSalesOrderNo, sItemCode, sPicklistNo, sBoxBarcode, sScannedBy, sLocationBarcode
                    ,sSiteCode,sLineCode
                    );
                if (dtBindDetails.Rows.Count > 0)
                {
                    sResult = dtBindDetails.Rows[0][0].ToString();
                }
                else
                {
                    sResult = "N~No result found for scanned barcode";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }

        public string GeneratePackingListNo(string sSalesOrderNo, string sItemCode, string sPickListNo, string sPackingBy
            ,string sSiteCode,string sLineCode
            )
        {
            string sResult = string.Empty;
            dlboj = new DL_PackingList();
            try
            {
                DataTable dt = dlboj.CreatePackingList(sSalesOrderNo, sItemCode, sPickListNo, sPackingBy
                    , sSiteCode,sLineCode
                    );
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
                    return sResult;
                }
                else
                {
                    sResult = "N~Packlist Generation Failed";
                    return sResult;
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
    }
}
