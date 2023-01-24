using Common;
using DataLayer;
using System;
using System.Data;
using System.Text;

namespace BusinessLayer
{
    public class BL_MachineMaster
    {
        StringBuilder sb = null;
        DL_WIPMachineMaster dlboj = null;
        public DataTable GetLineID(string sSiteCode)
        {
            DataTable dtMachineMaster = new DataTable();
            try
            {
                dlboj = new DL_WIPMachineMaster();
                dtMachineMaster = dlboj.BindLine(sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtMachineMaster;
        }
        public DataTable GetMachineRecord(string sSiteCode)
        {
            DataTable dtMachineMaster = new DataTable();
            try
            {
                dlboj = new DL_WIPMachineMaster();
                dtMachineMaster = dlboj.BindMachine(sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtMachineMaster;
        }

        public DataTable GetSeletedData(string sMachineCode, string sSiteCode)
        {
            DataTable dt = new DataTable();
            dlboj = new DL_WIPMachineMaster();
            try
            {
                dt = dlboj.FillMachineDetails(sMachineCode, sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dt;
        }

        public string SaveMachineMstData(string sMachineCode, string sMachineName, string sMachineDesc,
            string _sLineID, string _sCreBy, string type, int iMachineSeq, string sMachineFileValidate
             ,string sSiteCode
            )
        {
            dlboj = new DL_WIPMachineMaster();
            string sResult = string.Empty;
            try
            {
                DataTable dt = dlboj.SaveMachine(sMachineCode, sMachineName, sMachineDesc, _sLineID,
           _sCreBy, type, iMachineSeq, sMachineFileValidate, sSiteCode);
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
                }
                else
                {
                    sResult = "N~No result found";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }
        public string UpdateMachineRecords(string _sMachineID, string _sMName, string _sMachinedes, string _sLineID,
            string _sCreby, string type, int iMachineSeq, string sMachineFileValidate,string sSiteCode)
        {
            string sResult = string.Empty;
            dlboj = new DL_WIPMachineMaster();
            try
            {
                DataTable dt = dlboj.UpdateMachine(_sMachineID, _sMName, _sMachinedes, _sLineID,
            _sCreby, type, iMachineSeq, sMachineFileValidate,sSiteCode);
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
                }
                else
                {
                    sResult = "N~No result found";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }

        public string DeleteMachine(string _SN, string sSiteCode)
        {
            string sResult = string.Empty;
            dlboj = new DL_WIPMachineMaster();
            try
            {
                DataTable dt = dlboj.DeleteMachine(_SN, sSiteCode);
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
                }
                else
                {
                    sResult = "N~No result found";
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }

        public string MachinePrinting(string MachineId,
            string MachineName, string MACHINEDECRIPTION, string sPrinterIP
            , string sUserID, string sLineCode
            )
        {
            string sResult = string.Empty;
            string sPrintingResult = string.Empty;
            dlboj = new DL_WIPMachineMaster();
            BL_Common objBL_Common = new BL_Common();
            try
            {
                string chkPrinterStatus = string.Empty;
                chkPrinterStatus = objBL_Common.validPrinterConnection(sPrinterIP);
                string sPRN = string.Empty;
                if (chkPrinterStatus == "PRINTER READY")
                {
                    DataTable dt = objBL_Common.GetPRN("MACHINE");
                    if (dt.Rows.Count > 0)
                    {
                        sPRN = dt.Rows[0][0].ToString();
                        sPRN = objBL_Common.ALLPrinting(sPRN, MachineId, MachineName, "", "Machine Name", "Machine ID");
                        sPrintingResult = objBL_Common.sPrintDataLabel(sPrinterIP, sPRN, MachineId, "Machine_" + MachineId
                            , sUserID, sLineCode
                            );
                    }
                    else
                    {
                        sPrintingResult = "PRNNOTFOUND~~Prn not found";
                    }
                }
                else
                {
                    sPrintingResult = chkPrinterStatus;
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message + ", Query " + sb.ToString());
                throw ex;
            }
            return sPrintingResult;
        }

        public DataTable UploadMachine(DataTable dtUpload,string sUserID)
        {
            DataTable dt = new DataTable();
            dlboj = new DL_WIPMachineMaster();
            try
            {
                dt = dlboj.UploadData(dtUpload, sUserID);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dt;
        }
    }
}
