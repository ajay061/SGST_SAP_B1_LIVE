using Common;
using System;
using System.Data;

namespace DataLayer.Reports
{
    public class DL_FG_Box_Label_Report
    {
        DBManager oDbm;
        public DL_FG_Box_Label_Report()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }

        public DataTable BindBoxID()
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(1);
                oDbm.AddParameters(0, "@TYPE", "BINDBOXID");
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_WIP_BOX_LABEL_REPORT").Tables[0];
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
        public DataTable getBoxLabel(string sBoxID)
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@boxID", sBoxID);
                oDbm.AddParameters(1, "@TYPE", "GETREPORT");
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_WIP_BOX_LABEL_REPORT").Tables[0];
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
