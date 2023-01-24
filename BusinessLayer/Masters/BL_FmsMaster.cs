using System;
using System.Text;
using System.Data;
using DataLayer;
using Common;

namespace BusinessLayer.WIP
{
    public class BL_FmsMaster
    {
        StringBuilder sb = null;
        DL_FmsMaster dlobj = null;
        public DataTable BIND_LINEID()
        {
            DataTable dtBind_LINEID = new DataTable();
            dlobj = new DL_FmsMaster();
            try
            {
                dtBind_LINEID = dlobj.BindLine();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtBind_LINEID;
        }
        public DataTable BIND_MACHINEID(string LINE_ID)
        {
            DataTable dtBind_MACHINEID = new DataTable();
            dlobj = new DL_FmsMaster();
            try
            {
                dtBind_MACHINEID = dlobj.BindMachine(LINE_ID);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtBind_MACHINEID;
        }

        public DataTable GetFMSMASTERDETAILS()
        {
            DataTable dtMachineConfg = new DataTable();
            dlobj = new DL_FmsMaster();
            try
            {
                dtMachineConfg = dlobj.FillMachineDetails();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtMachineConfg;
        }

        public string SaveFMSMasterDetails(string Machineid, string FMS_TOP_IP, string FMS_TOP_IP_ENABLE,
            string LINE_ID, String FMS_LOCATION, string FMS_TOP_PORT)
        {
            string sResult = string.Empty;
            sb = new StringBuilder();
            dlobj = new DL_FmsMaster();
            try
            {
                DataTable dt = new DataTable();
                dt = dlobj.SaveFMSDetails(Machineid, FMS_TOP_IP, FMS_TOP_IP_ENABLE, LINE_ID,
                    FMS_LOCATION, FMS_TOP_PORT);
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
                }
                else
                {
                    sResult = "N~No result found";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }

        public string UpdateFMSDetails(string MACHINEID, string FMS_TOP_IP, string FMS_TOP_PORT, string FMS_TOP_IP_ENABLE,
            string FMS_LOC, string LINE_ID)
        {
            string sResult = string.Empty;
            sb = new StringBuilder();
            dlobj = new DL_FmsMaster();
            try
            {
                DataTable dt = new DataTable();
                dt = dlobj.UpdateFMS(MACHINEID, FMS_TOP_IP, FMS_TOP_IP_ENABLE, LINE_ID,
                    FMS_LOC, FMS_TOP_PORT);
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
                }
                else
                {
                    sResult = "N~No result found";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }

        public string DeleteFMS(string Id, string sMachineID)
        {
            string sResult = string.Empty;
            dlobj = new DL_FmsMaster();
            try
            {
                DataTable dt = new DataTable();
                dt = dlobj.DeleteFMS(sMachineID, Id);
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
                }
                else
                {
                    sResult = "N~No result found";
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
