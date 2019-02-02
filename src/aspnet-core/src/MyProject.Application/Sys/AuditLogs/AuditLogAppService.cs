using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Domain.Repositories;
using MyProject.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Abp.Linq.Extensions;
using Abp.Collections.Extensions;
using Abp.AutoMapper;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using System.Diagnostics;

namespace MyProject.Sys.AuditLogs
{
    public class AuditLogAppService : ApplicationService, IAuditLogAppService
    {
        private readonly IRepository<AuditLog, long> _auditLogRepository;
        private readonly IRepository<User, long> _userRepository;

        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="auditLogRepository">日志表</param>
        /// <param name="userRepository">用户表</param>
        public AuditLogAppService(
            IRepository<AuditLog, long> auditLogRepository,
            IRepository<User, long> userRepository
            )
        {
            _auditLogRepository = auditLogRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// 获取列表页内容
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<Dto.AuditLogShowIndexDto>> GetIndex(Dto.GetIndexInputDto input)
        {
            Logger.Info("开始获取日志列表");
            Stopwatch watch = new Stopwatch();
            watch.Start();
            var query = from auditLog in _auditLogRepository.GetAll()
                        join user in _userRepository.GetAll()
                        on auditLog.UserId equals user.Id
                        select new
                        {
                            AuditLog = auditLog,
                            User = user,
                        };
            Logger.InfoFormat("完成query{0}",watch.ElapsedMilliseconds);
            watch.Restart();
            Logger.Info("开始查询条件");
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.ServiceName), p => p.AuditLog.ServiceName == input.ServiceName)
                .WhereIf(!string.IsNullOrWhiteSpace(input.UserName), p => (p.User.Name + "" + p.User.Surname) == input.UserName)
                .WhereIf(input.StartTime != null, p => p.AuditLog.ExecutionTime.Date >= input.StartTime.Value.Date)
                .WhereIf(input.EndTime != null, p => p.AuditLog.ExecutionTime.Date <= input.StartTime.Value.Date)
                .WhereIf(input.Status == 1, p => p.AuditLog.Exception != "" && p.AuditLog.Exception != null)
                .WhereIf(input.Status == 2, p => p.AuditLog.Exception == "" || p.AuditLog.Exception == null);
            Logger.InfoFormat("完成条件查询{0}", watch.ElapsedMilliseconds);
            watch.Restart();
            Logger.Info("开始查询数量");
            var count = await query.CountAsync();
            Logger.InfoFormat("完成数量查询{0}", watch.ElapsedMilliseconds);
            watch.Restart();
            Logger.Info("开始查询结果");
            var list = await query.OrderByDescending(p => p.AuditLog.ExecutionTime).PageBy(input).ToListAsync();
            var returnList=list.Select(p => new Dto.AuditLogShowIndexDto()
            {
                Id = p.AuditLog.Id,
                ExecutionTime = p.AuditLog.ExecutionTime,
                UserName = p.User.FullName,
                ServiceName = p.AuditLog.ServiceName,
                MethodName = p.AuditLog.MethodName,
                ExecutionDuration = p.AuditLog.ExecutionDuration + "ms",
                ClientIpAddress = p.AuditLog.ClientIpAddress,
                ClientName = p.AuditLog.ClientName,
                BrowserInfo = p.AuditLog.BrowserInfo,
                Exception = p.AuditLog.Exception
            }).ToList();
            Logger.InfoFormat("完成结果查询{0}", watch.ElapsedMilliseconds);
            return new PagedResultDto<Dto.AuditLogShowIndexDto>(count, returnList);
        }

        /// <summary>
        /// 获取修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Dto.AuditLogDetailDto GetFroEdit(EntityDto input)
        {
            var data = (from auditLog in _auditLogRepository.GetAll()
                       join user in _userRepository.GetAll()
                       on auditLog.UserId equals user.Id
                       where auditLog.Id == input.Id
                       select new Dto.AuditLogDetailDto
                       {
                           UserFullName = user.FullName,
                           Exception = auditLog.Exception,
                           BrowserInfo = auditLog.BrowserInfo,
                           ClientName = auditLog.ClientName,
                           ClientIpAddress = auditLog.ClientIpAddress,
                           ExecutionDuration = auditLog.ExecutionDuration,
                           ExecutionTime = auditLog.ExecutionTime,
                           MethodName = auditLog.MethodName,
                           ServiceName = auditLog.ServiceName,
                           Parameters = auditLog.Parameters,
                           CustomData = auditLog.CustomData
                        }).FirstOrDefault();
            if (data != null)
            {
                return data;
            }
            else
            {
                throw new UserFriendlyException("数据丢失！");
            }
        }
    }
}
