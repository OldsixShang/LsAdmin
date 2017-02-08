using Ls.Domain.Entities;

namespace Ls.Configutation
{
    /// <summary>
    /// 配置键值对
    /// </summary>
    public class Setting:Entity
    {
        /// <summary>
        /// 键
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }


        public override string ToString()
        {
            return Name;
        }
    }
}