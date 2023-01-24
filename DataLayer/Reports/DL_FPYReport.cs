using Common;
using System;
using System.Data;

namespace DataLayer.Reports
{
    public class DL_FPYReport
    {
        DBManager oDbm;
        public DL_FPYReport()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable BindFGItemCode(string sHeaderValue)
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(1);
                oDbm.AddParameters(0, "@TYPE", "BINDFGITEMCODE");
                if (sHeaderValue == "FPY")
                {
                    dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_FPY_REPORT").Tables[0];
                }
                else if (sHeaderValue == "XRAY")
                {
                    dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_XRAY_SAMPLING_REPORT").Tables[0];
                }
                else
                {
                    dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_PDI_ACCESSORIES_REPORT").Tables[0];
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
        public DataTable GetReport(string sFromDate, string sTODate, string sFGItemCode,string sHeaderValue)
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "GETREPORT");
                oDbm.AddParameters(1, "@FROMDATE", sFromDate);
                oDbm.AddParameters(2, "@TODATE", sTODate);
                oDbm.AddParameters(3, "@FGITEMCODE", sFGItemCode);
                if (sHeaderValue == "FPY")
                {
                    dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_FPY_REPORT").Tables[0];
                }
                else if (sHeaderValue == "XRAY")
                {
                    dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_XRAY_SAMPLING_REPORT").Tables[0];
                }
                else
                {
                    dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_PDI_ACCESSORIES_REPORT").Tables[0];
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
