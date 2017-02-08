using System;

namespace Ls.Authorization {
    /// <summary>
    /// 用于应用层的方法，标识方法对应的权限。
    /// </summary>
    public class AuthorizeAttribute : Attribute {
        /// <summary>
        /// 方法对应的权限组。
        /// </summary>
        public string[] Permissions { get; private set; }

        /// <summary>
        /// 创建<see cref="AuthorizeAttribute"/>对象。
        /// </summary>
        /// <param name="permissions">方法对应的权限组。</param>
        public AuthorizeAttribute(params string[] permissions) {
            Permissions = permissions;
        }
    }
}
