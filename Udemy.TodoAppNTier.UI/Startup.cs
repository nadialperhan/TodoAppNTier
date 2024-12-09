using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Udemy.TodoAppNTier.UI.GlobalExceptionLogs;
using Udemy.TodoAppNTierBusiness.DependencyResolvers.Microsoft;

namespace Udemy.TodoAppNTier.UI
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
            
            services.AddDependencies(Configuration);
            services.AddRazorPages();
            services.AddControllersWithViews();
            services.AddMemoryCache();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {

                //app.UseDeveloperExceptionPage();
                
            }
            //else
            //{
            //    app.UseExceptionHandler("/Home/NotFound");
            //    app.UseHsts();
            //}
            app.UseExceptionHandler("/Error");

            app.UseStatusCodePagesWithReExecute("/Home/NotFound","?code={0}");
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
            Path.Combine(env.ContentRootPath, "node_modules")),
                RequestPath = "/node_modules"
            });
            //app.UseSerilogRequestLogging(options =>
            //{
            //    options.EnrichDiagnosticContext = null; // Fazla bilgi kaydını engelle
                
            //});
            app.UseRouting();
            app.UseMiddleware<GlobalExceptionsLogsMiddleware>();
            //app.UseMiddleware<RequestLogging>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Create}/{id?}");
            });
        }
    }
}
