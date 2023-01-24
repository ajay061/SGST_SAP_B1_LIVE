using Common;
using System;
using System.Data;

namespace DataLayer.WIP
{
    public class DL_WIP_PDIQuality
    {
        DBManager oDbm;
        public DL_WIP_PDIQuality()
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
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_PDI_QUALITY").Tables[0];
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
        public DataTable BindReworkstation(string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDREWORKSTATION");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_PDI_QUALITY").Tables[0];
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
        public DataTable BindDefect(string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDDEFECT");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_PDI_QUALITY").Tables[0];
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
        public DataTable VaildateBarcode(string sPartBarcode, string FGItemCode, string sSiteCode, string sLineCode
           )
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(5);
                oDbm.AddParameters(0, "@PART_BARCODE", sPartBarcode);
                oDbm.AddParameters(1, "@FGITEMCODE", FGItemCode);
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                oDbm.AddParameters(3, "@TYPE", "PDIVALIDATEBARCODE");
                oDbm.AddParameters(4, "@LINECODE", sLineCode);
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_PDI_QUALITY").Tables[0];
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
        public DataTable SaveData(string sPartBarcode, string FGItemCode
         , string sDefect, string sReworkStation, string sObservation, string sStatus
            , string sSiteCode, string sLineCode, string sUserID
            )
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(10);
                oDbm.AddParameters(0, "@PART_BARCODE", sPartBarcode);
                oDbm.AddParameters(1, "@FGITEMCODE", FGItemCode);
                oDbm.AddParameters(2, "@DEFECT", sDefect);
                oDbm.AddParameters(3, "@REWORKSTATIONID", sReworkStation);
                oDbm.AddParameters(4, "@OBSERVATION", sObservation);
                oDbm.AddParameters(5, "@STATUS", sStatus);
                oDbm.AddParameters(6, "@SITECODE", sSiteCode);
                oDbm.AddParameters(7, "@LINECODE", sLineCode);
                oDbm.AddParameters(8, "@QUALITYBY", sUserID);
                oDbm.AddParameters(9, "@TYPE", "SAVEQUALITY");
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_PDI_QUALITY").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError,
                   System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
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
