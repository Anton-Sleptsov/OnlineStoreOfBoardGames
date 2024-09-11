namespace OnlineStoreOfBoardGames.CustomMiddlewareServices
{
    public class MyGlobalHandlerException
    {
        private readonly RequestDelegate _next;

        public MyGlobalHandlerException(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // Get ALL Exception from any request to any contoller
                Console.WriteLine(ex.Message);
            }
        }
    }
}
