using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ls.Dto
{
    public interface ITreeNodeDto<T> : IDto<string>
        where T : ITreeNodeDto<T>
    {
        /// <summary>
        /// 父级菜单Id
        /// </summary>
        string ParentId { get; set; }
        /// <summary>
        /// 子集元素列表
        /// </summary>
        List<T> children { get; set; }
    }
}
