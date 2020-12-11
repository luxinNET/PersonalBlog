using Dapper.Contrib.Extensions;
using PersonalBlogweb.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PersonalBlogweb.Data
{
    public class BaseDAL
    {
        private ConcurrentDictionary<String,ConnectionSection> dicConnectionSection;

        /// <summary>
        /// 系统配置-数据库连接串字典
        /// </summary>
        private ConcurrentDictionary<String, ConnectionSection> QueryConnectionSection()
        {
            if (dicConnectionSection == null || dicConnectionSection.Count == 0)
            {
                //配置文件-数据库连接串
                dicConnectionSection = ConfigurationHelper.QueryConnectionSection();
            }
            return dicConnectionSection;
        }
        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="connName">连接串名称</param>
        /// <returns></returns>
        public IDbConnection CreateConnection(String connName)
        {
            if (String.IsNullOrEmpty(connName))
            {
                throw new Exception("数据库连接串名称不能为空.");
            }
            ConcurrentDictionary<String, ConnectionSection> DicConnectionSection = QueryConnectionSection();
            if (DicConnectionSection == null || DicConnectionSection.Count == 0)
            {
                throw new Exception("数据库连接串配置不能为空.");
            }
            ConnectionSection connectionSection = null;
            DicConnectionSection.TryGetValue(connName, out connectionSection);
            if (connectionSection == null)
            {
                throw new Exception(String.Format("未能找到{0}数据库连接串.", connName));
            }

            var connection = ConnectionFactory.CreateConnection(connectionSection);
            connection.Open();
            return connection;
        }
        /// <summary>
        /// 查询数据库,返回指定ID的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">主键Id</param>
        /// <param name="dbConnection"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public T FindById<T>(object id, IDbConnection dbConnection, IDbTransaction transaction = null, Int32? commandTimeout = null) where T : class
        {
            return dbConnection.Get<T>(id, transaction, commandTimeout);
        }

        /// <summary>
        /// 插入指定对象到数据库中
        /// </summary>
        /// <param name="info">准备插入对象</param>
        /// <param name="dbConnection"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public long Insert<T>(T info, IDbConnection dbConnection, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            long result = dbConnection.Insert(info, transaction, commandTimeout);
            return result;
        }

        /// <summary>
        /// 插入指定对象集合到数据库中
        /// </summary>
        /// <param name="list">准备插入对象列表</param>
        /// <param name="dbConnection"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public long Insert<T>(IEnumerable<T> list, IDbConnection dbConnection, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            long result = dbConnection.Insert(list, transaction, commandTimeout);

            return result;
        }

        /// <summary>
        /// 根据主键更新对象属性到数据库中
        /// </summary>
        /// <param name="info">准备更新对象</param>
        /// <param name="dbConnection"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public bool Update<T>(T info, IDbConnection dbConnection, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            bool result = dbConnection.Update(info, transaction, commandTimeout);
            return result;
        }

        /// <summary>
        /// 根据主键更新指定对象集合到数据库中
        /// </summary>
        /// <param name="list">准备更新对象</param>
        /// <param name="dbConnection"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public bool Update<T>(IEnumerable<T> list, IDbConnection dbConnection, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            bool result = dbConnection.Update(list, transaction, commandTimeout);
            return result;
        }

        /// <summary>
        /// 根据主键从数据库中删除指定对象
        /// </summary>
        /// <param name="info">准备删除对象</param>
        /// <param name="dbConnection"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public bool Delete<T>(T info, IDbConnection dbConnection, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            bool result = dbConnection.Delete(info, transaction, commandTimeout);
            return result;
        }

        /// <summary>
        /// 根据主键从数据库中删除指定对象集合
        /// </summary>
        /// <param name="list">准备删除对象列表</param>
        /// <param name="dbConnection"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public bool Delete<T>(IEnumerable<T> list, IDbConnection dbConnection, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            bool result = dbConnection.Delete(list, transaction, commandTimeout);
            return result;
        }

    }
}
