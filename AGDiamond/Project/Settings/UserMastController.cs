using BAL.DataAccessLayer;
using System.Web.Mvc;

namespace AGDiamond.Project.Settings
{
    public class UserMastController : Controller
    {

        public JsonResult Select(User_Mast _Role, BAL.FilterSetting _f)
        {
            var Data = BAL.Project.Settings.User.Select(_Role, _f);
            return Helper.JsonMax(Data);
        }
    }
}