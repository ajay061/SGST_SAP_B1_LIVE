using BusinessLayer.Reports;
using Common;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGST.INE.Reports
{
    public partial class ASNWifiAPSAPReport : System.Web.UI.Page
    {
        static DataTable dtTrackingData = new DataTable();
        static DataTable dtASNWifiAPSAP = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
            Response.AddHeader("Pragma", "no-cache");
            Response.AddHeader("Expires", "0");
            if (Session["usertype"].ToString() != "ADMIN")
            {
                string _strRights = CommonHelper.GetRights("Wifi AP SAP Report", (DataTable)Session["USER_RIGHTS"]);
                CommonHelper._strRights = _strRights.Split('^');
                if (CommonHelper._strRights[0] == "0")
                {
                    Response.Redirect("~/NoAccess/UnAuthorized.aspx");
                }
            }
            if (!IsPostBack)
            {
                try
                {


                    txtInvoiceNo.Text = null;
                    TextModelNo.Text = null;
                }
                catch (Exception ex)
                {
                    CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                }
            }

        }
        protected void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                CommonHelper.HideMessage(msginfo, msgsuccess, msgwarning, msgerror);
                ShowGridData();
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                if (ex.Message.Contains("System.OutOfMemoryException"))
                {
                    CommonHelper.ShowCustomErrorMessage("System Memory is full, Please select different dates", msgerror);
                }
                else
                {
                    CommonHelper.ShowCustomErrorMessage(ex.Message, msgerror);
                }
            }
        }
        private void ShowGridData()
        {
            try
            {
                dtTrackingData.Rows.Clear();
                if (txtInvoiceNo.Text == "")
                {
                    CommonHelper.ShowMessage("Invoice No mandatory.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtInvoiceNo.Text = null;
                    return;
                }
                if (TextModelNo.Text == "")
                {
                    CommonHelper.ShowMessage("Model No are mandatory.", msgerror, CommonHelper.MessageType.Error.ToString());
                    TextModelNo.Text = null;
                    return;
                }
                if (TextModelNo.Text != "WG430223-RL")
                {
                    CommonHelper.ShowMessage("please select correct model code .", msgerror, CommonHelper.MessageType.Error.ToString());
                    TextModelNo.Text = null;
                    return;
                }
                BL_SAPPostingDataReport blobj = new BL_SAPPostingDataReport();
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/ReportViewer/ASNWifiAPSAPReport.rdlc");
                dtTrackingData = blobj.GetASNWifiAPSAPData(txtInvoiceNo.Text, TextModelNo.Text);
                if (dtTrackingData.Rows.Count == 0)
                {
                    CommonHelper.ShowMessage("No details found.", msgerror, CommonHelper.MessageType.Error.ToString());
                    txtInvoiceNo.Text = null;
                    TextModelNo.Text = null;
                }
                else
                {
                    if (dtTrackingData.Columns.Count == 1)
                    {
                        CommonHelper.ShowMessage(dtTrackingData.Rows[0][0].ToString(), msgerror, CommonHelper.MessageType.Error.ToString());
                        return;
                    }

                    DataTable copyDataTable;
                    copyDataTable = dtTrackingData.Copy();
                    dtASNWifiAPSAP = dtASNWifiAP(copyDataTable, TextModelNo.Text);
                    ReportDataSource datasource = new ReportDataSource();
                    datasource = new ReportDataSource("DataSet1", dtASNWifiAPSAP);
                    ReportViewer1.Visible = true;
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasource);
                    ReportViewer1.LocalReport.EnableExternalImages = true;
                    // ReportParameter paramete2 = new ReportParameter("DateOFReprint", DateTime.Now.ToString("dd-MM-yyyy"));
                    string imagePath = new Uri(Server.MapPath("~/images/ReportLogo.png")).AbsoluteUri;
                    ReportParameter parameter = new ReportParameter("rptLogo", imagePath);

                    ReportParameter paramete1 = new ReportParameter("Invoice_no", txtInvoiceNo.Text);
                    ReportParameter paramete2 = new ReportParameter("Model_no", TextModelNo.Text);
                    ReportViewer1.LocalReport.SetParameters(parameter);
                    ReportViewer1.LocalReport.SetParameters(paramete1);
                    ReportViewer1.LocalReport.SetParameters(paramete2);
                    //ReportViewer1.LocalReport.SetParameters(paramete2);
                    ReportViewer1.LocalReport.Refresh();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ShowMessage();", true);
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
        }
        public DataTable dtASNWifiAP(System.Data.DataTable dt, string sModelCode)
        {
            DataTable dtASNFile = new DataTable();
            try
            {
                dtASNFile = dt.Copy();
                dtASNFile.Rows.Clear();
                dtASNFile.AcceptChanges();
                int colCount = dt.Columns.Count;
                int rowCount = dt.Rows.Count;


                if (sModelCode.Contains("WG430223-RL"))
                {
                    for (int index = 0; index < dt.Rows.Count; index++)
                    {
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            if (i == 0)
                            {
                                dtASNFile.Rows.Add();
                            }
                            dtASNFile.Rows[index][i] = dt.Rows[index][i].ToString();
                            dtASNFile.AcceptChanges();
                            if (i == 2)
                            {
                                dtASNFile.Rows[index][i] = dt.Rows[index][i + 1].ToString();
                            }
                            if (dtASNFile.Columns[i].ColumnName == "Physical_Device_Type")
                            {
                                if (sModelCode.Contains("WG430223-RL"))
                                {
                                    dt.Rows[index][i] = "Wi-Fi AP";
                                }
                                else
                                {
                                    dt.Rows[index][i] = "HOME GATEWAY";
                                }

                            }

                            if (dt.Columns[i].ColumnName == "Serial_Number")
                            {
                                dtASNFile.Rows[index][i] = dt.Rows[index][i + 1].ToString();
                                dtASNFile.AcceptChanges();
                            }

                            if (dtASNFile.Columns[i].ColumnName.Contains("Base_MAC_ID"))
                            {
                                string wifi_mac = dt.Rows[index][i + 1].ToString();
                                string MacValue = wifi_mac;
                                int insertedCount = 0;
                                for (int k = 4; k < MacValue.Length; k = k + 4)
                                {
                                    wifi_mac = wifi_mac.Insert(k + insertedCount++, ".");
                                }
                                dtASNFile.Rows[index][i] = wifi_mac;
                                wifi_mac = dt.Rows[index][i].ToString();
                                MacValue = wifi_mac;
                                insertedCount = 0;
                                if (sModelCode.Contains("WG430223-RL"))
                                {
                                    for (int k = 2; k < MacValue.Length; k = k + 2)
                                    {
                                        wifi_mac = wifi_mac.Insert(k + insertedCount++, ":");
                                    }
                                }
                                else
                                {
                                    for (int k = 4; k < MacValue.Length; k = k + 4)
                                    {
                                        wifi_mac = wifi_mac.Insert(k + insertedCount++, ".");
                                    }
                                }
                                dt.Rows[index][i] = wifi_mac;
                            }

                        }
                    }
                }





            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {

            }


            //if (sModelCode == "WG430223-RL")
            // {
            // dt.Columns.RemoveAt(2);
            //dt.AcceptChanges();

            // }
            //else
            // {
            // dt.Columns.RemoveAt(3);
            // dt.AcceptChanges();
            // }
            //dt.Columns.Remove("MAC_2");
            //dt.AcceptChanges();

            //DataView dv = new DataView();
            //dv = dt.DefaultView;
            //dv.Sort = "FPD_ID";
            //DataTable sortedDT = new DataTable();
            //sortedDT = dv.ToTable();
            //sortedDT.Columns.Remove("FPD_ID");
            //sortedDT.AcceptChanges();
            return dt;
        }
    }
}