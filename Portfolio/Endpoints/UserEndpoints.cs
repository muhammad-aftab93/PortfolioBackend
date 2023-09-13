using Api.Models;
using AutoMapper;
using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Endpoints
{
    public static class UserEndpoints
    {
        public static void RegisterUserEndpoint(this WebApplication app)
        {
            app.MapGet("/users", async (IUserService userService, IMapper mapper)
                    => mapper.Map<List<Api.Models.User>>(await userService.GetAsync()))
            .WithName("Get all users")
            .WithOpenApi();

            app.MapGet("/users/{id}", async (string id, IUserService userService, IMapper mapper)
                    => mapper.Map<Api.Models.User>(await userService.GetByIdAsync(id)))
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

            app.MapPut("/users/{id}", async (string id, User user, IUserService userService, IMapper mapper)
                    => await userService.UpdateAsync(mapper.Map<Database.Entities.User>(user), id))
                .WithName("Update user")
                .WithOpenApi();

            app.MapDelete("/users/{id}", async (string id, IUserService userService)
                    => await userService.DeleteAsync(id))
                .WithName("Delete user by id")
                .WithOpenApi();

            app.MapPost("/users/login", [AllowAnonymous] async ([FromBody] LoginRequest request, IUserService userService, IMapper mapper) =>
                {
                    if (string.IsNullOrEmpty(request.Email))
                        return Results.BadRequest("Email is required.");
                    if (string.IsNullOrEmpty(request.Password))
                        return Results.BadRequest("Password is required.");

                    var user = await userService.GetByEmailAsync(request.Email);
                    if (user is null)
                        return Results.NotFound("Invalid username/password.");

                    if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                        return Results.BadRequest("Invalid username/password.");

                    var token = HelperFunctions.GenerateToken(user.Id, request.Email);

                    return Results.Ok(new
                    {
                        user = mapper.Map<Api.Models.User>(user),
                        token = token
                    });
                })
                .WithName("Login user")
                .WithOpenApi();

            app.MapGet("users/test", [Authorize] (HttpContext context) =>
                {
                    var userId = context.User.FindFirstValue("id");
                    return Results.Ok("Hello World!");
                })
                .WithName("Test user")
                .WithOpenApi();
        }
    }
}
