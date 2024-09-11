using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OnlineStoreOfBoardGames.Data.Enums;
using OnlineStoreOfBoardGames.Services.AuthStuff;

namespace OnlineStoreOfBoardGames.Controllers.ActionFilterAttributes
{
    public class HasPermissionAttribute : ActionFilterAttribute
    {
        private Permission _permission;

        public HasPermissionAttribute(Permission permission)
        {
            _permission = permission;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var authService = context.HttpContext.RequestServices.GetService<AuthService>();
            if (!authService!.HasPermission(_permission))
            {
                context.Result = new ForbidResult();
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
