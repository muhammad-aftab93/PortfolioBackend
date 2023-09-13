using Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Services.Interfaces;

namespace Api.Endpoints;

public static class MySkillsEndpoints
{
    public static void RegisterMySkillsEndpoint(this WebApplication app)
    {
        app.MapGet("/myskills", async (IMySkillsService skillsService, IMapper mapper)
                => mapper.Map<List<Api.Models.MySkills>>(await skillsService.GetAsync()))
            .WithName("Get all my skills")
            .WithOpenApi();

        app.MapGet("/myskills/{id}", async (string id, IMySkillsService skillsService, IMapper mapper)
                => mapper.Map<Api.Models.MySkills>(await skillsService.GetByIdAsync(id)))
            .WithName("Get my skill by id")
            .WithOpenApi();
        
        app.MapPost("/myskills", [Authorize] async (CreateMySkillsRequest mySkill, IMySkillsService skillsService, IMapper mapper) =>
            {
                var result = await skillsService.CreateAsync(
                    mapper.Map<Database.Entities.MySkills>(mySkill)
                );
        
                return mapper.Map<Api.Models.MySkills>(result);
            })
            .WithName("Create new my skill")
            .WithOpenApi();
        
        app.MapPut("/myskills/{id}", [Authorize] async (string id, MySkills mySkills, IMySkillsService skillsService, IMapper mapper)
                => await skillsService.UpdateAsync(mapper.Map<Database.Entities.MySkills>(mySkills), id))
            .WithName("Update my skill")
            .WithOpenApi();
        
        app.MapDelete("/myskills/{id}", [Authorize] async (string id, IMySkillsService skillsService)
                => await skillsService.DeleteAsync(id))
            .WithName("Delete my skill by id")
            .WithOpenApi();
    }
}