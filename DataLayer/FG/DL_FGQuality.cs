using Common;
using System;
using System.Data;
using System.Text;

namespace DataLayer
{
    public class DL_FGQuality
    {
        DBManager oDbm;
        public DL_FGQuality()
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
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_FG_QUALITY").Tables[0];
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
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_FG_QUALITY").Tables[0];
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

        public DataTable ValidateSecondaryBox(string sFGitemCode, string sBoxID, string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "VALIDATESECONDARYBOX");
                oDbm.AddParameters(1, "@FGITEMCODE", sFGitemCode);
                oDbm.AddParameters(2, "@BOXBARCODE", sBoxID);
                oDbm.AddParameters(3, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_FG_QUALITY").Tables[0];
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

        public DataTable ValidatePCBBarcode(string sFGitemCode, string sPCBBarcode, string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "VALIDATEPCBBARCODE");
                oDbm.AddParameters(1, "@FGITEMCODE", sFGitemCode);
                oDbm.AddParameters(2, "@PCBBARCODE", sPCBBarcode);
                oDbm.AddParameters(3, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_FG_QUALITY").Tables[0];
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

        public DataTable dtDOCQualityUpdate(DataTable dtQualityScannedData, string sFGItemCode, string sModuletype, string sScannedBy, string sSiteCode,
          string sStatus, string sLocation, string sCode, string stype, string sLineCode
           )
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(11);
                oDbm.AddParameters(0, "@TYPE", "SAVEDATA");
                oDbm.AddParameters(1, "@STATUS", sStatus);
                oDbm.AddParameters(2, "@MODULENAME", sModuletype);
                oDbm.AddParameters(3, "@FGITEMCODE", sFGItemCode);
                oDbm.AddParameters(4, "@SCANNED_BY", sScannedBy);
                oDbm.AddParameters(5, "@DTQUALITYDATA", dtQualityScannedData);
                oDbm.AddParameters(6, "@SITECODE", sSiteCode);
                oDbm.AddParameters(7, "@LOCATION", sLocation);
                oDbm.AddParameters(8, "@CODE", sCode);
                oDbm.AddParameters(9, "@LOCTYPE", stype);
                oDbm.AddParameters(10, "@LINECODE", sLineCode);
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_FG_QUALITY").Tables[0];
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
