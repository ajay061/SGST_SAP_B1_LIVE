using Common;
using System;
using System.Data;
using DataLayer.WIP;

namespace BusinessLayer.WIP
{
    public class BL_WipQualityNew
    {
        DL_WIP_QUALITY dlobj = new DL_WIP_QUALITY();
        public DataTable BindDefect()
        {
            DataTable dtFGItemCode = new DataTable();
            try
            {
                dlobj = new DL_WIP_QUALITY();
                dtFGItemCode = dlobj.BindDefect();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtFGItemCode;
        }
        public DataTable BindFGItemCode()
        {
            DataTable dtFGItemCode = new DataTable();
            try
            {
                dlobj = new DL_WIP_QUALITY();
                dtFGItemCode = dlobj.BindFGItemCode();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtFGItemCode;
        }
        public DataTable BindFGQualityParameterList(string sFGItemCode)
        {
            DataTable dtFGItemCode = new DataTable();
            try
            {
                dlobj = new DL_WIP_QUALITY();
                dtFGItemCode = dlobj.BindFGQualityParameterList(sFGItemCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtFGItemCode;
        }
        public string ValidateSecondaryBarcode(string _sBarcode, string sFgItemCode)
        {
            string sResult = string.Empty;
            DataTable dtFGItemCode = new DataTable();
            try
            {
                dlobj = new DL_WIP_QUALITY();
                dtFGItemCode = dlobj.ValidateBox(sFgItemCode, _sBarcode);
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

        public string CheckPCBBarcode(string _sBarcode, string _sFgItemCode, string _sPrimary)
        {
            string sResult = string.Empty;
            DataTable dtFGItemCode = new DataTable();
            try
            {
                dlobj = new DL_WIP_QUALITY();
                dtFGItemCode = dlobj.ValidatePCBBarcode(_sFgItemCode, _sBarcode);
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
        public DataTable CheckAccessoriesBarcode(string _sBarcode, string _sFgItemCode)
        {
            string sResult = string.Empty;
            DataTable dtFGItemCode = new DataTable();
            try
            {
                dlobj = new DL_WIP_QUALITY();
                dtFGItemCode = dlobj.ValdateAccessoriesBarcode(_sFgItemCode, _sBarcode);                
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtFGItemCode;
        }
        public DataTable SaveQualityData(DataTable dt, string status, string modulename,
            string sCANBY, string fgitemcode, string sSiteCode, DataTable dtSAPPostingData
            , string sLineCode, string sFinalRemarks,DataTable dtAccessoriesDetails
            )
        {
            DataTable dtFGItemCode = new DataTable();
            try
            {
                dlobj = new DL_WIP_QUALITY();
                dtFGItemCode = dlobj.dtQualityUpdate(dt, fgitemcode, modulename, sCANBY,
                    sSiteCode, status, dtSAPPostingData, sLineCode, sFinalRemarks, dtAccessoriesDetails);
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
