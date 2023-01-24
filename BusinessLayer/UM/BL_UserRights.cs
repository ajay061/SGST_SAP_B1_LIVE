using Common;
using DataLayer;
using System;
using System.Data;
using System.Text;

namespace BusinessLayer
{
    public class BL_UserRights
    {
        DL_UserRights dlobj = null;
        StringBuilder sb = null;
        public DataTable BindModule(string sDepartMent)
        {
            DataTable dtModuleName = new DataTable();
            dlobj = new DL_UserRights();
            try
            {                
                dtModuleName = dlobj.dlBindModule(sDepartMent);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtModuleName;
        }
        public DataTable BINDUSERS(string _Department)
        {
            DataTable dtUsers = new DataTable();
            dlobj = new DL_UserRights();
            try
            {
                            
                dtUsers = dlobj.dlGetUser(_Department);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtUsers;
        }

        public DataTable GetAllUserRights(string _UserID)
        {
            DataTable dtUsers = new DataTable();
            dlobj = new DL_UserRights();
            try
            {              
                dtUsers = dlobj.dlGetRightsDetails(_UserID);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtUsers;
        }

        public string SaveUserRights( string _UserID, DataTable dtModuleName , string _CreatedBy)
        {
            string sSaveUserRights = string.Empty;
            dlobj = new DL_UserRights();
            try
            {
                sb = new StringBuilder();                
                if (dtModuleName.Rows.Count > 0)
                {
                    sb.AppendLine(" INSERT INTO mUSERRIGHTS_H ");
                    sb.AppendLine("( [USERID], [MODULENAME],[RIGHTSGIVENBY],RIGHTS_REMOVED_BY,RIGHTSGIVENON )");
                    sb.AppendLine(" ");
                    sb.AppendLine(" SELECT [USERID], [MODULENAME],[RIGHTSGIVENBY],'"+ _CreatedBy + "',RIGHTSGIVENON FROM mUSERRIGHTS  ");
                    sb.AppendLine(" WHERE upper(USERID) = '" + _UserID.ToUpper() + "'; ");
                    sb.AppendLine(" DELETE FROM mUSERRIGHTS ");
                    sb.AppendLine(" WHERE upper(USERID) = '" + _UserID.ToUpper() + "'; ");
                    for (int i = 0; i < dtModuleName.Rows.Count; i++)
                    {
                        sb.AppendLine(" INSERT mUSERRIGHTS ([USERID], [MODULENAME],[RIGHTSGIVENBY])");
                        sb.AppendLine(" VALUES ( ");
                        sb.AppendLine(" '" + _UserID.ToUpper() + "','" + dtModuleName.Rows[i][0].ToString().ToUpper() + "','" + _CreatedBy + "' ");
                        sb.AppendLine(" ) ");
                    }                    
                }
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtMessage, System.Reflection.MethodBase.GetCurrentMethod().Name,", Query " + sb.ToString());
                int iSaveUserRights = dlobj.SaveUserRights(sb);
                if (iSaveUserRights > 0)
                {
                    sSaveUserRights = "SUCCESS~Rights assigned successfully";
                }
                else
                {
                    sSaveUserRights = "N~Rights assigned failed";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message + ", Query " + sb.ToString());
                throw ex;
            }
            return sSaveUserRights;
        }
    }
}
