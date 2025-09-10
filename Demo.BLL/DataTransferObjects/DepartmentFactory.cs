using Azure.Core;
using Demo.DAL.Entities;
using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.DataTransferObjects
{
    internal static class DepartmentFactory
    {
        public static DepartmentResponse ToResponse(this DAL.Entities.Department department) => new()
        {
            Id = department.ID,
            Name = department.Name,
            Description = department.Description,
            CreatedAt = DateOnly.FromDateTime(department.CreatedOn),
            Code = department.Code,
        };
        public static DepartmentDetailsResponse ToDetailsResponse(this DAL.Entities.Department department) => new()
        {
            Id = department.ID,
            Name = department.Name,
            Description = department.Description,
            CreatedBy = department.CreatedBy,
            CreatedOn = department.CreatedOn,
            IsDeleted = department.IsDeleted,
            Code = department.Code,
            LastModifiedBy = department.LastModifiedBy,
            LastModifiedOn = department.LastModifiedOn,
            CreatedAt = department.CreatedAt,
        };
        public static DAL.Entities.Department ToEntity(this DepartmentRequest department)
        {
            return new()
            {
                Code = department.Code,
                CreatedAt = department.CreatedAt,
                Description = department.Description,
            };
        }
            public static DAL.Entities.Department ToEntity(this DepartmentUpdateRequest departmentRequest)
        {
            return new()
            {
                ID = departmentRequest.Id,
                Name = departmentRequest.Name,
                Code = departmentRequest.Code,
                Description = departmentRequest.Description,
                CreatedAt = departmentRequest.CreatedAt,
            };
        }

        public static DepartmentRequest ToRequest(this DepartmentUpdateRequest departmentRequest)
        {
            return new()
            {
                Name = departmentRequest.Name,
                Code = departmentRequest.Code,
                Description = departmentRequest.Description,
            };
        }
    }
}
