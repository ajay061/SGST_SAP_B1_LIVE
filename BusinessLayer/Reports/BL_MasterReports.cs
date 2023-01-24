using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Masters;
using System.Data;
using DataLayer.Reports;
using Common;

namespace BusinessLayer.Reports
{
    public class BL_MasterReports
    {
        public DataTable GetReports(string sSiteCode, string sType,string sFilter)
        {
            DataTable dtGRN = new DataTable();
            DL_MasterReport dlobj = new DL_MasterReport();
            try
            {
                dtGRN = dlobj.GetReport(sSiteCode,sType, sFilter);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtGRN;
        }

    }
}
