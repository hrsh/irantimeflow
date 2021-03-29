using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;

namespace IranTimeFlow.WebApp.Helpers
{
    public static class CustomPolicyConfiguration
    {
        public static void WebAppPolicyOptions(this RazorPagesOptions options)
        {
            options.Conventions.AuthorizeAreaFolder(AreaNames.Admin,
                "/",
                PolicyRequirements.RequireAdmin);

            options.Conventions.AuthorizeAreaFolder(AreaNames.SimpleUser,
                "/",
                PolicyRequirements.RequireLogin);
        }
    }
}
