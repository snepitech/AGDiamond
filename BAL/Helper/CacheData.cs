using BAL.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using SqlOps = ConfigLib.SqlServer.OperationSql;
namespace BAL

{
    public static class CacheData
    {
        private static DataSet _DS = new DataSet();
        private static DataModelDataContext dc = new DataModelDataContext();
        private static string mStrCacheData = "cacheData";


        public static DataTable SelectAllMasterCache()
        {
            DataTable dtAllMaster;
            if (string.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Cache["MasterCache"])))
            {
                SqlOps.DataSetClear(_DS);
                DataTable dtMasterSub = new DataTable();
                SqlOps.Clear();
                SqlOps.AddParams("qtype", "Select");
                SqlOps.FillDataSet(SqlOps.EnumServer.NewWorkSoft, _DS, mStrCacheData, StoredProcedures.spMaster, SqlOps.GetParams(), "");
                if (_DS.Tables[mStrCacheData].Columns.Count != 0)
                {
                    dtMasterSub = _DS.Tables[mStrCacheData];
                }
                if (dtMasterSub.Columns.Count != 0)
                {
                    HttpContext.Current.Cache.Insert("MasterCache", dtMasterSub);
                }
            }
            dtAllMaster = (DataTable)HttpContext.Current.Cache["MasterCache"];

            return dtAllMaster;
        }

        public static DataTable SelectAllUserCache()
        {
            DataTable dtAllMaster;
            if (HttpContext.Current.Cache["UserCache"] == null)
            {
                SqlOps.DataSetClear(_DS);
                DataTable dtMasterSub = new DataTable();
                SqlOps.Clear();
                SqlOps.FillDataSet(SqlOps.EnumServer.NewWorkSoft, _DS, mStrCacheData, StoredProcedures.spUser, SqlOps.GetParams(), "");
                if (_DS.Tables[mStrCacheData].Columns.Count != 0)
                {
                    dtMasterSub = _DS.Tables[0];
                }
                if (dtMasterSub.Columns.Count != 0)
                {
                    HttpContext.Current.Cache.Insert("UserCache", dtMasterSub);
                }
            }
            dtAllMaster = (DataTable)HttpContext.Current.Cache["UserCache"];

            return dtAllMaster;
        }

        public static DataTable SelectAllAccountCache()
        {
            DataTable dtAllMaster = new DataTable();
            if (HttpContext.Current.Cache["AccountCache"] == null)
            {
                SqlOps.DataSetClear(_DS);
                //======= All KYC Detail ============
                SqlOps.Clear();
                SqlOps.AddParams("is_active", 1);
                SqlOps.AddParams("qtype", "Select");
                SqlOps.FillDataSet(SqlOps.EnumServer.NewWorkSoft, _DS, mStrCacheData, StoredProcedures.spAccount, SqlOps.GetParams(), "");
                if (_DS.Tables[mStrCacheData].Rows.Count != 0)
                {
                    dtAllMaster = _DS.Tables[mStrCacheData];
                }
                if (dtAllMaster.Rows.Count != 0)
                {
                    HttpContext.Current.Cache.Insert("AccountCache", dtAllMaster);
                }
            }
            else
                dtAllMaster = (DataTable)HttpContext.Current.Cache["AccountCache"];

            return dtAllMaster;
        }

        public static string GetConstant(string _key)
        {
            try
            {
                List<DataAccessLayer.Constant> listConstant = new List<DataAccessLayer.Constant>();
                if (HttpContext.Current.Cache["Constant"] == null)
                {
                    listConstant = dc.Constants.Where(x => x.is_active == true).ToList();
                    HttpContext.Current.Cache.Insert("Constant", listConstant);
                }
                else
                    listConstant = (List<DataAccessLayer.Constant>)HttpContext.Current.Cache["Constant"];

                return Convert.ToString(listConstant.Where(x => x.Key == _key).FirstOrDefault().Value);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static DataTable SelectLastRapCache()
        {
            DataTable dtRapPrice = new DataTable();
            if (HttpContext.Current.Cache["RapCache"] == null)
            {
                SqlOps.DataSetClear(_DS);
                //======= All KYC Detail ============
                SqlOps.Clear();
               
                SqlOps.AddParams("qtype", "SelectLastPriceList");
                SqlOps.FillDataSet(SqlOps.EnumServer.NewWorkSoft, _DS, mStrCacheData, StoredProcedures.spRapPriceSelect, SqlOps.GetParams(), "");
                if (_DS.Tables[mStrCacheData].Rows.Count != 0)
                {
                    dtRapPrice = _DS.Tables[mStrCacheData];
                }
                if (dtRapPrice.Rows.Count != 0)
                {
                    HttpContext.Current.Cache.Insert("RapCache", dtRapPrice);
                }

               
            }
            else
                dtRapPrice = (DataTable)HttpContext.Current.Cache["RapCache"];
                return dtRapPrice;
        }


    }
}
