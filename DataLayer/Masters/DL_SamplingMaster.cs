using System;
using PL;
using Common;
using System.Data;

namespace DataLayer.Masters
{
    public class DL_SamplingMaster
    {
        DBManager oDbm;
        public DL_SamplingMaster()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable BindFGItemCode(string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDFGITEMCODE");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_SAMPLING_MASTER").Tables[0];
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
        public DataTable BindMachineID(string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDMACHINEID");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_SAMPLING_MASTER").Tables[0];
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
        public DataTable GetData(string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDSAMPLINGDETAILS");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_SAMPLING_MASTER").Tables[0];
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

        public DataTable GetDataByID(PL_SamplingMaster plobj, string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "FILLSAMPLINGDETAILS");
                oDbm.AddParameters(1, "@SMID", plobj.iSMID);
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_SAMPLING_MASTER").Tables[0];
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

        public DataTable SaveData(PL_SamplingMaster plobj, string sSiteCode, string sUserID)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(10);
                oDbm.AddParameters(0, "@TYPE", "SAVESAMPLINGDETAILS");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.AddParameters(2, "@FGITEMCODE", plobj.sFGItemCode);
                oDbm.AddParameters(3, "@LOT_QTY", plobj.iLotqty);
                oDbm.AddParameters(4, "@SAMPLING_QTY", plobj.iSamplingQty);
                oDbm.AddParameters(5, "@MODULE", plobj.sModuleType);
                oDbm.AddParameters(6, "@LTHOURS", plobj.iLtHours);
                oDbm.AddParameters(7, "@USERID", sUserID);
                oDbm.AddParameters(8, "@XRAYSAMPLINGQTY", plobj.iXraySamplingQty);
                oDbm.AddParameters(9, "@PDISAMPLINGQTY", plobj.iPDISamplingQty);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_SAMPLING_MASTER").Tables[0];
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
        public DataTable UpdateData(PL_SamplingMaster plobj, string sSiteCode, string sUserID)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(11);
                oDbm.AddParameters(0, "@TYPE", "UPDATESAMPLINGDETAILS");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.AddParameters(2, "@FGITEMCODE", plobj.sFGItemCode);
                oDbm.AddParameters(3, "@LOT_QTY", plobj.iLotqty);
                oDbm.AddParameters(4, "@SAMPLING_QTY", plobj.iSamplingQty);
                oDbm.AddParameters(5, "@MODULE", plobj.sModuleType);
                oDbm.AddParameters(6, "@LTHOURS", plobj.iLtHours);
                oDbm.AddParameters(7, "@USERID", sUserID);
                oDbm.AddParameters(8, "@SMID", plobj.iSMID);
                oDbm.AddParameters(9, "@XRAYSAMPLINGQTY", plobj.iXraySamplingQty);
                oDbm.AddParameters(10, "@PDISAMPLINGQTY", plobj.iPDISamplingQty);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_SAMPLING_MASTER").Tables[0];
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
