using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystemPractice.web.Models.Periods;

public class PeriodReadOnlyVM
{
    public int Id { get; set; }

    [Display(Name = "Period Name")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Start Date")]
    public DateOnly StartDate { get; set; }

    [Display(Name = "End Date")]
    public DateOnly EndDate { get; set; }
}