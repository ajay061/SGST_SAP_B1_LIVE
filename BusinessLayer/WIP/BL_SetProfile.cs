using System;
using System.Data;
using DataLayer.WIP;
using Common;

namespace BusinessLayer.WIP
{
    public class BL_SetProfile
    {
        DL_SetProfile dlboj = null;
        public DataTable BindFGITEMCOE()
        {
            DataTable dtFgItemCode = new DataTable();        
            dlboj = new DL_SetProfile();
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
        public DataTable BindLineId()
        {
            DataTable dtLineID = new DataTable();
            dlboj = new DL_SetProfile();
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
        public DataTable BindMachineType(string sLineID)
        {
            DataTable dtMachineId = new DataTable();
            dlboj = new DL_SetProfile();
            try
            {
                dtMachineId = dlboj.BindMachineType(sLineID);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtMachineId;
        }
      
        public DataTable GetSetProgramDetails()
        {
            DataTable dtFgRouting = new DataTable();
            dlboj = new DL_SetProfile();
            try
            {
                dtFgRouting = dlboj.GetDetails();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtFgRouting;
        }
        public string SaveProfileDetails(string LINEID, string FGITEMCODE,
         string CREATED_BY, string sMachineType)
        {
            string sResult = string.Empty;
            dlboj = new DataLayer.WIP.DL_SetProfile();
            try
            {
                DataTable dt = dlboj.SaveDetails(LINEID, FGITEMCODE, sMachineType, CREATED_BY);
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
                }
                else
                {
                    sResult = "N~Data deletion failed";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }
        public string DeleteProgram(string MPID, string LINEID, string sMachineType, string FG_ITEM_CODE)
        {            
            string sResult = string.Empty;
            dlboj = new DataLayer.WIP.DL_SetProfile();
            try
            {
                DataTable dt = dlboj.DeleteDetails(LINEID, FG_ITEM_CODE, sMachineType); 
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
                }
                else
                {
                    sResult = "N~Data deletion failed";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }
    }
}
