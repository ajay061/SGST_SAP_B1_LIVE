using Common;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer.WIP
{
    public class DL_WIPSFGAssembly
    {
        DBManager oDbm;
        public DL_WIPSFGAssembly()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable BindWorkOrderNo(string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDWONO");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_SFG_ASSEMBLY").Tables[0];
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
        public DataTable ValidateMachine(string sMachineID, string sLineiD, string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "VALIDATEMACHINE");
                oDbm.AddParameters(1, "@MACHINEID", sMachineID);
                oDbm.AddParameters(2, "@LINECODE", sLineiD);
                oDbm.AddParameters(3, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_SFG_ASSEMBLY").Tables[0];
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
        public DataTable BindFGItemCode(string sWorkOrderNo, string sSiteCode, string sMachineID
            , string sLineCode
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(5);
                oDbm.AddParameters(0, "@TYPE", "BINDFGITEMCODE");
                oDbm.AddParameters(1, "@WORKORDERNO", sWorkOrderNo);
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                oDbm.AddParameters(3, "@MACHINEID", sMachineID);
                oDbm.AddParameters(4, "@LINECODE", sLineCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_SFG_ASSEMBLY").Tables[0];
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
        public DataTable BindCustomerCode(string sFGItemCode, string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "BINDCUSTOMERCODE");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.AddParameters(2, "@FGITEMCODE", sFGItemCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_SFG_ASSEMBLY").Tables[0];
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

        public DataTable ValidateBarcode(string sScanBarcode, string sFGItemCode, string sCustomerPartCode
            , string sSiteCode, string sLineCode,string sWorkOrderNo,string sMachineID 
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(8);
                oDbm.AddParameters(0, "@TYPE", "VALIDATEPARTBARCODE");
                oDbm.AddParameters(1, "@FGITEMCODE", sFGItemCode);
                oDbm.AddParameters(2, "@PARTBARCODE", sScanBarcode);
                oDbm.AddParameters(3, "@SITECODE", sSiteCode);
                oDbm.AddParameters(4, "@LINECODE", sLineCode);
                oDbm.AddParameters(5, "@CUSTOMERPARTCODE", sCustomerPartCode);
                oDbm.AddParameters(6, "@WORKORDERNO", sWorkOrderNo);
                oDbm.AddParameters(7, "@MACHINEID", sMachineID);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_SFG_ASSEMBLY").Tables[0];
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

        public string GenerateSN_FGASSEMBLY(string sFGItemCode, string sCustomer, string sSiteCode)
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
                        cmd.Parameters.AddWithValue("@BARCODEGENERATEFOR", "FG Assembly");
                        cmd.Parameters.AddWithValue("@SITE_CODE", sSiteCode);
                        cmd.Parameters.AddWithValue("@CUSTOMER", sCustomer);
                        cmd.Parameters.AddWithValue("@FGITEMCODE", sFGItemCode);
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
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message + ", Query " + "USP_GETLASERSERIAL");
                throw ex;
            }
            finally
            {
                //oDbm.Close();
                //oDbm.Dispose();
            }
            return sSerialNo;
        }
        public DataTable StorePCBData(DataTable dtLaserFile,
        string sFGItemCode, string sSiteCode, string sLineCode, string sUserID, string sMachineID, string sWorkOrderNo
           ,DataTable dtAssemblyBarcode,string sPartBarcode,string sPCBBarcode )
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(11);
                oDbm.AddParameters(0, "@TYPE", "STORELASERFILE");
                oDbm.AddParameters(1, "@FGITEMCODE", sFGItemCode);
                oDbm.AddParameters(2, "@USER_ID", sUserID);
                oDbm.AddParameters(3, "@DTCHILSN_L", dtLaserFile);
                oDbm.AddParameters(4, "@SITECODE", sSiteCode);
                oDbm.AddParameters(5, "@LINECODE", sLineCode);
                oDbm.AddParameters(6, "@MACHINEID", sMachineID);
                oDbm.AddParameters(7, "@WORKORDERNO", sWorkOrderNo);
                oDbm.AddParameters(8, "@DTCHILSN_A", dtAssemblyBarcode);
                oDbm.AddParameters(9, "@PARTBARCODE", sPartBarcode);
                oDbm.AddParameters(10, "@BARCODE", sPCBBarcode);
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_WIP_SFG_ASSEMBLY").Tables[0];
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
    }
}
