using Common;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer
{
    public class DL_WIPLaserMachinePrinting
    {
        DBManager oDbm;
        public DL_WIPLaserMachinePrinting()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable ValidateMachine(string sMachineID, string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "VALIDATEMACHINE");
                oDbm.AddParameters(1, "@MACHINEID", sMachineID);
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_STORE_LASER_FILE").Tables[0];
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
        public DataTable BindWorkOrderNo(string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDRESERVATION");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_STORE_LASER_FILE").Tables[0];
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
        public DataTable BindType(string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDPROCESSTYPE");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_STORE_LASER_FILE").Tables[0];
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
        public DataTable GetProcssDetails(string sSiteCode,string sProcessType)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "GETPROCESSTYPEDETAILS");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.AddParameters(2, "@PROCESSTYPE", sProcessType);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_STORE_LASER_FILE").Tables[0];
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
        public DataTable BindFGItemCode(string sType, string sWorkOrderNo, string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "BINDFGITEMCODE");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.AddParameters(2, "@ISSUE_SLIPNO", sWorkOrderNo);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_STORE_LASER_FILE").Tables[0];
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

        public DataSet BindCustomerCode(string sFGItemCode, string sSiteCode)
        {
            DataSet dt = new DataSet();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "BINDCUSTOMERCODE");
                oDbm.AddParameters(1, "@FGITEMCODE", sFGItemCode);
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_STORE_LASER_FILE");
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

        public DataTable GetPurchaseOrderDetails(string sSiteCode,string sFGItemCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "GETCOUNTDETAILS");
                oDbm.AddParameters(1, "@FGITEMCODE", sFGItemCode);
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_STORE_LASER_FILE_TMO").Tables[0];
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

        public DataTable ValidateBarcode(string sScanBarcode, string sFGItemCode, string sMachineCode, string sCustomerCode
            , string sWorkOrderNo, string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(7);
                oDbm.AddParameters(0, "@TYPE", "VALIDATEPARTBARCODE");
                oDbm.AddParameters(1, "@FGITEMCODE", sFGItemCode);
                oDbm.AddParameters(2, "@PART_BARCODE", sScanBarcode);
                oDbm.AddParameters(3, "@MACHINEID", sMachineCode);
                oDbm.AddParameters(4, "@SITECODE", sSiteCode);
                oDbm.AddParameters(5, "@CUSTOMERPARTCODE", sCustomerCode);
                oDbm.AddParameters(6, "@ISSUE_SLIPNO", sWorkOrderNo);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_STORE_LASER_FILE").Tables[0];
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

        public DataTable GettopBarcode(string sPartCode, string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "GETTOPBARCODE");
                oDbm.AddParameters(1, "@PART_CODE", sPartCode);
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_STORE_LASER_FILE").Tables[0];
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
        public string GenerateSN(string sFGItemCode, string sCustomer, string sSiteCode)
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
                        cmd.Parameters.AddWithValue("@BARCODEGENERATEFOR", "PCB");
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

        public string GenerateSN_FGASSEMBLY(string sFGItemCode, string sCustomer, string sSiteCode)
        {
            string sSerialNo = string.Empty;
            DataTable dt = new DataTable();
            string sResult = string.Empty;
            try
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,
                           System.Reflection.MethodBase.GetCurrentMethod().Name, "FG Assembly Module : " +
                           ",FG item code : " + sFGItemCode +
                           ",Customer Code  : " + sCustomer +
                          ", Getting SN No. " );
                using (SqlConnection con = new SqlConnection(oDbm.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("USP_GETSERIAL_WITH_TABLE_PCB", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@BARCODEGENERATEFOR", "PCB");
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
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, 
                    System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message + ", Query " + "USP_GETSERIAL_WITH_TABLE_PCB");
                throw ex;
            }
            finally
            {
                //oDbm.Close();
                //oDbm.Dispose();
            }
            return sSerialNo;
        }



        public DataTable StoreData(string sGRPONo, string sWorkOrderNo, string sPartCode,
            string sPartBarcode, DataTable dtLaserFile,
           int iQty, int iArraySize, string sBatchNo
            , int iLastGeneratedSN, string sSiteCode, string sUserID, string sLineCode
            ,string sPacketType
            )
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(14);
                oDbm.AddParameters(0, "@TYPE", "STORELASERFILE");
                oDbm.AddParameters(1, "@GRPONO", sGRPONo);
                oDbm.AddParameters(2, "@ISSUE_SLIPNO", sWorkOrderNo);
                oDbm.AddParameters(3, "@PART_CODE", sPartCode);
                oDbm.AddParameters(4, "@BATCHNO", sBatchNo);
                oDbm.AddParameters(5, "@PART_BARCODE", sPartBarcode);
                oDbm.AddParameters(6, "@LASTGENERATESNNO", iLastGeneratedSN);
                oDbm.AddParameters(7, "@QTY", iQty);
                oDbm.AddParameters(8, "@ARRAYSIZE", iArraySize);
                oDbm.AddParameters(9, "@DTCHILSN", dtLaserFile);
                oDbm.AddParameters(10, "@PRINTEDBY", sUserID);
                oDbm.AddParameters(11, "@SITECODE", sSiteCode);
                oDbm.AddParameters(12, "@LINECODE", sLineCode);
                oDbm.AddParameters(13, "@PACKETTYPE", sPacketType);
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, "Packet Barcode : " + sPartBarcode +
                    ",Array Size:" + iArraySize.ToString() + ",Last SN Generation : " + iLastGeneratedSN.ToString()
                    + ", Work Order No :" + sWorkOrderNo + ", Part Code : " + sPartCode + ", Qty : " + iQty.ToString()
                    + ",Line Code : " + sLineCode
                    );
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_STORE_LASER_FILE").Tables[0];
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


        public DataTable StoreTMOBarcode(string sGRPONo, string sWorkOrderNo, string sPartCode,
            string sPartBarcode, string sPurchaseOrder,
           int iQty, int iArraySize, string sCustomerPartCode
            , int iLastGeneratedSN, string sSiteCode, string sUserID, string sLineCode
            , string sFGItemCode, string sPacketType)
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(15);
                oDbm.AddParameters(0, "@TYPE", "STORETMOLASERFILE");
                oDbm.AddParameters(1, "@GRPONO", sGRPONo);
                oDbm.AddParameters(2, "@ISSUE_SLIPNO", sWorkOrderNo);
                oDbm.AddParameters(3, "@PART_CODE", sPartCode);
                oDbm.AddParameters(4, "@CUSTOMERPARTCODE", sCustomerPartCode);
                oDbm.AddParameters(5, "@PART_BARCODE", sPartBarcode);
                oDbm.AddParameters(6, "@LASTGENERATESNNO", iLastGeneratedSN);
                oDbm.AddParameters(7, "@QTY", iQty);
                oDbm.AddParameters(8, "@ARRAYSIZE", iArraySize);
                oDbm.AddParameters(9, "@PURCHASEORDER", sPurchaseOrder);
                oDbm.AddParameters(10, "@PRINTEDBY", sUserID);
                oDbm.AddParameters(11, "@SITECODE", sSiteCode);
                oDbm.AddParameters(12, "@LINECODE", sLineCode);
                oDbm.AddParameters(13, "@FGITEMCODE", sFGItemCode);
                oDbm.AddParameters(14, "@PACKETTYPE", sPacketType);
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, "Packet Barcode : " + sPartBarcode +
                    ",Array Size:" + iArraySize.ToString() + ",Last SN Generation : " + iLastGeneratedSN.ToString()
                    + ", Work Order No :" + sWorkOrderNo + ", Part Code : " + sPartCode + ", Qty : " + iQty.ToString()
                    + ",Line Code : " + sLineCode
                    );
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_STORE_LASER_FILE_TMO").Tables[0];
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


        #region PCB Printing

        public DataTable BindLPFGItemCode(string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDLPFGITEMCODE");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_STORE_LASER_FILE").Tables[0];
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
        public DataTable BindLPWorkOrderno(string sFGItemCode, string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "BINDLPWORKORDERNO");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.AddParameters(2, "@FGITEMCODE", sFGItemCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_STORE_LASER_FILE").Tables[0];
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
        public DataTable BindPendingBarcode(string sFGItemCode, string sWorkOrderno, string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "BINDPENDINGBARCODE");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.AddParameters(2, "@ISSUE_SLIPNO", sWorkOrderno);
                oDbm.AddParameters(3, "@FGITEMCODE", sFGItemCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_STORE_LASER_FILE").Tables[0];
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
        public DataTable ValidatePCBBarcodeForLaserFilePrint(string sBarcode, string sWorkOrderno, string sFGItemCode
            , string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(5);
                oDbm.AddParameters(0, "@TYPE", "VALIDATEBARCODE");
                oDbm.AddParameters(1, "@PART_BARCODE", sBarcode);
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                oDbm.AddParameters(3, "@ISSUE_SLIPNO", sWorkOrderno);
                oDbm.AddParameters(4, "@FGITEMCODE", sFGItemCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_STORE_LASER_FILE").Tables[0];
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
        public DataTable UpdateSNStatus(string sPartBarcode, string sSiteCode, string sUserID)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "UPDATEBARCODE");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.AddParameters(2, "@PART_BARCODE", sPartBarcode);
                oDbm.AddParameters(3, "@PRINTEDBY", sUserID);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_STORE_LASER_FILE").Tables[0];
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
        #endregion


        #region PCB MAPPING

        public DataTable BindMappindWorkOrderNo(string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDRESERVATION");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_PCB_MAPPING").Tables[0];
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

        public DataTable ValidatePCBBarcode(string sSiteCode,string sWorkOrderNo,string sPCBBarcode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "VALIDATEBARCODE");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.AddParameters(2, "@ISSUE_SLIPNO", sWorkOrderNo);
                oDbm.AddParameters(3, "@PARTBARCODE", sPCBBarcode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_PCB_MAPPING").Tables[0];
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

        public DataTable UpdatePCBMappingData(string sSiteCode, string sWorkOrderNo, string sPCBBarcode,
            DataTable dtLaserFile, string sMappedBy)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(6);
                oDbm.AddParameters(0, "@TYPE", "UPDATEPCBMAPPING");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.AddParameters(2, "@ISSUE_SLIPNO", sWorkOrderNo);
                oDbm.AddParameters(3, "@PARTBARCODE", sPCBBarcode);
                oDbm.AddParameters(4, "@DTCHILSN", dtLaserFile);
                oDbm.AddParameters(5, "@MAPPED_BY", sMappedBy);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_PCB_MAPPING").Tables[0];
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

        #endregion

    }
}
