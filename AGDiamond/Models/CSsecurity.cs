using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;

namespace AGDiamond

{

    public class CSsecurity
    {


        public static Guid? GetCurrentUserId(System.Security.Principal.IPrincipal User)
        {
            if (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier) != null)
            {
                return Guid.Parse(((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            }
            else
            {
                return null;
            }
        }
        public static string GetCurrentUserRole(System.Security.Principal.IPrincipal User)
        {
            return ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
        }
        public static Guid? GetCurrentUserCompany(System.Security.Principal.IPrincipal User)
        {
            string company_id = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(c => c.Type == "company_id").Value;
            return (string.IsNullOrEmpty(company_id) || company_id == Guid.Empty.ToString() ? null : (Guid?)Guid.Parse(company_id));
        }
        public static Guid? GetCurrentUserDepartment(System.Security.Principal.IPrincipal User)
        {
            string department_id = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(c => c.Type == "department_id").Value;
            return (string.IsNullOrEmpty(department_id) || department_id == Guid.Empty.ToString() ? null : (Guid?)Guid.Parse(department_id));
        }
        public static Guid? GetCurrentUserYear(System.Security.Principal.IPrincipal User)
        {
            string year_id = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(c => c.Type == "year_id").Value;

            return (string.IsNullOrEmpty(year_id) || year_id == Guid.Empty.ToString() ? null : (Guid?)Guid.Parse(year_id));
        }

        public static dynamic GetKYCDetailOnAccessToken(System.Security.Principal.IPrincipal User, Guid? userId = null)
        {


            System.Data.DataSet dtUserDetail = new System.Data.DataSet();
            if (userId != null)
            {
                // dtUserDetail = SqlConnectionOperation.GetStoredProcedure("[GetKYCDetailByUserId]", "user_id=" + userId);
            }
            else
            {
                Guid? user_id = GetCurrentUserId(User);
                if (user_id == null || user_id == Guid.Empty)
                    return null;
                //dtUserDetail = SqlConnectionOperation.GetStoredProcedure("[GetKYCDetailByUserId]", "user_id=" + user_id);
            }
            if (dtUserDetail.Tables.Count == 0 || dtUserDetail.Tables[0].Rows.Count == 0)
                return null;
            var kycDetail = (from System.Data.DataRow DR in dtUserDetail.Tables[0].Rows
                             select new
                             {
                                 account_id = DR["account_id"] is DBNull ? null : DR["account_id"],
                                 company_id = DR["company_id"] is DBNull ? null : DR["company_id"],
                                 department_id = DR["department_id"] is DBNull ? null : DR["department_id"],

                                 account_name = DR["account_name"],
                                 term_id = DR["term_id"] is DBNull ? null : DR["term_id"],
                                 term_name = DR["term_name"],
                                 day_terms = DR["day_terms"],
                                 user_id = DR["user_id"] is DBNull ? null : DR["user_id"],
                                 employee_id = DR["employee_id"] is DBNull ? null : DR["employee_id"],
                                 broker_id = DR["broker_id"] is DBNull ? null : DR["broker_id"],
                                 broker_name = DR["broker_name"],
                                 multidepartment_discount = DR["multidepartment_discount"],
                             }).ToList();
            return kycDetail;

            #region Old Code
            //DataModelDataContext dc = new DataModelDataContext();
            ////Set Static Term ID and Account Id on User ID 
            //var kycDetail = (from u in dc.Users
            //                 from a in dc.Accounts
            //                 where a.id == u.ref_id && u.id == GetCurrentUserId(User)
            //                 && (a.id != null && a.id != Guid.Empty)
            //                 && (u.term_id != null && u.term_id != Guid.Empty)
            //                 select new
            //                 {
            //                     account_id = a.id,
            //                     account_name = a.name,
            //                     term_id = u.term_id,
            //                     term_name = u.TermsDetail.term_name,
            //                     day_terms = u.TermsDetail.Master2.name,
            //                     user_id = u.id,
            //                     employee_id = a.employee_id
            //                 }).ToList();
            //return kycDetail; 
            #endregion
        }

        // Generate random string in capital and lower case
        public static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        public static string Md5Hash(string text)
        {
            string final_string = "";
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            // code for substring of original string 
            final_string = strBuilder.ToString().Substring(4, strBuilder.ToString().Length - 8);

            return final_string;

        }


        public static string EncryptText(string openText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(openText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {


                        csEncrypt.Write(clearBytes, 0, clearBytes.Length);
                        csEncrypt.Close();




                    }
                    openText = Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
            return openText;
        }


        public static string DecryptText(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream msDecrypt = new MemoryStream())
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        csDecrypt.Write(cipherBytes, 0, cipherBytes.Length);
                        csDecrypt.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(msDecrypt.ToArray());
                }
            }

            return cipherText;
        }

    }

