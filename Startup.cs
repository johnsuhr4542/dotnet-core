using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using application.Context;
using Microsoft.EntityFrameworkCore;
using NLog;
using application.Security;

namespace application {
    public class Startup {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            services
                .AddControllers();

            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(option => {
                    option.Cookie.HttpOnly = false;
                    option.Cookie.Name = "_n_session_";
                    option.LoginPath = "/User/Login";
                    option.LogoutPath = "/User/Logout";
                    option.AccessDeniedPath = "/User/AccessDenied";
                });
                
            services.AddAuthorization(option => {
                option.AddPolicy(
                    name: "MyPolicy",
                    policy => policy.RequireClaim("Admin")
                );
            });
            services.AddDbContext<ApplicationContext>(options => {
                var connectionString = Configuration.GetConnectionString("ApplicationContext");
                options.UseMySql(connectionString);
            });
            services.AddSingleton<IAuthorizationService, SecurityHandler>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions {
                OnPrepareResponse = ctx => 
                    ctx.Context.Response.Headers[HeaderNames.CacheControl] = "no-cache, must-revalidate, private, s-maxage=0"
            });
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllerRoute(
                name: "default", 
                pattern: "{controller=Home}/{action=Index}"
            ));
        }
    }
}
