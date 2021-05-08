using JWTMicroNetCore.Data;
using JWTMicroNetCore.Models;
using JWTMicroNetCore.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using Microsoft.AspNetCore.Authentication;

namespace JWTMicroNetCore
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
             .SetBasePath(env.ContentRootPath)
             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
             .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
             .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "JWTMicroNetCore", Version = "v1" });
            });
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("QlteqConnection"));
            });
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            var jwtAppSettingOptions = Configuration.GetSection("JwtIssuerOptions");
            services
                     .AddAuthentication(options =>
                     {
                         options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                         options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                         options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                     })
                     .AddJwtBearer(cfg =>
                     {
                         cfg.RequireHttpsMetadata = false;
                         cfg.SaveToken = true;
                         cfg.TokenValidationParameters = new TokenValidationParameters
                         {
                             ValidIssuer = jwtAppSettingOptions["JwtIssuer"],
                             ValidAudience = jwtAppSettingOptions["JwtIssuer"],
                             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtAppSettingOptions["JwtKey"])),
                             ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                     });

            services.AddDistributedRedisCache(options =>
            {
                options.InstanceName = "Session";
                options.Configuration = "localhost";             
            });

            services.AddSession(options =>
            {
                // 24 saat Redis Timeout Süresi.
                options.IdleTimeout = TimeSpan.FromHours(24);
            });

            services.AddTransient<IEmailSender, AuthMessageSender>()
               .AddTransient<ISmsSender, AuthMessageSender>();

            services.AddScoped<ITokenBuilder, TokenBuilder>();
            services.AddScoped<SignInManager<ApplicationUser>, ApplicationSignInManager>();
            //services.AddScoped<IClaimsTransformation, ClaimsTransformer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JWTMicroNetCore v1"));
            }

            app.UseSession();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
