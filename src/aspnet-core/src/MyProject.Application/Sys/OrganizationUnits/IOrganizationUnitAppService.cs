using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyProject.Sys.OrganizationUnits.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Sys.OrganizationUnits
{
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
    }
}
