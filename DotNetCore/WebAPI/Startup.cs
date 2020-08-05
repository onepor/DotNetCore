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
            //    o.Filters.Add(typeof(MyExceptionFilterAttribute));// ȫ���쳣Filter  
            //}).AddRazorRuntimeCompilation();
            //��������֤����
            var jwtConfig = Configuration.GetSection("Jwt").Get<JwtConfig>();
            services.AddAuthentication
                (authoption =>
                {
                    //ָ��Ĭ��ѡ��
                    authoption.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    authoption.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    authoption.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    authoption.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
           .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, option =>
           {
               option.Cookie.Name = "adCookie";//���ô洢�û���¼��Ϣ���û�Token��Ϣ����Cookie����
               option.Cookie.HttpOnly = true;//���ô洢�û���¼��Ϣ���û�Token��Ϣ����Cookie���޷�ͨ���ͻ���������ű�(��JavaScript��)���ʵ�
               option.ExpireTimeSpan = TimeSpan.FromDays(3);// ����ʱ��
               option.SlidingExpiration = true;// �Ƿ��ڹ���ʱ������ʱ���Զ�����
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
           //        //�������ʱ�䣬�ܵ���Чʱ��������ʱ�����jwt�Ĺ���ʱ��
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
