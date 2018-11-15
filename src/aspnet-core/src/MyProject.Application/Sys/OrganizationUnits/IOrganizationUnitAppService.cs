using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyProject.Sys.OrganizationUnits.Dto;
using MyProject.Users.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Sys.OrganizationUnits
{
    /// <summary>
    /// 组织架构接口API
    /// </summary>
    public interface IOrganizationUnitAppService : IApplicationService
    {
        /// <summary>
        /// 新增组织单元
        /// </summary>
        /// <param name="input"><see cref="CreateOrUpdateOrganizationUnitInput"/></param>
        /// <returns><see cref="OrganizationUnitDto"/></returns>
        Task<OrganizationUnitDto> Create(CreateOrUpdateOrganizationUnitInput input);

        /// <summary>
        /// 更新组织单元
        /// </summary>
        /// <param name="input"><see cref="CreateOrUpdateOrganizationUnitInput"/></param>
        /// <returns><see cref="OrganizationUnitDto"/></returns>
        Task<OrganizationUnitDto> Update(CreateOrUpdateOrganizationUnitInput input);

        /// <summary>
        /// 获取全部组织架构（非树结构）
        /// </summary>
        /// <returns></returns>
        Task<List<OrganizationUnitDto>> GetAll();

        /// <summary>
        /// 获取全部组织架构（树结构）
        /// </summary>
        /// <returns></returns>
        Task<List<OrganizationUnitTreeDto>> GetAllTree();

        /// <summary>
        /// 获取组织架构By Code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<OrganizationUnitDto> GetOrganizationUnitByCode(string code);

        /// <summary>
        /// 根据组织架构Code获取用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<OrganizationUnitUserDto>> GetUserByOrganizationUnit(GetUserByOrganizationUnitInput input);

        /// <summary>
        /// 向组织单元添加用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task AddUsers(UsersToOrganizationUnitInput input);

        /// <summary>
        /// 从组织单元删除用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task RemoveUsers(UsersToOrganizationUnitInput input);
    }
}
