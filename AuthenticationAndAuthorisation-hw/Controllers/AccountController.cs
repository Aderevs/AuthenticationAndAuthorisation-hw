using AuthenticationAndAuthorization_hw.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AuthenticationAndAuthorization_hw.Logic;
using AuthenticationAndAuthorization_hw.Logic.Account;

namespace AuthenticationAndAuthorization_hw.Controllers
{
    public class AccountController : Controller
    {
        private PseudoDatabase _db;

        public AccountController(PseudoDatabase db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(RegisterBindingModel model)
        {
            if (ModelState.IsValid)
            {
                bool loginIsUnique = !_db.Users.Any(u => u.Login == model.Login);
                if (loginIsUnique)
                {
                    var newUser = new User
                    {
                        Id = Guid.NewGuid(),
                        Login = model.Login,
                        Salt = Guid.NewGuid(),
                    };
                    newUser.PasswordHash = PasswordHasher.HashPassword(model.Password + newUser.Salt.ToString());
                    _db.Users.Add(newUser);
                    await SignInAsync(newUser);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(nameof(model.Login), "This Login is already in use :(");
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }

        }


        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(AuthorizeBindingModel model)
        {

            if (ModelState.IsValid)
            {
                var userOrNull = _db.Users.FirstOrDefault(x => x.Login == model.Login);
                if (userOrNull is User user)
                {
                    var isCorrectPassword = PasswordHasher.IsCorrectPassword(user, model.Password);
                    if (isCorrectPassword)
                    {
                        await SignInAsync(user);
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "Wrong login or password");
                return View(model);
            }
            else
            {
                return View(model);
            }
        }

        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        private async Task SignInAsync(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role, "User"),
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(claimsPrincipal);
        }
    }
}
