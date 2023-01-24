using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DataLayer.Masters
{    
    public class DL_Label_Printing
    {
        DBManager oDbm;
        public DL_Label_Printing()
        {
            DCommon c = new DCommon();
            oDbm = c.SqlDBProvider();
        }

        public DataTable GetSeletedData(StringBuilder sb)
        {
            DataTable dt = new DataTable();
            try
            {
                oDbm.Open();
                dt = oDbm.ExecuteDataSet(System.Data.CommandType.Text, sb.ToString()).Tables[0];
            }
            catch (Exception ex)
            {
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                oDbm.Close();
            }
            return dt;
        }

        public int SavePalletPrinting(StringBuilder sb)
        {
            int iResult = 0;
            try
            {
                oDbm.Open();
                oDbm.BeginTransaction();
                iResult = oDbm.ExecuteNonQuery(System.Data.CommandType.Text, sb.ToString());
                oDbm.CommitTransaction();
            }
            catch (Exception ex)
            {
                oDbm.RollbackTransaction();
                PCommon.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.Assembly.GetExecutingAssembly().GetName() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                throw ex;
            }
            finally
            {
                oDbm.Close();
            }
            return iResult;
        }
    }
}
