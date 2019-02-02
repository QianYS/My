using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using MyProject.LoginAttempts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.LoginAttempts
{
    public class LoginAttemptAppService : ApplicationService, ILoginAttemptAppService
    {
        private readonly IRepository<UserLoginAttempt, long> _userLoginAttemptRepository;

        public LoginAttemptAppService(
            IRepository<UserLoginAttempt, long> userLoginAttemptRepository
            )
        {
            _userLoginAttemptRepository = userLoginAttemptRepository;
        }

        /// <summary>
        /// 获取列表页内容
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<LoginAttemptShowIndexDto>> GetIndex(Sys.LoginAttempts.Dto.GetIndexInputDto input)
        {
            var query = _userLoginAttemptRepository.GetAll();
            var x = query.ToList();
            var count = await query.CountAsync();
            var list = query.OrderBy(p => p.Id).PageBy(input).ToList();
            return new PagedResultDto<LoginAttemptShowIndexDto>(count, list.MapTo<List<LoginAttemptShowIndexDto>>());
        }
    }
}
