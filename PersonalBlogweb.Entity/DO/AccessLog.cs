using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalBlogweb.Entity
{
    /// <summary>
    /// API访问日志
    /// </summary>
    [Table("AccessLog")]
    public partial class AccessLog
    {
        [Key]
        public long Id { get; set; }
        public string AccessPath { get; set; }
        public string UserNo { get; set; }
        public string LoginCode { get; set; }
        public byte AccessStatus { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUser { get; set; }
    }
}
