using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Roles.Dto
{
    public class CreateOrUpdateInput
    {
        public RoleEditDto Role { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}
