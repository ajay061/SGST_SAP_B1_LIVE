using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DL_SiteDetails
    {
        DBManager oDbm;
        public DL_SiteDetails()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }
        public DataTable GetSiteCode(StringBuilder sb)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.Text, sb.ToString()).Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                oDbm.Close();
            }
            return dt;
        }
    }
}
