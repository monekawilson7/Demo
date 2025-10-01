using Demo.BLL.DataTransferObjects;
using Demo.DAL.Repositories;
using System.Threading.Tasks;

public class DepartmentService(IUnitOfWork unitOfWork) : IDepartmentService
{
    public async Task<int> AddAsync(DepartmentRequest request)
    {
        //Mapping 
        //Manual
        //var department = new Department{ 
        //Code = request.Code,
        //CreatedAt = request.CreatedAt,
        //Description = request.Description,
        //};
        //var department = DepartmentFactory.ToEntity(request);
        var department = request.ToEntity();
        // AutoMapper
        // Mapster

         unitOfWork.Departments.Add(department);
        return await unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {

        var department = await unitOfWork.Departments.GetByIdAsync(id);
        if (department is null) 
        return false;
        unitOfWork.Departments.Delete( department);
        return (await unitOfWork.SaveChangesAsync()) > 0;
    }

    public async Task<IEnumerable<DepartmentResponse>> GetAllAsync()
    {
        return (await unitOfWork.Departments.GetAllAsync()).Select(x => x.ToResponse());
    }

    public async Task<DepartmentDetailsResponse?> GetByIdAsync(int id)
    {
        return (await unitOfWork.Departments.GetByIdAsync(id))?.ToDetailsResponse();
    }

    public async Task<int> UpdateAsync(DepartmentUpdateRequest request)
    {
        var department = await unitOfWork.Departments.GetByIdAsync(request.Id);
        if (department == null)
            return 0; 

        department.Name = request.Name;
        department.Code = request.Code;
        department.Description = request.Description;
        department.CreatedAt = request.CreatedAt;
        department.LastModifiedOn = DateTime.Now;

         unitOfWork.Departments.Update(department);
        return await unitOfWork.SaveChangesAsync();
    }
}
