using PersonalBlogweb.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalBlogweb.Domain
{

    public interface IAccessLogService
    {
        /// <summary>
        /// 新增日志
        /// </summary>
        /// <param name="accessLog"></param>
        /// <param name="opUser"></param>
        /// <returns></returns>
        bool Add(AccessLog accessLog, string opUser);
    }
}
