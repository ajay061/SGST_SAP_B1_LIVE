using System;
using DataLayer;
using PL;
using System.Data;
using Common;
using System.Reflection;

namespace BusinessLayer
{
    public class BL_MobCommon
    {
        DL_Common dlboj = null;
        public DataTable BindModel()
        {
            DataTable dt = new DataTable();
            dlboj = new DL_Common();
            try
            {
                dlboj = new DL_Common();
                dt = dlboj.dtBindModel();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dt;
        }
        public DataTable BindReasonReprint()
        {
            DataTable dt = new DataTable();
            dlboj = new DL_Common();
            try
            {
                dlboj = new DL_Common();
                dt = dlboj.dtBindReasonReprint();
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dt;
        }
        public DataTable DisplayedData(PL_Printing plobj)
        {
            DataTable dt = new DataTable();
            dlboj = new DL_Common();
            try
            {
                dlboj = new DL_Common();
                dt = dlboj.dtGetModelDetails(plobj).Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dt;
        }

        public DataSet GetModelDetails(PL_Printing plobj)
        {
            DataSet ds = new DataSet();
            dlboj = new DL_Common();
            try
            {
                dlboj = new DL_Common();
                ds = dlboj.dtGetModelDetails(plobj);
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return ds;
        }

        public bool charCheck(char input_char)
        {
            bool bValue = true;
            // Checking for Alphabet 
            if ((input_char >= 65 && input_char <= 90) || (input_char >= 97 && input_char <= 122))
            {
                bValue = true;
            }
            // Checking for Digits 
            else
            {
                bValue = false;
            }
            return bValue;
        }
        public string sFinalBarcodeValue(string sValue)
        {
            string sFinalValue = string.Empty;
            try
            {
                char[] sarr = sValue.ToCharArray();
                string sNumber = "!099";
                string sAlpha = "!100";
                string sStartNumber = "!105";
                string sStartAlpha = "!104";
                bool bLastChar = false;
                for (int i = 0; i < sarr.Length; i++)
                {
                    if (i == 0)
                    {
                        if (charCheck(sarr[i]))
                        {
                            sFinalValue = sStartAlpha + sarr[i];
                            bLastChar = true;
                        }
                        else
                        {
                            sFinalValue = sStartNumber + sarr[i];
                            bLastChar = false;
                        }
                    }
                    else
                    {
                        if (charCheck(sarr[i]))
                        {
                            if (bLastChar == false)
                            {
                                sFinalValue = sFinalValue + sAlpha + sarr[i];
                            }
                            else
                            {
                                sFinalValue = sFinalValue + sarr[i];
                            }
                            bLastChar = true;

                        }
                        else
                        {
                            if (bLastChar == true)
                            {
                                sFinalValue = sFinalValue + sNumber + sarr[i];
                            }
                            else
                            {
                                sFinalValue = sFinalValue + sarr[i];
                            }
                            bLastChar = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sFinalValue;
        }
        public string sPRNLogic(PL_Printing obj, string sPRN)
        {
            dlboj = new DL_Common();
            string sFinalBarcodeValueData = string.Empty;
            string sReplacedPRN = string.Empty;
            string SERIAL_NO = string.Empty;
            string GRPONSN = string.Empty;
            string MAC = string.Empty;
            string MAC_2 = string.Empty;
            string WIFI_MAC = string.Empty;
            string KEY_PART_NO = string.Empty;
            string WIRELSESS_SSID = string.Empty;
            string PRE_PASSWORD = string.Empty;
            string PCB_SN = string.Empty;
            string VmxData = string.Empty;
            string sCOl1 = string.Empty;
            string sCol2 = string.Empty;
            string sCol3 = string.Empty;
            string sCol4 = string.Empty;
            string sCol5 = string.Empty;
            string sCol6 = string.Empty;
            string sCol7 = string.Empty;
            string sCol8 = string.Empty;
            string sCol9 = string.Empty;
            try
            {
                DataSet dsLabelPrintingData = dlboj.dlGetLabelPrintingDetails(obj);
                if (dsLabelPrintingData.Tables.Count > 0)
                {
                    SERIAL_NO = dsLabelPrintingData.Tables[0].Rows[0]["SERIAL_NO"].ToString();
                    GRPONSN = dsLabelPrintingData.Tables[0].Rows[0]["GRPONSN"].ToString();
                    MAC = dsLabelPrintingData.Tables[0].Rows[0]["MAC"].ToString();
                    MAC_2 = dsLabelPrintingData.Tables[0].Rows[0]["MAC_2"].ToString();
                    KEY_PART_NO = dsLabelPrintingData.Tables[0].Rows[0]["KEY_PART_NO"].ToString();
                    WIFI_MAC = dsLabelPrintingData.Tables[0].Rows[0]["WIFI_MAC"].ToString();
                    WIRELSESS_SSID = dsLabelPrintingData.Tables[0].Rows[0]["WIRELSESS_SSID"].ToString();
                    PRE_PASSWORD = dsLabelPrintingData.Tables[0].Rows[0]["PRE_PASSWORD"].ToString();
                    PCB_SN = dsLabelPrintingData.Tables[0].Rows[0]["PCB_SN"].ToString();
                    VmxData = dsLabelPrintingData.Tables[0].Rows[0]["VmxData"].ToString();
                    sCOl1 = dsLabelPrintingData.Tables[0].Rows[0]["COL1"].ToString();
                    sCol2 = dsLabelPrintingData.Tables[0].Rows[0]["COL2"].ToString();
                    sCol3 = dsLabelPrintingData.Tables[0].Rows[0]["COL3"].ToString();
                    sCol4 = dsLabelPrintingData.Tables[0].Rows[0]["COL4"].ToString();
                    sCol5 = dsLabelPrintingData.Tables[0].Rows[0]["COL5"].ToString();
                    sCol6 = dsLabelPrintingData.Tables[0].Rows[0]["COL6"].ToString();
                    sCol7 = dsLabelPrintingData.Tables[0].Rows[0]["COL7"].ToString();
                    sCol8 = dsLabelPrintingData.Tables[0].Rows[0]["COL8"].ToString();
                    sCol9 = dsLabelPrintingData.Tables[0].Rows[0]["COL9"].ToString();

                    sPRN = sPRN.Replace("{RSN_1}", SERIAL_NO);
                    sFinalBarcodeValueData = string.Empty;
                    sFinalBarcodeValueData = sFinalBarcodeValue(SERIAL_NO);
                    sPRN = sPRN.Replace("{RSN_BC}", sFinalBarcodeValueData);
                    sPRN = sPRN.Replace("{PCB}", PCB_SN);
                    sFinalBarcodeValueData = string.Empty;
                    sFinalBarcodeValueData = sFinalBarcodeValue(PCB_SN);
                    sPRN = sPRN.Replace("{PCB_BC}", sFinalBarcodeValueData);
                    sPRN = sPRN.Replace("{WIFI_MAC}", WIFI_MAC);
                    sFinalBarcodeValueData = string.Empty;
                    sFinalBarcodeValueData = sFinalBarcodeValue(WIFI_MAC);
                    sPRN = sPRN.Replace("{WIFI_MAC_BC}", sFinalBarcodeValueData);
                    sPRN = sPRN.Replace("{MAC}", MAC);
                    sFinalBarcodeValueData = string.Empty;
                    sFinalBarcodeValueData = sFinalBarcodeValue(MAC);
                    sPRN = sPRN.Replace("{MAC_BC}", sFinalBarcodeValueData);
                    sPRN = sPRN.Replace("{KEYPART}", KEY_PART_NO);
                    sFinalBarcodeValueData = string.Empty;
                    sFinalBarcodeValueData = sFinalBarcodeValue(KEY_PART_NO);
                    sPRN = sPRN.Replace("{KEYPART_BC}", sFinalBarcodeValueData);
                    sPRN = sPRN.Replace("{PASSWORD}", PRE_PASSWORD);
                    sPRN = sPRN.Replace("{SSID}", WIRELSESS_SSID);
                    sPRN = sPRN.Replace("{VMXDATA}", VmxData);

                    /// ADDITIONAL PRINTING TOCKEN
                    sPRN = sPRN.Replace("{COL1}", sCOl1);
                    sPRN = sPRN.Replace("{COL2}", sCol2);
                    sPRN = sPRN.Replace("{COL3}", sCol3);
                    sPRN = sPRN.Replace("{COL4}", sCol4);
                    sPRN = sPRN.Replace("{COL5}", sCol5);
                    sPRN = sPRN.Replace("{COL6}", sCol6);
                    sPRN = sPRN.Replace("{COL7}", sCol7);
                    sPRN = sPRN.Replace("{COL8}", sCol8);
                    sPRN = sPRN.Replace("{COL9}", sCol9);
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
            return sPRN;
        }
        public string PrintingLogic(PL_Printing plobj, string sPRN, int iNoofPrints, string sModule)
        {
            DL_Common dlobj = new DL_Common();
            BL_MobCommon objBL_Common = new BL_MobCommon();
            BL_Common obj = new BL_Common();
            string sResult = string.Empty;
            string sFinalBarcodeValueData = string.Empty;
            int iCount = 1;
            try
            {
                DataSet dsPrintingData = GetModelDetails(plobj);
                if (dsPrintingData.Tables.Count > 0)
                {
                    DataTable dtModelDetails = dsPrintingData.Tables[0];
                    if (dtModelDetails.Rows.Count > 0)
                    {
                        plobj.dMRP = Convert.ToDouble(dtModelDetails.Rows[0]["MRP"].ToString());
                        plobj.sModelName = dtModelDetails.Rows[0]["MODEL_CODE"].ToString();
                        plobj.sModelType = dtModelDetails.Rows[0]["MODEL_DESC"].ToString();
                        plobj.sEANNO = dtModelDetails.Rows[0]["EAN_CODE"].ToString();
                        plobj.sBOMCode = dtModelDetails.Rows[0]["BOM_CODE"].ToString();
                    }
                    DataTable dtWeakData = dsPrintingData.Tables[1];
                    if (dtWeakData.Rows.Count > 0)
                    {
                        string sData = dtWeakData.Rows[0][0].ToString();
                        string[] sArr = sData.Split('^');
                        sPRN = sPRN.Replace("{WEEK}", "W" + Convert.ToString(sArr[0]));
                        sPRN = sPRN.Replace("{YEAR}", Convert.ToString(sArr[1]).Substring(2, 2));
                        sPRN = sPRN.Replace("{MONTHNAME}", Convert.ToString(sArr[2]));
                        sPRN = sPRN.Replace("{MONTH}", Convert.ToString(sArr[3]));
                        sPRN = sPRN.Replace("{DATE}", Convert.ToString(sArr[4]));
                        sPRN = sPRN.Replace("{MONTHYEAR}", Convert.ToString(sArr[3]) + "-" + Convert.ToString(sArr[1]).Substring(2, 2));
                    }
                }
                if (sModule == "BOX")
                {
                    DataSet dsBoxLabelPrintingData = dlobj.dlGetFGPackingDetailForPrn(plobj);
                    if (dsBoxLabelPrintingData.Tables.Count > 0)
                    {
                        if (dsBoxLabelPrintingData.Tables[0].Rows.Count > 0)
                        {
                            sPRN = sPRN.Replace("{BOXID}", Convert.ToString(dsBoxLabelPrintingData.Tables[0].Rows[0][0]));
                            sFinalBarcodeValueData = string.Empty;
                            sFinalBarcodeValueData = sFinalBarcodeValue(Convert.ToString(dsBoxLabelPrintingData.Tables[0].Rows[0][0]));
                            sPRN = sPRN.Replace("{BOX_B}", sFinalBarcodeValueData);
                            sPRN = sPRN.Replace("{BOXNETWT}", Convert.ToString(dsBoxLabelPrintingData.Tables[0].Rows[0][1]));
                            sPRN = sPRN.Replace("{BOXGROSSWT}", Convert.ToString(dsBoxLabelPrintingData.Tables[0].Rows[0][2]));
                            sPRN = sPRN.Replace("{BOXWT}", Convert.ToString(dsBoxLabelPrintingData.Tables[0].Rows[0][3]));
                            sPRN = sPRN.Replace("{BOXPRINTDATE}", Convert.ToString(dsBoxLabelPrintingData.Tables[0].Rows[0][4]));
                            sPRN = sPRN.Replace("{PONUMBER}", Convert.ToString(dsBoxLabelPrintingData.Tables[0].Rows[0][5]));
                            sPRN = sPRN.Replace("{PODATE}", Convert.ToString(dsBoxLabelPrintingData.Tables[0].Rows[0]["PO_DATE"]));
                            sPRN = sPRN.Replace("{CARTON_NO}", Convert.ToString(dsBoxLabelPrintingData.Tables[0].Rows[0]["CARTON_NO"]));//   CARTON NO : 05/500
                            sPRN = sPRN.Replace("{MSN}", Convert.ToString(dsBoxLabelPrintingData.Tables[0].Rows[0]["MSN_NO"]));
                            sFinalBarcodeValueData = string.Empty;
                            sFinalBarcodeValueData = sFinalBarcodeValue(Convert.ToString(dsBoxLabelPrintingData.Tables[0].Rows[0]["MSN_NO"]));
                            sPRN = sPRN.Replace("{MSN_B}", sFinalBarcodeValueData);
                        }
                        iCount = dsBoxLabelPrintingData.Tables[1].Rows.Count;
                        sPRN = sPRN.Replace("{UNIT}", Convert.ToString(iCount));
                        DataTable dtIMEIDetails = dsBoxLabelPrintingData.Tables[1];
                        for (int i = 1; i <= iCount; i++)
                        {
                            if (Convert.ToString(dtIMEIDetails.Rows[i - 1]["IMEI1"]) != "")
                            {
                                sPRN = sPRN.Replace("{IMEI" + i + "_A}", Convert.ToString(dtIMEIDetails.Rows[i - 1]["IMEI1"]));
                                sFinalBarcodeValueData = string.Empty;
                                sFinalBarcodeValueData = sFinalBarcodeValue(Convert.ToString(dtIMEIDetails.Rows[i - 1]["IMEI1"]));
                                sPRN = sPRN.Replace("{IMEI" + i + "_AB}", sFinalBarcodeValueData);
                            }
                            if (Convert.ToString(dtIMEIDetails.Rows[i - 1]["IMEI2"]) != "")
                            {
                                sPRN = sPRN.Replace("{IMEI" + i + "_B}", Convert.ToString(dtIMEIDetails.Rows[i - 1]["IMEI2"]));
                                sFinalBarcodeValueData = string.Empty;
                                sFinalBarcodeValueData = sFinalBarcodeValue(Convert.ToString(dtIMEIDetails.Rows[i - 1]["IMEI2"]));
                                sPRN = sPRN.Replace("{IMEI" + i + "_BB}", sFinalBarcodeValueData);
                            }
                            if (Convert.ToString(dtIMEIDetails.Rows[i - 1]["IMEI3"]) != "")
                            {
                                sPRN = sPRN.Replace("{IMEI" + i + "_C}", Convert.ToString(dtIMEIDetails.Rows[i - 1]["IMEI3"]));
                                sFinalBarcodeValueData = string.Empty;
                                sFinalBarcodeValueData = sFinalBarcodeValue(Convert.ToString(dtIMEIDetails.Rows[i - 1]["IMEI3"]));
                                sPRN = sPRN.Replace("{IMEI" + i + "_CB}", sFinalBarcodeValueData);
                            }
                            if (Convert.ToString(dtIMEIDetails.Rows[i - 1]["IMEI4"]) != "")
                            {
                                sPRN = sPRN.Replace("{IMEI" + i + "_D}", Convert.ToString(dtIMEIDetails.Rows[i - 1]["IMEI4"]));
                                sFinalBarcodeValueData = string.Empty;
                                sFinalBarcodeValueData = sFinalBarcodeValue(Convert.ToString(dtIMEIDetails.Rows[i - 1]["IMEI4"]));
                                sPRN = sPRN.Replace("{IMEI" + i + "_DB}", sFinalBarcodeValueData);
                            }
                            if (Convert.ToString(dtIMEIDetails.Rows[i - 1]["BT"]) != "")
                            {
                                sPRN = sPRN.Replace("{BT" + i + "}", Convert.ToString(dtIMEIDetails.Rows[i - 1]["BT"]));
                                sFinalBarcodeValueData = string.Empty;
                                sFinalBarcodeValueData = sFinalBarcodeValue(Convert.ToString(dtIMEIDetails.Rows[i - 1]["BT"]));
                                sPRN = sPRN.Replace("{BT" + i + "B}", sFinalBarcodeValueData);
                            }
                            if (Convert.ToString(dtIMEIDetails.Rows[i - 1]["WIFI"]) != "")
                            {
                                sPRN = sPRN.Replace("{WIFI" + i + "}", Convert.ToString(dtIMEIDetails.Rows[i - 1]["WIFI"]));
                                sFinalBarcodeValueData = string.Empty;
                                sFinalBarcodeValueData = sFinalBarcodeValue(Convert.ToString(dtIMEIDetails.Rows[i - 1]["WIFI"]));
                                sPRN = sPRN.Replace("{WIFI" + i + "B}", sFinalBarcodeValueData);
                            }
                            if (Convert.ToString(dtIMEIDetails.Rows[i - 1]["PCB_ID"]) != "")
                            {
                                sPRN = sPRN.Replace("{PCB" + i + "}", Convert.ToString(dtIMEIDetails.Rows[i - 1]["PCB_ID"]));
                                sFinalBarcodeValueData = string.Empty;
                                sFinalBarcodeValueData = sFinalBarcodeValue(Convert.ToString(dtIMEIDetails.Rows[i - 1]["PCB_ID"]));
                                sPRN = sPRN.Replace("{PCB" + i + "B}", sFinalBarcodeValueData);
                            }
                            if (Convert.ToString(dtIMEIDetails.Rows[i - 1]["MAC"]) != "")
                            {
                                sPRN = sPRN.Replace("{MAC" + i + "}", Convert.ToString(dtIMEIDetails.Rows[i - 1]["MAC"]));
                                sFinalBarcodeValueData = string.Empty;
                                sFinalBarcodeValueData = sFinalBarcodeValue(Convert.ToString(dtIMEIDetails.Rows[i - 1]["MAC"]));
                                sPRN = sPRN.Replace("{MAC" + i + "B}", sFinalBarcodeValueData);
                            }
                            if (Convert.ToString(dtIMEIDetails.Rows[i - 1]["MAC_2"]) != "")
                            {
                                sPRN = sPRN.Replace("{MAC_2" + i + "}", Convert.ToString(dtIMEIDetails.Rows[i - 1]["MAC_2"]));
                                sFinalBarcodeValueData = string.Empty;
                                sFinalBarcodeValueData = sFinalBarcodeValue(Convert.ToString(dtIMEIDetails.Rows[i - 1]["MAC_2"]));
                                sPRN = sPRN.Replace("{MAC_2" + i + "B}", sFinalBarcodeValueData);
                            }
                            if (Convert.ToString(dtIMEIDetails.Rows[i - 1]["KEY_PART_NO"]) != "")
                            {
                                sPRN = sPRN.Replace("{KEY_PART_NO" + i + "}", Convert.ToString(dtIMEIDetails.Rows[i - 1]["KEY_PART_NO"]));
                                sFinalBarcodeValueData = string.Empty;
                                sFinalBarcodeValueData = sFinalBarcodeValue(Convert.ToString(dtIMEIDetails.Rows[i - 1]["KEY_PART_NO"]));
                                sPRN = sPRN.Replace("{KEY_PART_NO" + i + "B}", sFinalBarcodeValueData);
                            }
                            sPRN = sPRN.Replace("{RSN" + i + "}", Convert.ToString(dtIMEIDetails.Rows[i - 1]["SR_NO"]));
                            sFinalBarcodeValueData = string.Empty;
                            sFinalBarcodeValueData = sFinalBarcodeValue(Convert.ToString(dtIMEIDetails.Rows[i - 1]["SR_NO"]));
                            sPRN = sPRN.Replace("{RSN" + i + "_B}", sFinalBarcodeValueData);
                        }
                    }
                }
                else
                {
                    sPRN = sPRNLogic(plobj, sPRN);
                    plobj.sBoxId = plobj.sBarcodestring;
                }
                sPRN = sPRN.Replace("{EAN}", plobj.sEANNO);
                sPRN = sPRN.Replace("{BOMCODE}", plobj.sBOMCode);
                sPRN = sPRN.Replace("{MODELNAME}", plobj.sModelName);
                sPRN = sPRN.Replace("{MRP}", Convert.ToString(plobj.dMRP));
                sPRN = sPRN.Replace("{MODELDESC}", plobj.sModelType);

                for (int i = 0; i < iNoofPrints; i++)
                {
                    sResult = obj.sPrintDataLabel(plobj.sPrinterIP, sPRN, plobj.sBoxId.Replace('/', 'P') + "_" + i.ToString(), sModule
                     , plobj.sUserID, plobj.sLineCode
                     );
                }
                
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }
        public string RPrintingLogic(PL_Printing plobj, string sPRN, int iNoofPrints, string sModule,string ddlReasonofReprint, string txtRemarks,string _sPrintingType)
        {
            DL_Common dlobj = new DL_Common();
            BL_MobCommon objBL_Common = new BL_MobCommon();
            BL_Common obj = new BL_Common();
            string sResult = string.Empty;
            string sFinalBarcodeValueData = string.Empty;
            int iCount = 1;
            try
            {
                DataSet dsPrintingData = GetModelDetails(plobj);
                if (dsPrintingData.Tables.Count > 0)
                {
                    DataTable dtModelDetails = dsPrintingData.Tables[0];
                    if (dtModelDetails.Rows.Count > 0)
                    {
                        plobj.dMRP = Convert.ToDouble(dtModelDetails.Rows[0]["MRP"].ToString());
                        plobj.sModelName = dtModelDetails.Rows[0]["MODEL_CODE"].ToString();
                        plobj.sModelType = dtModelDetails.Rows[0]["MODEL_DESC"].ToString();
                        plobj.sEANNO = dtModelDetails.Rows[0]["EAN_CODE"].ToString();
                        plobj.sBOMCode = dtModelDetails.Rows[0]["BOM_CODE"].ToString();
                    }
                    DataTable dtWeakData = dsPrintingData.Tables[1];
                    if (dtWeakData.Rows.Count > 0)
                    {
                        string sData = dtWeakData.Rows[0][0].ToString();
                        string[] sArr = sData.Split('^');
                        sPRN = sPRN.Replace("{WEEK}", "W" + Convert.ToString(sArr[0]));
                        sPRN = sPRN.Replace("{YEAR}", Convert.ToString(sArr[1]).Substring(2, 2));
                        sPRN = sPRN.Replace("{MONTHNAME}", Convert.ToString(sArr[2]));
                        sPRN = sPRN.Replace("{MONTH}", Convert.ToString(sArr[3]));
                        sPRN = sPRN.Replace("{DATE}", Convert.ToString(sArr[4]));
                        sPRN = sPRN.Replace("{MONTHYEAR}", Convert.ToString(sArr[3]) + "-" + Convert.ToString(sArr[1]).Substring(2, 2));
                    }
                }
                if (sModule == "BOX")
                {
                    DataSet dsBoxLabelPrintingData = dlobj.dlGetFGPackingDetailForPrn(plobj);
                    if (dsBoxLabelPrintingData.Tables.Count > 0)
                    {
                        if (dsBoxLabelPrintingData.Tables[0].Rows.Count > 0)
                        {
                            sPRN = sPRN.Replace("{BOXID}", Convert.ToString(dsBoxLabelPrintingData.Tables[0].Rows[0][0]));
                            sFinalBarcodeValueData = string.Empty;
                            sFinalBarcodeValueData = sFinalBarcodeValue(Convert.ToString(dsBoxLabelPrintingData.Tables[0].Rows[0][0]));
                            sPRN = sPRN.Replace("{BOX_B}", sFinalBarcodeValueData);
                            sPRN = sPRN.Replace("{BOXNETWT}", Convert.ToString(dsBoxLabelPrintingData.Tables[0].Rows[0][1]));
                            sPRN = sPRN.Replace("{BOXGROSSWT}", Convert.ToString(dsBoxLabelPrintingData.Tables[0].Rows[0][2]));
                            sPRN = sPRN.Replace("{BOXWT}", Convert.ToString(dsBoxLabelPrintingData.Tables[0].Rows[0][3]));
                            sPRN = sPRN.Replace("{BOXPRINTDATE}", Convert.ToString(dsBoxLabelPrintingData.Tables[0].Rows[0][4]));
                            sPRN = sPRN.Replace("{PONUMBER}", Convert.ToString(dsBoxLabelPrintingData.Tables[0].Rows[0][5]));
                            sPRN = sPRN.Replace("{PODATE}", Convert.ToString(dsBoxLabelPrintingData.Tables[0].Rows[0]["PO_DATE"]));
                            sPRN = sPRN.Replace("{CARTON_NO}", Convert.ToString(dsBoxLabelPrintingData.Tables[0].Rows[0]["CARTON_NO"]));//   CARTON NO : 05/500
                            sPRN = sPRN.Replace("{MSN}", Convert.ToString(dsBoxLabelPrintingData.Tables[0].Rows[0]["MSN_NO"]));
                            sFinalBarcodeValueData = string.Empty;
                            sFinalBarcodeValueData = sFinalBarcodeValue(Convert.ToString(dsBoxLabelPrintingData.Tables[0].Rows[0]["MSN_NO"]));
                            sPRN = sPRN.Replace("{MSN_B}", sFinalBarcodeValueData);
                        }
                        iCount = dsBoxLabelPrintingData.Tables[1].Rows.Count;
                        sPRN = sPRN.Replace("{UNIT}", Convert.ToString(iCount));
                        DataTable dtIMEIDetails = dsBoxLabelPrintingData.Tables[1];
                        for (int i = 1; i <= iCount; i++)
                        {
                            if (Convert.ToString(dtIMEIDetails.Rows[i - 1]["IMEI1"]) != "")
                            {
                                sPRN = sPRN.Replace("{IMEI" + i + "_A}", Convert.ToString(dtIMEIDetails.Rows[i - 1]["IMEI1"]));
                                sFinalBarcodeValueData = string.Empty;
                                sFinalBarcodeValueData = sFinalBarcodeValue(Convert.ToString(dtIMEIDetails.Rows[i - 1]["IMEI1"]));
                                sPRN = sPRN.Replace("{IMEI" + i + "_AB}", sFinalBarcodeValueData);
                            }
                            if (Convert.ToString(dtIMEIDetails.Rows[i - 1]["IMEI2"]) != "")
                            {
                                sPRN = sPRN.Replace("{IMEI" + i + "_B}", Convert.ToString(dtIMEIDetails.Rows[i - 1]["IMEI2"]));
                                sFinalBarcodeValueData = string.Empty;
                                sFinalBarcodeValueData = sFinalBarcodeValue(Convert.ToString(dtIMEIDetails.Rows[i - 1]["IMEI2"]));
                                sPRN = sPRN.Replace("{IMEI" + i + "_BB}", sFinalBarcodeValueData);
                            }
                            if (Convert.ToString(dtIMEIDetails.Rows[i - 1]["IMEI3"]) != "")
                            {
                                sPRN = sPRN.Replace("{IMEI" + i + "_C}", Convert.ToString(dtIMEIDetails.Rows[i - 1]["IMEI3"]));
                                sFinalBarcodeValueData = string.Empty;
                                sFinalBarcodeValueData = sFinalBarcodeValue(Convert.ToString(dtIMEIDetails.Rows[i - 1]["IMEI3"]));
                                sPRN = sPRN.Replace("{IMEI" + i + "_CB}", sFinalBarcodeValueData);
                            }
                            if (Convert.ToString(dtIMEIDetails.Rows[i - 1]["IMEI4"]) != "")
                            {
                                sPRN = sPRN.Replace("{IMEI" + i + "_D}", Convert.ToString(dtIMEIDetails.Rows[i - 1]["IMEI4"]));
                                sFinalBarcodeValueData = string.Empty;
                                sFinalBarcodeValueData = sFinalBarcodeValue(Convert.ToString(dtIMEIDetails.Rows[i - 1]["IMEI4"]));
                                sPRN = sPRN.Replace("{IMEI" + i + "_DB}", sFinalBarcodeValueData);
                            }
                            if (Convert.ToString(dtIMEIDetails.Rows[i - 1]["BT"]) != "")
                            {
                                sPRN = sPRN.Replace("{BT" + i + "}", Convert.ToString(dtIMEIDetails.Rows[i - 1]["BT"]));
                                sFinalBarcodeValueData = string.Empty;
                                sFinalBarcodeValueData = sFinalBarcodeValue(Convert.ToString(dtIMEIDetails.Rows[i - 1]["BT"]));
                                sPRN = sPRN.Replace("{BT" + i + "B}", sFinalBarcodeValueData);
                            }
                            if (Convert.ToString(dtIMEIDetails.Rows[i - 1]["WIFI"]) != "")
                            {
                                sPRN = sPRN.Replace("{WIFI" + i + "}", Convert.ToString(dtIMEIDetails.Rows[i - 1]["WIFI"]));
                                sFinalBarcodeValueData = string.Empty;
                                sFinalBarcodeValueData = sFinalBarcodeValue(Convert.ToString(dtIMEIDetails.Rows[i - 1]["WIFI"]));
                                sPRN = sPRN.Replace("{WIFI" + i + "B}", sFinalBarcodeValueData);
                            }
                            if (Convert.ToString(dtIMEIDetails.Rows[i - 1]["PCB_ID"]) != "")
                            {
                                sPRN = sPRN.Replace("{PCB" + i + "}", Convert.ToString(dtIMEIDetails.Rows[i - 1]["PCB_ID"]));
                                sFinalBarcodeValueData = string.Empty;
                                sFinalBarcodeValueData = sFinalBarcodeValue(Convert.ToString(dtIMEIDetails.Rows[i - 1]["PCB_ID"]));
                                sPRN = sPRN.Replace("{PCB" + i + "B}", sFinalBarcodeValueData);
                            }
                            if (Convert.ToString(dtIMEIDetails.Rows[i - 1]["MAC"]) != "")
                            {
                                sPRN = sPRN.Replace("{MAC" + i + "}", Convert.ToString(dtIMEIDetails.Rows[i - 1]["MAC"]));
                                sFinalBarcodeValueData = string.Empty;
                                sFinalBarcodeValueData = sFinalBarcodeValue(Convert.ToString(dtIMEIDetails.Rows[i - 1]["MAC"]));
                                sPRN = sPRN.Replace("{MAC" + i + "B}", sFinalBarcodeValueData);
                            }
                            if (Convert.ToString(dtIMEIDetails.Rows[i - 1]["MAC_2"]) != "")
                            {
                                sPRN = sPRN.Replace("{MAC_2" + i + "}", Convert.ToString(dtIMEIDetails.Rows[i - 1]["MAC_2"]));
                                sFinalBarcodeValueData = string.Empty;
                                sFinalBarcodeValueData = sFinalBarcodeValue(Convert.ToString(dtIMEIDetails.Rows[i - 1]["MAC_2"]));
                                sPRN = sPRN.Replace("{MAC_2" + i + "B}", sFinalBarcodeValueData);
                            }
                            if (Convert.ToString(dtIMEIDetails.Rows[i - 1]["KEY_PART_NO"]) != "")
                            {
                                sPRN = sPRN.Replace("{KEY_PART_NO" + i + "}", Convert.ToString(dtIMEIDetails.Rows[i - 1]["KEY_PART_NO"]));
                                sFinalBarcodeValueData = string.Empty;
                                sFinalBarcodeValueData = sFinalBarcodeValue(Convert.ToString(dtIMEIDetails.Rows[i - 1]["KEY_PART_NO"]));
                                sPRN = sPRN.Replace("{KEY_PART_NO" + i + "B}", sFinalBarcodeValueData);
                            }
                            sPRN = sPRN.Replace("{RSN" + i + "}", Convert.ToString(dtIMEIDetails.Rows[i - 1]["SR_NO"]));
                            sFinalBarcodeValueData = string.Empty;
                            sFinalBarcodeValueData = sFinalBarcodeValue(Convert.ToString(dtIMEIDetails.Rows[i - 1]["SR_NO"]));
                            sPRN = sPRN.Replace("{RSN" + i + "_B}", sFinalBarcodeValueData);
                        }
                    }
                }
                else
                {
                    sPRN = sPRNLogic(plobj, sPRN);
                    plobj.sBoxId = plobj.sBarcodestring;
                }
                sPRN = sPRN.Replace("{EAN}", plobj.sEANNO);
                sPRN = sPRN.Replace("{BOMCODE}", plobj.sBOMCode);
                sPRN = sPRN.Replace("{MODELNAME}", plobj.sModelName);
                sPRN = sPRN.Replace("{MRP}", Convert.ToString(plobj.dMRP));
                sPRN = sPRN.Replace("{MODELDESC}", plobj.sModelType);

                for (int i = 0; i < iNoofPrints; i++)
                {
                    sResult = obj.sRPrintDataLabel(plobj.sPrinterIP, sPRN, plobj.sBoxId.Replace('/', 'P') + "_" + i.ToString(), sModule
                     , plobj.sUserID, plobj.sLineCode, ddlReasonofReprint, txtRemarks, _sPrintingType
                     );
                }

            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }
        public string sPalletPrint(PL_Printing plobj, string sPRN, string sPalletID, int iNoofPrints)
        {
            BL_Common objCommon = new BL_Common();
            string sPRNData = string.Empty;
            DL_Common dlobj = new DL_Common();
            try
            {
                plobj.sPalletID = sPalletID;
                DataSet dsPrintingData = GetModelDetails(plobj);
                if (dsPrintingData.Tables.Count > 0)
                {
                    DataTable dtPrintingData = dsPrintingData.Tables[0];
                    if (dtPrintingData.Rows.Count > 0)
                    {
                        plobj.dMRP = Convert.ToDouble(dtPrintingData.Rows[0]["MRP"].ToString());
                        plobj.sModelName = dtPrintingData.Rows[0]["MODEL_CODE"].ToString();
                        plobj.sModelType = dtPrintingData.Rows[0]["MODEL_DESC"].ToString();
                        plobj.sEANNO = dtPrintingData.Rows[0]["EAN_CODE"].ToString();
                        plobj.sBOMCode = dtPrintingData.Rows[0]["BOM_CODE"].ToString();
                        plobj.sModelName = dtPrintingData.Rows[0]["MODEL_CODE"].ToString();
                        sPRN = sPRN.Replace("{MODELNAME}", plobj.sModelName);
                        sPRN = sPRN.Replace("{EAN}", plobj.sEANNO);
                        sPRN = sPRN.Replace("{BOM}", plobj.sBOMCode);
                        sPRN = sPRN.Replace("{MODELNAME}", plobj.sModelName);
                        sPRN = sPRN.Replace("{MODELDESC}", plobj.sModelType);
                    }
                    DataSet ds = dlobj.dtGetPalletPrintingData(plobj);
                    if (ds.Tables.Count > 0)
                    {
                        DataTable dtPalletData = ds.Tables[0];
                        if (dtPalletData.Rows.Count > 0)
                        {
                            string sBoxCount = Convert.ToString(dtPalletData.Rows.Count);
                            string _sMRP = string.Empty;
                            if (Convert.ToString(plobj.dMRP).Trim().Length > 0)
                            {
                                _sMRP = Convert.ToString(plobj.dMRP * Convert.ToInt32(sBoxCount));
                            }
                            string sFinalBarcodeValueData = string.Empty;
                            sFinalBarcodeValueData = sFinalBarcodeValue(Convert.ToString(dtPalletData.Rows[0]["SECONDARY_BOX_ID"]));
                            sPRN = sPRN.Replace("{MRP}", Convert.ToString(_sMRP));
                            plobj.iLotSize = Convert.ToInt32(sBoxCount);
                            sPRN = sPRN.Replace("{BOXCOUNT}", sBoxCount);
                            sPRN = sPRN.Replace("{PONO}", dtPalletData.Rows[0]["WORK_ORDER_NO"].ToString());
                            sPRN = sPRN.Replace("{NOC}", dtPalletData.Rows[0]["NO_OF_CASE"].ToString());
                            sPRN = sPRN.Replace("{INV_NUMBER}", dtPalletData.Rows[0]["INVOICE_NO"].ToString());
                            sPRN = sPRN.Replace("{INV_DATE}", dtPalletData.Rows[0]["INVOICE_DATE"].ToString());
                            sPRN = sPRN.Replace("{PALLETNUMBER}", dtPalletData.Rows[0]["RUNNING_PALLETNO"].ToString());
                            sPRN = sPRN.Replace("{PALLETID}", dtPalletData.Rows[0]["SECONDARY_BOX_ID"].ToString());
                            sPRN = sPRN.Replace("{PALLETID_BC}", sFinalBarcodeValueData);
                            sPRN = sPRN.Replace("{PALLETWT}", dtPalletData.Rows[0]["BOX_WT"].ToString());
                            sPRN = sPRN.Replace("{PALLETGWT}", dtPalletData.Rows[0]["BOX_GWT"].ToString());
                            sPRN = sPRN.Replace("{PALLETNWT}", dtPalletData.Rows[0]["BOX_NWT"].ToString());
                        }
                        DataTable dtSRNData = new DataTable();
                        DataTable dtBoxData = new DataTable();
                        string sSNBarcode = string.Empty;
                        string sBoxID = string.Empty;
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            dtBoxData = ds.Tables[1].DefaultView.ToTable(true, "BOX_ID");
                            for (int i = 0; i < dtBoxData.Rows.Count; i++)
                            {
                                sSNBarcode = string.Empty;
                                sBoxID = dtBoxData.Rows[i]["BOX_ID"].ToString();
                                DataRow[] dr = ds.Tables[1].Select("BOX_ID = '" + sBoxID + "'");                                
                                for (int j = 0; j < dr.Length; j++)
                                {
                                    sSNBarcode = sSNBarcode + dr[j].ItemArray[2].ToString() + " / ";
                                }
                                sSNBarcode = sSNBarcode.Substring(0, sSNBarcode.Length - 3);
                                sPRN = sPRN.Replace("{RNS_B}" + i, sSNBarcode);
                                sPRN = sPRN.Replace("{BOX_B}" + i, sBoxID);
                            }
                        }
                        if (ds.Tables.Count > 2)
                        {
                            if (ds.Tables[2].Rows.Count > 0)
                            {
                                sPRN = sPRN.Replace("{ADDRESS1}", ds.Tables[2].Rows[0]["ADDRESS1"].ToString());
                                sPRN = sPRN.Replace("{ADDRESS2}", ds.Tables[2].Rows[0]["ADDRESS2"].ToString());
                                sPRN = sPRN.Replace("{ADDRESS3}", ds.Tables[2].Rows[0]["ADDRESS3"].ToString());
                                sPRN = sPRN.Replace("{ADDRESS4}", ds.Tables[2].Rows[0]["ADDRESS4"].ToString());
                                sPRN = sPRN.Replace("{ADDRESS5}", ds.Tables[2].Rows[0]["ADDRESS5"].ToString());
                                sPRN = sPRN.Replace("{ADDRESS6}", ds.Tables[2].Rows[0]["ADDRESS6"].ToString());
                                sPRN = sPRN.Replace("{ADDRESS7}", ds.Tables[2].Rows[0]["ADDRESS7"].ToString());
                            }
                        }                       
                    }
                    for (int i = 0; i < iNoofPrints; i++)
                    {
                        sPRNData = objCommon.sPrintDataLabel(plobj.sPrinterIP, sPRN, plobj.sPalletID.Replace('/', 'P') + "_" + i.ToString(), "Pallet"
                      , plobj.sUserID, plobj.sLineCode
                      );
                    }                  
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
            return sPRNData;
        }
        public string sRPalletPrint(PL_Printing plobj, string sPRN, string sPalletID, int iNoofPrints,string ddlReasonofReprint, string txtRemarks, string _sPrintingType)
        {
            BL_Common objCommon = new BL_Common();
            string sPRNData = string.Empty;
            DL_Common dlobj = new DL_Common();
            try
            {
                plobj.sPalletID = sPalletID;
                DataSet dsPrintingData = GetModelDetails(plobj);
                if (dsPrintingData.Tables.Count > 0)
                {
                    DataTable dtPrintingData = dsPrintingData.Tables[0];
                    if (dtPrintingData.Rows.Count > 0)
                    {
                        plobj.dMRP = Convert.ToDouble(dtPrintingData.Rows[0]["MRP"].ToString());
                        plobj.sModelName = dtPrintingData.Rows[0]["MODEL_CODE"].ToString();
                        plobj.sModelType = dtPrintingData.Rows[0]["MODEL_DESC"].ToString();
                        plobj.sEANNO = dtPrintingData.Rows[0]["EAN_CODE"].ToString();
                        plobj.sBOMCode = dtPrintingData.Rows[0]["BOM_CODE"].ToString();
                        plobj.sModelName = dtPrintingData.Rows[0]["MODEL_CODE"].ToString();
                        sPRN = sPRN.Replace("{MODELNAME}", plobj.sModelName);
                        sPRN = sPRN.Replace("{EAN}", plobj.sEANNO);
                        sPRN = sPRN.Replace("{BOM}", plobj.sBOMCode);
                        sPRN = sPRN.Replace("{MODELNAME}", plobj.sModelName);
                        sPRN = sPRN.Replace("{MODELDESC}", plobj.sModelType);
                    }
                    DataSet ds = dlobj.dtGetPalletPrintingData(plobj);
                    if (ds.Tables.Count > 0)
                    {
                        DataTable dtPalletData = ds.Tables[0];
                        if (dtPalletData.Rows.Count > 0)
                        {
                            string sBoxCount = Convert.ToString(dtPalletData.Rows.Count);
                            string _sMRP = string.Empty;
                            if (Convert.ToString(plobj.dMRP).Trim().Length > 0)
                            {
                                _sMRP = Convert.ToString(plobj.dMRP * Convert.ToInt32(sBoxCount));
                            }
                            string sFinalBarcodeValueData = string.Empty;
                            sFinalBarcodeValueData = sFinalBarcodeValue(Convert.ToString(dtPalletData.Rows[0]["SECONDARY_BOX_ID"]));
                            sPRN = sPRN.Replace("{MRP}", Convert.ToString(_sMRP));
                            plobj.iLotSize = Convert.ToInt32(sBoxCount);
                            sPRN = sPRN.Replace("{BOXCOUNT}", sBoxCount);
                            sPRN = sPRN.Replace("{PONO}", dtPalletData.Rows[0]["WORK_ORDER_NO"].ToString());
                            sPRN = sPRN.Replace("{NOC}", dtPalletData.Rows[0]["NO_OF_CASE"].ToString());
                            sPRN = sPRN.Replace("{INV_NUMBER}", dtPalletData.Rows[0]["INVOICE_NO"].ToString());
                            sPRN = sPRN.Replace("{INV_DATE}", dtPalletData.Rows[0]["INVOICE_DATE"].ToString());
                            sPRN = sPRN.Replace("{PALLETNUMBER}", dtPalletData.Rows[0]["RUNNING_PALLETNO"].ToString());
                            sPRN = sPRN.Replace("{PALLETID}", dtPalletData.Rows[0]["SECONDARY_BOX_ID"].ToString());
                            sPRN = sPRN.Replace("{PALLETID_BC}", sFinalBarcodeValueData);
                            sPRN = sPRN.Replace("{PALLETWT}", dtPalletData.Rows[0]["BOX_WT"].ToString());
                            sPRN = sPRN.Replace("{PALLETGWT}", dtPalletData.Rows[0]["BOX_GWT"].ToString());
                            sPRN = sPRN.Replace("{PALLETNWT}", dtPalletData.Rows[0]["BOX_NWT"].ToString());
                        }
                        DataTable dtSRNData = new DataTable();
                        DataTable dtBoxData = new DataTable();
                        string sSNBarcode = string.Empty;
                        string sBoxID = string.Empty;
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            dtBoxData = ds.Tables[1].DefaultView.ToTable(true, "BOX_ID");
                            for (int i = 0; i < dtBoxData.Rows.Count; i++)
                            {
                                sSNBarcode = string.Empty;
                                sBoxID = dtBoxData.Rows[i]["BOX_ID"].ToString();
                                DataRow[] dr = ds.Tables[1].Select("BOX_ID = '" + sBoxID + "'");
                                for (int j = 0; j < dr.Length; j++)
                                {
                                    sSNBarcode = sSNBarcode + dr[j].ItemArray[2].ToString() + " / ";
                                }
                                sSNBarcode = sSNBarcode.Substring(0, sSNBarcode.Length - 3);
                                sPRN = sPRN.Replace("{RNS_B}" + i, sSNBarcode);
                                sPRN = sPRN.Replace("{BOX_B}" + i, sBoxID);
                            }
                        }
                        if (ds.Tables.Count > 2)
                        {
                            if (ds.Tables[2].Rows.Count > 0)
                            {
                                sPRN = sPRN.Replace("{ADDRESS1}", ds.Tables[2].Rows[0]["ADDRESS1"].ToString());
                                sPRN = sPRN.Replace("{ADDRESS2}", ds.Tables[2].Rows[0]["ADDRESS2"].ToString());
                                sPRN = sPRN.Replace("{ADDRESS3}", ds.Tables[2].Rows[0]["ADDRESS3"].ToString());
                                sPRN = sPRN.Replace("{ADDRESS4}", ds.Tables[2].Rows[0]["ADDRESS4"].ToString());
                                sPRN = sPRN.Replace("{ADDRESS5}", ds.Tables[2].Rows[0]["ADDRESS5"].ToString());
                                sPRN = sPRN.Replace("{ADDRESS6}", ds.Tables[2].Rows[0]["ADDRESS6"].ToString());
                                sPRN = sPRN.Replace("{ADDRESS7}", ds.Tables[2].Rows[0]["ADDRESS7"].ToString());
                            }
                        }
                    }
                    for (int i = 0; i < iNoofPrints; i++)
                    {
                        sPRNData = objCommon.sRPrintDataLabel(plobj.sPrinterIP, sPRN, plobj.sPalletID.Replace('/', 'P') + "_" + i.ToString(), "Pallet"
                      , plobj.sUserID, plobj.sLineCode, ddlReasonofReprint, txtRemarks, _sPrintingType
                      );
                    }
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
            return sPRNData;
        }
    }
}
