using Microsoft.Extensions.Logging;
using PersonalBlogweb.Common;
using PersonalBlogweb.Data;
using PersonalBlogweb.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalBlogweb.Domain.Service
{
    public class BlogPostsServiceImpl : IBlogPostsService
    {
        private readonly ILogger _logger;

        private BlogPostsProvider _blogPostsProvider;
        

        public BlogPostsServiceImpl()
        {
            _logger = LogHelper.CreateLogger<BlogPostsServiceImpl>();

            _blogPostsProvider = new BlogPostsProvider();
        }

        /// <summary>
        /// 分页查询-首页显示
        /// </summary>
        /// <param name="pageVO"></param>
        /// <returns></returns>
        public List<BlogPosts> QueryPageblog(BlogPostsPageVO pageVO)
        {
           List<BlogPosts> blogPosts= _blogPostsProvider.QueryPageblog(pageVO);
            return blogPosts;
        }

    }
}
