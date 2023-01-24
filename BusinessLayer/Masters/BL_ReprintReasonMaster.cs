using Common;
using DataLayer.Masters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Masters
{
    public class BL_ReprintReasonMaster
    {
        DL_ReasonReprintMaster dlboj = null;
        public string SaveLocationData(string _sReason_of_Reprint, string _sCreBy)
        {
            DataTable dt = new DataTable();
            dlboj = new DL_ReasonReprintMaster();
            string sResult = string.Empty;
            try
            {
                dt = dlboj.SaveReasonReprint(_sReason_of_Reprint, _sCreBy
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
        public string UpdateReasonReprint(string _sReason_of_Reprint, string _sCreBy, int Reprint_ID)
        {
            DataTable dt = new DataTable();
            dlboj = new DL_ReasonReprintMaster();
            string sResult = string.Empty;
            try
            {
                dt = dlboj.UpdateReasonReprint(_sReason_of_Reprint, _sCreBy, Reprint_ID
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
        public DataTable BindReasonReprint()
        {
            DL_ReasonReprintMaster dlobj = new DL_ReasonReprintMaster();
            return dlobj.BindReasonReprint();
        }
        public DataTable DeleteReasonReprint(string Reprint_ID)
        {
            DL_ReasonReprintMaster dlobj = new DL_ReasonReprintMaster();
            return dlobj.DeleteReasonReprint(Reprint_ID);
        }
        public DataTable SearchReasonReprint(string Reprint_ID)
        {
            DL_ReasonReprintMaster dlobj = new DL_ReasonReprintMaster();
            return dlobj.SearchReasonReprint(Reprint_ID);
        }
        public string SaveReprintData(string sType, string sPrinterIP, string sBarcode, string _sCreBy, string ddlReasonofReprint,string txtRemarks,string _sPrintingType)
        {
            DataTable dt = new DataTable();
            dlboj = new DL_ReasonReprintMaster();
            string sResult = string.Empty;
            try
            {
                dt = dlboj.SaveReprintData(sType, sPrinterIP, sBarcode, _sCreBy, ddlReasonofReprint, txtRemarks, _sPrintingType
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
    }
}
