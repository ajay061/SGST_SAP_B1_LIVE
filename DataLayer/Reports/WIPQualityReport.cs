using Common;
using System;
using System.Data;

namespace DataLayer.Reports
{
    public class WIPQualityReport
    {
        DBManager oDbm;
        public WIPQualityReport()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable BindFGItemCode()
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(1);
                oDbm.AddParameters(0, "@TYPE", "BINDFGITEMCODE");
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_WIP_QUALITY_REPORT").Tables[0];
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
        public DataTable GetReport(string sFGItemCode, string sFromDate, string sToDate, string sReportType,string sWorkOrderNo)
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(5);
                if (sReportType == "Details")
                {
                    oDbm.AddParameters(0, "@TYPE", "GETREPORT");
                }
                else
                {
                    oDbm.AddParameters(0, "@TYPE", "GETSUMMARYREPORT");
                }
                oDbm.AddParameters(1, "@FGITEMCODE", sFGItemCode);
                oDbm.AddParameters(2, "@FROMDATE", sFromDate);
                oDbm.AddParameters(3, "@TODATE", sToDate);
                oDbm.AddParameters(4, "@WORKORDERNO", sWorkOrderNo);
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_WIP_QUALITY_REPORT").Tables[0];
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


        public DataTable BindReworkFGItemCode()
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(1);
                oDbm.AddParameters(0, "@TYPE", "BINDFGITEMCODE");
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_WIP_QUALITY_REWORK_REPORT").Tables[0];
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
        public DataTable GetREWORKReport(string sFGItemCode, string sFromDate, string sToDate
            , string sWorkOrderNo, string sPartBarcode)
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(6);
                oDbm.AddParameters(0, "@TYPE", "GETREPORT");
                oDbm.AddParameters(1, "@FGITEMCODE", sFGItemCode);
                oDbm.AddParameters(2, "@FROMDATE", sFromDate);
                oDbm.AddParameters(3, "@TODATE", sToDate);
                oDbm.AddParameters(4, "@WORKORDERNO", sWorkOrderNo);
                oDbm.AddParameters(5, "@BARCODE", sPartBarcode);
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_WIP_QUALITY_REWORK_REPORT").Tables[0];
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
