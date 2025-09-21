global using Demo.BLL.DataTransferObjects;
global using Microsoft.AspNetCore.Mvc;

public class DepartmentsController(IDepartmentService departmentService,
    ILogger<DepartmentsController> logger,
    IWebHostEnvironment env) : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        // departmentService.GetAll();
        var departments = departmentService.GetAll();
        return View(departments);
    }

    #region Create
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(DepartmentRequest request)
    {
        if (!ModelState.IsValid)
            return View(request);

        try
        {
            var result = departmentService.Add(request);
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
        var department = departmentService.GetById(id.Value);
        if (department == null)
            return NotFound();
        return View(department);
    }

    #endregion

    #region Edit 
    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (!id.HasValue)
            return BadRequest();
        var department = departmentService.GetById(id.Value);
        if (department == null)
            return NotFound();
        return View(department.ToUpdateRequest(id.Value));
    }

    [HttpPost]
    public IActionResult Edit([FromRoute]int? id,DepartmentUpdateRequest request)
    {
        if (!id.HasValue)
            return BadRequest();
        if (id.Value != request.Id)
            return BadRequest();
        if (!ModelState.IsValid)
            return View(request);
        try
        {
            var result = departmentService.Update(request);
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
        var department = departmentService.GetById(id.Value);
        if (department == null)
            return NotFound();
        return View(department);
    }
        [HttpPost, ActionName("Delete")]
    public IActionResult ConfermDelete(int? id)
    {
        if (!id.HasValue)
            return BadRequest();
        try
        {
            var isDeleted = departmentService.Delete(id.Value);
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
        var department = departmentService.GetById(id.Value);
        if (department == null)
            return NotFound();
        return View(department);

        #endregion

    }
}