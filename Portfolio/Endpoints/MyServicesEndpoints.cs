using Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Services.Interfaces;

namespace Api.Endpoints;

public static class MyServicesEndpoints
{
    public static void RegisterMyServicesEndpoint(this WebApplication app)
    {
        app.MapGet("/myservices", async (IMyServicesService myServicesService, IMapper mapper)
                => mapper.Map<List<Api.Models.MyServices>>(await myServicesService.GetAsync()))
            .WithName("Get all my services")
            .WithOpenApi();

        app.MapGet("/myservices/{id}", async (string id, IMyServicesService myServicesService, IMapper mapper)
                => mapper.Map<Api.Models.MyServices>(await myServicesService.GetByIdAsync(id)))
            .WithName("Get my service by id")
            .WithOpenApi();
        
        app.MapPost("/myservices", [Authorize] async (CreateMyServiceRequest myServiceRequest, IMyServicesService myServicesService, IMapper 
        mapper) =>
            {
                var result = await myServicesService.CreateAsync(
                    mapper.Map<Database.Entities.MyServices>(myServiceRequest)
                );
        
                return mapper.Map<Api.Models.MyServices>(result);
            })
            .WithName("Create new my service")
            .WithOpenApi();
        
        app.MapPut("/myservices/{id}", [Authorize] async (string id, MyServices myServices, IMyServicesService myServicesService, 
        IMapper mapper)
                => await myServicesService.UpdateAsync(mapper.Map<Database.Entities.MyServices>(myServices), id))
            .WithName("Update my service")
            .WithOpenApi();
        
        app.MapDelete("/myservices/{id}", [Authorize] async (string id, IMyServicesService myServicesService)
                => await myServicesService.DeleteAsync(id))
            .WithName("Delete my services by id")
            .WithOpenApi();
    }
}