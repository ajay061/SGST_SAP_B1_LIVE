using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Data;
using PL;
using System.Data.SqlClient;

namespace DataLayer.WIP
{
    public class DL_WIP_Serial_Generation
    {
        DBManager oDbm;
        public DL_WIP_Serial_Generation()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable BindBarcode_Gen()
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDBARCODEGENERATETYPE");
                oDbm.AddParameters(1, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_FILDATA").Tables[0];
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
        public DataTable BindPlantCode()
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(1);
                oDbm.AddParameters(0, "@TYPE", "BINDSITECODE");
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_FILDATA").Tables[0];
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
        public DataTable GetCustomer(string sFGItemCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "BINDCUSTOMERCODE");
                oDbm.AddParameters(1, "@SITECODE", PCommon.sSiteCode);
                oDbm.AddParameters(2, "@FGITEMCODE", sFGItemCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_FILDATA").Tables[0];
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
        public DataTable GetFGItemCode(string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDFGITEMCODE");
                oDbm.AddParameters(1, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_FILDATA").Tables[0];
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
        public DataTable GetRestPeriod(string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDRESETPERIOD");
                oDbm.AddParameters(1, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_FILDATA").Tables[0];
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
        public DataTable GetPrefix(string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDDATACODE");
                oDbm.AddParameters(1, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_FILDATA").Tables[0];
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
        public DataTable GetFormat(string sFormat)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "BINDDATAFORMAT");
                oDbm.AddParameters(1, "@DATATYPE", sFormat);
                oDbm.AddParameters(2, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_FILDATA").Tables[0];
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


        public DataTable GetDetailsData()
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "GETRECORD");
                oDbm.AddParameters(1, "@SITE_CODE", PCommon.sSiteCode);
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_STORE_LOGIC_DATA_WITH_TABLE").Tables[0];
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
            return dtResult;
        }
      

        public DataTable dlSaveData(PL_WIP_SerialGeneration plobj)
        {
            DataTable dtobj = new DataTable();
            try
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtMessage, System.Reflection.MethodBase.GetCurrentMethod().Name, "Serial Generation Logic Update for Fg Item Code :" + plobj.sFGItemCode);
                StringBuilder sb = new StringBuilder();
                oDbm.CreateParameters(19);
                oDbm.AddParameters(0, "@TYPE", "SAVERECORD");
                oDbm.AddParameters(1, "@BARCODE_GENERATE_FOR", plobj.sBarcodeGenerationType);
                oDbm.AddParameters(2, "@SITE_CODE", plobj.sSiteCode);
                oDbm.AddParameters(3, "@CUSTOMER", plobj.sCustomer);
                oDbm.AddParameters(4, "@PART_NO", plobj.sPartCode);
                oDbm.AddParameters(5, "@PART_DESC", plobj.sPartDesc);
                oDbm.AddParameters(6, "@FG_ITEM_CODE", plobj.sFGItemCode);
                oDbm.AddParameters(7, "@REVISION", plobj.Revision);
                oDbm.AddParameters(8, "@FG_QTY_PER_BOX", plobj.iFGQtyPerBox);
                oDbm.AddParameters(9, "@START_NO", plobj.StartNo);
                oDbm.AddParameters(10, "@LENGTH", plobj.iLength);
                oDbm.AddParameters(11, "@RESET_PERIOD", plobj.sResetPeriod);
                oDbm.AddParameters(12, "@PREFIX", plobj.sPrefix);
                oDbm.AddParameters(13, "@PRN_FILE", plobj.sprn);
                oDbm.AddParameters(14, "@DESIGNER_FORMAT", plobj.sDesignerFormat);
                oDbm.AddParameters(15, "@ACTIVE", plobj.iActive);
                oDbm.AddParameters(16, "@DETAILS", plobj.dtRecord);
                oDbm.AddParameters(17, "@SUFFIX", plobj.sOtherValue);
                oDbm.AddParameters(18, "@PAGELABELCOUNT", plobj.iPageLabelCount);
                sb.Append("USP_STORE_LOGIC_DATA_WITH_TABLE");
                oDbm.Open();
                dtobj = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, sb.ToString()).Tables[0];
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
            return dtobj;
        }
        public DataTable dlUpdateData(PL_WIP_SerialGeneration plobj)
        {
            DataTable dtobj = new DataTable();
            try
            {
                StringBuilder sb = new StringBuilder();
                oDbm.CreateParameters(20);
                oDbm.AddParameters(0, "@TYPE", "UPDATE");
                oDbm.AddParameters(1, "@BARCODE_GENERATE_FOR", plobj.sBarcodeGenerationType);
                oDbm.AddParameters(2, "@SITE_CODE", plobj.sSiteCode);
                oDbm.AddParameters(3, "@CUSTOMER", plobj.sCustomer);
                oDbm.AddParameters(4, "@PART_NO", plobj.sPartCode);
                oDbm.AddParameters(5, "@PART_DESC", plobj.sPartDesc);
                oDbm.AddParameters(6, "@FG_ITEM_CODE", plobj.sFGItemCode);
                oDbm.AddParameters(7, "@REVISION", plobj.Revision);
                oDbm.AddParameters(8, "@FG_QTY_PER_BOX", plobj.iFGQtyPerBox);
                oDbm.AddParameters(9, "@START_NO", plobj.StartNo);
                oDbm.AddParameters(10, "@LENGTH", plobj.iLength);
                oDbm.AddParameters(11, "@RESET_PERIOD", plobj.sResetPeriod);
                oDbm.AddParameters(12, "@PREFIX", plobj.sPrefix);
                oDbm.AddParameters(13, "@PRN_FILE", plobj.sprn);
                oDbm.AddParameters(14, "@DESIGNER_FORMAT", plobj.sDesignerFormat);
                oDbm.AddParameters(15, "@ACTIVE", plobj.iActive);
                oDbm.AddParameters(16, "@DETAILS", plobj.dtRecord);
                oDbm.AddParameters(17, "@P_ID", plobj.iPID);
                oDbm.AddParameters(18, "@SUFFIX", plobj.sOtherValue);
                oDbm.AddParameters(19, "@PAGELABELCOUNT", plobj.iPageLabelCount);
                sb.Append("USP_STORE_LOGIC_DATA_WITH_TABLE");
                oDbm.Open();
                dtobj = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, sb.ToString()).Tables[0];
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
            return dtobj;
        }
        public DataTable GetEDITData(int PID)
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "GETRECORD_EDIT");
                oDbm.AddParameters(1, "@P_ID", PID);
                oDbm.AddParameters(2, "@SITE_CODE", PCommon.sSiteCode);
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_STORE_LOGIC_DATA_WITH_TABLE").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dtResult;
        }
        public DataTable GetFormatData(int PID)
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "GETRECORDDATA");
                oDbm.AddParameters(1, "@P_ID", PID);
                oDbm.AddParameters(2, "@SITE_CODE", PCommon.sSiteCode);
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_STORE_LOGIC_DATA_WITH_TABLE").Tables[0];
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
            return dtResult;
        }

        public string GenerateSN(string sFGItemCode, string sCustomer, string sSiteCode,string sType)
        {
            string sSerialNo = string.Empty;
            DataTable dt = new DataTable();
            string sResult = string.Empty;
            try
            {
                using (SqlConnection con = new SqlConnection(oDbm.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("USP_GETSERIAL_WITH_TABLE_PCB", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@BARCODEGENERATEFOR", sType);
                        cmd.Parameters.AddWithValue("@SITE_CODE", sSiteCode);
                        cmd.Parameters.AddWithValue("@CUSTOMER", sCustomer);
                        cmd.Parameters.AddWithValue("@FGITEMCODE", sFGItemCode);
                        cmd.Parameters.AddWithValue("@ISSNGET", "1");
                        cmd.Parameters.Add("@FINALRESULT", SqlDbType.VarChar, 100);
                        cmd.Parameters["@FINALRESULT"].Direction = ParameterDirection.Output;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        sSerialNo = cmd.Parameters["@FINALRESULT"].Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message + ", Query " + "USP_GETLASERSERIAL");
                throw ex;
            }
            finally
            {
                //oDbm.Close();
                //oDbm.Dispose();
            }
            return sSerialNo;
        }
    }
}
