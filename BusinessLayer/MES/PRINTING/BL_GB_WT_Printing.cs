using System;
using DataLayer.MES.PRINTING;
using PL;
using System.Data;
using Common;
using System.Reflection;

namespace BusinessLayer.MES.PRINTING
{
    public class BL_GB_WT_Printing
    {
        DL_GB_WT_Printing dlobj;
        DataTable dt;      

        public DataTable blValidateWeight(PL_Printing plobj)
        {
            dlobj = new DL_GB_WT_Printing();
            dt = new DataTable();
            try
            {
                dt = dlobj.dlValidateWeight(plobj);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, MethodBase.GetCurrentMethod().Name, "Error - blUpdateWeightRecord - dlUpdateWeightRecord  = " + ex.Message + "");
            }
            return dt;
        }

        public DataTable blGetCaptureWeight(PL_Printing plobj)
        {
            dlobj = new DL_GB_WT_Printing();
            dt = new DataTable();
            try
            {
                dt = dlobj.dlGetCaptureWeight(plobj);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, MethodBase.GetCurrentMethod().Name, "Error - blGetCaptureWeight - dlUpdateWeightRecord  = " + ex.Message + "");
            }
            return dt;
        }

        public DataTable blSaveWeight(PL_Printing plobj)
        {
            dlobj = new DL_GB_WT_Printing();
            dt = new DataTable();
            try
            {
                dt = dlobj.dlSaveWeight(plobj);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, MethodBase.GetCurrentMethod().Name, "Error - blSaveWeight - dlUpdateWeightRecord  = " + ex.Message + "");
            }
            return dt;
        }
    }
}
