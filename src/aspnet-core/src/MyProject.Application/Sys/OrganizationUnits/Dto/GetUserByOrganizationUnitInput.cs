using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyProject.Sys.OrganizationUnits.Dto
{
    public class GetUserByOrganizationUnitInput: PagedResultRequestDto
    {
        /// <summary>
        /// 组织架构编号
        /// </summary>
        [Required(ErrorMessage = "组织架构编号是必须的")]
        public string Code { get; set; }
    }
}
