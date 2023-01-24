using DataLayer.Masters;
using Common;
using System;
using System.Data;
namespace BusinessLayer.Masters
{
    public class BL_FG_CustomeMapping
    {
        DL_FG_Customer_Mapping dlobj = null;
        public DataTable BindFGitemCode()
        {
            DataTable dt = new DataTable();
            dlobj = new DL_FG_Customer_Mapping();
            try
            {
                dt = dlobj.BindFGItemCode();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dt;
        }
        public DataTable BindCustomerCode()
        {
            DataTable dt = new DataTable();
            dlobj = new DL_FG_Customer_Mapping();
            try
            {
                dt = dlobj.BindCustomeCode();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dt;
        }
        public DataTable GetData()
        {
            DataTable dt = new DataTable();
            dlobj = new DL_FG_Customer_Mapping();
            try
            {
                dt = dlobj.GetDetails();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dt;
        }

        public string SaveData(string sFGItemCode, string sCustomerCode, int FGID, int iPackingHours
            , string sApprovedStatus, int iNoOFSFGMapping,int iPackingSamplingSize
            , string sUserID, int iAgingTimePeriod,string ISFULLAGING
            )
        {
            string sResult = string.Empty;
            dlobj = new DL_FG_Customer_Mapping();
            try
            {
                DataTable dt = dlobj.SaveDeleteData(FGID, sFGItemCode, sCustomerCode, "SAVEDETAILS",
                    iPackingHours, sApprovedStatus, iNoOFSFGMapping, iPackingSamplingSize, sUserID, iAgingTimePeriod, ISFULLAGING);
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
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }
        public string DeleteData(string sFGItemCode, string sCustomerCode, int FGID, int iPackingHours            
            )
        {
            string sResult = string.Empty;
            dlobj = new DL_FG_Customer_Mapping();
            try
            {
                DataTable dt = dlobj.SaveDeleteData(FGID, sFGItemCode, sCustomerCode, "DELETEDETAILS",
                    iPackingHours, "0", 1,1, "",0,"");
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
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }

        public DataTable UploadFGCustomerMapping(DataTable dtUpload)
        {
            DataTable dt = new DataTable();
            dlobj = new DL_FG_Customer_Mapping();
            try
            {
                dt = dlobj.UploadData(dtUpload);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dt;
        }
    }
}
