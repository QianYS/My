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
        /// 子类构造函数
        /// </summary>
        /// <param name="skipCount">跳过条数</param>
        /// <param name="maxResultCount">每页最大显示条数</param>
        /// <param name="filter">查询参数</param>
        /// <param name="roleNameArray">查询角色</param>
        public LookupModelUserInput(int skipCount, int maxResultCount, string filter = "", string[] roleNameArray = null)
            : base(filter, skipCount, maxResultCount)           
        {
            this.RoleNameArray = roleNameArray;
        }

        /// <summary>
        /// 角色显示名（数组）
        /// </summary>
        public string[] RoleNameArray { get; set; }
    }
}
