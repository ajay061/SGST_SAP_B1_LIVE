using Common;
using System;
using System.Data;
namespace DataLayer.Masters
{
    public class DL_LocationMaster
    {
        DBManager oDbm;
        public DL_LocationMaster()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }       

        public DataTable BindPartCode(string sArea,string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDPARTCODE");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.AddParameters(2, "@LOCATIONAREA", sArea);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_LOCATION_MASTER").Tables[0];
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
        public DataTable GetLocationDetails(string sArea,string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "BINDLOCATIONDETAILS");
                oDbm.AddParameters(1, "@LOCATIONAREA", sArea);
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_LOCATION_MASTER").Tables[0];
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

        public DataTable FillLocationDetails(string sLocationCode, string sArea, string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "FILLLOCATIONDETAILS");
                oDbm.AddParameters(1, "@LOCATIONAREA", sArea);              
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                oDbm.AddParameters(3, "@LOCATIONCODE", sLocationCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_LOCATION_MASTER").Tables[0];
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

        public DataTable SaveLocationDetails(string _sLocationCode, string _sLocationArea, 
            string _slocationtype, string _sDescription, string _sCreBy, string _PartCode
            ,string sWHLoc, string sSiteCode
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(9);
                oDbm.AddParameters(0, "@TYPE", "SaveLocationData");
                oDbm.AddParameters(1, "@LOCATIONCODE", _sLocationCode);
                oDbm.AddParameters(2, "@LOCATIONAREA", _sLocationArea);
                oDbm.AddParameters(3, "@LOCATIONTYPE", _slocationtype);
                oDbm.AddParameters(4, "@DESCRIPTION", _sDescription);
                oDbm.AddParameters(5, "@CREATED_BY", _sCreBy);
                oDbm.AddParameters(6, "@PART_CODE", _PartCode);
                oDbm.AddParameters(7, "@SITECODE", sSiteCode);
                oDbm.AddParameters(8, "@WHLOC", sWHLoc);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_LOCATION_MASTER").Tables[0];
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

        public DataTable UpdateLocation(string _sLocationCode, string _sLocationArea,
       string _slocationtype, string _sDescription, string _sCreBy, string _PartCode
            ,string sID
            ,string sWHLoc, string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(9);
                oDbm.AddParameters(0, "@TYPE", "UpdateLocation");
                oDbm.AddParameters(1, "@LOCATIONCODE", _sLocationCode);
                oDbm.AddParameters(2, "@LOCATIONAREA", _sLocationArea);
                oDbm.AddParameters(3, "@LOCATIONTYPE", _slocationtype);
                oDbm.AddParameters(4, "@DESCRIPTION", _sDescription);
                oDbm.AddParameters(5, "@CREATED_BY", _sCreBy);
                oDbm.AddParameters(6, "@PART_CODE", _PartCode);              
                oDbm.AddParameters(7, "@SITECODE", sSiteCode);
                oDbm.AddParameters(8, "@WHLOC", sWHLoc);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_LOCATION_MASTER").Tables[0];
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
        public DataTable UploadLocationData(DataTable dtUploadLocation,string sUserID)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "UPLOADLOCATION");
                oDbm.AddParameters(1, "@DETAILS", dtUploadLocation);
                oDbm.AddParameters(2, "@CREATED_BY", sUserID);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_LOCATION_MASTER").Tables[0];
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
        public DataTable Deletelocation(string sLocationCode,string sLocationArea,string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "DELETELOCATION");
                oDbm.AddParameters(1, "@LOCATIONCODE", sLocationCode);
                oDbm.AddParameters(2, "@LOCATIONAREA", sLocationArea);
                oDbm.AddParameters(3, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_LOCATION_MASTER").Tables[0];
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
