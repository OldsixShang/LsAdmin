using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Ls.Validation {
    /// <summary>
    /// 框架数据验证异常。
    /// </summary>
    [Serializable]
    public class LsValidationException : LsException {
        /// <summary>
        /// 数据验证错误信息。
        /// </summary>
        public List<ValidationResult> ValidationErrors { get; set; }

        /// <summary>
        /// 默认构造函数。
        /// </summary>
        public LsValidationException() {
            ValidationErrors = new List<ValidationResult>();
        }

        /// <summary>
        /// 异常构造函数。
        /// </summary>
        /// <param name="message">异常消息</param>
        public LsValidationException(string message) : base(message) {
            ValidationErrors = new List<ValidationResult>();
        }

        /// <summary>
        /// 异常构造函数。
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="innerException">内部异常</param>
        public LsValidationException(string message, Exception innerException) : base(message, innerException) {
            ValidationErrors = new List<ValidationResult>();
        }

        /// <summary>
        /// 异常构造函数。
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected LsValidationException(SerializationInfo info, StreamingContext context) : base(info, context) {
            ValidationErrors = new List<ValidationResult>();
        }
    }
}
