using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalBlogweb.Entity
{
    public class BlogPostsPageVO : IPageInfo
    {
        /// <summary>
        /// 查询页码，从1开始
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页记录数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 符合条件的总记录数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 排序规则
        /// </summary>
        public string SortValue { get; set; }

        public String BlogTypeENo { get; set; }

        public String EnumNo { get; set; }

    }
}
