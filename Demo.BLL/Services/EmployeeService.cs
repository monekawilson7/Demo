using AutoMapper;
using AutoMapper.QueryableExtensions;
using Demo.BLL.DataTransferObjects.Employee;
using Demo.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Demo.BLL.Services;
public class EmployeeService (IUnitOfWork unitOfWork,
    IMapper mapper,
    IDocumentService documentService
    ) : IEmployeeService

{
    public  async Task<int> AddAsync(EmployeeRequest request)
    {
        //s=> D
        // R => E
        var employee = mapper.Map<EmployeeRequest, Employee>(request);
        if (request.Image is not null && request.Image.Length > 0)
        {
            var imageName =  documentService.UploadAsync(request.Image, "Images");
            employee.Image = await imageName;
        }
        unitOfWork.Employees.Add(employee);
        return await unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {

        var employee = await unitOfWork.Employees.GetByIdAsync(id);
        if (employee is null)
            return false;
        unitOfWork.Employees.Delete(employee);
        var result = await unitOfWork.SaveChangesAsync();
        if (result > 0 && employee.Image is not null)
        {
            documentService.Delete(employee.Image, "Images");
            return true;
        }
        return false;
    }

    public async Task<IEnumerable<EmployeeResponse>> GetAllAsync()
    {
        var employees = await unitOfWork.Employees.GetAllAsync
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

    public async Task<IEnumerable<EmployeeResponse>> GetAllAsync(string searchValue)
    {
        return await unitOfWork.Employees.GetAllAsQuerable()
            .Where(e => e.Name.Contains(searchValue))
            .ProjectTo<EmployeeResponse>(mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<EmployeeDetailsResponse?> GetByIdAsync(int id)
    {
        var employee = await unitOfWork.Employees.GetByIdAsync(id);
        return mapper.Map<EmployeeDetailsResponse>(employee);
    }

    public async Task<int> UpdateAsync(EmployeeUpdateRequest request)
    {
        var employee = await unitOfWork.Employees.GetByIdAsync(request.Id);
        if (employee == null) return 0;
        mapper.Map(request, employee);

        if (request.Image is not null && request.Image.Length > 0)
        {
            if (!string.IsNullOrEmpty(employee.Image))
                documentService.Delete(employee.Image, "Images");
            var imageName = await documentService.UploadAsync(request.Image, "Images");
            employee.Image = imageName;
        }

        unitOfWork.Employees.Update(employee);
        return await unitOfWork.SaveChangesAsync();
    }

}
