using Common;
using System;
using System.Data;

namespace DataLayer
{
    public class DL_RMIssue
    {
        DBManager oDbm;
        public DL_RMIssue()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable BindIssueSlipNo(string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDISSUESLIPNO");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_RM_ISSUE_SHOP_FLOR").Tables[0];
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

        public DataTable GetIssueDetails(string sIssueSlipNo,string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "BINDISSUEDETAILS");
                oDbm.AddParameters(1, "@ISSUESLIPNO", sIssueSlipNo);
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_RM_ISSUE_SHOP_FLOR").Tables[0];
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

        public DataTable CompleteSlip(string sIssueSlipNo,string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "TRANSFERTOSAP");
                oDbm.AddParameters(1, "@ISSUESLIPNO", sIssueSlipNo);
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_RM_ISSUE_SHOP_FLOR").Tables[0];
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

        public DataTable GetLocation(string sPartCode,string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "GETLOCATION");
                oDbm.AddParameters(1, "@PARTCODE", sPartCode);
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_RM_ISSUE_SHOP_FLOR").Tables[0];
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

        public DataTable ScanLocation(string sLocation, string _PartCode, string sIssueSlipNo
            ,string sSiteCode
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(5);
                oDbm.AddParameters(0, "@TYPE", "VALIDATELOCATION");
                oDbm.AddParameters(1, "@PARTCODE", _PartCode);
                oDbm.AddParameters(2, "@ISSUESLIPNO", sIssueSlipNo);
                oDbm.AddParameters(3, "@LOCATION", sLocation);
                oDbm.AddParameters(4, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_RM_ISSUE_SHOP_FLOR").Tables[0];
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

        public DataTable ValidateBarcode(string sLocation, string _PartCode, string sIssueSlipNo,
            string sPartBarcode, string sScannedby
            , string sSiteCode, string sFIFORequired
            , string  sLineCode
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(9);
                oDbm.AddParameters(0, "@TYPE", "VALIDATEBARCODE");
                oDbm.AddParameters(1, "@PARTCODE", _PartCode);
                oDbm.AddParameters(2, "@ISSUESLIPNO", sIssueSlipNo);
                oDbm.AddParameters(3, "@LOCATION", sLocation);
                oDbm.AddParameters(4, "@PARTBARCODE", sPartBarcode);
                oDbm.AddParameters(5, "@PRINTEDBY", sScannedby);
                oDbm.AddParameters(6, "@SITECODE", sSiteCode);              
                oDbm.AddParameters(7, "@LINECODE", sLineCode);
                oDbm.AddParameters(8, "@FIFOREQUIRED", sFIFORequired);
                oDbm.Open();
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.MethodBase.GetCurrentMethod().Name,
                    "USP_RM_ISSUE_SHOP_FLOR: Location :" + sLocation + ", Work Ordr No:" + sIssueSlipNo +",Barcode : " + sPartBarcode);
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_RM_ISSUE_SHOP_FLOR").Tables[0];
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.MethodBase.GetCurrentMethod().Name,
                    "USP_RM_ISSUE_SHOP_FLOR : Location :" + sLocation + ", Work Ordr No:" + sIssueSlipNo + ",Barcode : " + sPartBarcode + ", Result :" + dt.Rows[0][0].ToString());
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
