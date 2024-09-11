namespace BoardGamesReviewsApi.Middlewares
{
    public class AllowAllCorsMiddleware
    {
        private readonly RequestDelegate _next;

        public AllowAllCorsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.Headers.Add("Access-Control-Allow-Methods", "POST,GET,DELETE,PUT,OPTIONS,HEAD,PATCH");

            // Call next middleware service
            await _next.Invoke(context);
        }
    }
}
