using System.Buffers.Text;
using System.Reflection.Metadata;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocilaMediaProject.Interfaces;
using SocilaMediaProject.Models;
using SocilaMediaProject.ViewModels;
using SocilaMediaProject.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SocilaMediaProject.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly MyDBContext _myDBContext;

        public UserController(ILogger<UserController> logger, MyDBContext myDBContext, IUserRepository userRepository, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _userRepository = userRepository;
            _userManager = userManager;
            _myDBContext = myDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> UserDetails(string id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null) return RedirectToAction("Index", "User");

            var UservM = new UserDetailsViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Pace = user.Pace,
                Mileage = user.Mileage,
                City = user.Address?.City,
                State = user.Address?.State,
                ProfileImageUrl = user.ProfileImageUrl ?? "/img/avatar-male-4.jpg",
            };

            return View(UservM);
        }
        public async Task<IActionResult> Index()
        {
            var usersRoles = _myDBContext.UserRoles.ToList();
            var Users = await _userRepository.GetAllUsers();
            foreach (var user in Users)
            {
                var role = usersRoles.FirstOrDefault(x => x.UserId == user.Id);
                if (role == null)
                {
                    user.Role = "None";

                }
                else
                {
                    user.Role = _myDBContext.Roles.FirstOrDefault(x => x.Id == role.RoleId).Name;

                }
            }
            return View(Users);

        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditProfile()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null) return View("Error");
            var editVM = new EditProfileViewModel()
            {
                ProfileImageUrl = user.ProfileImageUrl,
                Pace = user.Pace,
                Mileage = user.Mileage,
                AddressID = user.AddresID,
                Address = user.Address,

            };
            return View(editVM);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditProfile(EditProfileViewModel EditVM)
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "faild to Edit profile");
                return View(EditVM);
            }
            else
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return View("Error");
                }
                else
                {
                    if (!string.IsNullOrEmpty(EditVM.Image?.FileName))
                    {
                        var result = _userRepository.UploadFile(EditVM.Image);
                        user.ProfileImageUrl = result.ToString();
                    }

                    user.Pace = EditVM.Pace;
                    user.Mileage = EditVM.Mileage;
                    user.Address.City = EditVM.Address?.City;
                    user.Address.State = EditVM.Address?.State;
                }
                _userRepository.Update(user);
            }
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet]
        public IActionResult EditRole(string Id)
        {
            var user = _myDBContext.Users.Find(Id);
            if (user == null) return NotFound();

            var UserRole = _myDBContext.UserRoles.FirstOrDefault(x => x.UserId == user.Id);
            if (UserRole != null)
            {
                user.RoleId = _myDBContext.Roles.FirstOrDefault(x => x.Id == UserRole.RoleId).Id;
            }
            user.RoleList = _myDBContext.Roles.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id
            });
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(AppUser user)
        {
            if (ModelState.IsValid)
            {
                var userDB = _myDBContext.Users.FirstOrDefault(x => x.Id == user.Id);
                if (userDB == null) return NotFound();
                var UserRole = _myDBContext.UserRoles.FirstOrDefault(x => x.UserId == userDB.Id);

                if(UserRole !=null){
                    var prviousRoleName = _myDBContext.Roles.Where(x=>x.Id == UserRole.RoleId).Select(e=>e.Name).FirstOrDefault();
                  var x=  await _userManager.RemoveFromRoleAsync(userDB,prviousRoleName);
                } 

               var newuser= await _userManager.AddToRoleAsync(userDB, _myDBContext.Roles.FirstOrDefault(x => x.Id == user.RoleId).Name);
                _myDBContext.SaveChanges();
            }
            return RedirectToAction("Index");

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}