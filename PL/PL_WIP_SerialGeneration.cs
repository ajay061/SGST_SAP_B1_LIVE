using System.Data;

namespace PL
{
    public class PL_WIP_SerialGeneration
    {
        public int iPID { get; set; }

        public int iPageLabelCount { get; set; }
        public string sOtherValue { get; set; }
        public string sSiteCode { get; set; }
        public string sBarcodeGenerationType { get; set; }
        public string sCustomer { get; set; }
        public string sPartCode { get; set; }
        public string sPartDesc { get; set; }
        public string sFGItemCode { get; set; }
        public string Revision { get; set; }
        public int iFGQtyPerBox { get; set; }
        public string StartNo { get; set; }
        public int iLength { get; set; }

        public string sResetPeriod { get; set; }
        public string sPrefix { get; set; }

        public string sSufix { get; set; }
        public string sprn { get; set; }
        public string sDesignerFormat { get; set; }
        public string sFormat1 { get; set; }        
        public bool iActive { get; set; }
        public string sF1Value { get; set; }    
        public DataTable dtRecord { get; set; }

    }
}
