using System;
using System.Data;
using DataLayer.MES.PRINTING;
using PL;
using System.Reflection;
using Common;

namespace BusinessLayer.MES.PRINTING
{
    public class BL_LabelPrinting
    {
        DL_LabelPrinting dlobj;
        public int blCountModelIMEIDetails(PL_Printing plobj)
        {
            try
            {
                dlobj = new DL_LabelPrinting();
                return dlobj.dlCountModelIMEIDetails(plobj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string blLabelPrinting(PL_Printing obj)
        {
            BL_Common objBL_Common = new BL_Common();
            BL_MobCommon objBL_MobCommon = new BL_MobCommon();
            string sResult = string.Empty;
            dlobj = new DL_LabelPrinting();
            try
            {
                string chkPrinterStatus = string.Empty;
                chkPrinterStatus = objBL_Common.validPrinterConnection(obj.sPrinterIP);
                string sPRN = string.Empty;
                if (chkPrinterStatus == "PRINTER READY")
                {
                    DataTable dtPRN = dlobj.GetPrn(obj);
                    if (dtPRN.Rows.Count > 0)
                    {
                        sPRN = dtPRN.Rows[0][0].ToString();
                        if (sPRN.StartsWith("ERROR~"))
                        {
                            sResult = sPRN;
                            return sResult;
                        }
                        object _oResult = dlobj.dlInsertGetBarcode(obj);
                        if (_oResult.ToString().Split('^')[0] == "OKAY~")
                        {
                            int iNoOFPrints = 1;
                            iNoOFPrints = 1;
                            obj.sBarcodestring = _oResult.ToString().Split('^')[1];                          
                            sResult = objBL_MobCommon.PrintingLogic(obj, sPRN, iNoOFPrints, obj.sStageName);
                        }
                        else
                        {
                            sResult = "N~" + _oResult.ToString().Split('~')[1];
                        }
                    }
                    else
                    {
                        sResult = "N~PRN Not found";
                    }
                }
                else
                {
                    sResult = chkPrinterStatus;
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
            return sResult;
        }
    }
}
