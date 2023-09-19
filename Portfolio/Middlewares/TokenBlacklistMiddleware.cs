using Services.Interfaces;

namespace Api.Middlewares
{
    public class TokenBlacklistMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ITokensService _tokenService;

        public TokenBlacklistMiddleware(RequestDelegate next, ITokensService tokenService)
        {
            _next = next;
            _tokenService = tokenService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].ToString();

            if (!string.IsNullOrEmpty(token)
                && await _tokenService.IsTokenBlacklistedAsync(token.Split(' ')[1]))
            {
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsync("This token is invalid.");
                return;
            }

            await _next(context);
        }
    }
}
