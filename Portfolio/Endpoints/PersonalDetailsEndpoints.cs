using Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Services.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints
{
    public static class PersonalDetailsEndpoints
    {
        public static void RegisterPersonalDetailsEndpoint(this WebApplication app)
        {
            app.MapGet("/personal-details", async (IPersonalDetailsService personalDetailsService, IUserService userService, IMapper mapper) =>
                {
                    var personalDetails = mapper.Map<Api.Models.PersonalDetails>(await personalDetailsService.GetAsync());
                    var users = await userService.GetAsync();
                    personalDetails.Email = users is not null && users.Count > 0 ? users[0].Email : "";
                    return personalDetails;
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
            
            app.MapPost("/upload-picture",
            //[Authorize]
            async ([FromForm(Name = "file")] IFormFile file, IBlobService blobService, IPersonalDetailsService 
            personalDetailsService, IMapper mapper) =>
            {
                var url = await blobService.UploadAsync(file);
                var personalDetails = mapper.Map<Api.Models.PersonalDetails>(await personalDetailsService.GetAsync());
                personalDetails.Picture = url;
                var result = await personalDetailsService.SaveAsync(
                    mapper.Map<Database.Entities.PersonalDetails>(personalDetails),
                    true
                );
                
                return result ? "File uploaded successfully!" : "Failed to upload file!";
            })
            .WithName("Upload picture");
        }
    }
}
