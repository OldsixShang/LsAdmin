
using System;
using System.Collections.Generic;
using Ls.Authorization;
using Ls.EntityFramework.Repositories;
using Example.Domain.Entities.Authorization;

namespace Example.Repository.Repositories
{
    public class AuthStore : EfRepository<ExampleDbContext, IUser>, IAuthStore
    {
        public ICollection<IPermission> GetPermissions(long? roleId)
        {
            throw new NotImplementedException();
        }

        public IRole GetRole(long? roleId)
        {
            throw new NotImplementedException();
        }

        public IUser GetUser(long? userId)
        {
            throw new NotImplementedException();
        }
    }
}
