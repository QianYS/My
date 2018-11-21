using Abp.Application.Services.Dto;
using MyProject.Dto.Paged;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Sys.LoginAttempts.Dto
{
    public class GetIndexInputDto : PagedAndFilterInputDto
    {
        public GetIndexInputDto() { }

        public GetIndexInputDto(int skipCount, int maxResultCount, string filter = "")
            : base(filter, skipCount, maxResultCount)           //子类的第二个构造函数
        {

        }
    }
}
