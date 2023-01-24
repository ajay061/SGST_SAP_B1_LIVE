using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.MES.QUALITY;
using PL;

namespace BusinessLayer.MES.QUALITY
{
    public class BL_mobLifeTesting
    {
        DL_mobLifeTesting dlobj = new DL_mobLifeTesting();
        public DataTable ValidateMachine(string sMachineID, string sSiteCode, string sLineCode)
        {
            DataTable dtLocation = new DataTable();
            dlobj = new DL_mobLifeTesting();
            try
            {
                dtLocation = dlobj.ValidateMachine(sMachineID, sSiteCode, sLineCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLocation;
        }
        public DataTable BindFGItemCode(string sMachineID,string sSiteCode, string sLineCode)
        {
            DataTable dtFGItemCode = new DataTable();
            try
            {
                dlobj = new DL_mobLifeTesting();
                dtFGItemCode = dlobj.BindFGItemCode(sMachineID, sSiteCode, sLineCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtFGItemCode;
        }       
        public DataTable BindDefect(string sSiteCode)
        {
            DataTable dtLineData = new DataTable();
            try
            {
                dlobj = new DL_mobLifeTesting();
                dtLineData = dlobj.BindDefect(sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLineData;
        }
        public DataTable BindFQASampling(string sFGItemCode, string sSiteCode)
        {
            DataTable dtLineData = new DataTable();
            try
            {
                dlobj = new DL_mobLifeTesting();
                dtLineData = dlobj.BindSamplingRate(sFGItemCode, sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtLineData;
        }      
        public DataTable BindReworkstation(string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                dlobj = new DL_mobLifeTesting();
                dt = dlobj.BindReworkstation(sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dt;
        }
        public DataTable blValidatePCBCreateLot(PL_mobLifeTesting plobj)
        {
            DataTable dt = new DataTable();
            try
            {
                dlobj = new DL_mobLifeTesting();
                dt = dlobj.dtValidatePCBCreateLot(plobj);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dt;
        }

        public DataTable blValidateOutLot(PL_mobLifeTesting plobj)
        {
            DataTable dt = new DataTable();
            try
            {
                dlobj = new DL_mobLifeTesting();
                dt = dlobj.dtValidatePCBOutLot(plobj);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dt;
        }


        public DataTable GetDataagainstRefNo(PL_mobLifeTesting plobj)
        {
            DataTable dt = new DataTable();
            try
            {
                dlobj = new DL_mobLifeTesting();
                dt = dlobj.BindScannedPCB(plobj);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dt;
        }
        public DataTable blVaildateBarcode(PL_mobLifeTesting plobj)
        {
            DataTable dt = new DataTable();
            try
            {
                dlobj = new DL_mobLifeTesting();
                dt = dlobj.VaildateBarcode(plobj);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dt;
        }
        public DataTable SaveData(PL_mobLifeTesting plobj)
        {
            DataTable dt = new DataTable();
            try
            {
                dlobj = new DL_mobLifeTesting();
                dt = dlobj.SaveData(plobj);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dt;
        }
    }
}
