using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ls.Utilities.Formatter
{
    /// <summary>
    /// 时间格式化字符
    /// </summary>
    public class DateTimeFormatterConstString
    {
        /// <summary>
        /// 标准日期格式 yyyy-MM-dd
        /// </summary>
        public const string DateStandard = "yyyy-MM-dd";
        /// <summary>
        /// 标准中文日期格式 yyyy年MM月dd日
        /// </summary>
        public const string DateStandardCn = "yyyy年MM月dd日";
        /// <summary>
        /// 标准时间格式24小时制  yyyy-MM-dd HH:mm:ss
        /// </summary>
        public const string DateTimeH24 = "yyyy-MM-dd HH:mm:ss";
        /// <summary>
        /// 标准中文时间格式24小时制  yyyy年MM月dd日 HH:mm:ss
        /// </summary>
        public const string DateTimeH24Cn = "yyyy年MM月dd日 HH:mm:ss";
    }
}
