using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using MyProject.Authorization.Roles;
using MyProject.Authorization.Users;
using MyProject.MultiTenancy;

namespace MyProject.EntityFrameworkCore
{
    public class MyProjectDbContext : AbpZeroDbContext<Tenant, Role, User, MyProjectDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public MyProjectDbContext(DbContextOptions<MyProjectDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// 新增实体时设置修改时间修改人
        /// </summary>
        /// <param name="entityAsObj"></param>
        /// <param name="userId"></param>
        protected override void SetCreationAuditProperties(object entityAsObj, long? userId)
        {
            base.SetCreationAuditProperties(entityAsObj, userId);
            base.SetModificationAuditProperties(entityAsObj, userId);
        }


    }
}
