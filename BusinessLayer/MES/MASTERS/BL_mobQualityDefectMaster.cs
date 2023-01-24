using System;
using DataLayer.Masters;
using System.Data;

namespace BusinessLayer.MES.MASTERS
{
    public class BL_mobQualityDefectMaster
    {
        DL_MobQualityDefectMaster dlobj;
        public DataTable dtbindDefectFilter(string sSiteCode,string sLineCode)
        {
            DataTable dt = new DataTable();
            try
            {
                dlobj = new DL_MobQualityDefectMaster();
                dt = dlobj.BindFilterDefect(sSiteCode, sLineCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public DataTable SearchDetailsByID(string sQSDCode, string sSiteCode, string sLineCode)
        {
            DataTable dt = new DataTable();
            try
            {
                dlobj = new DL_MobQualityDefectMaster();
                dt = dlobj.SearchDetailsByID(sQSDCode, sSiteCode, sLineCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public DataTable SearchDetails(string sSiteCode, string sLineCode)
        {
            DataTable dt = new DataTable();
            try
            {
                dlobj = new DL_MobQualityDefectMaster();
                dt = dlobj.SearchDetails(sSiteCode, sLineCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public DataTable SaveDetails(DataTable dtData,string sSiteCode,string sUserID,string sLineCode
            ,string sArea)
        {
            DataTable dt = new DataTable();
            try
            {
                dlobj = new DL_MobQualityDefectMaster();
                dt = dlobj.SaveData(dtData, sSiteCode,sUserID,sLineCode, sArea);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public DataTable UpdateData(DataTable dtData,string sMachineID, string sSiteCode, 
            string sUserID, string sLineCode,string sArea)
        {
            DataTable dt = new DataTable();
            try
            {
                dlobj = new DL_MobQualityDefectMaster();
                dt = dlobj.UpdateData(dtData,sMachineID, sSiteCode, sUserID, sLineCode, sArea);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
    }
}
