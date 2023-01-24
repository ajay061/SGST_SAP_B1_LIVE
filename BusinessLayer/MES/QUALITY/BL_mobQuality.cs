using System;
using System.Data;
using DataLayer.MES.QUALITY;
using PL;
using System.Reflection;
using Common;

namespace BusinessLayer.MES.QUALITY
{
    public class BL_mobQuality
    {
        mobQuality dlobj;

        public DataTable ValidateMachine(PL_Printing obj)
        {
            DataTable dtLocation = new DataTable();
            dlobj = new mobQuality();
            try
            {
                dtLocation = dlobj.ValidateMachine(obj);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLocation;
        }
        public DataTable BindDefect(PL_Printing obj)
        {
            DataTable dtLocation = new DataTable();
            dlobj = new mobQuality();
            try
            {
                dtLocation = dlobj.BindDefect(obj);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLocation;
        }

        public string blMobQuality(PL_Printing obj)
        {
            string sResult = string.Empty;
            dlobj = new mobQuality();
            try
            {
                sResult = Convert.ToString(dlobj.dlInsertGetBarcode(obj));
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
            return sResult;
        }
    }
}
