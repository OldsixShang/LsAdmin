using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ls.Validation
{
    /// <summary>
    /// 自定义模型验证接口
    /// </summary>
    public interface ICustomValidate
    {
        /// <summary>
        /// 添加自定义的模型错误信息
        /// </summary>
        /// <param name="results"></param>
        void AddValidationErrors(List<ValidationResult> results);
    }
}