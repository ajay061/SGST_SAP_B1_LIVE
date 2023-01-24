using System;
using System.Data;
using PL;
using System.Reflection;
using BcilLib;
using Common;

namespace DataLayer
{
    public class DL_IMEIUpload
    {
        DBManager oDbm;
        public DL_IMEIUpload()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }

        public DataTable BindModel()
        {
            DataTable dtobj = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDMODEL");
                oDbm.AddParameters(1, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dtobj = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_MODELIMEI_UPLOAD").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtError, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dtobj;
        }    
        public DataSet GetModelDetails(PL_IMEIMaster plobj)
        {
            DataSet dtobj = new DataSet();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "GETMODELDETAILS");
                oDbm.AddParameters(1, "@MODELCODE", plobj.sModelName);
                oDbm.AddParameters(2, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dtobj = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_MODELIMEI_UPLOAD");
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtError, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dtobj;
        }
        public DataTable dtCheckDuplicate(PL_IMEIMaster plobj)
        {
            DataTable dtobj = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@DETAIL", plobj.dtIMEIUpload);
                oDbm.AddParameters(1, "@TYPE", "CHECKDUPLICATE");
                oDbm.AddParameters(2, "@MODELCODE", plobj.sModelName);
                oDbm.Open();
                dtobj = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_MODELIMEI_UPLOAD").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtError, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dtobj;
        }

        public string dlUploadIMEIDetalsByDataTable(PL_IMEIMaster plobj)
        {
            DataTable dtobj = new DataTable();
            string _sResult = string.Empty;
            try
            {
                oDbm.CreateParameters(7);               
                oDbm.AddParameters(0, "@DETAIL", plobj.dtIMEIUpload);            
                oDbm.AddParameters(1, "@MODELCODE", plobj.sModelName);
                oDbm.AddParameters(2, "@SITECODE", PCommon.sSiteCode);
                oDbm.AddParameters(3, "@UPLOADEDBY", plobj.sUploadedby);
                oDbm.AddParameters(4, "@LINECODE", plobj.sLineCode);
                oDbm.AddParameters(5, "@TYPE", "SAVE");
                oDbm.AddParameters(6, "@HEXAMACDETAILS", plobj.dtHexaMac);
                oDbm.Open();
                dtobj = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_MODELIMEI_UPLOAD").Tables[0];
                if (dtobj.Rows[0].ItemArray[0].ToString().Split('~')[0].ToUpper().Trim() == "OKAY")
                {
                    PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtInfo, MethodBase.GetCurrentMethod().Name, " IMEI Uploaded of Model Name = " + plobj.sModelName + " and Model Type = " + plobj.sModelType + " ");
                }
                else { }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtError, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dtobj.Rows[0].ItemArray[0].ToString();
        }



        public DataTable dtCheckDuplicate_boxaFile(PL_IMEIMaster plobj)
        {
            DataTable dtobj = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@DETAIL", plobj.dtIMEIUpload);
                oDbm.AddParameters(1, "@TYPE", "CHECKDUPLICATE");
                oDbm.Open();
                dtobj = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "[USP_BosaData_UPLOAD]").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtError, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dtobj;
        }

        public string dlUploadboxaDetalsByDataTable(PL_IMEIMaster plobj)
        {
            DataTable dtobj = new DataTable();
            string _sResult = string.Empty;
            try
            {
                oDbm.CreateParameters(11);
                oDbm.AddParameters(0, "@DETAIL", plobj.dtIMEIUpload);
                oDbm.AddParameters(1, "@SITECODE", PCommon.sSiteCode);
                oDbm.AddParameters(2, "@UPLOADEDBY", plobj.sUploadedby);                
                oDbm.AddParameters(3, "@TYPE", "SAVE");
                oDbm.AddParameters(4, "@CUSTOMER_PO", plobj.sCustomer);
                oDbm.AddParameters(5, "@TESTER", plobj.sTester);
                oDbm.AddParameters(6, "@TEST_DATE", plobj.dTestingDate);
                oDbm.AddParameters(7, "@PRODUCT_NAME", plobj.sProduct);
                oDbm.AddParameters(8, "@TEST_TEMPRATURE", plobj.sTestTemp);
                oDbm.AddParameters(9, "@TEST_CONDITION", plobj.sTestCondition);
                oDbm.AddParameters(10, "@MPN", plobj.sMPN);

                oDbm.Open();
                dtobj = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "[USP_BosaData_UPLOAD]").Tables[0];
                if (dtobj.Rows[0].ItemArray[0].ToString().Split('~')[0].ToUpper().Trim() == "OKAY")
                {
                    PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtInfo, MethodBase.GetCurrentMethod().Name, " BoxaSN Uploaded of Model Name = " + plobj.sModelName + " and Model Type = " + plobj.sModelType + " ");
                }
                else { }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtError, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dtobj.Rows[0].ItemArray[0].ToString();
        }
    }
}
