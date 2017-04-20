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
    public class PermissionRepository : EfRepository<PlatformDbContext, Permission>, IPermissionRepository
    {
        public dynamic QueryPermission(string name = "")
        {
            var query = from t1 in Context.Permissions
                        join t3 in Context.Menus on t1.MenuId equals t3.Id into t3s
                        join t4 in Context.Actions on t1.ActionId equals t4.Id into t4s
                        from menu in t3s.DefaultIfEmpty()
                        from ac in t4s.DefaultIfEmpty()
                        select new
                        {
                            Id = t1.Id,
                            ParentId = (long?)t1.Parent.Id,
                            Name = t1.Name,
                            MenuId = (long?)t1.MenuId,
                            ActionId = (long?)t1.ActionId,
                            Url = menu.Url,
                            MenuType = (MenuType?)menu.MenuType,
                            MenuName = menu.Name,
                            ActionName = ac.Name,
                            HtmlTemplete = ac.Template
                        };
            if (!string.IsNullOrEmpty(name)) query = query.Where(t => t.Name.Contains(name));
            return query;
        }

        public dynamic GetPermission(long id)
        {
            var query = Context.Permissions.Where(t => t.Id == id);
            return query.FirstOrDefault();
        }

        public dynamic QueryMenuPermission(long roleId, MenuType menuType)
        {
            var role = Context.Roles.Include(t => t.Permissions).Where(t => t.Id == roleId).FirstOrDefault();
            var perms = role.Permissions.Select(t => t.Id).ToArray();
            var query = from t1 in Context.Permissions
                        join t3 in Context.Menus on t1.MenuId equals t3.Id into t3s
                        join t4 in Context.Actions on t1.ActionId equals t4.Id into t4s
                        from menu in t3s.DefaultIfEmpty()
                        from ac in t4s.DefaultIfEmpty()
                        select new
                        {
                            Id = t1.Id,
                            ParentId = (long?)t1.Parent.Id,
                            Name = t1.Name,
                            MenuId = (long?)t1.MenuId,
                            ActionId = (long?)t1.ActionId,
                            Url = menu.Url,
                            MenuType = (MenuType?)menu.MenuType,
                            MenuName = menu.Name,
                            ActionName = ac.Name,
                            HtmlTemplete = ac.Template,
                            Icon = menu.Icon
                        };
            query = query.Where(t => t.MenuType == menuType)
                .Where(t => t.ActionId == null);
            if (roleId > 0)
                query = query.Where(t => perms.Contains(t.Id));
            return query;
        }

        public dynamic QueryActionPermission(long roleId, long parentId)
        {
            var role = Context.Roles.Include(t => t.Permissions).Where(t => t.Id == roleId).FirstOrDefault();
            var perms = role.Permissions.Select(t => t.Id).ToArray();
            var query = from t1 in Context.Permissions
                        join t3 in Context.Menus on t1.MenuId equals t3.Id into t3s
                        join t4 in Context.Actions on t1.ActionId equals t4.Id into t4s
                        from menu in t3s.DefaultIfEmpty()
                        from ac in t4s.DefaultIfEmpty()
                        select new
                        {
                            Id = t1.Id,
                            ParentId = (long?)t1.Parent.Id,
                            Name = t1.Name,
                            MenuId = (long?)t1.MenuId,
                            ActionId = (long?)t1.ActionId,
                            Url = menu.Url,
                            MenuType = (MenuType?)menu.MenuType,
                            MenuName = menu.Name,
                            ActionName = ac.Name,
                            HtmlTemplate = ac.Template
                        };
            if (parentId > 0) query = query.Where(t => t.ParentId == parentId);
             query = query.Where(t => t.ActionId != null);
            if (roleId > 0) query = query.Where(t => perms.Contains(t.Id));
            return query;
        }
    }
}
