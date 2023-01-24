using System;
using System.Data;
using System.Reflection;
using BcilLib;
using Common;

namespace DataLayer.MES.MASTERS
{
    public class DL_ModelAccessoriesMapping : DCommon
    {
        DBManager odb;
        DataTable dtobj;
        public DL_ModelAccessoriesMapping()
        {
            odb = SqlDBProvider();
        }
        public DataTable dtBindKeysInGrid()
        {
            dtobj = new DataTable();
            try
            {
                odb.CreateParameters(1);
                odb.AddParameters(0, "@TYPE", "BIND_ACCESSORIES");
                odb.Open();
                dtobj = odb.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_MODEL_ACCESSORIES_MAPPING").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtError, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {
                odb.Close();
                odb.Dispose();
            }
            return dtobj;
        }

        public DataTable Bind_Model_Mapping_Keys(string sModelNo)
        {
            dtobj = new DataTable();
            try
            {
                odb.CreateParameters(2);
                odb.AddParameters(0, "@TYPE", "SELECTED_MODEL_BIND_IN_GRID");
                odb.AddParameters(1, "@Model_Code", sModelNo);
                odb.Open();
                dtobj = odb.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_MODEL_ACCESSORIES_MAPPING").Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtError, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {
                odb.Close();
                odb.Dispose();
            }
            return dtobj;
        }

        public DataTable SaveKeyModelMapping(DataTable _dtModelKey, string sModelNo
            , string sUserID,string sSiteCode,string sFGItemCode)
        {
            dtobj = new DataTable();
            try
            {
                odb.CreateParameters(6);
                odb.AddParameters(0, "TYPE", "MODEL_ACCESSORIES_MAPPING");
                odb.AddParameters(1, "@DT_ACC_MAPPING", _dtModelKey);
                odb.AddParameters(2, "@Model_Code", sModelNo.Trim());
                odb.AddParameters(3, "@CREATED_BY", sUserID);
                odb.AddParameters(4, "@FG_ITEM_CODE", sFGItemCode);
                odb.AddParameters(5, "@SITECODE", sSiteCode);
                odb.Open();
                dtobj = odb.ExecuteDataSet(System.Data.CommandType.StoredProcedure, "USP_MODEL_ACCESSORIES_MAPPING").Tables[0];
                if (dtobj.Rows[0][0].ToString().ToUpper() == "INSERT_SUCC")
                {
                    PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtInfo, MethodBase.GetCurrentMethod().Name, "Success Result Found");
                }
                else if (dtobj.Rows[0][0].ToString().StartsWith("ERROR"))
                {
                    PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtInfo, MethodBase.GetCurrentMethod().Name, "Error Found ~" + dtobj.Rows[0][0].ToString());
                }
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(EventNotice.EventTypes.evtError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                odb.Close();
                odb.Dispose();
            }
            return dtobj;
        }
    }
}
