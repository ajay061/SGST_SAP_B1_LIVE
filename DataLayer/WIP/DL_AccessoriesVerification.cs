using System;
using System.Data;
using System.Reflection;
using BcilLib;
using Common;

namespace DataLayer.WIP
{
    public class DL_AccessoriesVerification : DCommon
    {
        DBManager odb;
        DataTable dtobj;
        public DL_AccessoriesVerification()
        {
            odb = SqlDBProvider();
        }

        public DataTable Bind_Model_Mapping_Accessories(string sFGitemCode, string sSiteCode)
        {
            dtobj = new DataTable();
            try
            {
                odb.CreateParameters(3);
                odb.AddParameters(0, "@TYPE", "GETACCESSORIESDETAILS");
                odb.AddParameters(1, "@FG_ITEM_CODE", sFGitemCode);
                odb.AddParameters(2, "@SITECODE", sSiteCode);
                odb.Open();
                dtobj = odb.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_ACCESSORIES_VERIFICATION").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                odb.Close();
                odb.Dispose();
            }
            return dtobj;
        }

        public DataTable dlScanIMEIBarcode(string sFGitemCode, string sSiteCode, string sBarcode)
        {
            dtobj = new DataTable();
            try
            {
                odb.CreateParameters(4);
                odb.AddParameters(0, "@TYPE", "IMEISCAN");
                odb.AddParameters(1, "@FG_ITEM_CODE", sFGitemCode);
                odb.AddParameters(2, "@SITECODE", sSiteCode);
                odb.AddParameters(3, "@PART_BARCODE", sBarcode);
                odb.Open();
                dtobj = odb.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_ACCESSORIES_VERIFICATION").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                odb.Close();
                odb.Dispose();
            }
            return dtobj;
        }
        public DataTable dlScanAccessoriesBarcode(string sFGitemCode, string sSiteCode, string sBarcode
            , string sLineCode, string sModelName, string sAccessoriesBarcode, string sAccName,
            string sScannedBy
            )
        {
            dtobj = new DataTable();
            try
            {
                odb.CreateParameters(9);
                odb.AddParameters(0, "@TYPE", "ACCESSORIESSCAN");
                odb.AddParameters(1, "@FG_ITEM_CODE", sFGitemCode);
                odb.AddParameters(2, "@SITECODE", sSiteCode);
                odb.AddParameters(3, "@PART_BARCODE", sBarcode);
                odb.AddParameters(4, "@LINECODE", sLineCode);
                odb.AddParameters(5, "@MODELNAME", sModelName);
                odb.AddParameters(6, "@ACCESSORIESBARCODE", sAccessoriesBarcode);
                odb.AddParameters(7, "@ACCESSORIESNAME", sAccName);
                odb.AddParameters(8, "@SCANBY", sScannedBy);
                odb.Open();
                dtobj = odb.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_ACCESSORIES_VERIFICATION").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                odb.Close();
                odb.Dispose();
            }
            return dtobj;
        }

        public DataTable dlPcbScanDeviceVerify(string sFGitemCode, string sSiteCode, string sBarcode
          , string sLineCode, string sModelName, string sScannedBy, string sMacId, string sType,string sPcbBarcode)
        {
            dtobj = new DataTable();
            try
            {
                odb.CreateParameters(9);
                odb.AddParameters(0, "@TYPE", sType);
                odb.AddParameters(1, "@FG_ITEM_CODE", sFGitemCode);
                odb.AddParameters(2, "@SITECODE", sSiteCode);
                odb.AddParameters(3, "@PART_BARCODE", sBarcode);
                odb.AddParameters(4, "@LINECODE", sLineCode);
                odb.AddParameters(5, "@MODELNAME", sModelName);
                odb.AddParameters(6, "@SCANBY", sScannedBy);
                odb.AddParameters(7, "@MACID", sMacId);
                odb.AddParameters(8, "@PCBSN", sPcbBarcode);

                odb.Open();
                dtobj = odb.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_ACCESSORIES_VERIFICATION").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                odb.Close();
                odb.Dispose();
            }
            return dtobj;
        }

        public DataTable dlPcbScanGBVerify(string sFGitemCode, string sSiteCode, string sBarcode
         , string sLineCode, string sModelName, string sScannedBy, string sMacId, string sType, string sDeviceBarcode, string sGBBarcode,string sPcbBarcode)
        {
            dtobj = new DataTable();
            try
            {
                odb.CreateParameters(11);
                odb.AddParameters(0, "@TYPE", sType);
                odb.AddParameters(1, "@FG_ITEM_CODE", sFGitemCode);
                odb.AddParameters(2, "@SITECODE", sSiteCode);
                odb.AddParameters(3, "@PART_BARCODE", sBarcode);
                odb.AddParameters(4, "@LINECODE", sLineCode);
                odb.AddParameters(5, "@MODELNAME", sModelName);
                odb.AddParameters(6, "@SCANBY", sScannedBy);
                odb.AddParameters(7, "@MACID", sMacId);
                odb.AddParameters(8, "@DEVICESCANBARCODE", sDeviceBarcode);
                odb.AddParameters(9, "@GBSCANBARCODE", sGBBarcode);
                odb.AddParameters(10, "@PCBSN", sPcbBarcode);
                odb.Open();
                dtobj = odb.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_ACCESSORIES_VERIFICATION").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                odb.Close();
                odb.Dispose();
            }
            return dtobj;
        }
    }
}
