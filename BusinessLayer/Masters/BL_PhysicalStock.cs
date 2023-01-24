using Common;
using DataLayer;
using System;
using System.Data;
namespace BusinessLayer
{
    public class BL_PhysicalStock
    {
        DL_PhysicalStock dlboj = null;
        public DataTable GetPhysicalStock(string _sReelID, string _sStockArea)
        {
            DataTable dtPhysicalStock = new DataTable();
            dlboj = new DL_PhysicalStock();
            try
            {
                dtPhysicalStock = dlboj.GetPhysicalStock(_sStockArea, _sReelID);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtPhysicalStock;
        }

        public string SavePhysicalStock(decimal _sQuantity, string _sReelID, string _sStockArea, string _sScanBy)
        {
            DataTable dtPhysicalStock = new DataTable();
            dlboj = new DL_PhysicalStock();
            string sResult = string.Empty;
            try
            {
                dtPhysicalStock = dlboj.UpdatePhysicalStock(_sStockArea, _sReelID, _sQuantity, _sScanBy);
                if (dtPhysicalStock.Rows.Count > 0)
                {
                    sResult = dtPhysicalStock.Rows[0][0].ToString();
                }
                else 
                {
                    return "N~No result found";
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
