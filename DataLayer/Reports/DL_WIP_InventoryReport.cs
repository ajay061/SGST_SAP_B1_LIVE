using Common;
using System;
using System.Data;
namespace DataLayer.Reports
{
    public class DL_WIP_InventoryReport
    {
        DBManager oDbm;
        public DL_WIP_InventoryReport()
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
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_WIP_INVENTORY_REPORT").Tables[0];
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
        public DataTable GetReport(string sWorkOrderNo, string sPartCode,string SGRPONo,string sBatchNo)
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(5);
                oDbm.AddParameters(0, "@TYPE", "GETREPORT");
                oDbm.AddParameters(1, "@WORKORDERNO", sWorkOrderNo);
                oDbm.AddParameters(2, "@PARTCODE", sPartCode);
                oDbm.AddParameters(3, "@GRPONO", SGRPONo);
                oDbm.AddParameters(4, "@BATCHNO", sBatchNo);
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_WIP_INVENTORY_REPORT").Tables[0];
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
