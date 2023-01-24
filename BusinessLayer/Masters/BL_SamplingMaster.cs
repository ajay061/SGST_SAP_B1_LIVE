using System;
using System.Data;
using DataLayer.Masters;
using Common;
using PL;

namespace BusinessLayer.Masters
{
    public class BL_SamplingMaster
    {
        DL_SamplingMaster dlobj = new DL_SamplingMaster();
        public DataTable BindFGitemCode(string sSiteCode)
        {
            DataTable dtBins = new DataTable();
            dlobj = new DL_SamplingMaster();
            try
            {
                dtBins = dlobj.BindFGItemCode(sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtBins;
        }

        public DataTable BindMachineID(string sSiteCode)
        {
            DataTable dtBins = new DataTable();
            dlobj = new DL_SamplingMaster();
            try
            {
                dtBins = dlobj.BindMachineID(sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtBins;
        }
        public DataTable GetData(string sSiteCode)
        {
            DataTable dtBins = new DataTable();
            dlobj = new DL_SamplingMaster();
            try
            {
                dtBins = dlobj.GetData(sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtBins;
        }

        public DataTable GetSeletedData(PL_SamplingMaster plobj,string sSiteCode)
        {
            DataTable dtBins = new DataTable();
            dlobj = new DL_SamplingMaster();
            try
            {
                dtBins = dlobj.GetDataByID(plobj, sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtBins;
        }
        public string SaveData(PL_SamplingMaster plobj,string sSiteCode,string sUserID)
        {
            DataTable dt = new DataTable();
            dlobj = new DL_SamplingMaster();
            string sResult = string.Empty;
            try
            {
                dt = dlobj.SaveData(plobj, sSiteCode, sUserID);
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
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
        public string UpdateData(PL_SamplingMaster plobj, string sSiteCode, string sUserID)
        {
            DataTable dt = new DataTable();
            dlobj = new DL_SamplingMaster();
            string sResult = string.Empty;
            try
            {
                dt = dlobj.UpdateData(plobj, sSiteCode, sUserID);
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
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
