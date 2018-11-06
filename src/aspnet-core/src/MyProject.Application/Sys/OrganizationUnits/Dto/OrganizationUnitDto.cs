using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Sys.OrganizationUnits.Dto
{
    [AutoMap(typeof(OrganizationUnit))]
    public class OrganizationUnitDto:EntityDto<long>
    {
        public virtual string Code { get; set; }
        public virtual long? ParentId { get; set; }
        public virtual int? TenantId { get; set; }
        public virtual string DisplayName { get; set; }
    }
}
