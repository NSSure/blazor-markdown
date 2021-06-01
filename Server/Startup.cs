using Blazor.Markdown.Core;
using Blazor.Markdown.Core.DAL.Mongo;
using Blazor.Markdown.Core.Mediator.Behavior;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Blazor.Markdown.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            
            services.AddMediatR(typeof(MarkdownApp));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizeActionBehavior<,>));

            // Database will automatically be created if there are any indexes to apply.
            services.AddMongoDB<MongoDBContext>(typeof(MongoDBContext), (options) =>
            {
                // Will ensure the database is created by excuting the mapping and index registrations.
                options.EnsureCreated = true;
                options.DropDatabaseOnLoad = true;
            });

            // Register repositories.
            services.AddInjections();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
