using BAL.DataAccessLayer;
using System.Web.Mvc;

namespace AGDiamond.Project.Settings
{
    public class PagePermissionListController : Controller
    {

        public JsonResult Select(PagePermission _Page, BAL.FilterSetting _f)
        {
            var Data = BAL.Project.Settings.PagePermissionListMast.Select(_Page, _f);
            return Helper.JsonMax(Data);
        }

        public JsonResult Insert(PagePermission _Page, BAL.FilterSetting _f)
        {
            var Data = BAL.Project.Settings.PagePermissionListMast.Insert(_Page, _f);
            return Helper.JsonMax(Data);
        }
        public JsonResult Delete(PagePermission _Page, BAL.FilterSetting _f)
        {
            var Data = BAL.Project.Settings.PagePermissionListMast.Delete(_Page, _f);
            return Helper.JsonMax(Data);
        }
    }
}