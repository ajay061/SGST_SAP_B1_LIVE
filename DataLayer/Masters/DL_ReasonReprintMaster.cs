using BcilLib;
using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Masters
{
    public class DL_ReasonReprintMaster
    {
        DBManager oDbm;
        public DL_ReasonReprintMaster()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable SaveReasonReprint(string _sReason_of_Reprint
          , string _sCreBy
           )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "SaveReasonOfReprint");
                oDbm.AddParameters(1, "@Reason_of_Reprint", _sReason_of_Reprint);
                oDbm.AddParameters(2, "@CREATED_BY", _sCreBy);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_ReasonReprint_MASTER").Tables[0];
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
        public DataTable SaveReprintData(string sType, string sPrinterIP, string sBarcode, string _sCreBy,string ddlReasonofReprint,string txtRemarks,string _sPrintingType
          )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(7);
                oDbm.AddParameters(0, "@TYPE", "Insert");
                oDbm.AddParameters(1, "@sType", _sPrintingType);
                oDbm.AddParameters(2, "@sPrinterIP", sPrinterIP);
                oDbm.AddParameters(3, "@sBarcode", sBarcode);
                oDbm.AddParameters(4, "@CREATED_BY", _sCreBy);
                oDbm.AddParameters(5, "@ReasonofReprint", ddlReasonofReprint);
                oDbm.AddParameters(6, "@txtRemarks", txtRemarks);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_Reprint_History_Data_Record").Tables[0];
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
        public DataTable BindReasonReprint()
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(1);
                oDbm.AddParameters(0, "@TYPE", "SELECT");
                dt = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_ReasonReprint_MASTER").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dt;
        }
        public DataTable DeleteReasonReprint(string Reprint_ID)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "DELETE");
                oDbm.AddParameters(1, "@Reprint_ID", Reprint_ID);
                dt = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_ReasonReprint_MASTER").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dt;
        }

        public DataTable SearchReasonReprint(string Reprint_ID)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "SEARCH_Reason_Of_Reprint");
                oDbm.AddParameters(1, "@Reprint_ID", Reprint_ID);
                dt = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_ReasonReprint_MASTER").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dt;
        }
        public DataTable UpdateReasonReprint(string ReasonReprint, string _sCreBy, int Reprint_ID)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "UPDATE");
                oDbm.AddParameters(1, "@Reason_of_Reprint", ReasonReprint);
                oDbm.AddParameters(2, "@CREATED_BY", _sCreBy);
                oDbm.AddParameters(3, "@Reprint_ID", Reprint_ID);
                dt = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_ReasonReprint_MASTER").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtError, MethodBase.GetCurrentMethod().Name, ex.Message);
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
