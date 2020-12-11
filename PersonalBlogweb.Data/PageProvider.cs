using Dapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PersonalBlogweb.Common;
using PersonalBlogweb.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace PersonalBlogweb.Data
{
    internal class PageProvider : BaseDAL
    {
        private static readonly ILogger _logger;

        static PageProvider()
        {
            _logger = LogHelper.CreateLogger<PageProvider>();

        }

        public static IList<T> PageQuery<T>(ref IPageInfo page, String sqlTotalCount, String sqlPageRecord, IDbConnection conn, object param = null, bool buffered = true, Int32? commandTimeout = null, CommandType? commandType = null)
        {
            if (page == null) { throw new Exception("分页参数不能为空"); }
            if (String.IsNullOrWhiteSpace(page.SortValue)) { throw new Exception("分页查询时排序规则不能为空"); }
            try
            {
                //分页查询SQL拼装
                String strSqlPageRecord = string.Format(@"{0} ORDER BY {1} OFFSET {2} ROWS FETCH NEXT {3} ROWS ONLY;", JsonConvert.SerializeObject(page), JsonConvert.SerializeObject(sqlTotalCount), JsonConvert.SerializeObject(sqlPageRecord), JsonConvert.SerializeObject(param));
                _logger.LogTrace(String.Format("分页参数：{0}，总数查询SQL：{1}，分页查询SQL：{2}，参数：{3}"
                 , JsonConvert.SerializeObject(page)
                 , JsonConvert.SerializeObject(sqlTotalCount)
                 , JsonConvert.SerializeObject(sqlPageRecord)
                 , JsonConvert.SerializeObject(param)
                 ));
                //总记录数查询
                page.TotalCount = conn.ExecuteScalar<int>(sql: sqlTotalCount, param: param, commandTimeout: commandTimeout, commandType: commandType);

                //所查询页数已经没有记录
                if (page.TotalCount <= GetBeginIndex(page)) { return new List<T>(); }

                return conn.Query<T>(sql: strSqlPageRecord, param: param, buffered: buffered, commandTimeout: commandTimeout, commandType: commandType).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("分页：{0}，总数查询：{1}，分页查询：{2}，参数：{3}， 异常信息：{4}"
                    , JsonConvert.SerializeObject(page)
                    , JsonConvert.SerializeObject(sqlTotalCount)
                    , JsonConvert.SerializeObject(sqlPageRecord)
                    , JsonConvert.SerializeObject(param)
                    , ex.ToString()));
                throw ex;
            }
        }

        /// <summary>
        /// 开始记录数，传递给数据库，计数从0开始
        /// </summary>
        /// <returns></returns>
        private static int GetBeginIndex(IPageInfo page)
        {
            if (page.PageIndex < 1 || page.PageSize < 1)
            {
                throw new Exception("查询页码以及每页记录数不能为空");
            }
            return (page.PageIndex - 1) * page.PageSize;
        }
    }
}
