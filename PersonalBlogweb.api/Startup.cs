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
            // ע��Swagger����
            services.AddSwaggerGen(c =>
            {
                // ����ĵ���Ϣ
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CoreWebApi", Version = "v1" });

                //swagger�п��������ʱ���Ƿ���Ҫ��url������accesstoken
                c.OperationFilter<AddAuthTokenHeaderParameter>();

                // Ϊ Swagger JSON and UI����xml�ĵ�ע��·��
                //var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//��ȡӦ�ó�������Ŀ¼�����ܹ���Ŀ¼Ӱ�죬������ô˷�����ȡ·����
                //var xmlPath = Path.Combine(basePath, "PersonalBlogweb.api.xml");
                //c.IncludeXmlComments(xmlPath);
            });

            //ͳһ�쳣����
            services.AddControllers(options => { options.Filters.Add<CustomExceptionFilter>(); });

            //�����ļ�
            services.AddOptions();
            services.Configure<OssConfigVO>(Configuration.GetSection("OSS"));
            services.Configure<ReportGenerateConfigVO>(Configuration.GetSection("ReportGenerate"));

            //ͨ����������з���ӿڽ�����ע��
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
            _logger.LogInformation("���л������ã�" + env.EnvironmentName);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // ����Swagger�м��
            app.UseSwagger();

            // ����SwaggerUI
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
