using Common;
using System;
using System.Data;

namespace DataLayer.WIP
{
    public class DL_FGRouting
    {
        DBManager oDbm;
        public DL_FGRouting()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable BindLine()
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDLINE");
                oDbm.AddParameters(1, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_ROUTING_MASTER").Tables[0];
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

        public DataTable BindFGItemCode()
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDFGITEMCODE");
                oDbm.AddParameters(1, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_ROUTING_MASTER").Tables[0];
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
        public DataTable GetRouteName(string sFGItemCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "GETROUTENAME");
                oDbm.AddParameters(1, "@SITECODE", PCommon.sSiteCode);
                oDbm.AddParameters(2, "@FGITEMCODE", sFGItemCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_ROUTING_MASTER").Tables[0];
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
        public DataTable BindMachineID(string sLineID)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "BINDMACHINE");
                oDbm.AddParameters(1, "@LINEID", sLineID);
                oDbm.AddParameters(2, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_ROUTING_MASTER").Tables[0];
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
        public DataTable BindProgram(string MACHINEID)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "GETPROGRAMID");
                oDbm.AddParameters(1, "@MACHINEID", MACHINEID);
                oDbm.AddParameters(2, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_ROUTING_MASTER").Tables[0];
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
        public DataTable GetRoutingDetails(string sFGItemCode,string sRouteName)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "GETROUTINGDETAILS");
                oDbm.AddParameters(1, "@FGITEMCODE", sFGItemCode);
                oDbm.AddParameters(2, "@SITECODE", PCommon.sSiteCode);
                oDbm.AddParameters(3, "@ROUTE_NAME", sRouteName);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_ROUTING_MASTER").Tables[0];
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
        public DataTable SaveRouting(string LINEID, string MACHINEID, string FGITEMCODE,
            string UPDATEOUTTIME, string PROFILEID, string SEQUEANCE, string RESEQUENACE, string CREATEBY
            , string sISSFG, string sSFGItemCode, string sEnable, string sOutRequired,string sIsLotCreate
            ,string sRouteName,string sTMOPartCode,string sIsXrayAutoSampledPIcked,string SFGQty
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(19);
                oDbm.AddParameters(0, "@TYPE", "SAVEROUTING");
                oDbm.AddParameters(1, "@LINEID", LINEID);
                oDbm.AddParameters(2, "@MACHINEID", MACHINEID);
                oDbm.AddParameters(3, "@FGITEMCODE", FGITEMCODE);
                oDbm.AddParameters(4, "@UPDATEOUTTIME", UPDATEOUTTIME);
                oDbm.AddParameters(5, "@PROGRAMID", PROFILEID);
                oDbm.AddParameters(6, "@SEQUEANCE", SEQUEANCE);
                oDbm.AddParameters(7, "@RESEQUENACE", RESEQUENACE);
                oDbm.AddParameters(8, "@CREATEDBY", CREATEBY);
                oDbm.AddParameters(9, "@sISSFG", sISSFG);
                oDbm.AddParameters(10, "@sSFGItemCode", sSFGItemCode);
                oDbm.AddParameters(11, "@sEnable", sEnable);
                oDbm.AddParameters(12, "@sOutRequired", sOutRequired);
                oDbm.AddParameters(13, "@SITECODE", PCommon.sSiteCode);
                oDbm.AddParameters(14, "@ISLOTCREATE", sIsLotCreate);
                oDbm.AddParameters(15, "@ROUTE_NAME", sRouteName);
                oDbm.AddParameters(16, "@TMO_PARTCODE", sTMOPartCode);
                oDbm.AddParameters(17, "@XRAYSCANNINGAUTOPICUP", sIsXrayAutoSampledPIcked);
                oDbm.AddParameters(18, "@SFGQty", SFGQty);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_ROUTING_MASTER").Tables[0];
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

        public DataTable EditRoutingDetails(string sFGItemCode, string sMachineID, int sSequence
             , string sRouteName
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(6);
                oDbm.AddParameters(0, "@TYPE", "EDITROUTINGDETAILS");
                oDbm.AddParameters(1, "@FGITEMCODE", sFGItemCode);
                oDbm.AddParameters(2, "@MACHINEID", sMachineID);
                oDbm.AddParameters(3, "@SEQUEANCE", sSequence);
                oDbm.AddParameters(4, "@SITECODE", PCommon.sSiteCode);
                oDbm.AddParameters(5, "@ROUTE_NAME", sRouteName);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_ROUTING_MASTER").Tables[0];
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
        
        public DataTable UpdateRouting(string LINEID, string MACHINEID, string FGITEMCODE,
           string UPDATEOUTTIME, string PROFILEID, string SEQUEANCE, string RESEQUENACE, string CREATEBY
           , string sISSFG, string sSFGItemCode, string sEnable, string sOutRequired,string sIsLotCreate
            ,string sRouteName,string sTMOPartCode,string sIsXrayAutoSampledPIcked
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(18);
                oDbm.AddParameters(0, "@TYPE", "UPDATEROUTING");
                oDbm.AddParameters(1, "@LINEID", LINEID);
                oDbm.AddParameters(2, "@MACHINEID", MACHINEID);
                oDbm.AddParameters(3, "@FGITEMCODE", FGITEMCODE);
                oDbm.AddParameters(4, "@UPDATEOUTTIME", UPDATEOUTTIME);
                oDbm.AddParameters(5, "@PROGRAMID", PROFILEID);
                oDbm.AddParameters(6, "@SEQUEANCE", SEQUEANCE);
                oDbm.AddParameters(7, "@RESEQUENACE", RESEQUENACE);
                oDbm.AddParameters(8, "@CREATEDBY", CREATEBY);
                oDbm.AddParameters(9, "@sISSFG", sISSFG);
                oDbm.AddParameters(10, "@sSFGItemCode", sSFGItemCode);
                oDbm.AddParameters(11, "@sEnable", sEnable);
                oDbm.AddParameters(12, "@sOutRequired", sOutRequired);
                oDbm.AddParameters(13, "@SITECODE", PCommon.sSiteCode);
                oDbm.AddParameters(14, "@ISLOTCREATE", sIsLotCreate);
                oDbm.AddParameters(15, "@ROUTE_NAME", sRouteName);
                oDbm.AddParameters(16, "@TMO_PARTCODE", sTMOPartCode);
                oDbm.AddParameters(17, "@XRAYSCANNINGAUTOPICUP", sIsXrayAutoSampledPIcked);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_ROUTING_MASTER").Tables[0];
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

        public DataTable DeleteRouting(int iID)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "DELETEROUTING");
                oDbm.AddParameters(1, "@ID", iID);
                oDbm.AddParameters(2, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_ROUTING_MASTER").Tables[0];
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
        public DataTable DeleteCompleteRouting(string sFGItemCode,string sRouteName)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "DELETECOMPLETEROUTING");
                oDbm.AddParameters(1, "@FGITEMCODE", sFGItemCode);
                oDbm.AddParameters(2, "@SITECODE", PCommon.sSiteCode);
                oDbm.AddParameters(3, "@ROUTE_NAME", sRouteName);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_ROUTING_MASTER").Tables[0];
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

        public DataTable dtuploadRouting(DataTable dt, string type,string sRouteName,string sUserID)
        {
            DataTable dt1 = new DataTable();
            try
            {
                oDbm.CreateParameters(5);
                oDbm.AddParameters(0, "@DETAILS", dt);
                oDbm.AddParameters(1, "@TYPE", type);
                oDbm.AddParameters(2, "@SITECODE", PCommon.sSiteCode);
                oDbm.AddParameters(3, "@ROUTE_NAME", sRouteName);
                oDbm.AddParameters(4, "@DOWNLOADEDBY", sUserID);
                oDbm.Open();
                dt1 = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_UPLOAD_ROUTING_MASTER").Tables[0];
                
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
            return dt1;
        }
    }
}
