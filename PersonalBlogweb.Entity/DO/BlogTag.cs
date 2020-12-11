using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalBlogweb.Entity
{
    [Table("BlogTag")]
    public class BlogTag
    {
        [Key]
        public Int32 Id { get; set; }
        public String BlogNo { get; set; }
        public String EnumNo { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public Byte DataState { get; set; }
    }
}
