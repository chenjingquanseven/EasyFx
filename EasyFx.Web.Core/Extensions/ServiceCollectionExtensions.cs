using AutoMapper;
using EasyFx.Core.Abstracts;
using EasyFx.Core.Utils;
using EasyFx.Web.Core.Filters;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;

namespace EasyFx.Web.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// app 相关
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddEasyApp(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddAutoMapper(ReflectionHelper.GetApplicationAssemblies());
            services.AddMediatR();
            return services;
        }
        /// <summary>
        /// mvc相关
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IMvcBuilder AddEasyMvc(this IServiceCollection services, Action<MvcOptions> configure = null)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            
            //Mvc
            var builder = services.AddControllers(config =>
            {
                config.Filters.Add(new CustomExceptionFilter());
                config.Filters.Add(new GenerateReturnFilter());
                config.Filters.Add(new ModelStateValidFilter());

                configure?.Invoke(config);

            }).AddNewtonsoftJson(
                options =>
                {
                    //Json 
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    options.SerializerSettings.DateFormatString = DateTimeFormat.Normal;
                    options.SerializerSettings.FloatParseHandling = FloatParseHandling.Decimal;
                });

            //在nginx上获取用户端真实IP
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                options.ForwardLimit = null;
                options.KnownProxies.Clear();
                options.KnownNetworks.Clear();
            });

            return builder;
        }
        /// <summary>
        /// cors
        /// </summary>
        /// <param name="services"></param>
        /// <param name="policyName"></param>
        /// <returns></returns>
        public static IServiceCollection AddEasyCors(this IServiceCollection services, string policyName)
        {
            services.AddCors(c =>
            {
                c.AddPolicy(policyName, policy =>
                {
                    policy.SetIsOriginAllowed(o => true) //允许任何源
                        .AllowAnyMethod() //允许任何方式
                        .AllowAnyHeader() //允许任何头
                        .AllowCredentials(); //允许cookie
                });
            });


            return services;
        }
    }
}