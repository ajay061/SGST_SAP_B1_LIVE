using System;
using System.Data;
using DataLayer;
using Common;
using System.Windows.Forms;

namespace BusinessLayer
{
    public class BL_RM_Issue
    {
        DL_RMIssue dlboj = null;
        public DataTable BINDISSUESLIPNO(string sSiteCode)
        {
            DataTable dtIssueSlipNo = new DataTable();
            dlboj = new DL_RMIssue();
            try
            {
                dtIssueSlipNo = dlboj.BindIssueSlipNo(sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtIssueSlipNo;
        }
        public DataTable BINDDETAILS(string sIssueSlipNo, string sItemCode, string sSiteCode)
        {
            DataTable dtBindDetails = new DataTable();
            dlboj = new DL_RMIssue();
            try
            {
                dtBindDetails = dlboj.GetIssueDetails(sIssueSlipNo, sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtBindDetails;
        }

        public DataTable BindLocationDetails(string sPartCode, string sSiteCode)
        {
            DataTable dtBindDetails = new DataTable();
            dlboj = new DL_RMIssue();
            try
            {
                dtBindDetails = dlboj.GetLocation(sPartCode, sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtBindDetails;
        }
        public string SCANLOCATION(string sLocationBarcode, string _PartCode, string sIssueSlipNo
            , string sSiteCode
            )
        {
            string sLocationResult = string.Empty;
            try
            {
                DataTable dtBindDetails = dlboj.ScanLocation(sLocationBarcode, _PartCode, sIssueSlipNo
                    , sSiteCode
                    );
                if (dtBindDetails.Rows.Count > 0)
                {
                    sLocationResult = dtBindDetails.Rows[0][0].ToString();
                }
                else
                {
                    sLocationResult = "N~No result found for scanned barcode";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sLocationResult;
        }

        public DataTable CloseIssueSlipNo(string sIssueSlipNo, string sSiteCode)
        {
            DataTable dtBindDetails = new DataTable();
            dlboj = new DL_RMIssue();
            try
            {
                dtBindDetails = dlboj.CompleteSlip(sIssueSlipNo, sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtBindDetails;
        }

        public string sScanIssueBarcode(string _sIssueSlipNo, string _PartCode,
            string _ScanLocation, string sScanBy, string sPartBarcode
            , string sSiteCode, string sFIFOByPass, string sLineCode
            )
        {
            dlboj = new DL_RMIssue();
            string sResult = string.Empty;
            DataTable dt = new DataTable();
            try
            {              

                dt = dlboj.ValidateBarcode(
                           _ScanLocation, _PartCode, _sIssueSlipNo,
               sPartBarcode, sScanBy, sSiteCode, sFIFOByPass, sLineCode
                       );
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
                }
                else
                {
                    sResult = "N~No result found, Please try again.";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                sResult = "ERROR~" + ex.Message;
            }
            return sResult;
        }
    }
}
