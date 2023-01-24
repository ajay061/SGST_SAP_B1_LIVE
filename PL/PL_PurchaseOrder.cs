using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PL
{
    public class PL_PurchaseOrder
    {
        public int PO_ID { get; set; }
        public string sSiteCode { get; set; }       
        public string sModelCode { get; set; }
        public string sPurchaseOrderNo { get; set; }
        public string sPurchaseDate { get; set; }        
        public int iPO_QTY { get; set; }       
      
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string CREATED_BY { get; set; }      
        public bool Active { get; set; }
    }
    public class PL_invoiceMaster
    {
        public int Inv_ID { get; set; }

        public int MSMID { get; set; }

        public string sSiteCode { get; set; }
        public string sModelCode { get; set; }
        public string sPurchaseOrderNo { get; set; }
        public string sInvoiceNo { get; set; }
        public string sInvoiceDate { get; set; }
        public int iInvoice_QTY { get; set; }
        public int iInvoiceBoxSize { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string Address5 { get; set; }
        public string Address6 { get; set; }
        public string Address7 { get; set; }

        public string CREATED_BY { get; set; }

        public string STOCK_POINT_NOTE { get; set; }
        public string SUPPLIER_CODE { get; set; }
        public DateTime SHIPMENT_DATE { get; set; }
    }
}
