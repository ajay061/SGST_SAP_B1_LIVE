using System.Data;

namespace PL
{
    public class PL_Printing
    {
        public string sSiteCode { get; set; }
        public int iSNModel { get; set; }
        public string sModelPrefix { get; set; }
        public string sColorCode { get; set; }    
        public string sModelName { get; set; }
        public string sModelType { get; set; }
        public string sSNBarcode { get; set; }
        public string sPCBBarcode { get; set; }
        public string sWorkOrderComplete { get; set; }
        public string sWorkOrderNo { get; set; }
        public string sBarcodestring { get; set; }
        public string sStageCode { get; set; }
        public string sStageName { get; set; }
        public string sPrintedBy { get; set; }
        public string sType { get; set; }       
        public int iLotSize { get; set; }     
        public string sReworkStation { get; set; }
        public DataTable dPackingDetail { get; set; }      
        public string sBoxId { get; set; }
        public string sUserID { get; set; }
        public string sLineCode { get; set; }
        public string sPalletID { get; set; }
        public string sPO_Number { get; set; }        
        public string sPO_Date { get; set; }
        public string sMSN { get; set; }  
        public decimal dBoxNetWt { get; set; }
        public decimal dBoxWT { get; set; }
        public decimal dBoxGrossWt { get; set; }
        public string sPrinterIP { get; set; }  
        public string sBatchNo { get; set; }
        public double dMRP { get; set; }
        public int iSERIES { get; set; }
        public string sEANNO { get; set; }
        public string sBOMCode { get; set; }     

        public string sDefect { get; set; }
        public string sObservation { get; set; }
        public string sRemarks { get; set; }

        public string sCustomerCode { get; set; }
        public string sCustomerName { get; set; }
        public string sCustomerPartNo { get; set; }
        public string sScanType { get; set; }

        public string sScanningAllowed { get; set; }
        public int iScanningTime { get; set; }

        public string sFIFORequied { get; set; }
        public string sSamplingPCB { get; set; }

    }
}
