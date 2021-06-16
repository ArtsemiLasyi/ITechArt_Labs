using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Text;
using DataAccess.Contexts;
using BusinessLogic.Services;
using DataAccess.Repositories;
using WebAPI.Services;
using WebAPI.Options;
using FluentValidation.AspNetCore;
using BusinessLogic.Validators;
using DataAccess.Storages;
using Mapster;
using WebAPI.Requests;
using BusinessLogic.Models;
using System;
using WebAPI.Validators;
using DataAccess.Entities;
using DataAccess.Options;

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
            services.AddScoped<FilmService>();
            services.AddScoped<PosterFileService>();

            services.AddScoped<UserRepository>();
            services.AddScoped<PasswordRepository>();
            services.AddScoped<FilmRepository>();
            services.AddScoped<PosterFileStorage>();

            services.AddTransient<SignInValidator>();
            services.AddTransient<SignUpValidator>();
            services.AddTransient<UserValidator>();
            services.AddTransient<PasswordValidator>();
            services.AddTransient<FilmValidator>();

            services.AddSingleton<JwtService>();

            services.Configure<StorageOptions>(Configuration.GetSection("Storage"));

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
                        JwtOptions jwtOptions = Configuration.GetSection(JwtOptions.JwToken).Get<JwtOptions>();

                        options.RequireHttpsMetadata = false;
                        string key = jwtOptions.Key;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = jwtOptions.Issuer,

                            ValidateAudience = true,
                            ValidAudience = jwtOptions.Audience,

                            ValidateLifetime = true,

                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                            ValidateIssuerSigningKey = true,
                        };
                    }
                );
            services.AddCors();
            services
                .AddControllers()
                .AddFluentValidation(
                    fv =>
                    {
                        fv.RegisterValidatorsFromAssemblyContaining<FilmRequestValidator>();
                        fv.RegisterValidatorsFromAssemblyContaining<SignInRequestValidator>();
                        fv.RegisterValidatorsFromAssemblyContaining<SignUpRequestValidator>();
                        fv.RegisterValidatorsFromAssemblyContaining<UserEditRequestValidator>();
                    }
                );
            services.AddDbContext<CinemabooContext>(
                options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("CinemabooContext"));
                }
            );

            TypeAdapterConfig<FilmRequest, FilmModel>
                .NewConfig()
                .Map(
                    dest => dest.Duration,
                    src => TimeSpan.FromMinutes(src.DurationInMinutes)
                );

            TypeAdapterConfig<FilmModel, FilmEntity>
                .NewConfig()
                .Map(
                    dest => dest.DurationInTicks,
                    src => src.Duration.Ticks
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
