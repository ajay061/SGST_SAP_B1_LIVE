using Common;
using DataLayer;
using System;
using System.Data;

namespace BusinessLayer
{
    public class BL_LineMaster
    {
        DL_LineMaster dlobj = null;
        public DataTable GetLines()
        {
            DataTable dtLines = new DataTable();
            dlobj = new DL_LineMaster();
            try
            {
                dtLines = dlobj.GetLine();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLines;
        }

        public DataTable GetSeletedData(string _SN)
        {
            DataTable dt = new DataTable();
            dlobj = new DL_LineMaster();
            try
            {
                dt = dlobj.GetLineByID(_SN);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dt;
        }
        public string SaveLine(string _LineID, string _LineName, string _LineDesc, string _CreatedBy)
        {
            string sResult = string.Empty;
            dlobj = new DL_LineMaster();
            try
            {
                DataTable dt = dlobj.SaveLineID(_LineID, _LineName, _LineDesc, _CreatedBy, "SAVELINEDETAILS");
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

        public string DeleteLine(string sLineiD)
        {
            string sResult = string.Empty;
            dlobj = new DL_LineMaster();
            try
            {
                DataTable dt = dlobj.SaveLineID(sLineiD, "", "", "", "DELETELINE");
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
        public string UpdateLine(string _LineName, string _LineDescription, string sLineiD)
        {
            string sResult = string.Empty;
            dlobj = new DL_LineMaster();
            try
            {
                DataTable dt = dlobj.SaveLineID(sLineiD, _LineName, _LineDescription, "", "UPDATELINE");
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
    }
}
