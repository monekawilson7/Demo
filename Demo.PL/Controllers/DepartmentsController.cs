global using Demo.BLL.DataTransferObjects;
global using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class DepartmentsController(IDepartmentService departmentService,
    ILogger<DepartmentsController> logger,
    IWebHostEnvironment env) : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        // departmentService.GetAll();
        var departments = departmentService.GetAllAsync();
        return View(departments);
    }

    #region Create
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(DepartmentRequest request)
    {
        if (!ModelState.IsValid)
            return View(request);

        try
        {
            var result = await departmentService.AddAsync(request);
            // throw new Exception("Test Error");
            if (result > 0)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "Can't Add Department now");
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
        if (!id.HasValue)
            return BadRequest();
        var department = departmentService.GetByIdAsync(id.Value);
        if (department == null)
            return NotFound();
        return View(department);
    }

    #endregion

    #region Edit 
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (!id.HasValue)
            return BadRequest();
        var department = await departmentService.GetByIdAsync(id.Value);
        if (department == null)
            return NotFound();
        return View(department.ToUpdateRequest(id.Value));
    }

    [HttpPost]
    public async Task<IActionResult> Edit([FromRoute]int? id,DepartmentUpdateRequest request)
    {
        if (!id.HasValue)
            return BadRequest();
        if (id.Value != request.Id)
            return BadRequest();
        if (!ModelState.IsValid)
            return View(request);
        try
        {
            var result = await departmentService.UpdateAsync(request);
            if (result > 0)
                return RedirectToAction(nameof(Index));
            ModelState.AddModelError(string.Empty, "Can't Update Department now");
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
        if (!id.HasValue)
            return BadRequest();
        var department = departmentService.GetByIdAsync(id.Value);
        if (department == null)
            return NotFound();
        return View(department);
    }
        [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> ConfermDelete(int? id)
    {
        if (!id.HasValue)
            return BadRequest();
        try
        {
            var isDeleted = await departmentService.DeleteAsync(id.Value);
            if (isDeleted)
                return RedirectToAction(nameof(Index));
            ModelState.AddModelError(string.Empty, "Can't Delete Department now");
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
        var department = departmentService.GetByIdAsync(id.Value);
        if (department == null)
            return NotFound();
        return View(department);

        #endregion

    }
}