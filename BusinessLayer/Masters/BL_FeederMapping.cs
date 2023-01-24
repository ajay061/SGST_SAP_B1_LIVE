using DataLayer.Masters;
using Common;
using System;
using System.Data;


namespace BusinessLayer.Masters
{
    public class BL_FeederMapping
    {
        DL_Feeder_Mapping dlobj = null;
        public DataTable BindFeederData()
        {
            DataTable dt = new DataTable();
            dlobj = new DL_Feeder_Mapping();
            try
            {
                dt = dlobj.GetFeederDetails();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dt;
        }

        public string SaveFeederMappingData(string sFeederNo, string sFeederID, int IFeederID)
        {
            string sResult = string.Empty;
            dlobj = new DL_Feeder_Mapping();
            try
            {
                DataTable dt = dlobj.SaveFeeder(IFeederID, sFeederID, sFeederNo, "SAVEFEEDERDATA");
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
    }
}
