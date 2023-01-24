using System.Data;
using DataLayer;
using PL;
namespace BusinessLayer
{ 
    public class BL_Purchase_Order
    {
        public DataTable BindFGitemCode()
        {
            DL_Purchase_Order dlobj = new DL_Purchase_Order();
            return dlobj.BindFGItemCode();
        }
        public DataTable BindGrid()
        {
            DL_Purchase_Order dlobj = new DL_Purchase_Order();
            return dlobj.BindGrid();
        }
        public DataTable SearchPurchaseOrder(string sPurchaseOrder)
        {
            DL_Purchase_Order dlobj = new DL_Purchase_Order();
            return dlobj.SearchPurchaseOrder(sPurchaseOrder);
        }

        public DataTable SavePurChaseOrder(PL_PurchaseOrder plObj)
        {
            DL_Purchase_Order dlobj = new DL_Purchase_Order();
            return dlobj.SavePurChaseOrder(plObj);
        }

        public DataTable UpdatePurChaseOrder(PL_PurchaseOrder plObj)
        {
            DL_Purchase_Order dlobj = new DL_Purchase_Order();
            return dlobj.UpdatePurChaseOrder(plObj);
        }
        public DataTable DeletePurchaseOrder(string sPurchaseOrder)
        {
            DL_Purchase_Order dlobj = new DL_Purchase_Order();
            return dlobj.DeletePurchaseOrder(sPurchaseOrder);
        }
    }
}
