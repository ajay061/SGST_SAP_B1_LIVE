using Common;
using DataLayer.Reports;
using System;
using System.Data;

namespace BusinessLayer.Reports
{
    public class BL_FG_Box_Label_Report
    {
        DL_FG_Box_Label_Report dlobj;
        public DataTable BindBoxID()
        {
            DataTable dtGRN = new DataTable();
            dlobj = new DL_FG_Box_Label_Report();
            try
            {
                dtGRN = dlobj.BindBoxID();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtGRN;
        }
        public DataTable GetReport(string sBoxID)
        {
            DataTable dtGRN = new DataTable();
            dlobj = new DL_FG_Box_Label_Report();
            try
            {
                dtGRN = dlobj.getBoxLabel(sBoxID);
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
