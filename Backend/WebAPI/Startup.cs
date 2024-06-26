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
using WebAPI.Responses;
using WebAPI.Constants;
using System.Security.Claims;
using System.Globalization;
using FluentValidation;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("en");
            CultureInfo culture = new ("en-Us", false);
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
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
            services.AddScoped<PosterService>();
            services.AddScoped<CinemaService>();
            services.AddScoped<CinemaPhotoService>();
            services.AddScoped<HallService>();
            services.AddScoped<HallPhotoService>();
            services.AddScoped<CityService>();
            services.AddScoped<SeatTypeService>();
            services.AddScoped<SeatService>();
            services.AddScoped<ServiceService>();
            services.AddScoped<CinemaServiceService>();
            services.AddScoped<SessionService>();
            services.AddScoped<SeatTypePriceService>();
            services.AddScoped<SessionSeatService>();
            services.AddScoped<OrderService>();
            services.AddScoped<SeatOrderService>();
            services.AddScoped<CurrencyService>();

            services.AddScoped<UserRepository>();
            services.AddScoped<PasswordRepository>();
            services.AddScoped<FilmRepository>();
            services.AddScoped<PosterFileStorage>();
            services.AddScoped<PosterRepository>();
            services.AddScoped<CinemaRepository>();
            services.AddScoped<HallRepository>();
            services.AddScoped<CinemaPhotoRepository>();
            services.AddScoped<HallPhotoRepository>();
            services.AddScoped<CinemaPhotoFileStorage>();
            services.AddScoped<HallPhotoFileStorage>();
            services.AddScoped<CityRepository>();
            services.AddScoped<SeatTypeRepository>();
            services.AddScoped<SeatRepository>();
            services.AddScoped<ServiceRepository>();
            services.AddScoped<CinemaServiceRepository>();
            services.AddScoped<SessionRepository>();
            services.AddScoped<SeatTypePriceRepository>();
            services.AddScoped<SeatOrderRepository>();
            services.AddScoped<OrderRepository>();
            services.AddScoped<SessionSeatRepository>();
            services.AddScoped<CurrencyRepository>();

            services.AddTransient<SignInValidator>();
            services.AddTransient<SignUpValidator>();
            services.AddTransient<UserValidator>();
            services.AddTransient<PasswordValidator>();
            services.AddTransient<FilmValidator>();
            services.AddTransient<CinemaValidator>();
            services.AddTransient<HallValidator>();
            services.AddTransient<SeatValidator>();
            services.AddTransient<SeatTypeValidator>();
            services.AddTransient<CityValidator>();
            services.AddTransient<ServiceValidator>();
            services.AddTransient<CinemaServiceValidator>();
            services.AddTransient<PriceValidator>();
            services.AddTransient<SeatTypePriceValidator>();
            services.AddTransient<OrderValidator>();
            services.AddTransient<CurrencyValidator>();
            services.AddTransient<SessionValidator>();

            services.AddSingleton<JwtService>();

            services.Configure<StorageOptions>(Configuration.GetSection("Storage"));
            services.Configure<SeatOptions>(Configuration.GetSection("Seats"));
            services.Configure<BusinessLogic.Options.SeatOptions>(Configuration.GetSection("Seats"));

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

            services.AddAuthorization(
                opts =>
                {
                    opts.AddPolicy(
                        PolicyNames.Authorized, 
                        policy => 
                        {
                            policy.RequireClaim(ClaimTypes.NameIdentifier);
                        }
                    );
                    opts.AddPolicy(
                        PolicyNames.Administrator,
                        policy => 
                        {
                            policy.RequireClaim(
                                ClaimTypes.Role,
                                (UserRole.Administrator).ToString()
                            );
                        }
                    );
                }
            );


            string[] originsArray = Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();

            services.AddCors(
                options =>
                {
                    options.AddDefaultPolicy(
                        builder =>
                        {
                            builder
                                .WithOrigins(originsArray)
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                        }
                    );
                }
            );

            services
                .AddControllers()
                .AddFluentValidation(
                    fv =>
                    {
                        fv.RegisterValidatorsFromAssemblyContaining<FilmRequestValidator>();
                        fv.RegisterValidatorsFromAssemblyContaining<SignInRequestValidator>();
                        fv.RegisterValidatorsFromAssemblyContaining<SignUpRequestValidator>();
                        fv.RegisterValidatorsFromAssemblyContaining<UserEditRequestValidator>();
                        fv.RegisterValidatorsFromAssemblyContaining<CinemaRequestValidator>();
                        fv.RegisterValidatorsFromAssemblyContaining<HallRequestValidator>();
                        fv.RegisterValidatorsFromAssemblyContaining<SeatTypeRequestValidator>();
                        fv.RegisterValidatorsFromAssemblyContaining<ServiceRequestValidator>();
                        fv.RegisterValidatorsFromAssemblyContaining<CinemaRequestValidator>();
                        fv.RegisterValidatorsFromAssemblyContaining<HallRequestValidator>();
                        fv.RegisterValidatorsFromAssemblyContaining<CinemaServiceRequestValidator>();
                        fv.RegisterValidatorsFromAssemblyContaining<FilmRequestSearchParametersValidator>();
                        fv.RegisterValidatorsFromAssemblyContaining<FormFileValidator>();
                        fv.RegisterValidatorsFromAssemblyContaining<SeatTypePriceRequestValidator>();
                        fv.RegisterValidatorsFromAssemblyContaining<OrderRequestValidator>();
                        fv.RegisterValidatorsFromAssemblyContaining<CurrencyRequestValidator>();
                        fv.RegisterValidatorsFromAssemblyContaining<SessionRequestValidator>();
                    }
                );
            services.AddDbContext<CinemabooContext>(
                options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("CinemabooContext"));
                }
            );

            ConfigureAdapters();
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

            app.UseRouting();

            app.UseCors();

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

        private void ConfigureAdapters()
        {
            TypeAdapterConfig<SeatEntity, SeatModel>
                .NewConfig()
                .Map(
                    dest => dest.ColorRgb,
                    src => src.SeatType.ColorRgb
                );

            TypeAdapterConfig<CinemaServiceEntity, CinemaServiceModel>
                .NewConfig()
                .Map(
                    dest => dest.Name,
                    src => src.Service.Name
                )
                .Map(
                    dest => dest.Price,
                    src => new PriceModel(
                        src.Price,
                        src.Currency.Adapt<CurrencyModel>()
                    )
                );

            TypeAdapterConfig<CinemaServiceModel, CinemaServiceEntity>
                .NewConfig()
                .Map(
                    dest => dest.Price,
                    src => src.Price.Value
                )
                .Map(
                    dest => dest.CurrencyId,
                    src => src.Price.Currency.Id
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

            TypeAdapterConfig<FilmEntity, FilmModel>
                .NewConfig()
                .Map(
                    dest => dest.Duration,
                    src => new TimeSpan(src.DurationInTicks)
                );

            TypeAdapterConfig<FilmModel, FilmResponse>
                .NewConfig()
                .Map(
                    dest => dest.DurationInMinutes,
                    src => src.Duration.TotalMinutes
                );

            TypeAdapterConfig<CinemaEntity, CinemaModel>
                .NewConfig()
                .Map(
                    dest => dest.CityName,
                    src => src.City.Name
                );

            TypeAdapterConfig<UserEntity, UserModel>
                .NewConfig()
                .Map(
                    dest => dest.Role,
                    src => src.RoleId
                );

            TypeAdapterConfig<UserModel, UserEntity>
                .NewConfig()
                .Map(
                    dest => dest.RoleId,
                    src => (int)src.Role
                );

            TypeAdapterConfig<PriceRequest, PriceModel>
                .NewConfig()
                .Map(
                    dest => dest.Currency,
                    src => 
                        new CurrencyModel()
                        {
                            Id = src.CurrencyId
                        }
                );

            TypeAdapterConfig<CinemaServiceRequest, CinemaServiceModel>
                .NewConfig()
                .Map(
                    dest => dest.Price,
                    src => src.Price.Adapt<PriceModel>()
                );

            TypeAdapterConfig<SeatOrderEntity, SeatOrderModel>
                .NewConfig()
                .Map(
                    dest => dest.Place,
                    src => src.Seat.Place
                )
                .Map(
                    dest => dest.Row,
                    src => src.Seat.Row
                );

            TypeAdapterConfig<OrderEntity, OrderModel>
                .NewConfig()
                .Map(
                    dest => dest.SessionStart,
                    src => src.Session.StartDateTime
                )
                .Map(
                    dest => dest.CinemaId,
                    src => src.Session.Hall.CinemaId
                )
                .Map(
                    dest => dest.CinemaName,
                    src => src.Session.Hall.Cinema.Name
                )
                .Map(
                    dest => dest.HallId,
                    src => src.Session.HallId
                )
                .Map(
                    dest => dest.HallName,
                    src => src.Session.Hall.Name
                )
                .Map(
                    dest => dest.Price,
                    src => new PriceModel(
                        src.Price,
                        src.Currency.Adapt<CurrencyModel>()
                    )
                );

            TypeAdapterConfig<OrderModel, OrderEntity>
                .NewConfig()
                .Map(
                    dest => dest.Price,
                    src => src.Price.Value
                )
                .Map(
                    dest => dest.CurrencyId,
                    src => src.Price.Currency.Id
                );

            TypeAdapterConfig<SeatTypePriceModel, SeatTypePriceEntity>
                .NewConfig()
                .Map(
                    dest => dest.Price,
                    src => src.Price.Value
                )
                .Map(
                    dest => dest.CurrencyId,
                    src => src.Price.Currency.Id
                );

            TypeAdapterConfig<SeatTypePriceEntity, SeatTypePriceModel>
                .NewConfig()
                .Map(
                    dest => dest.SeatTypeName,
                    src => src.SeatType.Name
                )
                .Map(
                    dest => dest.Price,
                    src => new PriceModel(
                        src.Price,
                        src.Currency.Adapt<CurrencyModel>()
                    )
                );

            TypeAdapterConfig<SessionEntity, SessionModel>
                .NewConfig()
                .Map(
                    dest => dest.FilmName,
                    src => src.Film.Name
                )
                .Map(
                    dest => dest.HallName,
                    src => src.Hall.Name
                );

            TypeAdapterConfig<SessionSeatEntity, SessionSeatModel>
                .NewConfig()
                .Map(
                    dest => dest.Row,
                    src => src.Seat.Row
                )
                .Map(
                    dest => dest.Place,
                    src => src.Seat.Place
                )
                .Map(
                    dest => dest.SeatTypeId,
                    src => src.Seat.SeatTypeId
                );
        }
    }
}
