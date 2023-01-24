using BusinessLayer;
using BusinessLayer.WIP;
using Common;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGST.INE.WIP
{
    public partial class WIPLabelReprinting : System.Web.UI.Page
    {
        BL_WIP_LabelReprint blobj = new BL_WIP_LabelReprint();
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Signin/v1/Login.aspx?Session=Null");
            }
        }
        private void getprinterlist()
        {
            try
            {
                BL_Common blCommonobj = new BL_Common();
                DataTable dt = blCommonobj.BINDPRINTER("WIP");
                if (dt.Rows.Count > 0)
                {
                    CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                    clsCommon.FillComboBox(drpPrinterName, dt, true);
                    drpPrinterName.Focus();
                }
                else
                {
                    CommonHelper.ShowMessage("Printer not available", msgerror, CommonHelper.MessageType.Error.ToString());
                }

            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        public void BindPartCode(string stype)
        {
            try
            {
                blobj = new BL_WIP_LabelReprint();
                DataTable dtPcode = blobj.BindINELPartNo(stype);
                gv_LabelReprint.DataSource = null;
                gv_LabelReprint.DataBind();
                if (dtPcode.Rows.Count > 0)
                {
                    CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                    clsCommon.FillComboBox(drpItemCode, dtPcode, true);
                    drpItemCode.Focus();
                }
                else
                {
                    CommonHelper.ShowMessage("Part Code not available", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpItemCode.Items.Clear();
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
                if (drpItemCode.SelectedIndex > 0)
                {
                    blobj = new BL_WIP_LabelReprint();
                    DataTable dt = new DataTable();
                    dt = blobj.BindReelBarcode(drpItemCode.Text.Trim(), ddltype.SelectedItem.Text);
                    lblNumberofRecords.Text = dt.Rows.Count.ToString();
                    if (dt.Rows.Count > 0)
                    {
                        dt.TableName = "Table1";
                        dt.AcceptChanges();
                        ViewState["Data"] = dt;
                        System.Data.DataView view = new System.Data.DataView(dt.DefaultView.ToTable(true, "PART_BARCODE"));
                        System.Data.DataTable selected =
                                view.ToTable("Table1", false, "PART_BARCODE");
                        drpBarcode.Items.Clear();
                        if (selected.Rows.Count > 0)
                        {
                            clsCommon.FillComboBox(drpBarcode, selected, true);
                            drpBarcode.Focus();
                        }

                        gv_LabelReprint.DataSource = dt;
                        gv_LabelReprint.DataBind();
                    }
                    else
                    {
                        gv_LabelReprint.DataSource = null;
                        gv_LabelReprint.DataBind();
                    }
                }
                else
                {
                    gv_LabelReprint.DataSource = null;
                    gv_LabelReprint.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usertype"].ToString() != "ADMIN")
            {
                string _strRights = CommonHelper.GetRights("WIP LABEL RE-PRINTING", (DataTable)Session["USER_RIGHTS"]);
                CommonHelper._strRights = _strRights.Split('^');
                if (CommonHelper._strRights[0] == "0")  //Check view rights
                {
                    Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                }
            }
            Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
            Response.AddHeader("Pragma", "no-cache");
            Response.AddHeader("Expires", "0");
            if (!IsPostBack)
            {
                try
                {
                    BindReasonReprint();
                    dvPrintergrup.Visible = true;
                    if (PCommon.sUseNetworkPrinter == "1")
                    {

                        getprinterlist();
                    }
                    else
                    {
                        dvPrintergrup.Visible = false;

                    }
                }
                catch (Exception ex)
                {
                    CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                }
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (drpItemCode.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select item code", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpItemCode.Focus();
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    return;
                }
                if (dvPrintergrup.Visible == true && drpPrinterName.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select printer.", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpPrinterName.Focus();
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    return;
                }
                if (ddlReasonofReprint.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select Reason of Reprint.", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddlReasonofReprint.Focus();
                    return;
                }

                blobj = new BL_WIP_LabelReprint();
                string sResult = "";
                string sCustomer = string.Empty;
                string sFGitemcode = string.Empty;
                string sPartCode = string.Empty;
                string sLineCode = Session["LINECODE"].ToString();
                string sUserID = Session["UserID"].ToString();
                foreach (GridViewRow item in gv_LabelReprint.Rows)
                {
                    if (item.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chk = (item.FindControl("chkSelect") as CheckBox);
                        if (chk.Checked)
                        {
                            string lblPartBarcode = gv_LabelReprint.Rows[item.RowIndex].Cells[1].Text;
                            string lblBatchNo = gv_LabelReprint.Rows[item.RowIndex].Cells[2].Text;
                            string lblQty = gv_LabelReprint.Rows[item.RowIndex].Cells[3].Text;
                            sCustomer = gv_LabelReprint.Rows[item.RowIndex].Cells[4].Text;
                            sFGitemcode = gv_LabelReprint.Rows[item.RowIndex].Cells[5].Text;
                            string sMobileLabel = gv_LabelReprint.Rows[item.RowIndex].Cells[6].Text.Replace("&nbsp;", "").Trim();
                            if (sMobileLabel.Length > 0)
                            {
                                sResult = blobj.Reprint(drpItemCode.Text, lblPartBarcode, Session["UserID"].ToString()
, Convert.ToDecimal(lblQty), drpPrinterName.Text, ddltype.Text, sCustomer, sFGitemcode, sPartCode, "1", sMobileLabel
, sLineCode, ddlReasonofReprint.SelectedItem.Text.Trim(), txtRemarks.Text
);
                            }
                            else
                            {
                                sResult = blobj.Reprint(drpItemCode.Text, lblPartBarcode, Session["UserID"].ToString()
, Convert.ToDecimal(lblQty), drpPrinterName.Text, ddltype.Text, sCustomer, sFGitemcode, sPartCode, "0", sMobileLabel
, sLineCode, ddlReasonofReprint.SelectedItem.Text.Trim(), txtRemarks.Text
);
                            }
                            System.Threading.Thread.Sleep(2000);
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
                        CommonHelper.ShowMessage(sResult.Split('~')[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
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

        protected void btnReset_Click(object sender, EventArgs e)
        {
            msgdiv.InnerText = "";
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            gv_LabelReprint.DataSource = null;
            gv_LabelReprint.DataBind();
            drpItemCode.Items.Clear();
            lblNumberofRecords.Text = "0";
        }

        protected void gv_LabelReprint_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_LabelReprint.PageIndex = e.NewPageIndex;
            this.ShowGridData();
        }

        protected void drpItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddltype.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select type", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddltype.Focus();
                    return;
                }
                ShowGridData();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddltype.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please select type", msgerror, CommonHelper.MessageType.Error.ToString());
                    ddltype.Focus();
                    return;
                }
                BindPartCode(ddltype.Text);
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void drpBarcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpBarcode.SelectedIndex > 0)
                {
                    DataTable dt = (DataTable)ViewState["Data"];
                    DataView dataView = dt.DefaultView;
                    dataView.RowFilter = "PART_BARCODE = '" + drpBarcode.SelectedValue + "'";
                    gv_LabelReprint.DataSource = dataView;
                    gv_LabelReprint.DataBind();
                }
                else
                {
                    DataTable dt = (DataTable)ViewState["Data"];
                    gv_LabelReprint.DataSource = dt;
                    gv_LabelReprint.DataBind();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void BindReasonReprint()
        {
            try
            {


                ddlReasonofReprint.Items.Clear();
                BL_MobCommon blobj = new BL_MobCommon();
                string sResult = string.Empty;
                DataTable dtReasonofReprint = blobj.BindReasonReprint();
                if (dtReasonofReprint.Rows.Count > 0)
                {
                    CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                    clsCommon.FillMultiColumnsCombo(ddlReasonofReprint, dtReasonofReprint, true);
                    ddlReasonofReprint.SelectedIndex = 0;
                    ddlReasonofReprint.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError,
               System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}