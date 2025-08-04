using LeaveManagementSystemPractice.web.Data.Entities;

namespace LeaveManagementSystemPractice.web.Services.LeaveAllocations;

public interface ILeaveAllocationsService
{
    Task AllocateLeaveAsync(string employeeId);
    Task<List<LeaveAllocation>> GetAllocationsAsync(string employeeId);
}