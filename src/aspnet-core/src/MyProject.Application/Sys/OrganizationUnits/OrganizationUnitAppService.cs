using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Organizations;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using MyProject.Authorization;
using MyProject.Authorization.Users;
using MyProject.Sys.OrganizationUnits.Dto;
using MyProject.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Sys.OrganizationUnits
{
    /// <summary>
    /// 组织架构接口实现
    /// </summary>
    public class OrganizationUnitAppService : ApplicationService, IOrganizationUnitAppService
    {
        private IRepository<User, long> _userRepository;
        private IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private IRepository<UserOrganizationUnit, long> _userOrganizationUnitRepository;
        private readonly UserManager _userManager;
        private readonly OrganizationUnitManager _organizationUnitManager;

        /// <summary>
        /// 组织架构依赖注入
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="organizationUnitRepository"></param>
        /// <param name="userOrganizationUnitRepository"></param>
        /// <param name="userManager"></param>
        /// <param name="organizationUnitManager"></param>
        public OrganizationUnitAppService(
            IRepository<User, long> userRepository,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository,
            UserManager userManager,
            OrganizationUnitManager organizationUnitManager
            )
        {
            _userRepository = userRepository;
            _organizationUnitRepository = organizationUnitRepository;
            _userOrganizationUnitRepository = userOrganizationUnitRepository;
            _userManager = userManager;
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
            var list = await _organizationUnitRepository.GetAll().ToListAsync();
            return list.MapTo<List<OrganizationUnitDto>>();
        }

        /// <summary>
        /// 获取全部组织架构（树结构）
        /// </summary>
        /// <returns></returns>
        public async Task<List<OrganizationUnitTreeDto>> GetAllTree()
        {
            var list = await _organizationUnitRepository.GetAll().ToListAsync();
            return list.MapTo<List<OrganizationUnitTreeDto>>().Where(p => p.ParentId == null).ToList();
        }

        /// <summary>
        /// 获取组织架构By Code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<OrganizationUnitDto> GetOrganizationUnitByCode(string code)
        {
            OrganizationUnit organizationUnit = await _organizationUnitRepository.GetAll().Where(p => p.Code == code).FirstOrDefaultAsync();
            if (organizationUnit == null)
            {
                throw new UserFriendlyException("组织架构信息丢失!");
            }
            else
            {
                return organizationUnit.MapTo<OrganizationUnitDto>();
            }
        }

        /// <summary>
        /// 根据组织架构Code获取用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<OrganizationUnitUserDto>> GetUserByOrganizationUnit(GetUserByOrganizationUnitInput input)
        {
            OrganizationUnit organizationUnit = _organizationUnitRepository.GetAll().Where(p => p.Code == input.Code).FirstOrDefault();
            if (organizationUnit != null)
            {
                var query = from a in _userOrganizationUnitRepository.GetAll()
                            join b in _userRepository.GetAll()
                            on a.UserId equals b.Id
                            join c in _organizationUnitRepository.GetAll()
                            on a.OrganizationUnitId equals c.Id
                            where a.OrganizationUnitId == organizationUnit.Id
                            select new OrganizationUnitUserDto()
                            {
                                Id = a.Id,
                                UserId = a.UserId,
                                Name = b.Name,
                                UserName = b.UserName,
                                OrganizationUnitCode = c.Code,
                                AddedTime = a.CreationTime
                            };
                var count = await query.CountAsync();
                var list = await query.ToListAsync();
                return new PagedResultDto<OrganizationUnitUserDto>(count, list);
            }
            else
            {
                throw new UserFriendlyException("组织架构信息丢失!");
            }
        }

        /// <summary>
        /// 向组织单元添加用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddUsers(UsersToOrganizationUnitInput input)
        {
            OrganizationUnit organizationUnit = _organizationUnitRepository.GetAll().Where(p => p.Code == input.OrganizationUnitCode).FirstOrDefault();
            if (organizationUnit != null)
            {
                foreach (long userId in input.UserIds)
                {
                    await _userManager.AddToOrganizationUnitAsync(userId, organizationUnit.Id);
                }
            }
            else
            {
                throw new UserFriendlyException("组织架构信息丢失!");
            }
        }

        /// <summary>
        /// 从组织单元删除用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task RemoveUsers(UsersToOrganizationUnitInput input)
        {
            OrganizationUnit organizationUnit = _organizationUnitRepository.GetAll().Where(p => p.Code == input.OrganizationUnitCode).FirstOrDefault();
            if (organizationUnit != null)
            {
                foreach (long userId in input.UserIds)
                {
                    await _userManager.RemoveFromOrganizationUnitAsync(userId, organizationUnit.Id);
                }
            }
            else
            {
                throw new UserFriendlyException("组织架构信息丢失!");
            }
        }
    }
}
