using Common;
using DataLayer.WIP;
using System;
using System.Data;
namespace BusinessLayer.WIP
{
    public class BL_ProfileMaster
    {
        DL_ProfileMaster dlboj = null;
        public DataTable GetPartCode()
        {
            DataTable dtpartcode = new DataTable();
            try
            {
                dlboj = new DL_ProfileMaster();
                dtpartcode = dlboj.GetPartCode();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtpartcode;
        }
        public DataTable GetMachineRecord()
        {
            DataTable dtMachine = new DataTable();
            try
            {
                dlboj = new DL_ProfileMaster();
                dtMachine = dlboj.BindMachine();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtMachine;
        }
        public DataTable GetProgramID()
        {
            DataTable dtProfileID = new DataTable();
            try
            {
                dlboj = new DL_ProfileMaster();
                dtProfileID = dlboj.BindProgram();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtProfileID;
        }
        public DataTable GetProgramDetails(string _sProgramID, string sMachineID)
        {
            DataTable dtProfiledata = new DataTable();
            dlboj = new DL_ProfileMaster();
            try
            {
                dtProfiledata = dlboj.GetProgramDetails(_sProgramID, sMachineID);
                return dtProfiledata;
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        public DataTable ValidateTool(string sToolID)
        {
            DataTable dtpartcode = new DataTable();
            try
            {
                dlboj = new DL_ProfileMaster();
                dtpartcode = dlboj.ValidateTool(sToolID);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtpartcode;
        }
        public string SaveProfileValue(DataTable _profilemst, bool ProgramEnable,string sUserID)
        {
            try
            {
                dlboj = new DL_ProfileMaster();
                DataTable dtpartcode = dlboj.SaveProgram(_profilemst, ProgramEnable, sUserID);
                if (dtpartcode.Rows.Count > 0)
                {
                    return dtpartcode.Rows[0][0].ToString();
                }
                else
                {
                    return "N~ Profile creation failed.";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }

        public string DeleteProfilePartCode(string _partCode, string sProgramID, string sMachineID)
        {
            string sResult = string.Empty;
            dlboj = new DL_ProfileMaster();
            try
            {
                DataTable dt = dlboj.DeleteProgram(sProgramID, _partCode, sMachineID);
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
                }
                else
                {
                    sResult = "N~Profile part code deletion failed";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }

        public string DeleteCompleteProgramDetails(string _profileID)
        {
            string sResult = string.Empty;
            dlboj = new DL_ProfileMaster();
            try
            {
                DataTable dt = dlboj.DeleteCompleteProgram(_profileID);
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
                }
                else
                {
                    sResult = "N~ Profile ID not deleted";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }

        public DataTable dtuploadprofile(DataTable dt, string type)
        {
            try
            {
                dlboj = new DL_ProfileMaster();
                return dlboj.ValidateData(dt, type);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
    }
}

