using BusinessLayer;
using BusinessLayer.WIP;
using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGST.INE.WIP
{
    public partial class WIPPCBRePrininting : System.Web.UI.Page
    {
        BL_WIP_LabelReprint blobj = new BL_WIP_LabelReprint();
        static int iPageLabelCount = 4;
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

        private void BindFGItemCode()
        {
            try
            {
                drpFGItemCode.Items.Clear();
                drpWorkOrderNo.Items.Clear();
                drpPendingBarcode.Items.Clear();
                dvLaserFileData.DataSource = null;
                dvLaserFileData.DataBind();
                blobj = new BL_WIP_LabelReprint();
                DataTable dt = blobj.BindFGItemCode();
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(drpFGItemCode, dt, true);
                    drpFGItemCode.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        private void BindWorkOrderNo()
        {
            try
            {
                drpWorkOrderNo.Items.Clear();
                drpPendingBarcode.Items.Clear();
                dvLaserFileData.DataSource = null;
                dvLaserFileData.DataBind();
                if (drpFGItemCode.SelectedIndex > 0)
                {
                    blobj = new BL_WIP_LabelReprint();
                    DataTable dt = blobj.BindPrintedWorkOrderNo(drpFGItemCode.SelectedItem.Text);
                    if (dt.Rows.Count > 0)
                    {
                        clsCommon.FillComboBox(drpWorkOrderNo, dt, true);
                        drpWorkOrderNo.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        private void BindPrintedReels()
        {
            try
            {
                drpPendingBarcode.Items.Clear();
                dvLaserFileData.DataSource = null;
                dvLaserFileData.DataBind();
                if (drpWorkOrderNo.SelectedIndex > 0)
                {
                    blobj = new BL_WIP_LabelReprint();
                    DataTable dt = blobj.BindPrintedReels(drpFGItemCode.SelectedItem.Text, drpWorkOrderNo.SelectedItem.Text);
                    if (dt.Rows.Count > 0)
                    {
                        clsCommon.FillComboBox(drpPendingBarcode, dt, true);
                        drpPendingBarcode.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        private void GetPageLabelCount()
        {
            try
            {
                iPageLabelCount = 4;
                blobj = new BL_WIP_LabelReprint();
                DataTable dt = blobj.GetPageLabelCount(drpFGItemCode.Text);
                if (dt.Rows.Count > 0)
                {
                    iPageLabelCount = Convert.ToInt32(dt.Rows[0][0].ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        private void bindData()
        {
            try
            {
                dvLaserFileData.DataSource = null;
                dvLaserFileData.DataBind();
                if (drpPendingBarcode.SelectedIndex > 0)
                {
                    blobj = new BL_WIP_LabelReprint();
                    DataTable dt = new DataTable();
                    dt = blobj.GetData(drpPendingBarcode.SelectedItem.Text);
                    if (dt.Rows.Count > 0)
                    {
                        dvLaserFileData.DataSource = dt;
                        dvLaserFileData.DataBind();
                    }
                    GetPageLabelCount();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usertype"].ToString() != "ADMIN")
            {
                string _strRights = CommonHelper.GetRights("PCB LABEL RE-PRINTING", (DataTable)Session["USER_RIGHTS"]);
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
                    dvPrintergrup.Visible = true;
                    if (PCommon.sUseNetworkPrinter == "1")
                    {
                        getprinterlist();
                    }
                    else
                    {
                        dvPrintergrup.Visible = false;
                    }
                    BindFGItemCode();
                }
                catch (Exception ex)
                {
                    CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                }
            }
        }

        protected void drpFGItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindWorkOrderNo();
        }

        protected void drpWorkOrderNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindPrintedReels();
        }

        protected void drpPendingBarcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpPendingBarcode.SelectedIndex > 0)
                {
                    bindData();
                }
                else
                {
                    dvLaserFileData.DataSource = null;
                    dvLaserFileData.DataBind();
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
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (dvLaserFileData.Rows.Count == 0)
                {
                    CommonHelper.ShowMessage("Please select pending reels", msgerror, CommonHelper.MessageType.Error.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    return;
                }
                if (drpPrinterName.SelectedIndex == 0 && dvPrintergrup.Visible == true)
                {
                    CommonHelper.ShowMessage("Please select printer.", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpPrinterName.Focus();
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    return;
                }
                blobj = new BL_WIP_LabelReprint();
                string sResult = "";
                string sPartBarcode = string.Empty;
                string sPartCode = string.Empty;
                string sFGItemCode = string.Empty;
                string sTMONO = string.Empty;
                string sCustomer = string.Empty;
                string sSiteCode = Session["SiteCode"].ToString();
                string sLineCode = Session["LINECODE"].ToString();
                string sUserID = Session["UserID"].ToString();

                List<String> ht = new List<String>();
                List<String> lstTMONo = new List<String>();
                foreach (GridViewRow item in dvLaserFileData.Rows)
                {
                    if (item.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chk = (item.FindControl("chkSelect") as CheckBox);
                        if (chk.Checked)
                        {
                            sPartCode = dvLaserFileData.Rows[item.RowIndex].Cells[1].Text;
                            sCustomer = dvLaserFileData.Rows[item.RowIndex].Cells[4].Text;
                            sPartBarcode = dvLaserFileData.Rows[item.RowIndex].Cells[5].Text;
                            sFGItemCode = dvLaserFileData.Rows[item.RowIndex].Cells[7].Text;
                            sTMONO = dvLaserFileData.Rows[item.RowIndex].Cells[8].Text;
                            ht.Add(sPartBarcode);
                            lstTMONo.Add(sTMONO);
                        }
                    }
                }
                int iNewRow = iPageLabelCount;
                int iCounter = iPageLabelCount - 1;
                int TotalQty = ht.Count;
                int iFinalCounter = 0;
                int iPrintqty = 0;
                int iChildQty = TotalQty / iNewRow;
                string sPrintedLabel = string.Empty;
                string sTMOPrintLabel = string.Empty;
                for (int i = 0; i < TotalQty; i++)
                {
                    if (iChildQty > iPrintqty)
                    {

                    }
                    else
                    {
                        if (iFinalCounter == 0)
                        {
                            iFinalCounter = TotalQty - i;
                            iCounter = i + iFinalCounter - 1;
                        }
                    }
                    sPartBarcode = ht[i].ToString();
                    sTMONO = lstTMONo[i].ToString();
                    if (sPrintedLabel.Length > 0)
                    {
                        sPrintedLabel = sPrintedLabel + "#" + sPartBarcode;
                    }
                    else
                    {
                        sPrintedLabel = sPartBarcode;
                    }
                    if (sTMOPrintLabel.Length > 0)
                    {
                        sTMOPrintLabel = sTMOPrintLabel + "#" + sTMONO;
                    }
                    else
                    {
                        sTMOPrintLabel = sTMONO;
                    }

                    if (i == iCounter)
                    {
                        sResult = blobj.blPrintLabel(sPrintedLabel, drpPrinterName.Text
                               , sPartCode, sCustomer, sSiteCode, sFGItemCode, sUserID, sLineCode
                               , sTMOPrintLabel
                               );
                        iCounter = iCounter + iNewRow;
                        iPrintqty++;
                        sPrintedLabel = string.Empty;
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
                        CommonHelper.ShowMessage("PCB Sr No. Printed Successfully", msgsuccess, CommonHelper.MessageType.Success.ToString());
                        BindFGItemCode();
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
            BindFGItemCode();
        }
    }
}