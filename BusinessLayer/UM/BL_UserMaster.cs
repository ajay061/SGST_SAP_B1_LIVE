using Common;
using DataLayer;
using System;
using System.Data;
using System.Text;

namespace BusinessLayer
{
    public class BL_UserMaster
    {
        DL_UserMaster dlobj = null;      

        public DataTable GetUsersForSuperAdmin(string sUserID,string sDepartMent)
        {
            DataTable dtUsers = new DataTable();
            dlobj = new DL_UserMaster();
            try
            {
                dtUsers = dlobj.dlGetUsersForSuperAdmin(sUserID, sDepartMent);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtUsers;
        }

        public DataTable GetSeletedData(string _SN)
        {
            DataTable dt = new DataTable();
            dlobj = new DL_UserMaster();
            try
            {
                dt = dlobj.dlGetSeletedData(_SN);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dt;
        }
        public DataTable SaveUser(string _SiteCode, string _UserID, string _UserType, string _Department, string _UserName, string _Password, bool _active, string _CreatedBy, string contactno, string emailid)
        {
            DataTable dt = new DataTable();
            dlobj = new DL_UserMaster();
            try
            {
                dt = dlobj.dlSaveUserData(_SiteCode
                    , _UserID, _UserType, _Department, _UserName,
                    _Password, _active,
                    _CreatedBy, contactno, emailid
                    );
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dt;
        }

        public string DeleteUser(string _SN)
        {
            string sResult = string.Empty;           
            dlobj = new DL_UserMaster();
            try
            {
                DataTable dt = dlobj.dlDeleteUser(_SN);
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
        public string UpdateUser(string uSER_TYPE, string dEPARTMENT, string uSERNAME, string pASSWORD, 
            bool aCTIVE, string sN, string acONTACT, string aMAIL)
        {
            string sResult = string.Empty;
            dlobj = new DL_UserMaster();
            try
            {
                DataTable dt = dlobj.dlUpdateUser(uSER_TYPE, dEPARTMENT, uSERNAME, pASSWORD, 
                    aCTIVE, acONTACT
                    , aMAIL, Convert.ToInt32(sN)                    
                    );
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
