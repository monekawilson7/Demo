using Demo.BLL.DataTransferObjects.Employee;
using System.Buffers;

namespace Demo.BLL.Services;
public interface IEmployeeService
{
    Task<EmployeeDetailsResponse> GetByIdAsync(int id);
    Task<IEnumerable<EmployeeResponse>>GetAllAsync();
    Task<IEnumerable<EmployeeResponse>>GetAllAsync(string searchValue);
    Task<int> AddAsync(EmployeeRequest request);
    Task<int> UpdateAsync(EmployeeUpdateRequest request);
    Task<bool> DeleteAsync (int id);

}
