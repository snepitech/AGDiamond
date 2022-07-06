using BAL.DataAccessLayer;
using System.Web.Mvc;

namespace AGDiamond.Project.Settings
{
    public class RoleMastController : Controller
    {

        public JsonResult Insert(Role_Mast _Role, BAL.FilterSetting _f)
        {
            if (_Role.code != null)
            {
                var Data = BAL.Project.Settings.Role.Insert(_Role, _f);
                return Json(Data);
            }
            return Json(string.Empty);
        }

        public JsonResult Select(Role_Mast _Role, BAL.FilterSetting _f)
        {
            var Data = BAL.Project.Settings.Role.Select(_Role, _f);
            return Helper.JsonMax(Data);
        }
        public JsonResult Delete(Role_Mast _Role, BAL.FilterSetting _f)
        {
            var Data = BAL.Project.Settings.Role.Delete(_Role, _f);
            return Helper.JsonMax(Data);
        }
    }
}