using Common;
using System;
using System.Data;

namespace DataLayer.Reports
{
    public class DL_PCBTrackingReport
    {
        DBManager oDbm;
        public DL_PCBTrackingReport()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable dtBindMachineID()
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(1);
                oDbm.AddParameters(0, "@CONDITION", "BINDMACHINEID");
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_PCB_TRACKING_REPORT").Tables[0];
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
        public DataTable dtBindFGItemCode()
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(1);
                oDbm.AddParameters(0, "@CONDITION", "BINDFGITEMCODE");
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_PCB_TRACKING_REPORT").Tables[0];
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
        public DataTable GetReportData(string sMachineID,
            string sFromDate, string sTODate
            , string sFgItemCode, string sType
            ,string sBarcode,string sWorkOrderNo,string sReportType)
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(8);
                oDbm.AddParameters(0, "@FGITEMCODE", sFgItemCode);
                oDbm.AddParameters(1, "@FROMDATE", sFromDate);
                oDbm.AddParameters(2, "@TODATE", sTODate);
                oDbm.AddParameters(3, "@TYPE", sType);
                oDbm.AddParameters(4, "@MACHINEID", sMachineID);
                oDbm.AddParameters(5, "@PART_BARCODE", sBarcode);
                oDbm.AddParameters(6, "@CONDITION", "GETREPORT");
                oDbm.AddParameters(7, "@WORKORDERNO", sWorkOrderNo);
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.MethodBase.GetCurrentMethod().Name,
                 "PCB Tracking Report : Data : " + sBarcode + ", FGITEMCODE : " + sFgItemCode + ", Machine ID :" + sMachineID
                 +", Report Type " + sType + ", Work Order No :" + sWorkOrderNo
                 );
                if (sReportType.ToUpper() == "DETAILS")
                {
                    dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_PCB_TRACKING_REPORT").Tables[0];
                }
                else
                {
                    dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_PCB_TRACKING_REPORT_SUMMARY").Tables[0];
                }
                if (dtResult.Rows.Count > 0)
                {
                    PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.MethodBase.GetCurrentMethod().Name,
               "PCB Tracking Report : Data : " + sBarcode + ", FGITEMCODE : " + sFgItemCode + ", Machine ID :" + sMachineID
               + ", Report Type " + sType + ", Work Order No :" + sWorkOrderNo
               + ", Output:" + dtResult.Rows[0][0].ToString()
               );
                }
                else
                {
                    PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.MethodBase.GetCurrentMethod().Name,
              "PCB Tracking Report : Data : " + sBarcode + ", FGITEMCODE : " + sFgItemCode + ", Machine ID :" + sMachineID
              + ", Report Type " + sType + ", Work Order No :" + sWorkOrderNo
              + ", Output:No Result found");
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
