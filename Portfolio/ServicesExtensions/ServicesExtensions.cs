using Api.Endpoints;
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
            var mongoDbSettings = builder.Configuration.GetSection("MongoDbSettings");
            MongoDbSettings.ConnectionURI = mongoDbSettings["ConnectionURI"]!;
            MongoDbSettings.DatabaseName = mongoDbSettings["DatabaseName"]!;
            //builder.Services.AddOptions<MongoDbSettings>().BindConfiguration("MongoDbSettings"); an alternate to the above line
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
