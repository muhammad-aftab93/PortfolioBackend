using Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Services.Interfaces;

namespace Api.Endpoints
{
    public static class ExperiencesEndpoints
    {
        public static void RegisterExperiencesEndpoint(this WebApplication app)
        {
            app.MapGet("/experiences", async (IExperiencesService experiencesService, IMapper mapper)
                    => mapper.Map<List<Api.Models.Experiences>>(await experiencesService.GetAsync()))
                .WithName("Get all my experiences")
                .WithOpenApi();

            app.MapGet("/experiences/{id}", async (string id, IExperiencesService experiencesService, IMapper mapper)
                    => mapper.Map<Api.Models.Experiences>(await experiencesService.GetByIdAsync(id)))
                .WithName("Get my experience by id")
                .WithOpenApi();

            app.MapPost("/experiences", [Authorize] async (CreateExperienceRequest experienceRequest, IExperiencesService experiencesService, IMapper
                    mapper) =>
                {
                    var result = await experiencesService.CreateAsync(
                        mapper.Map<Database.Entities.Experiences>(experienceRequest)
                    );

                    return mapper.Map<Api.Models.Experiences>(result);
                })
                .WithName("Create new my experience")
                .WithOpenApi();

            app.MapPut("/experiences/{id}", [Authorize] async (string id, Experiences experiences, IExperiencesService experiencesService,
                        IMapper mapper)
                    => await experiencesService.UpdateAsync(mapper.Map<Database.Entities.Experiences>(experiences), id))
                .WithName("Update my experience")
                .WithOpenApi();

            app.MapDelete("/experiences/{id}", [Authorize] async (string id, IExperiencesService experiencesService)
                    => await experiencesService.DeleteAsync(id))
                .WithName("Delete my experience by id")
                .WithOpenApi();
        }
    }
}
