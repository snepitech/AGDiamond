using BAL.DataAccessLayer;
using System.Web.Mvc;

namespace AGDiamond.Project.Settings
{
    public class PageListController : Controller
    {

        public JsonResult Select(PageList _Page, BAL.FilterSetting _f)
        {
            var Data = BAL.Project.Settings.PageListMast.Select(_Page, _f);
            return Helper.JsonMax(Data);
        }

        public JsonResult Insert(PageList _Page, BAL.FilterSetting _f)
        {
            var Data = BAL.Project.Settings.PageListMast.Select(_Page, _f);
            return Helper.JsonMax(Data);
        }
        public JsonResult Delete(PageList _Page, BAL.FilterSetting _f)
        {
            var Data = BAL.Project.Settings.PageListMast.Delete(_Page, _f);
            return Helper.JsonMax(Data);
        }
    }
}