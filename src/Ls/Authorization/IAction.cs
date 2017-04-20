using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ls.Authorization
{
    /// <summary>
    /// 权限操作
    /// </summary>
    public interface IAction
    {
        /// <summary>
        /// 操作名称
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 操作模板
        /// </summary>
        string Template { get; set; }
    }
}
