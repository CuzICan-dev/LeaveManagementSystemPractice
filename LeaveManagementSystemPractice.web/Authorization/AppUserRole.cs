namespace LeaveManagementSystemPractice.web.Authorization;

public enum AppUserRole
{
    Employee      = 0, // Full access, “god-mode”
    Supervisor    = 1, // Manages users/permissions beneath him
    Administrator = 2, // Limited managerial capabilities
}