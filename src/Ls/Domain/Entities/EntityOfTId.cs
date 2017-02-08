namespace Ls.Domain.Entities {
    /// <summary>
    /// 实体基类。
    /// </summary>
    /// <typeparam name="TId">标识类型参数</typeparam>
    public abstract class Entity<TId> : IEntity<TId> {
        /// <summary>
        /// 实体标识。
        /// </summary>
        public virtual TId Id { get; set; }

        /// <summary>
        /// 判断实体相等。
        /// </summary>
        /// <param name="obj">实体对象</param>
        /// <returns></returns>
        public override bool Equals(object obj) {
            if (obj == null || !(obj is Entity<TId>)) {
                return false;
            }

            if (ReferenceEquals(this, obj)) {
                return true;
            }

            var other = (Entity<TId>)obj;

            var typeOfThis = GetType();
            var typeOfOther = other.GetType();
            if (!typeOfThis.IsAssignableFrom(typeOfOther) && !typeOfOther.IsAssignableFrom(typeOfThis)) {
                return false;
            }

            return Id.Equals(other.Id);
        }

        /// <summary>
        /// 获取实体对象的 HashCode。
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() {
            return Id.GetHashCode();
        }

        /// <summary>
        /// 重写 == 操作符。
        /// </summary>
        /// <param name="left">左操作值</param>
        /// <param name="right">右操作值</param>
        /// <returns></returns>
        public static bool operator ==(Entity<TId> left, Entity<TId> right) {
            if (Equals(left, null)) {
                return Equals(right, null);
            }

            return left.Equals(right);
        }

        /// <summary>
        /// 重写 != 操作符。
        /// </summary>
        /// <param name="left">左操作值</param>
        /// <param name="right">右操作值</param>
        /// <returns></returns>
        public static bool operator !=(Entity<TId> left, Entity<TId> right) {
            return !(left == right);
        }
    }
}
