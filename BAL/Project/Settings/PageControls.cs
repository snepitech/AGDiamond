using BAL.DataAccessLayer;
using System;
using System.Collections;
using System.Data;
using System.Linq;
using SqlOps = ConfigLib.SqlServer.OperationSql;
using Val = ConfigLib.Validation.BOValidation;
namespace BAL.Project.Settings
{
    public class PageControls
    {
        private static SuccessResponse _SR = new SuccessResponse();
        private static ErrorResponse _ER = new ErrorResponse();
        private static DataModelDataContext dc = new DataModelDataContext();

        private static string mStrPageControls = "PageControls";
        public static dynamic Select(BAL.DataAccessLayer.PageControl _ac, BAL.FilterSetting _f)
        {
            try
            {
                DataSet _DS = new DataSet();
                SqlOps.Clear();
                Hashtable _HT = new Hashtable();
                SqlOps.AddParams(_HT, "qtype", "Select");
                if (_ac.page_id > 0) { SqlOps.AddParams(_HT, "page_id", _ac.page_id); }
                if (_f.page > 0) { SqlOps.AddParams(_HT, "page", _f.page); }
                if (_f.limit > 0) { SqlOps.AddParams(_HT, "limit", _f.limit); }
                if (_f.search != null) { SqlOps.AddParams(_HT, "search", _f.search); }
                if (_f.sortColumn != null) { SqlOps.AddParams(_HT, "sortColumn", _f.sortColumn); }
                if (_f.sortOrder != null) { SqlOps.AddParams(_HT, "sortOrder", _f.sortOrder); }
                SqlOps.FillDataSet(SqlOps.EnumServer.NewWorkSoft, _DS, mStrPageControls, StoredProcedures.spPageControList, SqlOps.GetParams(_HT), "");
                if (Val.IsEmptyDataSet(_DS) && Val.DataTableIsEmpty(_DS.Tables[mStrPageControls]))
                {
                    var data = Helper.ConvertDataTableList<PageControl>(_DS.Tables[mStrPageControls]).ToList();
                    _SR.message = CacheData.GetConstant(Constant.InsertSuccess);
                    _SR.data = new { count = _DS.Tables[mStrPageControls].Rows[0]["total_rows"], list = data };
                    return _SR;
                }
                _ER.message = CacheData.GetConstant(Constant.NoData);
                return _ER;
            }
            catch (Exception ex)
            {
                _ER.message = ex.Message;
                return _ER;
            }
        }

        public static dynamic Insert(BAL.DataAccessLayer.PageControl _ac, BAL.FilterSetting _f)
        {
            try
            {
                DataSet _DS = new DataSet();
                SqlOps.Clear();
                Hashtable _HT = new Hashtable();
                SqlOps.AddParams(_HT, "qtype", "Insert");
                SqlOps.AddParams(_HT, "id", _ac.id);
                SqlOps.AddParams(_HT, "page_id", _ac.page_id);
                SqlOps.AddParams(_HT, "button_name", _ac.button_name);
                SqlOps.AddParams(_HT, "button_color", _ac.button_color);
                SqlOps.AddParams(_HT, "is_active", _ac.is_active);
                SqlOps.AddParams(_HT, "added_by", _ac.added_by);
                SqlOps.FillDataSet(SqlOps.EnumServer.NewWorkSoft, _DS, mStrPageControls, StoredProcedures.spPageControList, SqlOps.GetParams(_HT), "");
                if (Val.IsEmptyDataSet(_DS) && Val.DataTableIsEmpty(_DS.Tables[mStrPageControls]))
                {
                    var data = Helper.ConvertDataTableList<Role_Mast>(_DS.Tables[mStrPageControls]).ToList();
                    _SR.message = CacheData.GetConstant(Constant.InsertSuccess);
                    _SR.data = new { count = _DS.Tables[mStrPageControls].Rows[0]["total_rows"], list = data };
                    return _SR;
                }
                _ER.message = CacheData.GetConstant(Constant.NoData);
                return _ER;
            }
            catch (Exception ex)
            {
                _ER.message = ex.Message;
                return _ER;
            }

        }
        public static dynamic Delete(BAL.DataAccessLayer.PageControl _ac, BAL.FilterSetting _f)
        {
            try
            {
                DataSet _DS = new DataSet();
                SqlOps.Clear();
                Hashtable _HT = new Hashtable();
                SqlOps.AddParams(_HT, "id", _ac.id);
                SqlOps.AddParams(_HT, "qtype", "Delete");
                int id = SqlOps.ExNonQuery(SqlOps.EnumServer.NewWorkSoft, StoredProcedures.spPageControList, SqlOps.GetParams());
                if (id != -1)
                {
                    _SR.message = CacheData.GetConstant(Constant.DeleteSuccess);
                    return _SR;
                }
                _ER.message = CacheData.GetConstant(Constant.DeleteError);
                return _ER;
            }
            catch (Exception ex)
            {
                _ER.message = ex.Message;
                return _ER;
            }
        }
    }
}
