using Common;
using System;
using System.Configuration;
using System.IO;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using PL;
using System.Web.UI;
using System.Collections;
using System.Linq;
using BcilLib;
using System.Reflection;

namespace SGST.INE.Masters
{
    public partial class IMEI_UPLOAD : System.Web.UI.Page
    {
        BL_IMEIUpload blobj = new BL_IMEIUpload();
        ArrayList arr = new ArrayList();
        ArrayList arrUpload = new ArrayList();
        string strDuplicate = string.Empty;
        DataTable dtDuplicateDataColumn = new DataTable();
        string detailsofModelName = "";
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Signin/v1/Login.aspx?Session=Null");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
                Response.AddHeader("Pragma", "no-cache");
                Response.AddHeader("Expires", "0");
                if (Session["usertype"].ToString() != "ADMIN")
                {
                    string _strRights = CommonHelper.GetRights("IMEI UPLOAD", (DataTable)Session["USER_RIGHTS"]);
                    CommonHelper._strRights = _strRights.Split('^');
                    if (CommonHelper._strRights[0] == "0")  //Check view rights
                    {
                        Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                    }
                }
                if (!IsPostBack)
                {
                    Get_ModelName();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        public void Get_ModelName()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                ddlModel_Name.Items.Clear();
                lblNumberofRecords.Text = string.Empty;
                gvExcelFile.DataSource = null;
                gvExcelFile.DataBind();
                blobj = new BL_IMEIUpload();
                string sResult = string.Empty;
                System.Data.DataTable dt = blobj.dtBindModel();
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillMultiColumnsCombo(ddlModel_Name, dt, true);
                    ddlModel_Name.SelectedIndex = 0;
                    ddlModel_Name.Focus();
                    return;
                }
                else
                {
                    CommonHelper.ShowMessage("No Model Name Found", msgerror, CommonHelper.MessageType.Error.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        public string UploadCsvData_HalfFile(DataTable dtIMEI) // Upload CSV File and convert into an array 
        {
            string sResult = string.Empty;
            try
            {
                arr = new ArrayList();
                arr.Clear();
                for (int i = 0; i < dtIMEI.Rows.Count; i++)
                {
                    if (dtIMEI.Rows[i]["SERIAL_NUMBER"].ToString().Length > 0)
                    {
                        arr.Add(dtIMEI.Rows[i]["SERIAL_NUMBER"].ToString());
                    }
                    if (dtIMEI.Rows[i]["MAC"].ToString().Length > 0)
                    {
                        arr.Add(dtIMEI.Rows[i]["MAC"].ToString());
                    }
                    for (int j = 0; j < dtDuplicateDataColumn.Rows.Count; j++)
                    {
                        if (dtDuplicateDataColumn.Rows[j][0].ToString() == "GRPONSN")
                        {
                            if (dtIMEI.Rows[i]["ARC/Gpon  SN"].ToString().Length > 0)
                            {
                                arr.Add(dtIMEI.Rows[i]["ARC/Gpon  SN"].ToString());
                            }
                        }
                        if (dtDuplicateDataColumn.Rows[j][0].ToString() == "KEY_PART_NO")
                        {
                            if (dtIMEI.Rows[i]["KEY_PART_NO"].ToString().Length > 0)
                            {
                                arr.Add(dtIMEI.Rows[i]["KEY_PART_NO"].ToString());
                            }
                        }
                        if (dtDuplicateDataColumn.Rows[j][0].ToString() == "Wifi_MAC")
                        {
                            if (dtIMEI.Rows[i]["Wifi key"].ToString().Length > 0)
                            {
                                arr.Add(dtIMEI.Rows[i]["Wifi key"].ToString());
                            }
                        }
                        if (dtDuplicateDataColumn.Rows[j][0].ToString() == "Wireless_SSID")
                        {
                            if (dtIMEI.Rows[i]["Wireless SSID"].ToString().Length > 0)
                            {
                                arr.Add(dtIMEI.Rows[i]["Wireless SSID"].ToString());
                            }
                        }
                        if (dtDuplicateDataColumn.Rows[j][0].ToString() == "Pre_password")
                        {
                            if (dtIMEI.Rows[i]["Pre-password"].ToString().Length > 0)
                            {
                                arr.Add(dtIMEI.Rows[i]["Pre-password"].ToString());
                            }
                        }
                        if (dtDuplicateDataColumn.Rows[j][0].ToString() == "ACS_DATA")
                        {
                            if (dtIMEI.Rows[i]["ACS_DATA"].ToString().Length > 0)
                            {
                                arr.Add(dtIMEI.Rows[i]["ACS_DATA"].ToString());
                            }
                        }
                        if (dtDuplicateDataColumn.Rows[j][0].ToString() == "HDCP_FILE_NAME")
                        {
                            if (dtIMEI.Rows[i]["HDCP_FILE_NAME"].ToString().Length > 0)
                            {
                                arr.Add(dtIMEI.Rows[i]["HDCP_FILE_NAME"].ToString());
                            }
                        }
                        if (dtDuplicateDataColumn.Rows[j][0].ToString() == "COL1")
                        {
                            if (dtIMEI.Rows[i]["COL1"].ToString().Length > 0)
                            {
                                arr.Add(dtIMEI.Rows[i]["COL1"].ToString());
                            }
                        }
                        if (dtDuplicateDataColumn.Rows[j][0].ToString() == "COL2")
                        {
                            if (dtIMEI.Rows[i]["COL2"].ToString().Length > 0)
                            {
                                arr.Add(dtIMEI.Rows[i]["COL2"].ToString());
                            }
                        }
                        if (dtDuplicateDataColumn.Rows[j][0].ToString() == "COL3")
                        {
                            if (dtIMEI.Rows[i]["COL3"].ToString().Length > 0)
                            {
                                arr.Add(dtIMEI.Rows[i]["COL3"].ToString());
                            }
                        }
                        if (dtDuplicateDataColumn.Rows[j][0].ToString() == "COL4")
                        {
                            if (dtIMEI.Rows[i]["COL4"].ToString().Length > 0)
                            {
                                arr.Add(dtIMEI.Rows[i]["COL4"].ToString());
                            }
                        }
                        if (dtDuplicateDataColumn.Rows[j][0].ToString() == "COL5")
                        {
                            if (dtIMEI.Rows[i]["COL5"].ToString().Length > 0)
                            {
                                arr.Add(dtIMEI.Rows[i]["COL5"].ToString());
                            }
                        }
                        if (dtDuplicateDataColumn.Rows[j][0].ToString() == "COL6")
                        {
                            if (dtIMEI.Rows[i]["COL6"].ToString().Length > 0)
                            {
                                arr.Add(dtIMEI.Rows[i]["COL6"].ToString());
                            }
                        }
                        if (dtDuplicateDataColumn.Rows[j][0].ToString() == "COL7")
                        {
                            if (dtIMEI.Rows[i]["COL7"].ToString().Length > 0)
                            {
                                arr.Add(dtIMEI.Rows[i]["COL7"].ToString());
                            }
                        }
                        if (dtDuplicateDataColumn.Rows[j][0].ToString() == "COL8")
                        {
                            if (dtIMEI.Rows[i]["COL8"].ToString().Length > 0)
                            {
                                arr.Add(dtIMEI.Rows[i]["COL8"].ToString());
                            }
                        }
                        if (dtDuplicateDataColumn.Rows[j][0].ToString() == "COL9")
                        {
                            if (dtIMEI.Rows[i]["COL9"].ToString().Length > 0)
                            {
                                arr.Add(dtIMEI.Rows[i]["COL9"].ToString());
                            }
                        }
                    }
                }
                string sDuplicate = string.Empty;
                var duplicates =
               (from string item in arr select item).GroupBy(s => s).Select(
                   group => new { Word = group.Key, Count = group.Count() }).Where(x => x.Count >= 2);
                foreach (var group in duplicates)
                {
                    sDuplicate += group + Environment.NewLine;
                }
                if (!string.IsNullOrEmpty(sDuplicate))
                {
                    PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtInfo, MethodBase.GetCurrentMethod().Name, "Duplicate record Found in csv file for model = " + ddlModel_Name.Text + "" + Environment.NewLine + sDuplicate);
                    return sResult = "Duplicate" + sDuplicate;
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtInfo, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return sResult;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                blobj = new BL_IMEIUpload();
                PL_IMEIMaster plobj = new PL_IMEIMaster();
                DataTable dtFileUploading = new DataTable();
                if (ddlModel_Name.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select model name.", msgerror, CommonHelper.MessageType.Error.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    ddlModel_Name.Focus();
                    return;
                }
                string sProgramID = string.Empty;
                if (Path.GetExtension(FileUpload1.FileName).Equals(".csv"))
                {
                    plobj.sModelName = ddlModel_Name.SelectedValue.ToString();
                    DataTable dt = new DataTable();
                    DataSet ds = blobj.dtBindModelDetails(plobj);
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
                    if (ds.Tables.Count == 2)
                    {
                        dtDuplicateDataColumn = new DataTable();
                        dtDuplicateDataColumn = ds.Tables[1];
                    }
                    DirectoryInfo _dir = null;
                    string CSVFilePath = Path.GetFileName(FileUpload1.FileName);    //getting full file path of Uploaded file  
                    string sUploadFilePath = ConfigurationManager.AppSettings["Upload_File_Path"].ToString();
                    string sPath = Server.MapPath("~/" + sUploadFilePath + "//");
                    _dir = new DirectoryInfo(sPath);
                    if (_dir.Exists == false)
                    {
                        _dir.Create();
                        Directory.CreateDirectory(_dir.ToString());
                    }
                    CSVFilePath = CSVFilePath + DateTime.Now.ToString("ddyymmhhMMsstt");
                    FileUpload1.SaveAs(Server.MapPath("~/Upload_File//") + CSVFilePath);
                    string sFileName = Server.MapPath("~/Upload_File//") + CSVFilePath;

                    dtFileUploading = PCommon.ConvertCSVtoDataTable(sFileName);
                    if (!dtFileUploading.Columns.Contains("ARC/Gpon  SN"))
                    {
                        dtFileUploading.Columns.Add("ARC/Gpon  SN");
                    }
                    if (!dtFileUploading.Columns.Contains("KEY_PART_NO"))
                    {
                        dtFileUploading.Columns.Add("KEY_PART_NO");
                    }
                    if (!dtFileUploading.Columns.Contains("Wifi key"))
                    {
                        dtFileUploading.Columns.Add("Wifi key");
                    }
                    if (!dtFileUploading.Columns.Contains("Wireless SSID"))
                    {
                        dtFileUploading.Columns.Add("Wireless SSID");
                    }
                    if (!dtFileUploading.Columns.Contains("Pre-password"))
                    {
                        dtFileUploading.Columns.Add("Pre-password");
                    }
                    if (!dtFileUploading.Columns.Contains("ACS_DATA"))
                    {
                        dtFileUploading.Columns.Add("ACS_DATA");
                    }
                    if (!dtFileUploading.Columns.Contains("HDCP_FILE_NAME"))
                    {
                        dtFileUploading.Columns.Add("HDCP_FILE_NAME");
                    }
                    if (!dtFileUploading.Columns.Contains("COL1"))
                    {
                        dtFileUploading.Columns.Add("COL1");
                    }
                    if (!dtFileUploading.Columns.Contains("COL2"))
                    {
                        dtFileUploading.Columns.Add("COL2");
                    }
                    if (!dtFileUploading.Columns.Contains("COL3"))
                    {
                        dtFileUploading.Columns.Add("COL3");
                    }
                    if (!dtFileUploading.Columns.Contains("COL4"))
                    {
                        dtFileUploading.Columns.Add("COL4");
                    }
                    if (!dtFileUploading.Columns.Contains("COL5"))
                    {
                        dtFileUploading.Columns.Add("COL5");
                    }
                    if (!dtFileUploading.Columns.Contains("COL6"))
                    {
                        dtFileUploading.Columns.Add("COL6");
                    }
                    if (!dtFileUploading.Columns.Contains("COL7"))
                    {
                        dtFileUploading.Columns.Add("COL7");
                    }
                    if (!dtFileUploading.Columns.Contains("COL8"))
                    {
                        dtFileUploading.Columns.Add("COL8");
                    }
                    if (!dtFileUploading.Columns.Contains("COL9"))
                    {
                        dtFileUploading.Columns.Add("COL9");
                    }
                    dtFileUploading.AcceptChanges();
                    if (dtFileUploading.Columns.Count != 18)
                    {
                        CommonHelper.ShowMessage("Invalid file column no not matched", msgerror, CommonHelper.MessageType.Error.ToString());
                        gvExcelFile.DataSource = null;
                        gvExcelFile.DataBind();
                        return;
                    }
                    DataTable dtIMEI = dtFileUploading; //PCommon.ConvertCSVtoDataTable(sFileName);
                    string sResult = UploadCsvData_HalfFile(dtIMEI);
                    if (sResult.StartsWith("Duplicate"))
                    {
                        CommonHelper.ShowMessage("Duplicate Data found : " + sResult, msgerror, CommonHelper.MessageType.Error.ToString());
                        ResetDetails();
                        return;
                    }
                    if (sResult.StartsWith("Blank"))
                    {
                        CommonHelper.ShowMessage("Some IMEI Contains blank, Kindly check your file and remove blank IMEI from list", msgerror, CommonHelper.MessageType.Error.ToString());
                        ResetDetails();
                        return;
                    }
                    DataTable dtDuplicate = PCommon.ConvertArrayDataTableSingleColumn(arr);
                    plobj.dtIMEIUpload = dtDuplicate;
                    plobj.sModelName = ddlModel_Name.SelectedItem.Text.Trim();
                    plobj.sSiteCode = Session["SiteCode"].ToString();
                    plobj.sUploadedby = Session["UserID"].ToString();
                    plobj.sLineCode = Session["LineCode"].ToString();
                    plobj.sModelName = ddlModel_Name.SelectedValue.ToString();
                    DataTable dtModelNameDetails = blobj.dtCheckDuplicate(plobj);
                    if (dtModelNameDetails.Rows.Count == 0)
                    {
                        DataTable dtHexaMac = dtGetHexaMacValue(dtIMEI, Convert.ToInt32(lblNoOfMacAddress.Text));
                        var duplicates = dtHexaMac.AsEnumerable().GroupBy(r => r[2]).Where(gr => gr.Count() > 1).ToList();
                        if (duplicates.Any())
                        {
                            CommonHelper.ShowMessage("Displayed Data found duplicate, "
                                + String.Join(", ", duplicates.Select(dupl => dupl.Key))
                                , msgerror, CommonHelper.MessageType.Error.ToString());
                            gvExcelFile.DataSource = dtIMEI;
                            gvExcelFile.DataBind();
                            return;
                        }
                        dtFileUploading.Columns.Add("IMEI1");
                        dtFileUploading.Columns["SERIAL_NUMBER"].SetOrdinal(0);
                        dtFileUploading.Columns["ARC/Gpon  SN"].SetOrdinal(1);
                        dtFileUploading.Columns["MAC"].SetOrdinal(2);
                        dtFileUploading.Columns.Add("MAC2").SetOrdinal(3);
                        dtFileUploading.AcceptChanges();
                        dtFileUploading.Columns["KEY_PART_NO"].SetOrdinal(4);
                        dtFileUploading.Columns["Wifi key"].SetOrdinal(5);
                        dtFileUploading.Columns["Wireless SSID"].SetOrdinal(6);
                        dtFileUploading.Columns["Pre-password"].SetOrdinal(7);
                        dtFileUploading.Columns["IMEI1"].SetOrdinal(8);
                        dtFileUploading.Columns["ACS_DATA"].SetOrdinal(9);
                        dtFileUploading.Columns["HDCP_FILE_NAME"].SetOrdinal(10);
                        dtFileUploading.Columns["COL1"].SetOrdinal(11);
                        dtFileUploading.Columns["COL2"].SetOrdinal(12);
                        dtFileUploading.Columns["COL3"].SetOrdinal(13);
                        dtFileUploading.Columns["COL4"].SetOrdinal(14);
                        dtFileUploading.Columns["COL5"].SetOrdinal(15);
                        dtFileUploading.Columns["COL6"].SetOrdinal(16);
                        dtFileUploading.Columns["COL7"].SetOrdinal(17);
                        dtFileUploading.Columns["COL8"].SetOrdinal(18);
                        dtFileUploading.Columns["COL9"].SetOrdinal(19);
                        dtFileUploading.AcceptChanges();
                        plobj.dtIMEIUpload = dtFileUploading;
                        plobj.dtHexaMac = dtHexaMac;
                        plobj.sColorCode = "";
                        
                        string Message = string.Empty;
                        string sOutput = blobj.blUploadIMEIDataByDataTable(plobj);
                        if (sOutput.ToUpper().StartsWith("OKAY"))
                        {
                            Message = sOutput.Split('~')[1];
                            CommonHelper.ShowMessage(Message, msgsuccess, CommonHelper.MessageType.Success.ToString());
                            ResetDetails();
                            return;
                        }
                        else if (sOutput.StartsWith("N~"))
                        {
                            Message = sOutput.Split('~')[1];
                            CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                            return;
                        }
                        else if (sOutput.StartsWith("ERROR~"))
                        {
                            if (sOutput.Contains("PK_mSNHexaData"))
                            {
                                Message = sOutput.Split('~')[1].Split('(')[1];
                                CommonHelper.ShowMessage("Duplicate Mac address found: " + Message, msgerror, CommonHelper.MessageType.Error.ToString());
                            }
                            else
                            {
                                Message = sOutput.Split('~')[1];
                                CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                            }
                        }
                        else if (sOutput.StartsWith("NOTFOUND~"))
                        {
                            Message = sOutput.Split('~')[1];
                            CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                        }
                    }
                    else
                    {
                        CommonHelper.ShowMessage("Displayed Data found duplicate, Please remove duplicate records", msgerror, CommonHelper.MessageType.Error.ToString());
                        //DataView dv = new DataView(dtDuplicate);                   
                        gvExcelFile.DataSource = dtModelNameDetails;
                        gvExcelFile.DataBind();
                    }
                }
                else
                {
                    CommonHelper.ShowMessage("Please select valid file", msgerror, CommonHelper.MessageType.Error.ToString());
                    gvExcelFile.DataSource = dtFileUploading;
                    gvExcelFile.DataBind();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate MAC"))
                {
                    CommonHelper.ShowCustomErrorMessage("Mac address already exist :" + ex.Message.ToString(), msgerror);
                }
                else
                {
                    CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                }
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void gvExcelFile_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (GridViewRow row in gvExcelFile.Rows)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    var array = detailsofModelName.Split(',');
                    foreach (string r in array)
                    {
                        if (r != "")
                        {
                            if (e.Row.Cells[1].Text.Contains(r))
                            {
                                e.Row.Cells[1].ForeColor = System.Drawing.Color.Red;
                            }
                        }
                    }
                }
            }
        }
        protected void ddlModel_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetDetails();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        public void GetDetails()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (ddlModel_Name.SelectedIndex > 0)
                {
                    lblNumberofRecords.Text = string.Empty;
                    gvExcelFile.DataSource = null;
                    gvExcelFile.DataBind();
                    blobj = new BL_IMEIUpload();
                    PL_IMEIMaster plobj = new PL_IMEIMaster();
                    plobj.sModelName = ddlModel_Name.SelectedValue.ToString();
                    DataTable dt = new DataTable();
                    DataSet ds = blobj.dtBindModelDetails(plobj);
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }
                    if (dt.Rows.Count > 0)
                    {
                        lblModelType.Text = dt.Rows[0][0].ToString();
                        lblNoOfMacAddress.Text = dt.Rows[0][1].ToString();
                    }
                    if (ds.Tables.Count == 2)
                    {
                        dtDuplicateDataColumn = new DataTable();
                        dtDuplicateDataColumn = ds.Tables[1];
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }

        public void ResetDetails()
        {
            try
            {
                ddlModel_Name.Focus();
                lblNumberofRecords.Text = string.Empty;
                gvExcelFile.DataSource = null;
                gvExcelFile.DataBind();
                ddlModel_Name.SelectedIndex = 0;
                lblModelType.Text = "";
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            ResetDetails();
        }

        public DataTable dtGetHexaMacValue(DataTable dtIMEI, int iNoOFMacAddress)
        {
            DataTable dtData = new DataTable();
            try
            {
                dtIMEI = dtIMEI.Select().CopyToDataTable().DefaultView.ToTable(false, "SERIAL_NUMBER", "MAC");
                dtData.Columns.Add("SERIALNO");
                dtData.Columns.Add("MACPARENT");
                dtData.Columns.Add("MACCHILD");
                dtData.Columns.Add("SEQ");
                for (int i = 0; i < dtIMEI.Rows.Count; i++)
                {
                    string sSRNO = dtIMEI.Rows[i][0].ToString();
                    string sMacAddress = dtIMEI.Rows[i][1].ToString();
                    ulong myHex = Convert.ToUInt64(sMacAddress, 16);
                    for (int j = 0; j < iNoOFMacAddress; j++)
                    {
                        var dr = dtData.NewRow();
                        dr["SERIALNO"] = sSRNO;
                        dr[1] = sMacAddress;
                        string hexString = string.Format("{0:X}", myHex);
                        dr[2] = hexString;
                        dr[3] = j.ToString();
                        dtData.Rows.Add(dr);
                        myHex++;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            return dtData;
        }
    }
}