using Ls.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ls.Mvc.Validate
{
    /// <summary>
    /// 验证操作
    /// </summary>
    public interface IValidation
    {
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="target">验证目标</param>
        ValidationResultCollection Validate(object target);
    }
}