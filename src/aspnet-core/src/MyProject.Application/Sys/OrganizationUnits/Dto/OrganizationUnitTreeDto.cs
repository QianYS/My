using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Sys.OrganizationUnits.Dto
{
    [AutoMap(typeof(OrganizationUnit))]
    public class OrganizationUnitTreeDto : EntityDto<long>
    {
        public virtual string Code { get; set; }

        public virtual string Key { get { return Code; } }

        public virtual long? ParentId { get; set; }

        public virtual OrganizationUnitTreeDto[] Children { get; set; }

        public virtual string DisplayName { get; set; }

        public virtual string Title { get { return DisplayName; } }
    }
}
