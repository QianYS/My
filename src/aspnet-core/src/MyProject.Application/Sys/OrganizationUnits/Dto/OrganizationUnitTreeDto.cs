using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Sys.OrganizationUnits.Dto
{
    /// <summary>
    /// 组织架构树结构
    /// </summary>
    [AutoMap(typeof(OrganizationUnit))]
    public class OrganizationUnitTreeDto : EntityDto<long>
    {
        /// <summary>
        /// 组织架构编号
        /// </summary>
        public virtual string Code { get; set; }
        /// <summary>
        /// 树结构编号
        /// </summary>
        public virtual string Key { get { return Code; } }
        /// <summary>
        /// 上级组织架构Id（可空）
        /// </summary>
        public virtual long? ParentId { get; set; }
        /// <summary>
        /// 下级组织架构
        /// </summary>
        public virtual OrganizationUnitTreeDto[] Children { get; set; }
        /// <summary>
        /// 组织架构显示名
        /// </summary>
        public virtual string DisplayName { get; set; }
        /// <summary>
        /// 树结构显示名
        /// </summary>
        public virtual string Title { get { return DisplayName; } }
    }
}
