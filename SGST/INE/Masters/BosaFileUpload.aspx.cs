using Common;
using System;
using System.Configuration;
using System.IO;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using PL;
using System.Linq;
using System.Data.OleDb;

namespace SGST.INE.Masters
{
    public partial class BosaFileUpload : System.Web.UI.Page
    {
        BL_IMEIUpload blobj = new BL_IMEIUpload();
        string strDuplicate = string.Empty;       
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
                    string _strRights = CommonHelper.GetRights("BOSA SN UPLOAD", (DataTable)Session["USER_RIGHTS"]);
                    CommonHelper._strRights = _strRights.Split('^');
                    if (CommonHelper._strRights[0] == "0")  //Check view rights
                    {
                        Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        public void ResetDetails()
        {
            try
            {
                lblNumberofRecords.Text = string.Empty;
                gvExcelFile.DataSource = null;
                gvExcelFile.DataBind();
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError,
                  System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }        
        public static DataTable ConvertExcelToDataTable(string FileName)
        {
            try
            {
                DataTable dtResult = null;
                int totalSheet = 0; //No of sheets on excel file  
                string sConnectionString = string.Empty;
                if (Path.GetExtension(FileName).ToLower().Trim() == ".xls" && Environment.Is64BitOperatingSystem == false)
                    sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FileName + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
                else
                    sConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1\"";

                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtInfo,
                     System.Reflection.MethodBase.GetCurrentMethod().Name,
                     "Connection Validate with string : " + sConnectionString
                     );
                using (OleDbConnection objConn = new OleDbConnection(sConnectionString))
                {
                    objConn.Open();
                    OleDbCommand cmd = new OleDbCommand();
                    OleDbDataAdapter oleda = new OleDbDataAdapter();
                    DataSet ds = new DataSet();
                    DataTable dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    string sheetName = string.Empty;
                    if (dt != null)
                    {
                        var tempDataTable = (from dataRow in dt.AsEnumerable()
                                             where !dataRow["TABLE_NAME"].ToString().Contains("FilterDatabase")
                                             select dataRow).CopyToDataTable();
                        dt = tempDataTable;
                        totalSheet = dt.Rows.Count;
                        sheetName = dt.Rows[0]["TABLE_NAME"].ToString();
                    }
                    cmd.Connection = objConn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM [" + sheetName + "]";
                    oleda = new OleDbDataAdapter(cmd);
                    oleda.Fill(ds, "excelData");
                    dtResult = ds.Tables["excelData"];
                    objConn.Close();
                    return dtResult; //Returning Dattable  
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError,
                      System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;                
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                blobj = new BL_IMEIUpload();
                PL_IMEIMaster plobj = new PL_IMEIMaster();
                DataTable dtFileUploading = new DataTable();
                string sProgramID = string.Empty;
                DataTable dtModelNameDetails = new DataTable();
                if (Path.GetExtension(FileUpload1.FileName).Equals(".xlsx"))
                {
                    DirectoryInfo _dir = null;
                    string CSVFilePath = Path.GetFileName(FileUpload1.FileName);    //getting full file path of Uploaded file  
                    string sUploadFilePath = ConfigurationManager.AppSettings["Upload_File_Path"].ToString();
                    string sPath = Server.MapPath("~/" + sUploadFilePath + "//BOSAFILE//");
                    _dir = new DirectoryInfo(sPath);
                    if (_dir.Exists == false)
                    {
                        _dir.Create();
                        Directory.CreateDirectory(_dir.ToString());
                    }
                    CSVFilePath = CSVFilePath + DateTime.Now.ToString("ddyymmhhMMsstt");
                    FileUpload1.SaveAs(Server.MapPath("~/Upload_File//BOSAFILE//") + CSVFilePath);
                    string sFileName = Server.MapPath("~/Upload_File//BOSAFILE//") + CSVFilePath;
                    dtFileUploading = ConvertExcelToDataTable(sFileName);
                    string sMPN = dtFileUploading.Rows[1][1].ToString();
                    string sTester = dtFileUploading.Rows[2][3].ToString();
                    DateTime dTestdate = Convert.ToDateTime(dtFileUploading.Rows[2][5]);
                    string sCustomerPO = dtFileUploading.Rows[2][6].ToString();
                    string sProductName = dtFileUploading.Rows[2][1].ToString();
                    string sTestTemp = dtFileUploading.Rows[3][0].ToString();
                    string sTestCondition = dtFileUploading.Rows[4][0].ToString();
                    dtFileUploading.Rows.RemoveAt(0);
                    dtFileUploading.AcceptChanges();
                    dtFileUploading.Rows.RemoveAt(0);
                    dtFileUploading.AcceptChanges();
                    dtFileUploading.Rows.RemoveAt(0);
                    dtFileUploading.AcceptChanges();
                    dtFileUploading.Rows.RemoveAt(0);
                    dtFileUploading.AcceptChanges();
                    dtFileUploading.Rows.RemoveAt(0);
                    dtFileUploading.AcceptChanges();
                    dtFileUploading.Rows.RemoveAt(0);
                    dtFileUploading.AcceptChanges();
                    dtFileUploading.Rows.RemoveAt(0);
                    dtFileUploading.AcceptChanges();
                    dtFileUploading.Columns[0].ColumnName = "SN";
                    dtFileUploading.AcceptChanges();
                    if (dtFileUploading.Columns.Count != 8)
                    {
                        CommonHelper.ShowMessage("Invalid file column no not matched", msgerror, CommonHelper.MessageType.Error.ToString());
                        gvExcelFile.DataSource = null;
                        gvExcelFile.DataBind();
                        return;
                    }
                    System.Data.DataView view = new System.Data.DataView(dtFileUploading.DefaultView.ToTable());
                    System.Data.DataTable dtIMEI =
                            view.ToTable("Table1", false, "SN");

                    var duplicates = dtIMEI.AsEnumerable().GroupBy(r => r[0]).Where(gr => gr.Count() > 1).ToList();
                    if (duplicates.Any())
                    {
                        CommonHelper.ShowMessage("Displayed Data found duplicate, "
                            + String.Join(", ", duplicates.Select(dupl => dupl.Key))
                            , msgerror, CommonHelper.MessageType.Error.ToString());
                        gvExcelFile.DataSource = dtIMEI;
                        gvExcelFile.DataBind();
                        return;
                    }
                    plobj.dtIMEIUpload = dtFileUploading;
                    dtModelNameDetails = blobj.dtCheckDuplicate_BosaFile(plobj);
                    if (dtModelNameDetails.Rows.Count == 0)
                    {
                        plobj.dtIMEIUpload = dtFileUploading;
                        plobj.sColorCode = "";
                        plobj.sSiteCode = Session["SiteCode"].ToString();
                        plobj.sUploadedby = Session["UserID"].ToString();
                        plobj.sLineCode = Session["LineCode"].ToString();
                        plobj.sCustomer = sCustomerPO;
                        plobj.sMPN = sMPN;
                        plobj.sProduct = sProductName;
                        plobj.sTester = sTester;
                        plobj.sTestTemp = sTestTemp;
                        plobj.sTestCondition = sTestCondition;
                        plobj.dTestingDate = dTestdate;

                        string Message = string.Empty;
                        string sOutput = blobj.blUploadBosaDataByDataTable(plobj);
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
                            if (sOutput.Contains("PK_mBoxaFile"))
                            {
                                Message = sOutput.Split('~')[1].Split('(')[1];
                                CommonHelper.ShowMessage("Duplicate SN address found: " + Message, msgerror, CommonHelper.MessageType.Error.ToString());
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
                        gvExcelFile.DataSource = dtIMEI;
                        gvExcelFile.DataBind();
                    }
                }
                else
                {
                    CommonHelper.ShowMessage("Please select valid file", msgerror, CommonHelper.MessageType.Error.ToString());
                    gvExcelFile.DataSource = dtModelNameDetails;
                    gvExcelFile.DataBind();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PK_mBoxaFile"))
                {
                    CommonHelper.ShowCustomErrorMessage("SR No already exist :" + ex.Message.ToString(), msgerror);
                }
                else
                {
                    CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                }
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            ResetDetails();
        }
    }
}