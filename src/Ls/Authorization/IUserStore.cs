using Ls.Domain.Repositories;

namespace Ls.Authorization
{
    /// <summary>
    /// 用户接口
    /// </summary>
    public interface IUserStore : IRepository<IUser>
    {
         
    }
}