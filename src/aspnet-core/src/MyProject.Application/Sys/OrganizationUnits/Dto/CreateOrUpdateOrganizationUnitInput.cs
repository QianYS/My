using Abp.Application.Services.Dto;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyProject.Sys.OrganizationUnits.Dto
{
    public class CreateOrUpdateOrganizationUnitInput : NullableIdDto<long>
    {
        public long? ParentId { get; set; }
        
        [Required]
        [StringLength(OrganizationUnit.MaxDisplayNameLength)]
        public string DisplayName { get; set; }
    }
}
