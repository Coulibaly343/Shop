using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shop.Api.ActionFIlters;
using Shop.Infrastructure.AutoMapper;
using Shop.Infrastructure.Data;
using Shop.Infrastructure.Extensions.Exception;
using Shop.Infrastructure.Extensions.Jwt;
using Shop.Infrastructure.Repositories;
using Shop.Infrastructure.Repositories.Interfaces;
using Shop.Infrastructure.Services;
using Shop.Infrastructure.Services.Interfaces;

namespace Shop.Api {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddMvc (opt => {
                    opt.Filters.Add (typeof (ValidationActionFilter));
                }).AddFluentValidation ()
                .AddJsonOptions (options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            #region DbContextAndSettings

            services.AddCors ();
            services.AddDbContext<ShopContext> (options =>
                options.UseSqlServer (Configuration.GetConnectionString ("ShopDatabase"), b =>
                    b.MigrationsAssembly ("Shop.Api")));
            var key = Encoding.ASCII.GetBytes (Configuration.GetSection ("JWTSettings:Key").Value);
            services.AddAuthentication (JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer (options => {
                    options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey (key),
                    ValidateIssuer = false,
                    ValidateAudience = false

                    };
                });
            services.AddSingleton (AutoMapperConfig.Initialize ());
            services.AddAuthorization (options => options.AddPolicy ("admin", policy => policy.RequireRole ("admin")));
            services.AddAuthorization (options => options.AddPolicy ("user", policy => policy.RequireRole ("user")));
            services.AddSingleton<IJwtSettings> (Configuration.GetSection ("JwtSettings").Get<JwtSettings> ());

            #endregion
            #region Repositories

            services.AddScoped<IAccountRepository, AccountRepository> ();
            services.AddScoped<IAdminRepository, AdminRepository> ();
            services.AddScoped<IUserRepository, UserRepository> ();

            #endregion
            #region Services

            services.AddScoped<IAccountService, AccountService> ();
            services.AddScoped<IAdminService, AdminService> ();
            services.AddScoped<IUserService, UserService> ();
            services.AddScoped<IAuthService, AuthService> ();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                app.UseExceptionHandler (builder => {
                    builder.Run (async context => {
                        context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                        var error = context.Features.Get<IExceptionHandlerFeature> ();
                        if (error != null) {
                            context.Response.AddApplicationError (error.Error.Message);
                            await context.Response.WriteAsync (error.Error.Message);
                        }
                    });
                });
            }

            app.UseCors (x => x.AllowAnyHeader ().AllowAnyMethod ().AllowAnyOrigin ().AllowCredentials ());
            app.UseAuthentication ();
            app.UseMvc ();
        }
    }
}