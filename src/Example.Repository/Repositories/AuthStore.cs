
using System;
using System.Collections.Generic;
using Ls.Authorization;
using Ls.EntityFramework.Repositories;
using Example.Domain.Entities.Authorization;

namespace Example.Repository.Repositories
{
    public class AuthStore : EfRepository<PlatformDbContext, IUser>, IAuthStore<User, Role, Permission, AuthAction, Menu>
    {
        public ICollection<Permission> GetPermissions(long? roleId)
        {
            throw new NotImplementedException();
        }

        public Role GetRole(long? roleId)
        {
            throw new NotImplementedException();
        }

        public User GetUser(long? userId)
        {
            throw new NotImplementedException();
        }
    }
}
