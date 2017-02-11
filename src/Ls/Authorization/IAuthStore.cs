using Ls.Domain.Repositories;
using System.Collections.Generic;

namespace Ls.Authorization
{
    /// <summary>
    /// 用户接口
    /// </summary>
    public interface IAuthStore
    {
        IUser GetUser(long? userId);

        IUser GetRole(long? roleId);

        ICollection<IPermission> GetPermissions(long? roleId);
    }
}