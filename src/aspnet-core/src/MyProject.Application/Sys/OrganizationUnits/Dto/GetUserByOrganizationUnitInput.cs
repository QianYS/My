using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Sys.OrganizationUnits.Dto
{
    public class GetUserByOrganizationUnitInput: PagedResultRequestDto
    {
        public string Code { get; set; }
    }
}
