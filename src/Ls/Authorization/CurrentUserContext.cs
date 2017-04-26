using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ls.Authorization
{
    /// <summary>
    /// 用户上下文对象
    /// </summary>
    [Serializable]
    public class CurrentUserContext : IUser
    {
        public DateTime CreatedTime { get; set; }

        public string Id { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime LastUpdatedTime { get; set; }

        public string LoginId { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string RoleId { get; set; }

        public int TenantId { get; set; }
    }
}
