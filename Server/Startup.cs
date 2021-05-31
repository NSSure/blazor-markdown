using Blazor.Markdown.Core;
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
using System.Collections.Generic;
using System.Linq;
using static Blazor.Markdown.Server.Program;

namespace Blazor.Markdown.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            MarkdownApp.DBConfiguration = Configuration.GetSection("DBConfiguration") as DBConfiguration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public async void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            services.AddMediatR(typeof(MarkdownApp));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizeActionBehavior<,>));

            services.AddSingleton(typeof(MongoDBContext));
            services.AddTransient(typeof(SettingsRepository));

            services.Configure<ConfigureMongoDBSeedingOptions>(options =>
            {

            });

            List<Type> _registerMapTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(x => typeof(IRegisterMap).IsAssignableFrom(x) && !x.ContainsGenericParameters && !x.IsInterface).ToList();

            foreach (Type registerMapType in _registerMapTypes)
            {
                var _registerMapInstance = (IRegisterMap)Activator.CreateInstance(registerMapType);

                if (_registerMapInstance != null)
                {
                    _registerMapInstance.Execute();
                }
            }

            List<Type> _registerIndexesTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(x => typeof(IRegisterIndexes).IsAssignableFrom(x) && !x.ContainsGenericParameters && !x.IsInterface).ToList();

            foreach (Type registerIndexesType in _registerIndexesTypes)
            {
                var _registerIndexesInstance = (IRegisterIndexes)Activator.CreateInstance(registerIndexesType);

                if (_registerIndexesInstance != null)
                {
                    await _registerIndexesInstance.Execute();
                }
            }
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
