using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocilaMediaProject.Data;
using SocilaMediaProject.Models;

namespace SocilaMediaProject.Controllers
{
    public class RoleController : Controller
    {
        private readonly MyDBContext _myDBContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleController(MyDBContext myDBContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _myDBContext = myDBContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var roles = _myDBContext.Roles.ToList();
            return View(roles);
        }

        [HttpGet]
        public IActionResult InsertRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> InsertRole(IdentityRole role)
        {
            if (await _roleManager.RoleExistsAsync(role.Name))
            {
                ModelState.AddModelError("", "Name is already Exsites");
                return View("Index", "Role");
            }

            await _roleManager.CreateAsync(new IdentityRole()
            {
                Name = role.Name
            });
            return RedirectToAction("Index", "Role");

        }

        [HttpGet]
        public IActionResult UpdateRole(string Id)
        {
            if (string.IsNullOrEmpty(Id)) return View();
            else
            {
                var user = _myDBContext.Roles.Find(Id);
                return View(user);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(IdentityRole role, string Id)
        {
            var UpRole = _myDBContext.Roles.Find(Id);

            UpRole.Name = role.Name;
            UpRole.NormalizedName = role.Name.ToUpper();
            await _roleManager.UpdateAsync(UpRole);
            return RedirectToAction("Index");

        }

        [HttpPost]

        public async Task<IActionResult> Delete(string Id)
        {
            var role = _myDBContext.Roles.Find(Id);
            if (role == null)
            {
                return RedirectToAction("Index");

            }

            var UsersRole = _myDBContext.UserRoles.Where(x => x.RoleId == Id).Count();
            if (UsersRole > 0)
            {
            TempData["Error"] = "THis Role Is Can not Deleted, Assigned To User";
                return RedirectToAction("Index");
            }
            var reslut = await _roleManager.DeleteAsync(role);
            if (reslut.Succeeded)
            {
                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");

        }
    }
}