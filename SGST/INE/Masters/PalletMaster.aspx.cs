using Common;
using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer.Masters;
using BusinessLayer;

namespace SGST.INE.Masters
{
    public partial class PalletMaster : System.Web.UI.Page
    {
        BL_PrintingMaster blobj = new BL_PrintingMaster();
        string Message = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["usertype"].ToString() != "ADMIN")
                {
                    string _strRights = CommonHelper.GetRights("PALLET PRINTING", (DataTable)Session["USER_RIGHTS"]);
                    CommonHelper._strRights = _strRights.Split('^');
                    if (CommonHelper._strRights[0] == "0")  //Check view rights
                    {
                        Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                    }
                }
                if (!IsPostBack)
                {
                    dvPrintergrup.Visible = true;
                    if (PCommon.sUseNetworkPrinter == "1")
                    {
                        bindPRINTER();
                    }
                    else
                    {
                        dvPrintergrup.Visible = false;
                    }
                    ShowGridData();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }

        }
        protected void gv_printingmst_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_printingmst.PageIndex = e.NewPageIndex;
            this.ShowGridData();
        }
        public void bindPRINTER()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                string sPrinter = string.Empty;
                BL_Common blobj = new BL_Common();
                DataTable dt = blobj.BINDPRINTER("RM");
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(drpPrinterName, dt, true);
                    drpPrinterName.SelectedIndex = 0;
                    drpPrinterName.Focus();
                }
                else
                {
                    CommonHelper.ShowMessage("No result found.", msgerror, CommonHelper.MessageType.Error.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }

        private void ShowGridData()
        {
            try
            {
                blobj = new BL_PrintingMaster();
                DataTable dt = new DataTable();
                dt = blobj.GetDataForPallet();
                lblNumberofRecords.Text = dt.Rows.Count.ToString(); 
                if (dt.Rows.Count > 0)
                {
                    gv_printingmst.DataSource = dt;
                    gv_printingmst.DataBind();
                }
                else
                {
                    gv_printingmst.DataSource = null;
                    gv_printingmst.DataBind();

                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string sLineCode = Session["LINECODE"].ToString();
                string sUserID = Session["UserID"].ToString();
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                string _sPrinterPort = ConfigurationManager.AppSettings["sPrinterPort"].ToString();
                blobj = new BL_PrintingMaster();
                string _Result = blobj.PalletPrinting(drpPrinterName.Text, _sPrinterPort, sUserID, sLineCode);
                Message = _Result;
                string[] msg = Message.Split('~');
                if (Message.Length > 0)
                {
                    if (Message.Contains("SUCCESS~"))
                    {
                        CommonHelper.ShowMessage(msg[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                    }
                }
                if (Message.Length > 0)
                {
                    if (Message.Contains("N~"))
                    {
                        CommonHelper.ShowMessage(msg[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }
                    else if (Message.Contains("PRNNOTFOUND~"))
                    {
                        CommonHelper.ShowMessage("PRN for RM printing is not available.", msgerror, CommonHelper.MessageType.Error.ToString());
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                        ShowGridData();
                        return;
                    }
                    else if (Message.Contains("PRINTERNOTCONNECTED~"))
                    {
                        CommonHelper.ShowMessage("Printer is not attached or printer is offline. Printing failed.", msgerror, CommonHelper.MessageType.Error.ToString());
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                        ShowGridData();
                        return;
                    }
                    else if (Message.Contains("SUCCESS~"))
                    {
                        CommonHelper.ShowMessage(msg[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                        ShowGridData();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                if (ex.Message.Contains("The string was not recognized as a valid DateTime."))
                {
                    CommonHelper.ShowMessage("Date is not in correct format", msgerror, CommonHelper.MessageType.Error.ToString());
                    ShowGridData();
                }
                else
                {
                    CommonHelper.ShowMessage(ex.Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    ShowGridData();
                }
            }
        }
    }
}