using MyProject.Dto.Paged;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Users.Dto
{
    /// <summary>
    /// 人员选择Input
    /// </summary>
    public class LookupModelUserInput: PagedAndFilterInputDto
    {
        /// <summary>
        /// 角色显示名（数组）
        /// </summary>
        public string[] RoleNameArray { get; set; }
    }
}
