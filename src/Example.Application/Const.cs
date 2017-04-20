using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Application
{
    /// <summary>
    /// 缓存键值
    /// </summary>
    public class CacheKeys
    {
        /// <summary>
        /// 所有缓存
        /// formatter:{0} - 角色Id
        /// </summary>
        public const string ALL_PERMISSIONS = "AllActionPermission_{0}";
    }
}
