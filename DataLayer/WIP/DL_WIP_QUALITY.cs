using Common;
using System;
using System.Data;
using System.Text;

namespace DataLayer.WIP
{
    public class DL_WIP_QUALITY
    {
        DBManager oDbm;
        public DL_WIP_QUALITY()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }        
        public DataTable BindDefect()
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDDEFECT");
                oDbm.AddParameters(1, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_QUALITY").Tables[0];
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
        public DataTable BindFGItemCode()
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(2);
                oDbm.AddParameters(0, "@TYPE", "BINDFGITEMCODE");
                oDbm.AddParameters(1, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_QUALITY").Tables[0];
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
        public DataTable BindFGQualityParameterList(string sFGItemCode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(3);
                oDbm.AddParameters(0, "@TYPE", "BINDFGDEFECT");
                oDbm.AddParameters(1, "@SITECODE", PCommon.sSiteCode);
                oDbm.AddParameters(2, "@FGITEMCODE", sFGItemCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_QUALITY").Tables[0];
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
        public DataTable ValidateBox(string sFGitemCode, string sBoxID)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "VALIDATEBARCODE");
                oDbm.AddParameters(1, "@FGITEMCODE", sFGitemCode);
                oDbm.AddParameters(2, "@BOXBARCODE", sBoxID);
                oDbm.AddParameters(3, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_QUALITY").Tables[0];
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
        public DataTable ValidatePCBBarcode(string sFGitemCode, string sPCBBarcode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "VALIDATEPCBBARCODE");
                oDbm.AddParameters(1, "@FGITEMCODE", sFGitemCode);
                oDbm.AddParameters(2, "@PCBBARCODE", sPCBBarcode);
                oDbm.AddParameters(3, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_QUALITY").Tables[0];
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
        public DataTable ValdateAccessoriesBarcode(string sFGitemCode, string sAccessoriesBarcode)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.CreateParameters(4);
                oDbm.AddParameters(0, "@TYPE", "VALIDATEACCBARCODE");
                oDbm.AddParameters(1, "@FGITEMCODE", sFGitemCode);
                oDbm.AddParameters(2, "@PCBBARCODE", sAccessoriesBarcode);
                oDbm.AddParameters(3, "@SITECODE", PCommon.sSiteCode);
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_WIP_QUALITY").Tables[0];
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
        public DataTable dtQualityUpdate(DataTable dtQualityScannedData, string sFGItemCode, 
            string sModuletype, string sScannedBy, string sSiteCode,
           string sStatus,DataTable dtSAPPostingData,string sLineCode,string sFinalRemarks
            ,DataTable dtAccDetails
            )
        {
            DataTable dtResult = new DataTable();
            try
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, "Save Quality : Status : " + sStatus
                    + ", FG Item Code : " + sFGItemCode + ",MODULENAME : " + sModuletype
                    );
                oDbm.Open();
                oDbm.CreateParameters(11);
                oDbm.AddParameters(0, "@TYPE", "SAVEDATA");
                oDbm.AddParameters(1, "@STATUS", sStatus);
                oDbm.AddParameters(2, "@MODULENAME", sModuletype);
                oDbm.AddParameters(3, "@FGITEMCODE", sFGItemCode);
                oDbm.AddParameters(4, "@SCANNED_BY", sScannedBy);
                oDbm.AddParameters(5, "@DTQUALITYDATA", dtQualityScannedData);
                oDbm.AddParameters(6, "@SITECODE", sSiteCode);                        
                oDbm.AddParameters(7, "@LINECODE", sLineCode);
                oDbm.AddParameters(8, "@DETAILS", dtSAPPostingData);
                oDbm.AddParameters(9, "@FINALREMARKS", sFinalRemarks);
                oDbm.AddParameters(10, "@ACCDETAILS", dtAccDetails);
                dtResult = oDbm.ExecuteDataSet(CommandType.StoredProcedure, "USP_WIP_QUALITY").Tables[0];
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
            return dtResult;
        }
    }
}
