using AutoMapper;
using AutoMapper.QueryableExtensions;
using Demo.BLL.DataTransferObjects.Employee;
using Demo.DAL.Repositories;

namespace Demo.BLL.Services;
public class EmployeeService (IUnitOfWork unitOfWork, IMapper mapper) : IEmployeeService
{
    public int Add(EmployeeRequest request)
    {
        //s=> D
        // R => E

        var employee = mapper.Map<EmployeeRequest, Employee>(request);
        return unitOfWork.SaveChanges();
    }

    public bool Delete(int id)
    {

        var employee = unitOfWork.Employees.GetById(id);
        if (employee is null)
            return false;
        unitOfWork.Employees.Delete(employee);
        return unitOfWork.SaveChanges()>0;
    }

    public IEnumerable<EmployeeResponse> GetAll()
    {
        var employees = unitOfWork.Employees.GetAll
            (e=> new EmployeeResponse { 
            Age = e.Age,
            Email = e.Email,
                Gender = e.Gender.ToString(),      
               EmployeeType = e.EmployeeType.ToString(),
            Id = e.Id,
            IsActive = e.IsActive,
            Name = e.Name,
            Salary = e.Salary,
            Department = e.Department.Name
            });
        return employees;
        //return mapper.Map<IEnumerable<EmployeeResponse>>(employees);
    }

    public IEnumerable<EmployeeResponse> GetAll(string searchValue)
    {
        return unitOfWork.Employees.GetAllAsQuerable()
            .Where(e => e.Name.Contains(searchValue))
            .ProjectTo<EmployeeResponse>(mapper.ConfigurationProvider).ToList();
    }

    public EmployeeDetailsResponse? GetById(int id)
    {
        var employee = unitOfWork.Employees.GetById(id);
        return mapper.Map<EmployeeDetailsResponse>(employee);
    }

    public int Update(EmployeeUpdateRequest request)
    {
        var employee = mapper.Map<Employee>(request);
        unitOfWork.Employees.Update(mapper.Map<Employee>(employee));
        return unitOfWork.SaveChanges();

    }
}
