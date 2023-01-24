using Common;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer.Masters;
using BusinessLayer;

namespace SGST.INE.Masters
{
    public partial class LabelPrinting : System.Web.UI.Page
    {
        BL_PrintingMaster blobj = new BL_PrintingMaster();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["usertype"].ToString() != "ADMIN")
                {
                    string _strRights = CommonHelper.GetRights("Master Data Label Printing", (DataTable)Session["USER_RIGHTS"]);
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
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            ddlPrinter.SelectedIndex = 0;
            lblNumberofRecords.Text = "0";
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
                    clsCommon.FillComboBox(ddlPrinter, dt, true);
                    ddlPrinter.SelectedIndex = 0;
                    ddlPrinter.Focus();
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
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlPrinter.SelectedIndex == 0 && dvPrintergrup.Visible == true)
                {
                    CommonHelper.ShowMessage("Please select printer.", msgerror, CommonHelper.MessageType.Error.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    return;
                }
                string sLineCode = Session["LINECODE"].ToString();
                string sUserID = Session["UserID"].ToString();
                BL_PrintingMaster blobj = new BL_PrintingMaster();
                string sResult = "";
                foreach (GridViewRow item in gv_printingmst.Rows)
                {
                    if (item.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chk = (item.FindControl("chkSelect") as CheckBox);
                        if (chk.Checked)
                        {
                            string lbltoolid = gv_printingmst.Rows[item.RowIndex].Cells[1].Text;
                            string lbldescription = gv_printingmst.Rows[item.RowIndex].Cells[2].Text;
                            sResult = blobj.ToolBinPrinting(lbltoolid, ddlPrinter.Text, lbldescription, ddltype.SelectedItem.Text
                               , sUserID, sLineCode
                                );
                        }
                    }
                }
                if (sResult.Length > 0)
                {
                    if (sResult.StartsWith("ERROR"))
                    {
                        CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    else if (sResult.StartsWith("N~"))
                    {
                        CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    else if (sResult.StartsWith("PRINTERNOTCONNECTED~"))
                    {
                        CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    else if (sResult.StartsWith("PRNNOTFOUND~"))
                    {
                        CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    else if (sResult.StartsWith("SUCCESS~"))
                    {
                        CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                        if (ddltype.SelectedItem.Text == "Part Code")
                        {
                            CommonHelper.ShowMessage("Part code label printed successfully", msgsuccess, CommonHelper.MessageType.Success.ToString());
                        }
                        else
                        {
                            CommonHelper.ShowMessage(sResult.Split('~')[1], msgsuccess, CommonHelper.MessageType.Success.ToString());

                        }
                        gv_printingmst.DataSource = null;
                        gv_printingmst.DataBind();
                        ddltype.SelectedIndex = 0;
                        lblNumberofRecords.Text = "0";
                    }
                    else
                    {
                        CommonHelper.ShowMessage(sResult.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                }
                else
                {
                    CommonHelper.ShowMessage("No result found for printing.", msgerror, CommonHelper.MessageType.Error.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        private void ShowGridData2()
        {
            try
            {
                blobj = new BL_PrintingMaster();
                DataTable dt = new DataTable();
                dt = blobj.GetData(ddltype.SelectedItem.Text);
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
        protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowGridData2();
            gv_printingmst.Visible = true;
        }
        protected void gv_printingmst_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gv_printingmst.PageIndex = e.NewPageIndex;
                this.ShowGridData2();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
    }
}
