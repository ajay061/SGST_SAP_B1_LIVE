using Common;
using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer.WIP;

namespace SGST.INE.WIP
{
    public partial class ProfileMaster : System.Web.UI.Page
    {
        BL_ProfileMaster blobj = new BL_ProfileMaster();
        static DataTable dtAdd;
        static string _profileID = string.Empty;
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
                if (Session["usertype"].ToString() != "ADMIN")
                {
                    string _strRights = CommonHelper.GetRights("WIP PROGRAM MASTER", (DataTable)Session["USER_RIGHTS"]);
                    CommonHelper._strRights = _strRights.Split('^');
                    if (CommonHelper._strRights[0] == "0")  //Check view rights
                    {
                        Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                    }
                }
                Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
                Response.AddHeader("Pragma", "no-cache");
                Response.AddHeader("Expires", "0");
                if (!Page.IsPostBack)
                {
                    dtAdd = new DataTable();
                    AddColumns();
                    bindPartCode();
                    BindProgramID();
                    blobj = new BL_ProfileMaster();
                    DataTable dt = blobj.GetMachineRecord();
                    if (dt.Rows.Count > 0)
                    {
                        clsCommon.FillMachine(drpMachine, dt, true);
                        drpMachine.Focus();
                    }
                    else
                    {
                        drpMachine.Items.Clear();
                        CommonHelper.ShowMessage("no machine details found ", msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void bindPartCode()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                blobj = new BL_ProfileMaster();
                DataTable dtPartCode = blobj.GetPartCode();
                if (dtPartCode.Rows.Count > 0)
                {
                    CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                    clsCommon.FillComboBox(drpPartcode, dtPartCode, true);
                    drpPartcode.SelectedIndex = 0;
                    drpPartcode.Focus();
                }
                else
                {
                    drpPartcode.DataSource = null;
                    drpPartcode.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }

        }
        private void BindProgramID()
        {
            try
            {
                blobj = new BL_ProfileMaster();
                DataTable dtProfileID = blobj.GetProgramID();
                if (dtProfileID.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(drpProgramID, dtProfileID, true);
                    drpProgramID.SelectedIndex = 0;
                    drpProgramID.Focus();
                }
                else
                {
                    drpProgramID.DataSource = null;
                    drpProgramID.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        private void AddColumns()
        {
            DataColumn ID = new DataColumn("ID", typeof(int));
            DataColumn PID = new DataColumn("PID", typeof(String));
            DataColumn ProfileID = new DataColumn("PROGRAM_ID", typeof(string));
            DataColumn MachineID = new DataColumn("MACHINEID", typeof(String));
            DataColumn PartCode = new DataColumn("PART_CODE", typeof(string));
            //DataColumn FeederNo = new DataColumn("FeederNo", typeof(String));
            DataColumn FeederLoc = new DataColumn("FeederLoc", typeof(String));
            DataColumn PCBQty = new DataColumn("MACHINE_PCB_QTY", typeof(String));
            //DataColumn FeeerID = new DataColumn("FeederID", typeof(String));
            DataColumn ToolID = new DataColumn("ToolID", typeof(String));

            dtAdd.Columns.Add(ID);
            dtAdd.Columns.Add(PID);
            dtAdd.Columns.Add(ProfileID);
            dtAdd.Columns.Add(MachineID);
            dtAdd.Columns.Add(PartCode);
            dtAdd.Columns.Add(FeederLoc);
            dtAdd.Columns.Add(ToolID);
            dtAdd.Columns.Add(PCBQty);
        }
        private void Reset()
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                drpPartcode.SelectedIndex = 0;
                gvProfileMaster.DataSource = null;
                gvProfileMaster.DataBind();
                dtAdd.Clear();
                txtProgramName.Enabled = true;
                drpMachine.Enabled = true;
                _profileID = string.Empty;
                btnedit.Visible = true;
                drpProgramID.Enabled = true;
                btnAdd.Visible = true;
                btnSave.Visible = true;
                txtFeederLocation.Text = string.Empty;
                txtMachinePCBQty.Text = string.Empty;
                txtProgramName.Text = string.Empty;
                txtTool.Text = string.Empty;
                if (drpProgramID.Items.Count > 0)
                {
                    drpProgramID.SelectedIndex = 0;
                }
                lblNumberofRecords.Text = "0";
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void AddNewRow()
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (btnSave.Text == "Save")
                {
                    if (string.IsNullOrEmpty(txtProgramName.Text.Trim()))
                    {
                        CommonHelper.ShowMessage("Please enter program name.", msgerror, CommonHelper.MessageType.Error.ToString());
                        txtProgramName.Focus();
                        return;
                    }
                    if (drpMachine.SelectedIndex == 0)
                    {
                        CommonHelper.ShowMessage("Please select machine id.", msgerror, CommonHelper.MessageType.Error.ToString());
                        drpMachine.Focus();
                        return;
                    }
                }
                string MachineID = HIDMACHINEID.Value;
                if (string.IsNullOrEmpty(MachineID))
                {
                    MachineID = drpMachine.SelectedValue.ToString();
                }
                string sProgramID = txtProgramName.Text.Trim();
                string PartCode = "";
                if (drpPartcode.SelectedItem.Text == "--Select--")
                {
                    PartCode = "";
                }
                else
                {
                    PartCode = drpPartcode.SelectedItem.Text;
                }
                bool drs = dtAdd.AsEnumerable().Any(tt => tt.Field<string>("PROGRAM_ID") != sProgramID);
                if (drs == true)
                {
                    CommonHelper.ShowMessage("Machine id is not maching with the profile.", msgerror, CommonHelper.MessageType.Error.ToString());
                    return;
                }
                //if (!string.IsNullOrEmpty(txtFeederNo.Text.Trim()))
                //{
                //    DataRow rw2 = dtAdd.AsEnumerable().FirstOrDefault(tt => tt.Field<string>("MACHINEID") == MachineID
                //&& tt.Field<string>("FeederNo") == txtFeederNo.Text && tt.Field<string>("PART_CODE") == PartCode);
                //    if (rw2 != null)
                //    {
                //        CommonHelper.ShowMessage("This machine and solder is already attached.", msgerror, CommonHelper.MessageType.Error.ToString());
                //        txtFeederNo.Text = string.Empty;
                //        txtFeederNo.Focus();
                //        return;
                //    }
                //}
                if (!string.IsNullOrEmpty(txtFeederLocation.Text.Trim()))
                {
                    DataRow rw3 = dtAdd.AsEnumerable().FirstOrDefault(tt => tt.Field<string>("MACHINEID") == MachineID
                && tt.Field<string>("FeederLoc") == txtFeederLocation.Text.Trim() && tt.Field<string>("PART_CODE") == PartCode);
                    if (rw3 != null)
                    {
                        CommonHelper.ShowMessage("This machine and feeder location is already attached.", msgerror, CommonHelper.MessageType.Error.ToString());
                        txtFeederLocation.Text = string.Empty;
                        txtFeederLocation.Focus();
                        return;
                    }
                }
                if (!string.IsNullOrEmpty(txtTool.Text.Trim()))
                {
                    DataRow rw5 = dtAdd.AsEnumerable().FirstOrDefault(tt => tt.Field<string>("MACHINEID") == MachineID
                && tt.Field<string>("ToolID") == txtTool.Text.Trim() && tt.Field<string>("PART_CODE") == PartCode);
                    if (rw5 != null)
                    {
                        CommonHelper.ShowMessage("This machine and Tool is already attached.", msgerror, CommonHelper.MessageType.Error.ToString());
                        txtTool.Text = string.Empty;
                        txtTool.Focus();
                        return;
                    }
                }
                DataRow dr = dtAdd.NewRow();
                dr["ID"] = 1;
                dr["PID"] = 1;
                dr["PROGRAM_ID"] = txtProgramName.Text;
                dr["MACHINEID"] = MachineID;
                dr["PART_CODE"] = PartCode;
                dr["FeederLoc"] = txtFeederLocation.Text.Trim();
                dr["ToolID"] = txtTool.Text.Trim();
                dr["MACHINE_PCB_QTY"] = txtMachinePCBQty.Text.Trim();
                dtAdd.Rows.Add(dr);
                gvProfileMaster.DataSource = dtAdd;
                gvProfileMaster.DataBind();
                txtProgramName.Enabled = false;
                CommonHelper.ShowMessage("Record added successfully for machine" + drpMachine.Text, msgsuccess, CommonHelper.MessageType.Success.ToString());
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        public void DeleteTableRecord(string sPartCode, string sMachineID)
        {
            try
            {
                foreach (DataRow dr in dtAdd.Rows)
                {
                    if (dr["PART_CODE"].ToString() == sPartCode.Trim() && dr["MACHINEID"].ToString() == sMachineID.Trim())
                    {
                        dtAdd.Rows.Remove(dr);
                        break;
                    }
                }
                dtAdd.AcceptChanges();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                gvProfileMaster.DataSource = dtAdd;
                gvProfileMaster.DataBind();
            }

        }

        private void DeleteRecords(string _SN, string sProgramName, string sMachineID)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                blobj = new BL_ProfileMaster();
                string sResult = blobj.DeleteProfilePartCode(_SN, sProgramName, sMachineID);
                string Message = sResult.Split('~')[1];
                if (sResult.StartsWith("SUCCESS~"))
                {
                    gvProfileMaster.DataSource = null;
                    gvProfileMaster.DataBind();
                    gvProfileMaster.DataSource = dtAdd;
                    gvProfileMaster.DataBind();
                    CommonHelper.ShowMessage(Message, msgsuccess, CommonHelper.MessageType.Success.ToString());
                }
                else if (sResult.StartsWith("Not~"))
                {
                    CommonHelper.ShowMessage("Record deleted successfully", msgsuccess, CommonHelper.MessageType.Success.ToString());
                    gvProfileMaster.DataSource = null;
                    gvProfileMaster.DataBind();
                    gvProfileMaster.DataSource = dtAdd;
                    gvProfileMaster.DataBind();
                }
                else if (sResult.StartsWith("N~"))
                {
                    if (Message.Contains("Error converting"))
                    {

                    }
                    else
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
            finally
            {
                gvProfileMaster.DataSource = dtAdd;
                gvProfileMaster.DataBind();
            }
        }

        public void ClearControl()
        {
            drpMachine.Enabled = true;
            drpMachine.CssClass = "form-control";
            drpPartcode.SelectedIndex = 0;
            gvProfileMaster.DataSource = null;
            gvProfileMaster.DataBind();
            txtProgramName.Text = string.Empty;
            txtFeederLocation.Text = string.Empty;
            txtMachinePCBQty.Text = string.Empty;
            txtTool.Text = string.Empty;
            txtProgramName.Enabled = true;
            dtAdd.Clear();
            lblNumberofRecords.Text = "0";
        }

        protected void txtTool_TextChanged(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (txtTool.Text.Trim().Length > 0)
                {
                    blobj = new BL_ProfileMaster();
                    string sResult = string.Empty;
                    DataTable dt = blobj.ValidateTool(txtTool.Text.Trim());
                    if (dt.Rows.Count > 0)
                    {
                        CommonHelper.ShowMessage("Tool is OK", msgsuccess, CommonHelper.MessageType.Success.ToString());
                        btnAdd.Focus();
                        return;
                    }
                    else
                    {
                        CommonHelper.ShowMessage("Invalid Tool", msgerror, CommonHelper.MessageType.Error.ToString());
                        txtTool.Text = string.Empty;
                    }
                }

            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            Reset();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (btnSave.Text == "Save")
                {
                    if (gvProfileMaster.Rows.Count == 0)
                    {
                        CommonHelper.ShowMessage("Please add at least one item.", msgerror, CommonHelper.MessageType.Error.ToString());
                        drpMachine.Focus();
                        return;
                    }
                }
                if (dtAdd.Rows.Count > 0)
                {
                    bool IsEnable = true;
                    if (drpProgramID.Enabled == true)
                    {
                        IsEnable = true;
                    }
                    else
                    {
                        IsEnable = false;
                    }
                    string Message = string.Empty;
                    dtAdd.Columns.Remove("PID");
                    dtAdd.AcceptChanges();
                    dtAdd.Columns.Remove("ID");
                    dtAdd.AcceptChanges();
                    string sResult = blobj.SaveProfileValue(dtAdd, IsEnable, Session["UserID"].ToString());
                    if (sResult.ToUpper().StartsWith("SUCCESS"))
                    {
                        Message = sResult.Split('~')[1];
                        CommonHelper.ShowMessage(Message, msgsuccess, CommonHelper.MessageType.Success.ToString());
                        ClearControl();
                        drpMachine.Focus();
                        dtAdd.Clear();
                        BindProgramID();
                        btnedit.Visible = true;
                        drpMachine.Enabled = true;
                        drpProgramID.Enabled = true;
                        txtProgramName.Enabled = true;
                        return;
                    }
                    else if (sResult.StartsWith("N~"))
                    {
                        ClearControl();
                        drpMachine.Focus();
                        Message = sResult.Split('~')[1];
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                    else if (sResult.StartsWith("ERROR~"))
                    {
                        ClearControl();
                        drpMachine.Focus();
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
                    CommonHelper.ShowMessage("Please add at least one item.", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpMachine.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
            finally
            {
                DataColumnCollection columns = dtAdd.Columns;
                if (!columns.Contains("PID"))
                {
                    dtAdd.Columns.Add("PID");
                    dtAdd.AcceptChanges();
                }
                if (!columns.Contains("ID"))
                {
                    dtAdd.Columns.Add("ID");
                    dtAdd.AcceptChanges();
                }
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvProfileMaster.PageIndex = e.NewPageIndex;
                gvProfileMaster.DataSource = dtAdd;
                gvProfileMaster.DataBind();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                string _SN = string.Empty;
                string CommandName = e.CommandName;
                if (CommandName != "Page")
                {
                    _SN = e.CommandArgument.ToString();
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    string MachineID = gvr.Cells[3].Text.Replace("&nbsp;", " ").Trim();
                    _SN = gvr.Cells[4].Text.Replace("&nbsp;", " ").Trim();
                    if (e.CommandName == "DeleteRecords")
                    {
                        DeleteTableRecord(_SN.Trim(), MachineID);
                        CommonHelper.ShowMessage("Data delete successfully. Press save button to save the record in database", msginfo, CommonHelper.MessageType.Info.ToString());
                        btnSave.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }


        protected void btnedit_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            if (drpProgramID.SelectedIndex == 0 || drpProgramID.SelectedIndex == -1)
            {
                CommonHelper.ShowMessage("Please Select Program ID", msgerror, CommonHelper.MessageType.Error.ToString());
                drpProgramID.Focus();
                return;
            }
            try
            {
                string Message = string.Empty;
                DataTable dtProgramDetails = blobj.GetProgramDetails(drpProgramID.SelectedItem.Text, "");
                if (dtProgramDetails.Rows.Count > 0)
                {
                    gvProfileMaster.DataSource = dtProgramDetails;
                    gvProfileMaster.DataBind();
                    drpProgramID.Enabled = false;
                    txtProgramName.Text = dtProgramDetails.Rows[0]["PROGRAM_ID"].ToString();
                    lblNumberofRecords.Text = dtProgramDetails.Rows.Count.ToString();
                    foreach (DataRow dr1 in dtProgramDetails.Rows)
                    {
                        DataRow dr = dtAdd.NewRow();
                        dr["PID"] = dr1["P_ID"];
                        dr["PROGRAM_ID"] = dr1["PROGRAM_ID"];
                        dr["MACHINEID"] = dr1["MACHINEID"];
                        dr["PART_CODE"] = dr1["PART_CODE"];
                        dr["MACHINE_PCB_QTY"] = dr1["MACHINE_PCB_QTY"];
                        dr["FeederLoc"] = dr1["FeederLoc"];
                        dr["ToolID"] = dr1["ToolID"];
                        dtAdd.Rows.Add(dr);
                    }
                    gvProfileMaster.DataSource = dtAdd;
                    gvProfileMaster.DataBind();
                }
                else
                {
                    gvProfileMaster.DataSource = null;
                    gvProfileMaster.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
        }
        protected void btnDelProfile_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            if (drpProgramID.SelectedIndex == 0 || drpProgramID.SelectedIndex == -1)
            {
                CommonHelper.ShowMessage("Please select profile ID", msgerror, CommonHelper.MessageType.Error.ToString());
                return;
            }
            try
            {
                blobj = new BL_ProfileMaster();
                string sResult = blobj.DeleteCompleteProgramDetails(drpProgramID.Text);
                string Message = sResult.Split('~')[1];
                if (sResult.StartsWith("SUCCESS~"))
                {
                    dtAdd.Clear();
                    gvProfileMaster.DataSource = null;
                    gvProfileMaster.DataBind();
                    BindProgramID();
                    CommonHelper.ShowMessage(Message, msgsuccess, CommonHelper.MessageType.Success.ToString());
                }
                else if (sResult.StartsWith("N~"))
                {
                    if (Message.Contains("Error converting"))
                    {

                    }
                    else
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                }
                else if (sResult.StartsWith("ERROR~"))
                {
                    if (Message.Contains("Error converting"))
                    {

                    }
                    else
                        CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
            finally
            {
                gvProfileMaster.DataSource = dtAdd;
                gvProfileMaster.DataBind();
                btnedit.Visible = true;
            }
        }

        protected void btnDownloadData_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (drpProgramID.SelectedIndex == 0 || drpProgramID.SelectedIndex == -1)
                {
                    CommonHelper.ShowMessage("Please Select Program ID", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpProgramID.Focus();
                    return;
                }
                blobj = new BL_ProfileMaster();
                DataTable dtProgramDetails = blobj.GetProgramDetails(drpProgramID.SelectedItem.Text, "");
                if (dtProgramDetails.Rows.Count > 0)
                {
                    dtProgramDetails.Columns.Remove("ID");
                    dtProgramDetails.Columns.Remove("P_ID");                   
                    dtProgramDetails.Columns["FeederLoc"].ColumnName = "FEEDER_LOCATON";
                    dtProgramDetails.Columns["ToolID"].ColumnName = "TOOL_ID";
                    dtProgramDetails.AcceptChanges();
                    string sData = PCommon.ToCSV(dtProgramDetails);
                    Response.Clear();
                    Response.Buffer = true;
                    string myName = Server.UrlEncode("ProgramData" + "_" + DateTime.Now.ToShortDateString() + ".csv");
                    Response.AddHeader("content-disposition", "attachment;filename=" + myName);
                    Response.Charset = "";
                    Response.ContentType = "application/text";
                    Response.Output.Write(sData);
                    Response.Flush();
                    Response.End();
                }
                else
                {
                    CommonHelper.ShowMessage("Please Select Program", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpProgramID.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
    }
}