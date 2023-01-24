using Common;
using System;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;
using BusinessLayer.WIP;
using System.IO;

namespace SGST.INE.WIP
{
    public partial class uploafprofile : System.Web.UI.Page
    {
        BL_ProfileMaster blobj = new BL_ProfileMaster();
        string detailsofMachine, detailsofpart, detailsoftools = "";
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Signin/v1/Login.aspx?Session=Null");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usertype"].ToString() != "ADMIN")
            {
                string _strRights = CommonHelper.GetRights("UPLOAD PROGRAM DETAILS", (DataTable)Session["USER_RIGHTS"]);
                CommonHelper._strRights = _strRights.Split('^');
                if (CommonHelper._strRights[0] == "0")  //Check view rights
                {
                    Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                }
            }
            Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
            Response.AddHeader("Pragma", "no-cache");
            Response.AddHeader("Expires", "0");
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                DataTable dt = new DataTable();
                string sProgramID = string.Empty;
                if (Path.GetExtension(FileUpload1.FileName).Equals(".csv"))
                {
                    DirectoryInfo _dir = null;
                    string CSVFilePath = Path.GetFileName(FileUpload1.FileName);    //getting full file path of Uploaded file  
                    string sUploadFilePath = ConfigurationManager.AppSettings["Upload_File_Path"].ToString();
                    string sPath = Server.MapPath("~/" + sUploadFilePath + "//ProgramFile//");
                    _dir = new DirectoryInfo(sPath);
                    if (_dir.Exists == false)
                    {
                        _dir.Create();
                        Directory.CreateDirectory(_dir.ToString());
                    }
                    FileUpload1.SaveAs(Server.MapPath("~/Upload_File//ProgramFile//") + CSVFilePath);
                    string sFileName = Server.MapPath("~/Upload_File//ProgramFile//") + CSVFilePath;
                    dt = PCommon.ConvertCSVtoDataTable(sFileName);
                    if (dt.Columns.Count  != 6)
                    {
                        CommonHelper.ShowMessage("Invalid file, File Column should be equal to 6", msgerror, CommonHelper.MessageType.Error.ToString());
                        gvExcelFile.DataSource = null;
                        gvExcelFile.DataBind();
                        return;
                    }
                    if (dt.Columns[0].ToString() != "PROGRAM_ID")
                    {
                        CommonHelper.ShowMessage("Invalid file", msgerror, CommonHelper.MessageType.Error.ToString());
                        gvExcelFile.DataSource = null;
                        gvExcelFile.DataBind();
                        return;
                    }
                    else if (dt.Columns[1].ToString() != "MACHINEID")
                    {
                        CommonHelper.ShowMessage("Invalid file", msgerror, CommonHelper.MessageType.Error.ToString());
                        gvExcelFile.DataSource = null;
                        gvExcelFile.DataBind();
                        return;
                    }
                    if (dt.Columns[2].ToString() != "PART_CODE")
                    {
                        CommonHelper.ShowMessage("Invalid file", msgerror, CommonHelper.MessageType.Error.ToString());
                        gvExcelFile.DataSource = null;
                        gvExcelFile.DataBind();
                        return;
                    }
                    DataTable dtCheckMachineDetails = blobj.dtuploadprofile(dt, "MACHINE");
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtInfo, System.Reflection.MethodBase.GetCurrentMethod().Name, "Machine Validate result :" + dtCheckMachineDetails.Rows[0][0].ToString());
                    DataTable dtCheckPartCodeDetails = blobj.dtuploadprofile(dt, "PART_CODE");
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtInfo, System.Reflection.MethodBase.GetCurrentMethod().Name, "Part Code Validate result :" + dtCheckPartCodeDetails.Rows[0][0].ToString());
                    DataTable dtCheckTool = blobj.dtuploadprofile(dt, "TOOLID");
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtInfo, System.Reflection.MethodBase.GetCurrentMethod().Name, "ToolS Validate result :" + dtCheckTool.Rows[0][0].ToString());
                    detailsofMachine = dtCheckMachineDetails.Rows[0][0].ToString();
                    detailsofpart = dtCheckPartCodeDetails.Rows[0][0].ToString();
                    detailsoftools = dtCheckTool.Rows[0][0].ToString();
                    if (detailsofMachine.Contains("OKAY~") && detailsofpart.Contains("OKAY~") 
                        && detailsoftools.Contains("OKAY~"))
                    {
                        string Message = string.Empty;
                        string sResult = blobj.SaveProfileValue(dt, false,Session["UserID"].ToString());
                        if (sResult.ToUpper().StartsWith("SUCCESS"))
                        {
                            Message = sResult.Split('~')[1];
                            CommonHelper.ShowMessage(Message, msgsuccess, CommonHelper.MessageType.Success.ToString());
                            return;
                        }
                        else if (sResult.StartsWith("N~"))
                        {                          
                            Message = sResult.Split('~')[1];
                            CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                            return;
                        }
                        else if (sResult.StartsWith("ERROR~"))
                        {
                            Message = sResult.Split('~')[1];
                            CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                        }
                        else if (sResult.StartsWith("NOTFOUND~"))
                        {
                            Message = sResult.Split('~')[1];
                            CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                        }
                    }
                    else
                    {
                        CommonHelper.ShowMessage("Items marks in red are either wrong. Please upload file with correct details. Please check log files", msgerror, CommonHelper.MessageType.Error.ToString());
                        string sResult = string.Empty;
                        if (detailsofpart.ToUpper().Contains("MISSING"))
                        {
                            sResult = detailsofpart;
                        }
                        if (detailsofMachine.ToUpper().Contains("MISSING"))
                        {
                           sResult += "   " + detailsofMachine;                          
                        }
                        if (detailsoftools.ToUpper().Contains("MISSING"))
                        {
                            sResult += "   " + detailsoftools;
                        }
                        CommonHelper.ShowMessage(sResult, msgerror, CommonHelper.MessageType.Error.ToString());
                        gvExcelFile.DataSource = dt;
                        gvExcelFile.DataBind();
                    }
                }
                else
                {
                    CommonHelper.ShowMessage("Please select valid file", msgerror, CommonHelper.MessageType.Error.ToString());
                    gvExcelFile.DataSource = dt;
                    gvExcelFile.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void gvExcelFile_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (GridViewRow row in gvExcelFile.Rows)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    var array = detailsofMachine.Split(',');
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
                    var array1 = detailsofpart.Split(',');
                    foreach (string rr in array1)
                    {
                        if (rr != "")
                        {
                            if (e.Row.Cells[2].Text.ToUpper() == rr.ToString().ToUpper())
                            {
                                e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;
                            }
                        }
                    }
                    var array3 = detailsoftools.Split(',');
                    foreach (string rrrr in array3)
                    {
                        if (rrrr != "")
                        {
                            if (e.Row.Cells[4].Text.Contains(rrrr))
                            {
                                e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
                            }
                        }
                    }                   
                }
            }
        }
    }
}