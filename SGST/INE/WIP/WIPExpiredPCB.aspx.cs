using BusinessLayer;
using BusinessLayer.WIP;
using Common;
using PL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace SGST.INE.WIP
{
    public partial class WIPExpiredPCB : System.Web.UI.Page
    {
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
                if (Session["usertype"].ToString().ToUpper() != "ADMIN")
                {
                    string sModuleName = "Expired PCB Permission";
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
                    GetFGItemCode();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        private void GetFGItemCode()
        {
            try
            {
                BL_WIP_AllowedExpiredPCB blobj = new BL_WIP_AllowedExpiredPCB();
                DataTable dt = blobj.BindFgItemCode(Session["SiteCode"].ToString());
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(drpFGItemCode, dt, true);
                }
                else
                {
                    CommonHelper.ShowMessage("No FG Item Code Found", msginfo, CommonHelper.MessageType.Info.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void btnGetDetails_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (drpFGItemCode.SelectedIndex > 0)
                {
                    BL_WIP_AllowedExpiredPCB blobj = new BL_WIP_AllowedExpiredPCB();
                    DataTable dt = blobj.GetDetails(Session["SiteCode"].ToString(), txtWorkOrderNo.Text, drpFGItemCode.Text);
                    lblNumberofRecords.Text = dt.Rows.Count.ToString();
                    if (dt.Rows.Count > 0)
                    {
                        gvPCBCount.DataSource = dt;
                        gvPCBCount.DataBind();
                        dt.TableName = "Table1";
                        dt.AcceptChanges();
                        System.Data.DataView view = new System.Data.DataView(dt.DefaultView.ToTable(true, "MACHINEID"));
                        System.Data.DataTable selected =
                                view.ToTable("Table1", false, "MACHINEID");
                        clsCommon.FillComboBox(drpMachineID, selected, true);
                        ViewState["Data"] = dt;
                    }
                    else
                    {
                        gvPCBCount.DataSource = null;
                        gvPCBCount.DataBind();
                        CommonHelper.ShowMessage("No details Found", msginfo, CommonHelper.MessageType.Info.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                gvPCBCount.DataSource = null;
                gvPCBCount.DataBind();
                txtWorkOrderNo.Text = string.Empty;
                GetFGItemCode();
                if (drpMachineID.Items.Count > 0)
                {
                    drpMachineID.Items.Clear();
                }                
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                DataTable dt = new DataTable();
                if (gvPCBCount.Rows.Count > 0)
                {
                    DataTable dtRigthsData = new DataTable();
                    dtRigthsData.Columns.Add("PCB_ID");
                    foreach (GridViewRow gvRow in gvPCBCount.Rows)
                    {
                        if (gvRow.RowType == DataControlRowType.DataRow)
                        {
                            Label lblModuleID = gvRow.FindControl("lblPCBBarcode") as Label;
                            bool _ViewRights = ((CheckBox)gvRow.FindControl("ChkViewRights")).Checked;
                            if (_ViewRights.ToString() == "True")
                            {
                                dtRigthsData.Rows.Add(lblModuleID.Text.ToUpper());
                            }
                        }
                    }
                    BL_WIP_AllowedExpiredPCB dlobj = new BL_WIP_AllowedExpiredPCB();
                    DataTable dtData = dlobj.UpdateStatus(Session["UserID"].ToString().ToUpper(), dtRigthsData, Session["SiteCode"].ToString().ToUpper());
                    if (dtData.Rows.Count > 0)
                    {
                        string sSaveResult = dtData.Rows[0][0].ToString();
                        if (sSaveResult.StartsWith("N~") || sSaveResult.StartsWith("ERROR~"))
                        {
                            CommonHelper.ShowMessage(sSaveResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        }
                        else
                        {
                            CommonHelper.ShowMessage(sSaveResult.Split('~')[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                            gvPCBCount.DataSource = null;
                            gvPCBCount.DataBind();
                            txtWorkOrderNo.Text = string.Empty;
                            GetFGItemCode();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void drpMachineID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpMachineID.SelectedIndex > 0)
                {
                    DataTable dt = (DataTable)ViewState["Data"];
                    DataView dataView = dt.DefaultView;
                    dataView.RowFilter = "MACHINEID = '" + drpMachineID.SelectedValue + "'";
                    gvPCBCount.DataSource = dataView;
                    gvPCBCount.DataBind();
                }
                else
                {
                    DataTable dt = (DataTable)ViewState["Data"];
                    gvPCBCount.DataSource = dt;
                    gvPCBCount.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        protected void btnDownloadData_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (ViewState["Data"] == null)
                {
                    CommonHelper.ShowCustomErrorMessage("Please select fg item code", msgerror);
                    return;
                }
                DataTable dt = (DataTable)ViewState["Data"];
                if (dt.Rows.Count > 0)
                {
                    dt.Columns.RemoveAt(0);
                    dt.AcceptChanges();        
                    string sData = PCommon.ToCSV(dt);
                    Response.Clear();
                    Response.Buffer = true;
                    string myName = Server.UrlEncode("ExpiredPCBList" + "_" + DateTime.Now.ToShortDateString() + ".csv");
                    Response.AddHeader("content-disposition", "attachment;filename=" + myName);
                    Response.Charset = "";
                    Response.ContentType = "application/text";
                    Response.Output.Write(sData);
                    Response.Flush();
                    Response.End();
                }
                else
                {
                    CommonHelper.ShowMessage("Please select FG Item Code", msgerror, CommonHelper.MessageType.Error.ToString());
                    
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
    }
}