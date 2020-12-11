using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PersonalBlogweb.Common;

namespace PersonalBlogweb.api
{
    public class Startup
    {
        private ILogger _logger;

        public IConfiguration Configuration { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="loggerFactory"></param>
        public Startup(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            LogHelper.LoggerFactory = loggerFactory;
            ConfigurationHelper.Configuration = configuration;

            _logger = LogHelper.CreateLogger<Startup>();

            DbProviderFactories.RegisterFactory("MySql.Data.MySqlClient", "MySql.Data.MySqlClient.MySqlClientFactory,MySql.Data");
            DbProviderFactories.RegisterFactory("System.Data.SqlClient", "System.Data.SqlClient.SqlClientFactory,System.Data");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // If using Kestrel:
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
                options.Limits.MaxRequestBodySize = 1024 * 1024 * 2000;
                options.Limits.MaxRequestBufferSize = 1024 * 1024 * 2000;
            });
            // If using IIS:
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
                options.MaxRequestBodySize = 1024 * 1024 * 2000;
            });
            // 注册Swagger服务
            services.AddSwaggerGen(c =>
            {
                // 添加文档信息
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CoreWebApi", Version = "v1" });

                //swagger中控制请求的时候发是否需要在url中增加accesstoken
                c.OperationFilter<AddAuthTokenHeaderParameter>();

                // 为 Swagger JSON and UI设置xml文档注释路径
                //var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录（不受工作目录影响，建议采用此方法获取路径）
                //var xmlPath = Path.Combine(basePath, "PersonalBlogweb.api.xml");
                //c.IncludeXmlComments(xmlPath);
            });

            //统一异常处理
            services.AddControllers(options => { options.Filters.Add<CustomExceptionFilter>(); });

            //配置文件
            services.AddOptions();
            services.Configure<OssConfigVO>(Configuration.GetSection("OSS"));
            services.Configure<ReportGenerateConfigVO>(Configuration.GetSection("ReportGenerate"));

            //通过反射把所有服务接口进行了注入
            var serviceAsm = Assembly.Load(new AssemblyName("PersonalBlogweb.Domain"));
            IList<Type> lst = serviceAsm.GetTypes().Where(t => t.Name.EndsWith("ServiceImpl") && !t.GetTypeInfo().IsAbstract).ToList();
            foreach (Type serviceType in lst)
            {
                var interfaceTypes = serviceType.GetInterfaces();
                if (interfaceTypes == null)
                {
                    continue;
                }
                foreach (var interfaceType in interfaceTypes)
                {
                    services.AddSingleton(interfaceType, serviceType);
                }
            }

            // Add Quartz services
            //services.AddSingleton<IJobFactory, SingletonJobFactory>();
            //services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            // Add our job
            //services.AddSingleton<HeartBeatJob>();
            //services.AddJob();
            //services.AddHostedService<QuartzHostedService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _logger.LogInformation("运行环境配置：" + env.EnvironmentName);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // 启用Swagger中间件
            app.UseSwagger();

            // 配置SwaggerUI
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CoreWebApi");

            });

            app.UseRouting();
            app.Use(next => context =>
            {
                context.Request.EnableBuffering();
                return next(context);
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
