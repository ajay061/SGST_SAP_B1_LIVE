using BusinessLayer;
using Common;
using PL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace SGST.INE.UserManagement
{
    public partial class UserRights : System.Web.UI.Page
    {
        string _Department = "";
        bool _ViewRights;
        string Message = string.Empty;
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
                _Department = Session["Department"].ToString();
                if (Session["usertype"].ToString().ToUpper() != "ADMIN")
                {
                    string sModuleName = "User Rights";
                    string _strRights = CommonHelper.GetRights(sModuleName, (DataTable)Session["USER_RIGHTS"]);
                    CommonHelper._strRights = _strRights.Split('^');
                    if (CommonHelper._strRights[0] == "0")  //Check view rights
                    {
                        Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                    }
                }
                if (!IsPostBack)
                {
                    Session["UserRights"] = null;
                    GetAllModules();
                    GetAllUsers();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        private void GetAllUsers()
        {
            try
            {
                ddlUserID.Items.Add(new ListItem("--Select User ID--", "0", true));
                List<PL_GroupRights> UserDetails = new List<PL_GroupRights>();
                PL_GroupRights entity = null;
                BL_UserRights blobj = new BL_UserRights();
                DataTable dt = blobj.BINDUSERS(_Department);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        entity = new PL_GroupRights()
                        {
                            USERID = dt.Rows[i]["UserID"].ToString(),
                            RESULT = dt.Rows[i][0].ToString(),
                        }; UserDetails.Add(entity);
                    }
                }
                else
                {
                    CommonHelper.ShowMessage("No standard user found.", msginfo, CommonHelper.MessageType.Info.ToString());
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, Message);
                }
                if (UserDetails.Count > 0)
                {
                    foreach (PL_GroupRights c in UserDetails)
                    {
                        ddlUserID.Items.Add(c.USERID);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        private void GetAllModules()
        {
            try
            {
                GVGrpRights.DataSource = null;
                GVGrpRights.DataBind();
                BL_UserRights blobj = new BL_UserRights();
                DataTable dt = blobj.BindModule(_Department);
                if (dt.Rows.Count > 0)
                {
                    System.Data.DataColumn newColumn = new System.Data.DataColumn("VIEW_RIGHTS", typeof(System.String));
                    newColumn.DefaultValue = "false";
                    dt.Columns.Add(newColumn);
                    dt.AcceptChanges();
                    GVGrpRights.DataSource = dt;
                    GVGrpRights.DataBind();
                    lblNumberofRecords.Text = dt.Rows.Count.ToString();
                    dt.TableName = "Table1";
                    dt.AcceptChanges();
                    System.Data.DataView view = new System.Data.DataView(dt.DefaultView.ToTable());
                    drpUserType.Items.Clear();
                    System.Data.DataTable selected =
                            view.ToTable("Table1", true, "MODULETYPE");
                    if (selected.Rows.Count > 0)
                    {
                        clsCommon.FillComboBox(drpUserType, selected, true);
                    }
                    ViewState["Data"] = dt;
                    Session["UserRights"] = dt;
                }
                else
                {
                    lblNumberofRecords.Text = "0";
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                DataTable dt = new DataTable();
                if (ddlUserID.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select User ID.", msgerror, CommonHelper.MessageType.Error.ToString());
                    return;
                }
                if (GVGrpRights.Rows.Count > 0)
                {
                    DataTable dtData = new DataTable();
                    if (Session["UserRights"] != null)
                    {
                        dtData = (DataTable)Session["UserRights"];
                    }
                    else
                    {
                        dtData = (DataTable)ViewState["Data"];
                    }
                    DataTable dtRigthsData = new DataTable();
                    dtRigthsData.Columns.Add("MODULENAME");
                    dtRigthsData.Columns.Add("VIEW_RIGHTS");
                    foreach (GridViewRow gvRow in GVGrpRights.Rows)
                    {
                        if (gvRow.RowType == DataControlRowType.DataRow)
                        {
                            Label lblModuleID = gvRow.FindControl("lblModuleID") as Label;
                            _ViewRights = ((CheckBox)gvRow.FindControl("ChkViewRights")).Checked;
                            if (_ViewRights.ToString() == "True")
                            {
                                dtRigthsData.Rows.Add(lblModuleID.Text.ToUpper(), true);
                            }
                        }
                    }

                    dtData.AsEnumerable().Join(dtRigthsData.AsEnumerable(), _dtmater => Convert.ToString(_dtmater["MODULENAME"]).ToUpper(),
         _dtchild => Convert.ToString(_dtchild["MODULENAME"]).ToUpper(), (_dtmater, _dtchild) => new { _dtmater, _dtchild }).ToList().ForEach(o => o._dtmater.SetField("VIEW_RIGHTS", o._dtchild["VIEW_RIGHTS"].ToString()));

                    DataTable dtModuleName = dtRigthsData.Select("VIEW_RIGHTS = 'True'").CopyToDataTable().DefaultView.ToTable(true, "MODULENAME");
                    BL_UserRights dlobj = new BL_UserRights();
                    string sSaveResult = dlobj.SaveUserRights(ddlUserID.Text.Trim().ToUpper(), dtModuleName, Session["UserID"].ToString().ToUpper());
                    if (sSaveResult.Length > 0)
                    {
                        if (sSaveResult.StartsWith("N~") || sSaveResult.StartsWith("ERROR~"))
                        {
                            CommonHelper.ShowMessage(sSaveResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        }
                        else
                        {
                            CommonHelper.ShowMessage(sSaveResult.Split('~')[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                        }
                    }
                    else
                    {
                        CommonHelper.ShowMessage(sSaveResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    }

                }
                ddlUserID.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
            finally
            {
                DataTable dtData = (DataTable)ViewState["Data"];
                DataView dataView1 = dtData.DefaultView;
                dataView1.RowFilter = string.Empty;
                GVGrpRights.DataSource = null;
                GVGrpRights.DataBind();
                GVGrpRights.Dispose();
                GVGrpRights.DataSource = dataView1;
                GVGrpRights.DataBind();
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            ddlUserID.SelectedIndex = 0;
            GetAllModules();
            btnSave.Text = "Save Rights";
            Session["UserRights"] = null;
        }
        protected void ddlUserID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Text == "Update Rights")
                {
                    btnSave.Text = "Save Rights";
                }
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (ddlUserID.SelectedIndex == 0)
                {
                    GetAllModules();
                    return;
                }
                if (drpUserType.Items.Count > 0)
                {
                    drpUserType.SelectedIndex = 0;
                }
                BL_UserRights blobj = new BL_UserRights();
                DataTable dtUserRights = blobj.GetAllUserRights(ddlUserID.Text.Trim());
                if (dtUserRights.Rows.Count > 0)
                {
                    DataTable dtData = (DataTable)ViewState["Data"];
                    dtData.AsEnumerable().Join(dtUserRights.AsEnumerable(), _dtmater => Convert.ToString(_dtmater["MODULENAME"]),
         _dtchild => Convert.ToString(_dtchild["MODULENAME"]), (_dtmater, _dtchild) => new { _dtmater, _dtchild }).ToList().ForEach(o => o._dtmater.SetField("VIEW_RIGHTS", o._dtchild["VIEW_RIGHTS"].ToString()));
                    Session["UserRights"] = dtData;
                    GVGrpRights.DataSource = dtData;
                    GVGrpRights.DataBind();
                    lblNumberofRecords.Text = dtData.Rows.Count.ToString();
                    dtData.TableName = "Table1";
                    dtData.AcceptChanges();
                    System.Data.DataView view = new System.Data.DataView(dtData.DefaultView.ToTable());
                    drpUserType.Items.Clear();
                    System.Data.DataTable selected =
                            view.ToTable("Table1", true, "MODULETYPE");
                    if (selected.Rows.Count > 0)
                    {
                        clsCommon.FillComboBox(drpUserType, selected, true);
                    }
                    btnSave.Text = "Update Rights";
                }
                else
                {
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, "No Data found");
                }


            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void drpUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtData = new DataTable();
                if (Session["UserRights"] != null)
                {
                    dtData = (DataTable)Session["UserRights"];
                }
                else
                {
                    dtData = (DataTable)ViewState["Data"];
                }
                DataTable dtRigthsData = new DataTable();
                dtRigthsData.Columns.Add("MODULENAME");
                dtRigthsData.Columns.Add("VIEW_RIGHTS");
                foreach (GridViewRow gvRow in GVGrpRights.Rows)
                {
                    if (gvRow.RowType == DataControlRowType.DataRow)
                    {
                        Label lblModuleID = gvRow.FindControl("lblModuleID") as Label;
                        _ViewRights = ((CheckBox)gvRow.FindControl("ChkViewRights")).Checked;
                        if (_ViewRights.ToString() == "True")
                        {
                            dtRigthsData.Rows.Add(lblModuleID.Text.ToUpper(), true);
                        }
                    }
                }

                dtData.AsEnumerable().Join(dtRigthsData.AsEnumerable(), _dtmater => Convert.ToString(_dtmater["MODULENAME"]).ToUpper(),
     _dtchild => Convert.ToString(_dtchild["MODULENAME"]).ToUpper(), (_dtmater, _dtchild) => new { _dtmater, _dtchild }).ToList().ForEach(o => o._dtmater.SetField("VIEW_RIGHTS", o._dtchild["VIEW_RIGHTS"].ToString()));
                Session["UserRights"] = dtData;
                if (drpUserType.SelectedIndex > 0)
                {
                    DataView dataView = dtData.DefaultView;
                    dataView.RowFilter = "MODULETYPE = '" + drpUserType.SelectedValue + "'";
                    GVGrpRights.DataSource = dataView;
                    GVGrpRights.DataBind();
                }
                else
                {
                    DataView dataView1 = dtData.DefaultView;
                    dataView1.RowFilter = string.Empty;
                    GVGrpRights.DataSource = null;
                    GVGrpRights.DataBind();
                    GVGrpRights.Dispose();
                    GVGrpRights.DataSource = dataView1;
                    GVGrpRights.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void Checked_UncheckedAllRights(bool bRights)
        {
            try
            {
                DataTable dtData = new DataTable();
                if (Session["UserRights"] != null)
                {
                    dtData = (DataTable)Session["UserRights"];
                }
                else
                {
                    dtData = (DataTable)ViewState["Data"];
                }
                DataTable dtRigthsData = new DataTable();
                dtRigthsData.Columns.Add("MODULENAME");
                dtRigthsData.Columns.Add("VIEW_RIGHTS");
                foreach (GridViewRow gvRow in GVGrpRights.Rows)
                {
                    if (gvRow.RowType == DataControlRowType.DataRow)
                    {
                        Label lblModuleID = gvRow.FindControl("lblModuleID") as Label;
                        _ViewRights = ((CheckBox)gvRow.FindControl("ChkViewRights")).Checked;
                        dtRigthsData.Rows.Add(lblModuleID.Text.ToUpper(), bRights);
                    }
                }

                dtData.AsEnumerable().Join(dtRigthsData.AsEnumerable(), _dtmater => Convert.ToString(_dtmater["MODULENAME"]).ToUpper(),
     _dtchild => Convert.ToString(_dtchild["MODULENAME"]).ToUpper(), (_dtmater, _dtchild) => new { _dtmater, _dtchild }).ToList().ForEach(o => o._dtmater.SetField("VIEW_RIGHTS", o._dtchild["VIEW_RIGHTS"].ToString()));
                Session["UserRights"] = dtData;
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }

        protected void checkAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //if (drpUserType.SelectedIndex > 0)
                //{
                //    CommonHelper.ShowMessage("Please remove filter of department for selected the all.", msgerror, CommonHelper.MessageType.Error.ToString());
                //    drpUserType.Focus();
                //    foreach (GridViewRow row in GVGrpRights.Rows)
                //    {
                //        CheckBox ChkBoxRows = (CheckBox)row.FindControl("ChkViewRights");
                //        ChkBoxRows.Checked = false;
                //    }
                //    return;
                //}

            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
    }
}