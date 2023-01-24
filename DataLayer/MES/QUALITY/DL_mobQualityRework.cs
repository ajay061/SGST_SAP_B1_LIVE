using Common;
using System;
using System.Data;
using PL;

namespace DataLayer.MES.QUALITY
{
    public class DL_mobQualityRework
    {
        DBManager oDbm;
        public DL_mobQualityRework()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable BindDefect(PL_Printing plobj)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(1);
                oDbm.AddParameters(0, "@TYPE", "BINDDEFECT");
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_MOB_QUALITY_REWORK").Tables[0];
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
        public DataTable ValidateBarcode(PL_Printing plobj)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(12);
                oDbm.AddParameters(0, "@TYPE", "VALIDATEBARCODE");
                oDbm.AddParameters(1, "@SNMODEL", plobj.iSNModel);
                oDbm.AddParameters(2, "@COLORCODE", plobj.sColorCode);
                oDbm.AddParameters(3, "@FGITEMCODE", plobj.sBOMCode);
                oDbm.AddParameters(4, "@DEFECT", plobj.sDefect);
                oDbm.AddParameters(5, "@OBSERVATION", plobj.sObservation);
                oDbm.AddParameters(6, "@REMARKS", plobj.sRemarks);
                oDbm.AddParameters(7, "@BARCODE", plobj.sSNBarcode);
                oDbm.AddParameters(8, "@MODULESTATUS", plobj.sType);
                oDbm.AddParameters(9, "@SITECODE", plobj.sSiteCode);
                oDbm.AddParameters(10, "@LINECODE", plobj.sUserID);
                oDbm.AddParameters(11, "@REPAIRBY", plobj.sLineCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_MOB_QUALITY_REWORK").Tables[0];
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
