using AutoMapper;
using LeaveManagementSystemPractice.web.Data.Entities;
using LeaveManagementSystemPractice.web.Models.LeaveTypes;

namespace LeaveManagementSystemPractice.web.MappingProfiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<LeaveType, LeaveTypeReadOnlyVM>();
        CreateMap<LeaveTypeCreateVM, LeaveType>();
        CreateMap<LeaveTypeEditVM, LeaveType>().ReverseMap();
    }
}