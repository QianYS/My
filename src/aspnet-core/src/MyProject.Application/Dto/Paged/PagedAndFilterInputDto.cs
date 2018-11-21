using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Dto.Paged
{
    public class PagedAndFilterInputDto : PagedResultRequestDto
    {
        public string Filter { get; set; }

        public PagedAndFilterInputDto()
        {

        }

        public PagedAndFilterInputDto(string filter,int skipCount, int maxResultCount)
        {
            this.Filter = filter;
            this.SkipCount = skipCount;
            this.MaxResultCount = maxResultCount;
        }
    }
}
