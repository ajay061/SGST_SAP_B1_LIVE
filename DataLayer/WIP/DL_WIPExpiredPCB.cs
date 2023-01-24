using Common;
using System;
using System.Data;

namespace DataLayer.WIP
{
    public class DL_WIPExpiredPCB
    {
        DBManager oDbm;
        public DL_WIPExpiredPCB()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable BindFGitemCode(string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDFGITEMCODE");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_EXPIRED_PCB_ALLOWED").Tables[0];
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
        public DataTable GetData(string sSiteCode,string sWorkOrderNo,string sFGItemCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "GETDATA");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.AddParameters(2, "@WORKORDERNO", sWorkOrderNo);
                oDbm.AddParameters(3, "@FGITEMCODE", sFGItemCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_EXPIRED_PCB_ALLOWED").Tables[0];
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
        public DataTable AllowedPCB(string sAllowedBy, DataTable dtPCBData, string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "ALLOWEDPCB");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.AddParameters(2, "@SCANNEDBY", sAllowedBy);
                oDbm.AddParameters(3, "@DETAILS", dtPCBData);                
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_EXPIRED_PCB_ALLOWED").Tables[0];
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
