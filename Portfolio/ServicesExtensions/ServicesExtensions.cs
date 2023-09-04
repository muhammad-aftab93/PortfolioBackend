using Api.Endpoints;
using Common;
using Database.Services;
using Database.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Services;
using Services.Interfaces;
using System.Text;

namespace Api.ServicesExtensions
{
    public static class ServicesExtensions
    {
        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            string connectionString = builder.Configuration["AppSettings"]!;

            // Load Configuration form Azure App Configuration
            builder.Configuration.AddAzureAppConfiguration(connectionString);
            MongoDbSettings.ConnectionURI = builder.Configuration["MongoDbSettings:ConnectionURI"]!;
            MongoDbSettings.DatabaseName = builder.Configuration["MongoDbSettings:DatabaseName"]!;
            JwtSettings.JwtIssuer = builder.Configuration["JwtSettings:JwtIssuer"]!;
            JwtSettings.JwtAudience = builder.Configuration["JwtSettings:JwtAudience"]!;
            JwtSettings.JwtSecretKey = builder.Configuration["JwtSettings:JwtSecretKey"]!;
            builder.Services.AddCors();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = JwtSettings.JwtIssuer,
                        ValidAudience = JwtSettings.JwtAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.JwtSecretKey))
                    };
                });
            builder.Services.AddAuthorization();

            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddSingleton(typeof(IMongoDbService<>), typeof(MongoDbService<>)); // below service is being used as an alternative
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

            app.UseCors(options =>
            {
                options.AllowAnyOrigin();
                options.AllowAnyMethod();
                options.AllowAnyHeader();
            });
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHttpsRedirection();
            app.RegisterEndpoints();

        }
    }
}
