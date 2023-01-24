using System;
using System.Data;
using Common;
using DataLayer.Reports;

namespace BusinessLayer.Reports
{
    public class BL_ASNReport
    {
        DL_ASNReport dlobj;
        public DataTable GetReport(string sPONO)
        {
            DataTable dtGRN = new DataTable();
            dlobj = new DL_ASNReport();
            try
            {
                dtGRN = dlobj.GetReport(sPONO);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtGRN;
        }

        public DataTable BindAccessoriesFGitemCode()
        {
            DataTable dtGRN = new DataTable();
            dlobj = new DL_ASNReport();
            try
            {
                dtGRN = dlobj.BindAccessoriesFGItemCode();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtGRN;
        }

        public DataTable GetReportAccessoriesScanning(string sFromDate, string sTODate, string sFGItemCode
            ,string sWorkOrderNo
            )
        {
            DataTable dtGRN = new DataTable();
            dlobj = new DL_ASNReport();
            try
            {
                dtGRN = dlobj.getAccessoriesReport(sFGItemCode, sFromDate, sTODate, sWorkOrderNo);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtGRN;
        }


        public DataTable GetDeviceActivationReport(string sPONO)
        {
            DataTable dtGRN = new DataTable();
            dlobj = new DL_ASNReport();
            try
            {
                dtGRN = dlobj.GetDeviceActiationReport(sPONO);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtGRN;
        }
        public DataTable GetAdditionalReport(string sPONO)
        {
            DataTable dtGRN = new DataTable();
            dlobj = new DL_ASNReport();
            try
            {
                dtGRN = dlobj.AdditionalDataReport(sPONO);
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
