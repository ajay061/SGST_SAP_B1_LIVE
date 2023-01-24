using System;
using System.Text;
using DataLayer;
using System.Data;
using Common;

namespace BusinessLayer
{
    public class BL_FGQuality
    {
        DL_FGQuality dlobj = new DL_FGQuality();
        public DataTable BindDefect(string sSiteCode)
        {
            DataTable dtPartCode = new DataTable();
            dlobj = new DL_FGQuality();
            try
            {
                dtPartCode = dlobj.BindDefect(sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtPartCode;
        }

        public DataTable BindFGItemCode(string sSiteCode)
        {
            DataTable dtFGItemCode = new DataTable();
            try
            {
                dlobj = new DL_FGQuality();
                dtFGItemCode = dlobj.BindFGItemCode(sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtFGItemCode;
        }

        public string ValidateSecondaryBarcode(string _sBarcode, string sFgItemCode
            ,string sSiteCode
            )
        {
            string sResult = string.Empty;
            DataTable dtFGItemCode = new DataTable();
            try
            {
                dlobj = new DL_FGQuality();
                dtFGItemCode = dlobj.ValidateSecondaryBox(sFgItemCode, _sBarcode
                    , sSiteCode
                    );
                if (dtFGItemCode.Rows.Count > 0)
                {
                    sResult = dtFGItemCode.Rows[0][0].ToString();
                }
                else
                {
                    sResult = "N~No result found for scanned barcode";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }

        public string CheckPCBBarcode(string _sBarcode, string _sFgItemCode
            ,string sSiteCode
            )
        {
            string sResult = string.Empty;
            DataTable dtFGItemCode = new DataTable();
            try
            {
                dlobj = new DL_FGQuality();
                dtFGItemCode = dlobj.ValidatePCBBarcode(_sFgItemCode, _sBarcode, sSiteCode);
                if (dtFGItemCode.Rows.Count > 0)
                {
                    sResult = dtFGItemCode.Rows[0][0].ToString();
                }
                else
                {
                    sResult = "N~No result found for scanned barcode";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }

        public DataTable SaveQualityData(DataTable dt, string status, string modulename,
            string sCANBY, string fgitemcode, string sSiteCode, string sLocation,
            string sCode, string stype,string sLineCode)
        {
            DataTable dtFGItemCode = new DataTable();
            try
            {
                dlobj = new DL_FGQuality();
                dtFGItemCode = dlobj.dtDOCQualityUpdate(dt, fgitemcode, modulename, sCANBY, sSiteCode, status, sLocation, sCode, stype
                    , sLineCode
                    );
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtFGItemCode;
        }
    }
}
