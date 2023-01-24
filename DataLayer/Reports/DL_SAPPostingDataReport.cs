using Common;
using System;
using System.Data;

namespace DataLayer.Reports
{
    public class DL_SAPPostingDataReport
    {
        DBManager oDbm;
        public DL_SAPPostingDataReport()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable GetReportData(string sType, string sOrderNo)
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", sType);
                oDbm.AddParameters(1, "@ORDERNO", sOrderNo);
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_SAP_POSTING_REPORT").Tables[0];
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
        public DataTable GetReprintData(string sType, string sFromDate, string sTODate)
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", sType);
                oDbm.AddParameters(1, "@FROMDATE", sFromDate);
                oDbm.AddParameters(2, "@TODATE", sTODate);
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_Reprint_Report").Tables[0];
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
        public DataTable GetASNWifiAPSAPData(string txtInvoiceNo, string TextModelNo)
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@PONO", txtInvoiceNo);
                oDbm.AddParameters(1, "@MODEL_CODE", TextModelNo);

                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_ASN_REPORT_Wifi_AP_SAP").Tables[0];
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
