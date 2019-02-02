using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Sys.AuditLogs
{
    public interface IAuditLogAppService
    {
        /// <summary>
        /// 获取列表页内容
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<Dto.AuditLogShowIndexDto>> GetIndex(Dto.GetIndexInputDto input);

        /// <summary>
        /// 获取修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Dto.AuditLogDetailDto GetFroEdit(EntityDto input);
    }
}
