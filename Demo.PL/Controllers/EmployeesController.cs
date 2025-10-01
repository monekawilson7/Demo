using AutoMapper;
using Demo.BLL.DataTransferObjects.Employee;
using Demo.BLL.Services;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing.Text;
using System.Threading.Tasks;

namespace Demo.PL.Controllers;

public class EmployeesController(IEmployeeService Service,
    ILogger<EmployeesController> logger,
    IWebHostEnvironment env,
    IMapper mapper,
    IDepartmentService departmentService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string? searchValue)
    {
        if (string.IsNullOrWhiteSpace(searchValue))
            return View(await Service.GetAllAsync());
        return View(await Service.GetAllAsync(searchValue));

        //// EmployeeService.GetAll();
        //var Employees = Service.GetAll();
        //ViewBag.Message = "Hello from Employees Index";
        //return View(Employees);
    }

    #region Create
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var departments = await departmentService.GetAllAsync();
        var selectList = new SelectList(departments, "Id", "Name");
        ViewBag.Departments = selectList;
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(EmployeeRequest request)
    {
        if (!ModelState.IsValid)
            return View(request);

        try
        {
            var result = await Service.AddAsync(request);
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
    public async Task<IActionResult> Details(int? id)
    {
        //EmployeeDetailsResponse? Employee;
        //(bool flowControl, IActionResult value) =await ValidateEmployeeIdAndFetchAsync(id, out Employee);
        //if (!flowControl)
        //    return value;

        //return View(Employee);
        var result = await ValidateEmployeeIdAndFetchAsync(id);
        if (!result.flowControl)
            return result.value;

        return View(result.employee);


    }

    #endregion

    #region Edit 
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        //EmployeeDetailsResponse? Employee;
        //  (bool flowControl , IActionResult value) = ValidateEmployeeIdAndFetch(id, out Employee);
        //  if (!flowControl)
        //      return value;

        //  return View(mapper.Map<EmployeeUpdateRequest>(Employee));

        if (!id.HasValue)
            return BadRequest();
        var employee = await Service.GetByIdAsync(id.Value);
        if (employee == null)
            return NotFound();
        var departments = await departmentService.GetAllAsync();
        var selectList = new SelectList(departments, "Id", "Name");
        ViewBag.Departments = selectList;
        return View(mapper.Map<EmployeeUpdateRequest>(employee));
    }

    [HttpPost]
    public async Task<IActionResult> Edit([FromRoute] int? id, EmployeeUpdateRequest request)
    {
        if (!id.HasValue)
            return BadRequest();
        if (id.Value != request.Id)
            return BadRequest();
        if (!ModelState.IsValid)
            return View(request);
        try
        {
            var result = await Service.UpdateAsync(request);
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
    public async Task<IActionResult> Delete(int? id)
    {
        //EmployeeDetailsResponse? Employee;
        //(bool flowControl, IActionResult value) = await ValidateEmployeeIdAndFetchAsync(id, out Employee);
        //if (!flowControl)
        //    return value;
        //return View(Employee);
        var result = await ValidateEmployeeIdAndFetchAsync(id);
        if (!result.flowControl)
            return result.value;

        return View(result.employee);

    }
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> ConfermDelete(int? id)
    {
        if (!id.HasValue)
            return BadRequest();
        try
        {
            var isDeleted = await Service.DeleteAsync(id.Value);
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
        var Employee = await Service.GetByIdAsync(id.Value);
        if (Employee == null)
            return NotFound();
        return View(Employee);
    }

    #endregion

    //Helpers 
    private async Task<(bool flowControl, IActionResult? value, EmployeeDetailsResponse? employee)>
     ValidateEmployeeIdAndFetchAsync(int? id)
    {
        if (!id.HasValue)
            return (false, BadRequest(), null);

        var employee = await Service.GetByIdAsync(id.Value);
        if (employee == null)
            return (false, NotFound(), null);

        return (true, null, employee);
    }


}