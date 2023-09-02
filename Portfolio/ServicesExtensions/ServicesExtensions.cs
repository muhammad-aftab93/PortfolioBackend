using Api.Endpoints;
using Api.Mappings;
using Common;
using Database.Services;
using Database.Services.Interfaces;
using Services;
using Services.Interfaces;

namespace Api.ServicesExtensions
{
    public static class ServicesExtensions
    {
        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            string connectionString = builder.Configuration["AppSettings"]!;

            // Load Confiuration form Azure App Configuration
            builder.Configuration.AddAzureAppConfiguration(connectionString);
            MongoDbSettings.ConnectionURI = builder.Configuration["MongoDbSettings:ConnectionURI"]!;
            MongoDbSettings.DatabaseName = builder.Configuration["MongoDbSettings:DatabaseName"]!;
            
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddSingleton(typeof(IMongoDbService<>), typeof(MongoDbService<>));
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

            app.UseHttpsRedirection();
            app.RegisterEndpoints();

        }
    }
}
