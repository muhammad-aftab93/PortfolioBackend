namespace Api.Endpoints
{
    public static class Endpoints
    {
        public static void RegisterEndpoints(this WebApplication app)
        {
            app.RegisterUserEndpoint();
        }
    }
}
