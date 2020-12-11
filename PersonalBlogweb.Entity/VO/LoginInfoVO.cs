using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalBlogweb.Entity
{
    /// <summary>
    /// 用户登录信息
    /// </summary>
    public class LoginInfoVO : ILoginUser
    {
        public string OperationSubjectNo { get; set; }
        public string UserNo { get; set; }
        public string UserAccount { get; set; }
        public string UserName { get; set; }
        public byte LoginType { get; set; }
        public string LoginCode { get; set; }

        public string DisplayName
        {
            get
            {
                return String.Format("{0}({1})", String.IsNullOrWhiteSpace(UserName) ? "未知" : UserName, UserNo);
            }
        }
    }
}
