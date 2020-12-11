using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalBlogweb.api
{
    /// <summary>
    /// 分页搜索结果返回格式
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiPageResponse<T> : ApiResponse<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public ApiPageResponse()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        public ApiPageResponse(ApiResponse<T> response)
        {
            this.Code = response.Code;
            this.Content = response.Content;
            this.Msg = response.Msg;
        }

        /// <summary>
        /// 查询页码，从1开始
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页记录数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 符合条件的总记录数
        /// </summary>
        public int TotalCount { get; set; }
    }
}
