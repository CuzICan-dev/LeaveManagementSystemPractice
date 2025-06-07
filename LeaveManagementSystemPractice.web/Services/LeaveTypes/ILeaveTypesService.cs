namespace LeaveManagementSystemPractice.web.Services.LeaveTypes;

public interface ILeaveTypesService
{
    Task<List<LeaveTypeReadOnlyVM>> GetAllAsync();
    Task<T> GetTypeByIdAsync<T>(int id) where T : class;
    Task CreateAsync(LeaveTypeCreateVM leaveTypeCreate);
    Task<bool> CheckIfLeaveTypeNameExists(string name);
    Task<bool> CheckIfLeaveTypeNameExists(LeaveTypeEditVM leaveTypeEdit);
    Task EditAsync(LeaveTypeEditVM leaveTypeEdit);
    bool LeaveTypeExists(int id);
    Task RemoveAsync(int id);
}