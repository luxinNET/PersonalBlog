using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PersonalBlogweb.Entity
{
    /// <summary>
    /// 逻辑删除标记，1-正常，2-删除
    /// </summary>
    public enum DataStateEnum
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Description("退出")]
        NORMAL = 1,

        /// <summary>
        /// 已删除
        /// </summary>
        [Description("退出")]
        DELETED = 2,
    }
}
