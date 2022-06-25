using BAL.DataAccessLayer;
using System.Web.Mvc;

namespace AGDiamond.Project.Settings
{
    public class RoleMastController : Controller
    {

        //public JsonResult Insert(User _user, Account _Account, DocumentDetail _D, string docList)
        //{
        //    if (_Account.name != null)
        //    {
        //        var Data = BAL.Account.Insert(_user, _Account, _D, docList);
        //        return Json(Data);
        //    }
        //    return Json(string.Empty);
        //}


        public JsonResult Select(Role_Mast _Role, BAL.FilterSetting _f)
        {
            var Data = BAL.Project.Settings.Role.Select(_Role, _f);
            return Helper.JsonMax(Data);
        }
    }
}