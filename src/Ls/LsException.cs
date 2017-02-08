using System;
using System.Runtime.Serialization;

namespace Ls {
    /// <summary>
    /// Ls 框架异常基类。
    /// </summary>
    [Serializable]
    public class LsException : Exception {

        /// <summary>
        /// 
        /// </summary>
        public LsExceptionEnum LsExceptionEnum { get; set; }  

        /// <summary>
        /// 默认构造函数。
        /// </summary>
        public LsException() {
        }

        /// <summary>
        /// Ls 异常构造函数。
        /// </summary>
        /// <param name="message">异常消息</param>
        public LsException(string message) : base(message) {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="LsExceptionEnum"></param>
        public LsException(string message, LsExceptionEnum LsExceptionEnum) : base(message)
        {
            LsExceptionEnum = LsExceptionEnum;
        }
        
        /// <summary>
        /// Ls 异常构造函数。
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="innerException">内部异常</param>
        public LsException(string message, Exception innerException) : base(message, innerException) {
        }

        /// <summary>
        /// Ls 异常构造函数。
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected LsException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum LsExceptionEnum
    {
        /// <summary>
        /// 业务异常
        /// </summary>
        BusinessException = 0,
        /// <summary>
        /// 用户没有登录
        /// </summary>
        NoLogin,
        /// <summary>
        /// 用户没有角色
        /// </summary>
        NoRole,
        /// <summary>
        /// 用户没有权限访问
        /// </summary>
        NoPermission
    }
}
