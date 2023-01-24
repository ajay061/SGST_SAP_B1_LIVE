using Common;
using System;
using System.Data;
using PL;
using BcilLib;
using System.Reflection;

namespace DataLayer.MES.QUALITY
{
    public class mobQuality
    {
        DBManager oDbm;
        public mobQuality()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable ValidateMachine(PL_Printing objPl)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(5);
                oDbm.AddParameters(0, "@TYPE", "VALIDATEMACHINE");
                oDbm.AddParameters(1, "@MACHINEID", objPl.sStageCode);
                oDbm.AddParameters(2, "@LINECODE", objPl.sLineCode);
                oDbm.AddParameters(3, "@FGITEMCODE", objPl.sBOMCode);
                oDbm.AddParameters(4, "@SITECODE", objPl.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_MOB_QUALITY").Tables[0];
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
        public DataTable BindDefect(PL_Printing objPl)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(5);
                oDbm.AddParameters(0, "@TYPE", "BINDDEFECT");
                oDbm.AddParameters(1, "@MACHINEID", objPl.sStageCode);
                oDbm.AddParameters(2, "@LINECODE", objPl.sLineCode);
                oDbm.AddParameters(3, "@FGITEMCODE", objPl.sBOMCode);
                oDbm.AddParameters(4, "@SITECODE", objPl.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_MOB_QUALITY").Tables[0];
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
        public object dlInsertGetBarcode(PL_Printing objPl)
        {
            object _s2DBarcode = string.Empty;
            try
            {
                DataTable dt = new DataTable();
                oDbm.CreateParameters(10);
                oDbm.AddParameters(0, "@SITECODE", objPl.sSiteCode);
                oDbm.AddParameters(1, "@MODELCODE", objPl.sModelName);               
                oDbm.AddParameters(2, "@LINECODE", objPl.sLineCode);
                oDbm.AddParameters(3, "@BARCODE", objPl.sSNBarcode);
                oDbm.AddParameters(4, "@PRINTEDBY", objPl.sUserID);
                oDbm.AddParameters(5, "@FGITEMCODE", objPl.sBOMCode);
                oDbm.AddParameters(6, "@MACHINEID", objPl.sStageCode);
                oDbm.AddParameters(7, "@DEFECT", objPl.sDefect);
                oDbm.AddParameters(8, "@TYPE", "SAVE");
                oDbm.AddParameters(9, "@REWORKSTATIONID", objPl.sReworkStation);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_MOB_QUALITY").Tables[0];
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
