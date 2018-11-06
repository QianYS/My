namespace MyProject.Authorization
{
    public static class PermissionNames
    {
        //public const string Pages_Tenants = "Pages.Tenants";

        //public const string Pages_Users = "Pages.Users";

        //public const string Pages_Roles = "Pages.Roles";

        public const string Pages = "Pages";

        #region 系统管理
        public const string Pages_Sys = Pages + ".Sys";

        #region 地区管理
        public const string Pages_Sys_Places = Pages_Sys + ".Places";
        public const string Pages_Sys_Places_ShowIndex = Pages_Sys_Places + ".ShowIndex";//显示列表页
        public const string Pages_Sys_Places_CreateOrEdit = Pages_Sys_Places + ".CreateOrEdit";//新增修改权限
        public const string Pages_Sys_Places_Delete = Pages_Sys_Places + ".Delete";//删除权限
        #endregion

        #region 租户管理
        public const string Pages_Sys_Tenants = Pages_Sys + ".Tenants";
        public const string Pages_Sys_Tenants_ShowIndex = Pages_Sys_Tenants + ".ShowIndex";//显示列表页
        #endregion

        #region 用户管理
        public const string Pages_Sys_Users = Pages_Sys + ".Users";
        public const string Pages_Sys_Users_ShowIndex = Pages_Sys_Users + ".ShowIndex";//显示列表页
        #endregion

        #region 角色管理
        public const string Pages_Sys_Roles = Pages_Sys + ".Roles";
        public const string Pages_Sys_Roles_ShowIndex = Pages_Sys_Roles + ".ShowIndex";//显示列表页
        #endregion
        #endregion
    }
}
