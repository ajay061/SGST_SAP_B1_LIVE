using Common;
using System;
using System.Web.UI.WebControls;
using BusinessLayer.Masters;
using System.Data;
using System.IO;
using System.Configuration;

namespace SGST.INE.Masters
{
    public partial class FGCustomerMapping : System.Web.UI.Page
    {
        string Message = string.Empty;
        BL_FG_CustomeMapping blobj = new BL_FG_CustomeMapping();
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
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (Session["usertype"].ToString() != "ADMIN")
                {
                    string _strRights = CommonHelper.GetRights("FG Customer Mapping", (DataTable)Session["USER_RIGHTS"]);
                    CommonHelper._strRights = _strRights.Split('^');
                    if (CommonHelper._strRights[0] == "0")  //Check view rights
                    {
                        Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                    }
                }
                if (!IsPostBack)
                {
                    BindFGItemCode();
                    BindCustomerCode();
                    ShowGridData();
                    drpFGItemCode.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        private void BindFGItemCode()
        {
            try
            {
                drpFGItemCode.Items.Clear();
                blobj = new BL_FG_CustomeMapping();
                DataTable dt = blobj.BindFGitemCode();
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(drpFGItemCode, dt, true);
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        private void BindCustomerCode()
        {
            try
            {
                drpCustomerCode.Items.Clear();
                blobj = new BL_FG_CustomeMapping();
                DataTable dt = blobj.BindCustomerCode();
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillMultiColumnsCombo(drpCustomerCode, dt, true);
                    drpCustomerCode.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        private void ShowGridData()
        {
            try
            {
                blobj = new BL_FG_CustomeMapping();
                DataTable dt = blobj.GetData();
                lblNumberofRecords.Text = dt.Rows.Count.ToString();
                if (dt.Rows.Count > 0)
                {
                    gvMappingData.DataSource = dt;
                    gvMappingData.DataBind();
                    dt.TableName = "Table1";
                    dt.AcceptChanges();
                    System.Data.DataView view = new System.Data.DataView(dt.DefaultView.ToTable(true, "FG_ITEM_CODE"));
                    System.Data.DataTable selected =
                            view.ToTable("Table1", false, "FG_ITEM_CODE");
                    clsCommon.FillComboBox(drpFGItemCodeSearch, selected, true);
                    ViewState["Data"] = dt;
                }
                else
                {
                    gvMappingData.DataSource = dt;
                    gvMappingData.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        protected void gvMappingData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvMappingData.PageIndex = e.NewPageIndex;
                ShowGridData();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (drpFGItemCode.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select fg item code", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpFGItemCode.Focus();
                    return;
                }
                if (drpCustomerCode.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select customer code", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpCustomerCode.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtPackignTimeHours.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please enter packing time", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpCustomerCode.Focus();
                    return;
                }
                string sApprovedStatus = "0";
                if (drpApproved.SelectedIndex == 0)
                {
                    sApprovedStatus = "0";
                }
                else
                {
                    sApprovedStatus = "1";
                }
                string ISFULLAGING = "0";
                if (chkAgingfullAging.Checked == true)
                {
                    ISFULLAGING = "1";
                }
                else
                {
                    ISFULLAGING = "0";
                }
                blobj = new BL_FG_CustomeMapping();
                string sResult = blobj.SaveData(drpFGItemCode.SelectedItem.Text.Trim(),
                    drpCustomerCode.SelectedValue.ToString().Trim(), 0
                    , Convert.ToInt32(txtPackignTimeHours.Text.Trim())
                    , sApprovedStatus, Convert.ToInt32(textNOPMapped.Text)
                    , Convert.ToInt32(txtPackingSamplingQty.Text),Session["UserID"].ToString()
                     , Convert.ToInt32(txtAgeingTimePeriod.Text), ISFULLAGING
                    );
                if (sResult.Length > 0)
                {
                    if (sResult.StartsWith("SUCCESS~"))
                    {
                        _ResetField();
                        CommonHelper.ShowMessage("Details has been saved", msgsuccess, CommonHelper.MessageType.Success.ToString());
                        ShowGridData();
                    }
                    else
                    {
                        if (sResult.Contains("Violation of PRIMARY KEY"))
                        {
                            drpFGItemCode.SelectedIndex = 0;
                            CommonHelper.ShowMessage("Details already exist, Please select another one", msgerror, CommonHelper.MessageType.Error.ToString());
                            ShowGridData();
                        }
                        else
                        {
                            _ResetField();
                            CommonHelper.ShowMessage(sResult, msgerror, CommonHelper.MessageType.Error.ToString());
                            ShowGridData();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                if (ex.Message.Contains("Violation of PRIMARY KEY"))
                {
                    CommonHelper.ShowCustomErrorMessage("Id already exist, Please enter another one", msgerror);
                }
                else
                {
                    CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                }
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ShowGridData();
                }
                _ResetField();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void gvMappingData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                string _SN = string.Empty;
                _SN = e.CommandArgument.ToString();
                if (e.CommandName == "DeleteRecords")
                {
                    DeleteRecords(_SN);
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void _ResetField()
        {
            if (drpCustomerCode.Items.Count > 0)
            {
                drpCustomerCode.SelectedIndex = 0;
            }
            if (drpFGItemCode.Items.Count > 0)
            {
                drpFGItemCode.SelectedIndex = 0;
            }
            txtPackignTimeHours.Text = "0";
            txtAgeingTimePeriod.Text = "0";
        }
        private void DeleteRecords(string _SN)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                blobj = new BL_FG_CustomeMapping();
                string sResult = blobj.DeleteData("", "", Convert.ToInt32(_SN), 0);
                if (sResult.StartsWith("SUCCESS~"))
                {
                    CommonHelper.ShowMessage("Details deleted successfully", msgsuccess, CommonHelper.MessageType.Success.ToString());
                    _ResetField();
                    return;
                }
                else
                {
                    CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    return;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
            finally
            {
                ShowGridData();
            }
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
                    string sPath = Server.MapPath("~/" + sUploadFilePath + "//");
                    _dir = new DirectoryInfo(sPath);
                    if (_dir.Exists == false)
                    {
                        _dir.Create();
                        Directory.CreateDirectory(_dir.ToString());
                    }
                    FileUpload1.SaveAs(Server.MapPath("~/Upload_File//") + CSVFilePath);
                    string sFileName = Server.MapPath("~/Upload_File//") + CSVFilePath;
                    dt = PCommon.ConvertCSVtoDataTable(sFileName);
                    if (dt.Columns.Count != 8)
                    {
                        CommonHelper.ShowMessage("Invalid file,No of columns should be 6", msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                    if (dt.Columns[0].ToString() != "SITE_CODE")
                    {
                        CommonHelper.ShowMessage("Invalid file, Wrong column name", msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                    if (dt.Columns[1].ToString() != "FG_ITEM_CODE")
                    {
                        CommonHelper.ShowMessage("Invalid file, Wrong column name", msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                    else if (dt.Columns[2].ToString() != "CUSTOMER_CODE")
                    {
                        CommonHelper.ShowMessage("Invalid file, Wrong column name", msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                    else if (dt.Columns[3].ToString() != "PACKINGTIMEHOURS")
                    {
                        CommonHelper.ShowMessage("Invalid file, Wrong column name", msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                    else if (dt.Columns[7].ToString() != "PACKINGSAMPLINGQTY")
                    {
                        CommonHelper.ShowMessage("Invalid file, Wrong column name", msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                    else if (dt.Columns[8].ToString() != "AGINGTIMEPERIOD")
                    {
                        CommonHelper.ShowMessage("Invalid file, Wrong column name", msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                    else if (dt.Columns[9].ToString() != "ISFULLAGING")
                    {
                        CommonHelper.ShowMessage("Invalid file, Wrong column name", msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                    BL_FG_CustomeMapping blobj = new BL_FG_CustomeMapping();
                    string Message = string.Empty;
                    DataTable dtResult = blobj.UploadFGCustomerMapping(dt);
                    string sResult = string.Empty;
                    if (dtResult.Rows.Count > 0)
                    {
                        sResult = dtResult.Rows[0][0].ToString();
                    }
                    if (sResult.ToUpper().StartsWith("SUCCESS"))
                    {
                        Message = sResult.Split('~')[1];
                        CommonHelper.ShowMessage(Message, msgsuccess, CommonHelper.MessageType.Success.ToString());
                        ShowGridData();
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
                        if (sResult.ToUpper().Contains("PRIMARY"))
                        {
                            Message = "Data in selected file already uploaded in database, Please upload fresh record only";
                        }
                        else if (sResult.ToUpper().Contains("FOREIGN KEY"))
                        {
                            Message = "Data in selected file not found in master tables, Please check master data before upload";
                        }
                        else
                        {
                            Message = sResult.Split('~')[1];
                        }
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
                    CommonHelper.ShowMessage("Please select valid(.csv) file only", msgerror, CommonHelper.MessageType.Error.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void drpFGItemCodeSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpFGItemCodeSearch.SelectedIndex > 0)
                {
                    DataTable dt = (DataTable)ViewState["Data"];
                    DataView dataView = dt.DefaultView;
                    dataView.RowFilter = "FG_ITEM_CODE = '" + drpFGItemCodeSearch.SelectedValue + "'";
                    gvMappingData.DataSource = dataView;
                    gvMappingData.DataBind();
                }
                else
                {
                    DataTable dt = (DataTable)ViewState["Data"];
                    gvMappingData.DataSource = dt;
                    gvMappingData.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}