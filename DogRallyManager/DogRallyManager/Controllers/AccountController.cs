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

    // POSSIBLE TO-DO: Concider if we want to use ASP NET DI instead of newing up like this.
    public RegisterUserVM _RegisterUserVM;
    public LoginUserVM _LoginUserVM;
    private readonly IDataService _dataService;

    public AccountController(
        SignInManager<RallyUser> signManager,
        UserManager<RallyUser> userManager,
        IDataService dataService
    )
    {
        _signInManager = signManager;
        _userManager = userManager;
        _dataService = dataService;
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
        return View();
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

        return View(LoginUserVM); // Return the Login view
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
                // Add similar checks for other properties
            }
        }

        // Returnerer objektet og viser behæftede fejl som opstod objekt-relateret i forbindelse med registrering.
        return View("register", RegisterUserVM);
    }
}