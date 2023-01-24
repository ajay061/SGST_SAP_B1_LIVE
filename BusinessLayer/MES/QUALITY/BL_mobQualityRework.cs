using System;
using System.Data;
using DataLayer.MES.QUALITY;
using PL;
using Common;


namespace BusinessLayer.MES.QUALITY
{
    public class BL_mobQualityRework
    {
        public DataTable bindefect(PL_Printing obj)
        {
            DL_mobQualityRework dlobj = new DL_mobQualityRework();
            DataTable dtLocation = new DataTable();
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
        public DataTable validateBarcode(PL_Printing obj)
        {
            DataTable dtLocation = new DataTable();
            DL_mobQualityRework dlobj = new DL_mobQualityRework();
            try
            {
                dtLocation = dlobj.ValidateBarcode(obj);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLocation;
        }
    }
}
