using AutoMapper;
using LeaveManagementSystemPractice.web.Data;
using LeaveManagementSystemPractice.web.Data.Entities;
using LeaveManagementSystemPractice.web.Models.Periods;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystemPractice.web.Services.Periods;

public class PeriodService(ApplicationDbContext context, IMapper mapper) : IPeriodService
{
    public async Task<List<PeriodReadOnlyVM>> GetAllAsync()
    {
        var periods = await context.Periods.ToListAsync();
        return mapper.Map<List<PeriodReadOnlyVM>>(periods);
    }

    public async Task<T?> GetByIdAsync<T>(int id) where T : class
    {
        var period = await context.Periods.FirstOrDefaultAsync(p => p.Id == id);
        return period is null ? null : mapper.Map<T>(period);
    }

    public async Task CreateAsync(PeriodCreateVM periodCreate)
    {
        Period period = mapper.Map<Period>(periodCreate);
        context.Add(period);
        await context.SaveChangesAsync();
    }

    public async Task EditAsync(PeriodEditVM periodEdit)
    {
        var period = mapper.Map<Period>(periodEdit);
        context.Update(period);
        await context.SaveChangesAsync();
    }

    public async Task RemoveAsync(int id)
    {
        var period = await context.Periods.FirstOrDefaultAsync(p => p.Id == id);
        if (period is null) return;

        context.Periods.Remove(period);
        await context.SaveChangesAsync();
    }


    /* ---------- helpers ---------- */

    public bool PeriodExists(int id) => context.Periods.Any(p => p.Id == id);

    public async Task<bool> CheckIfPeriodNameExists(string name)
        => await context.Periods.AnyAsync(p => p.Name.ToLower().Equals(name.ToLower()));

    public async Task<bool> CheckIfPeriodNameExists(PeriodEditVM periodEdit)
        => await context.Periods.AnyAsync(p =>
            p.Name.ToLower().Equals(periodEdit.Name.ToLower()) && p.Id != periodEdit.Id);
}
