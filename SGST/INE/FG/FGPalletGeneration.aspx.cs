using BusinessLayer.FG;
using Common;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace SGST.INE.FG
{
    public partial class FGPalletGeneration : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Signin/v1/Login.aspx?Session=Null");
            }
        }
        string Message = "";
        private void GetPickList()
        {
            try
            {
                BL_MasterPacking blobj = new BL_MasterPacking();
                DataTable dt = blobj.BindPickList( Session["SiteCode"].ToString(), Session["LINECODE"].ToString());
                drpPickLIst.Items.Clear();
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(drpPickLIst, dt, true);
                    drpPickLIst.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void ShowGridData()
        {
            try
            {
                if (drpPackingList.SelectedIndex > 0 && drpPickLIst.SelectedIndex > 0)
                {
                    string PACKINGLIST_NO = drpPackingList.Text;
                    string PICKLIST_NO = drpPickLIst.Text;
                    BL_MasterPacking blobj = new BL_MasterPacking();
                    DataTable dt = blobj.BindScanItemDetails(PICKLIST_NO, PACKINGLIST_NO, Session["SiteCode"].ToString(), Session["LINECODE"].ToString());
                    if (dt.Rows.Count > 0)
                    {
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        private void resetData()
        {
            DataTable dt = new DataTable();
            GridView1.DataSource = dt;
            GridView1.DataBind();
            txtBarcode.Text = string.Empty;
            txtPallet.Text = string.Empty;
            drpPackingList.Items.Clear();
            GetPickList();
            drpPickLIst.Focus();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["usertype"].ToString() != "ADMIN")
                {
                    string _strRights = CommonHelper.GetRights("PALLET ALLOCATION", (DataTable)Session["USER_RIGHTS"]);
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
                    GetPickList();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        protected void drpPickLIst_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpPickLIst.SelectedIndex > 0)
                {
                    string PICKLIST_NO = drpPickLIst.Text;
                    BL_MasterPacking blobj = new BL_MasterPacking();
                    DataTable dt = blobj.BindPackingList(PICKLIST_NO,Session["SiteCode"].ToString(),Session["LINECODE"].ToString()
                        );
                    if (dt.Rows.Count > 0)
                    {
                        drpPackingList.Items.Clear();
                        clsCommon.FillComboBox(drpPackingList, dt, true);
                        drpPackingList.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }
        protected void drpPackingList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ShowGridData();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }

        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                string Values = string.Empty;
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[2] { new DataColumn("BOX_ID", typeof(string)),
                        new DataColumn("BOX_QTY", typeof(string)),
                        });
                foreach (GridViewRow row1 in GridView1.Rows)
                {
                    if ((row1.FindControl("CheckBox1") as CheckBox).Checked)
                    {
                        string BOX_ID = row1.Cells[1].Text;
                        string BOX_QTY = row1.Cells[2].Text;
                        dt.Rows.Add(BOX_ID, BOX_QTY);
                    }
                    else
                    {
                        //CommonHelper.ShowMessage("Please Select at least one barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                        continue;
                    }
                }
                if (drpPickLIst.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please Select picklist", msgerror, CommonHelper.MessageType.Error.ToString());
                    return;
                }
                if (drpPackingList.SelectedIndex == 0)
                {
                    CommonHelper.ShowMessage("Please Select packing list", msgerror, CommonHelper.MessageType.Error.ToString());
                    return;
                }
                if (txtPallet.Text == "")
                {
                    CommonHelper.ShowMessage("Please scan pallet barcode", msgerror, CommonHelper.MessageType.Error.ToString());
                    return;
                }
                if (dt.Rows.Count == 0)
                {
                    CommonHelper.ShowMessage("Please scan at least one box", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtBarcode.Focus();
                    return;
                }
                BL_MasterPacking blobj = new BL_MasterPacking();
                string _Result = blobj.FGPalletPacking(drpPickLIst.Text,
                     drpPackingList.Text, Session["UserID"].ToString(), dt, txtPallet.Text
                     , Session["SiteCode"].ToString(), Session["LINECODE"].ToString()
                     );
                string[] multiArray = _Result.Split('~');
                if (_Result.Contains("N~"))
                {
                    Message = multiArray[1];
                    CommonHelper.ShowMessage(Message, msgerror, CommonHelper.MessageType.Error.ToString());
                    return;
                }
                if (_Result.Contains("PRNNOTFOUND~"))
                {
                    Message = multiArray[1];
                    CommonHelper.ShowMessage("Prn not found for printing", msgerror, CommonHelper.MessageType.Error.ToString());
                    return;
                }
                if (_Result.Contains("PRINTERNOTCONNECTED~"))
                {
                    Message = multiArray[1];
                    CommonHelper.ShowMessage("Selected printer is offline", msgerror, CommonHelper.MessageType.Error.ToString());
                    return;
                }
                else if (_Result.Contains("SUCCESS~"))
                {
                    Message = multiArray[1];
                    CommonHelper.ShowMessage(Message, msgsuccess, CommonHelper.MessageType.Success.ToString());
                    resetData();
                    return;
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void txtPallet_TextChanged(object sender, EventArgs e)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (string.IsNullOrEmpty(txtPallet.Text.Trim()))
                {
                    CommonHelper.ShowMessage("Please scan pallet barcode.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPallet.Focus();
                    return;
                }
                string PALLETBARCODE = txtPallet.Text;
                BL_MasterPacking blobj = new BL_MasterPacking();
                string _OuptPuts = blobj.CheckpalletBarcode(PALLETBARCODE, Session["SiteCode"].ToString(), Session["LINECODE"].ToString());
                string[] Message = _OuptPuts.Split('~');
                if (_OuptPuts.StartsWith("N~") || _OuptPuts.StartsWith("ERROR~"))
                {

                    CommonHelper.ShowMessage(Message[1], msgerror, CommonHelper.MessageType.Error.ToString());
                    txtPallet.Focus();
                    txtBarcode.ReadOnly = true;
                }
                else
                {
                    CommonHelper.ShowMessage(Message[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                    txtBarcode.ReadOnly = false;
                    txtBarcode.Focus();
                    txtBarcode.Text = "";
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                string Values = string.Empty;
                DataTable dt = new DataTable();
                if (txtBarcode.Text != "")
                {
                    foreach (GridViewRow item in GridView1.Rows)
                    {
                        if (item.RowType == DataControlRowType.DataRow && item.Cells[1].Text == txtBarcode.Text.Trim())
                        {
                            CheckBox chk = (CheckBox)item.FindControl("CheckBox1");
                            chk.Checked = !String.IsNullOrEmpty(txtBarcode.Text);
                            chk.Checked = true;
                            txtBarcode.Text = "";
                            txtBarcode.Focus();
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
    }
}