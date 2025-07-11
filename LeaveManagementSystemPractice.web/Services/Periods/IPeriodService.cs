using LeaveManagementSystemPractice.web.Models.Periods;

namespace LeaveManagementSystemPractice.web.Services.Periods;

public interface IPeriodService
{
    Task<List<PeriodReadOnlyVM>> GetAllAsync();
    Task<T?> GetByIdAsync<T>(int id) where T : class;
    Task CreateAsync(PeriodCreateVM periodCreate);
    Task<bool>  CheckIfPeriodNameExists(string name);
    Task<bool>  CheckIfPeriodNameExists(PeriodEditVM periodEdit);
    Task EditAsync(PeriodEditVM model);
    bool PeriodExists(int id);
    Task RemoveAsync(int id);
}
