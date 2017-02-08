using Ls.IoC;

namespace Ls.Domain.Repositories {
    /// <summary>
    /// 仓储接口，仓储以实例方式注入到框架。
    /// </summary>
    public interface IRepository : ITransientDependency {
    }
}
