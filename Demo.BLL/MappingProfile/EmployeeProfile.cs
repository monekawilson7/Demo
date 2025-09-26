using AutoMapper;
using Demo.BLL.DataTransferObjects.Employee;

namespace Demo.BLL.MappingProfile;
public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<EmployeeRequest, Employee>();
        CreateMap<EmployeeUpdateRequest, Employee>();

        CreateMap<Employee, EmployeeResponse>()
            .ForMember(d=>d.Department, o=>o.MapFrom(s=> s.Department.Name));

        CreateMap<Employee, EmployeeDetailsResponse>()
            .ForMember(d => d.DepartmentId, o => o.MapFrom(s => s.Department.Id))
            .ForMember(d => d.Department, o => o.MapFrom(s => s.Department.Name));

        CreateMap<EmployeeDetailsResponse, EmployeeUpdateRequest>();
        CreateMap<EmployeeUpdateRequest, EmployeeRequest>();
    }
}
