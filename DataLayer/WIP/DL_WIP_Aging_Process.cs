using Common;
using System;
using System.Data;

namespace DataLayer.WIP
{
    public class DL_WIP_Aging_Process
    {
        DBManager oDbm;
        public DL_WIP_Aging_Process()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable BindFGItemCode(string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDFGITEMCODE");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);

                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_AGING_PROCESS").Tables[0];
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
        public DataTable AGINGPROCESS(string _Part_Barcode, string sType, string sFGItemCode
            , string sSiteCode, string sUserID, string sLineCode
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(6);
                oDbm.AddParameters(0, "@PART_BARCODE", _Part_Barcode);
                oDbm.AddParameters(1, "@TYPE", sType);
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                oDbm.AddParameters(3, "@LINECODE", sLineCode);
                oDbm.AddParameters(4, "@SCANNED_BY", sUserID);
                oDbm.AddParameters(5, "@FGITEMCODE", sFGItemCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_AGING_PROCESS").Tables[0];
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
