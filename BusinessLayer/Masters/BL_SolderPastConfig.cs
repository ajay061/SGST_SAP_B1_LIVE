using System;
using System.Text;
using System.Data;
using DataLayer;
using Common;

namespace BusinessLayer.WIP
{
    public class BL_SolderPastConfig
    {
        DL_SolderPastConfig dlboj = null;
        public DataTable BIND_MACHINEID()
        {
            DataTable dtBind_MACHINEID = new DataTable();
            try
            {
                dlboj = new DL_SolderPastConfig();
                dtBind_MACHINEID = dlboj.BIND_MACHINEID();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtBind_MACHINEID;
        }
        public DataTable GetMachineConfig()
        {
            DataTable dtBind_MACHINEID = new DataTable();
            try
            {
                dlboj = new DL_SolderPastConfig();
                dtBind_MACHINEID = dlboj.BindMachineDetails();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtBind_MACHINEID;
        }
        public DataTable GetSeletedData(string _SN)
        {
            DataTable dtBind_MACHINEID = new DataTable();
            try
            {
                dlboj = new DL_SolderPastConfig();
                dtBind_MACHINEID = dlboj.EditMachineData(_SN);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtBind_MACHINEID;
        }

        public string GenerateMachineDetails(string MACHINEID, string MACHINENAME
            , string processtime, string sprocesstimeEnable
             , string Nextprocesstime, string NextprocesstimeEnable
           )
        {
            string sResult = string.Empty;
            try
            {
                dlboj = new DL_SolderPastConfig();
                DataTable dtBind_MACHINEID = dlboj.SaveMachineData(MACHINEID
                    , MACHINENAME
            , processtime, sprocesstimeEnable
             , Nextprocesstime, NextprocesstimeEnable
                    );
                if (dtBind_MACHINEID.Rows.Count > 0)
                {
                    sResult = dtBind_MACHINEID.Rows[0][0].ToString();
                }
                else
                {
                    sResult = "N~No Result found";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }

        public string UpdateMachineDetails(string MACHINEID, string MACHINENAME
            , string processtime, string sprocesstimeEnable
             , string Nextprocesstime, string NextprocesstimeEnable)
        {
            string sResult = string.Empty;
            try
            {
                dlboj = new DL_SolderPastConfig();
                DataTable dtBind_MACHINEID = dlboj.UpdateMachine(MACHINEID
                    , MACHINENAME
            , processtime, sprocesstimeEnable
             , Nextprocesstime, NextprocesstimeEnable
                    );
                if (dtBind_MACHINEID.Rows.Count > 0)
                {
                    sResult = dtBind_MACHINEID.Rows[0][0].ToString();
                }
                else
                {
                    sResult = "N~No Result found";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }

        public string DeleteMachine(string MACHINEID)
        {
            string sResult = string.Empty;
            try
            {
                dlboj = new DL_SolderPastConfig();
                DataTable dtBind_MACHINEID = dlboj.DeleteMachine(MACHINEID);
                if (dtBind_MACHINEID.Rows.Count > 0)
                {
                    sResult = dtBind_MACHINEID.Rows[0][0].ToString();
                }
                else
                {
                    sResult = "N~No Result found";
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
