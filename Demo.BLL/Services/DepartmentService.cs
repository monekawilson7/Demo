using Demo.BLL.DataTransferObjects;
using Demo.DAL.Entities;
using Demo.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DepartmentService(IReopsitory<Department> departmentRepository) : IDepartmentService
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

        return departmentRepository.Add(department);
    }

    public bool Delete(int id)
    {

        var department = departmentRepository.GetById(id);
        if (department is null) 
        return false;
        var result = departmentRepository.Delete(department);
        return result > 0;
    }

    public IEnumerable<DepartmentResponse> GetAll()
    {
        return departmentRepository.GetAll().Select(x => x.ToResponse());
    }

    public DepartmentDetailsResponse? GetById(int id)
    {
        return departmentRepository.GetById(id)?.ToDetailsResponse();
    }

    public int Update(DepartmentUpdateRequest request)
    {
        var department = departmentRepository.GetById(request.Id);
        if (department == null)
            return 0; 

        department.Name = request.Name;
        department.Code = request.Code;
        department.Description = request.Description;
        department.CreatedAt = request.CreatedAt;
        department.LastModifiedOn = DateTime.Now;

        return departmentRepository.Update(department);
    }
}
