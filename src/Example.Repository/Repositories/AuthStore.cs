using System;
using System.Collections.Generic;
using Ls.Authorization;
using Ls.EntityFramework.Repositories;
using Example.Domain.Entities.Authorization;
using Ls.Caching;
using Example.Domain.Repositories.Authorization;
using System.Linq;

namespace Example.Repository.Repositories
{
    public class AuthStore : EfRepository<ExampleDbContext, IUser>, IAuthStore
    {
        private readonly ICacheManager _cacheManager;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IRoleRepository _roleRepository;

        public AuthStore(ICacheManager cacheManager, 
            IPermissionRepository permissionRepository, 
            IRoleRepository roleRepository)
        {
            _cacheManager = cacheManager;
            _permissionRepository = permissionRepository;
            _roleRepository = roleRepository;
        }

        public ICollection<IPermission> GetPermissions(long? roleId)
        {
            string rolePermissionCacheKey = string.Format(CacheKeys.ALL_PERMISSIONS, roleId);
            if (!_cacheManager.IsSet(rolePermissionCacheKey))
            {
                dynamic permissions = _permissionRepository.QueryActionPermission(roleId.Value, -1);
                //缓存权限数据
                _cacheManager.Set(rolePermissionCacheKey, permissions, 20);
            }
            return _cacheManager.Get<ICollection<IPermission>>(rolePermissionCacheKey);
        }

        public IRole GetRole(long? roleId)
        {
            return _roleRepository.Get(roleId.Value);
        }

        public IUser GetUser(long? userId)
        {
            return Context.Users.Where(t => t.Id == userId).FirstOrDefault();
        }
    }
}
