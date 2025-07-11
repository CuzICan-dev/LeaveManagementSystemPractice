using AutoMapper;
using LeaveManagementSystemPractice.web.Data;
using LeaveManagementSystemPractice.web.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystemPractice.web.Services.LeaveTypes;

public class LeaveTypesService(ApplicationDbContext context, IMapper mapper) : ILeaveTypesService
{
    public async Task<List<LeaveTypeReadOnlyVM>> GetAllAsync()
    {
        var leaveTypes = await context.LeaveTypes.ToListAsync();
        return mapper.Map<List<LeaveTypeReadOnlyVM>>(leaveTypes);
    }

    public async Task<T> GetTypeByIdAsync<T>(int id) where T : class
    {
        var leaveType = await context.LeaveTypes
            .FirstOrDefaultAsync(m => m.Id == id);
        if (leaveType == null)
        {
            return null;
        }
        return mapper.Map<T>(leaveType);
    }
    
    public async Task CreateAsync(LeaveTypeCreateVM leaveTypeCreate)
    {
        LeaveType leaveType = mapper.Map<LeaveType>(leaveTypeCreate);
        context.Add(leaveType);
        await context.SaveChangesAsync();
    }

    public async Task EditAsync(LeaveTypeEditVM leaveTypeEdit)
    {
        var leaveType = mapper.Map<LeaveType>(leaveTypeEdit);
        context.Update(leaveType);
        await context.SaveChangesAsync();
    }

    public async Task RemoveAsync(int id)
    {
        var leaveType = await context.LeaveTypes.FirstOrDefaultAsync(m => m.Id == id);
        if (leaveType != null)
        {
            context.LeaveTypes.Remove(leaveType);
            await context.SaveChangesAsync();
        }
    }
    
    public bool LeaveTypeExists(int id)
    {
        return context.LeaveTypes.Any(e => e.Id == id);
    }
    
    public async Task<bool> CheckIfLeaveTypeNameExists(string name)
    {
        return await context.LeaveTypes
            .AnyAsync(e => e.Name.ToLower().Equals(name.ToLower()));
    }
    
    public async Task<bool> CheckIfLeaveTypeNameExists(LeaveTypeEditVM leaveTypeEdit)
    {
        return await context.LeaveTypes
            .AnyAsync(e => e.Name.ToLower().Equals(leaveTypeEdit.Name.ToLower()) 
                           && e.Id != leaveTypeEdit.Id);
    }
}