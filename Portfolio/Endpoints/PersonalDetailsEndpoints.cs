using System.Security.Claims;
using AutoMapper;
using Common;
using Microsoft.AspNetCore.Authorization;
using Services.Interfaces;

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
        }
    }
}
