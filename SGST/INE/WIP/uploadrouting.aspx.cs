using Common;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;
using BusinessLayer.WIP;

namespace SGST.INE.WIP
{
    public partial class uploadrouting : System.Web.UI.Page
    {
        BL_FGRouting blobj = new BL_FGRouting();
        string detailsofMachine, detailsofFG, detailsofLine, detailsofprogrmaid = "";
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
                string _strRights = CommonHelper.GetRights("UPLOAD ROUTING DETAILS", (DataTable)Session["USER_RIGHTS"]);
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
                if (Path.GetExtension(FileUpload1.FileName).Equals(".csv"))
                {
                    DirectoryInfo _dir = null;
                    string CSVFilePath = Path.GetFileName(FileUpload1.FileName);    //getting full file path of Uploaded file  
                    string sUploadFilePath = ConfigurationManager.AppSettings["Upload_File_Path"].ToString();
                    string sPath = Server.MapPath("~/" + sUploadFilePath + "//Route//");
                    _dir = new DirectoryInfo(sPath);
                    if (_dir.Exists == false)
                    {
                        _dir.Create();
                        Directory.CreateDirectory(_dir.ToString());
                    }
                    FileUpload1.SaveAs(Server.MapPath("~/Upload_File//Route//") + CSVFilePath);
                    string sFileName = Server.MapPath("~/Upload_File//Route//") + CSVFilePath;
                    dt = PCommon.ConvertCSVtoDataTable(sFileName);
                    if (dt.Columns.Count != 16)
                    {
                        CommonHelper.ShowMessage("Invalid file:No Of Columns Mismatch", msgerror, CommonHelper.MessageType.Error.ToString());
                        gvExcelFile.DataSource = null;
                        gvExcelFile.DataBind();
                        return;
                    }
                    if (dt.Columns[0].ToString() != "LINEID")
                    {
                        CommonHelper.ShowMessage("Invalid file:Line ID Column name Mismatch", msgerror, CommonHelper.MessageType.Error.ToString());
                        gvExcelFile.DataSource = null;
                        gvExcelFile.DataBind();
                        return;
                    }
                    else if (dt.Columns[1].ToString() != "FG_ITEM_CODE")
                    {
                        CommonHelper.ShowMessage("Invalid file:FG Item Code Column name Mismatch", msgerror, CommonHelper.MessageType.Error.ToString());
                        gvExcelFile.DataSource = null;
                        gvExcelFile.DataBind();
                        return;
                    }
                    if (dt.Columns[2].ToString() != "MACHINEID")
                    {
                        CommonHelper.ShowMessage("Invalid file:Machine ID Column name Mismatch", msgerror, CommonHelper.MessageType.Error.ToString());
                        gvExcelFile.DataSource = null;
                        gvExcelFile.DataBind();
                        return;
                    }
                    if (dt.Columns[3].ToString() != "SEQ")
                    {
                        CommonHelper.ShowMessage("Invalid file:Seq Column name Mismatch", msgerror, CommonHelper.MessageType.Error.ToString());
                        gvExcelFile.DataSource = null;
                        gvExcelFile.DataBind();
                        return;
                    }
                    if (dt.Columns[5].ToString() != "PROGRAMID")
                    {
                        CommonHelper.ShowMessage("Invalid file:Program ID Column name Mismatch", msgerror, CommonHelper.MessageType.Error.ToString());
                        gvExcelFile.DataSource = null;
                        gvExcelFile.DataBind();
                        return;
                    }
                    if (dt.Columns[6].ToString() != "REWORK_SEQ")
                    {
                        CommonHelper.ShowMessage("Invalid file:Rework seq Column name Mismatch", msgerror, CommonHelper.MessageType.Error.ToString());
                        gvExcelFile.DataSource = null;
                        gvExcelFile.DataBind();
                        return;
                    }
                    if (dt.Columns[7].ToString() != "IS_SFG")
                    {
                        CommonHelper.ShowMessage("Invalid file:IS SFG Column name Mismatch", msgerror, CommonHelper.MessageType.Error.ToString());
                        gvExcelFile.DataSource = null;
                        gvExcelFile.DataBind();
                        return;
                    }
                    if (dt.Columns[8].ToString() != "SFG_ITEM_CODE")
                    {
                        CommonHelper.ShowMessage("Invalid file:SFG Item Code Column name Mismatch", msgerror, CommonHelper.MessageType.Error.ToString());
                        gvExcelFile.DataSource = null;
                        gvExcelFile.DataBind();
                        return;
                    }
                    if (dt.Columns[9].ToString() != "ENABLE")
                    {
                        CommonHelper.ShowMessage("Invalid file:Enable Column name Mismatch", msgerror, CommonHelper.MessageType.Error.ToString());
                        gvExcelFile.DataSource = null;
                        gvExcelFile.DataBind();
                        return;
                    }
                    if (dt.Columns[10].ToString() != "OUT_SCAN_REQUIRED")
                    {
                        CommonHelper.ShowMessage("Invalid file:Out Scan Required Column name Mismatch", msgerror, CommonHelper.MessageType.Error.ToString());
                        gvExcelFile.DataSource = null;
                        gvExcelFile.DataBind();
                        return;
                    }
                    if (dt.Columns[12].ToString() != "ROUTE_NAME")
                    {
                        CommonHelper.ShowMessage("Invalid file:Route Name Required Column name Mismatch", msgerror, CommonHelper.MessageType.Error.ToString());
                        gvExcelFile.DataSource = null;
                        gvExcelFile.DataBind();
                        return;
                    }
                    if (dt.Columns[13].ToString() != "TMO_PARTCODE")
                    {
                        CommonHelper.ShowMessage("Invalid file:TMO_PARTCODE Required Column name Mismatch", msgerror, CommonHelper.MessageType.Error.ToString());
                        gvExcelFile.DataSource = null;
                        gvExcelFile.DataBind();
                        return;
                    }
                    if (dt.Columns[14].ToString() != "ISAUTOXRAYSAMPLING")
                    {
                        CommonHelper.ShowMessage("Invalid file:ISAUTOXRAYSAMPLING Required Column name Mismatch", msgerror, CommonHelper.MessageType.Error.ToString());
                        gvExcelFile.DataSource = null;
                        gvExcelFile.DataBind();
                        return;
                    }
                    if (dt.Columns[15].ToString() != "SFGQty")
                    {
                        CommonHelper.ShowMessage("Invalid file:SFGQty Required Column name Mismatch", msgerror, CommonHelper.MessageType.Error.ToString());
                        gvExcelFile.DataSource = null;
                        gvExcelFile.DataBind();
                        return;
                    }
                    DataView view = new DataView(dt);
                    DataTable distinctValues = view.ToTable(true, "ROUTE_NAME");
                    if (distinctValues.Rows.Count > 1)
                    {
                        CommonHelper.ShowMessage("Invalid file:Route name can not be multiple for single fg item code", msgerror, CommonHelper.MessageType.Error.ToString());
                        gvExcelFile.DataSource = null;
                        gvExcelFile.DataBind();
                        return;
                    }
                    string sFGItemCode = dt.Rows[0][1].ToString();
                    string sRouteName = dt.Rows[0][12].ToString();
                    dt.Columns.Remove("ROUTE_NAME");
                    dt.AcceptChanges();               

                    DataTable dtChecklINEDetails = blobj.dtuploadRouting(dt, "LINE", sRouteName,Session["userid"].ToString());
                    DataTable dtCheckfg = blobj.dtuploadRouting(dt, "FGITEMCODE", sRouteName, Session["userid"].ToString());
                    DataTable dtcheckmachine = blobj.dtuploadRouting(dt, "MACHINE", sRouteName, Session["userid"].ToString());
                    DataTable dtcheckprogramid = blobj.dtuploadRouting(dt, "PROGRAMID", sRouteName, Session["userid"].ToString());

                    detailsofLine = dtChecklINEDetails.Rows[0][0].ToString();
                    detailsofFG = dtCheckfg.Rows[0][0].ToString();
                    detailsofMachine = dtcheckmachine.Rows[0][0].ToString();
                    detailsofprogrmaid = dtcheckprogramid.Rows[0][0].ToString();
                    if (detailsofLine.Contains("OKAY~") && detailsofFG.Contains("OKAY~") && detailsofMachine.Contains("OKAY~") && detailsofprogrmaid.Contains("OKAY~"))
                    {
                        try
                        {
                            CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtInfo,
                     System.Reflection.MethodBase.GetCurrentMethod().Name,"Route Upload for fg Item Code :"+ sFGItemCode
                     +"and File Name :" + sFileName );
                            DataTable dtData = blobj.dtuploadRouting(dt, "UPLOADROUTING", sRouteName,Session["userid"].ToString());
                            if (dtData.Rows.Count > 0)
                            {
                                if (dtData.Rows[0][0].ToString().StartsWith("OKAY~"))
                                {
                                    CommonHelper.ShowMessage("Data Upload Successfully", msgsuccess, CommonHelper.MessageType.Success.ToString());
                                    gvExcelFile.DataSource = dt;
                                    gvExcelFile.DataBind();
                                }
                                else
                                {
                                    CommonHelper.ShowMessage(dtData.Rows[0][0].ToString().Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                                    gvExcelFile.DataSource = dt;
                                    gvExcelFile.DataBind();
                                }                            
                            }
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message.Contains("Violation of PRIMARY KEY constraint"))
                            {
                                string message = "";
                                string[] s = ex.Message.ToString().Split('(');
                                message = s[1];
                                CommonHelper.ShowMessage("Cannot insert duplicate Data : (" + message, msgerror, CommonHelper.MessageType.Error.ToString());
                                return;
                            }
                            else
                            {
                                CommonHelper.ShowMessage(ex.Message, msgerror, CommonHelper.MessageType.Error.ToString());
                                gvExcelFile.DataSource = null;
                                gvExcelFile.DataBind();
                                return;
                            }
                        }
                    }
                    else
                    {
                        string sResult = string.Empty;                        
                        if (!detailsofFG.StartsWith("OKAY~"))
                        {
                            sResult = detailsofFG;
                        }
                        else if (!detailsofLine.StartsWith("OKAY~"))
                        {
                            sResult = detailsofLine;
                        }
                        else if (!detailsofMachine.StartsWith("OKAY~"))
                        {
                            sResult = detailsofMachine;
                        }
                        else if (!detailsofprogrmaid.StartsWith("OKAY~"))
                        {
                            sResult = detailsofprogrmaid;
                        }
                        CommonHelper.ShowMessage(sResult, msgerror, CommonHelper.MessageType.Error.ToString());
                       // CommonHelper.ShowMessage("Items marks in red are either wrong. Please upload file with correct details.", msgerror, CommonHelper.MessageType.Error.ToString());
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
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, 
                    System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void gvExcelFile_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                foreach (GridViewRow row in gvExcelFile.Rows)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        var array = detailsofLine.Split(',');
                        foreach (string r in array)
                        {
                            if (r != "")
                            {
                                if (e.Row.Cells[0].Text.Contains(r))
                                {
                                    e.Row.Cells[0].ForeColor = System.Drawing.Color.Red;
                                }
                            }
                        }
                        var array1 = detailsofFG.Split(',');
                        foreach (string rr in array1)
                        {
                            if (rr != "")
                            {
                                if (e.Row.Cells[1].Text.Contains(rr))
                                {
                                    e.Row.Cells[1].ForeColor = System.Drawing.Color.Red;
                                }
                            }
                        }
                        var array2 = detailsofMachine.Split(',');
                        foreach (string rrr in array2)
                        {
                            if (rrr != "")
                            {
                                if (e.Row.Cells[2].Text.Contains(rrr))
                                {
                                    e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;
                                }
                            }
                        }
                        var array3 = detailsofprogrmaid.Split(',');
                        foreach (string rrrr in array3)
                        {
                            if (rrrr != "")
                            {
                                if (e.Row.Cells[5].Text.Contains(rrrr))
                                {
                                    e.Row.Cells[5].ForeColor = System.Drawing.Color.Red;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}