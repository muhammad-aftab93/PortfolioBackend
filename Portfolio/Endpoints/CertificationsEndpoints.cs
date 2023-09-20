using Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Services.Interfaces;

namespace Api.Endpoints;

public static class CertificationsEndpoints
{
    public static void RegisterCertificationsEndpoints(this WebApplication app)
    {
        app.MapGet("/certifications", async (ICertificationsService certificationService, IMapper mapper)
                => mapper.Map<List<Api.Models.Certifications>>(await certificationService.GetAsync()))
            .WithName("Get all certifications")
            .WithOpenApi();

        app.MapGet("/certifications/{id}", async (string id, ICertificationsService certificationService, IMapper mapper)
                => mapper.Map<Api.Models.Certifications>(await certificationService.GetByIdAsync(id)))
            .WithName("Get certification by id")
            .WithOpenApi();

        app.MapPost("/certifications", [Authorize] async (CreateCertificationRequest createCertificationRequest, ICertificationsService 
        certificationService, IMapper
                mapper) =>
            {
                var result = await certificationService.CreateAsync(
                    mapper.Map<Database.Entities.Certifications>(createCertificationRequest)
                );

                return mapper.Map<Api.Models.Certifications>(result);
            })
            .WithName("Create new certification")
            .WithOpenApi();

        app.MapPut("/certifications/{id}", [Authorize] async (string id, Certifications certification, 
        ICertificationsService 
        certificationService,
                    IMapper mapper)
                => await certificationService.UpdateAsync(mapper.Map<Database.Entities.Certifications>(certification), id))
            .WithName("Update certification")
            .WithOpenApi();

        app.MapDelete("/certifications/{id}", [Authorize] async (string id, ICertificationsService certificationService)
                => await certificationService.DeleteAsync(id))
            .WithName("Delete certification by id")
            .WithOpenApi();
    }
}