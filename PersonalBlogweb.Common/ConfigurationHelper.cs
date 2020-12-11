using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace PersonalBlogweb.Common
{
    public class ConfigurationHelper
    {
        public static IConfiguration Configuration { get; set; }

        /// <summary>
        /// 获取数据库连接串配置
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static ConcurrentDictionary<String, ConnectionSection> QueryConnectionSection()
        {
            //配置文件-数据库连接串
            ConcurrentDictionary<String, ConnectionSection> DicConnectionSection = new ConcurrentDictionary<string, ConnectionSection>();
            foreach (var item in Configuration.GetSection("ConnStrings").GetChildren())
            {
                ConnectionSection connection = item.Get<ConnectionSection>();
                if (!DicConnectionSection.ContainsKey(item.Key))
                {
                    DicConnectionSection.TryAdd(item.Key, connection);
                }
            }

            return DicConnectionSection;
        }

    }
}
