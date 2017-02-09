using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ls.Utilities
{
    /// <summary>
    /// 加密解密工具类
    /// </summary>
    public static class CryptTool
    {
        /// <summary>
        /// 根据加密密钥加密加一段明文,采用TripleDES加密算法
        /// </summary>
        /// <param name="content">需要加密的明文内容</param>
        /// <param name="secret">加密密钥</param>
        /// <returns>返回加密后密文字符串</returns>
        /// <exception cref="ArgumentNullException"/>
        public static string Encrypt(string content, string secret)
        {
            if ((content == null) || (secret == null) || (content.Length == 0) || (secret.Length == 0))
                throw new ArgumentNullException("Invalid Argument");

            byte[] Key = GetKey(secret);
            byte[] ContentByte = Encoding.Unicode.GetBytes(content);
            MemoryStream MSTicket = new MemoryStream();

            MSTicket.Write(ContentByte, 0, ContentByte.Length);

            byte[] ContentCryptByte = Crypt(MSTicket.ToArray(), Key);

            string ContentCryptStr = Encoding.ASCII.GetString(Base64Encode(ContentCryptByte));

            return ContentCryptStr;
        }

        /// <summary>
        /// 解密方法
        /// </summary>
        /// <param name="content">需要解密的密文内容</param>
        /// <param name="secret">解密密钥</param>
        /// <returns>返回解密后明文字符串</returns>
        /// <exception cref="ArgumentNullException"/>
        public static string Decrypt(string content, string secret)
        {
            if ((content == null) || (secret == null) || (content.Length == 0) || (secret.Length == 0))
                throw new ArgumentNullException("Invalid Argument");

            byte[] Key = GetKey(secret);

            byte[] CryByte = Base64Decode(Encoding.ASCII.GetBytes(content));
            byte[] DecByte = Decrypt(CryByte, Key);

            byte[] RealDecByte;
            string RealDecStr;

            RealDecByte = DecByte;
            byte[] Prefix = new byte[CryptConstants.Operation.UnicodeReversePrefix.Length];
            Array.Copy(RealDecByte, Prefix, 2);

            if (CompareByteArrays(CryptConstants.Operation.UnicodeReversePrefix, Prefix))
            {
                byte SwitchTemp = 0;
                for (int i = 0; i < RealDecByte.Length - 1; i = i + 2)
                {
                    SwitchTemp = RealDecByte[i];
                    RealDecByte[i] = RealDecByte[i + 1];
                    RealDecByte[i + 1] = SwitchTemp;
                }
            }

            RealDecStr = Encoding.Unicode.GetString(RealDecByte);
            return RealDecStr;
        }


        /// <summary>
        /// 使用TripleDES加密
        /// </summary>
        /// <param name="source">需要加密的明文字节数组</param>
        /// <param name="key">加密密钥字节数组</param>
        /// <returns>返回加密后密文字节数组</returns>
        /// <exception cref="ArgumentException"/>
        private static byte[] Crypt(byte[] source, byte[] key)
        {
            if ((source.Length == 0) || (source == null) || (key == null) || (key.Length == 0))
            {
                throw new ArgumentException("Invalid Argument");
            }

            TripleDESCryptoServiceProvider dsp = new TripleDESCryptoServiceProvider();
            dsp.Mode = CipherMode.ECB;

            ICryptoTransform des = dsp.CreateEncryptor(key, null);

            return des.TransformFinalBlock(source, 0, source.Length);
        }

        /// <summary>
        /// 解密通过TripleDES算法加密后的密文字节数组
        /// </summary>
        /// <param name="source">需要解密的密文字节数组</param>
        /// <param name="key">解密密钥字节数组</param>
        /// <returns>返回解密后明文字节数组</returns>
        /// <exception cref="ArgumentNullException"/>
        private static byte[] Decrypt(byte[] source, byte[] key)
        {
            if ((source.Length == 0) || (source == null) || (key == null) || (key.Length == 0))
            {
                throw new ArgumentNullException("Invalid Argument");
            }

            TripleDESCryptoServiceProvider dsp = new TripleDESCryptoServiceProvider();
            dsp.Mode = CipherMode.ECB;

            ICryptoTransform des = dsp.CreateDecryptor(key, null);

            byte[] ret = new byte[source.Length + 8];

            int num;
            num = des.TransformBlock(source, 0, source.Length, ret, 0);

            ret = des.TransformFinalBlock(source, 0, source.Length);
            ret = des.TransformFinalBlock(source, 0, source.Length);
            num = ret.Length;

            byte[] RealByte = new byte[num];
            Array.Copy(ret, RealByte, num);
            ret = RealByte;
            return ret;
        }


        /// <summary>
        /// 原始base64编码
        /// </summary>
        /// <param name="source">待加密的明文字节数组</param>
        /// <returns>加密后密文的字节数组</returns>
        private static byte[] Base64Encode(byte[] source)
        {
            if ((source == null) || (source.Length == 0))
                throw new ArgumentException("source is not valid");

            ToBase64Transform tb64 = new ToBase64Transform();
            MemoryStream stm = new MemoryStream();
            int pos = 0;
            byte[] buff;

            while (pos + 3 < source.Length)
            {
                buff = tb64.TransformFinalBlock(source, pos, 3);
                stm.Write(buff, 0, buff.Length);
                pos += 3;
            }

            buff = tb64.TransformFinalBlock(source, pos, source.Length - pos);
            stm.Write(buff, 0, buff.Length);

            return stm.ToArray();

        }


        /// <summary>
        /// 原始base64解码
        /// </summary>
        /// <param name="source">待解密的密文字节数组</param>
        /// <returns>解密后明文的字节数组</returns>
        /// <exception cref="ArgumentException">当密文字节数组为空或数组长度为0</exception>
        private static byte[] Base64Decode(byte[] source)
        {
            if ((source == null) || (source.Length == 0))
                throw new ArgumentException("source is not valid");

            FromBase64Transform fb64 = new FromBase64Transform();
            MemoryStream stm = new MemoryStream();
            int pos = 0;
            byte[] buff;

            while (pos + 4 < source.Length)
            {
                buff = fb64.TransformFinalBlock(source, pos, 4);
                stm.Write(buff, 0, buff.Length);
                pos += 4;
            }

            buff = fb64.TransformFinalBlock(source, pos, source.Length - pos);
            stm.Write(buff, 0, buff.Length);
            return stm.ToArray();

        }

        /// <summary>
        /// 将密钥通过散列算法生成统一长度的字节数组，做为key
        /// </summary>
        /// <param name="secret">密钥</param>
        /// <returns>Key字节数组</returns>
        /// <exception cref="ArgumentException">当密文字节数组为空或数组长度为0</exception>
        private static byte[] GetKey(string secret)
        {
            if ((secret == null) || (secret.Length == 0))
                throw new ArgumentException("Secret is not valid");

            byte[] temp;

            ASCIIEncoding ae = new ASCIIEncoding();
            temp = Hash(ae.GetBytes(secret));

            byte[] ret = new byte[CryptConstants.Operation.KeySize];

            int i;

            if (temp.Length < CryptConstants.Operation.KeySize)
            {
                System.Array.Copy(temp, 0, ret, 0, temp.Length);
                for (i = temp.Length; i < CryptConstants.Operation.KeySize; i++)
                {
                    ret[i] = 0;
                }
            }
            else
                System.Array.Copy(temp, 0, ret, 0, CryptConstants.Operation.KeySize);

            return ret;
        }

        /// <summary>
        /// 比较两个byte数组是否相同
        /// </summary>
        /// <param name="source">比较参数一</param>
        /// <param name="dest">比较参数二</param>
        /// <returns>true表示为相同，false表示为不同</returns>
        /// <exception cref="ArgumentException"/>
        private static bool CompareByteArrays(byte[] source, byte[] dest)
        {
            if ((source == null) || (dest == null))
                throw new ArgumentException("source or dest is not valid");

            bool ret = true;

            if (source.Length != dest.Length)
                return false;
            else
                if (source.Length == 0)
                    return true;

            for (int i = 0; i < source.Length; i++)
                if (source[i] != dest[i])
                {
                    ret = false;
                    break;
                }
            return ret;
        }


        /// <summary>
        /// 使用md5计算散列
        /// </summary>
        /// <param name="source">需要散列的字节数组</param>
        /// <returns>散列后的字节数组</returns>
        /// <exception cref="ArgumentException">当字节数组为空或数组长度为0</exception>
        private static byte[] Hash(byte[] source)
        {
            if ((source == null) || (source.Length == 0))
                throw new ArgumentException("source is not valid");

            MD5 m = MD5.Create();
            return m.ComputeHash(source);
        }

        /// <summary>
        /// 对传入的明文密码进行Hash加密,密码不能为中文
        /// </summary>
        /// <param name="oriPassword">需要加密的明文密码</param>
        /// <returns>经过Hash加密的密码</returns>
        public static string HashPassword(string oriPassword)
        {
            if (string.IsNullOrEmpty(oriPassword))
                throw new ArgumentException("oriPassword is valid");

            ASCIIEncoding acii = new ASCIIEncoding();
            byte[] hashedBytes = Hash(acii.GetBytes(oriPassword));

            StringBuilder sb = new StringBuilder(30);
            foreach (byte b in hashedBytes)
            {
                sb.AppendFormat("{0:X2}", b);
            }
            return sb.ToString();

        }


        /// <summary>
        /// 将一个字符串进行base64编码後返回
        /// </summary>
        /// <param name="code_type">编码类型</param>
        /// <param name="code">编码内容</param>
        /// <returns>编码结果</returns>
        public static string EncodeBase64(string code_type, string code)
        {
            string encode = "";
            byte[] bytes = Encoding.GetEncoding(code_type).GetBytes(code);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = code;
            }
            return encode;
        }

        /// <summary>
        /// 对Url地址进行Base64编码
        /// </summary>
        /// <param name="code_type">编码类型</param>
        /// <param name="code">编码内容</param>
        /// <returns>编码结果</returns>
        public static string EncodeBase64UsedInURL(string code_type, string code)
        {
            string encode = "";
            encode = EncodeBase64(code_type, code).Replace("+", " ");
            return encode;
        }

        /// <summary>
        /// base64解码
        /// </summary>
        /// <param name="code_type">编码类型</param>
        /// <param name="code">解码内容</param>
        /// <returns>解码结果</returns>
        public static string DecodeBase64(string code_type, string code)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(code);
            try
            {
                decode = Encoding.GetEncoding(code_type).GetString(bytes);
            }
            catch
            {
                decode = code;
            }
            return decode;
        }

        /// <summary>
        /// 对Url地址进行Base64解码
        /// </summary>
        /// <param name="code_type"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string DecodeBase64UsedInURL(string code_type, string code)
        {
            string decode = "";
            decode = DecodeBase64(code_type, code.Replace(" ", "+"));
            return decode;
        }

        /// <summary>
        /// 包含有Key字节数组的统一的长度，及编码前置数组
        /// </summary>
        public struct CryptConstants
        {
            public const string PassKey = "123456";
            public struct Operation
            {
                public static readonly int KeySize = 24;
                public static readonly byte[] UnicodeOrderPrefix = new byte[2] { 0xFF, 0xFE };
                public static readonly byte[] UnicodeReversePrefix = new byte[2] { 0xFE, 0xFF };
            }
        }
    }
}
