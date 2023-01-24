using Common;
using System;
using System.Data;
using PL;

namespace DataLayer.MES.QUALITY
{
    public class DL_mobLifeTesting
    {
        DBManager oDbm;
        public DL_mobLifeTesting()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }

        public DataTable ValidateMachine(string sMachineID, string sSiteCode, string sLineCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "VALIDATEMACHINE");
                oDbm.AddParameters(1, "@MACHINEID", sMachineID);
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                oDbm.AddParameters(3, "@LINECODE", sLineCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_MOB_LIFETESTING_BIND").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dt;
        }
        public DataTable BindFGItemCode(string sMachineID, string sSiteCode, string sLineCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "BINDFGITEMCODE");
                oDbm.AddParameters(1, "@MACHINEID", sMachineID);
                oDbm.AddParameters(2, "@SITECODE", sSiteCode);
                oDbm.AddParameters(3, "@LINECODE", sLineCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_MOB_LIFETESTING_BIND").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dt;
        }
        public DataTable BindReworkstation(string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDREWORKSTATION");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_MOB_LIFETESTING").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dt;
        }
        public DataTable BindDefect(string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDDEFECT");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_MOB_LIFETESTING").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dt;
        }
        public DataTable BindSamplingRate(string sFGItemCode, string sSiteCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "BINDFQASAMPLING");
                oDbm.AddParameters(1, "@SITECODE", sSiteCode);
                oDbm.AddParameters(2, "@FGITEMCODE", sFGItemCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_MOB_LIFETESTING_BIND").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dt;
        }
        public DataTable dtValidatePCBCreateLot(PL_mobLifeTesting plobj)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(7);
                oDbm.AddParameters(0, "@TYPE", "VALIDATEPCB");
                oDbm.AddParameters(1, "@SITECODE", plobj.sSiteCode);
                oDbm.AddParameters(2, "@PART_BARCODE", plobj.sBarcode);
                oDbm.AddParameters(3, "@FGITEMCODE", plobj.sFGItemCode);
                oDbm.AddParameters(4, "@MACHINEID", plobj.sMachineID);
                oDbm.AddParameters(5, "@LINECODE", plobj.sLineCode);
                oDbm.AddParameters(6, "@SCANNEDBY", plobj.sScannedBy);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_MOB_LIFETESTING").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dt;
        }

        public DataTable dtValidatePCBOutLot(PL_mobLifeTesting plobj)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(7);
                oDbm.AddParameters(0, "@TYPE", "SAVEQUALITY");
                oDbm.AddParameters(1, "@SITECODE", plobj.sSiteCode);
                oDbm.AddParameters(2, "@PART_BARCODE", plobj.sBarcode);
                oDbm.AddParameters(3, "@FGITEMCODE", plobj.sFGItemCode);
                oDbm.AddParameters(4, "@MACHINEID", plobj.sMachineID);
                oDbm.AddParameters(5, "@LINECODE", plobj.sLineCode);
                oDbm.AddParameters(6, "@SCANNEDBY", plobj.sScannedBy);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_MOB_LIFETESTING").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dt;
        }



        public DataTable BindScannedPCB(PL_mobLifeTesting plobj)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "BINDSCANNEDREFNO");
                oDbm.AddParameters(1, "@SITECODE", plobj.sSiteCode);
                oDbm.AddParameters(2, "@BARCODE", plobj.sBarcode);
                oDbm.AddParameters(3, "@FGITEMCODE", plobj.sFGItemCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_MOB_LIFETESTING").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dt;
        }

        public DataTable VaildateBarcode(PL_mobLifeTesting plobj)
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(10);
                oDbm.AddParameters(0, "@TYPE", "VALIDATEPCB");
                oDbm.AddParameters(1, "@SITECODE", plobj.sSiteCode);
                oDbm.AddParameters(2, "@LINECODE", plobj.sLineCode);
                oDbm.AddParameters(3, "@FGITEMCODE", plobj.sFGItemCode);
                oDbm.AddParameters(4, "@BARCODE", plobj.sBarcode);
                oDbm.AddParameters(5, "@DEFECT", plobj.sDefect);
                oDbm.AddParameters(6, "@REWORKSTATIONID", plobj.sReworkStation);
                oDbm.AddParameters(7, "@OBSERVATION", plobj.sObservation);
                oDbm.AddParameters(8, "@ACTION", plobj.sActionType);
                oDbm.AddParameters(9, "@SCANNEDBY", plobj.sScannedBy);
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_WIP_MOB_LIFETESTING").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dtResult;
        }

        public DataTable SaveData(PL_mobLifeTesting plobj)
        {
            DataTable dtResult = new DataTable();
            try
            {
                oDbm.Open();
                oDbm.CreateParameters(11);
                oDbm.AddParameters(0, "@TYPE", "SAVEQUALITY");
                oDbm.AddParameters(1, "@SITECODE", plobj.sSiteCode);
                oDbm.AddParameters(2, "@LINECODE", plobj.sLineCode);
                oDbm.AddParameters(3, "@FGITEMCODE", plobj.sFGItemCode);
                oDbm.AddParameters(4, "@BARCODE", plobj.sBarcode);
                oDbm.AddParameters(5, "@DEFECT", plobj.sDefect);
                oDbm.AddParameters(6, "@REWORKSTATIONID", plobj.sReworkStation);
                oDbm.AddParameters(7, "@OBSERVATION", plobj.sObservation);
                oDbm.AddParameters(8, "@ACTION", plobj.sActionType);
                oDbm.AddParameters(9, "@SCANNEDBY", plobj.sScannedBy);
                oDbm.AddParameters(10, "@STATUS", plobj.sFinalResult);
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_WIP_MOB_LIFETESTING").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                oDbm.Close();
                oDbm.Dispose();
            }
            return dtResult;
        }
    }
}
