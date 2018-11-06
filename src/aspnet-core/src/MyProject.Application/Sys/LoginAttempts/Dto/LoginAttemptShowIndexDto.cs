using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.LoginAttempts.Dto
{
    [AutoMap(typeof(UserLoginAttempt))]
    public class LoginAttemptShowIndexDto : EntityDto<long>
    {
        /// <summary>
        /// 租户名称
        /// </summary>
        public string TenancyName { get; set; }
        /// <summary>
        /// 用户名称或者邮箱
        /// </summary>
        public string UserNameOrEmailAddress { get; set; }
        /// <summary>
        /// 客户端IP地址
        /// </summary>
        public string ClientIpAddress { get; set; }
        /// <summary>
        /// 客户端名称
        /// </summary>
        public string ClientName { get; set; }
        /// <summary>
        /// 浏览器信息
        /// </summary>
        public string BrowserInfo { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}
