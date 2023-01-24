using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
  public  class DL_ToolMaster
    {
        DBManager oDbm;
        public DL_ToolMaster()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable BindTool(string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDTOOL");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_TOOLMASTER").Tables[0];
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

        public DataTable FillToolDetails(string sTOOLID, string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "FILLTOOLDETAILS");
                oDbm.AddParameters(1, "@TOOLID", sTOOLID);
                oDbm.AddParameters(2, "@SITECODE",sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_TOOLMASTER").Tables[0];
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

        public DataTable SaveTool(string toolid, string decription, string type, string qty, string createdBy
             ,string sSiteCode, string sMake, string sModel, string sEquipSrno,
            string sUsageRange, string sAccuracy, DateTime dCalibrationDate, DateTime dAlertDate
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(14);
                oDbm.AddParameters(0, "@TYPE", "SAVETOOL");
                oDbm.AddParameters(1, "@TOOLID", toolid);
                oDbm.AddParameters(2, "@DESCRIPTION", decription);
                oDbm.AddParameters(3, "@TOOLTYPE", type);
                oDbm.AddParameters(4, "@QTY", qty);
                oDbm.AddParameters(5, "@CREATED_BY", createdBy);
                oDbm.AddParameters(6, "@SITECODE",sSiteCode);
                oDbm.AddParameters(7, "@MAKE", sMake);
                oDbm.AddParameters(8, "@MODEL", sModel);
                oDbm.AddParameters(9, "@EQIPSRNO", sEquipSrno);
                oDbm.AddParameters(10, "@USAGERANGE", sUsageRange);
                oDbm.AddParameters(11, "@ACCURACY", sAccuracy);
                oDbm.AddParameters(12, "@CALDATE", dCalibrationDate);
                oDbm.AddParameters(13, "@ALERTDATE", dAlertDate);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_TOOLMASTER").Tables[0];
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

        public DataTable UpdateTool(string toolid, string decription, string type, string qty, string createdBy
            , string sSiteCode, string sMake, string sModel, string sEquipSrno,
            string sUsageRange, string sAccuracy, DateTime dCalibrationDate, DateTime dAlertDate)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(14);
                oDbm.AddParameters(0, "@TYPE", "UPDATETOOL");
                oDbm.AddParameters(1, "@TOOLID", toolid);
                oDbm.AddParameters(2, "@DESCRIPTION", decription);
                oDbm.AddParameters(3, "@TOOLTYPE", type);
                oDbm.AddParameters(4, "@QTY", qty);
                oDbm.AddParameters(5, "@CREATED_BY", createdBy);
                oDbm.AddParameters(6, "@SITECODE",sSiteCode);
                oDbm.AddParameters(7, "@MAKE", sMake);
                oDbm.AddParameters(8, "@MODEL", sModel);
                oDbm.AddParameters(9, "@EQIPSRNO", sEquipSrno);
                oDbm.AddParameters(10, "@USAGERANGE", sUsageRange);
                oDbm.AddParameters(11, "@ACCURACY", sAccuracy);
                oDbm.AddParameters(12, "@CALDATE", dCalibrationDate);
                oDbm.AddParameters(13, "@ALERTDATE", dAlertDate);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_TOOLMASTER").Tables[0];
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

        public DataTable DeleteTool(string toolid, string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "DELETETOOL");
                oDbm.AddParameters(1, "@TOOLID", toolid);
                oDbm.AddParameters(2, "@SITECODE",sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_TOOLMASTER").Tables[0];
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

        public int SaveTool(StringBuilder sb)
        {
            int iResult = 0;
            try
            {
                oDbm.Open();
                oDbm.BeginTransaction();
                iResult = oDbm.ExecuteNonQuery(System.Data.CommandType.Text, sb.ToString());
                oDbm.CommitTransaction();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                oDbm.RollbackTransaction();
                throw ex;
            }
            finally
            {
                oDbm.Close();
            }
            return iResult;
        }

        public DataTable UploadData(DataTable dtUpload,string sUserID)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "UPLOADDETAILS");
                oDbm.AddParameters(1, "@DETAILS", dtUpload);
                oDbm.AddParameters(2, "@CREATED_BY", sUserID);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_TOOLMASTER").Tables[0];
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
