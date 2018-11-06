using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Organizations;
using MyProject.Authorization;
using MyProject.Sys.OrganizationUnits.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Sys.OrganizationUnits
{
    public class OrganizationUnitAppService : ApplicationService, IOrganizationUnitAppService
    {
        private IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly OrganizationUnitManager _organizationUnitManager;

        public OrganizationUnitAppService(
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            OrganizationUnitManager organizationUnitManager
            )
        {
            _organizationUnitRepository = organizationUnitRepository;
            _organizationUnitManager = organizationUnitManager;
        }
        
        /// <summary>
        /// 新增组织单元
        /// </summary>
        /// <param name="input"><see cref="CreateOrUpdateOrganizationUnitInput"/></param>
        /// <returns><see cref="OrganizationUnitDto"/></returns>
        public async Task<OrganizationUnitDto> Create(CreateOrUpdateOrganizationUnitInput input)
        {
            var organization = new OrganizationUnit(AbpSession.TenantId, input.DisplayName, input.ParentId);
            await _organizationUnitManager.CreateAsync(organization);
            await CurrentUnitOfWork.SaveChangesAsync();

            var dto = organization.MapTo<OrganizationUnitDto>();
            return dto;
        }

        /// <summary>
        /// 更新组织单元
        /// </summary>
        /// <param name="input"><see cref="CreateOrUpdateOrganizationUnitInput"/></param>
        /// <returns><see cref="OrganizationUnitDto"/></returns>
        public async Task<OrganizationUnitDto> Update(CreateOrUpdateOrganizationUnitInput input)
        {
            var organization = await _organizationUnitRepository.GetAsync(input.Id.Value);
            organization.DisplayName = input.DisplayName;
            await _organizationUnitRepository.UpdateAsync(organization);

            var dto = organization.MapTo<OrganizationUnitDto>();
            return dto;
        }

        /// <summary>
        /// 获取全部组织架构（非树结构）
        /// </summary>
        /// <returns></returns>
        public async Task<List<OrganizationUnitDto>> GetAll()
        {
            return _organizationUnitRepository.GetAll().ToList().MapTo<List<OrganizationUnitDto>>();
        }

        /// <summary>
        /// 获取全部组织架构（树结构）
        /// </summary>
        /// <returns></returns>
        public async Task<List<OrganizationUnitTreeDto>> GetAllTree()
        {
            return _organizationUnitRepository.GetAll().ToList().MapTo<List<OrganizationUnitTreeDto>>().Where(p => p.ParentId == null).ToList();
        }

        /// <summary>
        /// 获取组织架构By Code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<OrganizationUnitDto> GetOrganizationUnitByCode(string code)
        {
            return _organizationUnitRepository.GetAll().Where(p => p.Code == code).FirstOrDefault().MapTo<OrganizationUnitDto>();
        }
    }
}
