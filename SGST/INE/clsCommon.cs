using System;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;
namespace SGST.INE
{
    public class clsCommon
    {
        public static void FillComboBox(DropDownList dro, DataTable dt, bool isSelect)
        {
            try
            {
                if (isSelect)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    //dr[0] = "";
                    dt.Rows.InsertAt(dr, 0);
                }
                dt.Columns[0].ColumnName = "Column1";
                //dt.Columns[1].ColumnName = "Column2";
                dt.AcceptChanges();
                dro.DataSource = dt;
                dro.DataTextField = "Column1";
                //dro.DataValueField = "Column2";
                dro.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void FillMultiColumnsCombo(DropDownList dro, DataTable dt, bool isSelect)
        {
            try
            {
                if (isSelect)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "";
                    dt.Rows.InsertAt(dr, 0);
                }
                dt.Columns[0].ColumnName = "Column1";
                dt.Columns[1].ColumnName = "Column2";
                dt.AcceptChanges();
                dro.DataSource = dt;
                dro.DataTextField = "Column1";
                dro.DataValueField = "Column2";
                dro.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void FillMachine(DropDownList dro, DataTable dt, bool isSelect)
        {
            try
            {
                dt.Columns[1].ColumnName = "Column1";
                dt.Columns[0].ColumnName = "Column2";
                dro.DataSource = dt;
                dro.DataValueField = "Column1";
                dro.DataTextField = "Column2";
                dro.DataBind();
                dro.Items.Insert(0, new ListItem("--Select--", ""));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string ExportToTextFile(DataTable dtTable)
        {
            StringBuilder sbldr = new StringBuilder();
            try
            {
                if (dtTable.Columns.Count != 0)
                {
                    foreach (DataColumn col in dtTable.Columns)
                    {
                        sbldr.Append(col.ColumnName + ',');
                    }
                    sbldr.Append("#");
                    foreach (DataRow row in dtTable.Rows)
                    {
                        foreach (DataColumn column in dtTable.Columns)
                        {
                            sbldr.Append(row[column].ToString() + ',');
                        }
                        sbldr.Remove(sbldr.Length - 1, 1);
                        sbldr.ToString().TrimEnd();
                        sbldr.Append("#");
                        sbldr.ToString().TrimEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            sbldr.ToString().TrimEnd();
            return sbldr.ToString();
        }
    }
}