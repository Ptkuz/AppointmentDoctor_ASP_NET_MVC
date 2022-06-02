using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DoctorsAppointments.Models.DataBase;
using DoctorsAppointments.Models.ViewModels.UserViewModel;
using DoctorsAppointments.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace DoctorsAppointments.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationContext db;
        public static Guid idUser { get; set; }

        public AccountController(ApplicationContext context) 
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Login() 
        {
            return View();
        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model) 
        {
            if (ModelState.IsValid) 
            {
                User? user = await db.Users.Include(u=>u.Role).FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null) 
                {
                    await Authenticate(user);
                    
                    return RedirectToAction("Index", "Home");

                }
                ModelState.AddModelError("","Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register() 
        {
            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model) 
        {
            if (ModelState.IsValid) 
            {
                User? user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    
                    user = new User {Id = Guid.NewGuid(), Email = model.Email, Password = model.Password };
                    Role? userRole = await db.Roles.FirstOrDefaultAsync(r => r.Name == "patient");

                    if (userRole != null)
                        user.Role = userRole;

                    db.Users.Add(user);

                    Patient patient = new Patient()
                    {
                        FirstName = "Не задано",
                        Patronymic = "Не задано",
                        LastName = "Не задано",
                        DateAge = default,
                        Genders = Genders.male,
                        Passport = "Не задано",
                        Polis = "Не задано",
                        Snils = "Не задано",
                        UserKey = user.Id
                    };
                   

                    
                    db.Patients.Add(patient);
                    await db.SaveChangesAsync();

                    await Authenticate(user);

                    return RedirectToAction("Index", "Home");
                }
                else 
                {
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                }
            }
            return View(model);
        }

        private async Task Authenticate(User user) 
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            idUser = user.Id;
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout() 
        {
            idUser = Guid.Empty;
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

    }
}
