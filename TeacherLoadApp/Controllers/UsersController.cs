using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeacherLoad.Core.DataInterfaces;
using TeacherLoad.Core.Models;
using TeacherLoad.Data.Service;
using TeacherLoadApp.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace TeacherLoadApp.Controllers
{   
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;       

        public UsersController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;            
        }
        [HttpGet]       
        public IActionResult Register()
        {
            UserWithRoleViewModel model = new UserWithRoleViewModel
            {               
                AllRoles = _roleManager.Roles.ToList()
            };
            return View("CreateUser",model);
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserWithRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser {  UserName = model.UserName };
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    //AuthenticationProperties authProp = new AuthenticationProperties();
                    //authProp.ExpiresUtc = DateTime.UtcNow.AddMinutes(1);
                    //authProp.IsPersistent = true;
                    //await _signInManager.SignInAsync(user, authProp);
                    // получем список ролей пользователя
                    var userRoles = await _userManager.GetRolesAsync(user);
                    // получаем все роли
                    var allRoles = _roleManager.Roles.ToList();
                    // получаем список ролей, которые были добавлены
                    var addedRoles = model.UserRoles.Except(userRoles);                   

                    // получаем роли, которые были удалены
                    var removedRoles = userRoles.Except(model.UserRoles);
                    
                    await _userManager.AddToRolesAsync(user, addedRoles);

                    await _userManager.RemoveFromRolesAsync(user, removedRoles);
                    return RedirectToAction("UsersList");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    // проверяем, принадлежит ли URL приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logoff()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        
        public ActionResult UsersList()
        {       
            var usersWithRole = new List<UserWithRoleViewModel>();
            
            foreach(ApplicationUser user in _userManager.Users.ToList())
            {
                usersWithRole.Add(new UserWithRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    UserRoles = _userManager.GetRolesAsync(user).Result                    
                });
            }
            return View("UsersList", usersWithRole);
        }

        public async Task<ActionResult> Edit(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            UserWithRoleViewModel model = new UserWithRoleViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                UserRoles = _userManager.GetRolesAsync(user).Result,
                AllRoles = _roleManager.Roles.ToList()
            };
            return View("EditUser",model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(UserWithRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByIdAsync(model.UserId);
                if (user != null)
                {
                    user.UserName = model.UserName;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        var userRoles = await _userManager.GetRolesAsync(user);
                        // получаем все роли
                        var allRoles = _roleManager.Roles.ToList();
                        // получаем список ролей, которые были добавлены
                        var addedRoles = model.UserRoles.Except(userRoles);
                        // получаем роли, которые были удалены
                        var removedRoles = userRoles.Except(model.UserRoles);

                        await _userManager.AddToRolesAsync(user, addedRoles);

                        await _userManager.RemoveFromRolesAsync(user, removedRoles);
                        return RedirectToAction("UsersList");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }

        /*[HttpPost]*/
        public async Task<ActionResult> Delete(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("UsersList");
        }
    }
}