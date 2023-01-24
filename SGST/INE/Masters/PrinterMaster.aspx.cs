using Common;
using System;
using System.Data;
using BusinessLayer;
using System.Net;
using System.Web.UI.WebControls;

namespace SGST.INE.Masters
{
    public partial class PrinterMaster : System.Web.UI.Page
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
                if (Session["usertype"].ToString() != "ADMIN")
                {
                    string _strRights = CommonHelper.GetRights("PRINTER MASTER", (DataTable)Session["USER_RIGHTS"]);
                    CommonHelper._strRights = _strRights.Split('^');
                    if (CommonHelper._strRights[0] == "0")  //Check view rights
                    {
                        Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                    }
                }

                if (!IsPostBack)
                {
                    ShowGridData();
                    txtPrinterIP.Focus();
                }
                Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
                Response.AddHeader("Pragma", "no-cache");
                Response.AddHeader("Expires", "0");
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvPrinterMaster.PageIndex = e.NewPageIndex;
                ShowGridData();
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
                string[] strValue = e.CommandArgument.ToString().Split('~');
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

        private void DeleteRecords(string _SN)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                BL_PrinterMaster blobj = new BL_PrinterMaster();
                string sResult = blobj.Deleteprinters(_SN);
                if (sResult == "1")
                {
                    txtPrinterIP.Text = "";
                    drpType.SelectedIndex = 0;
                    ShowGridData();
                    CommonHelper.ShowMessage("Printer deleted successfully.", msgsuccess, CommonHelper.MessageType.Error.ToString());
                    return;
                }
                else
                {
                    CommonHelper.ShowMessage("No data found.", msgerror, CommonHelper.MessageType.Error.ToString());
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

        private void ShowGridData()
        {
            try
            {
                BL_PrinterMaster dlobj = new BL_PrinterMaster();
                DataTable dt = dlobj.Getprinters();
                lblNumberofRecords.Text = dt.Rows.Count.ToString();
                if (dt.Rows.Count == 0)
                {
                    gvPrinterMaster.DataSource = null;
                    gvPrinterMaster.DataBind();
                }
                else
                if (dt.Rows.Count > 0)
                {
                    gvPrinterMaster.DataSource = dt;
                    gvPrinterMaster.DataBind();                   
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
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (txtPrinterIP.Text == string.Empty)
                {
                    CommonHelper.ShowMessage("Please enter printer IP.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPrinterIP.Focus();
                    return;
                }
                if (drpType.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select printer type.", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpType.Focus();
                    return;

                }
                if (validatePrinterIP(txtPrinterIP.Text.Trim()) == false)
                {
                    CommonHelper.ShowMessage("Please enter correct printer IP.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPrinterIP.Focus();
                    txtPrinterIP.Text = "";
                    return;
                }
                BL_PrinterMaster blobj = new BL_PrinterMaster();
                string sResult = blobj.SavePrinter(txtPrinterIP.Text.Trim(), drpType.SelectedItem.Text.Trim());
                if (sResult.StartsWith("Duplicate~"))
                {
                    CommonHelper.ShowMessage("Record Already Exist.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPrinterIP.Text = "";
                    drpType.SelectedIndex = 0;
                    txtPrinterIP.Focus();
                }
                else if (sResult.StartsWith("Success~"))
                {
                    ShowGridData();
                    CommonHelper.ShowMessage("Data saved Sucessfully.", msgsuccess, CommonHelper.MessageType.Error.ToString());
                    txtPrinterIP.Text = "";
                    drpType.SelectedIndex = 0;
                    txtPrinterIP.Focus();
                }
                else if (sResult.Contains("Violation of PRIMARY KEY constraint"))
                {
                    CommonHelper.ShowMessage("Printer IP is already allocated. Please enter different IP", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPrinterIP.Text = "";
                    return;
                }
                else
                {
                    CommonHelper.ShowMessage("No result found.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPrinterIP.Text = "";
                    drpType.SelectedIndex = 0;
                    txtPrinterIP.Focus();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("REFERENCE constraint"))
                {
                    CommonHelper.ShowMessage("Printer already in transaction, it can not be delete ", msgerror, CommonHelper.MessageType.Error.ToString());
                    return;
                }
                if (ex.Message.Contains("Violation of PRIMARY KEY constraint"))
                {
                    CommonHelper.ShowMessage("Printer IP is already allocated. Please enter different IP", msgerror, CommonHelper.MessageType.Error.ToString());
                    return;
                }
                else
                {
                    PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                }
                return;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        public bool validatePrinterIP(string ipAddr)
        {
            IPAddress IP;
            bool flag = IPAddress.TryParse(ipAddr, out IP);
            if (flag)
                return true;
            else
                return false;
        }

        public void Reset()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            txtPrinterIP.Focus();
            txtPrinterIP.Text = "";
            drpType.SelectedIndex = 0;
        }

        protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}