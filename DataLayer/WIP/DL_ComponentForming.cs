using Common;
using System;
using System.Data;
using System.Text;

namespace DataLayer.WIP
{    
    public class DL_ComponentForming
    {
        DBManager oDbm;
        public DL_ComponentForming()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable BindSFGItemCode()
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDSFGITEMCODE");
                oDbm.AddParameters(1, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_COMPONENT_REEL_PRINTING").Tables[0];
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

        public DataTable ValidateTool(string sFORMING_TOOL_ID, string sSFGItemCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "VALIDATETOOL");
                oDbm.AddParameters(1, "@TOOLBARCODE", sFORMING_TOOL_ID);
                oDbm.AddParameters(2, "@SFGITEMCODE", sSFGItemCode);
                oDbm.AddParameters(3, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_COMPONENT_REEL_PRINTING").Tables[0];
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

        public DataTable ValidateReel(string _Part_Barcode, string sSFGItemCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "VALIDATEREEL");
                oDbm.AddParameters(1, "@REELBARCODE", _Part_Barcode);
                oDbm.AddParameters(2, "@SFGITEMCODE", sSFGItemCode);
                oDbm.AddParameters(3, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_COMPONENT_REEL_PRINTING").Tables[0];
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

        public DataTable BindData(StringBuilder sb)
        {
            DataTable dtBindData = new DataTable();
            try
            {
                oDbm.Open();
                dtBindData = oDbm.ExecuteDataSet(System.Data.CommandType.Text, sb.ToString()).Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message + ", Query " + sb.ToString());
                throw ex;
            }
            finally
            {
                oDbm.Close();
            }
            return dtBindData;
        }
       
        public int iValidateBarcode(StringBuilder sb)
        {
            int iResult = 0;
            try
            {
                oDbm.Open();
                iResult = Convert.ToInt32(oDbm.ExecuteScalar(System.Data.CommandType.Text, sb.ToString()));
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message + ", Query " + sb.ToString());
                throw ex;
            }
            finally
            {
                oDbm.Close();
            }
            return iResult;
        }
        public int SaveBarcode(StringBuilder sb)
        {
            int iResult = 0;
            try
            {
                oDbm.Open();
                oDbm.BeginTransaction();
                iResult = oDbm.ExecuteNonQuery(System.Data.CommandType.Text, sb.ToString());
                if (iResult > 0)
                {
                    oDbm.CommitTransaction();
                }
                else
                {
                    oDbm.RollbackTransaction();
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                oDbm.RollbackTransaction();
                throw ex;
            }
            finally
            {
                oDbm.Close();
            }
            return iResult;
        }
    }
}
