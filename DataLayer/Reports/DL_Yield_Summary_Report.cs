using Common;
using System;
using System.Data;

namespace DataLayer.Reports
{
    public class DL_Yield_Summary_Report
    {
        DBManager oDbm;
        public DL_Yield_Summary_Report()
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
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_YELD_SUMMARY_REPORT").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dtResult;
        }
        public DataTable BindMachine(string sFGItemCode)
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDMACHINEID");
                oDbm.AddParameters(1, "@PARTCODE", sFGItemCode);
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_YELD_SUMMARY_REPORT").Tables[0];
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
        public DataTable GetReport(string sPartCode,string sFromDate,string sToDate,string sReportType,string sMachineID
            ,string sWorkOrderNo
            )
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(6);
                oDbm.AddParameters(0, "@PARTCODE", sPartCode);
                oDbm.AddParameters(1, "@TYPE", "GETREPORT");
                oDbm.AddParameters(2, "@FROM", sFromDate);
                oDbm.AddParameters(3, "@TO", sToDate);
                oDbm.AddParameters(4, "@MACHINEID", sMachineID);
                oDbm.AddParameters(5, "@WORKORDERNO", sWorkOrderNo);
                if (sReportType == "Summary")
                {
                    dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_YELD_SUMMARY_REPORT").Tables[0];
                }
                else
                {
                    dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_YELD_REPORT").Tables[0];
                }               
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
