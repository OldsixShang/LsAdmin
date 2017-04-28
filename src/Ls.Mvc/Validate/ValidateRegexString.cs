using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ls.Mvc.Validate
{
    /// <summary>
    /// 正则表达式
    /// </summary>
    public class ValidateRegexString
    {
        /// <summary>
        /// 整数
        /// </summary>
        public const string Integer = @"^-?\d+$";
        /// <summary>
        /// 大于零的整数
        /// </summary>
        public const string PositiveInteger = @"^[0-9]*[1-9][0-9]*$";
        /// <summary>
        /// 非负整数
        /// </summary>
        public const string Negative = @"^[0-9]*[0-9][0-9]*$";
        /// <summary>
        /// 数字
        /// </summary>
        public const string Decimal = @"^(-?\d+)(\.\d+)?$";
        /// <summary>
        /// 邮政编码
        /// </summary>
        public const string ZIPCode = @"^[1-9]\d{5}$";
        /// <summary>
        /// 电话和手机
        /// </summary>
        public const string TelAndMobilePhone = @"(^(\d{3,4}-)?\d{7,8}$)|(^1[2|3|4|5|6|7|8|9][0-9]\d{4,8}$)";
        /// <summary>
        /// 邮箱
        /// </summary>
        public const string Email = @"^(\w)+(\.\w+)*@(\w)+((\.\w+)+)$";
        /// <summary>
        /// 英文字符或数字
        /// </summary>
        public const string EngCodeMexNumber = @"[a-zA-Z0-9]+";
    }
}