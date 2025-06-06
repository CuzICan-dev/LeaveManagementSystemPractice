using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystemPractice.web.Data.Entities;

public class LeaveType
{
    public int Id { get; set; }
    [StringLength(150)]
    public string Name { get; set; }
    public int NumberOfDays { get; set; }
}