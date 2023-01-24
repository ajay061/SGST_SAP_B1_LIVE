using Common;
using DataLayer.WIP;
using PL;
using System;
using System.Data;

namespace BusinessLayer.WIP
{
    public class BL_WIP_AllowedExpiredPCB
    {
        DL_WIPExpiredPCB dlobj;
        public DataTable BindFgItemCode(string sSiteCode)
        {
            dlobj = new DL_WIPExpiredPCB();
            return dlobj.BindFGitemCode(sSiteCode);
        }
        public DataTable GetDetails(string sSiteCode, string sWorkOrderNo, string sFGItemCode)
        {
            dlobj = new DL_WIPExpiredPCB();
            return dlobj.GetData(sSiteCode, sWorkOrderNo, sFGItemCode);
        }
        public DataTable UpdateStatus(string sAllowedBy, DataTable dtPCBData, string sSiteCode)
        {
            dlobj = new DL_WIPExpiredPCB();
            return dlobj.AllowedPCB(sAllowedBy, dtPCBData, sSiteCode);
        }
    }
}
