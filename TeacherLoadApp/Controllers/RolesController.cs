using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeacherLoad.Core.Models;
using TeacherLoadApp.Models;
using TeacherLoadApp.Models.User;

namespace TeacherLoadApp.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IActionResult Index() => View("Roles",_roleManager.Roles.ToList());

        public IActionResult Create() => View("CreateRole");

        [HttpPost]
        public async Task<IActionResult> Create(RoleVM role)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(role.RoleName));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }            
            return View("CreateRole", model: role);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                await _roleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");
        }

        public IActionResult UserList() => View(_userManager.Users.ToList());

        public async Task<IActionResult> Edit(string userId)
        {
            // получаем пользователя
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }           
            var model = new UserWithRoleVM()
            {
                UserId = user.Id,
                UserName = user.UserName,
                UserRoles = await _userManager.GetRolesAsync(user),
                AllRoles = _roleManager.Roles.ToList()
            };
            return View(model);
        }
    }
}