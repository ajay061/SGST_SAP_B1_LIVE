using System;
using System.Data;
using System.Reflection;
using BcilLib;
using Common;

namespace DataLayer.MES.PRINTING
{
    public class DL_AccessoriesScanning : DCommon
    {
        DBManager odb;
        DataTable dtobj;
        public DL_AccessoriesScanning()
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
                dtobj = odb.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_ACCESSORIES_SCANNING").Tables[0];
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

        public DataTable dlScanIMEIBarcode(string sFGitemCode, string sSiteCode,string sBarcode)
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
                dtobj = odb.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_ACCESSORIES_SCANNING").Tables[0];
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
            string sScannedBy, string sIsStandlabel,string sStandLabelbarcode, string ADAPTOR_SN , string sMacID
            )
        {
            dtobj = new DataTable();
            try
            {
                odb.CreateParameters(13);
                odb.AddParameters(0, "@TYPE", "ACCESSORIESSCAN");
                odb.AddParameters(1, "@FG_ITEM_CODE", sFGitemCode);
                odb.AddParameters(2, "@SITECODE", sSiteCode);
                odb.AddParameters(3, "@PART_BARCODE", sBarcode);
                odb.AddParameters(4, "@LINECODE", sLineCode);
                odb.AddParameters(5, "@MODELNAME", sModelName);
                odb.AddParameters(6, "@ACCESSORIESBARCODE", sAccessoriesBarcode);
                odb.AddParameters(7, "@ACCESSORIESNAME", sAccName);
                odb.AddParameters(8, "@SCANBY", sScannedBy);
                odb.AddParameters(9, "@ISSTANDLABELSCAN", sIsStandlabel);
                odb.AddParameters(10, "@STANDLABELBARCODE", sStandLabelbarcode);
                //odb.AddParameters(11, "@ADAPTOR_SN", sAccessoriesBarcode);
                odb.AddParameters(11, "@ADAPTOR_SN", ADAPTOR_SN);
                odb.AddParameters(12, "@sMacID", sMacID);
               
                PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtData, MethodBase.GetCurrentMethod().Name, 
                    "Accessories Barcode Log : Scanned Barcode : " + sBarcode
                    + ", Accessories Barcode : " + sAccessoriesBarcode
                    + ", Accessories Name : " + sAccName
                    +", Stand Label :  " + sStandLabelbarcode + ", Adapter SN :  " + ADAPTOR_SN + ", MacID :  " + sMacID 
                    );
                odb.Open();
                dtobj = odb.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_ACCESSORIES_SCANNING").Tables[0];
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
