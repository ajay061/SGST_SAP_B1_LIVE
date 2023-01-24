using Common;
using System;
using System.Data;
using System.Text;

namespace DataLayer
{
    public class DL_Quality
    {
        DBManager oDbm;
        public DL_Quality()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }

        public DataTable ScanPCBBarcode(string sPCBBarcode, bool IsMRN, string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "ScanBarcode");
                oDbm.AddParameters(1, "@PARTBARCODE", sPCBBarcode);
                oDbm.AddParameters(2, "@ISMRN", IsMRN);
                oDbm.AddParameters(3, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_RM_QUALITY").Tables[0];
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
        public DataSet BindDefect(string sItemCode, string sSiteCode)
        {
            DataSet dt = new DataSet();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "BINDDEFECT");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.AddParameters(2, "@PARTCODE", sItemCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_RM_QUALITY");
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
        public DataTable dtQuality(string _qStatus, decimal _dApprovedQty, string _PartBarcode, string sGRNNo,
            string sItemLineNo
            , string _PartCode
            , DataTable dtDefect, string sRemarks
               , string sSiteCode, string sUserID, string sLineCode,string sQualityResult
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(13);
                oDbm.AddParameters(0, "@TYPE", "QUALITY");
                oDbm.AddParameters(1, "@STATUS", _qStatus);
                oDbm.AddParameters(2, "@QTY", _dApprovedQty);
                oDbm.AddParameters(3, "@PARTBARCODE", _PartBarcode);
                oDbm.AddParameters(4, "@GRNNO", sGRNNo);
                oDbm.AddParameters(5, "@ITEMLINENO", sItemLineNo);
                oDbm.AddParameters(6, "@PARTCODE", _PartCode);
                oDbm.AddParameters(7, "@DETAILS", dtDefect);
                oDbm.AddParameters(8, "@LINECODE", sLineCode);
                oDbm.AddParameters(9, "@SITECODE", sSiteCode);
                oDbm.AddParameters(10, "@PRINTED_BY", sUserID);
                oDbm.AddParameters(11, "@REMARKS", sRemarks);
                oDbm.AddParameters(12, "@RESULT", sQualityResult);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_RM_QUALITY").Tables[0];
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

        public DataTable dtQualityFullBatch(string _qStatus, decimal _dApprovedQty, string _PartBarcode, string sGRNNo,
          string sItemLineNo
          , string _PartCode
           , string sBatchNo, DataTable sDefect, string sRemarks
              , string sSiteCode, string sUserID, string sLineCode, string sQualityResult
          )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(14);
                oDbm.AddParameters(0, "@TYPE", "QUALITYFULLBATCH");
                oDbm.AddParameters(1, "@STATUS", _qStatus);
                oDbm.AddParameters(2, "@QTY", _dApprovedQty);
                oDbm.AddParameters(3, "@PARTBARCODE", _PartBarcode);
                oDbm.AddParameters(4, "@GRNNO", sGRNNo);
                oDbm.AddParameters(5, "@ITEMLINENO", sItemLineNo);
                oDbm.AddParameters(6, "@PARTCODE", _PartCode);
                oDbm.AddParameters(7, "@BATCHNO", sBatchNo);
                oDbm.AddParameters(8, "@DETAILS", sDefect);
                oDbm.AddParameters(9, "@LINECODE", sLineCode);
                oDbm.AddParameters(10, "@SITECODE", sSiteCode);
                oDbm.AddParameters(11, "@PRINTED_BY", sUserID);
                oDbm.AddParameters(12, "@REMARKS", sRemarks);
                oDbm.AddParameters(13, "@RESULT", sQualityResult);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_RM_QUALITY").Tables[0];
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

        public DataTable dtMRNQuality(string _qStatus, decimal _dApprovedQty,
            string _PartBarcode, string sGRNNo, string sItemLineNo
            , string _PartCode, string sScannedBy,
            DataTable sDefect, string sRemarks,string sSiteCode,string sLineCode, string sQualityResult
         )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(13);
                oDbm.AddParameters(0, "@TYPE", "MRNQUALITY");
                oDbm.AddParameters(1, "@STATUS", _qStatus);
                oDbm.AddParameters(2, "@QTY", _dApprovedQty);
                oDbm.AddParameters(3, "@PARTBARCODE", _PartBarcode);
                oDbm.AddParameters(4, "@GRNNO", sGRNNo);
                oDbm.AddParameters(5, "@ITEMLINENO", sItemLineNo);
                oDbm.AddParameters(6, "@PARTCODE", _PartCode);
                oDbm.AddParameters(7, "@PRINTED_BY", sScannedBy);
                oDbm.AddParameters(8, "@DETAILS", sDefect);
                oDbm.AddParameters(9, "@LINECODE", sLineCode);
                oDbm.AddParameters(10, "@SITECODE", sSiteCode);
                oDbm.AddParameters(11, "@REMARKS", sRemarks);
                oDbm.AddParameters(12, "@RESULT", sQualityResult);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_RM_QUALITY").Tables[0];
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
