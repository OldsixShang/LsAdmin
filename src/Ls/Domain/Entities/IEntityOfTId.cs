namespace Ls.Domain.Entities {
    /// <summary>
    /// 实体对象接口。
    /// </summary>
    /// <typeparam name="TId">对象标识的类型</typeparam>
    public interface IEntity<TId> {
        /// <summary>
        /// 对象的标识。
        /// </summary>
        TId Id { get; set; }
    }
}
