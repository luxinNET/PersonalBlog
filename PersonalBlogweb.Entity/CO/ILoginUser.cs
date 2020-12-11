using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalBlogweb.Entity
{   
    /// <summary>
     /// 用户基本信息
     /// </summary>
    public interface ILoginUser
    {
        /// <summary>
        /// 运维主体
        /// </summary>
        public string OperationSubjectNo { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        string UserNo { get; }
        /// <summary>
        /// 用户登录账号
        /// </summary>
        string UserAccount { get; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        string UserName { get; }
        /// <summary>
        /// 登录类型，1-管理用户，2-测评用户
        /// </summary>
        byte LoginType { get; }
        /// <summary>
        /// 登录编号
        /// </summary>
        string LoginCode { get; }
    }
}
