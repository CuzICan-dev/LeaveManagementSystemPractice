using LeaveManagementSystemPractice.web.Data;
using LeaveManagementSystemPractice.web.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystemPractice.web.Services.LeaveAllocations;

public class LeaveAllocationsService(ApplicationDbContext context) : ILeaveAllocationsService
{
    public async Task AllocateLeaveAsync(string employeeId)
    {
        //get all the leave types
        var leaveTypes = await context.LeaveTypes.ToListAsync();
        
        // get the current period based on the year
        var currentDate = DateTime.Now;
        var currentPeriod = await context.Periods.SingleAsync(p => p.EndDate.Year == currentDate.Year);
        var monthRemaining = currentPeriod.EndDate.Month - currentDate.Month;
        
        // for each leave type, create a new allocation for the employee
        foreach (var leaveType in leaveTypes)
        {
            var accuralRate = decimal.Divide(leaveType.NumberOfDays, 12);
            var leaveAllocation = new LeaveAllocation
            {
                EmployeeId = employeeId,
                LeaveTypeId = leaveType.Id,
                PeriodId = currentPeriod.Id,
                Days = (int)Math.Ceiling(monthRemaining * accuralRate)
            };
            context.Add(leaveAllocation);
        }
        // save changes to the database
        await context.SaveChangesAsync();
    }
    public async Task<List<LeaveAllocation>> GetAllocationsAsync(string employeeId)
    {
        return await context.LeaveAllocations
            .Include(la => la.LeaveType)
            .Include(la => la.Period)
            .Include(la => la.Employee)
            .Where(la => la.EmployeeId == employeeId)
            .ToListAsync();
    }
}