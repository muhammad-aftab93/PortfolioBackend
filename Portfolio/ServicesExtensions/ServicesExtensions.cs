﻿using Api.Endpoints;
using Common;
using Database.Services;
using Database.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Services;
using Services.Interfaces;
using Api.Middlewares;
using Microsoft.OpenApi.Models;
using Common.Interfaces;
using Microsoft.Extensions.Options;

namespace Api.ServicesExtensions
{
    public static class ServicesExtensions
    {
        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Portfolio API", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            string connectionString = builder.Configuration["AppSettings"]!;
            // Load Configuration form Azure App Configuration
            builder.Configuration.AddAzureAppConfiguration(connectionString);
            MongoDbSettings.ConnectionURI = builder.Configuration["MongoDbSettings:ConnectionURI"]!;
            MongoDbSettings.DatabaseName = builder.Configuration["MongoDbSettings:DatabaseName"]!;
            JwtSettings.JwtIssuer = builder.Configuration["JwtSettings:JwtIssuer"]!;
            JwtSettings.JwtAudience = builder.Configuration["JwtSettings:JwtAudience"]!;
            JwtSettings.JwtSecretKey = builder.Configuration["JwtSettings:JwtSecretKey"]!;
            BlobSettings.ConnectionString = builder.Configuration["BlobSettings:ConnectionString"]!;
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });
            builder.Services.AddTransient<IHelperFunctions, HelperFunctions>();
            var helperFunctions = builder.Services.BuildServiceProvider().GetRequiredService<IHelperFunctions>();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = helperFunctions.GetTokenValidationParameters());
            builder.Services.AddAuthorization();
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IPersonalDetailsService, PersonalDetailsService>();
            builder.Services.AddTransient<IMySkillsService, MySkillsService>();
            builder.Services.AddTransient<IMyServicesService, MyServicesService>();
            builder.Services.AddTransient<ITokensService, TokensService>();
            builder.Services.AddTransient<IExperiencesService, ExperiencesService>();
            builder.Services.AddTransient<IEducationsService, EducationsService>();
            builder.Services.AddTransient<ICertificationsService, CertificationsService>();
            builder.Services.AddTransient<IBlobService, BlobService>();
            builder.Services.AddSingleton(typeof(IMongoRepository<>), typeof(MongoRepository<>)); // below service is being used as an alternative
            builder.Services.AddSingleton(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        public static void UseServices(this WebApplication app)
        {
            app.UseSwagger();
            if (!app.Environment.IsDevelopment())
                app.UseSwaggerUI(c =>
                {
                    c.RoutePrefix = "";
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Portfolio API v1");
                });
            else
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Portfolio API v1"));

            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<AuthorizationHeaderMiddleware>();
            app.UseMiddleware<TokenBlacklistMiddleware>();
            app.UseHttpsRedirection();
            app.RegisterEndpoints();

        }
    }
}
