using AutoMapper;
using Demo.BLL.DataTransferObjects.Employee;
using Demo.BLL.Services;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing.Text;

namespace Demo.PL.Controllers;

public class EmployeesController(IEmployeeService Service,
    ILogger<EmployeesController> logger,
    IWebHostEnvironment env,
    IMapper mapper,
    IDepartmentService departmentService) : Controller
{
    [HttpGet]
    public IActionResult Index(string? searchValue)
    {
        if (string.IsNullOrWhiteSpace(searchValue))
            return View(Service.GetAll());
        return View(Service.GetAll(searchValue));

        //// EmployeeService.GetAll();
        //var Employees = Service.GetAll();
        //ViewBag.Message = "Hello from Employees Index";
        //return View(Employees);
    }

    #region Create
    [HttpGet]
    public IActionResult Create()
    {
        var departments = departmentService.GetAll();
        var selectList = new SelectList(departments, "Id", "Name");
        ViewBag.Departments = selectList;
        return View();
    }
    [HttpPost]
    public IActionResult Create(EmployeeRequest request)
    {
        if (!ModelState.IsValid)
            return View(request);

        try
        {
            var result = Service.Add(request);
            // throw new Exception("Test Error");
            if (result > 0)
           
                TempData["Message"] = $"Employee {request.Name} Added Successfully";
            else
                TempData["Message"] = $"Can't Add Employee {request.Name}";
                return RedirectToAction(nameof(Index));


            //ModelState.AddModelError(string.Empty, "Can't Add Employee now");
        }
        catch (Exception ex)
        {
            //Dev=> Display 
            // prod => log
            if (env.IsDevelopment())
                ModelState.AddModelError(string.Empty, ex.Message);
            else
                logger.LogError(ex.Message);
        }

        return View(request);

    }
    #endregion

    #region Details
    [HttpGet]
    public IActionResult Details(int? id)
    {
        EmployeeDetailsResponse? Employee;
        (bool flowControl, IActionResult value) = ValidateEmployeeIdAndFetch(id, out Employee);
        if (!flowControl)
            return value;

        return View(Employee);
    }

    #endregion

    #region Edit 
    [HttpGet]
    public IActionResult Edit(int? id)
    {
        //EmployeeDetailsResponse? Employee;
        //  (bool flowControl , IActionResult value) = ValidateEmployeeIdAndFetch(id, out Employee);
        //  if (!flowControl)
        //      return value;

        //  return View(mapper.Map<EmployeeUpdateRequest>(Employee));

        if (!id.HasValue)
            return BadRequest();
        var employee = Service.GetById(id.Value);
        if (employee == null)
            return NotFound();
        var departments = departmentService.GetAll();
        var selectList = new SelectList(departments, "Id", "Name");
        ViewBag.Departments = selectList;
        return View(mapper.Map<EmployeeUpdateRequest>(employee));
    }

    [HttpPost]
    public IActionResult Edit([FromRoute] int? id, EmployeeUpdateRequest request)
    {
        if (!id.HasValue)
            return BadRequest();
        if (id.Value != request.Id)
            return BadRequest();
        if (!ModelState.IsValid)
            return View(request);
        try
        {
            var result = Service.Update(request);
            if (result > 0)
                return RedirectToAction(nameof(Index));
            ModelState.AddModelError(string.Empty, "Can't Update Employee now");
        }
        catch (Exception ex)
        {
            //Dev=> Display 
            // prod => log
            if (env.IsDevelopment())
                ModelState.AddModelError(string.Empty, ex.Message);
            else
                logger.LogError(ex.Message);
        }
        return View(request);
    }
    #endregion

    #region Delete
    [HttpGet]
    public IActionResult Delete(int? id)
    {
        EmployeeDetailsResponse? Employee;
        (bool flowControl, IActionResult value) = ValidateEmployeeIdAndFetch(id, out Employee);
        if (!flowControl)
            return value;
        return View(Employee);
    }
    [HttpPost, ActionName("Delete")]
    public IActionResult ConfermDelete(int? id)
    {
        if (!id.HasValue)
            return BadRequest();
        try
        {
            var isDeleted = Service.Delete(id.Value);
            if (isDeleted)
                return RedirectToAction(nameof(Index));
            ModelState.AddModelError(string.Empty, "Can't Delete Employee now");
        }
        catch (Exception ex)
        {
            //Dev=> Display 
            // prod => log
            if (env.IsDevelopment())
                ModelState.AddModelError(string.Empty, ex.Message);
            else
                logger.LogError(ex.Message);
        }
        var Employee = Service.GetById(id.Value);
        if (Employee == null)
            return NotFound();
        return View(Employee);
    }

    #endregion

    //Helpers 
    private (bool flowControl, IActionResult value) ValidateEmployeeIdAndFetch(int? id, out EmployeeDetailsResponse? employee)
    {

        if (!id.HasValue)
        {
            employee = default;
            return (flowControl: false, value: BadRequest());
        }
        employee = Service.GetById(id.Value);
        if (employee == null)
            return (flowControl: false, value: NotFound());
        return (flowControl: true, value: null);
    }
    
}