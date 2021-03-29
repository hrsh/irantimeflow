using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IranTimeFlow.WebApp.Helpers
{
    public static class CustomAuthOptions
    {
        public static void AddCustomPolicies(this AuthorizationOptions options)
        {
            var roles = ClassProperties
                .GetFields(typeof(RoleNames))
                .Resolve(a => a.Key);

            foreach (var role in roles)
            {
                var requirement = $"Require{role}";
                options.AddPolicy(requirement, policy =>
                {
                    policy.RequireRole(role);
                });
            }

            options.AddPolicy(PolicyRequirements.RequireLogin, policy =>
            {
                policy.RequireRole(
                    RoleNames.Admin,
                    RoleNames.SimpleUser);
            });
        }
    }
}
