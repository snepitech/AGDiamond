using BAL.DataAccessLayer;
using System.Web.Mvc;

namespace AGDiamond.Project.Settings
{
    public class PageListController : Controller
    {

        public JsonResult Select(PageList _Role, BAL.FilterSetting _f)
        {
            var Data = BAL.Project.Settings.PageListMast.Select(_Role, _f);
            return Helper.JsonMax(Data);
        }
    }
}