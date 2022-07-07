namespace BAL
{
    public class StoredProcedures
    {
        #region Dashboard
        public static string spDashboard = "USP_Dashboard";

        #endregion Dashboard

        #region Utility
        public static string spUtilityMasterSave = "USP_UtilityMaster_Save";
        #endregion Utility

        #region Master
        public static string spMaster = "USP_Master";
        public static string spMasterCache = "sp_Master_SelectAll";
        #endregion Master

        #region User
        public static string spUser = "USP_User";
        public static string spUserActivityLog = "USP_UserActivityLog";
        #endregion User


        #region Account
        public static string spAccount = "USP_Account_Save";
        public static string spAccountCache = "sp_Account_SelectAll";
        public static string spAccountName = "USP_Account_SelectName";
        public static string spDocumentUpload = "USP_DocumentDetail_Upsert";
        public static string spDocumentGetImage = "USP_GetDocumentImage";
        public static string spDocumentGetDeleteImage = "USP_GetDeleteDocumentImage";
        #endregion Account


        #region Role
        public static string spRole = "USP_Role_Select";
        #endregion Role

        public static string spRapPriceSelect = "USP_RapPrice_SelectAll";

        #region PageList
        public static string spPageList = "USP_PageList";

        #endregion

        #region PagePermissionList
        public static string spPagePermissionList = "USP_PagePermissionList";

        #endregion
    }
}
