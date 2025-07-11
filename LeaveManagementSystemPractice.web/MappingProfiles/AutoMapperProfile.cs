using AutoMapper;
using LeaveManagementSystemPractice.web.Data.Entities;
using LeaveManagementSystemPractice.web.Models.LeaveTypes;
using LeaveManagementSystemPractice.web.Models.Periods;

namespace LeaveManagementSystemPractice.web.MappingProfiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<LeaveType, LeaveTypeReadOnlyVM>();
        CreateMap<LeaveTypeCreateVM, LeaveType>();
        CreateMap<LeaveTypeEditVM, LeaveType>().ReverseMap();
        
        CreateMap<Period, PeriodReadOnlyVM>();
        CreateMap<PeriodCreateVM, Period>();
        CreateMap<PeriodEditVM, Period>().ReverseMap();
    }
}