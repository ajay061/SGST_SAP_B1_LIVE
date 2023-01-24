using BusinessLayer;
using BusinessLayer.WIP;
using Common;
using System;
using System.Configuration;
using System.Data;
using System.Web.UI;

namespace SGST.INE.WIP
{
    public partial class WIP2DLabelPrinting : System.Web.UI.Page
    {
        static int iPageLabelCount = 4;
        BL_WIP_LaserMachine blobj = new BL_WIP_LaserMachine();
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usertype"].ToString() != "ADMIN")
            {
                string _strRights = CommonHelper.GetRights("PCB LABEL PRINTING", (DataTable)Session["USER_RIGHTS"]);
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
        protected void btnPrint_Click(object sender, EventArgs e)
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
                if (drpWorkOrderNo.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select work order no", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpWorkOrderNo.Focus();
                    return;
                }
                if (drpPendingBarcode.SelectedIndex <= 0)
                {
                    CommonHelper.ShowMessage("Please select PKT barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    drpPendingBarcode.Focus();
                    return;
                }
                if (dvLaserFileData.Rows.Count == 0)
                {
                    CommonHelper.ShowMessage("Please scan barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBarcode.Focus();
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
                string sLineCode = Session["LINECODE"].ToString();
                string sUserID = Session["UserID"].ToString();
                string _sPrinterPort = ConfigurationManager.AppSettings["sPrinterPort"].ToString();
                blobj = new BL_WIP_LaserMachine();
                string sResult = "";
                string sPartBarcode = string.Empty;
                string sPartCode = string.Empty;
                string sFGItemCode = string.Empty;
                string sCustomer = string.Empty;
                string sSiteCode = Session["SiteCode"].ToString();
                string sPrintedLabel = string.Empty;
                string sFinalTMONo = string.Empty;
                string sTMOProductNO = string.Empty;
                int iNewRow = iPageLabelCount;
                int iCounter = iPageLabelCount - 1;
                DataTable dt = (DataTable)Session["GENERATEDDATA"];
                int TotalQty = dt.Rows.Count;
                int iFinalCounter = 0;
                int iPrintqty = 0;
                int iChildQty = TotalQty / iNewRow;
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
                    sPartBarcode = dt.Rows[i][4].ToString();
                    sTMOProductNO = dt.Rows[i]["TMO_PRODUCT_NO"].ToString();
                    if (sPrintedLabel.Length > 0)
                    {
                        sPrintedLabel = sPrintedLabel + "#" + sPartBarcode;
                        if (sFinalTMONo.Length > 0)
                        {
                            sFinalTMONo = sFinalTMONo + "#" + sTMOProductNO;
                        }                        
                    }
                    else
                    {
                        sPartCode = dt.Rows[i][0].ToString();
                        sCustomer = dt.Rows[i][3].ToString();
                        sPartBarcode = dt.Rows[i][4].ToString();
                        sFGItemCode = dt.Rows[i][6].ToString();
                        sFinalTMONo = sTMOProductNO;
                        sPrintedLabel = sPartBarcode;
                    }
                    if (i == iCounter)
                    {
                        CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.MethodBase.GetCurrentMethod().Name, "PCB Label Printing Barcode : " + sPrintedLabel);
                        sResult = blobj.blPrintLabel(sPrintedLabel, drpPrinterName.Text, _sPrinterPort
                 , sPartCode, sCustomer, sSiteCode, sFGItemCode, sUserID, sLineCode, sFinalTMONo);
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
                        blobj = new BL_WIP_LaserMachine();
                        blobj.dlUpdateData(txtBarcode.Text.Trim(), Session["SiteCode"].ToString(), Session["UserID"].ToString());
                        txtBarcode.Text = string.Empty;
                        CommonHelper.ShowMessage("PCB Sr No. Printed Successfully", msgsuccess, CommonHelper.MessageType.Success.ToString());
                        dvLaserFileData.DataSource = null;
                        dvLaserFileData.DataBind();
                        BindPendingBarcode();
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

        private void GetPageLabelCount()
        {
            try
            {
                iPageLabelCount = 4;
                BL_WIP_LabelReprint blobj = new BL_WIP_LabelReprint();
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
        private void BindFGItemCode()
        {
            try
            {
                blobj = new BL_WIP_LaserMachine();
                drpPendingBarcode.Items.Clear();
                drpFGItemCode.Items.Clear();
                drpWorkOrderNo.Items.Clear();
                dvLaserFileData.DataSource = null;
                dvLaserFileData.DataBind();
                DataTable dt = blobj.BindLPFGItemCode(Session["SiteCode"].ToString());
                if (dt.Rows.Count > 0)
                {
                    CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                    clsCommon.FillComboBox(drpFGItemCode, dt, true);
                    drpFGItemCode.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void BindWorkOrderNo()
        {
            try
            {
                blobj = new BL_WIP_LaserMachine();
                drpPendingBarcode.Items.Clear();
                drpWorkOrderNo.Items.Clear();
                dvLaserFileData.DataSource = null;
                dvLaserFileData.DataBind();
                if (drpFGItemCode.SelectedIndex > 0)
                {
                    DataTable dt = blobj.BindLPWorkOrderNo(drpFGItemCode.SelectedItem.Text
                        , Session["SiteCode"].ToString()
                        );
                    if (dt.Rows.Count > 0)
                    {
                        CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                        clsCommon.FillComboBox(drpWorkOrderNo, dt, true);
                        drpWorkOrderNo.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void BindPendingBarcode()
        {
            try
            {
                blobj = new BL_WIP_LaserMachine();
                DataTable dt = new DataTable();
                drpPendingBarcode.Items.Clear();
                dvLaserFileData.DataSource = null;
                dvLaserFileData.DataBind();
                if (drpWorkOrderNo.SelectedIndex > 0)
                {
                    dt = blobj.BindPendingBarcode(drpFGItemCode.SelectedItem.Text, drpWorkOrderNo.SelectedItem.Text
                        , Session["SiteCode"].ToString()
                        );
                    if (dt.Rows.Count > 0)
                    {
                        CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
                        clsCommon.FillComboBox(drpPendingBarcode, dt, true);
                        drpPendingBarcode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void bindData(string sType)
        {
            blobj = new BL_WIP_LaserMachine();
            DataTable dt = new DataTable();
            dvLaserFileData.DataSource = null;
            dvLaserFileData.DataBind();
            if (sType == "SCAN")
            {
                dt = blobj.ValidatePCBBarcode(txtBarcode.Text.Trim(), "", "", Session["SiteCode"].ToString());
            }
            else
            {
                string sBarcode = txtBarcode.Text;
                dt = blobj.ValidatePCBBarcode(sBarcode, drpWorkOrderNo.SelectedItem.Text, drpFGItemCode.SelectedItem.Text
                    , Session["SiteCode"].ToString()
                    );
            }
            if (dt.Rows.Count > 0)
            {
                if (dt.Columns.Count > 0)
                {
                    Session["GENERATEDDATA"] = dt;
                    dvLaserFileData.DataSource = dt;
                    dvLaserFileData.DataBind();
                    GetPageLabelCount();
                }
                else
                {
                    CommonHelper.ShowMessage("No result found for printing.", msgerror, CommonHelper.MessageType.Error.ToString());
                }
            }
        }

        protected void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtBarcode.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBarcode.Focus();
                    return;
                }
                bindData("SCAN");
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void dvLaserFileData_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            try
            {
                dvLaserFileData.PageIndex = e.NewPageIndex;
                //if (drpReservationNo.SelectedIndex > 0)
                //{
                //    bindData("RESERVATIONSLIP");
                //}
                //else
                //{
                bindData("SCANBARCODE");
                //}
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void drpPendingBarcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dvLaserFileData.DataSource = null;
                dvLaserFileData.DataBind();
                txtBarcode.Text = string.Empty;
                if (drpPendingBarcode.SelectedIndex > 0)
                {
                    txtBarcode.Text = drpPendingBarcode.Text;
                    bindData("BIND");
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        protected void drpFGItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindWorkOrderNo();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void drpWorkOrderNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindPendingBarcode();
                if (drpPendingBarcode.Items.Count == 0)
                {
                    BindWorkOrderNo();
                    if (drpWorkOrderNo.Items.Count == 0)
                    {
                        BindFGItemCode();
                    }
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