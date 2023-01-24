using Common;
using System;
using System.Data;

namespace DataLayer.WIP
{
    public class DL_WIP_DRYProcess
    {
        DBManager oDbm;
        public DL_WIP_DRYProcess()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable dlDryProcess(string stype,string sModuleType,string sPartBarcode,
            int iExpiryDays,string sSiteCode,string sScannedBy)
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(6);
                oDbm.AddParameters(0, "@TYPE", stype);
                oDbm.AddParameters(1, "@MODULETYPE", sModuleType);
                oDbm.AddParameters(2, "@PART_BARCODE", sPartBarcode);
                oDbm.AddParameters(3, "@EXPIRYINCREASE", iExpiryDays);
                oDbm.AddParameters(4, "@SITECODE", sSiteCode);
                oDbm.AddParameters(5, "@SCANNED_BY", sScannedBy);
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_DRYPROCESS").Tables[0];
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
