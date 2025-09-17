using AutoMapper;
using Demo.BLL.DataTransferObjects.Employee;
using Demo.DAL.Repositories;

namespace Demo.BLL.Services;
public class EmployeeService (IEmployeeRepositiry repositiry, IMapper mapper) : IEmployeeService
{
    public int Add(EmployeeRequest request)
    {
        //s=> D
        // R => E

        var employee = mapper.Map<EmployeeRequest, Employee>(request);
        return repositiry.Add(employee);
    }

    public bool Delete(int id)
    {

        var employee = repositiry.GetById(id);
        if (employee is null)
            return false;
        var result = repositiry.Delete(employee);
        return result > 0;
    }

    public IEnumerable<EmployeeResponse> GetAll()
    {
        var employees = repositiry.GetAll();
        return mapper.Map<IEnumerable<EmployeeResponse>>(employees);
    }

    public EmployeeDetailsResponse? GetById(int id)
    {
        var employee = repositiry.GetById(id);
        return mapper.Map<EmployeeDetailsResponse>(employee);
    }

    public int Update(EmployeeUpdateRequest employee)
    => repositiry.Update(mapper.Map<Employee>(employee));
}
