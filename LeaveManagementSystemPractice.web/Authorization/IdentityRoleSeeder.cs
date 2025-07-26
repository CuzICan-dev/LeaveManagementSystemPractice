using Microsoft.AspNetCore.Identity;

namespace LeaveManagementSystemPractice.web.Authorization;

public static class IdentityRoleSeeder
{
    public static async Task SeedRolesAsync(this IServiceProvider services, ILogger logger)
    {
        using var scope = services.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        foreach (var role in Roles.All)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                logger.LogInformation("Creating missing role {Role}", role);
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}