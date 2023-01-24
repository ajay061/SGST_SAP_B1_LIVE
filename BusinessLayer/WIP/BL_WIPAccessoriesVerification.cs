using DataLayer.WIP;
using System.Data;

namespace BusinessLayer.WIP
{
    public class BL_WIPAccessoriesVerification
    {
        DL_AccessoriesVerification dlobj;
        public DataTable Bind_Model_Mapping_Accessories(string sFGitemCode, string sSiteCode)
        {
            dlobj = new DL_AccessoriesVerification();
            return dlobj.Bind_Model_Mapping_Accessories(sFGitemCode, sSiteCode);
        }
        public DataTable blScanIMEI(string sFGitemCode, string sSiteCode, string sBarcode)
        {
            dlobj = new DL_AccessoriesVerification();
            return dlobj.dlScanIMEIBarcode(sFGitemCode, sSiteCode, sBarcode);
        }

        public DataTable blScanAccessories(string sFGitemCode, string sSiteCode, string sBarcode
            , string sLineCode, string sModelName, string sAccessoriesBarcode, string sAccName,
            string sScannedBy)
        {
            dlobj = new DL_AccessoriesVerification();
            return dlobj.dlScanAccessoriesBarcode(sFGitemCode, sSiteCode, sBarcode, sLineCode, sModelName
                , sAccessoriesBarcode, sAccName, sScannedBy
                );
        }
        public DataTable blPcbScanDeviceVerify(string sFGitemCode, string sSiteCode, string sBarcode
            , string sLineCode, string sModelName,string sScannedBy, string sMAcId, string sType,string sPcbBarcode)
        {
            dlobj = new DL_AccessoriesVerification();
            return dlobj.dlPcbScanDeviceVerify(sFGitemCode, sSiteCode, sBarcode, sLineCode, sModelName
                , sScannedBy, sMAcId, sType, sPcbBarcode
                );
        }
        public DataTable blPcbScanGBVerify(string sFGitemCode, string sSiteCode, string sBarcode
           , string sLineCode, string sModelName, string sScannedBy, string sMAcId, string sType, string sDeviceBarcode, string sGBBarcode,string sPcbBarcode)
        {
            dlobj = new DL_AccessoriesVerification();
            return dlobj.dlPcbScanGBVerify(sFGitemCode, sSiteCode, sBarcode, sLineCode, sModelName
                , sScannedBy, sMAcId, sType, sDeviceBarcode,sGBBarcode, sPcbBarcode
                );
        }
    }
}
