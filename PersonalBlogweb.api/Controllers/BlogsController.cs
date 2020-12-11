using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PersonalBlogweb.Common;
using PersonalBlogweb.Domain;
using PersonalBlogweb.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalBlogweb.api
{
    [TypeFilter(typeof(CustomAuthorizationFilter))]
    [CustomActionFilter]
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : BaseController
    {
        private readonly ILogger _logger;
        private IBlogPostsService _blogPostsService;
        public BlogsController(IBlogPostsService blogPostsService)
        {
            _logger = LogHelper.CreateLogger<BlogsController>();
            _blogPostsService = blogPostsService;
        }


        /// <summary>
        /// 首页分页搜索-根据时间排序
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost, Route("Index")]
        public ApiPageResponse<List<BlogPosts>> Search([FromBody] BlogPostsPageVO dto)
        {
            try
            {
        
                ApiPageResponse<List<BlogPosts>> result = new ApiPageResponse<List<BlogPosts>>();

                dto.SortValue = "CreateTime DESC";
                var lst = _blogPostsService.QueryPageblog(dto);
                if (lst != null)
                {
                    result = new ApiPageResponse<List<BlogPosts>>(ApiResponse<List<BlogPosts>>.ResponseSucceed(lst));
                    result.PageIndex = dto.PageIndex;
                    result.PageSize = dto.PageSize;
                    result.TotalCount = dto.TotalCount;
                }
                else
                {
                    result = new ApiPageResponse<List<BlogPosts>>(ApiResponse<List<BlogPosts>>.ResponseFailed(StatusCodeEnum.ERROR));
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "分页搜索异常");
                return new ApiPageResponse<List<BlogPosts>>(ApiResponse<List<BlogPosts>>.ResponseFailed(StatusCodeEnum.ERROR, ex.Message));
            }
        }
    }
}
