﻿namespace Api.Endpoints
{
    public static class Endpoints
    {
        public static void RegisterEndpoints(this WebApplication app)
        {
            app.RegisterUserEndpoint();
            app.RegisterPersonalDetailsEndpoint();
            app.RegisterMySkillsEndpoint();
            app.RegisterMyServicesEndpoint();
            app.RegisterExperiencesEndpoint();
            app.RegisterEducationsEndpoints();
            app.RegisterCertificationsEndpoints();
        }
    }
}
