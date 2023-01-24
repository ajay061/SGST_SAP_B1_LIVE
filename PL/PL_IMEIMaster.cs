using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace PL
{
    public class PL_IMEIMaster
    {
        public int sSNModel { get; set; }
        public string sModelName { get; set; }
        public string sModelType { get; set; }
        public string sColorCode { get; set; }
        public string sSiteCode { get; set; }
        public string sUploadedby { get; set; }
        public string sLineCode { get; set; }
        public string sDuplicateRows { get; set; }
        public string sPurchaseOrder { get; set; }
        public string sMO { get; set; }
        public string sSWVersion { get; set; }
        public DataTable dtIMEIUpload { get; set; }

        public DataTable dtHexaMac { get; set; }

        public string sMPN { get; set; }
        public string sTester { get; set; }
        public DateTime dTestingDate { get; set; }
        public string sCustomer { get; set; }
        public string sProduct { get; set; }
        public string sTestTemp { get; set; }
        public string sTestCondition { get; set; }

    }
}
