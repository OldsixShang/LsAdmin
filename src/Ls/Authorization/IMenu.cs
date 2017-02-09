using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ls.Authorization
{
    /// <summary>
    /// 菜单
    /// </summary>
    public interface IMenu
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        string Url { get; set; }
    }
}
