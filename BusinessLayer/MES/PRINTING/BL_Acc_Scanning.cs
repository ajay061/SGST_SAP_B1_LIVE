using DataLayer.MES.PRINTING;
using System.Data;

namespace BusinessLayer.MES.PRINTING
{
    public class BL_Acc_Scanning
    {
        DL_AccessoriesScanning dlobj;
        public DataTable Bind_Model_Mapping_Accessories(string sFGitemCode, string sSiteCode)
        {
            dlobj = new DL_AccessoriesScanning();
            return dlobj.Bind_Model_Mapping_Accessories(sFGitemCode, sSiteCode);
        }
        public DataTable blScanIMEI(string sFGitemCode, string sSiteCode, string sBarcode)
        {
            dlobj = new DL_AccessoriesScanning();
            return dlobj.dlScanIMEIBarcode(sFGitemCode, sSiteCode, sBarcode);
        }

        public DataTable blScanAccessories(string sFGitemCode, string sSiteCode, string sBarcode
            , string sLineCode, string sModelName, string sAccessoriesBarcode, string sAccName,
            string sScannedBy,string sIsStandLabel,string sStandLabelbarcode, string ADAPTOR_SN,string sMacID)
        {
            dlobj = new DL_AccessoriesScanning();
            return dlobj.dlScanAccessoriesBarcode(sFGitemCode,sSiteCode,sBarcode,sLineCode,sModelName
                ,sAccessoriesBarcode,sAccName,sScannedBy, sIsStandLabel, sStandLabelbarcode, ADAPTOR_SN, sMacID
                );
        }
    }
}
