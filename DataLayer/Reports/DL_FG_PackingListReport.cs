using Common;
using System;
using System.Data;
namespace DataLayer.Reports
{
    public class DL_FG_PackingListReport
    {
        DBManager oDbm;
        public DL_FG_PackingListReport()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable BindSalesOrder()
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(1);
                oDbm.AddParameters(0, "@TYPE", "BINDSALESORDER");
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_FG_PACKINGLIST_REPORT").Tables[0];
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

        public DataTable BindPickListNo(string sSalesOrderNo)
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDPICKLIST");
                oDbm.AddParameters(1, "@SALESORDERNO", sSalesOrderNo);
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_FG_PACKINGLIST_REPORT").Tables[0];
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

        public DataTable BindPackinglistNo(string sPickListnO)
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDPACKINGLISTNO");
                oDbm.AddParameters(1, "@PICKLISTNO", sPickListnO);
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_FG_PACKINGLIST_REPORT").Tables[0];
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
        public DataTable GetReport(string sSalesOrderno,string sPickListnO,string sPackinglistNo)
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "GETREPORT");
                oDbm.AddParameters(1, "@SALESORDERNO", sSalesOrderno);
                oDbm.AddParameters(2, "@PICKLISTNO", sPickListnO);
                oDbm.AddParameters(3, "@PACKINGLIST", sPackinglistNo);
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_FG_PACKINGLIST_REPORT").Tables[0];
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
