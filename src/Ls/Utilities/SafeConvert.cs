using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ls.Utilities
{
    public static class SafeConvert
    {
        #region 数值转换
        /// <summary>
        /// 转换为32位整型
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static int ToInt32(object data, int defaultVal = 0)
        {
            if (data == null)
                return defaultVal;
            int result;
            var success = int.TryParse(data.ToString(), out result);
            if (success == true)
                return result;
            return defaultVal;
        }

        /// <summary>
        /// 转换为可空32位整型
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static int? ToInt32OrNull(object data, int defaultVal)
        {
            if (data == null)
                return null;
            int result;
            bool isValid = int.TryParse(data.ToString(), out result);
            if (isValid)
                return result;
            return defaultVal;
        }

        /// <summary>
        /// 转换为64位整型
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static long ToInt64(object data, long defaultVal = 0)
        {
            if (data == null)
                return defaultVal;
            long result;
            var success = long.TryParse(data.ToString(), out result);
            if (success == true)
                return result;
            return defaultVal;
        }

        /// <summary>
        /// 转换为可空64位整型
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static long? ToInt64OrNull(object data, long defaultVal)
        {
            if (data == null)
                return null;
            long result;
            bool isValid = long.TryParse(data.ToString(), out result);
            if (isValid)
                return result;
            return defaultVal;
        }

        /// <summary>
        /// 转换为双精度浮点数
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="defaultVal">默认值</param>
        public static double ToDouble(object data, double defaultVal = 0)
        {
            if (data == null)
                return defaultVal;
            double result;
            return double.TryParse(data.ToString(), out result) ? result : defaultVal;
        }

        /// <summary>
        /// 转换为双精度浮点数,并按指定的小数位4舍5入
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="digits">小数位数</param>
        /// <param name="defaultVal">默认值</param>
        public static double ToDouble(object data, int digits, double defaultVal = 0)
        {
            return Math.Round(ToDouble(data, defaultVal), digits);
        }

        /// <summary>
        /// 转换为可空双精度浮点数
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="defaultVal">默认值</param>
        public static double? ToDoubleOrNull(object data, double defaultVal = 0)
        {
            if (data == null)
                return null;
            double result;
            bool isValid = double.TryParse(data.ToString(), out result);
            if (isValid)
                return result;
            return defaultVal;
        }

        /// <summary>
        /// 转换为高精度浮点数
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="defaultVal">默认值</param>
        public static decimal ToDecimal(object data, decimal defaultVal = 0)
        {
            if (data == null)
                return defaultVal;
            decimal result;
            return decimal.TryParse(data.ToString(), out result) ? result : defaultVal;
        }

        /// <summary>
        /// 转换为高精度浮点数,并按指定的小数位4舍5入
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="digits">小数位数</param>
        /// <param name="defaultVal">默认值</param>
        public static decimal ToDecimal(object data, int digits, decimal defaultVal = 0)
        {
            return Math.Round(ToDecimal(data, defaultVal), digits);
        }

        /// <summary>
        /// 转换为可空高精度浮点数
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="defaultVal">默认值</param>
        public static decimal? ToDecimalOrNull(object data, decimal defaultVal = 0)
        {
            if (data == null)
                return null;
            decimal result;
            bool isValid = decimal.TryParse(data.ToString(), out result);
            if (isValid)
                return result;
            return defaultVal;
        }

        /// <summary>
        /// 转换为可空高精度浮点数,并按指定的小数位4舍5入
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="digits">小数位数</param>
        public static decimal? ToDecimalOrNull(object data, int digits, decimal defaultVal)
        {
            var result = ToDecimalOrNull(data);
            if (result == null)
                return null;
            return Math.Round(result.Value, digits);
        }

        #endregion

        #region Guid转换

        /// <summary>
        /// 转换为Guid
        /// </summary>
        /// <param name="data">数据</param>
        public static Guid ToGuid(object data)
        {
            if (data == null)
                return Guid.Empty;
            Guid result;
            return Guid.TryParse(data.ToString(), out result) ? result : Guid.Empty;
        }

        /// <summary>
        /// 转换为可空Guid
        /// </summary>
        /// <param name="data">数据</param>
        public static Guid? ToGuidOrNull(object data)
        {
            if (data == null)
                return null;
            Guid result;
            bool isValid = Guid.TryParse(data.ToString(), out result);
            if (isValid)
                return result;
            return null;
        }

        /// <summary>
        /// 转换为Guid集合
        /// </summary>
        /// <param name="guid">guid集合字符串，范例:83B0233C-A24F-49FD-8083-1337209EBC9A,EAB523C6-2FE7-47BE-89D5-C6D440C3033A</param>
        public static List<Guid> ToGuidList(string guid)
        {
            var listGuid = new List<Guid>();
            if (string.IsNullOrWhiteSpace(guid))
                return listGuid;
            var arrayGuid = guid.Split(',');
            listGuid.AddRange(from each in arrayGuid where !string.IsNullOrWhiteSpace(each) select new Guid(each));
            return listGuid;
        }

        #endregion

        #region 日期转换
        /// <summary>
        /// 转换为日期
        /// </summary>
        /// <param name="data">数据</param>
        public static DateTime ToDate(object data)
        {
            if (data == null)
                return DateTime.MinValue;
            DateTime result;
            return DateTime.TryParse(data.ToString(), out result) ? result : DateTime.MinValue;
        }

        /// <summary>
        /// 转换为可空日期
        /// </summary>
        /// <param name="data">数据</param>
        public static DateTime? ToDateOrNull(object data)
        {
            if (data == null)
                return null;
            DateTime result;
            bool isValid = DateTime.TryParse(data.ToString(), out result);
            if (isValid)
                return result;
            return null;
        }

        #endregion

        #region 布尔转换

        /// <summary>
        /// 转换为布尔值
        /// </summary>
        /// <param name="data">数据</param>
        public static bool ToBool(object data)
        {
            if (data == null)
                return false;
            bool? value = GetBool(data);
            if (value != null)
                return value.Value;
            bool result;
            return bool.TryParse(data.ToString(), out result) && result;
        }

        /// <summary>
        /// 获取布尔值
        /// </summary>
        private static bool? GetBool(object data)
        {
            switch (data.ToString().Trim().ToLower())
            {
                case "0":
                    return false;
                case "1":
                    return true;
                case "是":
                    return true;
                case "否":
                    return false;
                case "yes":
                    return true;
                case "no":
                    return false;
                case "true":
                    return true;
                case "false":
                    return false;
                default:
                    return null;
            }
        }

        /// <summary>
        /// 转换为可空布尔值
        /// </summary>
        /// <param name="data">数据</param>
        public static bool? ToBoolOrNull(object data)
        {
            if (data == null)
                return null;
            bool? value = GetBool(data);
            if (value != null)
                return value.Value;
            bool result;
            bool isValid = bool.TryParse(data.ToString(), out result);
            if (isValid)
                return result;
            return null;
        }

        #endregion

        #region 字符串转换
        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <param name="data">数据</param>
        public static string ToString(object data)
        {
            return data == null ? string.Empty : data.ToString().Trim();
        }

        #endregion

        #region 通用转换
        /// <summary>
        /// 泛型转换
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="data">数据</param>
        public static T To<T>(object data)
        {
            if (data == null || string.IsNullOrWhiteSpace(data.ToString()))
                return default(T);
            Type type = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
            try
            {
                if (type.Name.ToLower() == "guid")
                    return (T)(object)new Guid(data.ToString());
                if (data is IConvertible)
                    return (T)Convert.ChangeType(data, type);
                return (T)data;
            }
            catch
            {
                return default(T);
            }
        }

        #endregion

        #region 特殊转化
        /// <summary>
        /// 金额小写转中文大写。
        /// 整数支持到万亿；小数部分支持到分(超过两位将进行Banker舍入法处理)
        /// </summary>
        /// <param name="Num">需要转换的双精度浮点数</param>
        /// <returns>转换后的字符串</returns>
        public static string ToChineseNumberUpper(double Num)
        {
            String[] Ls_ShZ = { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖", "拾" };
            String[] Ls_DW_Zh = { "元", "拾", "佰", "仟", "万", "拾", "佰", "仟", "亿", "拾", "佰", "仟", "万" };
            String[] Num_DW = { "", "拾", "佰", "仟", "万", "拾", "佰", "仟", "亿", "拾", "佰", "仟", "万" };
            String[] Ls_DW_X = { "角", "分" };

            Boolean iXSh_bool = false;//是否含有小数，默认没有(0则视为没有)
            Boolean iZhSh_bool = true;//是否含有整数,默认有(0则视为没有)
            Boolean negative = false;

            string NumStr;//整个数字字符串
            string NumStr_Zh;//整数部分
            string NumSr_X = "";//小数部分
            string NumStr_DQ;//当前的数字字符
            string NumStr_R = "";//返回的字符串

            Num = Math.Round(Num, 2);//四舍五入取两位

            //各种非正常情况处理
            if (Num < 0)
            {
                Num = Math.Abs(Num);
                negative = true;
                //return "无效值";
            }
            if (Num > 9999999999999.99)
                return "无效值";
            if (Num == 0)
                return Ls_ShZ[0];

            //判断是否有整数
            if (Num < 1.00)
                iZhSh_bool = false;

            NumStr = Num.ToString();

            NumStr_Zh = NumStr;//默认只有整数部分
            if (NumStr_Zh.Contains("."))
            {//分开整数与小数处理
                NumStr_Zh = NumStr.Substring(0, NumStr.IndexOf("."));
                NumSr_X = NumStr.Substring((NumStr.IndexOf(".") + 1), (NumStr.Length - NumStr.IndexOf(".") - 1));
                iXSh_bool = true;
            }


            if (NumSr_X == "" || int.Parse(NumSr_X) <= 0)
            {//判断是否含有小数部分
                iXSh_bool = false;
            }

            if (iZhSh_bool)
            {//整数部分处理
                NumStr_Zh = Reversion_Str(NumStr_Zh);//反转字符串

                for (int a = 0; a < NumStr_Zh.Length; a++)
                {//整数部分转换
                    NumStr_DQ = NumStr_Zh.Substring(a, 1);
                    if (int.Parse(NumStr_DQ) != 0)
                        NumStr_R = Ls_ShZ[int.Parse(NumStr_DQ)] + Ls_DW_Zh[a] + NumStr_R;
                    else if (a == 0 || a == 4 || a == 8)
                    {
                        if (NumStr_Zh.Length > 8 && a == 4)
                            continue;
                        NumStr_R = Ls_DW_Zh[a] + NumStr_R;
                    }
                    else if (int.Parse(NumStr_Zh.Substring(a - 1, 1)) != 0)
                        NumStr_R = Ls_ShZ[int.Parse(NumStr_DQ)] + NumStr_R;

                }
                if (negative) NumStr_R = "负" + NumStr_R;
                if (!iXSh_bool)
                    return NumStr_R + "整";

                //NumStr_R += "零";
            }

            for (int b = 0; b < NumSr_X.Length; b++)
            {//小数部分转换
                NumStr_DQ = NumSr_X.Substring(b, 1);
                if (int.Parse(NumStr_DQ) != 0)
                    NumStr_R += Ls_ShZ[int.Parse(NumStr_DQ)] + Ls_DW_X[b];
                else if (b != 1 && iZhSh_bool)
                    NumStr_R += Ls_ShZ[int.Parse(NumStr_DQ)];
            }
            if (negative) NumStr_R = "负" + NumStr_R;
            return NumStr_R;
        }

        /// <summary>
        /// 反转字符串
        /// </summary>
        /// <param name="Rstr">需要反转的字符串</param>
        /// <returns>反转后的字符串</returns>
        private static String Reversion_Str(String Rstr)
        {
            Char[] LS_Str = Rstr.ToCharArray();
            Array.Reverse(LS_Str);
            String ReturnSte = "";
            ReturnSte = new String(LS_Str);//反转字符串

            return ReturnSte;
        }
        #endregion

    }
}
