using Common;
using DataLayer.Masters;
using System;
using System.Data;

namespace BusinessLayer.Masters
{
    public class BL_LocationMaster
    {
        DL_LocationMaster dlboj = null;
        public DataTable GetPartCode(string sArea,string sSiteCode )
        {
            DataTable dtPartCode = new DataTable();
            try
            {
                dlboj = new DL_LocationMaster();
                dtPartCode = dlboj.BindPartCode(sArea, sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtPartCode;
        }
        public DataTable GetLocationRecord(string sArea, string sSiteCode)
        {
            dlboj = new DL_LocationMaster();
            DataTable dtLocation = new DataTable();
            try
            {
                dtLocation = dlboj.GetLocationDetails(sArea, sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLocation;
        }
        public DataTable GetSeletedData(string sLocationCode, string sLocationArea, string sSiteCode)
        {
            DataTable dt = new DataTable();
            dlboj = new DL_LocationMaster();
            try
            {
                dt = dlboj.FillLocationDetails(sLocationCode, sLocationArea,sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dt;
        }

        public string SaveLocationData(string _sLocationCode, string _sLocationArea, string _slocationtype,
            string _sDescription, string _sCreBy, string _PartCode, string sWHLoc, string sSiteCode)
        {
            DataTable dt = new DataTable();
            dlboj = new DL_LocationMaster();
            string sResult = string.Empty;
            try
            {
                dt = dlboj.SaveLocationDetails(_sLocationCode, _sLocationArea
                    , _slocationtype, _sDescription, _sCreBy, _PartCode, sWHLoc,sSiteCode
                    );
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
                }
                else
                {
                    sResult = "N~No result found";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }

        public string UpdateLocation(string _Uid, string _sLocationCode, string _sLocationArea, string _slocationtype,
            string _sDescription, string _sCreBy, string _PartCode, string sWHLoc, string sSiteCode)
        {
            DataTable dt = new DataTable();
            dlboj = new DL_LocationMaster();
            string sResult = string.Empty;
            try
            {
                dt = dlboj.UpdateLocation(_sLocationCode, _sLocationArea
                    , _slocationtype, _sDescription, _sCreBy, _PartCode, _Uid, sWHLoc,sSiteCode
                    );
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
                }
                else
                {
                    sResult = "N~No result found";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }
        public string DeleteLocation(string sLocationCode, string sLocationArea, string sSiteCode)
        {
            DataTable dt = new DataTable();
            dlboj = new DL_LocationMaster();
            string sResult = string.Empty;
            try
            {
                dt = dlboj.Deletelocation(sLocationCode, sLocationArea,sSiteCode);
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
                }
                else
                {
                    sResult = "N~No result found";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }
        public string UploadLocation(DataTable dtLocation,string sUserID)
        {
            DataTable dt = new DataTable();
            dlboj = new DL_LocationMaster();
            string sResult = string.Empty;
            try
            {
                dt = dlboj.UploadLocationData(dtLocation, sUserID);
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
                }
                else
                {
                    sResult = "N~No result found";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }

        public string LocationPrinting(string LOCATIONCODE,
            string LOCDESCRIPTION, string sPrinterIP, string sPartCode
            , string sUserID, string sLineCode
            )
        {
            string sPrintingResult = string.Empty;
            dlboj = new DL_LocationMaster();
            BL_Common objBL_Common = new BL_Common();
            try
            {
                string chkPrinterStatus = string.Empty;
                chkPrinterStatus = objBL_Common.validPrinterConnection(sPrinterIP);
                string sPRN = string.Empty;
                if (chkPrinterStatus == "PRINTER READY")
                {
                    DataTable dt = objBL_Common.GetPRN("LOCATION");
                    if (dt.Rows.Count > 0)
                    {
                        sPRN = dt.Rows[0][0].ToString();
                        if (sPartCode.Length > 0)
                        {
                            if (sPartCode == "--Select--")
                            {
                                sPartCode = "";
                            }
                        }
                        string sName = string.Empty;
                        sName = "Name";
                        string sPRNPrintingResult = objBL_Common.ALLPrinting(sPRN, LOCATIONCODE, LOCDESCRIPTION, sPartCode, sName, "Location Code");
                        if (sPRNPrintingResult.Trim().Length == 0)
                        {
                            sPrintingResult = "N~Printer not found, Location printing failed.";
                        }
                        else
                        {
                            sPrintingResult = objBL_Common.sPrintDataLabel(sPrinterIP, sPRNPrintingResult, LOCATIONCODE, "Location"
                                , sUserID, sLineCode
                                );
                        }
                    }
                    else
                    {
                        sPrintingResult = "PRNNOTFOUND~Prn for Location printing is not available.";
                    }
                }
                else
                {
                    sPrintingResult = chkPrinterStatus;
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sPrintingResult;
        }
    }
}
