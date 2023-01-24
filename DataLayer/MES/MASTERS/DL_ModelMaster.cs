using Common;
using System;
using System.Data;
using PL;
using BcilLib;
using System.Reflection;

namespace DataLayer
{
    public class DL_ModelMaster
    {
        DBManager oDbm;
        public DL_ModelMaster()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }

        public DataTable dlBindFGItemCode(PL_ModelMaster plobj)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDFGITEMCODE");
                oDbm.AddParameters(1, "@SITECODE", PCommon.sSiteCode);
                dt = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_MODEL_MASTER").Tables[0];
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

        public object dlCheckDuplicateModelName(PL_ModelMaster plobj)
        {
            object oResult = null;
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "CHECKDUPLICATEMODELNAME");
                oDbm.AddParameters(1, "@MODELCODE", plobj.sModelName);
                oDbm.AddParameters(2, "@SITECODE", PCommon.sSiteCode);
                oResult = oDbm.ExecuteScalar(CommandType.StoredProcedure, "USP_MODEL_MASTER");
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
            return oResult;
        }    
        public int dlSaveModelMaster(PL_ModelMaster objPl)
        {
            int iResult = 0;
            try
            {
                oDbm.CreateParameters(27);
                oDbm.AddParameters(0, "@SITECODE", objPl.sSiteCode);
                oDbm.AddParameters(1, "@MODELCODE", objPl.sModelName);
                oDbm.AddParameters(2, "@MODELDESC", objPl.sModelType);
                oDbm.AddParameters(3, "@SWVERSION", objPl.sSWVER);
                oDbm.AddParameters(4, "@MRP", objPl.dMRP);
                oDbm.AddParameters(5, "@TYPE", "INSERT");
                oDbm.AddParameters(6, "@EAN", objPl.sEAN);
                oDbm.AddParameters(7, "@FGITEMCODE", objPl.sBOM);
                oDbm.AddParameters(8, "@CREATED_BY", objPl.sCreatedBy);
                oDbm.AddParameters(9, "@VENDORTYPE", objPl.sVendorSupplying);
                oDbm.AddParameters(10, "@WARENTYINDAYS", objPl.iWarruntyInDays);
                oDbm.AddParameters(11, "@GROSSWT", objPl.dGBWT);
                oDbm.AddParameters(12, "@TPLUS", objPl.dTOLERANCE_PLUS);
                oDbm.AddParameters(13, "@TMINUS", objPl.dTOLERANCE_MINUS);
                oDbm.AddParameters(14, "@CGROSSWT", objPl.dCBWT);
                oDbm.AddParameters(15, "@CTPLUS", objPl.dCTOLERANCE_PLUS);
                oDbm.AddParameters(16, "@CTMINUS", objPl.dCTOLERANCE_MINUS);
                oDbm.AddParameters(17, "@ECWT", objPl.dEmptyCartonWT);
                oDbm.AddParameters(18, "@WAVETOOLVALIDATE", objPl.bWaveToolValidate);
                oDbm.AddParameters(19, "@IsASNMac2Required", objPl.bASNShowsMac2);
                oDbm.AddParameters(20, "@MACADDRESSCOUNT", objPl.iNoOfMacAddress);
                oDbm.AddParameters(21, "@duplcateColumnData", objPl.dtDuplicateModelList);
                oDbm.AddParameters(22, "@REPORT_LOCATION", objPl.REPORT_LOCATION);
                oDbm.AddParameters(23, "@PIN_NO", objPl.PIN_NO);
                oDbm.AddParameters(24, "@REPORT_LOT_NO", objPl.REPORT_LOT_NO);
                oDbm.AddParameters(25, "@HWVERSION", objPl.HWVERSION);
                oDbm.AddParameters(26, "@ASN_MODEL_NO", objPl.ASN_MODEL_NO);
                oDbm.Open();
                DataTable dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_MODEL_MASTER").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtMessage, MethodBase.GetCurrentMethod().Name, " Model Name = " + objPl.sModelName + "  and output = " + dt.Rows[0][0].ToString() + "");
                    if (dt.Rows[0][0].ToString().StartsWith("1"))
                    {
                        iResult = 1;
                    }
                    else if (dt.Rows[0][0].ToString().StartsWith("ERROR"))
                    {
                        PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtMessage, MethodBase.GetCurrentMethod().Name, " Model Name = " + objPl.sModelName + " and Error = " + dt.Rows[0][0].ToString() + "");
                        iResult = 0;
                    }
                }
                if (Convert.ToInt32(iResult) > 0)
                {
                    PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtMessage, MethodBase.GetCurrentMethod().Name, "Data saved succesfully Model Name = " + objPl.sModelName );
                }
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
            return iResult;
        }      
        public int dlUpdateModelMaster(PL_ModelMaster objPl)
        {
            int iResult = 0;
            try
            {
                oDbm.CreateParameters(27);
                oDbm.AddParameters(0, "@SITECODE", objPl.sSiteCode);
                oDbm.AddParameters(1, "@MODELCODE", objPl.sModelName);
                oDbm.AddParameters(2, "@MODELDESC", objPl.sModelType);
                oDbm.AddParameters(3, "@SWVERSION", objPl.sSWVER);
                oDbm.AddParameters(4, "@MRP", objPl.dMRP);
                oDbm.AddParameters(5, "@TYPE", "UPDATE");
                oDbm.AddParameters(6, "@EAN", objPl.sEAN);
                oDbm.AddParameters(7, "@FGITEMCODE", objPl.sBOM);
                oDbm.AddParameters(8, "@CREATED_BY", objPl.sCreatedBy);
                oDbm.AddParameters(9, "@VENDORTYPE", objPl.sVendorSupplying);
                oDbm.AddParameters(10, "@WARENTYINDAYS", objPl.iWarruntyInDays);
                oDbm.AddParameters(11, "@GROSSWT", objPl.dGBWT);
                oDbm.AddParameters(12, "@TPLUS", objPl.dTOLERANCE_PLUS);
                oDbm.AddParameters(13, "@TMINUS", objPl.dTOLERANCE_MINUS);
                oDbm.AddParameters(14, "@CGROSSWT", objPl.dCBWT);
                oDbm.AddParameters(15, "@CTPLUS", objPl.dCTOLERANCE_PLUS);
                oDbm.AddParameters(16, "@CTMINUS", objPl.dCTOLERANCE_MINUS);
                oDbm.AddParameters(17, "@ECWT", objPl.dEmptyCartonWT);
                oDbm.AddParameters(18, "@WAVETOOLVALIDATE", objPl.bWaveToolValidate);
                oDbm.AddParameters(19, "@IsASNMac2Required", objPl.bASNShowsMac2);
                oDbm.AddParameters(20, "@MACADDRESSCOUNT", objPl.iNoOfMacAddress);
                oDbm.AddParameters(21, "@duplcateColumnData", objPl.dtDuplicateModelList);
                oDbm.AddParameters(22, "@REPORT_LOCATION", objPl.REPORT_LOCATION);
                oDbm.AddParameters(23, "@PIN_NO", objPl.PIN_NO);
                oDbm.AddParameters(24, "@REPORT_LOT_NO", objPl.REPORT_LOT_NO);
                oDbm.AddParameters(25, "@HWVERSION", objPl.HWVERSION);
                oDbm.AddParameters(26, "@ASN_MODEL_NO", objPl.ASN_MODEL_NO);
                oDbm.Open();
                DataTable dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_MODEL_MASTER").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtMessage, MethodBase.GetCurrentMethod().Name, " Model Name = " + objPl.sModelName + "  and output = " + dt.Rows[0][0].ToString() + "");
                    if (dt.Rows[0][0].ToString().StartsWith("1"))
                    {
                        iResult = 1;
                    }
                    else if (dt.Rows[0][0].ToString().StartsWith("ERROR"))
                    {
                        PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtMessage, MethodBase.GetCurrentMethod().Name, " Model Name = " + objPl.sModelName + " and Error = " + dt.Rows[0][0].ToString() + "");
                        iResult = 0;
                    }
                }
                if (Convert.ToInt32(iResult) > 0)
                {
                    PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtMessage, MethodBase.GetCurrentMethod().Name, " Data updated successfully for Model Name = " + objPl.sModelName);
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtError, MethodBase.GetCurrentMethod().Name,  ex.Message);
                throw ex;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return iResult;
        }
        public DataTable dlDeleteUnusedModel(PL_ModelMaster objPl)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@SITECODE", PCommon.sSiteCode);
                oDbm.AddParameters(1, "@MODELCODE", objPl.sModelName);
                oDbm.AddParameters(2, "@TYPE", "DELETEMODEL");              
                dt = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_MODEL_MASTER").Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dt;
        }        
        public DataSet dlRetrieveModelDetails(PL_ModelMaster plobj)
        {
            DataSet ds = new DataSet();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@SITECODE", PCommon.sSiteCode);
                oDbm.AddParameters(1, "@MODELCODE", plobj.sModelName);
                oDbm.AddParameters(2, "@TYPE", "GETALL");
                ds = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_MODEL_MASTER");
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
            return ds;
        }   
    }
}
