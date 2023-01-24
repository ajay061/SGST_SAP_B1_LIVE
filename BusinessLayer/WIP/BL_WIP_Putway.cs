using Common;
using DataLayer.WIP;
using System;
using System.Data;


namespace BusinessLayer.WIP
{
    public class BL_WIP_Putway
    {
        DL_WIPPutway dlobj = null;
        public DataTable GetLocationCode(string _sLocationCode, string sSiteCode)
        {
            DataTable dtLocation = new DataTable();
            dlobj = new DL_WIPPutway();
            try
            {
                dtLocation = dlobj.ValidateLocation(_sLocationCode, sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLocation;
        }

        public string SaveBarcode(string _Location, string _PartBarcode, string sSiteCode, string sUserID, string sLineCode
            ,string sFIFORequired)
        {
            string sResult = string.Empty;
            dlobj = new DL_WIPPutway();
            DataTable dt = new DataTable();
            try
            {
                dt = dlobj.ValidateBarcode(_Location, _PartBarcode, sSiteCode, sUserID, sLineCode, sFIFORequired);
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
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }
    }
}
