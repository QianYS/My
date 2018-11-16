using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Sys.OrganizationUnits.Dto
{
    public class OrganizationUnitUserDto : EntityDto<long>
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 组织架构编号
        /// </summary>
        public string OrganizationUnitCode { get; set; }

        /// <summary>
        /// 添加到组织单元的时间
        /// </summary>
        public DateTime AddedTime { get; set; }
    }
}
