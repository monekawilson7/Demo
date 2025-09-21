using AutoMapper;
using Demo.BLL.DataTransferObjects.Employee;

namespace Demo.BLL.MappingProfile;
public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<EmployeeRequest, Employee>();
        CreateMap<EmployeeUpdateRequest, Employee>();

        CreateMap<Employee, EmployeeResponse>();
        CreateMap<Employee, EmployeeDetailsResponse>();
        CreateMap<EmployeeDetailsResponse, EmployeeUpdateRequest>();
        CreateMap<EmployeeUpdateRequest, EmployeeRequest>();
    }
}
