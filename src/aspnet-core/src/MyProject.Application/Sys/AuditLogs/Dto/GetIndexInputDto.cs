using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Sys.AuditLogs.Dto
{
    public class GetIndexInputDto: PagedResultRequestDto
    {
        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }
        /// <summary>
        /// 时间范围-起
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 时间范围-终
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 服务
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public string UserName { get; set; }
    }
}
