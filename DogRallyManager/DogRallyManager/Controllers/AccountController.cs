using DogRallyManager.Entities;
using DogRallyManager.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DogRallyManager.Controllers
{
    //private readonly SignInManager<RallyUser> _signInManager;

    //[BindProperty]
    //public LoginUserVM LoginUserVM { get; set; }

    //public LoginModel(SignInManager<RallyUser> signInManager)
    //{
    //    _signInManager = signInManager;
    //}
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
        public IActionResult Index()
        {

            return View("login");
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

        //[HttpGet]
        //public  IActionResult Login()
        //{
        //    return View("Login");
        //}

        [HttpPost]
        public async Task<IActionResult> OnPostLogoutAsync()
        {
            // ASP NET will handle all the signout functions for us
            await _signInManager.SignOutAsync();

            return RedirectToPage("Login");

        }

        [HttpPost]
        public IActionResult OnPostDontLogout()
        {
            return View("YourPage");
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
                    return View("YourPage");
                }

                ModelState.AddModelError("", "Username or Password is incorrect.");
            }

            return View(LoginUserVM); // Return the Login view
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(RegisterUserVM);
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
}
