using Common;
using System;
using System.Data;
namespace DataLayer.Reports
{
    public class DL_RM_Tracking_Report
    {
        DBManager oDbm;
        public DL_RM_Tracking_Report()
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
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_RM_INVENTORY_TRACKING_REPORT").Tables[0];
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
        public DataTable GetReport(string sPartCode,string sLocationType,string sLocationCode
            ,string sGRPONO,string sBatchNo
            )
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(6);
                oDbm.AddParameters(0, "@TYPE", "GETREPORT");
                oDbm.AddParameters(1, "@PARTCODE", sPartCode);
                oDbm.AddParameters(2, "@LOCATIONTYPE", sLocationType);
                oDbm.AddParameters(3, "@LOCATION", sLocationCode);
                oDbm.AddParameters(4, "@GRPONO", sGRPONO);
                oDbm.AddParameters(5, "@BATCHNO", sBatchNo);
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_RM_INVENTORY_TRACKING_REPORT").Tables[0];
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
