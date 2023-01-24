using Common;
using System;
using System.Data;
namespace DataLayer
{
    public class DL_ChildLabelPrinting
    {
        DBManager oDbm;
        public DL_ChildLabelPrinting()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }

        public DataTable BINDINEL_PARTNO(string sType,string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "BINDPRINTINGTYPE");
                oDbm.AddParameters(1, "@PTYPE", sType);
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_RM_CHILD_LABEL_PRINTING").Tables[0];
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

        public DataTable BindReelBarcode(string sType, string sPartCode, string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "BINDREELBARCODE");
                oDbm.AddParameters(1, "@PTYPE", sType);
                oDbm.AddParameters(2, "@PARTCODE", sPartCode);
                oDbm.AddParameters(3, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_RM_CHILD_LABEL_PRINTING").Tables[0];
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

        public DataTable ValidateReelBarcode(string sType, string sPartCode,string sPartBarcode
            , string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(5);
                oDbm.AddParameters(0, "@TYPE", "VALIDATEREELBARCODE");
                oDbm.AddParameters(1, "@PTYPE", sType);
                oDbm.AddParameters(2, "@PARTCODE", sPartCode);
                oDbm.AddParameters(3, "@PARTBARCODE", sPartBarcode);
                oDbm.AddParameters(4, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_RM_CHILD_LABEL_PRINTING").Tables[0];
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

        public DataTable ChildLabelPrinting(string sPartCode, string sPartBarcode
            ,decimal dQty, string sPrintedBy,string sSiteCode,string sLineCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(7);
                oDbm.AddParameters(0, "@TYPE", "CHILDLABELPRINTING");
                oDbm.AddParameters(1, "@PARTCODE", sPartCode);
                oDbm.AddParameters(2, "@PARTBARCODE", sPartBarcode);
                oDbm.AddParameters(3, "@UPDATEDQTY", dQty);
                oDbm.AddParameters(4, "@PRINTEDBY", sPrintedBy);
                oDbm.AddParameters(5, "@SITECODE", sSiteCode);
                oDbm.AddParameters(6, "@LINECODE", sLineCode);

                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_RM_CHILD_LABEL_PRINTING").Tables[0];
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
