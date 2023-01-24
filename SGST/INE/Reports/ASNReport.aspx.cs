using Common;
using System;
using System.Data;
using System.Web.UI;
using ClosedXML.Excel;
using System.IO;
using BusinessLayer.Reports;

namespace SGST.INE.Reports
{
    public partial class ASNReport : Page
    {
        static DataTable dtASNReport = new DataTable();
        string sHeaderValue = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
            Response.AddHeader("Pragma", "no-cache");
            Response.AddHeader("Expires", "0");

            if (Request.QueryString["Name"] != null && Request.QueryString["Name"] != string.Empty)
            {
                sHeaderValue = Request.QueryString["Name"].ToString();
                if (Request.QueryString["Name"].Contains("ASN"))
                {
                    lblHeader.Text = "ASN REPORT";
                }
                else if (Request.QueryString["Name"].Contains("DEVICE"))
                {
                    lblHeader.Text = "DEVICE ACTIVATION REPORT";
                }
                else if (Request.QueryString["Name"].Contains("ADDITIONAL"))
                {
                    lblHeader.Text = "ADDITIONAL DATA REPORT";
                }
                if (Request.QueryString["Name"].Contains("DEVICE") || Request.QueryString["Name"].Contains("ADDITIONAL"))
                {
                    LotNo.Visible = true;
                }
                else
                {
                    LotNo.Visible = false;
                }
                if (Session["usertype"].ToString() != "ADMIN")
                {
                    string _strRights = string.Empty;
                    _strRights = CommonHelper.GetRights(lblHeader.Text.ToUpper(), (DataTable)Session["USER_RIGHTS"]);
                    CommonHelper._strRights = _strRights.Split('^');
                    if (CommonHelper._strRights[0] == "0")  //Check view rights
                    {
                        Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                    }
                }
            }
            try
            {
                if (!Page.IsPostBack)
                {
                    txtPONO.Focus();

                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (lblHeader.Text.StartsWith("ASN"))
                {
                    ShowGridData();
                }
                else if (lblHeader.Text.StartsWith("DEVICE"))
                {
                    ShowDeviceActivationData();
                }
                else if (lblHeader.Text.StartsWith("ADDITIONAL"))
                {
                    ShowAddtionalData();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                if (ex.Message.Contains("System.OutOfMemoryException"))
                {
                    CommonHelper.ShowCustomErrorMessage("System Memory is full, Please try again", msgerror);
                }
                else
                {
                    CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                }
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            dtASNReport = new DataTable();
            txtPONO.Text = string.Empty;
            txtPONO.Focus();
            txtEnterLotNo.Text = string.Empty;
        }

        #region ASN Report

        private void ShowGridData()
        {
            try
            {
                if (txtPONO.Text.Trim() == string.Empty)
                {
                    CommonHelper.ShowCustomErrorMessage("Please enter Invoice no", msgerror);
                    txtPONO.Focus();
                }
                BL_ASNReport blobj = new BL_ASNReport();
                DataTable dtTrackingData = blobj.GetReport(txtPONO.Text);
                if (dtTrackingData.Rows.Count == 0)
                {
                    CommonHelper.ShowCustomErrorMessage("No Data found", msgerror);
                    txtPONO.Focus();
                    txtPONO.Text = string.Empty;
                }
                else
                {
                    string sFileName = txtPONO.Text.Trim() + DateTime.Now.ToString("_dd_MM_yyyy");
                    string sModelMac2sHows = dtTrackingData.Rows[0]["IsASNMac2Required"].ToString();
                    string sModelCode = dtTrackingData.Rows[0]["Physical Device Model"].ToString();
                    DataTable copyDataTable;
                    copyDataTable = dtTrackingData.Copy();
                    copyDataTable.Columns.Remove("IsASNMac2Required");
                    ///ASNReport_WA430223(copyDataTable);

                    //if (sModelCode.Contains("WA430223"))
                    //{
                    //    dtASNReport = ASNReport_WA430223(copyDataTable);                      
                    //    sFileName = txtPONO.Text.Trim() + DateTime.Now.ToString("_dd_MM_yyyy_HH_mm");                       
                    //}
                    //else
                    //{
                    dtASNReport = dtASNExport(copyDataTable, sModelMac2sHows, sModelCode);
                    //}
                    if (drpDownloadMode.Text == "XLS")
                    {
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            //commented on 23-12-2022 Amit
                            //wb.Worksheets.Add(dtASNReport, sFileName);
                            wb.Worksheets.Add(dtASNReport, txtPONO.Text);
                            string myName = Server.UrlEncode(sFileName + ".xlsx");
                            System.IO.MemoryStream stream = GetStream(wb);// The method is defined below
                            Response.Clear();
                            Response.Buffer = true;
                            Response.AddHeader("content-disposition",
                            "attachment; filename=" + myName);
                            Response.ContentType = "application/vnd.ms-excel";
                            Response.BinaryWrite(stream.ToArray());
                            Response.End();
                        }
                    }
                    else
                    {
                        DownLoadExcelFile(dtASNReport, sFileName);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        public DataTable dtASNExport(System.Data.DataTable dt, string sIsASNMac2Required, string sModelCode)
        {
            DataTable dtASNFile = new DataTable();
            try
            {
                dtASNFile = dt.Copy();
                dtASNFile.Rows.Clear();
                dtASNFile.AcceptChanges();
                int colCount = dt.Columns.Count;
                int rowCount = dt.Rows.Count;
                if (sModelCode == "JHSA400")
                {
                    for (int index = 0; index < dt.Rows.Count; index++)
                    {
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            if (i == 0)
                            {
                                dtASNFile.Rows.Add();
                            }
                            dtASNFile.Rows[index][i] = dt.Rows[index][i].ToString();
                            dtASNFile.AcceptChanges();
                            if (dt.Columns[i].ColumnName == "Serial Number")
                            {
                                dtASNFile.Rows[index][i] = dt.Rows[index][i + 1].ToString();
                                dtASNFile.AcceptChanges();
                            }
                            if (dtASNFile.Columns[i].ColumnName.Contains("Base MAC ID"))
                            {
                                string wifi_mac = dt.Rows[index][i + 1].ToString();
                                string MacValue = wifi_mac;
                                int insertedCount = 0;
                                for (int k = 4; k < MacValue.Length; k = k + 4)
                                {
                                    wifi_mac = wifi_mac.Insert(k + insertedCount++, ".");
                                }
                                dtASNFile.Rows[index][i] = wifi_mac;
                                wifi_mac = dt.Rows[index][i].ToString();
                                MacValue = wifi_mac;
                                insertedCount = 0;
                                for (int k = 2; k < MacValue.Length; k = k + 2)
                                {
                                    wifi_mac = wifi_mac.Insert(k + insertedCount++, ":");
                                }
                                dtASNFile.Rows[index][i] = wifi_mac;
                            }
                        }
                    }
                    dt = dtASNFile.Copy();
                }

                else if (sModelCode.Contains("WA430223"))
                {
                    for (int index = 0; index < dt.Rows.Count; index++)
                    {
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            if (i == 0)
                            {
                                dtASNFile.Rows.Add();
                            }
                            dtASNFile.Rows[index][i] = dt.Rows[index][i].ToString();
                            dtASNFile.AcceptChanges();
                            if (i == 2)
                            {
                                dtASNFile.Rows[index][i] = dt.Rows[index][i + 1].ToString();
                            }
                            if (dtASNFile.Columns[i].ColumnName == "Physical Device Type")
                            {
                                if (sModelCode.Contains("WA430223"))
                                {
                                    dt.Rows[index][i] = "Wi-Fi AP";
                                }
                                else
                                {
                                    dt.Rows[index][i] = "HOME GATEWAY";
                                }

                            }

                            if (dt.Columns[i].ColumnName == "Serial Number")
                            {
                                dtASNFile.Rows[index][i] = dt.Rows[index][i + 1].ToString();
                                dtASNFile.AcceptChanges();
                            }

                            if (dtASNFile.Columns[i].ColumnName.Contains("Base MAC ID"))
                            {
                                string wifi_mac = dt.Rows[index][i + 1].ToString();
                                string MacValue = wifi_mac;
                                int insertedCount = 0;
                                for (int k = 4; k < MacValue.Length; k = k + 4)
                                {
                                    wifi_mac = wifi_mac.Insert(k + insertedCount++, ".");
                                }
                                dtASNFile.Rows[index][i] = wifi_mac;
                                wifi_mac = dt.Rows[index][i].ToString();
                                MacValue = wifi_mac;
                                insertedCount = 0;
                                if (sModelCode.Contains("WA430223"))
                                {
                                    for (int k = 2; k < MacValue.Length; k = k + 2)
                                    {
                                        wifi_mac = wifi_mac.Insert(k + insertedCount++, ":");
                                    }
                                }
                                else
                                {
                                    for (int k = 4; k < MacValue.Length; k = k + 4)
                                    {
                                        wifi_mac = wifi_mac.Insert(k + insertedCount++, ".");
                                    }
                                }
                                dt.Rows[index][i] = wifi_mac;
                            }
                            if (dtASNFile.Columns[i].ColumnName == "Global SSID1")
                            {
                                dt.Rows[index][i] = "";
                            }
                            if (dtASNFile.Columns[i].ColumnName.Contains("WPA PSK"))
                            {
                                dt.Rows[index][i] = "";
                            }
                            if (dtASNFile.Columns[i].ColumnName.Contains("GPON ID"))
                            {
                                dt.Rows[index][i] = "";
                            }
                        }
                    }
                }

                // Added By Shivam (20/12/2022)

                else
                {
                    for (int index = 0; index < dt.Rows.Count; index++)
                    {
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            if (i == 0)
                            {
                                dtASNFile.Rows.Add();
                            }
                            dtASNFile.Rows[index][i] = dt.Rows[index][i].ToString();
                            dtASNFile.AcceptChanges();
                            if (i == 2)
                            {
                                dtASNFile.Rows[index][i] = dt.Rows[index][i + 1].ToString();
                            }
                            if (dtASNFile.Columns[i].ColumnName == "Physical Device Type")
                            {
                                dtASNFile.Rows[index][i] = "HOME GATEWAY";
                            }
                            if (dtASNFile.Columns[i].ColumnName.Contains("Base MAC ID"))
                            {
                                string wifi_mac = dt.Rows[index][i + 1].ToString();
                                string MacValue = wifi_mac;
                                int insertedCount = 0;
                                for (int k = 4; k < MacValue.Length; k = k + 4)
                                {
                                    wifi_mac = wifi_mac.Insert(k + insertedCount++, ".");
                                }
                                dtASNFile.Rows[index][i] = wifi_mac;
                                wifi_mac = dt.Rows[index][i].ToString();
                                MacValue = wifi_mac;
                                insertedCount = 0;
                                if (sModelCode.Contains("400"))
                                {
                                    for (int k = 2; k < MacValue.Length; k = k + 2)
                                    {
                                        wifi_mac = wifi_mac.Insert(k + insertedCount++, ":");
                                    }
                                }
                                else
                                {
                                    for (int k = 4; k < MacValue.Length; k = k + 4)
                                    {
                                        wifi_mac = wifi_mac.Insert(k + insertedCount++, ".");
                                    }
                                }
                                dt.Rows[index][i] = wifi_mac;
                            }
                            if (dtASNFile.Columns[i].ColumnName == "Global SSID1")
                            {
                                dt.Rows[index][i] = "";
                            }
                            if (dtASNFile.Columns[i].ColumnName.Contains("WPA PSK"))
                            {
                                dt.Rows[index][i] = "";
                            }
                            if (dtASNFile.Columns[i].ColumnName.Contains("GPON ID"))
                            {
                                dt.Rows[index][i] = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {

            }

            if (sIsASNMac2Required == "1" || sIsASNMac2Required == "True")
            {
                dt.Merge(dtASNFile);
            }
            if (sModelCode == "WA430223")
            {
                dt.Columns.RemoveAt(2);
                dt.AcceptChanges();

            }
            else
            {
                dt.Columns.RemoveAt(3);
                dt.AcceptChanges();
            }
            dt.Columns.Remove("MAC_2");
            dt.AcceptChanges();

            DataView dv = new DataView();
            dv = dt.DefaultView;
            dv.Sort = "FPD_ID";
            DataTable sortedDT = new DataTable();
            sortedDT = dv.ToTable();
            sortedDT.Columns.Remove("FPD_ID");
            sortedDT.AcceptChanges();
            return sortedDT;
        }

        public DataTable ASNReport_WA430223(System.Data.DataTable dt)
        {
            DataTable dtASNFile = new DataTable();
            try
            {
                dtASNFile = dt.Copy();
                dtASNFile.Columns["Purchasing Document Number"].SetOrdinal(0);
                dtASNFile.Columns["EAN Code of Device"].SetOrdinal(1);
                dtASNFile.Columns["Serial Number1"].SetOrdinal(2);
                dtASNFile.Columns["Pallet Number"].SetOrdinal(3);
                dtASNFile.Columns["Master Shipper Number"].SetOrdinal(4);
                dtASNFile.Columns["Vendor Lot Number"].SetOrdinal(5);
                dtASNFile.Columns["Invoice Number"].SetOrdinal(6);
                dtASNFile.Columns["Month of Manufacturing (YYYYMM)"].SetOrdinal(7);
                dtASNFile.Columns["Invoice Date"].SetOrdinal(8);
                dtASNFile.Columns["Warranty in Days"].SetOrdinal(9);
                dtASNFile.Columns["MAC"].SetOrdinal(10);
                dtASNFile.Columns["Physical Device Model"].SetOrdinal(11);
                dtASNFile.Columns["Physical Device Type"].SetOrdinal(12);
                dtASNFile.Columns["Physical  Device Vendor"].SetOrdinal(13);
                dtASNFile.Columns["Device IMEI number"].SetOrdinal(14);
                dtASNFile.Columns["OUI"].SetOrdinal(15);
                dtASNFile.Columns.Add("IS device a part of combination");
                dtASNFile.Columns["IS device a part of combination"].SetOrdinal(16);
                dtASNFile.Columns.Add("IS device a part of combination");
                dtASNFile.Columns["Physical  Device Vendor"].SetOrdinal(17);
                dtASNFile.Columns["Device IMEI number"].SetOrdinal(18);


                dtASNFile.Rows.Clear();
                dtASNFile.AcceptChanges();
                int colCount = dt.Columns.Count;
                int rowCount = dt.Rows.Count;
                for (int index = 0; index < dt.Rows.Count; index++)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (i == 0)
                        {
                            dtASNFile.Rows.Add();
                        }
                        dtASNFile.Rows[index][i] = dt.Rows[index][i].ToString();
                        dtASNFile.AcceptChanges();
                        if (dt.Columns[i].ColumnName == "Serial Number")
                        {
                            dtASNFile.Rows[index][i] = dt.Rows[index][i + 1].ToString();
                            dtASNFile.AcceptChanges();
                        }
                        if (dtASNFile.Columns[i].ColumnName == "Physical Device Type")
                        {
                            dtASNFile.Rows[index][i] = "HOME GATEWAY";
                        }
                        if (dtASNFile.Columns[i].ColumnName.Contains("Base MAC ID"))
                        {
                            string wifi_mac = dt.Rows[index][i + 1].ToString();
                            string MacValue = wifi_mac;
                            int insertedCount = 0;

                            wifi_mac = dt.Rows[index][i].ToString();
                            MacValue = wifi_mac;
                            insertedCount = 0;
                            for (int k = 2; k < MacValue.Length; k = k + 2)
                            {
                                wifi_mac = wifi_mac.Insert(k + insertedCount++, ":");
                            }
                            dt.Rows[index][i] = wifi_mac;
                        }
                        if (dtASNFile.Columns[i].ColumnName == "Global SSID1")
                        {
                            dt.Rows[index][i] = "";
                        }
                        if (dtASNFile.Columns[i].ColumnName.Contains("WPA PSK"))
                        {
                            dt.Rows[index][i] = "";
                        }
                        if (dtASNFile.Columns[i].ColumnName.Contains("GPON ID"))
                        {
                            dt.Rows[index][i] = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {

            }
            dt.Merge(dtASNFile);
            dt.Columns.RemoveAt(3);
            dt.AcceptChanges();
            dt.Columns.Remove("MAC_2");
            dt.AcceptChanges();
            dt.Columns.Remove("MAC");
            dt.AcceptChanges();
            DataView dv = dt.DefaultView;
            dv.Sort = "FPD_ID";
            DataTable sortedDT = dv.ToTable();
            sortedDT.Columns.Remove("FPD_ID");
            sortedDT.AcceptChanges();
            return sortedDT;
        }


        #endregion




        #region Device Activation Report
        private void ShowDeviceActivationData()
        {
            try
            {
                if (txtPONO.Text.Trim() == string.Empty)
                {
                    CommonHelper.ShowCustomErrorMessage("Please enter Invoice no", msgerror);
                    txtPONO.Focus();
                }
                if (txtEnterLotNo.Text == string.Empty)
                {
                    CommonHelper.ShowCustomErrorMessage("Please enter lot no", msgerror);
                    txtEnterLotNo.Focus();
                }
                BL_ASNReport blobj = new BL_ASNReport();
                DataTable dtTrackingData = blobj.GetDeviceActivationReport(txtPONO.Text);
                if (dtTrackingData.Rows.Count == 0)
                {
                    CommonHelper.ShowCustomErrorMessage("No Data found", msgerror);
                    txtPONO.Focus();
                    txtPONO.Text = string.Empty;
                }
                else
                {
                    string sFileName = txtEnterLotNo.Text + "_Device_Activation_Template" + DateTime.Now.ToString("_dd_MM_yyyy_HH_mm_") + dtTrackingData.Rows.Count.ToString();
                    if (drpDownloadMode.Text == "XLS")
                    {
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            wb.Worksheets.Add(dtTrackingData, txtEnterLotNo.Text + "_Device_Activation_Template");
                            string myName = Server.UrlEncode(sFileName + ".xlsx");
                            System.IO.MemoryStream stream = GetStream(wb);// The method is defined below
                            Response.Clear();
                            Response.Buffer = true;
                            Response.AddHeader("content-disposition",
                            "attachment; filename=" + myName);
                            Response.ContentType = "application/vnd.ms-excel";
                            Response.BinaryWrite(stream.ToArray());
                            Response.End();
                        }
                    }
                    else
                    {
                        DownLoadExcelFile(dtASNReport, sFileName);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }

        #endregion

        #region Additional Data Report

        private void ShowAddtionalData()
        {
            try
            {
                if (txtPONO.Text.Trim() == string.Empty)
                {
                    CommonHelper.ShowCustomErrorMessage("Please enter Invoice no", msgerror);
                    txtPONO.Focus();
                }
                BL_ASNReport blobj = new BL_ASNReport();
                DataTable dtTrackingData = blobj.GetAdditionalReport(txtPONO.Text);
                if (dtTrackingData.Rows.Count == 0)
                {
                    CommonHelper.ShowCustomErrorMessage("No Data found", msgerror);
                    txtPONO.Focus();
                    txtPONO.Text = string.Empty;
                }
                else
                {
                    string sFileName = txtEnterLotNo.Text + "_Additional_Data" + DateTime.Now.ToString("_dd_MM_yyyy_HH_mm_") + dtTrackingData.Rows.Count.ToString();
                    DataTable copyDataTable;
                    copyDataTable = dtTrackingData.Copy();
                    dtASNReport = dtGetAdditionaldataExport(copyDataTable);
                    if (drpDownloadMode.Text == "XLS")
                    {
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            wb.Worksheets.Add(dtASNReport, txtEnterLotNo.Text + "_Additional_Data");
                            string myName = Server.UrlEncode(sFileName + ".xlsx");
                            System.IO.MemoryStream stream = GetStream(wb);// The method is defined below
                            Response.Clear();
                            Response.Buffer = true;
                            Response.AddHeader("content-disposition",
                            "attachment; filename=" + myName);
                            Response.ContentType = "application/vnd.ms-excel";
                            Response.BinaryWrite(stream.ToArray());
                            Response.End();
                        }
                    }
                    else
                    {
                        DownLoadExcelFile(dtASNReport, sFileName);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }

        public DataTable dtGetAdditionaldataExport(System.Data.DataTable dt)
        {
            DataTable dtASNFile = new DataTable();
            try
            {
                dtASNFile = dt.Copy();
                dtASNFile.Rows.Clear();
                dtASNFile.AcceptChanges();
                string sBlankData = string.Empty;
                int colCount = dt.Columns.Count;
                int rowCount = dt.Rows.Count;
                for (int index = 0; index < dt.Rows.Count; index++)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        sBlankData = string.Empty;
                        if (i == 0)
                        {
                            dtASNFile.Rows.Add();
                        }
                        dtASNFile.Rows[index][i] = dt.Rows[index][i].ToString();
                        dtASNFile.AcceptChanges();
                        if (dt.Columns[i].ColumnName == "Wi-Fi Mac")
                        {
                            string wifi_mac = dt.Rows[index][i].ToString();
                            string MacValue = wifi_mac;
                            int insertedCount = 0;
                            for (int k = 2; k < MacValue.Length; k = k + 2)
                            {
                                wifi_mac = wifi_mac.Insert(k + insertedCount++, ":");
                            }
                            dtASNFile.Rows[index][i] = wifi_mac;
                            dtASNFile.AcceptChanges();
                        }
                        if (dt.Columns[i].ColumnName == "ManufacturerID")
                        {
                            string sData = dt.Rows[index][i].ToString();
                            string[] VMXData = sData.Split(',');
                            if (VMXData.Length > 0)
                            {
                                if (VMXData.Length > 3)
                                {
                                    sBlankData = VMXData[2];
                                }
                            }
                            dtASNFile.Rows[index][i] = sBlankData;
                            dtASNFile.AcceptChanges();
                        }
                        if (dt.Columns[i].ColumnName == "ProviderID")
                        {
                            string sData = dt.Rows[index][i].ToString();
                            string[] VMXData = sData.Split(',');
                            if (VMXData.Length > 0)
                            {
                                if (VMXData.Length > 4)
                                {
                                    sBlankData = VMXData[3];
                                }
                            }
                            dtASNFile.Rows[index][i] = sBlankData;
                            dtASNFile.AcceptChanges();
                        }
                        if (dt.Columns[i].ColumnName == "SmartCardNumber")
                        {
                            string sData = dt.Rows[index][i].ToString();
                            string[] VMXData = sData.Split(',');
                            if (VMXData.Length > 0)
                            {
                                if (VMXData.Length > 5)
                                {
                                    sBlankData = VMXData[4];
                                }
                            }
                            dtASNFile.Rows[index][i] = sBlankData;
                            dtASNFile.AcceptChanges();
                        }
                        if (dt.Columns[i].ColumnName == "ChipId")
                        {
                            string sData = dt.Rows[index][i].ToString();
                            string[] VMXData = sData.Split(',');
                            if (VMXData.Length > 0)
                            {
                                sBlankData = VMXData[0];
                            }
                            dtASNFile.Rows[index][i] = sBlankData;
                            dtASNFile.AcceptChanges();
                        }
                        if (dt.Columns[i].ColumnName == "BurnedResult")
                        {
                            string sData = dt.Rows[index][i].ToString();
                            string[] VMXData = sData.Split(',');
                            if (VMXData.Length > 0)
                            {
                                if (VMXData.Length == 6)
                                {
                                    sBlankData = VMXData[5];
                                }
                            }
                            dtASNFile.Rows[index][i] = sBlankData;
                            dtASNFile.AcceptChanges();
                        }
                    }
                }
                dt = dtASNFile.Copy();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {

            }
            return dt;
        }

        #endregion


        public MemoryStream GetStream(XLWorkbook excelWorkbook)
        {
            MemoryStream fs = new MemoryStream();
            excelWorkbook.SaveAs(fs);
            fs.Position = 0;
            return fs;
        }
        public void DownLoadExcelFile(DataTable dtTrackingData, string sFileName)
        {
            try
            {
                Response.Clear();
                string attachment = "attachment; filename=" + sFileName + ".csv";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.Charset = "";
                Response.ContentType = "application/text";
                string csv = string.Empty;
                foreach (DataColumn column in dtTrackingData.Columns)
                {
                    csv += column.ColumnName + ',';
                }
                //Add new line.
                csv += "\r\n";
                foreach (DataRow row in dtTrackingData.Rows)
                {
                    foreach (DataColumn column in dtTrackingData.Columns)
                    {
                        //Add the Data rows.
                        csv += row[column.ColumnName].ToString().Replace(",", ";") + ',';
                    }
                    //Add new line.
                    csv += "\r\n";
                }
                Response.Output.Write(csv);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                if (ex.Message.Contains("System.OutOfMemoryException"))
                {
                    CommonHelper.ShowCustomErrorMessage("System Memory is full, Please select different dates", msgerror);
                }
                else
                {
                    CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                }
            }
        }
    }
}