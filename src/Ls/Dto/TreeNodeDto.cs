using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ls.Dto
{
    public class TreeNodeDto<T> : BaseDto
        where T : TreeNodeDto<T>
    {
        public TreeNodeDto()
        {
            Children = new List<T>();
        }
        /// <summary>
        /// 父级菜单Id
        /// </summary>
        public string ParentId { get; set; }
        /// <summary>
        /// 子集元素列表
        /// </summary>
        public List<T> Children { get; set; }
    }
}
