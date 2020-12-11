using PersonalBlogweb.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace PersonalBlogweb.Data
{

    /// <summary>
    /// 数据库连接辅助类
    /// </summary>
    public class ConnectionFactory
    {

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <returns></returns>
        public static IDbConnection CreateConnection(ConnectionSection connectionSection)
        {
            var factory = DbProviderFactories.GetFactory(connectionSection.ProviderName);
            if (factory == null)
            {
                throw new InvalidOperationException(String.Format("Can't find provider={0}.", connectionSection.ProviderName));
            }
            else
            {
                var dbConnection = factory.CreateConnection();
                dbConnection.ConnectionString = connectionSection.ConnectionString;
                return dbConnection;
            }
        }

        /// <summary>
        /// 创建一个打开的数据库连接
        /// </summary>
        /// <param name="connectionSection"></param>
        /// <returns></returns>
        public static IDbConnection CreateOpenConnection(ConnectionSection connectionSection)
        {
            var dbConnection = CreateConnection(connectionSection);
            dbConnection.Open();
            return dbConnection;
        }
    }
}
