using Common;
using DataLayer.Reports;
using System;
using System.Data;

namespace BusinessLayer.Reports
{
    public class BL_WIP_TOOL_Report
    {
        DL_WIP_Tool_Report dlobj;
        public DataTable BindTOOl()
        {
            DataTable dtGRN = new DataTable();
            dlobj = new DL_WIP_Tool_Report();
            try
            {
                dtGRN = dlobj.BindTool();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtGRN;
        }
        public DataTable GetReport(string sTool)
        {
            DataTable dtGRN = new DataTable();
            dlobj = new DL_WIP_Tool_Report();
            try
            {
                dtGRN = dlobj.GetReport(sTool);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtGRN;
        }
        public DataTable GetToolDetailsReport(string sFromDate, string sTODate, string sToolID)
        {
            DataTable dtGRN = new DataTable();
            dlobj = new DL_WIP_Tool_Report();
            try
            {
                dtGRN = dlobj.GetToolDetailsReport(sFromDate, sTODate, sToolID);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtGRN;
        }
    }
}
