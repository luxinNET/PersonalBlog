using System;
using System.Collections.Generic;
using System.Text;
using Dapper.Contrib.Extensions;

namespace PersonalBlogweb.Entity
{
    [Table("BlogPosts")]
    public class BlogPosts
    {
        [Key]
        public Int32 Id { get; set; }
        public String BlogNo { get; set; }
        public String BlogTitle { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public Byte DataState { get; set; }
        public String BlogBody { get; set; }
        public String BlogBanner { get; set; }
        public String BlogTypeENo { get; set; }
        public Int64 BlogPageView { get; set; }
    }
}
