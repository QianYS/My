using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Dto.Paged
{
    public class PagedAndIdInputDto<T> : PagedResultRequestDto
    {
        public T Id { get; set; }
    }
}
