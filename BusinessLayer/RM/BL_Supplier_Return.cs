using Common;
using System;
using System.Data;
using DataLayer.RM;
namespace BusinessLayer.RM
{
    public class BL_Supplier_Return
    {
        DL_SupplierReturn dlboj = null;        
        public DataTable BindOrderNo(string sSiteCode)
        {
            DataTable dtOrderNo = new DataTable();
            try
            {
                dlboj = new DL_SupplierReturn();
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
                dlboj = new DL_SupplierReturn();
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
                dlboj = new DL_SupplierReturn();
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
                dlboj = new DL_SupplierReturn();
                dtOrderNo = dlboj.GetDetails(sOrderNo, sPartCode, sLineNo, sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtOrderNo;
        }

        public DataTable SaveSupplierReturnData(string sOrderNo, string sPartCode, string sLineNo
            , string sLocationCode, string sScannedBarcode, string UserID, string sSiteCode,string sLineCode
            )
        {
            DataTable dtOrderNo = new DataTable();
            try
            {
                dlboj = new DL_SupplierReturn();
                dtOrderNo = dlboj.ValidateSupplierData(sOrderNo, sPartCode, sLineNo
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
