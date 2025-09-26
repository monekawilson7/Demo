using Demo.BLL.DataTransferObjects.Employee;
using System.Buffers;

namespace Demo.BLL.Services;
public interface IEmployeeService
{
    EmployeeDetailsResponse GetById(int id);
    IEnumerable<EmployeeResponse>GetAll();
    IEnumerable<EmployeeResponse>GetAll(string searchValue);
    int Add(EmployeeRequest request);
    int Update(EmployeeUpdateRequest request);
    bool Delete(int id);

}
