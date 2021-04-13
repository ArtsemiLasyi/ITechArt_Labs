using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebAPI
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
            services.AddControllers();
            services.AddLogging(
                builder =>
                {
                    builder.AddFile(Configuration.GetSection("Logging"));
                }
            );
            services.AddSpaStaticFiles(
                configuration =>
                {
                    configuration.RootPath = Configuration.GetSection("Frontend:RootPath").Value;
                }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseAuthorization();

            app
                .UseEndpoints(
                    endpoints =>
                    {
                        endpoints.MapControllers();
                    }
                );

            app
                .UseSpa(
                    spa =>
                    {

                        spa.Options.SourcePath = Configuration.GetSection("Frontend:SourcePath").Value;

                        if (env.IsDevelopment())
                        {
                            spa.UseAngularCliServer(npmScript: "start");
                        }
                    }
                );
        }
    }
}
