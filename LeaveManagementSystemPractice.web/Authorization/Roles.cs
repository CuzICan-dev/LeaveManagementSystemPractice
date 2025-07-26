namespace LeaveManagementSystemPractice.web.Authorization;

public static class Roles
{
    public const string Employee      = "Employee";    
    public const string Supervisor    = "Supervisor";    
    public const string Administrator = "Administrator";
    
    public static IReadOnlyCollection<string> All { get; } =
        new[] { Employee, Supervisor, Administrator };
}