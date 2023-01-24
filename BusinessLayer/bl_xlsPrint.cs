using Common;
using Microsoft.Office.Interop.Excel;
using System;
using System.Diagnostics;
using System.Reflection;
namespace BusinessLayer
{
    public class bl_xlsPrint
    {
        public static string ExportInExcel(System.Data.DataTable dt, string Header, string filename, string sFilePath)
        {
            string sResult = string.Empty;
            try
            {
                Microsoft.Office.Interop.Excel._Application excel = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbooks workbooks = excel.Workbooks;
                Microsoft.Office.Interop.Excel._Workbook workbook = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                Microsoft.Office.Interop.Excel._Worksheet worksheet = (Microsoft.Office.Interop.Excel._Worksheet)workbook.Worksheets[1];
                Range range;
                Object[] data;
                //for (int j = 0; j < dt.Columns.Count; j++)
                //{
                //    if (j <= 25)
                //        range = (Range)worksheet.get_Range(Convert.ToChar(65 + j) + 1.ToString(), System.Reflection.Missing.Value);
                //    else
                //        range = (Range)worksheet.get_Range("A" + Convert.ToChar(65 + j - 26) + 1.ToString(), System.Reflection.Missing.Value);
                //    data = new object[] { dt.Columns[j].Caption };
                //    range.NumberFormat = "@";

                //    range.GetType().InvokeMember("Value", System.Reflection.BindingFlags.SetProperty, null, range, data);
                //    // PCommon.mAppLog.LogMessage(EventNotice.EventTypes.evtAll, MethodBase.GetCurrentMethod().Name, "" + j + " Coloumn Name Genrated");
                //}
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j <= 25)
                            range = (Range)worksheet.get_Range(Convert.ToChar(65 + j) + Convert.ToString(1 + i), System.Reflection.Missing.Value);
                        else
                            range = (Range)worksheet.get_Range("A" + Convert.ToChar(65 + j - 26) + Convert.ToString(1 + i), System.Reflection.Missing.Value);
                        data = new object[] { Convert.ToString(dt.Rows[i][j]) };
                        range.NumberFormat = "@";
                        range.GetType().InvokeMember("Value", System.Reflection.BindingFlags.SetProperty, null, range, data);
                        //PCommon.mAppLog.LogMessage(EventNotice.EventTypes.evtAll, MethodBase.GetCurrentMethod().Name, "" + i + " Row Name Genrated");
                    }
                }
                object misValue = System.Reflection.Missing.Value;
                worksheet.Columns.AutoFit();
                string sFileName = filename;
                workbook.SaveAs(sFileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                workbook.Close(false, System.Type.Missing, System.Type.Missing);
                excel.Quit();
                //Marshal.FinalReleaseComObject(excel);
                releaseObject(worksheet);
                releaseObject(workbook);
                releaseObject(excel);

                sResult = "Export";
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, Assembly.GetExecutingAssembly().GetName() + "::" + MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
            finally
            {
                foreach (Process clsProcess in Process.GetProcesses())
                {
                    if (clsProcess.ProcessName.Equals("EXCEL"))
                    {
                        clsProcess.Kill();
                        break;
                    }
                }
            }
            return sResult;
        }
        

        private static void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, Assembly.GetExecutingAssembly().GetName() + "::" + MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
