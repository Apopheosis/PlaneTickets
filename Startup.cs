using System;
using System.IO;
using System.Net.Mime;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Middleware;
using MoviesApp.Middleware;
using Tickets.JsonConvertService;
using Tickets.Services;


namespace Tickets
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
            services.AddDbContext<ItemContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("ItemContext")));
            services.AddControllers()            
                .AddJsonOptions(
                    options => { 
                        options.JsonSerializerOptions.PropertyNamingPolicy = 
                            SnakeCaseNamingPolicy.Instance;
                    });
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IService, Service>();
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });
            
        }

        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseRequestLog();
                app.UseExceptionHandler(exceptionHandlerApp =>
                {
                    exceptionHandlerApp.Run(async context =>
                    {
                        context.Response.ContentType = MediaTypeNames.Text.Plain;

                        await context.Response.WriteAsync("An exception was thrown.");

                        var exceptionHandlerPathFeature =
                            context.Features.Get<IExceptionHandlerPathFeature>();

                        if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
                        {
                            await context.Response.WriteAsync(" The file was not found.");
                        }

                        if (exceptionHandlerPathFeature?.Path == "/")
                        {
                            await context.Response.WriteAsync(" Page: Home.");
                        }
                    });
                });
                app.UseHsts();
            }

            app.UseHttpLogging();
            app.UseHttpsRedirection();
            app.UseStatusCodePages();
            app.UseRouting();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                
                endpoints.MapControllers();
            });
        }
    }
}