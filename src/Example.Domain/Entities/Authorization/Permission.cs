﻿using System;
using System.Collections.Generic;
using Ls.Authorization;
using Ls.Domain.Entities;

namespace Example.Domain.Entities.Authorization
{
    /// <summary>
    /// 权限拓展
    /// </summary>
    public class Permission : Entity, IPermission<Permission, AuthAction, Menu>
    {
        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 菜单Id
        /// </summary>
        public long? MenuId { get; set; }
        /// <summary>
        /// 操作Id
        /// </summary>
        public long? ActionId { get; set; }
        /// <summary>
        /// 操作
        /// </summary>
        public virtual AuthAction Action { get; set; }
        /// <summary>
        /// 菜单
        /// </summary>
        public virtual Menu Menu { get; set; }

        public Permission Parent { get; set; }

        public ICollection<Permission> Children { get; set; }
    }
}