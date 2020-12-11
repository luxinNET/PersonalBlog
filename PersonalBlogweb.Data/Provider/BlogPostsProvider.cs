using Dapper;
using PersonalBlogweb.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Extensions.Logging;
using System.Linq;
using Dapper.Contrib.Extensions;
using PersonalBlogweb.Common;

namespace PersonalBlogweb.Data
{
    public class BlogPostsProvider:BaseDAL
    {
        private readonly ILogger _logger;
        public BlogPostsProvider(ILogger logger = null)
        {
            if (logger == null)
            {
                _logger = LogHelper.CreateLogger<BlogPostsProvider>();
            }
            else
            {
                _logger = logger;
            }
        }

        /// <summary>
        /// 根据时间分页查询-首页显示
        /// </summary>
        /// <param name="pageVO"></param>
        /// <returns></returns>
        public List<BlogPosts> QueryPageblog(BlogPostsPageVO pageVO)
        {
            IPageInfo page = pageVO;
            string sqlCount = "SELECT COUNT(BlogPostsNo) FROM BlogPosts WITH(NOLOCK) WHERE DataState = @DataState ";
            string sqlRecord = "SELECT BlogPostsNo FROM BlogPosts WITH(NOLOCK) WHERE DataState = @DataState ";
            var para = new DynamicParameters();
            para.Add("DataState", (byte)DataStateEnum.NORMAL);
            IEnumerable<BlogPosts> blogs = null;
            using (IDbConnection dbConnection=CreateConnection(ConnectionNameConstant.PersonalBlog_READ))
            {
                blogs = PageProvider.PageQuery<BlogPosts>(ref page, sqlCount, sqlRecord, dbConnection);
                if (blogs == null)
                {
                    return null;
                }
                else
                {
                    return blogs.ToList();
                }
            }
        }
    }
}
