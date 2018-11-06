using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace MyProject.Authorization
{
    public class MyProjectAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            var pages = context.GetPermissionOrNull(PermissionNames.Pages);
            if (pages == null)
            {
                pages = context.CreatePermission(PermissionNames.Pages, L("全部"));
            }

            //context.CreatePermission(PermissionNames.Pages_Sys_Users, L("Users"));
            //context.CreatePermission(PermissionNames.Pages_Sys_Roles, L("Roles"));
            //context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);

            #region 系统管理
            {
                // 系统管理
                var pages_Sys = pages.CreateChildPermission(PermissionNames.Pages_Sys, L("系统管理"));
                #region 地区管理
                {
                    var pages_Sys_Places = pages_Sys.CreateChildPermission(PermissionNames.Pages_Sys_Places, L("地区管理"));
                    var pages_Sys_Places_ShowIndex = pages_Sys_Places.CreateChildPermission(PermissionNames.Pages_Sys_Places_ShowIndex, L("列表页查看"));
                    var pages_Sys_Places_CreateOrEdit = pages_Sys_Places.CreateChildPermission(PermissionNames.Pages_Sys_Places_CreateOrEdit, L("新增/修改"));
                    var pages_Sys_Places_Delete = pages_Sys_Places.CreateChildPermission(PermissionNames.Pages_Sys_Places_Delete, L("删除"));
                }
                #endregion
                #region 租户管理
                {
                    var pages_Sys_Tenants = pages_Sys.CreateChildPermission(PermissionNames.Pages_Sys_Tenants, L("租户管理"));
                    var pages_Sys_Tenants_ShowIndex = pages_Sys_Tenants.CreateChildPermission(PermissionNames.Pages_Sys_Tenants_ShowIndex, L("列表页查看"));
                }
                #endregion
                #region 用户管理
                {
                    var pages_Sys_Users = pages_Sys.CreateChildPermission(PermissionNames.Pages_Sys_Users, L("用户管理"));
                    var pages_Sys_Users_ShowIndex = pages_Sys_Users.CreateChildPermission(PermissionNames.Pages_Sys_Users_ShowIndex, L("列表页查看"));
                }
                #endregion
                #region 角色管理
                {
                    var pages_Sys_Roles = pages_Sys.CreateChildPermission(PermissionNames.Pages_Sys_Roles, L("角色管理"));
                    var pages_Sys_Roles_ShowIndex = pages_Sys_Roles.CreateChildPermission(PermissionNames.Pages_Sys_Roles_ShowIndex, L("列表页查看"));
                }
                #endregion
            }
            #endregion
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, MyProjectConsts.LocalizationSourceName);
        }
    }
}
