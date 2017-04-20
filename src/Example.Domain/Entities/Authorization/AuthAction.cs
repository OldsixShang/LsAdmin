using Ls.Authorization;
using Ls.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Domain.Entities.Authorization
{
    /// <summary>
    /// 操作动作
    /// </summary>
    public class AuthAction : Entity,IAction
    {
        /// <summary>
        /// 操作动作名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 操作模板
        /// </summary>
        public string Template { get; set; }
        /// <summary>
        /// 操作描述
        /// </summary>
        public string Description { get; set; }
    }
}
