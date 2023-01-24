using System;
using DataLayer;
using PL;
using System.Data;
namespace BusinessLayer
{
    public class BL_IMEIUpload
    {
        DL_IMEIUpload dlobj;
        public DataTable dtBindModel()
        {
            DataTable dt = new DataTable();
            try
            {
                dlobj = new DL_IMEIUpload();
                dt = dlobj.BindModel();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }     
        public DataSet dtBindModelDetails(PL_IMEIMaster plboj)
        {
            DataSet dt = new DataSet();
            try
            {
                dlobj = new DL_IMEIUpload();
                dt = dlobj.GetModelDetails(plboj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public DataTable dtCheckDuplicate(PL_IMEIMaster plboj)
        {
            DataTable dt = new DataTable();
            try
            {
                dlobj = new DL_IMEIUpload();
                dt = dlobj.dtCheckDuplicate(plboj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public string blUploadIMEIDataByDataTable(PL_IMEIMaster plobj)
        {
            string sResult = string.Empty;
            try
            {
                dlobj = new DL_IMEIUpload();
                sResult = dlobj.dlUploadIMEIDetalsByDataTable(plobj);
            }
            catch (Exception ex)
            {
                sResult = "ERROR~" + ex.Message;
            }
            return sResult;
        }


        public DataTable dtCheckDuplicate_BosaFile(PL_IMEIMaster plboj)
        {
            DataTable dt = new DataTable();
            try
            {
                dlobj = new DL_IMEIUpload();
                dt = dlobj.dtCheckDuplicate_boxaFile(plboj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public string blUploadBosaDataByDataTable(PL_IMEIMaster plobj)
        {
            string sResult = string.Empty;
            try
            {
                dlobj = new DL_IMEIUpload();
                sResult = dlobj.dlUploadboxaDetalsByDataTable(plobj);
            }
            catch (Exception ex)
            {
                sResult = "ERROR~" + ex.Message;
            }
            return sResult;
        }
    }
}
