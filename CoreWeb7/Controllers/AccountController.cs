using CoreWeb7.Data;
using CoreWeb7.Models.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreWeb7.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View(); 
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterUserModel model)
        {
            var newUser = new AppUser
            {
                Fullname = model.Fullname,
                Email = model.Email,
                UserName = model.Username
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("username", "XXXXX");
                ModelState.AddModelError("", "YYYYY");

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                ModelState.AddModelError("username", "Kullanıcı adı ve/veya şifre yanlış");
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("password", "Kullanıcı adı ve/veya şifre yanlış");
                ModelState.AddModelError("", "Kullanıcı adı ve/veya şifre yanlış");
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task Logout() => await _signInManager.SignOutAsync();
    }
}
