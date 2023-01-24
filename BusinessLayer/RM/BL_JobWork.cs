using Common;
using DataLayer;
using System;
using System.Data;
using System.Text;

namespace BusinessLayer
{
    public class BL_JobWork
    {
        DL_JobWork dlboj = null;      
        public DataTable BindOrderNo(string sSiteCode)
        {
            DataTable dtOrderNo = new DataTable();
            try
            {
                dlboj = new DL_JobWork();
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
                dlboj = new DL_JobWork();
                dtOrderNo = dlboj.BindPartCode(sOrderno, sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtOrderNo;
        }
        public DataTable BindItemLineNo(string sOrderNo,string sPartCode,string sSiteCode)
        {
            DataTable dtOrderNo = new DataTable();
            try
            {
                dlboj = new DL_JobWork();
                dtOrderNo = dlboj.BindLineno(sOrderNo, sPartCode, sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtOrderNo;
        }
        public DataTable GetDetails(string sOrderNo, string sPartCode,string sLineNo,string sSiteCode)
        {
            DataTable dtOrderNo = new DataTable();
            try
            {
                dlboj = new DL_JobWork();
                dtOrderNo = dlboj.GetDetails(sOrderNo, sPartCode, sLineNo, sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtOrderNo;
        }

        public DataTable SaveJobBarcode(string sOrderNo, string sPartCode, string sLineNo
            , string sLocationCode, string sScannedBarcode, string UserID, string sSiteCode
            ,string sLineCode
            )
        {
            DataTable dtOrderNo = new DataTable();
            try
            {
                dlboj = new DL_JobWork();
                dtOrderNo = dlboj.ValidateJobBarcode(sOrderNo, sPartCode, sLineNo
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

