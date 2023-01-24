using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer.MES.MASTERS;
using System.Data;

namespace BusinessLayer.MES.MASTERS
{
    public class BL_ModelAccessoriesMapping
    {
        DL_ModelAccessoriesMapping dlobj;

        public DataTable dtBindKeysInGrid()
        {
            dlobj = new DL_ModelAccessoriesMapping();
            return dlobj.dtBindKeysInGrid();
        }
        public DataTable Bind_Model_Mapping_Keys(string sModelNo)
        {
            dlobj = new DL_ModelAccessoriesMapping();
            return dlobj.Bind_Model_Mapping_Keys(sModelNo);
        }
        public DataTable SaveKeysModelMapping(DataTable _dtModelKey, string sModelNo
            , string sUserID, string sSiteCode, string sFGItemCode)
        {
            dlobj = new DL_ModelAccessoriesMapping();
            return dlobj.SaveKeyModelMapping(_dtModelKey, sModelNo, sUserID, sSiteCode, sFGItemCode);
        }
    }
}
