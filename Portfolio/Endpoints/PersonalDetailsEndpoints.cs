using Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Services.Interfaces;
using System.Security.Claims;
using Common;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Services;

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

            app.MapPost("/personal-details", [Authorize] async (PersonalDetails personalDetails,IPersonalDetailsService  personalDetailsService, IUserService userService, HttpContext context, IMapper mapper) =>
                {
                    var userId = context.User.FindFirstValue("id");
                    personalDetails.UserId = userId!;
                    var result = await personalDetailsService.SaveAsync(
                            mapper.Map<Database.Entities.PersonalDetails>(personalDetails)
                        );
                    var users = await userService.GetAsync();
                    var resultPersonalDetails = mapper.Map<Api.Models.PersonalDetails>(result);
                    resultPersonalDetails.Email = users is not null && users.Count > 0 ? users[0].Email : "";
                    return resultPersonalDetails;
                })
                .WithName("Save personal details")
                .WithOpenApi();
            
            app.MapPost("/upload-picture", [Authorize] async ([FromForm(Name = "file")] IFormFile file, IBlobService blobService, IPersonalDetailsService 
            personalDetailsService, IMapper mapper) =>
            {
                try
                {
                    var personalDetails = await personalDetailsService.GetAsync();
                    var url = await blobService.UploadAsync(file, BlobContainers.Images);
                    await blobService.RemoveFileAsync(HelperFunctions.ExtractFileNameFromUrl(personalDetails!.Picture), BlobContainers.Images);
                    personalDetails.Picture = url;
                    await personalDetailsService.SaveAsync(personalDetails, true);
                    return Results.Ok("File uploaded successfully!");
                }
                catch (Exception e)
                {
                    return Results.BadRequest("Failed to upload file!");
                }
                
            })
            .WithName("Upload picture");
        }
    }
}
