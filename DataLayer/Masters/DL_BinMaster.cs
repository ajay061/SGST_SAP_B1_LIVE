using Common;
using System;
using System.Data;

namespace DataLayer
{
    public class DL_BinMaster
    {
        DBManager oDbm;
        public DL_BinMaster()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        
        public DataTable GetBins(string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDBINDETAILS");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_BINMASTER").Tables[0];
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

        public DataTable GetBinsByID(string sBINID)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "FILLBINDETAILS");
                oDbm.AddParameters(1, "@BINID", sBINID);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_BINMASTER").Tables[0];
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

        public DataTable SaveDeleteBin(string sBINID
            ,string sBINDesc,string sStorageArea,int iCapacity, string sStatus
            ,string sType,string sPartCode,string sUserID,string sSiteCode
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(9);
                oDbm.AddParameters(0, "@TYPE", sType);
                oDbm.AddParameters(1, "@BINID", sBINID);
                oDbm.AddParameters(2, "@BINDESC", sBINDesc);
                oDbm.AddParameters(3, "@STORAGEAREA", sStorageArea);
                oDbm.AddParameters(4, "@CAPACITY", iCapacity);
                oDbm.AddParameters(5, "@STATUS", sStatus);
                oDbm.AddParameters(6, "@SITECODE", sSiteCode);
                oDbm.AddParameters(7, "@USERID", sUserID);
                oDbm.AddParameters(8, "@PARTCODE", sPartCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_BINMASTER").Tables[0];
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

        public DataTable UploadBinData(DataTable dtUploadBin,string sUserID)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "UPLOADBIN");
                oDbm.AddParameters(1, "@DETAILS", dtUploadBin);
                oDbm.AddParameters(2, "@USERID", sUserID);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_BINMASTER").Tables[0];
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
