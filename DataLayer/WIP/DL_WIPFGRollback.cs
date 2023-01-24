using Common;
using System;
using System.Data;

namespace DataLayer.WIP
{
    public class DL_WIPFGRollback
    {
        DBManager oDbm;
        public DL_WIPFGRollback()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }

        public DataTable ScanBoxID(string sBoxID)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "GETDATA");
                oDbm.AddParameters(1, "@BOXID", sBoxID);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_FG_ROLLBACK").Tables[0];
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
            return dt;
        }

        public DataTable RollbackBox(string sBoxID,string sRollbackBy,DataTable dtRollbackData,string sLineCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(5);
                oDbm.AddParameters(0, "@TYPE", "ROLLBACK");
                oDbm.AddParameters(1, "@BOXID", sBoxID);
                oDbm.AddParameters(2, "@ROLLBACKBY", sRollbackBy);
                oDbm.AddParameters(3, "@ROLLBACKDETAILS", dtRollbackData);
                oDbm.AddParameters(4, "@LINECODE", sLineCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_FG_ROLLBACK").Tables[0];
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
            return dt;
        }
    }
}
