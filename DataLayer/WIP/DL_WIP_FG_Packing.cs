using Common;
using PL;
using System;
using System.Data;
namespace DataLayer.WIP
{
    public class DL_WIP_FG_Packing
    {
        DBManager oDbm;
        public DL_WIP_FG_Packing()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable BindFGItemCode(string sSiteCode,string sScanType)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "BINDFGITEMCODE");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.AddParameters(2, "@SCANTYPE", sScanType);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_FG_BOX_PACKING_BIND").Tables[0];
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
        public DataTable BindWorkOrderNo(string sSiteCode,string sFGItemCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "BINDWORKORDERNO");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.AddParameters(2, "@FGITEMCODE", sFGItemCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_FG_BOX_PACKING_BIND").Tables[0];
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
        
        public DataTable GetModelDetails(PL_Printing plobj)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "GETMODELDETAILS");
                oDbm.AddParameters(1, "@SITECODE", plobj.sSiteCode);
                oDbm.AddParameters(2, "@FGITEMCODE", plobj.sBOMCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_FG_BOX_PACKING_BIND").Tables[0];
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
        public DataTable GetWODetails(PL_Printing plobj)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "GETWODETAILS");
                oDbm.AddParameters(1, "@SITECODE", plobj.sSiteCode);
                oDbm.AddParameters(2, "@FGITEMCODE", plobj.sBOMCode);
                oDbm.AddParameters(3, "@WORKORDERNO", plobj.sWorkOrderNo);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_FG_BOX_PACKING_BIND").Tables[0];
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
        public DataSet BindCustomerCode(string sFGItemCode,string sSiteCode)
        {
            DataSet dt = new DataSet();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "BINDCUSTOMERCODE");
                oDbm.AddParameters(1, "@FGITEMCODE", sFGItemCode);
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_FG_BOX_PACKING_BIND");
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
        public DataTable BindPurchaseOrder(PL_Printing plobj)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "GETPURCHASEORDER");
                oDbm.AddParameters(1, "@FGITEMCODE", plobj.sBOMCode);
                oDbm.AddParameters(2, "@SITECODE", plobj.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_FG_BOX_PACKING_BIND").Tables[0];
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
        public DataTable GetDetails(string sFGItemCode, string sCustomerCode,string sSiteCode,
            string sLineCode,string sUserID           
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(6);
                oDbm.AddParameters(0, "@TYPE", "GETDETAILS");
                oDbm.AddParameters(1, "@FGITEMCODE", sFGItemCode);
                oDbm.AddParameters(2, "@LINECODE", sLineCode);
                oDbm.AddParameters(3, "@SITECODE", sSiteCode);
                oDbm.AddParameters(4, "@SCANNEDBY", sUserID);
                oDbm.AddParameters(5, "@CUSTOMERCODE", sCustomerCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_FG_BOX_PACKING_BIND").Tables[0];
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
        public DataTable GetCustomerDetails(string sFGItemCode, string sCustomerCode, string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "GETCUSTOMERDETAILS");
                oDbm.AddParameters(1, "@FGITEMCODE", sFGItemCode);
                oDbm.AddParameters(2, "@CUSTOMERCODE", sCustomerCode);
                oDbm.AddParameters(3, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_FG_BOX_PACKING_BIND").Tables[0];
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

        public DataTable dlGetPickedImei(PL_Printing plobj)
        {
            DataTable dt = new DataTable();
            try
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.MethodBase.GetCurrentMethod().Name, 
                   "FG Item Code :" + plobj.sBOMCode + ", User ID : " + plobj.sUserID + ",Line code : " + plobj.sLineCode);
                oDbm.CreateParameters(7);
                oDbm.AddParameters(0, "@TYPE", "GETPICKEDIMEI");
                oDbm.AddParameters(1, "@FGITEMCODE", plobj.sBOMCode);
                oDbm.AddParameters(2, "@CUSTOMERCODE", plobj.sCustomerCode);
                oDbm.AddParameters(3, "@SCANNEDBY", plobj.sUserID);
                oDbm.AddParameters(4, "@SITECODE", plobj.sSiteCode);
                oDbm.AddParameters(5, "@SCANTYPE", plobj.sScanType);
                oDbm.AddParameters(6, "@LINECODE", plobj.sLineCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_FG_BOX_PACKING_BIND").Tables[0];
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
        public DataTable ScanBarcode(PL_Printing plobj)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(15);
                oDbm.AddParameters(0, "@TYPE", "SCANBARCODE");
                oDbm.AddParameters(1, "@LINECODE", plobj.sLineCode);
                oDbm.AddParameters(2, "@CUSTOMERCODE", plobj.sCustomerCode);
                oDbm.AddParameters(3, "@FGITEMCODE", plobj.sBOMCode);
                oDbm.AddParameters(4, "@BARCODE", plobj.sPCBBarcode);
                oDbm.AddParameters(5, "@SCANNEDBY", plobj.sUserID);
                oDbm.AddParameters(6, "@SITECODE", plobj.sSiteCode);
                oDbm.AddParameters(7, "@SCANTYPE", plobj.sScanType);
                oDbm.AddParameters(8, "@ORDERNO", plobj.sPO_Number);
                oDbm.AddParameters(9, "@MSNNO", plobj.sMSN);
                oDbm.AddParameters(10, "@WORKORDERNO", plobj.sWorkOrderNo);
                oDbm.AddParameters(11, "@SCANNINGALLOWED", plobj.sScanningAllowed);
                oDbm.AddParameters(12, "@TIMEEXPIRED", plobj.iScanningTime);
                oDbm.AddParameters(13, "@FIFOREQUIRED", plobj.sFIFORequied);
                oDbm.AddParameters(14, "@SAMPLINGPCB", plobj.sSamplingPCB);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_FG_BOX_PACKING").Tables[0];
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
        public DataTable GetPrn(PL_Printing plobj)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "GETPRN");
                oDbm.AddParameters(1, "@CUSTOMERCODE", plobj.sCustomerCode);
                oDbm.AddParameters(2, "@FGITEMCODE", plobj.sBOMCode);
                oDbm.AddParameters(3, "@SITECODE", plobj.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_FG_BOX_PACKING_BIND").Tables[0];
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
        public DataTable PrintBarcode(PL_Printing plobj)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(16);
                oDbm.AddParameters(0, "@TYPE", "PRINTBARCODE");
                oDbm.AddParameters(1, "@LINECODE", plobj.sLineCode);
                oDbm.AddParameters(2, "@FGITEMCODE", plobj.sBOMCode);
                oDbm.AddParameters(3, "@SCANNEDBY", plobj.sUserID);
                oDbm.AddParameters(4, "@sCustomerName", plobj.sCustomerName);
                oDbm.AddParameters(5, "@CustomerPartNumber", plobj.sCustomerPartNo);
                oDbm.AddParameters(6, "@SITECODE", plobj.sSiteCode);
                oDbm.AddParameters(7, "@CUSTOMERCODE", plobj.sCustomerCode);
                oDbm.AddParameters(8, "@SCANTYPE", plobj.sScanType);
                oDbm.AddParameters(9, "@ORDERNO", plobj.sPO_Number);
                oDbm.AddParameters(10, "@MSNNO", plobj.sMSN);
                oDbm.AddParameters(11, "@WORKORDERNO", plobj.sWorkOrderNo);
                oDbm.AddParameters(12, "@WORKORDERCOMPLETE", plobj.sWorkOrderComplete);
                oDbm.AddParameters(13, "@BATCHNO", plobj.sBatchNo);
                oDbm.AddParameters(14, "@dGrossWT", plobj.dBoxGrossWt);
                oDbm.AddParameters(15, "@dNetWT", plobj.dBoxNetWt);
                oDbm.Open();
                if (plobj.sScanType == "IMEI")
                {
                    dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_FG_BOX_PACKING_TMO").Tables[0];
                }
                else
                {
                    dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_FG_BOX_PACKING").Tables[0];
                }
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




        #region WORK Order Close
        public DataTable BindPendingFGItemCodeForWOClose(string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDFGITEMCODE");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_RM_WO_CLOSE").Tables[0];
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

        public DataTable BindPendingWorkOrderForWOClose(string sFGItemCode,string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "BINDWONO");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.AddParameters(2, "@FGITEMCODE",sFGItemCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_RM_WO_CLOSE").Tables[0];
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

        public DataTable WorkOrderClose(string sFGItemCode,string sWorkOrderNo
            ,string sRemarks,string sSiteCode,string sUserID
            )
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(6);
                oDbm.AddParameters(0, "@TYPE", "WORKORDERCLOSE");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.AddParameters(2, "@FGITEMCODE", sFGItemCode);
                oDbm.AddParameters(3, "@WONO", sWorkOrderNo);
                oDbm.AddParameters(4, "@CLOSED_BY", sUserID);
                oDbm.AddParameters(5, "@REMARKS", sRemarks);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_RM_WO_CLOSE").Tables[0];
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
