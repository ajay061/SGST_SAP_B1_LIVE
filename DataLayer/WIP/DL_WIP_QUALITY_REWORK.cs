using Common;
using System;
using System.Data;
namespace DataLayer.WIP
{
    public class DL_WIP_QUALITY_REWORK
    {
        DBManager oDbm;
        public DL_WIP_QUALITY_REWORK()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable BindDefect()
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@MODULETYPE", "BINDDEFECT");
                oDbm.AddParameters(1, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_QUALITY_REWORK").Tables[0];
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
        public DataTable dlQualityRework(string sType, string sPCBBarcode, string sDefect, string sObservation,
          string sRemarks, string sPcbMove, string sScannedBy,string sSiteCode,string sLineCode
          )
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(10);
                oDbm.AddParameters(0, "@TYPE", sType);
                oDbm.AddParameters(1, "@PCBBARCODE", sPCBBarcode);
                oDbm.AddParameters(2, "@DEFECT", sDefect);
                oDbm.AddParameters(3, "@OBSERVATION", sObservation);
                oDbm.AddParameters(4, "@REMARKS", sRemarks);
                oDbm.AddParameters(5, "@PCBMOVE", sPcbMove);
                oDbm.AddParameters(6, "@SCANNED_BY", sScannedBy);
                oDbm.AddParameters(7, "@MODULETYPE", "QUALITY_REWORK");
                oDbm.AddParameters(8, "@SITECODE", sSiteCode);
                oDbm.AddParameters(9, "@LINECODE",sLineCode);
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_WIP_QUALITY_REWORK").Tables[0];
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
