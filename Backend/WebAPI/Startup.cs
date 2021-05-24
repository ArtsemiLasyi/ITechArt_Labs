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
using System.Text;
using DataAccess.Contexts;
using BusinessLogic.Services;
using DataAccess.Repositories;
using WebAPI.Services;

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
            services.AddScoped<SignInService>();
            services.AddScoped<SignUpService>();
            services.AddScoped<UserService>();
            services.AddScoped<PasswordService>();

            services.AddScoped<UserRepository>();
            services.AddScoped<PasswordRepository>();

            services.AddSingleton<JwtService>();

            services.AddLogging(
                builder =>
                {
                    builder.AddFile(Configuration.GetSection("Logging"));
                }
            );
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(
                        options =>
                        {
                            options.RequireHttpsMetadata = false;
                            string key = Configuration.GetSection("JwToken:Key").Value;
                            options.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuer = true,
                                ValidIssuer = Configuration.GetSection("JwToken:Issuer").Value,

                                ValidateAudience = true,
                                ValidAudience = Configuration.GetSection("JwToken:Audience").Value,

                                ValidateLifetime = true,

                                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                                ValidateIssuerSigningKey = true,
                            };
                        }
                    );
            services.AddCors();
            services.AddControllers();
            services.AddDbContext<CinemabooContext>(
                options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("CinemabooContext"));
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
