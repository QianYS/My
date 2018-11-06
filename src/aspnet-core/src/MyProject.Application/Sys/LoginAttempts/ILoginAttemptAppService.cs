using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyProject.LoginAttempts.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.LoginAttempts
{
    public interface ILoginAttemptAppService : IApplicationService
    {
        Task<PagedResultDto<LoginAttemptShowIndexDto>> GetIndex(Sys.LoginAttempts.Dto.GetIndexInputDto input);
    }
}
