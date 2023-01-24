using Common;
using DataLayer.WIP;
using System;
using System.Data;
using System.Text;

namespace BusinessLayer.WIP
{
    public class BL_QualityRework
    {
        StringBuilder sb = null;
        DL_WIP_QUALITY_REWORK dlboj = null;

        public DataTable BindDefectMaster(out string result, string sType)
        {
            DataTable dtLineData = new DataTable();
            try
            {
                dlboj = new DL_WIP_QUALITY_REWORK();
                dtLineData = dlboj.BindDefect();
                if (dtLineData.Rows.Count > 0)
                {
                    result = "SUCCESS~";
                }
                else
                {
                    result = "N~No details found";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message + ", Query " + sb.ToString());
                throw ex;
            }
            return dtLineData;
        }

        public string QualityRework(string sType, string sPCBBarcode, string sDefect, string sObservation, string sRemarks,
            string sPcbMove, string sScannedBy,string sSiteCode,string sLineCode)
        {
            dlboj = new DL_WIP_QUALITY_REWORK();
            DataTable dtResult = new DataTable();
            string sResult = string.Empty;
            try
            {
                dtResult = dlboj.dlQualityRework(sType, sPCBBarcode, sDefect, sObservation, sRemarks
                            , sPcbMove, sScannedBy, sSiteCode,sLineCode
                            );
                if (dtResult.Rows.Count > 0)
                {
                    if (dtResult.Rows[0][0].ToString().StartsWith("ERROR~"))
                    {
                        sResult = dtResult.Rows[0][0].ToString();
                    }
                    else
                    {
                        sResult = dtResult.Rows[0][0].ToString();
                    }
                }
                else
                {
                    sResult = "N~No result found, Please try again";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }
    }
}
