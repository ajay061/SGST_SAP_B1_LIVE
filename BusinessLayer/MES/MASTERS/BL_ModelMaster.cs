using System;
using System.Data;
using DataLayer;
using PL;

namespace BusinessLayer
{
    public class BL_ModelMaster
    {
        DL_ModelMaster objDl;
        public BL_ModelMaster()
        {
            objDl = new DL_ModelMaster();
        }
        public DataTable blBindFGItemCode (PL_ModelMaster objpl)
        {
            try
            {
                return objDl.dlBindFGItemCode(objpl);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public object blCheckDuplicateModelName(PL_ModelMaster objpl)
        {
            try
            {
                return objDl.dlCheckDuplicateModelName(objpl);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int blSaveModelMaster(PL_ModelMaster objPl)
        {
            try
            {
                return objDl.dlSaveModelMaster(objPl);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int blUpdateModelMaster(PL_ModelMaster objpl)
        {
            try
            {
                return objDl.dlUpdateModelMaster(objpl);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable blDeleteUnusedModelDetails(PL_ModelMaster objpl)
        {
            try
            {
                return objDl.dlDeleteUnusedModel(objpl);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet blRetrieveModelDetails(PL_ModelMaster objpl)
        {
            try
            {
                return objDl.dlRetrieveModelDetails(objpl);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
