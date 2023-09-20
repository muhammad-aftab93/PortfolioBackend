using Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Services.Interfaces;

namespace Api.Endpoints
{
    public static class EducationsEndpoints
    {
        public static void RegisterEducationsEndpoints(this WebApplication app)
        {
            app.MapGet("/educations", async (IEducationsService educationService, IMapper mapper)
                    => mapper.Map<List<Api.Models.Educations>>(await educationService.GetAsync()))
                .WithName("Get all educations")
                .WithOpenApi();

            app.MapGet("/educations/{id}", async (string id, IEducationsService educationService, IMapper mapper)
                    => mapper.Map<Api.Models.Educations>(await educationService.GetByIdAsync(id)))
                .WithName("Get education by id")
                .WithOpenApi();

            app.MapPost("/educations", [Authorize] async (CreateEducationRequest createEducationRequest, IEducationsService educationService, IMapper
                    mapper) =>
                {
                    var result = await educationService.CreateAsync(
                        mapper.Map<Database.Entities.Educations>(createEducationRequest)
                    );

                    return mapper.Map<Api.Models.Educations>(result);
                })
                .WithName("Create new education")
                .WithOpenApi();

            app.MapPut("/educations/{id}", [Authorize] async (string id, Educations education, IEducationsService educationService,
                        IMapper mapper)
                    => await educationService.UpdateAsync(mapper.Map<Database.Entities.Educations>(education), id))
                .WithName("Update education")
                .WithOpenApi();

            app.MapDelete("/educations/{id}", [Authorize] async (string id, IEducationsService educationService)
                    => await educationService.DeleteAsync(id))
                .WithName("Delete education by id")
                .WithOpenApi();
        }
        
    }
}
