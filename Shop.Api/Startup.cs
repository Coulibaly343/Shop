using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shop.Infrastructure.AutoMapper;
using Shop.Infrastructure.Data;
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
            services.AddMvc ();

            #region DbContextAndSettings

            services.AddCors ();
            services.AddDbContext<ShopContext> (options =>
                options.UseSqlServer (Configuration.GetConnectionString ("ShopDatabase"), b =>
                    b.MigrationsAssembly ("Shop.Api")));
            services.AddSingleton (AutoMapperConfig.Initialize ());
            services.AddAuthorization (options => options.AddPolicy ("admin", policy => policy.RequireRole ("admin")));
            services.AddAuthorization (options => options.AddPolicy ("user", policy => policy.RequireRole ("user")));

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
            }

            app.UseCors (x => x.AllowAnyHeader ().AllowAnyMethod ().AllowAnyOrigin ().AllowCredentials ());
            app.UseAuthentication ();
            app.UseMvc ();
        }
    }
}