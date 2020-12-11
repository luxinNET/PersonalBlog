using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalBlogweb.Common
{
    /// <summary>
    /// 配置文件-数据库连接串格式
    /// </summary>
    public class ConnectionSection
    {
        public string ProviderName { get; set; }
        public string ConnectionString { get; set; }
    }
}
