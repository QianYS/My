using Abp.Application.Services.Dto;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyProject.Sys.OrganizationUnits.Dto
{
    /// <summary>
    /// 创建或修改组织架构-传入
    /// </summary>
    public class CreateOrUpdateOrganizationUnitInput : NullableIdDto<long>
    {
        /// <summary>
        /// 上级组织架构Id（可空）
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// 组织架构显示名称（必填）
        /// </summary>
        [Required(ErrorMessage = "组织架构编号是必须的")]
        [StringLength(OrganizationUnit.MaxDisplayNameLength, ErrorMessage = "组织架构显示名长度错误")]
        public string DisplayName { get; set; }
    }
}
