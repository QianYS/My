using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyProject.Dto.Paged;
using MyProject.Roles.Dto;

namespace MyProject.Roles
{
    public interface IRoleAppService : IAsyncCrudAppService<RoleDto, int, PagedResultRequestDto, CreateRoleDto, RoleDto>
    {
        /// <summary>
        /// 获取所有权限
        /// </summary>
        /// <returns></returns>
        Task<ListResultDto<PermissionDto>> GetAllPermissions();

        /// <summary>
        /// 获取权限树
        /// </summary>
        /// <returns></returns>
        Task<ListResultDto<PermissionDto>> GetAllPermissionsTree();

        /// <summary>
        /// 获取修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetRoleForEditOutput> GetRoleForEdit(EntityDto input);


        Task<ListResultDto<RoleListDto>> GetRolesAsync(GetRolesInput input);

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<RoleDto>> GetRolesIndexList(PagedAndFilterInputDto input);
    }
}