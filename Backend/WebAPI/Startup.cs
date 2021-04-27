using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;
using WebAPI.Contexts;
using System.Text;

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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        string key = Configuration.GetSection("JWTToken:Key").Value;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = Configuration.GetSection("JWTToken:Issuer").Value,

                            ValidateAudience = true,
                            ValidAudience = Configuration.GetSection("JWTToken:Audience").Value,

                            ValidateLifetime = true,

                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                            ValidateIssuerSigningKey = true,
                        };
                    });
            services.AddControllersWithViews();
            services.AddCors();

            services.AddDbContext<UsersContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("UsersContext")));
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

            string staticContentPath = Path.GetFullPath(Configuration.GetSection("Frontend:RootPath").Value);
            IFileProvider fileProvider = new PhysicalFileProvider(staticContentPath);

            DefaultFilesOptions options = new()
            {
                FileProvider = fileProvider
            };
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("index.html");

            app.UseDefaultFiles(options);
            app.UseStaticFiles(
                new StaticFileOptions
                {
                    FileProvider = fileProvider
                }
            );

            string[] originsArray = Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();          

            app.UseCors(
                builder =>
                {
                    builder.WithOrigins(originsArray);
                }
            );

            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();

            app
                .UseEndpoints(
                    endpoints =>
                    {
                        endpoints.MapControllers();
                    }
                );
        }
    }
}
