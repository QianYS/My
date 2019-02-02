using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Sys.AuditLogs.Dto
{
    /// <summary>
    /// 日志详情Dto
    /// </summary>
    public class AuditLogDetailDto:EntityDto<long>
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserFullName { get; set; }
        /// <summary>
        /// 详情描述
        /// </summary>
        public string Exception { get; set; }
        /// <summary>
        /// 浏览器信息
        /// </summary>
        public string BrowserInfo { get; set; }
        /// <summary>
        /// 客户端名称
        /// </summary>
        public string ClientName { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        public string ClientIpAddress { get; set; }
        /// <summary>
        /// 持续时间
        /// </summary>
        public int ExecutionDuration { get; set; }
        /// <summary>
        /// 发生时间
        /// </summary>
        public DateTime ExecutionTime { get; set; }
        /// <summary>
        /// 操作名
        /// </summary>
        public string MethodName { get; set; }
        /// <summary>
        /// 服务名
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        public string Parameters { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public string CustomData { get; set; }
    }
}
