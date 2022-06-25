using System.Collections.Generic;
using System.Web;

namespace BAL
{
    public class Constant
    {
        public static Dictionary<string, int> dicParameterType = new Dictionary<string, int>
        {
            {"SINGLE",1 },
            {"MIX",2 },
        };

        public static Dictionary<string, int> dicSettingType = new Dictionary<string, int>
        {
            {"Import",1 },
            {"Export",2 },
            {"Filter",3 },
            {"Display",4 },
            {"PropertyUpdate",5 },
        };

        public static Dictionary<string, string> ReceivableEmailTemplate = new Dictionary<string, string>
        {
            {"ReceivableEmailTemplate1",HttpContext.Current.Server.MapPath("~/EmailTemplate/mailFormat1.html")},
            {"ReceivableEmailTemplate2",HttpContext.Current.Server.MapPath("~/EmailTemplate/mailFormat2.html")},
            {"ReceivableEmailTemplate3",HttpContext.Current.Server.MapPath("~/EmailTemplate/mailFormat3.html")},
            {"ReceivableEmailTemplate4",HttpContext.Current.Server.MapPath("~/EmailTemplate/mailFormat4.html")},
            {"ReceiptEmailTemplate",HttpContext.Current.Server.MapPath("~/EmailTemplate/receiptEmailTemplate.html")},
            {"BalanceConfirmation",HttpContext.Current.Server.MapPath("~/EmailTemplate/balanceConfirmation.html")},
        };


        public static string ReceivableEmailTemplate1 = HttpContext.Current.Server.MapPath("~/EmailTemplate/mailFormat1.html");
        public static string ReceivableEmailTemplate2 = HttpContext.Current.Server.MapPath("~/EmailTemplate/mailFormat2.html");
        public static string ReceivableEmailTemplate3 = HttpContext.Current.Server.MapPath("~/EmailTemplate/mailFormat3.html");
        public static string ReceivableEmailTemplate4 = HttpContext.Current.Server.MapPath("~/EmailTemplate/mailFormat4.html");
        public static string SalePrint = HttpContext.Current.Server.MapPath("~/EmailTemplate/SalePrint.html");

        public static string MyRequestEmail = "MyRequestEmail";
        public static string MyRequestEmailTemplate = HttpContext.Current.Server.MapPath("~/EmailTemplate/myrequest.html");
        public static string RegistrationEmailTemplate = HttpContext.Current.Server.MapPath("~/EmailTemplate/registration.html");


        // Path
        public static string PDFPath = HttpContext.Current.Server.MapPath("~/PDF/");
        public static string ExcelPath = HttpContext.Current.Server.MapPath("~/Excels/");
        public static string CSVExcelPath = HttpContext.Current.Server.MapPath("~/CSV/");

        public static string PDF = "PDF/";

        public static string CompanyLogo = "CompanyLogo";
        public static string Excel = "ExcelPath";
        public static string UploadMasterPath = "UploadMasterPath";
        public static string UploadPurchasePath = "UploadPurchasePath";
        public static string SalepersonDocPath = "SalepersonDoc";
        public static string DayTerms = "DAYTERMS";

        // Success
        public static string Success = "Success";
        public static string InsertSuccess = "InsertSuccess";
        public static string UpdateSuccess = "UpdateSuccess";
        public static string DeleteSuccess = "DeleteSuccess";
        public static string ImportSuccess = "ImportSuccess";

        // Error
        public static string NoData = "NoData";
        public static string InsertError = "InsertError";
        public static string UpdateError = "UpdateError";
        public static string DeleteError = "DeleteError";
        public static string ImportError = "ImportError";

        // Validation
        public static string IDRequired = "IDRequired";
        public static string ValueUsedFurther = "ValueUsedFurther";
        public static string EmailVerify = "EmailVerify";
        public static string CheckMail = "CheckMail";
        public static string Required = "Is Required.";
        public static string CaratLessOrEqualStock = "CaratLessOrEqualStock";

        // Login
        public static string OTPFailed = "OTPFailed";
        public static string OTPCheck = "OTPCheck";
        public static string SuccessLogin = "SuccessLogin";
        public static string ChangePassword = "ChangePassword";
        public static string LinkExpired = "LinkExpired";

        //Token
        public static string Token = "Token";
        public static string DateFormat = "DateFormat";
        public static string email = "email";
    }
}
