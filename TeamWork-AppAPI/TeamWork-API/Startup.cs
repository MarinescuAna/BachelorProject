using TeamWork.DataAccess.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Linq;
using System.Text;
using System;
using Microsoft.AspNetCore.Http;
using System.IO;
using NLog;
using TeamWork.ApplicationLogger;
using TeamWork.ApplicationLogic.Service.Models.Interface;
using TeamWork.ApplicationLogic.Service.Models.Implementation;
using TeamWork.ApplicationLogic.Repository.UOW;
using Microsoft.EntityFrameworkCore;

namespace TeamWork_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();

            //For Entity Framework
            services.AddDbContext<TeamWorkDbContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("DefaultConnection")));

            //Dependency Injection
            services.AddSingleton<ILoggerService, LoggerService>();
            services.AddScoped<IUserService, UserServiceImpl>();
            services.AddScoped<IGroupService, GroupServiceImpl>();
            services.AddScoped<IImageService, ImageServiceImpl>();
            services.AddScoped<IChatService, ChatServiceImpl>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Session
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(120);
            });

            //For Jwt
            var tokenValidationParameter = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["SecretKey"])),
                RequireExpirationTime = true,
                ClockSkew=TimeSpan.Zero
            };
            services.AddSingleton(tokenValidationParameter);
            services.AddAuthentication(options =>
           {
               options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
           }).AddJwtBearer(options =>
           {
               options.RequireHttpsMetadata = false;
               options.SaveToken = true;
               options.TokenValidationParameters =tokenValidationParameter;
           });

            //For SWAGGER
            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Core API", Description = "Swagger Core API" });
                    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());                
                });

            //For Angular Consumer
            services.AddCors(o=>o.AddPolicy("CORSPolicy", builder => {
                builder.WithOrigins(Configuration.GetValue<string>("CORSOrigin"))
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
            }));

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //For Angular Consumer
            app.UseCors("CORSPolicy");

            //For Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Core API");
                });

            app.UseHttpsRedirection();

            app.UseRouting();

            //Session
            app.UseSession();

            //Add JWToken to all incoming HTTP Request Header
            app.Use(async (context, next) =>
            {
                var jwToken = context.Session.GetString("Token");
                if (!string.IsNullOrEmpty(jwToken))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + jwToken);
                }
                await next();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
