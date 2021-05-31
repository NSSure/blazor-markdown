using Blazor.Markdown.Core;
using Blazor.Markdown.Core.DAL.Entity;
using Blazor.Markdown.Core.DAL.Mongo;
using Blazor.Markdown.Core.DAL.Providers.Mongo;
using Blazor.Markdown.Core.DAL.Repository;
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

            services.AddMongoDB(typeof(MongoDBContext), (options) =>
            {
                // Will ensure the database is created by excuting the mapping and index registrations.
                options.EnsureCreated = true;
            });

            services.AddTransient(typeof(SettingsRepository));

            MongoDBContext _context = new MongoDBContext();

            _context.Role.InsertOne(new Role()
            {
                Id = Guid.Parse("AE2AB2DC-7CB9-4065-AB31-12613CE08F96"),
                Name = "Administrator",
                Key = "System.Admin"
            });

            _context.Role.InsertOne(new Role()
            {
                Id = Guid.Parse("536DDEB8-C050-45D9-94B7-2A365D88EB52"),
                Name = "User",
                Key = "System.User"
            });
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
