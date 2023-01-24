using System;
using DataLayer.WIP;
using System.Data;
using Common;
using PL;

namespace BusinessLayer.WIP
{
    public class BL_WIP_Serial_Generation
    {
        DL_WIP_Serial_Generation dlobj = null;
        public DataTable BindBarcodeGen()
        {
            DataTable dtData = new DataTable();
            try
            {
                dlobj = new DL_WIP_Serial_Generation();
                dtData = dlobj.BindBarcode_Gen();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtData;
        }
        public DataTable BindPlant()
        {
            DataTable dtData = new DataTable();
            try
            {
                dlobj = new DL_WIP_Serial_Generation();
                dtData = dlobj.BindPlantCode();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtData;
        }
        public DataTable GetCustomer(string sFGItemCode)
        {
            DataTable dtData = new DataTable();
            try
            {
                dlobj = new DL_WIP_Serial_Generation();
                dtData = dlobj.GetCustomer(sFGItemCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtData;
        }
        public DataTable GetFGitemCode(string sSiteCode)
        {
            DataTable dtData = new DataTable();
            try
            {
                dlobj = new DL_WIP_Serial_Generation();
                dtData = dlobj.GetFGItemCode(sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtData;
        }
        public DataTable GetRestPeriod(string sSiteCode)
        {
            DataTable dtData = new DataTable();
            try
            {
                dlobj = new DL_WIP_Serial_Generation();
                dtData = dlobj.GetRestPeriod(sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtData;
        }
        public DataTable GetPrefix(string sSiteCode)
        {
            DataTable dtData = new DataTable();
            try
            {
                dlobj = new DL_WIP_Serial_Generation();
                dtData = dlobj.GetPrefix(sSiteCode);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtData;
        }
        public DataTable GetFormat(string sFormat)
        {
            DataTable dtData = new DataTable();
            try
            {
                dlobj = new DL_WIP_Serial_Generation();
                dtData = dlobj.GetFormat(sFormat);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtData;
        }
        public DataTable GetGVData()
        {
            DataTable dtDetails = new DataTable();
            try
            {
                dlobj = new DL_WIP_Serial_Generation();
                dtDetails = dlobj.GetDetailsData();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtDetails;
        }

        public string GetRunningSN(PL_WIP_SerialGeneration plobj)
        {
            string sMainSN = string.Empty;
            try
            {
                dlobj = new DL_WIP_Serial_Generation();
                string sStartPrefix = string.Empty;
                string sPrintingSNNNo = string.Empty;
                string sBarcodeWithOutPrefix = string.Empty;
                string sAfterSNSuffix = string.Empty;
                string sBeforeSuffix = string.Empty;
                string sLaserSN = string.Empty;
                bool bSNPassed = false;
                int iLength = 0;
                sMainSN = "";
                sMainSN = dlobj.GenerateSN(plobj.sFGItemCode, plobj.sCustomer, plobj.sSiteCode, plobj.sBarcodeGenerationType);
                if (sMainSN.Contains("$"))
                {
                    int iCount = sMainSN.Split('$').Length;
                    sStartPrefix = sMainSN.Split('$')[0].ToString();
                    for (int i = 1; i < iCount; i++)
                    {
                        if (sBarcodeWithOutPrefix.Length > 0)
                        {
                            sBarcodeWithOutPrefix = sBarcodeWithOutPrefix + "$" + sMainSN.Split('$')[i];
                            if (sMainSN.Split('$')[i] == " " && sMainSN.Split('$')[i + 1] == "")
                            {
                                i = i + 1;
                            }
                        }
                        else
                        {
                            sBarcodeWithOutPrefix = sMainSN.Split('$')[i];
                            if (sMainSN.Split('$')[i] == " " && sMainSN.Split('$')[i + 1] == "")
                            {
                                i = i + 1;
                            }
                        }
                    }
                    string[] sArr = sBarcodeWithOutPrefix.Split('$');
                    string[] sArr2 = plobj.sDesignerFormat.Split('$');

                    for (int i = 0; i < sArr2.Length; i++)
                    {
                        if (sArr2[i] == "8")
                        {
                            sPrintingSNNNo = sArr[i].ToString();
                            bSNPassed = true;
                        }
                        else
                        {
                            if (bSNPassed == false)
                            {
                                if (sBeforeSuffix.Length > 0)
                                {
                                    sBeforeSuffix = sBeforeSuffix + sArr[i].ToString();
                                }
                                else
                                {
                                    sBeforeSuffix = sArr[i].ToString();
                                }
                            }
                            else
                            {
                                if (sAfterSNSuffix.Length > 0)
                                {
                                    sAfterSNSuffix = sAfterSNSuffix + sArr[i].ToString();
                                }
                                else
                                {
                                    sAfterSNSuffix = sArr[i].ToString();
                                }
                            }
                        }
                    }
                    iLength = sPrintingSNNNo.Length;
                    sMainSN = sStartPrefix + sBeforeSuffix + sPrintingSNNNo.PadLeft(iLength, '0') + "" + sAfterSNSuffix;
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sMainSN;
        }

        public DataTable SaveData(PL_WIP_SerialGeneration plobj)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                dlobj = new DL_WIP_Serial_Generation();
                dtDetails = dlobj.dlSaveData(plobj);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtDetails;
        }
        public DataTable UpdateData(PL_WIP_SerialGeneration plobj)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                dlobj = new DL_WIP_Serial_Generation();
                dtDetails = dlobj.dlUpdateData(plobj);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtDetails;
        }
        public DataTable GetEditData(int PID)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                dlobj = new DL_WIP_Serial_Generation();
                dtDetails = dlobj.GetEDITData(PID);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtDetails;
        }
        public DataTable EditFormatData(int PID)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                dlobj = new DL_WIP_Serial_Generation();
                dtDetails = dlobj.GetFormatData(PID);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtDetails;
        }
    }
}
