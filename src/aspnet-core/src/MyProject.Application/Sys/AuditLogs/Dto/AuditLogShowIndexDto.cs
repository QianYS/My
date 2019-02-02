using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Sys.AuditLogs.Dto
{
    /// <summary>
    /// 日志列表页Dto
    /// </summary>
    public class AuditLogShowIndexDto:EntityDto<long>
    {
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime ExecutionTime { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 服务名
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// 操作
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// 持续时间
        /// </summary>
        public string ExecutionDuration { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string ClientIpAddress { get; set; }

        /// <summary>
        /// 客户端
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// 浏览器
        /// </summary>
        public string BrowserInfo { get; set; }

        /// <summary>
        /// 详情
        /// </summary>
        public string Exception { get; set; }
    }
}
