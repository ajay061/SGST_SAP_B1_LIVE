using BusinessLayer.FG;
using Common;
using System;
using System.Data;

namespace SGST.INE.FG
{
    public partial class Loadinganddispatch : System.Web.UI.Page
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
                    string _strRights = CommonHelper.GetRights("LOADING AND DISPATCH", (DataTable)Session["USER_RIGHTS"]);
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
                    GetPackListDetails();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        private void GetPackListDetails()
        {
            try
            {
                drpPickLIst.Items.Clear();
                BL_Loading_Dispatch blobj = new BL_Loading_Dispatch();
                DataTable dt = blobj.BindPickList(Session["SiteCode"].ToString(), Session["LINECODE"].ToString());
                if (dt.Rows.Count > 0)
                {
                    clsCommon.FillComboBox(drpPickLIst, dt, true);
                    drpPickLIst.Focus();
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
                drpPackingList.Items.Clear();
                if (drpPickLIst.SelectedIndex > 0)
                {                    
                    string PICKLIST_NO = drpPickLIst.Text;
                    BL_Loading_Dispatch blobj = new BL_Loading_Dispatch();
                    DataTable dt = blobj.BindPackingList(PICKLIST_NO, Session["SiteCode"].ToString(), Session["LINECODE"].ToString());
                    if (dt.Rows.Count > 0)
                    {
                        clsCommon.FillComboBox(drpPackingList, dt, true);
                        drpPackingList.Focus();
                    }
                }
                else
                {
                    GetPackListDetails();
                    if (drpPickLIst.Items.Count == 0)
                    {
                        CommonHelper.ShowMessage("No Pending Picklist found", msginfo, CommonHelper.MessageType.Info.ToString());
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
            CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
            if (drpPackingList.SelectedIndex > 0)
            {
                ShowGridData();
            }
        }

        private void ShowGridData()
        {
            try
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                BL_Loading_Dispatch blobj = new BL_Loading_Dispatch();
                DataTable dt = blobj.BindDetails(drpPickLIst.Text, drpPackingList.Text, Session["SiteCode"].ToString(), Session["LINECODE"].ToString());
                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                else
                {
                    drpPackingList.Items.Clear();
                    drpPickLIst_SelectedIndexChanged(null, null);
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
        }

        protected void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            CommonHelper.HideSuccessMessage(msginfo, msgwarning, msgerror);
            if (drpPickLIst.SelectedIndex == 0)
            {
                CommonHelper.ShowMessage("Please select picklist.", msgerror, CommonHelper.MessageType.Error.ToString());
                txtBarcode.Text = "";
                return;
            }
            if (drpPackingList.SelectedIndex == 0)
            {
                CommonHelper.ShowMessage("Please select packing list.", msgerror, CommonHelper.MessageType.Error.ToString());
                txtBarcode.Text = "";
                return;
            }
            //if (drpScanType.SelectedIndex == 0)
            //{
            //    CommonHelper.ShowMessage("Please select scanning type.", msgerror, CommonHelper.MessageType.Error.ToString());
            //    txtBarcode.Text = "";
            //    return;
            //}
            string PACKINGLIST_NO = drpPackingList.Text;
            string PICKLIST_NO = drpPickLIst.Text;
            string BOX_BARCODE = txtBarcode.Text;
            string SCANNED_BY = Session["UserID"].ToString();
            string MODULE_TYPE = "BOX";
            BL_Loading_Dispatch blobj = new BL_Loading_Dispatch();
            string sResult = string.Empty;
            sResult = blobj.LoadingDispatch(PACKINGLIST_NO, PICKLIST_NO, BOX_BARCODE, SCANNED_BY, MODULE_TYPE, Session["SiteCode"].ToString(), Session["LINECODE"].ToString());
            string[] Message = sResult.Split('~');
            if (sResult.StartsWith("N~") || sResult.StartsWith("ERROR~"))
            {
                CommonHelper.ShowMessage(Message[1], msgerror, CommonHelper.MessageType.Error.ToString());
                txtBarcode.Text = string.Empty; txtBarcode.Focus();
            }
            else
            {
                CommonHelper.ShowMessage(Message[1], msgsuccess, CommonHelper.MessageType.Success.ToString());
                txtBarcode.Focus();
                //drpScanType.SelectedIndex = 0;
                txtBarcode.Text = "";
              
                ShowGridData();
            }
        }

        protected void drpScanType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (drpScanType.SelectedIndex == 1)
            //{
            //    hidBarcodeType.Value = "PALLET";
            //}
            //if (drpScanType.SelectedIndex == 2)
            //{
                hidBarcodeType.Value = "BOX";
            //}
        }
    }
}