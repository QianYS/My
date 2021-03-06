﻿using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Dto.Paged
{
    public class PagedAndNullableIdInputDto<T> : PagedResultRequestDto where T : struct
    {
        public T? Id { get; set; }
    }
}
