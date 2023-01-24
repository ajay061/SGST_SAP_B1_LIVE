using Common;
using System;
using System.Data;

namespace DataLayer.Reports
{
    public class DL_WIP_ReworkReport
    {
        DBManager oDbm;
        public DL_WIP_ReworkReport()
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
                oDbm.AddParameters(0, "@Condition", "BINDMACHINEID");
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_WIP_ReworkReport").Tables[0];
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
                oDbm.AddParameters(0, "@Condition", "BINDFGITEMCODE");
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_WIP_ReworkReport").Tables[0];
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
        public DataTable dtReworkStationId()
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(1);
                oDbm.AddParameters(0, "@Condition", "BINDREWORKSTATIONID");
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_WIP_ReworkReport").Tables[0];
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
            , string sFgItemCode, string sType,string sWorkOrderNo,string sPCBID, string sReworkStID)
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(8);
                oDbm.AddParameters(0, "@FGITEMCODE", sFgItemCode);
                oDbm.AddParameters(1, "@FROMDATE", sFromDate);
                oDbm.AddParameters(2, "@TODATE", sTODate);
                oDbm.AddParameters(3, "@Condition", sType);
                oDbm.AddParameters(4, "@MACHINEID", sMachineID);
                oDbm.AddParameters(5, "@WORK_ORDER_NO", sWorkOrderNo);
                oDbm.AddParameters(6, "@PART_BARCODE", sPCBID);
                oDbm.AddParameters(7, "@sReworkStID", sReworkStID);
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_WIP_ReworkReport").Tables[0];
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
    }
}
