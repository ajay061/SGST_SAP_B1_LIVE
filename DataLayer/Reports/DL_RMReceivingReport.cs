using Common;
using System;
using System.Data;

namespace DataLayer.Reports
{
    public class DL_RMReceivingReport
    {
        DBManager oDbm;
        public DL_RMReceivingReport()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        
        public DataTable BindPartCode()
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(1);
                oDbm.AddParameters(0, "@TYPE", "BINDPARTCODE");
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_RM_RECEVING_REPORT").Tables[0];
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
            return dtResult;
        }
        public DataTable GetReport(string sPartCode, string sGRPONO, string sLocationCode,string sSearchData
            ,string sFromDate,string sToDate
            )
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(7);
                oDbm.AddParameters(0, "@TYPE", "GETREPORT");
                oDbm.AddParameters(1, "@PARTCODE", sPartCode);
                oDbm.AddParameters(2, "@GRPONO", sGRPONO);
                oDbm.AddParameters(3, "@LOCATION", sLocationCode);
                oDbm.AddParameters(4, "@GRPOSTATUS", sSearchData);
                oDbm.AddParameters(5, "@FROMDATE", sFromDate);
                oDbm.AddParameters(6, "@TODATE", sToDate);
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_RM_RECEVING_REPORT").Tables[0];
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
            return dtResult;
        }
    }
}
