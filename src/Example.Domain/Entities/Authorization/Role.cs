
using System;
using System.Collections.Generic;
using Ls.Authorization;
using Ls.Domain.Entities;

namespace Example.Domain.Entities.Authorization
{
    public class Role : SoftDeleteEntity, IRole<Role, Permission, AuthAction, Menu>
    {
        public string Name { get; set; }
        public ICollection<Permission> Permissions { get; set; }
        public string Description { get; set; }
        public int TenantId { get; set; }
    }
}
