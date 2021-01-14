using PersonalBlogweb.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalBlogweb.Domain
{
    public interface IBlogPostsService
    {
        /// <summary>
        /// 分页查询博客-首页显示
        /// </summary>
        /// <param name="pageVO"></param>
        /// <returns></returns>
        List<BlogPosts> QueryPageblog(BlogPostsPageVO pageVO);



    }
}
