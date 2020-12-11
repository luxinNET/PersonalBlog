using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalBlogweb.Entity
{
    [Table("SystemEnum")]
    public class SystemEnum
    {
        [Key]
        public Int32 Id { get; set; }
        public String EnumText { get; set; }
        public String EnumValue { get; set; }
        public Byte EnumType { get; set; }
        public String EnumNo { get; set; }
        public String EnumParentNo { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public Byte DataState { get; set; }
    }
}