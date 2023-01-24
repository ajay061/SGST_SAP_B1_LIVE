using Common;
using Ionic.Zip;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SGST.INE
{
    public partial class PrintLabel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DownloadFile();
        }
        protected void DownloadFile()
        {
            string sApplicationPrintingFullFilePath = string.Empty;
            try
            {
                HttpResponse Response = HttpContext.Current.Response;
                string sHeaderName = string.Empty;
                string sUserID = Session["UserID"].ToString();
                string sLineCode = Session["LINECODE"].ToString();

                string sSearchParaMeter = "BCI" + "_" + sUserID + "_" + sLineCode + "_" + PCommon.sSiteCode;
                var SearchFile = Directory.GetFiles(PCommon.sFilePrintingPath).Where(file => Regex.IsMatch(file, sSearchParaMeter));
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, 
                    System.Reflection.MethodBase.GetCurrentMethod().Name, "File Name : " + sSearchParaMeter);
                String[] files = SearchFile.ToArray();
                using (ZipFile zipFile = new ZipFile())
                {
                    foreach (string item in files)
                    {
                        sHeaderName = item.ToString();
                        sApplicationPrintingFullFilePath = PCommon.sFilePrintingPath + "\\" + sHeaderName;                        
                        zipFile.AddFile(sHeaderName, string.Empty);
                        CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, 
                            System.Reflection.MethodBase.GetCurrentMethod().Name, "File Name : " + sSearchParaMeter + ", File Added : " + item.ToString());
                    }
                    Response.ClearContent();
                    Response.ClearHeaders();
                    //Set zip file name  
                    Response.AppendHeader("content-disposition", "attachment; filename=SGSTPrinting_" + sSearchParaMeter + "_" + DateTime.Now.ToString("ddMMyyyyhhmmsstt") + ".zip");
                    zipFile.CompressionMethod = CompressionMethod.BZip2;
                    zipFile.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                    //Save the zip content in output stream  
                    zipFile.Save(Response.OutputStream);
                }
                foreach (string file in files)
                {
                    FileInfo fi = new FileInfo(file);
                    fi.Delete();
                }
            }
            catch (Exception ex)
            {
                CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtError, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}