
namespace Ls.Dto.Response
{
    /// <summary>
    /// 响应实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseDto<T>
    {
        /// <summary>
        /// 响应码
        /// </summary>
        public ResponseCode ResponseCode { get; set; }
        /// <summary>
        /// 响应描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public T Content { get; set; }
    }

    /// <summary>
    /// 响应码
    /// </summary>
    public enum ResponseCode
    {
        /// <summary>
        /// 登录过期
        /// </summary>
        Expired = 100,
        /// <summary>
        /// 成功
        /// </summary>
        Success = 200,
        /// <summary>
        /// 失败
        /// </summary>
        Failure = 500
    }
}
