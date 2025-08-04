namespace LeaveManagementSystemPractice.web.Services.LeaveAllocations;

public interface ILeaveAllocationsService
{
    Task AllocateLeaveAsync(string employeeId);
}