using Common;
using DataLayer;
using System;
using System.Data;
using System.Text;

namespace BusinessLayer
{
    public class BL_FGPutAway
    {
        DL_FGPutaway dlboj = null;
        public string sScanSecondaryBarcode(string _sBarcode,string sSiteCode,string sLineCode,string sType,string sFIFOType)
        {
            dlboj = new DL_FGPutaway();
            string sResult = string.Empty;
            try
            {
                DataTable dt = dlboj.ValidateBarcode(_sBarcode, sSiteCode, sLineCode, sType, sFIFOType);
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
                }
                else
                {
                    sResult = "N~No result found for scanned barcode";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }

        public string StoreFGLocation(string _sBarcode, string _sScanBy, string _sLocation, string sSiteCode, string sLineCode,string sscanType)
        {
            dlboj = new DL_FGPutaway();
            string sResult = string.Empty;
            try
            {
                DataTable dt = dlboj.ValidateLocation(_sLocation, _sBarcode, _sScanBy, sSiteCode, sLineCode, sscanType);
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
                }
                else
                {
                    sResult = "N~No result found for scanned barcode";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }
    }
}