    // Added to customize response when user is not Authenticated or Authorized for web api
    public class CustomAuthorization : AuthorizeAttribute
    {
        bool validUser = true;
        protected override void HandleUnauthorizedRequest(System.Web.Mvc.AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated || !validUser)
                filterContext.Result = new JsonResult { Data = new { flag = false, message = "Unauthorized", code = HttpStatusCode.Unauthorized } };
            else
                filterContext.Result = new JsonResult { Data = new { flag = false, message = "Forbidden", code = HttpStatusCode.Forbidden } };
        }

        //protected override bool AuthorizeCore(HttpContextBase httpContext)
        //{
        //    try
        //    {
        //        var req = httpContext.Items["MS_HttpRequestMessage"];
        //        return true; //Priyank  
        //        var token = ((System.Net.Http.HttpRequestMessage)req).Headers.Authorization.Parameter;
        //        DataModelDataContext dc = new DataModelDataContext();
        //        var item = dc.LoginActivities.FirstOrDefault(x => x.access_token == token);
        //        if (item == null && !httpContext.Request.Url.IsLoopback)
        //        {
        //            //validUser = false;
        //            return true;
        //        }
        //        return httpContext.Request.Url.IsLoopback || httpContext.User.Identity.IsAuthenticated;//authenticate local user or with valid access token

        //        //var routeData = httpContext.Request.RequestContext.RouteData;
        //        //string controllerName = routeData.GetRequiredString("controller").ToLower();
        //        //string actionName = routeData.GetRequiredString("action").ToLower();

        //        //Guid currentUserId = CSsecurity.GetCurrentUserId(httpContext.User);
        //        //string currentUserRoleId = CSsecurity.GetCurrentUserRole(httpContext.User);
        //        //if (currentUserRoleId == "") return true;
        //        //return Permission.hasPermission(controllerName, actionName, currentUserRoleId, currentUserId);
        //    }
        //    catch (Exception ex)
        //    {
        //        return true;
        //    }
        //}
    }

    public class Permission
    {
        public static List<ApiPermission> ApiPermissionList;
        public static void getAllUserPermission()
        {

            // ApiPermissionList = Helper.DataTableToList<ApiPermission>(SqlConnectionOperation.FetchDataTable("select controller_name,action_name,isnull(a.allow_all, 1) as allow_all,role_id,user_id/*,[insert],[update],[delete],[view]*/ from ApiList a  with(nolock) left join RoleApiMapping r with(nolock) on r.api_id = a.id"));
        }
        public static bool hasPermission(string controllerName, string actionName, string roleId, Guid userId)
        {
            return ApiPermissionList.Where(c => c.controller_name.ToLower() == controllerName && c.action_name.ToLower() == actionName && (c.allow_all == 1 || (Convert.ToString(c.role_id) == roleId && c.user_id == userId))).Count() > 0;
        }
    }
    public class ApiPermission
    {
        public string controller_name { get; set; }
        public string action_name { get; set; }
        public short allow_all { get; set; }
        public Guid role_id { get; set; }
        public Guid user_id { get; set; }

    }
}
