using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalBlogweb.api
{
    public class AddAuthTokenHeaderParameter : IOperationFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            Type allowAnonymousAttribute = typeof(AllowAnonymousAttribute);
            foreach (var item in context.ApiDescription.ActionDescriptor.EndpointMetadata)
            {
                if (item.GetType() == allowAnonymousAttribute)
                {
                    //接口允许匿名访问
                    return;
                }
            }

            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }
            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = CustomAuthorizationFilter.HEADER_TOKEN,
                In = ParameterLocation.Header,
                Required = true
            });
        }
    }
}
