using Common;
using System;
using System.Data;
namespace DataLayer.Masters
{
    public class DL_FG_Customer_Mapping
    {
        DBManager oDbm;
        public DL_FG_Customer_Mapping()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable BindFGItemCode()
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDFGITEMCODE");
                oDbm.AddParameters(1, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_FG_CUSTOMER_MAPPING").Tables[0];
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
        public DataTable BindCustomeCode()
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDCUSTOMERCODE");
                oDbm.AddParameters(1, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_FG_CUSTOMER_MAPPING").Tables[0];
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
        public DataTable GetDetails()
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "GETDETAILS");
                oDbm.AddParameters(1, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_FG_CUSTOMER_MAPPING").Tables[0];
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

        public DataTable SaveDeleteData(int iID, string sFGItemCode
            , string sCustomerCode
            , string sType,int iPackingHours,string sApprovedStatus,int iNoOFSFGMapping
            ,int iPackingSamplingSize,string sUserID, int iAgingTimePeriod ,string ISFULLAGING
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(11);
                oDbm.AddParameters(0, "@TYPE", sType);
                oDbm.AddParameters(1, "@FGID", iID);
                oDbm.AddParameters(2, "@FGITEMCODE", sFGItemCode);
                oDbm.AddParameters(3, "@CUSTOMERCODE", sCustomerCode);
                oDbm.AddParameters(4, "@SITECODE", PCommon.sSiteCode);
                oDbm.AddParameters(5, "@PACKINGTIMEHOURS", iPackingHours);
                oDbm.AddParameters(6, "@APPROVEDSTATUS", sApprovedStatus);
                oDbm.AddParameters(7, "@SFG_MAPPING", iNoOFSFGMapping);
                oDbm.AddParameters(8, "@PACKING_SAMPLING_SIZE", iPackingSamplingSize);
                oDbm.AddParameters(9, "@CREATEDBY", sUserID);
                oDbm.AddParameters(10, "@AGING_TIME_PERIOD", iAgingTimePeriod);
                oDbm.AddParameters(10, "@ISFULLAGING", ISFULLAGING);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_FG_CUSTOMER_MAPPING").Tables[0];
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

        public DataTable UploadData(DataTable dtUpload)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "UPLOADDETAILS");
                oDbm.AddParameters(1, "@DETAILS", dtUpload);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_FG_CUSTOMER_MAPPING").Tables[0];
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
