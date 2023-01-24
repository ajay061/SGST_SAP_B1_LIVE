using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace PL
{
    public class PL_ModelMaster
    {
        public int iSNModel { get; set; }
        public string sSiteCode { get; set; }
        public string sModelName { get; set; }
        public string sEAN { get; set; }
        public string sBOM { get; set; }
        public string sModelType { get; set; }
        public string sModelPrefix { get; set; }
        public decimal dMRP { get; set; }
        public int iPACK_SIZE { get; set; }
        public int iLotSize { get; set; }
        public decimal dGBWT { get; set; }
        public decimal dTOLERANCE_PLUS { get; set; }
        public decimal dTOLERANCE_MINUS { get; set; }

        public decimal dCBWT { get; set; }
        public decimal dCTOLERANCE_PLUS { get; set; }
        public decimal dCTOLERANCE_MINUS { get; set; }

        public decimal dPBWT { get; set; }
        public decimal dPTOLERANCE_PLUS { get; set; }
        public decimal dPTOLERANCE_MINUS { get; set; }



        public int iSERIES { get; set; }
        public string sSAR { get; set; }
        public string sSWVER { get; set; }
        public string SHWVER { get; set; }
        public string sMODELDESC1 { get; set; }
        public string sMODELDESC2 { get; set; }
        public string sMODELDESC3 { get; set; }
        public string JIO_OEM { get; set; }
        public int iWarruntyInDays { get; set; }
        public string sVendorSupplying { get; set; }
        public string sRAM { get; set; }
        public string sROM { get; set; }
        public string sTACCode { get; set; } 
        public string sCreatedBy { get; set; } 
        public DataTable dtEANModelMapping { get; set; }
        public decimal dEmptyCartonWT { get; set; }
        public bool bWaveToolValidate { get; set; }
        public bool bASNShowsMac2 { get; set; }

        public int iNoOfMacAddress { get; set; }
        public DataTable dtDuplicateModelList { get; set; }

        public string REPORT_LOCATION { get; set; }
        public string PIN_NO { get; set; }
        public string REPORT_LOT_NO { get; set; }
        public string HWVERSION { get; set; }
        public string ASN_MODEL_NO { get; set; }

    }
}
