using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PersonalBlogweb.Entity
{
    /// <summary>
    /// 状态，1-正常，2-屏蔽
    /// </summary>
    public enum EntityStatusEnum : byte
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        NORMAL = 1,

        /// <summary>
        /// 屏蔽
        /// </summary>
        [Description("屏蔽")]
        DISABLED = 2,

        ///// <summary>
        ///// 删除（可恢复）
        ///// </summary>
        //[Description("删除")]
        //DELETED = 3,
    }
}
