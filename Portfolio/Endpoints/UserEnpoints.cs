using Api.Models;
using AutoMapper;
using Microsoft.Extensions.Options;
using Services;
using Services.Interfaces;

namespace Api.Endpoints
{
    public static class UserEnpoints
    {
        public static void RegisterWeatherForecastEndpoint(this WebApplication app)
        {
            app.MapGet("/users", async (IUserService userService, IMapper mapper) =>
                {
                    return mapper.Map<Api.Models.User>(await userService.GetAsync());
                })
            .WithName("Get all users")
            .WithOpenApi();

            app.MapGet("/users/{id}", async (string id, IUserService userService, IMapper mapper) =>
                {
                    return mapper.Map<Api.Models.User>(await userService.GetByIdAsync(id));
                })
                .WithName("Get user by id")
                .WithOpenApi();

            app.MapPost("/users", async (CreateUserRequest user, IUserService userService, IMapper mapper) =>
                {
                     var result = await userService.CreateAsync(
                        mapper.Map<Database.Entities.User>(user)
                        );

                     return mapper.Map<Api.Models.User>(result);
                })
                .WithName("Create user")
                .WithOpenApi();

            app.MapPut("/users/{id}", async (string id, User user, IUserService userService, IMapper mapper) =>
                {
                    return await userService.UpdateAsync(mapper.Map<Database.Entities.User>(user), id);
                })
                .WithName("Update user")
                .WithOpenApi();

            app.MapDelete("/users/{id}", async (string id, IUserService userService) =>
                {
                    return await userService.DeleteAsync(id);
                })
                .WithName("Delete user by id")
                .WithOpenApi();

        }
    }
}
