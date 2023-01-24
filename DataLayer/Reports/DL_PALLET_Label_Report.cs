using Common;
using System;
using System.Data;

namespace DataLayer.Reports
{
    public class DL_PALLET_Label_Report
    {
        DBManager oDbm;
        public DL_PALLET_Label_Report()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable dtBindPackingListno()
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(1);
                oDbm.AddParameters(0, "@TYPE", "BINDPACKINGLISTNO");
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_WIP_PACKING_LABEL_REPORT").Tables[0];
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
        public DataTable GetReport(string sPicklistNo)
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@PACKINGLIST", sPicklistNo);
                oDbm.AddParameters(1, "@TYPE", "GETREPORT");
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_WIP_PACKING_LABEL_REPORT").Tables[0];
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
