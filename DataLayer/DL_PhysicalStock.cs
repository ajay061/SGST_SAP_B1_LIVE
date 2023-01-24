using Common;
using System;
using System.Data;
using System.Text;

namespace DataLayer
{
    public class DL_PhysicalStock
    {
        DBManager oDbm;

        public DL_PhysicalStock()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }

        public DataTable GetPhysicalStock(string sStockArea,string sBarcode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "GETDETAILS");
                oDbm.AddParameters(1, "@STOCKAREA", sStockArea);
                oDbm.AddParameters(2, "@BARCODE", sBarcode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_PHYSICAL_STOCK_TAKE").Tables[0];
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

        public DataTable UpdatePhysicalStock(string sStockArea, string sBarcode, decimal dQty, string sScannedBy)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(5);
                oDbm.AddParameters(0, "@TYPE", "UPDATESTOCK");
                oDbm.AddParameters(1, "@STOCKAREA", sStockArea);
                oDbm.AddParameters(2, "@BARCODE", sBarcode);
                oDbm.AddParameters(3, "@QTY", dQty);
                oDbm.AddParameters(4, "@SCANNEDBY", sScannedBy);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_PHYSICAL_STOCK_TAKE").Tables[0];
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
