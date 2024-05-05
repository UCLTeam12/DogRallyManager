using DogRallyManager.Entities;
using DogRallyManager.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DogRallyManager.Controllers;

public class AccountController : Controller
{
    private readonly SignInManager<RallyUser> _signInManager;
    private readonly UserManager<RallyUser> _userManager;
    public RegisterUserVM RegisterUserVM = new();
    public LoginUserVM LoginUserVM = new();

    public AccountController(
        SignInManager<RallyUser> signManager,
        UserManager<RallyUser> userManager
    )
    {
        _signInManager = signManager;
        _userManager = userManager;
    }

    // POSSIBLE TO-DO:
    // Concider changing name of Index method to login, and returning view(). 
    // This will require a rerouting on paths 
    public async Task<IActionResult> Index()
    {
        await OnPostLogoutAsync();
        return View("Login");
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View(RegisterUserVM);
    }

    public async Task<IActionResult> Logout(string choice)
    {
        if (choice == "yes")
        {
            await OnPostLogoutAsync();
            return RedirectToAction("Index"); // Redirect to home after logout
        }

        if (choice == "no")
        {
            return OnPostDontLogout();
        }
        else
        {
            return View("Logout");
        }
    }

    [Authorize]
    public IActionResult YourPage()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> OnPostLogoutAsync()
    {
        // ASP NET will handle all the signout functions for us
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
                LoginUserVM.Email, LoginUserVM.Password, LoginUserVM.RememberMe, false);

            if (identityResult.Succeeded)
            {
                return RedirectToAction("YourPage");
            }

            ModelState.AddModelError("", "Username or Password is incorrect.");
        }

        return View(); // Return the Login view
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
                return View("Login");
            }

            // If errors in above print descriptions
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        // her kan vi lave en fejl-view side og printe nogle beskeder ud, eventuelt
        return RedirectToAction("Index", "Home");
    }
}