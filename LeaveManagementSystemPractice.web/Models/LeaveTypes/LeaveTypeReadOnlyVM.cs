using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystemPractice.web.Models.LeaveTypes;

public class LeaveTypeReadOnlyVM
{
    public int Id { get; set; } 
    [Display(Name = "Leave Type Name")]
    public string Name { get; set; } = string.Empty;
    [Display(Name = "Number of Days")]
    public int NumberOfDays { get; set; }
}