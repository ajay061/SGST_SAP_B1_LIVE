using Common;
using System;
using System.Data;

namespace DataLayer.WIP
{
    public class DL_WIP_LABEL_PRINTING
    {
        DBManager oDbm;
        public DL_WIP_LABEL_PRINTING()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }

        public DataTable BINDINEL_PARTNO(string sType)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "BINDPRINTINGTYPE");
                oDbm.AddParameters(1, "@PTYPE", sType);
                oDbm.AddParameters(2, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_LABEL_PRINTING").Tables[0];
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

        public DataTable BindReelBarcode(string sType, string sPartCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "BINDREELBARCODE");
                oDbm.AddParameters(1, "@PTYPE", sType);
                oDbm.AddParameters(2, "@PARTCODE", sPartCode);
                oDbm.AddParameters(3, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_LABEL_PRINTING").Tables[0];
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
        public DataTable GetPrn(string sSiteCode, string sCustomer, string sFGItemCode, string sPRNType)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(5);
                oDbm.AddParameters(0, "@TYPE", "GETPRN");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.AddParameters(2, "@CUSTOMER", sCustomer);
                oDbm.AddParameters(3, "@PRNTYPE", sPRNType);
                oDbm.AddParameters(4, "@FGITEMCODE", sFGItemCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_LABEL_PRINTING").Tables[0];
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


        #region PCB Label Reprinting

        public DataTable GetPageLabelCount(string sFGItemCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "GETPAGELLABELCOUNT");
                oDbm.AddParameters(1, "@SITECODE", PCommon.sSiteCode);
                oDbm.AddParameters(2, "@FGITEMCODE", sFGItemCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_LABEL_PRINTING").Tables[0];
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
        public DataTable BindFGItemCode()
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDFGITEMCODE");           
                oDbm.AddParameters(1, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_LABEL_PRINTING").Tables[0];
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

        public DataTable BindWorkOrderNo(string sFGitemCode )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "BINDWORKORDERNO");
                oDbm.AddParameters(1, "@SITECODE", PCommon.sSiteCode);
                oDbm.AddParameters(2, "@FGITEMCODE", sFGitemCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_LABEL_PRINTING").Tables[0];
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
        public DataTable BindPrintedReels(string sFGitemCode,string sWorkOrderno)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "BINDPRINTEDREEL");
                oDbm.AddParameters(1, "@SITECODE", PCommon.sSiteCode);
                oDbm.AddParameters(2, "@FGITEMCODE", sFGitemCode);
                oDbm.AddParameters(3, "@WORKORDERNO", sWorkOrderno);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_LABEL_PRINTING").Tables[0];
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
        public DataTable GetPrintedData(string sBarcode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "GETREELDATA");
                oDbm.AddParameters(1, "@PARTBARCODE", sBarcode);               
                oDbm.AddParameters(2, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_LABEL_PRINTING").Tables[0];
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
        #endregion  

    }
}
