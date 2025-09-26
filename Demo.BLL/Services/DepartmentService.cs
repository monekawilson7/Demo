using Demo.BLL.DataTransferObjects;
using Demo.DAL.Repositories;

public class DepartmentService(IUnitOfWork unitOfWork) : IDepartmentService
{
    public int Add(DepartmentRequest request)
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
        return unitOfWork.SaveChanges();
    }

    public bool Delete(int id)
    {

        var department = unitOfWork.Departments.GetById(id);
        if (department is null) 
        return false;
        unitOfWork.Departments.Delete(department);
        return unitOfWork.SaveChanges() > 0;
    }

    public IEnumerable<DepartmentResponse> GetAll()
    {
        return unitOfWork.Departments.GetAll().Select(x => x.ToResponse());
    }

    public DepartmentDetailsResponse? GetById(int id)
    {
        return unitOfWork.Departments.GetById(id)?.ToDetailsResponse();
    }

    public int Update(DepartmentUpdateRequest request)
    {
        var department = unitOfWork.Departments.GetById(request.Id);
        if (department == null)
            return 0; 

        department.Name = request.Name;
        department.Code = request.Code;
        department.Description = request.Description;
        department.CreatedAt = request.CreatedAt;
        department.LastModifiedOn = DateTime.Now;

         unitOfWork.Departments.Update(department);
        return unitOfWork.SaveChanges();
    }
}
