using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Dto.Paged
{
    public class PagedAndFilterInputDto : PagedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
