﻿using BAL.DataAccessLayer;
using System;
using System.Collections;
using System.Data;
using System.Linq;
using SqlOps = ConfigLib.SqlServer.OperationSql;
using Val = ConfigLib.Validation.BOValidation;
namespace BAL.Project.Settings
{
    public class PageListMast
    {
        private static SuccessResponse _SR = new SuccessResponse();
        private static ErrorResponse _ER = new ErrorResponse();
        private static DataModelDataContext dc = new DataModelDataContext();

        private static string mStrRole = "PageList";
        public static dynamic Select(BAL.DataAccessLayer.PageList _ac, BAL.FilterSetting _f)
        {
            try
            {
                DataSet _DS = new DataSet();
                SqlOps.Clear();
                Hashtable _HT = new Hashtable();
                SqlOps.AddParams(_HT, "qtype", "Select");
                if (_f.page > 0) { SqlOps.AddParams(_HT, "page", _f.page); }
                if (_f.limit > 0) { SqlOps.AddParams(_HT, "limit", _f.limit); }
                if (_f.search != null) { SqlOps.AddParams(_HT, "search", _f.search); }
                if (_f.sortColumn != null) { SqlOps.AddParams(_HT, "sortColumn", _f.sortColumn); }
                if (_f.sortOrder != null) { SqlOps.AddParams(_HT, "sortOrder", _f.sortOrder); }
                SqlOps.FillDataSet(SqlOps.EnumServer.NewWorkSoft, _DS, mStrRole, StoredProcedures.spPageList, SqlOps.GetParams(_HT), "");
                if (Val.IsEmptyDataSet(_DS) && Val.DataTableIsEmpty(_DS.Tables[mStrRole]))
                {
                    var data = Helper.ConvertDataTableList<PageList>(_DS.Tables[mStrRole]).ToList();
                    _SR.message = CacheData.GetConstant(Constant.InsertSuccess);
                    _SR.data = new { count = _DS.Tables[mStrRole].Rows[0]["total_rows"], list = data };
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
    }
}
