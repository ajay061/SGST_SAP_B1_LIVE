using Common;
using DataLayer;
using DataLayer.Masters;
using System;
using System.Data;

namespace BusinessLayer.Masters
{
    public class BL_DataTransfer
    {
        DL_DataTransfer dlboj = null;
        public DataTable DataTranfer(string sType, string sValue)
        {
            DataTable dtBins = new DataTable();
            dlboj = new DL_DataTransfer();
            try
            {
                dtBins = dlboj.DataTransfer(sType, sValue);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtBins;
        }
    }
}
