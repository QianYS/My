using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Authorization;
using System.Collections.Generic;

namespace MyProject.Roles.Dto
{
    [AutoMapFrom(typeof(Permission))]
    public class PermissionDto : EntityDto<long>
    {
        public string Name { get; set; }

        public string key { get { return Name; } }

        public string DisplayName { get; set; }

        public string Title { get { return DisplayName; } }

        public string Description { get; set; }

        public PermissionDto[] Children { get; set; }
    }
}
