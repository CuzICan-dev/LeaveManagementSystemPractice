using Microsoft.AspNetCore.Identity;

namespace LeaveManagementSystemPractice.web.Authorization;

public static class IdentityUserSeeder
{
    public static async Task SeedUsersAsync(this IServiceProvider services, ILogger logger)
    {
        using var scope = services.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        if (!roleManager.Roles.Any())
        {
            logger.LogInformation("No roles found. Please seed roles first.");
            throw new InvalidOperationException("Roles must be seeded before users.");
        }
        
        // 2️⃣  Users to create
        var seedUsers = new[]
        {
            new SeedUser
            {
                Email       = "admin@example.com",
                FirstName   = "System",
                LastName    = "Administrator",
                Password    = "ChangeMe123!",
                DateOfBirth = new DateOnly(1990, 1, 1), // optional, can be set later
                EnumRole    = AppUserRole.Administrator // default role
            },
            new SeedUser
            {
                Email       = "manager@example.com",
                FirstName   = "Marty",
                LastName    = "Manager",
                Password    = "ChangeMe123!",
                DateOfBirth = new DateOnly(1985, 5, 15), // optional, can be set later
                EnumRole    = AppUserRole.Supervisor // default role
            },
            new SeedUser
            {
                Email       = "employee1@example.com",
                FirstName   = "Stephanie",
                LastName    = "Muller",
                Password    = "ChangeMe123!",
                DateOfBirth = new DateOnly(1992, 3, 20), // optional, can be set later
                EnumRole    = AppUserRole.Employee // default role
            },
            new SeedUser
            {
            Email       = "employee2@example.com",
            FirstName   = "Stefan",
            LastName    = "King",
            Password    = "ChangeMe123!",
            DateOfBirth = new DateOnly(1995, 7, 30), // optional, can be set later
            EnumRole    = AppUserRole.Employee // default role
            }
            // add more if needed
        };

        foreach (var s in seedUsers)
        {
            var user = await userManager.FindByEmailAsync(s.Email);

            if (user is not null) continue;
            user = new ApplicationUser
            {
                UserName     = s.Email,
                Email        = s.Email,
                EmailConfirmed = true,
                FirstName    = s.FirstName,
                LastName     = s.LastName,
                DateOfBirth = s.DateOfBirth
            };
                
            var result = await userManager.CreateAsync(user, s.Password);
            if (!result.Succeeded)
            {
                logger.LogError("Failed to create seed user {Email}: {Errors}", s.Email,
                    string.Join(", ", result.Errors.Select(e => e.Description)));
                continue;
            }
                
            logger.LogInformation("Created seed user {Email}", s.Email);
            
            // assign roles
            var roleName = s.EnumRole.ToString();
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                logger.LogWarning("Role {Role} does not exist, skipping assignment for {Email}", roleName, s.Email);
                continue;
            }
                
            await userManager.AddToRoleAsync(user, roleName);
            logger.LogInformation("Assigned role {Role} to user {Email}", roleName, s.Email);
        }
    }
    
    // helper POCO
    private sealed record SeedUser
    {
        public string Email      { get; init; } = default!;
        public string FirstName  { get; init; } = default!;
        public string LastName   { get; init; } = default!;
        public string Password   { get; init; } = default!;
        public DateOnly DateOfBirth { get; set; }= default!; // optional, can be set later
        public AppUserRole EnumRole { get; init; } = AppUserRole.Employee; // default role
    }
}