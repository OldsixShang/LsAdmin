using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ls.Utilities
{
    /// <summary>
    /// Id 生成器。
    /// </summary>
    public class LsIdGenerator
    {
        private static object obj = new object();

        private static Int32 lastSequenceNo;
        private static int totalSeconds;
        private static int lastZeroSeconds;

        static LsIdGenerator()
        {
            lastZeroSeconds = Convert.ToInt32((DateTime.Now - DateTime.Today).TotalSeconds);
        }

        /// <summary>
        /// 根据应用程序编号创建全局唯一 Id。
        /// </summary>
        /// <param name="applicationId">应用程序编号，框架的每个部署都需要对应一个唯一编号</param>
        /// <returns>返回<see cref="Int64"/>类型的 Id。</returns>
        public static Int64 CreateIdentity(Int32 applicationId = LsConst.ApplicationId)
        {
            lock (obj)
            {
                StringBuilder sb = new StringBuilder();

                totalSeconds = Convert.ToInt32((DateTime.Now - DateTime.Today).TotalSeconds);

                if (lastSequenceNo < 9999)
                {
                    lastSequenceNo++;
                }
                else
                {
                    while (totalSeconds <= lastZeroSeconds)
                    {
                        Thread.Sleep(20);
                        totalSeconds = Convert.ToInt32((DateTime.Now - DateTime.Today).TotalSeconds);
                    }
                    lastZeroSeconds = totalSeconds;
                    lastSequenceNo = 1;
                }

                sb.Append(applicationId);
                sb.Append(Convert.ToInt16((DateTime.Now - new DateTime(2016, 1, 1)).TotalDays).ToString().PadLeft(4, '0'));
                sb.Append(totalSeconds.ToString().PadLeft(5, '0'));
                sb.Append(lastSequenceNo.ToString().PadLeft(4, '0'));

                return Convert.ToInt64(sb.ToString());
            }
        }

    }
}
