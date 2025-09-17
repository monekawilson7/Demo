using Demo.BLL.DataTransferObjects.Employee;

namespace Demo.BLL.Services;
public interface IEmployeeService
{
    EmployeeDetailsResponse GetById(int id);
    IEnumerable<EmployeeResponse>GetAll();
    int Add(EmployeeRequest request);
    int Update(EmployeeUpdateRequest request);
    bool Delete(int id);

}
