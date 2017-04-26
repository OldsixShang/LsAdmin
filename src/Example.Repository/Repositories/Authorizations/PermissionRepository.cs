using Ls.Authorization;
using Ls.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Example.Domain.Entities.Authorization;
using Example.Domain.Repositories.Authorization;
using Example.Repository;

namespace ExampleRepository.Repositories.Authorizations
{
    /// <summary>
    /// 权限仓储实现
    /// </summary>
    public class PermissionRepository : EfRepository<ExampleDbContext, Permission>, IPermissionRepository
    {
        public List<Permission> QueryPermission(string name = "")
        {
            var query = Context.Permissions.Include(t => t.Menu).Include(t => t.Action);
            if (!string.IsNullOrEmpty(name)) query = query.Where(t => t.Name.Contains(name));
            return query.ToList();
        }

        public Permission GetPermission(string id)
        {
            var query = Context.Permissions.Where(t => t.Id == id);
            return query.FirstOrDefault();
        }

        public List<Permission> QueryMenuPermission(string roleId, MenuType menuType)
        {
            var role = Context.Roles.Include(t => t.Permissions).Where(t => t.Id == roleId).FirstOrDefault();
            var query = Context.Permissions.Include(t => t.Menu).Include(t => t.Action);
            var perms = role.Permissions.Select(t => t.Id).ToArray();
            query = query.Where(t => t.Menu.MenuType == menuType).Where(t => t.ActionId == null);
            if (!string.IsNullOrEmpty(roleId)) query = query.Where(t => perms.Contains(t.Id));
            return query.ToList();
        }

        public List<Permission> QueryActionPermission(string roleId, string parentId)
        {
            var role = Context.Roles.Include(t => t.Permissions).Where(t => t.Id == roleId).FirstOrDefault();
            var query = Context.Permissions.Include(t => t.Menu).Include(t => t.Action).Where(t => t.ActionId != null);
            var perms = role.Permissions.Select(t => t.Id).ToArray();
            if (!string.IsNullOrEmpty(roleId)) query = query.Where(t => perms.Contains(t.Id));
            if (!string.IsNullOrEmpty(parentId)) query = query.Where(t => t.ParentId == parentId);
            return query.ToList();
        }
    }
}
