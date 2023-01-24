using BusinessLayer.Masters;
using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGST.INE.Masters
{
    public partial class ReprintReasonMaster : System.Web.UI.Page
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
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                if (Session["usertype"].ToString() != "ADMIN")
                {
                    string _strRights = CommonHelper.GetRights("Reprint Reason MASTER", (DataTable)Session["USER_RIGHTS"]);
                    CommonHelper._strRights = _strRights.Split('^');
                    if (CommonHelper._strRights[0] == "0")  //Check view rights
                    {
                        Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                    }
                }
                if (!IsPostBack)
                {
                    BindGrid();
                }
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
                gvReprintReasonMst.PageIndex = e.NewPageIndex;
                //ShowGridData();
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
                string Reprint_ID = string.Empty;
                string _sUserID = string.Empty;
                string _SiteCode = string.Empty;
                string[] strValue = e.CommandArgument.ToString().Split('~');
                Reprint_ID = e.CommandArgument.ToString();
                if (e.CommandName == "DeleteRecords")
                {
                    DeleteRecords(Reprint_ID);
                }
                if (e.CommandName == "EditRecords")
                {
                    if (btnSave.Text == "Save")
                    { btnSave.Text = "Update"; }
                    EditRecords(Reprint_ID);
                }
                //ShowGridData();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            save();
        }
        public void save()
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                if (Reason_of_Reprint.Text.Trim() == string.Empty)
                {
                    CommonHelper.ShowMessage("Please enter Reason of Reprint.", msgerror, CommonHelper.MessageType.Error.ToString());
                    Reason_of_Reprint.Focus();
                    return;
                }
                else
                {

                    BL_ReprintReasonMaster blobj = new BL_ReprintReasonMaster();
                    string _OuptPut = string.Empty;
          
                    if (btnSave.Text == "Save")
                    {
                        _OuptPut = blobj.SaveLocationData(Reason_of_Reprint.Text.Trim(), Session["UserID"].ToString());

                    }
                    else
                    {
                        
                        _OuptPut = blobj.UpdateReasonReprint(Reason_of_Reprint.Text.Trim(), Session["UserID"].ToString(), Convert.ToInt32(hidUID.Value));
                    }
                    if (_OuptPut.StartsWith("N~") || _OuptPut.StartsWith("ERROR~"))
                    {
                        CommonHelper.ShowMessage(_OuptPut.Split('~')[1], msgerror, CommonHelper.MessageType.Error.ToString());
                        Reason_of_Reprint.Text = string.Empty;
                    }
                    else if (_OuptPut.StartsWith("SAVE~"))
                    {
                           
                            BindGrid();
                        Reason_of_Reprint.Text = string.Empty;
                        CommonHelper.ShowMessage("Record saved successfully", msgsuccess, CommonHelper.MessageType.Success.ToString());
                        
                    }
                    else if (_OuptPut.StartsWith("UPDATE~"))
                    {

                        BindGrid();
                        Reason_of_Reprint.Text = string.Empty;
                        CommonHelper.ShowMessage("Record Updated successfully", msgsuccess, CommonHelper.MessageType.Success.ToString());

                    }
                    else
                    {
                        CommonHelper.ShowMessage("No result found.", msgerror, CommonHelper.MessageType.Error.ToString());
                    }
                    
                   
                    
                    
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return;
            }
            finally
            {
                //ShowGridData();
                btnSave.Text = "Save";
                Reason_of_Reprint.ReadOnly = false;

            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            Reason_of_Reprint.Text = string.Empty;
        }
        protected void BindGrid()
        {
            try
            {
                BL_ReprintReasonMaster blobj = new BL_ReprintReasonMaster();
                DataTable dt = blobj.BindReasonReprint();
                if (dt.Rows.Count > 0)
                {
                    gvReprintReasonMst.DataSource = dt;
                    gvReprintReasonMst.DataBind();
                    lblNumberofRecords.Text = dt.Rows.Count.ToString();
                }
                else
                {
                    gvReprintReasonMst.DataSource = null;
                    gvReprintReasonMst.DataBind();
                    lblNumberofRecords.Text = "0";
                }
            }
            catch (Exception ex)
            {
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
           

        }
        private void DeleteRecords(string Reprint_ID)
        {
            CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
            try
            {
                BL_ReprintReasonMaster blobj = new BL_ReprintReasonMaster();

                DataTable sResult = new DataTable();
                sResult = blobj.DeleteReasonReprint(Reprint_ID);
                Message = sResult.Rows[0][0].ToString();
                if (sResult.Rows.Count > 0)
                {
                   
                    

                        CommonHelper.ShowMessage("Record deleted successfully", msgsuccess, CommonHelper.MessageType.Error.ToString());
                    
                }
                else
                {
                    CommonHelper.ShowMessage("No result found", msgerror, CommonHelper.MessageType.Error.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
            }
            finally
            {
                btnSave.Text = "Save";
                BindGrid();
            }

        }
        private void EditRecords(string Reprint_ID)
        {
            try
            {
                BL_ReprintReasonMaster blobj = new BL_ReprintReasonMaster();
                //plobj.sInvoiceNo = Reprint_ID;
                //blobj.sInvoiceNo = ReasonReprint;
                DataTable dtDetails = blobj.SearchReasonReprint(Reprint_ID);
                if (dtDetails.Rows.Count > 0)
                {

                    Reason_of_Reprint.Text = dtDetails.Rows[0]["Reason_of_Reprint"].ToString();
                    
                    hidUpdate.Value = "Update";
                    hidUID.Value = Reprint_ID;
                }
                else
                {
                    CommonHelper.ShowMessage("No  details found", msgerror, CommonHelper.MessageType.Error.ToString());

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