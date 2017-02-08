using System;

namespace Ls.Domain.Entities {
    /// <summary>
    /// 实体基类，默认标识类型为 Int64。
    /// </summary>
    public abstract class Entity : Entity<Int64> {
    }
}
