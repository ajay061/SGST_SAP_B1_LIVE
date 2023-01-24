using Common;
using System;
using System.Data;
namespace DataLayer.Reports
{
    public class DL_MasterReport
    {
        DBManager oDbm;
        public DL_MasterReport()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable GetReport(string sSiteCode, string sType,string sFilterReport)
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", sType);
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.AddParameters(2, "@FILTERDATA", sFilterReport);
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_MASTER_DATA_REPORT").Tables[0];
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
