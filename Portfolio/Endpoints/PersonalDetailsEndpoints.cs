using Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Services.Interfaces;
using System.Security.Claims;

namespace Api.Endpoints
{
    public static class PersonalDetailsEndpoints
    {
        public static void RegisterPersonalDetailsEndpoint(this WebApplication app)
        {
            app.MapGet("/personal-details", async (IPersonalDetailsService personalDetailsService, IMapper mapper) =>
                {
                    return mapper.Map<Api.Models.PersonalDetails>(await personalDetailsService.GetAsync());
                })
                .WithName("Get personal details")
                .WithOpenApi();

            app.MapPost("/personal-details", [Authorize] async (PersonalDetails personalDetails,IPersonalDetailsService  personalDetailsService, HttpContext context, IMapper mapper) =>
                {
                    var userId = context.User.FindFirstValue("id");
                    personalDetails.UserId = userId!;
                    var result = await personalDetailsService.SaveAsync(
                            mapper.Map<Database.Entities.PersonalDetails>(personalDetails)
                        );
                    return mapper.Map<Api.Models.PersonalDetails>(result);
                })
                .WithName("Save personal details")
                .WithOpenApi();
        }
    }
}
