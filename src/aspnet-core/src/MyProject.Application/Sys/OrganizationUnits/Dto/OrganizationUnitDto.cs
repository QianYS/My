using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyProject.Sys.OrganizationUnits.Dto
{
    /// <summary>
    /// 组织架构Dto
    /// </summary>
    [AutoMap(typeof(OrganizationUnit))]
    public class OrganizationUnitDto:EntityDto<long>
    {
        /// <summary>
        /// 组织架构编号
        /// </summary>
        public virtual string Code { get; set; }
        /// <summary>
        /// 上级组织架构Id（可空）
        /// </summary>
        public virtual long? ParentId { get; set; }
        /// <summary>
        /// 租主Id（可空）
        /// </summary>
        public virtual int? TenantId { get; set; }
        /// <summary>
        /// 组织架构显示名称（必填）
        /// </summary>
        [Required(ErrorMessage = "组织架构编号是必须的")]
        [StringLength(OrganizationUnit.MaxDisplayNameLength,ErrorMessage = "组织架构显示名长度错误")]
        public virtual string DisplayName { get; set; }
    }
}
