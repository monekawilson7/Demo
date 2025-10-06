using Demo.DAL.Entities;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Demo.PL.Controllers;

public class AccountController (UserManager<ApplicationUsers> userManager,
    SignInManager<ApplicationUsers> signInManager )
    : Controller
{
    #region Register
    [HttpGet]
    public IActionResult Register()
    {
        
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if(!ModelState.IsValid)
            return View(model);

        var user = new ApplicationUsers
        {
            FristName = model.FirstName,
            LastName = model.LastName,
            UserName = model.UserName,
            Email = model.Email
        };
        var result = await userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
            return RedirectToAction("Login");
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
        return View(model);
    }
    #endregion

    #region Login 
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if(!ModelState.IsValid)
            return View(model);

        var user = await userManager.FindByEmailAsync(model.Email);
        if (user != null )
            {
            if (await userManager.CheckPasswordAsync(user, model.Password))
            {
               var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                if(result.Succeeded)
                return RedirectToAction("Index", "Home");
            }
        }
        ModelState.AddModelError(string.Empty, "Invalid Email or Password");
        return View(model);
    }
    #endregion

    #region Logout
    public async Task<IActionResult> Logout()
    {
       await signInManager.SignOutAsync();
        return RedirectToAction("Index","Home");
    }
    #endregion
}
