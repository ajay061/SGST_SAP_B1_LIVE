using Common;
using DataLayer;
using System;
using System.Data;

namespace BusinessLayer
{
    public class BL_Putaway
    {
        DL_Putaway dlobj = null;
        public DataTable GetLocationCode(string _sBarcode)
        {
            DataTable dtLocation = new DataTable();
            dlobj = new DL_Putaway();
            try
            {
                dtLocation = dlobj.GetLocation(_sBarcode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLocation;
        }

        public string SaveBarcode(string _Location, string _PartBarcode,string sUserID)
        {
            string sResult = string.Empty;
            dlobj = new DL_Putaway();
            DataTable dt = new DataTable();
            try
            {
                dt = dlobj.ValidateBarcode(_Location, _PartBarcode, sUserID);
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
