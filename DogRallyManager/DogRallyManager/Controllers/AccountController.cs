using DogRallyManager.Entities;
using DogRallyManager.Services;
using DogRallyManager.ViewModels.AccountVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DogRallyManager.Controllers;

public class AccountController : Controller
{
    private readonly SignInManager<RallyUser> _signInManager;
    private readonly UserManager<RallyUser> _userManager;

    public RegisterUserVM _RegisterUserVM;
    public LoginUserVM _LoginUserVM;
    private readonly IDataService _dataService;
    private readonly RoleManager<RallyUserRole> _roleManager;

    public AccountController(
        SignInManager<RallyUser> signManager,
        UserManager<RallyUser> userManager,
        IDataService dataService,
        RoleManager<RallyUserRole> roleManager
        )
    {
        _signInManager = signManager;
        _userManager = userManager;
        _dataService = dataService;
        _roleManager = roleManager;
    }

    public async Task<IActionResult> Index()
    {
        await OnPostLogoutAsync();
        return View("Login");
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    
    public async Task<IActionResult> Logout(string choice)
    {
        if (choice == "yes")
        {
            await OnPostLogoutAsync();
            return RedirectToAction("Index");
        }

        if (choice == "no")
        {
            return OnPostDontLogout();
        }

        return View("Logout");
    }

    [Authorize]
    public IActionResult YourPage()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> OnPostLogoutAsync()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login");
    }

    [HttpPost]
    public IActionResult OnPostDontLogout()
    {
        return RedirectToAction("YourPage");
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginUserVM LoginUserVM)
    {
        if (ModelState.IsValid)
        {
            var identityResult = await _signInManager.PasswordSignInAsync(
                LoginUserVM.UserName, LoginUserVM.Password, LoginUserVM.RememberMe, false);

            if (identityResult.Succeeded)
            {
                return RedirectToAction("YourPage");
            }

            ModelState.AddModelError("", "Username or Password is incorrect.");

            ViewBag.UserNameError = "Wrong association of credentials.";
            
            if (LoginUserVM.UserName.Contains("@"))
            {
                ModelState.AddModelError("",
                    "It seems like you've entered an email address. Please use your username");
                ViewBag.UserNameError = "Dit rally navn, ikke din e-mail addresse.";
            }
        }
        return View(LoginUserVM); 
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUser(RegisterUserVM RegisterUserVM)
    {
        if (ModelState.IsValid)
        {
            // If ModelState is valid, create user
            var user = new RallyUser()
            {
                UserName = RegisterUserVM.UserName,
                Email = RegisterUserVM.Email
            };

            var result = await _userManager.CreateAsync(user, RegisterUserVM.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);

                // If the account has succesfully been created, it is also associated with
                // the general chatroom, where all are a part of, until they choose to leave it.
                await _dataService.AddUserToChatRoomAsync(user.UserName, 1);
                return View("Login");
            }

            // If there are errors detected in the ModelState, we will attach errors to the object that is returned.
            foreach (var error in result.Errors)
            {
                if (error.Description.Contains("username", StringComparison.OrdinalIgnoreCase))
                {
                    ModelState.AddModelError(nameof(RegisterUserVM.UserName), error.Description);
                }
                else if (error.Description.Contains("email", StringComparison.OrdinalIgnoreCase))
                {
                    ModelState.AddModelError(nameof(RegisterUserVM.Email), error.Description);
                }
                else if (error.Description.Contains("password", StringComparison.OrdinalIgnoreCase))
                {
                    ModelState.AddModelError(nameof(RegisterUserVM.Password),
                        "Password must contain: One non alphanumeric character, be atleast 9 characters long and contain atleast one uppercase and one lowercase letter.");
                }
            }
        }
        return View("register", RegisterUserVM);
    }
    
    [HttpGet]
    public async Task<IActionResult> AssignAdmin ()
    {
        var getName = await _userManager.FindByNameAsync(User.Identity.Name);
        if (getName == null)
        {
            return NotFound();
        }

        await _userManager.AddToRoleAsync(getName, "Admin");
        return Ok();
    }
}