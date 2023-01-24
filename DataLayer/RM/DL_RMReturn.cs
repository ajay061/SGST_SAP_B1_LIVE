using Common;
using System;
using System.Data;

namespace DataLayer
{
    public class DL_RMReturn
    {
        DBManager oDbm;
        public DL_RMReturn()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable ValidateBarcodeFromWIPIssue(string _ReelID,string sWorkOrderNo,string sScanType
            ,string sSiteCode
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(5);
                oDbm.AddParameters(0, "@TYPE", "VALIDATEREELBARCODE");
                oDbm.AddParameters(1, "@PARTBARCODE", _ReelID);
                oDbm.AddParameters(2, "@WORKORDERNO", sWorkOrderNo);
                oDbm.AddParameters(3, "@SITECODE", sSiteCode);
                oDbm.AddParameters(4, "@RETURNTYPE", sScanType);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_RM_RETURN").Tables[0];
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

        public DataTable PrintBarcodeLabel(
            string _MRNNo, string _ReelID, string sScanBy,
decimal dReturnQty, string sSiteCode, string _PartCode,string sWorkOrderno,string sScanType
            ,string sLineCode,string sRemarks
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(11);
                oDbm.AddParameters(0, "@TYPE", "MRNLABELPRINTING");
                oDbm.AddParameters(1, "@PARTBARCODE", _ReelID);
                oDbm.AddParameters(2, "@MRNNO", _MRNNo);
                oDbm.AddParameters(3, "@PRINTEDBY", sScanBy);
                oDbm.AddParameters(4, "@RETURNQTY", dReturnQty);
                oDbm.AddParameters(5, "@SITECODE", sSiteCode);
                oDbm.AddParameters(6, "@PARTCODE", _PartCode);
                oDbm.AddParameters(7, "@LINECODE", sLineCode);
                oDbm.AddParameters(8, "@WORKORDERNO", sWorkOrderno);
                oDbm.AddParameters(9, "@RETURNTYPE", sScanType);
                oDbm.AddParameters(10, "@REMARKS", sRemarks);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_RM_RETURN").Tables[0];
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
