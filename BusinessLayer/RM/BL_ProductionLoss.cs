using System;
using DataLayer;
using DataLayer.RM;
using System.Data;
using Common;


namespace BusinessLayer.RM
{
    public class BL_ProductionLoss
    {
        DL_ProductionLoss dlobj = new DL_ProductionLoss();
        public string ValidateBarcodeFromWIPIssue(string _ReelID, string sWorkOrderNo,string sSiteCode)
        {
            string sResult = string.Empty;
            try
            {
                dlobj = new DL_ProductionLoss();
                DataTable dt = dlobj.ValidateBarcodeFromWIPIssue(_ReelID, sWorkOrderNo, sSiteCode);
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
                }
                else
                {
                    sResult = "N~No result found scanned barcode";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }
        public string PrintBarcode(string _Part_Barcode, string sScanBy,
            decimal dReturnQty,string _PartCode, string sWorkOrderNo,string sSiteCode,string sLineCode)
        {
            string sResult = string.Empty;
            dlobj = new DL_ProductionLoss();
            try
            {
                DataTable dtData = dlobj.PrintBarcodeLabel(_Part_Barcode, sScanBy, dReturnQty, _PartCode, sWorkOrderNo
                    ,sSiteCode,sLineCode
                    );
                if (dtData.Rows.Count > 0)
                {
                    sResult = dtData.Rows[0][0].ToString();
                }
                else
                {
                    sResult = "N~No result found table, Please try again.";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }
    }
}
