using Common;
using System;
using System.Data;

namespace DataLayer.WIP
{
    public class DL_WIPRework
    {
        DBManager oDbm;
        public DL_WIPRework()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable BindReworkStation(string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@MODULETYPE", "BINDREWORKSTATIONID");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_REWORK").Tables[0];
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

        public DataTable BindDefect(string sMachineiD,string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@MODULETYPE", "BINDDEFECT");
                oDbm.AddParameters(1, "@MACHINEID", sMachineiD);
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_REWORK").Tables[0];
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

        public DataTable ValidateRejectedPCB(string sPartBarcode,string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@MODULETYPE", "BINDREJECTEDPCB");
                oDbm.AddParameters(1, "@PCBBARCODE", sPartBarcode);
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_REWORK").Tables[0];
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

        public DataTable ValidateJioFGItem(string sFGITemCode, string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@MODULETYPE", "VALIDATEFGITEMCODE");
                oDbm.AddParameters(1, "@FGITEMCODE", sFGITemCode);
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_REWORK").Tables[0];
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

        public DataTable ValidateBarcode(string _sPartBarcode, string _sReworkStation,
            string sType, string _sScannedby
            ,string sSiteCode,string sLineCode
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(7);
                oDbm.AddParameters(0, "@MODULETYPE", "VALIDATEBARCODE");
                oDbm.AddParameters(1, "@PCBBARCODE", _sPartBarcode);
                oDbm.AddParameters(2, "@REWORKSTATION", _sReworkStation);
                oDbm.AddParameters(3, "@TYPE", sType);
                oDbm.AddParameters(4, "@SCANNED_BY", _sScannedby);
                oDbm.AddParameters(5, "@SITECODE", sSiteCode);
                oDbm.AddParameters(6, "@LINECODE", sLineCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_REWORK").Tables[0];
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

        public DataTable RepairOut(string _sPartBarcode, string _sReworkStation,
           string sType, string _sScannedby
            ,string _sobser, string _sRemrk, string sDefect, string sPCBType
            ,string sIsScraped,string sSiteCode,string sLineCode,DataTable dtReworkData
            ,string sMovingStage,string sJIORepairType
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(15);
                oDbm.AddParameters(0, "@MODULETYPE", "REPAIROUT");
                oDbm.AddParameters(1, "@PCBBARCODE", _sPartBarcode);
                oDbm.AddParameters(2, "@REWORKSTATION", _sReworkStation);
                oDbm.AddParameters(3, "@TYPE", sType);
                oDbm.AddParameters(4, "@SCANNED_BY", _sScannedby);
                oDbm.AddParameters(5, "@OBSERVATION", _sobser);
                oDbm.AddParameters(6, "@REMARKS", _sRemrk);
                oDbm.AddParameters(7, "@DEFECT", sDefect);
                oDbm.AddParameters(8, "@PCBMOVE", sPCBType);
                oDbm.AddParameters(9, "@SCRAPED", sIsScraped);
                oDbm.AddParameters(10, "@SITECODE", sSiteCode);
                oDbm.AddParameters(11, "@LINECODE", sLineCode);
                oDbm.AddParameters(12, "@DETAILS", dtReworkData);
                oDbm.AddParameters(13, "@JIOMOVINGSTAGE", sMovingStage);
                oDbm.AddParameters(14, "@JIOREPAIRTYPE", sJIORepairType);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_REWORK").Tables[0];
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
