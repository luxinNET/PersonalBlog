using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalBlogweb.Domain
{
    public interface IRoleAuthorizationService
    {
        /// <summary>
        /// 是否已授权
        /// </summary>
        /// <param name="loginType">登录类型</param>
        /// <param name="uri">API地址</param>
        /// <returns></returns>
        bool IsAuthorized(byte loginType, string uri);
    }
}
