using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Shifts.DAL.Models;

namespace Shifts.Components.Account
{
    internal sealed class IdentityUserAccessor
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly NavigationManager navigationManager;

        public IdentityUserAccessor(UserManager<ApplicationUser> userManager, NavigationManager navigationManager)
        {
            this.userManager = userManager;
            this.navigationManager = navigationManager;
        }

        public async Task<ApplicationUser?> GetRequiredUserAsync(HttpContext context)
        {
            var user = await userManager.GetUserAsync(context.User);

            if (user is null)
            {
                CookieBuilder statusCookieBuilder = new()
                {
                    SameSite = SameSiteMode.Strict,
                    HttpOnly = true,
                    IsEssential = true,
                    MaxAge = TimeSpan.FromSeconds(5),
                };
                context.Response.Cookies.Append("Identity.StatusMessage", $"Error: Unable to load user with ID '{userManager.GetUserId(context.User)}'.", statusCookieBuilder.Build(context));
                navigationManager.NavigateTo("Account/InvalidUser");
            }

            return user;
        }
    }
}
