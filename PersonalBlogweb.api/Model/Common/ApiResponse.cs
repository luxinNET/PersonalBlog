using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalBlogweb.api
{

    /// <summary>
    /// 统一返回格式
    /// </summary>
    [Description("统一返回格式")]
    public class ApiResponse<T>
    {
        /// <summary>
        /// 消息
        /// </summary>
        [Description("消息")]
        public string Msg { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [Description("内容")]
        public T Content { get; set; }
        /// <summary>
        /// 结果代码
        /// </summary>
        [Description("结果代码")]
        public StatusCodeEnum Code { get; set; }
        /// <summary>
        /// 构建执行成功返回
        /// </summary>
        public static ApiResponse<T> ResponseSucceed(T content)
        {
            return new ApiResponse<T> { Code = StatusCodeEnum.OK, Content = content, Msg = "OK" };
        }
        /// <summary>
        /// 构建执行失败返回
        /// </summary>
        public static ApiResponse<T> ResponseFailed(StatusCodeEnum code, string msg = null)
        {
            return new ApiResponse<T> { Code = code, Content = default(T), Msg = msg ?? "ERROR" };
        }
    }
}
