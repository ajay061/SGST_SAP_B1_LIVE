using Common;
using System;
using System.Data;
using System.Web.UI.WebControls;
using BusinessLayer.Masters;

namespace SGST.INE.Masters
{
    public partial class FeederMapping : System.Web.UI.Page
    {
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
                if (Session["usertype"].ToString() != "ADMIN")
                {
                    string _strRights = CommonHelper.GetRights("FEEDER MAPPING", (DataTable)Session["USER_RIGHTS"]);
                    CommonHelper._strRights = _strRights.Split('^');
                    if (CommonHelper._strRights[0] == "0")  //Check view rights
                    {
                        Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                    }
                }
                if (!IsPostBack)
                {
                    ShowGridData();
                    txtFeederNo.Focus();
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
                DataTable dt = new DataTable();
                BL_FeederMapping blobj = new BL_FeederMapping();
                dt = blobj.BindFeederData();
                lblNumberofRecords.Text = dt.Rows.Count.ToString();
                if (dt.Rows.Count > 0)
                {
                    gvfeederData.DataSource = dt;
                    gvfeederData.DataBind();
                    dt.TableName = "Table1";
                    dt.AcceptChanges();
                    System.Data.DataView view = new System.Data.DataView(dt.DefaultView.ToTable(true, "FEEDER_NO"));
                    System.Data.DataTable selected =
                            view.ToTable("Table1", false, "FEEDER_NO");
                    clsCommon.FillComboBox(drpFeederNo, selected, true);
                    ViewState["Data"] = dt;
                }
                else
                {
                    gvfeederData.DataSource = null;
                    gvfeederData.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void gvfeederData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvfeederData.PageIndex = e.NewPageIndex;
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
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (txtFeederNo.Text.Trim() == string.Empty)
                {
                    CommonHelper.ShowMessage("Please enter feeder no", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtFeederNo.Focus();
                    return;
                }
                if (txtFeederID.Text.Trim() == "")
                {
                    CommonHelper.ShowMessage("Please enter feeder id", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtFeederID.Focus();
                    return;
                }
                BL_FeederMapping blobj = new BL_FeederMapping();
                string sResult = blobj.SaveFeederMappingData(txtFeederNo.Text.Trim(), txtFeederID.Text.Trim(), 1);
                if (sResult.Length > 0)
                {
                    if (sResult.StartsWith("ERROR~"))
                    {
                        CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    if (sResult.StartsWith("N~"))
                    {
                        CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    else
                    {
                        _ResetField();
                        ShowGridData();
                        CommonHelper.ShowMessage(sResult.Split('~')[1], msgsuccess, CommonHelper.MessageType.Error.ToString());
                    }
                }
                else
                {
                    CommonHelper.ShowMessage("No result found.", msgerror, CommonHelper.MessageType.Error.ToString());
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.ToUpper().Contains("PRIMARY KEY"))
                {
                    CommonHelper.ShowMessage("Enter data already exist, Please enter new one ", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtFeederID.Text = "";
                    txtFeederNo.Text = "";
                    txtFeederNo.Focus();
                }
                else
                {
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                    CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                }
            }
            finally
            {
                ShowGridData();
            }
        }
        private void _ResetField()
        {
            txtFeederID.Text = "";
            txtFeederNo.Text = "";
            btnSave.Text = "Save";
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
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

        protected void drpFeederNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpFeederNo.SelectedIndex > 0)
                {
                    DataTable dt = (DataTable)ViewState["Data"];
                    DataView dataView = dt.DefaultView;
                    dataView.RowFilter = "FEEDER_NO = '" + drpFeederNo.SelectedValue + "'";
                    gvfeederData.DataSource = dataView;
                    gvfeederData.DataBind();
                }
                else
                {
                    DataTable dt = (DataTable)ViewState["Data"];
                    gvfeederData.DataSource = dt;
                    gvfeederData.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}