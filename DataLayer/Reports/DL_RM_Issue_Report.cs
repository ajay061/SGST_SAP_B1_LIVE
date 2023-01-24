using Common;
using System;
using System.Data;

namespace DataLayer.Reports
{
    public class DL_RM_Issue_Report
    {
        DBManager oDbm;
        public DL_RM_Issue_Report()
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
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_RI_ISSUE_REPORT").Tables[0];
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

        public DataTable BindFGItemCode()
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(1);
                oDbm.AddParameters(0, "@TYPE", "BINDFGITEMCODE");
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_RM_BOM_HEADER_REPORT").Tables[0];
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
        public DataTable GetReport(string sFromDate, string sTODate, string sIssueSlipNo
            ,string sItemCode,string sGRPONo,string sBatchNo
            )
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(7);
                oDbm.AddParameters(0, "@TYPE", "GETREPORT");
                oDbm.AddParameters(1, "@FROMDATE", sFromDate);
                oDbm.AddParameters(2, "@TODATE", sTODate);
                oDbm.AddParameters(3, "@ISSULESLIPNO", sIssueSlipNo);
                oDbm.AddParameters(4, "@PARTCODE", sItemCode);
                oDbm.AddParameters(5, "@GRPONO", sGRPONo);
                oDbm.AddParameters(6, "@BATCHNO", sBatchNo);
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_RI_ISSUE_REPORT").Tables[0];
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

        public DataTable GetMasterDataReport(string sFromDate, string sTODate,
            string sIssueSlipNo,string sReportType 
            ,string sFGItemCode        
           )
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(5);
                if (sReportType == "HEADER")
                {
                    oDbm.AddParameters(0, "@TYPE", "GETHEADERREPORT");
                }
                else
                {
                    oDbm.AddParameters(0, "@TYPE", "GETDETAILREPORT");
                }
                oDbm.AddParameters(1, "@FROMDATE", sFromDate);
                oDbm.AddParameters(2, "@TODATE", sTODate);
                oDbm.AddParameters(3, "@ISSULESLIPNO", sIssueSlipNo);
                oDbm.AddParameters(4, "@FGITEMCODE", sFGItemCode);
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_RM_BOM_HEADER_REPORT").Tables[0];
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
