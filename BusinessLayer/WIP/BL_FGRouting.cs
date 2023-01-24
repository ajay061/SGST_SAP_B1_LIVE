using System;
using System.Data;
using DataLayer.WIP;
using Common;

namespace BusinessLayer.WIP
{
    public class BL_FGRouting
    {
        DL_FGRouting dlboj = null;
        public DataTable BindLineId()
        {
            DataTable dtLineID = new DataTable();
            dlboj = new DL_FGRouting();
            try
            {
                dtLineID = dlboj.BindLine();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLineID;
        }
        public DataTable BindFGITEMCOE()
        {
            DataTable dtFgItemCode = new DataTable();
            dlboj = new DL_FGRouting();
            try
            {
                dtFgItemCode = dlboj.BindFGItemCode();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtFgItemCode;
        }
        public DataTable GetRouteName(string sFGItemCode)
        {
            DataTable dtFgItemCode = new DataTable();
            dlboj = new DL_FGRouting();
            try
            {
                dtFgItemCode = dlboj.GetRouteName(sFGItemCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtFgItemCode;
        }
        public DataTable BindMachineId(string LINEID)
        {
            DataTable dtMachineId = new DataTable();
            dlboj = new DL_FGRouting();
            try
            {
                dtMachineId = dlboj.BindMachineID(LINEID);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtMachineId;
        }
        public DataTable BindPROFILEID(string MACHINEID)
        {
            DataTable dtFgItemCode = new DataTable();
            dlboj = new DL_FGRouting();
            try
            {
                dtFgItemCode = dlboj.BindProgram(MACHINEID);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtFgItemCode;
        }

        public string GenerateRouting(string LINEID, string MACHINEID, string FGITEMCODE,
            string UPDATEOUTTIME, string PROFILEID, string SEQUEANCE, string RESEQUENACE, string CREATEBY
            , string sISSFG, string sSFGItemCode, string sEnable, string sOutRequired, string sIsLotCreate
            , string sRouteName,string sTMOPartCode, string sIsXrayAutoSampledPIcked, string SFGQty)
        {
            string sResult = string.Empty;
            dlboj = new DL_FGRouting();
            DataTable dt = new DataTable();
            try
            {
                dt = dlboj.SaveRouting(LINEID, MACHINEID, FGITEMCODE,
            UPDATEOUTTIME, PROFILEID, SEQUEANCE, RESEQUENACE, CREATEBY
            , sISSFG, sSFGItemCode, sEnable, sOutRequired, sIsLotCreate
            ,sRouteName, sTMOPartCode, sIsXrayAutoSampledPIcked, SFGQty
            );
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
                    return sResult;
                }
                else
                {
                    sResult = "N~Data updation failed";
                    return sResult;
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        public DataTable GetRoutingDetails(string sFGItemCode, string sRouteName)
        {
            DataTable dtFgRouting = new DataTable();
            dlboj = new DL_FGRouting();
            try
            {
                dtFgRouting = dlboj.GetRoutingDetails(sFGItemCode, sRouteName);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtFgRouting;
        }

        public DataTable EditRoutingDetails(string sFGItemCode, string sMachineID, int sSequence, string sRouteName)
        {
            DataTable dtFgRouting = new DataTable();
            dlboj = new DL_FGRouting();
            try
            {
                dtFgRouting = dlboj.EditRoutingDetails(sFGItemCode, sMachineID, sSequence, sRouteName);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtFgRouting;
        }
        
        public string UPDATEROUTING(string LINEID, string MACHINEID, string FG_ITEM_CODE,
            string PROFILE_ID, string SEQ, string UPDATE_OUT_TIME, string REWORK_SEQ, string Createdby
            , string ENABLE, string sOutTimeRequired, string sISSFG, string sSFGItemCode,string sIsLotCreate
            , string sRouteName,string sTMOPartCode, string sIsXrayAutoSampledPIcked
            )
        {
            string sResult = string.Empty;
            dlboj = new DataLayer.WIP.DL_FGRouting();
            try
            {
                DataTable dt = new DataTable();
                dt = dlboj.UpdateRouting(LINEID, MACHINEID, FG_ITEM_CODE,
            UPDATE_OUT_TIME, PROFILE_ID, SEQ, REWORK_SEQ, Createdby
            , sISSFG, sSFGItemCode, ENABLE, sOutTimeRequired, sIsLotCreate
            , sRouteName, sTMOPartCode, sIsXrayAutoSampledPIcked
            );
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
                }
                else
                {
                    sResult = "N~Data updation failed";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }
        public string DELETEROUTING(int Id)
        {
            string sResult = string.Empty;           
            dlboj = new DataLayer.WIP.DL_FGRouting();
            try
            {
                DataTable dt = new DataTable();
                dt = dlboj.DeleteRouting(Id);
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
                    return sResult;
                }
                else
                {
                    sResult = "N~Routing deletetion failed";
                    return sResult;
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }

        public string DELETECOMPLETEROUTING(string FG_ITEM_CODE, string sRouteName)
        {
            string sResult = string.Empty;
            dlboj = new DataLayer.WIP.DL_FGRouting();
            try
            {
                DataTable dt = new DataTable();
                dt = dlboj.DeleteCompleteRouting(FG_ITEM_CODE, sRouteName);
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
                    return sResult;
                }
                else
                {
                    sResult = "N~Routing deletetion failed";
                    return sResult;
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }


        public DataTable dtuploadRouting(DataTable dt, string type,string sRouteName,string sUserID)
        {
            try
            {
                dlboj = new DL_FGRouting();
                return dlboj.dtuploadRouting(dt, type, sRouteName, sUserID);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }

    }
}