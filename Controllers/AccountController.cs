using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppoinmentScheduling.Models;
using AppoinmentScheduling.Models.ViewModels;
using AppoinmentScheduling.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;



// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppoinmentScheduling.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;



        public AccountController(ApplicationDbContext db, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
           
            _roleManager = roleManager;
            _signInManager = signInManager;

        }
        // GET: /<controller>/

        public IActionResult Login()
        {
            return View();



        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.password, model.RememberMe, false);
                if(result.Succeeded)
                {
                    return RedirectToAction("index", "Home");

                }
                ModelState.AddModelError("", "Invalid Login Attempt");
            }
            return View();



        }


        public async Task<IActionResult> Register()
        {
            if(!_roleManager.RoleExistsAsync(Helper.Admin).GetAwaiter().GetResult())
            {
               await _roleManager.CreateAsync(new IdentityRole(Helper.Admin));

                await _roleManager.CreateAsync(new IdentityRole(Helper.Doctor));

                await _roleManager.CreateAsync(new IdentityRole(Helper.Patient));

               
            }

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Name = model.Name
                     
                };
                var Result =await _userManager.CreateAsync(user, model.Password);
                if(Result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, model.RoleName);
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Index", "Home");
                }
                foreach(var error in Result.Errors)
                {
                    ModelState.AddModelError("", error.Description);

                }
            }
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Logoff()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login", "Account");

        }
    }
}

