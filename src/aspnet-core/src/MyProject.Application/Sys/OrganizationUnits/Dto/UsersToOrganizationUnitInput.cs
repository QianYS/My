using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyProject.Sys.OrganizationUnits.Dto
{
    /// <summary>
    /// 用户-组织架构dto
    /// </summary>
    public class UsersToOrganizationUnitInput
    {
        /// <summary>
        /// 用户编号数组
        /// </summary>
        [MinLength(1)]
        public long[] UserIds { get; set; }

        /// <summary>
        /// 组织架构编号
        /// </summary>
        [Required(ErrorMessage = "组织架构编号是必须的")]
        public string OrganizationUnitCode { get; set; }
    }
}
