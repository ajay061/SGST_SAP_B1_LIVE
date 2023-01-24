using Common;
using System;
using System.Data;

namespace DataLayer
{
    public class DL_RM_Printing
    {
        DBManager oDbm;
        public DL_RM_Printing()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable BindGRPODate(string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDGRPODATE");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_RM_PRINTING").Tables[0];
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
        public DataTable BindGRNno(string sGRPODate,string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "BINDGRNNO");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.AddParameters(2, "@GRPODATE", sGRPODate);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_RM_PRINTING").Tables[0];
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
        public DataTable BindPartCode(string sGRNNo, string sGRPODate,string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "BINDPARTCODE");
                oDbm.AddParameters(1, "@GRNNO", sGRNNo);
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                oDbm.AddParameters(3, "@GRPODATE", sGRPODate);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_RM_PRINTING").Tables[0];
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
        public DataTable BindItemLineNo(string sGRNO, string sPartCode, string sGRPODate
            ,string sSiteCode
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(5);
                oDbm.AddParameters(0, "@TYPE", "BINDITEMLINENO");
                oDbm.AddParameters(1, "@GRNNO", sGRNO);
                oDbm.AddParameters(2, "@PARTCODE", sPartCode);
                oDbm.AddParameters(3, "@SITECODE", sSiteCode);
                oDbm.AddParameters(4, "@GRPODATE", sGRPODate);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_RM_PRINTING").Tables[0];
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
        public DataTable FillDetails(string sGRNO, string sPartCode, string sItemLineno, string sGRPODate
            ,string sSiteCode
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(6);
                oDbm.AddParameters(0, "@TYPE", "FILLDETAILS");
                oDbm.AddParameters(1, "@GRNNO", sGRNO);
                oDbm.AddParameters(2, "@PARTCODE", sPartCode);
                oDbm.AddParameters(3, "@ITEMLINENO", sItemLineno);
                oDbm.AddParameters(4, "@SITECODE", sSiteCode);
                oDbm.AddParameters(5, "@GRPODATE", sGRPODate);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_RM_PRINTING").Tables[0];
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

        public DataTable FillDatatable(int RMID)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "FILLGRID");
                oDbm.AddParameters(1, "@RMID", RMID);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_RM_PRINTING").Tables[0];
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

        public DataTable GRNPrinting(string sGRNO, string sPartCode, string sItemLinenO,
            string sMFRPartCode, string SUP_INV_NO, string Invoice_Date
            , string sBatchNo,
    string MFG_DATE, string EXP_DATE, string sSuppliername, string sSiteCode,
    string sSupplierCode, string sUOM, string sInbondDeliveryNo,
    decimal dPrintedQty, string sPrintedBy, int iRM_ID,
    decimal dReceivedQty, decimal dRRemainingQty, string sLHRH, string sMSLValue
            , string sGRPODate,string sLineCode
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(24);
                oDbm.AddParameters(0, "@TYPE", "GRNPRINTING");
                oDbm.AddParameters(1, "@GRNNO", sGRNO);
                oDbm.AddParameters(2, "@PARTCODE", sPartCode);
                oDbm.AddParameters(3, "@ITEMLINENO", sItemLinenO);
                oDbm.AddParameters(4, "@MFRPARTCODE", sMFRPartCode);
                oDbm.AddParameters(5, "@SUPINVNO", SUP_INV_NO);
                oDbm.AddParameters(6, "@INVOICEDATE", Invoice_Date);
                oDbm.AddParameters(7, "@BATCHNO", sBatchNo);
                oDbm.AddParameters(8, "@MFGDATE", MFG_DATE);
                oDbm.AddParameters(9, "@EXPDATE", EXP_DATE);
                oDbm.AddParameters(10, "@SUPNAME", sSuppliername);
                oDbm.AddParameters(11, "@SITECODE", sSiteCode);
                oDbm.AddParameters(12, "@SUPCODE", sSupplierCode);
                oDbm.AddParameters(13, "@UOM", sUOM);
                oDbm.AddParameters(14, "@INBDNO", sInbondDeliveryNo);
                oDbm.AddParameters(15, "@PRINTEDBY", sPrintedBy);
                oDbm.AddParameters(16, "@PRINTEDQTY", dPrintedQty);
                oDbm.AddParameters(17, "@RMID", iRM_ID);
                oDbm.AddParameters(18, "@RECQTY", dReceivedQty);
                oDbm.AddParameters(19, "@REMQTY", dRRemainingQty);
                oDbm.AddParameters(20, "@LH_RH", sLHRH);
                oDbm.AddParameters(21, "@MSL_INFO", sMSLValue);
                oDbm.AddParameters(22, "@LINECODE", sLineCode);
                oDbm.AddParameters(23, "@GRPODATE", sGRPODate);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_RM_PRINTING").Tables[0];
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
