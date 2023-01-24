using System.Data;
using DataLayer;
using PL;
using DataLayer.Masters;
using DataLayer.MES.MASTERS;

namespace BusinessLayer.Masters
{
    public class BL_InvoiceMaster
    {
        public DataTable BindFGitemCode()
        {
            DL_InvoiceMaster dlobj = new DL_InvoiceMaster();
            return dlobj.BindFGItemCode();
        }
        public DataTable BindPurchaseOrderNo(string sModel)
        {
            DL_InvoiceMaster dlobj = new DL_InvoiceMaster();
            return dlobj.BindPurchaseOrerNo(sModel);
        }
        public DataTable BindShipToAddrss(string sModel)
        {
            DL_InvoiceMaster dlobj = new DL_InvoiceMaster();
            return dlobj.BindShipToAddress(sModel);
        }
        public DataTable BindGrid()
        {
            DL_InvoiceMaster dlobj = new DL_InvoiceMaster();
            return dlobj.BindGrid();
        }
        public DataTable SearchInvoice(string sInvoice)
        {
            DL_InvoiceMaster dlobj = new DL_InvoiceMaster();
            return dlobj.SearchInvoiceNo(sInvoice);
        }

        public DataTable SaveInvoice(PL_invoiceMaster plObj)
        {
            DL_InvoiceMaster dlobj = new DL_InvoiceMaster();
            return dlobj.SaveInvoice(plObj);
        }

        public DataTable UpdateInvoice(PL_invoiceMaster plObj)
        {
            DL_InvoiceMaster dlobj = new DL_InvoiceMaster();
            return dlobj.UpdateInvoice(plObj);
        }
        public DataTable DeleteInvoice(string sInvoiceNo)
        {
            DL_InvoiceMaster dlobj = new DL_InvoiceMaster();
            return dlobj.DeleteInvoice(sInvoiceNo);
        }

        public DataTable BindAddressgRID()
        {
            DL_InvoiceMaster dlobj = new DL_InvoiceMaster();
            return dlobj.BindAddress();
        }
        public DataTable SearchAddress(string sMSMID)
        {
            DL_InvoiceMaster dlobj = new DL_InvoiceMaster();
            return dlobj.Searchaddress(sMSMID);
        }

        public DataTable SaveAdderss(PL_invoiceMaster plObj)
        {
            DL_InvoiceMaster dlobj = new DL_InvoiceMaster();
            return dlobj.dtSaveShippingAddress(plObj);
        }

        public DataTable UpdateAddress(PL_invoiceMaster plObj)
        {
            DL_InvoiceMaster dlobj = new DL_InvoiceMaster();
            return dlobj.dtUpdateShippingAddress(plObj);
        }
        public DataTable DeleteAddress(string sMSMID)
        {
            DL_InvoiceMaster dlobj = new DL_InvoiceMaster();
            return dlobj.DeleteAddress(sMSMID);
        }
    }
}
