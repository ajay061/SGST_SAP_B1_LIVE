using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Reports;
using System.Data;
using Common;

namespace BusinessLayer.Reports
{
    public class BL_FG_PackingListReport
    {
        DL_FG_PackingListReport dlboj = null;
        public DataTable BINDSALESORDERFORREPORT()
        {
            DataTable dtGRN = new DataTable();        
            dlboj = new DL_FG_PackingListReport();
            try
            {
                dtGRN = dlboj.BindSalesOrder();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtGRN;
        }
        public DataTable BindPickList(string sSalesOrderNO)
        {
            DataTable dtGRN = new DataTable();
            dlboj = new DL_FG_PackingListReport();
            try
            {
                dtGRN = dlboj.BindPickListNo(sSalesOrderNO);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtGRN;
        }
        public DataTable BindPackingList(string sPickListNo)
        {
            DataTable dtGRN = new DataTable();
            dlboj = new DL_FG_PackingListReport();
            try
            {
                dtGRN = dlboj.BindPackinglistNo(sPickListNo);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtGRN;
        }
        public DataTable GetReport(string sSalesOrderNo,string sPicklist,string sPackingList)
        {
            DataTable dtGRN = new DataTable();
            dlboj = new DL_FG_PackingListReport();
            try
            {
                dtGRN = dlboj.GetReport(sSalesOrderNo, sPicklist, sPackingList);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtGRN;
        }
    }
}
