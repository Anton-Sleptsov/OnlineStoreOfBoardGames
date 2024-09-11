using OnlineStoreOfBoardGames.Data.Repositories;
using OnlineStoreOfBoardGames.Services.AuthStuff;
using System.Globalization;

namespace OnlineStoreOfBoardGames.CustomMiddlewareServices
{
    public class LocalizationMiddleware
    {
        private readonly RequestDelegate _next;

        public LocalizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var authService = context.RequestServices.GetRequiredService<AuthService>();
            if (authService.IsAuthenticated())
            {
                var lang = authService.GetUserLanguage();

                CultureInfo culture;
                switch (lang)
                {
                    case Data.Enums.Language.En:
                        culture = new CultureInfo("en-US");
                        break;
                    case Data.Enums.Language.Ru:
                        culture = new CultureInfo("ru-RU");
                        break;
                    default:
                        throw new Exception($"Unexpected enum value: {lang}");
                }

                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }

            // Call next middleware service
            await _next.Invoke(context);
        }
    }
}
