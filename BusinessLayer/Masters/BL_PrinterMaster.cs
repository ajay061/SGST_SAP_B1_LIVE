using Common;
using DataLayer;
using System;
using System.Data;
using System.Text;

namespace BusinessLayer
{

    public class BL_PrinterMaster
    {
        DL_PrinterMaster dlobj = null;              
        public string SavePrinter(string printerip, string type)
        {
            string sResult = string.Empty;
            dlobj = new DL_PrinterMaster();
            try
            {
                DataTable dt = dlobj.SavePrinter(printerip, type);
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
                }
                else
                {
                    sResult = "N~No Result found";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }   

        public DataTable Getprinters()
        {
            DataTable dtUsers = new DataTable();
            dlobj = new DL_PrinterMaster();
            try
            {
                dtUsers = dlobj.GetPrinters();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtUsers;
        }        
        public string Deleteprinters(string sN)
        {
            string sResult = string.Empty;
            dlobj = new DL_PrinterMaster();
            try
            {
                DataTable dt = dlobj.Deleteprinters(sN);
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
                }
                else
                {
                    sResult = "N~No Result found";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }
    }
}
