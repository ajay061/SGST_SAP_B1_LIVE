using Common;
using DataLayer;
using System;
using System.Data;
using System.Text;

namespace BusinessLayer
{
    public class BL_SiteDetails
    {
        DL_SiteDetails dlobj = null;
        public DataTable GetSiteCode()
        {
            DataTable dtSiteCode = new DataTable();
            dlobj = new DL_SiteDetails();
            try
            {
                StringBuilder sb = new StringBuilder("Select SiteCode from [dbo].[mSITEMASTER]");
                dtSiteCode = dlobj.GetSiteCode(sb);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtSiteCode;
        }
    }
}
