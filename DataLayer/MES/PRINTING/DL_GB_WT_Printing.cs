using System;
using System.Data;
using System.Reflection;
using BcilLib;
using PL;
using Common;

namespace DataLayer.MES.PRINTING
{
    public class DL_GB_WT_Printing
    {
        DBManager oDbm;
        public DL_GB_WT_Printing()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable dlValidateWeight(PL_Printing plobj)
        {
            DataTable dt = new DataTable();
            try
            {               
                string _sResult;
                oDbm.CreateParameters(8);
                oDbm.AddParameters(0, "@PART_BARCODE", plobj.sBarcodestring.Trim());               
                oDbm.AddParameters(1, "@LINECODE", plobj.sLineCode.Trim());
                oDbm.AddParameters(2, "@SITECODE", plobj.sSiteCode.Trim());
                oDbm.AddParameters(3, "@SCANNEDBY", plobj.sUserID.Trim());
                oDbm.AddParameters(4, "@WEIGHT", plobj.dBoxWT);
                oDbm.AddParameters(5, "@MACHINEID", plobj.sStageCode);
                oDbm.AddParameters(6, "@FGITEMCODE", plobj.sBOMCode);
                oDbm.AddParameters(7, "@TYPE", "VALIDATE");
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_VALIDATE_WEIGHT").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    _sResult = Convert.ToString(dt.Rows[0][0]);
                    PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtData, MethodBase.GetCurrentMethod().Name, "Site Code = " + plobj.sSiteCode + "  and barcode = " + plobj.sBarcodestring.ToString() + "");
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtError, MethodBase.GetCurrentMethod().Name, " Site Code :  " + plobj.sSiteCode + " and barcode = " + plobj.sBarcodestring.ToString() + "" + ex.Message);
                throw ex;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dt;
        }

        public DataTable dlGetCaptureWeight(PL_Printing plobj)
        {
            DataTable dt = new DataTable();
            try
            {
                string _sResult;
                oDbm.CreateParameters(8);
                oDbm.AddParameters(0, "@PART_BARCODE", plobj.sBarcodestring.Trim());
                oDbm.AddParameters(1, "@LINECODE", plobj.sLineCode.Trim());
                oDbm.AddParameters(2, "@SITECODE", plobj.sSiteCode.Trim());
                oDbm.AddParameters(3, "@SCANNEDBY", plobj.sUserID.Trim());
                oDbm.AddParameters(4, "@WEIGHT", plobj.dBoxWT);
                oDbm.AddParameters(5, "@MACHINEID", plobj.sStageCode);
                oDbm.AddParameters(6, "@FGITEMCODE", plobj.sBOMCode);
                oDbm.AddParameters(7, "@TYPE", "GETCAPTUREWEIGHT");
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_VALIDATE_WEIGHT").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    _sResult = Convert.ToString(dt.Rows[0][0]);
                    PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtData, MethodBase.GetCurrentMethod().Name, "Site Code = " + plobj.sSiteCode + "  and barcode = " + plobj.sBarcodestring.ToString() + "");
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtError, MethodBase.GetCurrentMethod().Name, " Site Code :  " + plobj.sSiteCode + " and barcode = " + plobj.sBarcodestring.ToString() + "" + ex.Message);
                throw ex;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dt;
        }       

        public DataTable dlSaveWeight(PL_Printing plobj)
        {
            DataTable dt = new DataTable();
            try
            {
                string _sResult;
                oDbm.CreateParameters(8);
                oDbm.AddParameters(0, "@PART_BARCODE", plobj.sBarcodestring.Trim());
                oDbm.AddParameters(1, "@LINECODE", plobj.sLineCode.Trim());
                oDbm.AddParameters(2, "@SITECODE", plobj.sSiteCode.Trim());
                oDbm.AddParameters(3, "@SCANNEDBY", plobj.sUserID.Trim());
                oDbm.AddParameters(4, "@WEIGHT", plobj.dBoxWT);
                oDbm.AddParameters(5, "@MACHINEID", plobj.sStageCode);
                oDbm.AddParameters(6, "@FGITEMCODE", plobj.sBOMCode);
                oDbm.AddParameters(7, "@TYPE","SAVE");
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_VALIDATE_WEIGHT").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    _sResult = Convert.ToString(dt.Rows[0][0]);
                    PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtData, MethodBase.GetCurrentMethod().Name, "Site Code = " + plobj.sSiteCode + "  and barcode = " + plobj.sBarcodestring.ToString() + "");
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtError, MethodBase.GetCurrentMethod().Name, " Site Code :  " + plobj.sSiteCode + " and barcode = " + plobj.sBarcodestring.ToString() + "" + ex.Message);
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
