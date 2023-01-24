using Common;
using System;
using System.Data;
using System.Text;

namespace DataLayer.RM
{
    public class DL_ProductionLoss
    {
        DBManager oDbm;
        public DL_ProductionLoss()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable ValidateBarcodeFromWIPIssue(string _ReelID, string sWorkOrderNo
            ,string sSiteCode
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "VALIDATEREELBARCODE");
                oDbm.AddParameters(1, "@PARTBARCODE", _ReelID);
                oDbm.AddParameters(2, "@WORKORDERNO", sWorkOrderNo);
                oDbm.AddParameters(3, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_PRODUCTIONLOSS").Tables[0];
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
           string _ReelID, string sScanBy,decimal dLossQty, string _PartCode, string sWorkOrderno
            ,string sLineCode,string sSiteCode

            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(8);
                oDbm.AddParameters(0, "@TYPE", "LOSSRETURN");
                oDbm.AddParameters(1, "@PARTBARCODE", _ReelID);               
                oDbm.AddParameters(2, "@PRINTEDBY", sScanBy);
                oDbm.AddParameters(3, "@RETURNQTY", dLossQty);
                oDbm.AddParameters(4, "@SITECODE", sSiteCode);
                oDbm.AddParameters(5, "@PARTCODE", _PartCode);
                oDbm.AddParameters(6, "@LINECODE", sLineCode);
                oDbm.AddParameters(7, "@WORKORDERNO", sWorkOrderno);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_PRODUCTIONLOSS").Tables[0];
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
