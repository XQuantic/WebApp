using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhoneStore.Models;
using PhoneStore.Services;

namespace PhoneStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddServerSideBlazor();
            services.AddAntiforgery(options =>
            {
                options.Cookie = new CookieBuilder
                {
                    Name = "AntiForgery",
                    HttpOnly = true,
                    SecurePolicy = CookieSecurePolicy.Always,
                };
                options.FormFieldName = "AntiForgery";
            });
            services.AddAuthentication("BasicScheme")
                .AddCookie("BasicScheme",options =>
                {
                    options.Cookie.Name = "LoginCookie";
                    options.LoginPath = "/Account/Login";
                    options.AccessDeniedPath = "/Account/AccessDenied";
                });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AppleCompany", policy =>
                {
                    policy.RequireClaim("Company", "Apple");
                });
            });
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<MyDbContext>(options => options.UseSqlServer(connection));
            services.AddScoped<IRepository, Repository>();
            services.AddTransient<ICalculateService, CalculateService>();
            services.AddTransient<IPhoneService, PhoneService>();
            services.AddSingleton<HttpClient>();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStatusCodePagesWithReExecute("/Home/Errors/{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();    
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapControllerRoute(
                    name: "errors",
                    pattern: "Home/Errors/{errorCode}",
                    defaults: new { controller = "Home", action = "Errors" });

                endpoints.MapControllerRoute(
                    name: "phoneStore",
                    pattern: "PhoneStore",
                    defaults: new { controller = "Home", action = "Index" });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}