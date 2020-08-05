using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Model;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSession();
            //services.AddMvc(o =>
            //{
            //    o.Filters.Add(typeof(MyExceptionFilterAttribute));// 全局异常Filter  
            //}).AddRazorRuntimeCompilation();
            //添加身份认证方案
            var jwtConfig = Configuration.GetSection("Jwt").Get<JwtConfig>();
            services.AddAuthentication
                (authoption =>
                {
                    //指定默认选项
                    authoption.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    authoption.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    authoption.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    authoption.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
           .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, option =>
           {
               option.Cookie.Name = "adCookie";//设置存储用户登录信息（用户Token信息）的Cookie名称
               option.Cookie.HttpOnly = true;//设置存储用户登录信息（用户Token信息）的Cookie，无法通过客户端浏览器脚本(如JavaScript等)访问到
               option.ExpireTimeSpan = TimeSpan.FromDays(3);// 过期时间
               option.SlidingExpiration = true;// 是否在过期时间过半的时候，自动延期
               option.LoginPath = "/Account/Login";
               option.LogoutPath = "/Account/LoginOut";
           }); 
           //.AddJwtBearer(option =>
           //{
           //    option.TokenValidationParameters = new TokenValidationParameters
           //    {
           //        ValidIssuer = jwtConfig.Issuer,
           //        ValidAudience = jwtConfig.Audience,
           //        ValidateIssuer = true,
           //        ValidateLifetime = jwtConfig.ValidateLifetime,
           //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SigningKey)),
           //        //缓冲过期时间，总的有效时间等于这个时间加上jwt的过期时间
           //        ClockSkew = TimeSpan.FromSeconds(0)
           //    };
           //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
