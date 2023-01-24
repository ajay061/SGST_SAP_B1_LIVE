using System;
using System.Data;
using PL;
using BcilLib;
using System.Reflection;
using Common;

namespace DataLayer.MES.PRINTING
{
    public class DL_LabelPrinting
    {
        DBManager oDbm;
        public DL_LabelPrinting()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }

        public DataTable GetPrn(PL_Printing plobj)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(5);
                oDbm.AddParameters(0, "@TYPE", "GETPRN");
                oDbm.AddParameters(1, "@CUSTOMERCODE", plobj.sCustomerCode);
                oDbm.AddParameters(2, "@FGITEMCODE", plobj.sBOMCode);
                oDbm.AddParameters(3, "@SITECODE", plobj.sSiteCode);
                oDbm.AddParameters(4, "@STAGECODE", plobj.sStageCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_BIND_PRINTINGDATA").Tables[0];
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
        public int dlCountModelIMEIDetails(PL_Printing plobj)
        {
            int iResult = 0;
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "COUNTMODELIMEI");
                oDbm.AddParameters(1, "@MODELCODE", plobj.sModelName);
                oDbm.AddParameters(2, "@SITECODE", plobj.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_BIND_PRINTINGDATA").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    iResult = Convert.ToInt32(dt.Rows[0][0].ToString());
                }
                else
                {
                    iResult = 0;
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
            return iResult;
        }
        public object dlInsertGetBarcode(PL_Printing objPl)
        {
            object _s2DBarcode = string.Empty;
            try
            {
                DataTable dt = new DataTable();
                oDbm.CreateParameters(7);
                oDbm.AddParameters(0, "@SITECODE", PCommon.sSiteCode);
                oDbm.AddParameters(1, "@MODELCODE", objPl.sModelName);
                oDbm.AddParameters(2, "@LINECODE", objPl.sLineCode);
                oDbm.AddParameters(3, "@BARCODE", objPl.sSNBarcode);
                oDbm.AddParameters(4, "@PRINTEDBY", objPl.sUserID);
                oDbm.AddParameters(5, "@FGITEMCODE", objPl.sBOMCode);
                oDbm.AddParameters(6, "@STAGECODE", objPl.sStageCode);
                string sprocesName = string.Empty;
                sprocesName = "USP_MOB_VALIDATELABEL_PRINTING";
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, sprocesName).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    _s2DBarcode = Convert.ToString(dt.Rows[0][0]);
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtError, MethodBase.GetCurrentMethod().Name, " Barcode value come from database :  = " + Convert.ToString(_s2DBarcode) + " Error :  " + ex.Message);
                throw ex;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return _s2DBarcode;
        }
    }
}
