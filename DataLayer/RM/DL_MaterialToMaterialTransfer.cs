using Common;
using System;
using System.Data;

namespace DataLayer
{
    public class DL_MaterialToMaterialTransfer
    {
        DBManager oDbm;
        public DL_MaterialToMaterialTransfer()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable BindMatererialRefNo(string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDMATREFNO");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_RM_M_TO_M_TRANSFER_PRINTING").Tables[0];
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
        public DataTable BindPartCode(string sMatRefNo,string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "GETPARTCODE");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.AddParameters(2, "@MATREFNO", sMatRefNo);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_RM_M_TO_M_TRANSFER_PRINTING").Tables[0];
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

        public DataTable GETMATTRANSFERDETAILS(string sMatRefNo,string sOutputPartCode
            ,string sSiteCode
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "GETMATTRANSFERDETAILS");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.AddParameters(2, "@MATREFNO", sMatRefNo);
                oDbm.AddParameters(3, "@NEWPARTCODE", sOutputPartCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_RM_M_TO_M_TRANSFER_PRINTING").Tables[0];
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
        public DataTable ValidateBarcode(string _ReelID,string sSiteCode)
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "VALIDATEREELBARCODE");
                oDbm.AddParameters(1, "@PARTBARCODE", _ReelID);
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_RM_M_TO_M_TRANSFER_PRINTING").Tables[0];
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
            return dtResult;
        }
        public DataTable M_TOMTransferPrint(string _PartCode, string _ReelID, string sPrintedBy,
    decimal dUpdatedQty, string sPrinterIP,
    string SUP_INV_NO, DateTime MFGDate, DateTime EXPDate, DateTime InvoiceDate,
    string BATCH_NO, string Quntity,string sMatRefNo,string sSiteCode,string sLineCode)
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(13);
                oDbm.AddParameters(0, "@TYPE", "MATERIALPRINTING");
                oDbm.AddParameters(1, "@PARTBARCODE", _ReelID);
                oDbm.AddParameters(2, "@UPDATEDQTY", dUpdatedQty);
                oDbm.AddParameters(3, "@NEWPARTCODE", _PartCode);
                oDbm.AddParameters(4, "@INVOICENO", SUP_INV_NO);
                oDbm.AddParameters(5, "@INVOICEDATE", InvoiceDate);
                oDbm.AddParameters(6, "@MFG_DATE", MFGDate);
                oDbm.AddParameters(7, "@EXP_DATE", EXPDate);
                oDbm.AddParameters(8, "@BATCH_NO", BATCH_NO);
                oDbm.AddParameters(9, "@PRINTEDBY", sPrintedBy);
                oDbm.AddParameters(10, "@SITECODE", sSiteCode);
                oDbm.AddParameters(11, "@LINECODE", sLineCode);
                oDbm.AddParameters(12, "@MATREFNO", sMatRefNo);
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_RM_M_TO_M_TRANSFER_PRINTING").Tables[0];

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
            return dtResult;
        }
    }
}
