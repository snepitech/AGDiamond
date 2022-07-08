using BAL.DataAccessLayer;
using System.Web.Mvc;

namespace AGDiamond.Project.Settings
{
    public class PageControlsController : Controller
    {

        public JsonResult Select(PageControl _Page, BAL.FilterSetting _f)
        {
            var Data = BAL.Project.Settings.PageControls.Select(_Page, _f);
            return Helper.JsonMax(Data);
        }

        public JsonResult Insert(PageControl _Page, BAL.FilterSetting _f)
        {
            var Data = BAL.Project.Settings.PageControls.Insert(_Page, _f);
            return Helper.JsonMax(Data);
        }
        public JsonResult Delete(PageControl _Page, BAL.FilterSetting _f)
        {
            var Data = BAL.Project.Settings.PageControls.Delete(_Page, _f);
            return Helper.JsonMax(Data);
        }
    }
}